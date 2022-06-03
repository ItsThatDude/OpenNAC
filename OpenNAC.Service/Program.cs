using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenNAC.Core.Authentication;
using OpenNAC.Core.Endpoints;
using OpenNAC.Core.Policies;
using OpenNAC.Core.Policies.Conditions;
using OpenNAC.Core.Policies.Rules;
using OpenNAC.Core.Radius;
using OpenNAC.Vendors.Aruba.Radius;
using System.Net;

namespace OpenNAC.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.TimestampFormat = "[hh:mm:ss] ";
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<GenericPacketHandler>();
                    services.AddSingleton<ArubaPacketHandler>();
                    services.AddSingleton<IAccessPolicyRepository, InMemoryAccessPolicyRepository>(configure =>
                    {
                        var policy = new AccessPolicy("Test Policy", 0)
                        {
                            Enabled = true,
                            EnableAccounting = true,
                            ConditionMatchPolicy = CollectionMatchRule.MATCH_ANY,
                            RuleMatchPolicy = CollectionMatchRule.MATCH_ANY
                        };

                        policy.AddCondition(new AttributeCondition("Filter-Id", Comparator.EQUAL_TO, "Test"));
                        policy.AddAuthenticationMethod(new PAPAuthenticationMethod());
                        policy.AddAuthenticationSource(new TestAuthenticationSource());
                        policy.AddRule(new PolicyRule("User-Name", Comparator.EQUAL_TO, "user@example.com", new PolicyRuleAction[]
                        {
                            new AcceptRequestAction(),
                            new AddRadiusAttributeAction("User-Role", "Test"),
                            new AddRadiusAttributeAction("Filter-Id", (context) => context.SourceAddress.ToString())
                        }));

                        return new InMemoryAccessPolicyRepository(new[]
                        {
                            policy
                        });
                    });
                    services.AddSingleton<IRadiusClientRepository, InMemoryRadiusClientRepository>(configure =>
                    {
                        return new InMemoryRadiusClientRepository(new[]
                        {
                            new RadiusClient { IPAddress = new IPAddress(new byte[] { 192, 168, 1, 33 }), Name = "Aruba RADIUS Device", PacketHandler = "Aruba", SharedSecret = "Test1234" },
                            new RadiusClient { IPAddress = IPAddress.Any, Name = "Generic RADIUS Device", PacketHandler = "Default", SharedSecret = "Test1234" }
                        });
                    });

                    services.AddSingleton<IEndpointRepository, InMemoryEndpointRepository>();

                    services.AddHostedService<RadiusWorker>();
                });
    }
}
