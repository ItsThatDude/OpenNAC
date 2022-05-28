using Flexinets.Net;
using Flexinets.Radius;
using Flexinets.Radius.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OpenNAC.Core.Policies;
using OpenNAC.Core.Radius;
using OpenNAC.Vendors.Aruba.Radius;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OpenNAC.Service
{
    public class RadiusWorker : IHostedService, IDisposable
    {
        private readonly ILogger<RadiusWorker> _logger;
        private RadiusServer _authenticationServer;
        private RadiusServer _accountingServer;

        private readonly IRadiusClientRepository _radiusClients;
        private readonly IServiceProvider _serviceProvider;

        public RadiusWorker(ILogger<RadiusWorker> logger, IRadiusClientRepository clientRepository, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _radiusClients = clientRepository;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var devices = _radiusClients.GetAll();

                var dictionaryPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/Content/Radius.dictionary";
                var dictionary = new RadiusDictionary(dictionaryPath, NullLogger<RadiusDictionary>.Instance);
                var radiusPacketParser = new RadiusPacketParser(NullLogger<RadiusPacketParser>.Instance, dictionary);
                var repository = new PacketHandlerRepository();
                var udpClientFactory = new UdpClientFactory();

                foreach(var device in devices)
                {
                    IPacketHandler handler;

                    switch(device.PacketHandler.ToLowerInvariant())
                    {
                        case "aruba":
                            handler = _serviceProvider.GetService<ArubaPacketHandler>();
                            break;
                        default:
                            handler = _serviceProvider.GetService<GenericPacketHandler>();
                            break;
                    }

                    repository.AddPacketHandler(device.IPAddress, handler, device.SharedSecret);
                }

                _logger.LogInformation("Starting Authentication Server...");
                _authenticationServer = new RadiusServer(
                    new UdpClientFactory(),
                    new IPEndPoint(IPAddress.Any, 1812),
                    radiusPacketParser,
                    RadiusServerType.Authentication,
                    repository,
                    NullLogger<RadiusServer>.Instance);
                _authenticationServer.Start();
                _logger.LogInformation("Authentication Server Started");

                _logger.LogInformation("Starting Accounting Server...");
                _accountingServer = new RadiusServer(
                   udpClientFactory,
                   new IPEndPoint(IPAddress.Any, 1813),
                   radiusPacketParser,
                   RadiusServerType.Accounting,
                   repository,
                   NullLogger<RadiusServer>.Instance);
                _accountingServer.Start();
                _logger.LogInformation("Accounting Server Started");
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Failed to start the Radius Worker Service.", ex);
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if(_accountingServer != null && _accountingServer.Running)
                _accountingServer.Stop();

            if (_authenticationServer != null && _authenticationServer.Running)
                _authenticationServer.Stop();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_accountingServer != null)
                _accountingServer.Dispose();

            if (_authenticationServer != null)
                _authenticationServer.Dispose();
        }
    }
}
