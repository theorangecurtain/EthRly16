﻿using System;
using System.Net;
using System.Runtime.Loader;
using System.Threading;

namespace Toc.EthRly.Core.Models
{
    public class Options
    {
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromMilliseconds(100);
        private readonly int _defaultPort = 17494;
        private readonly int _defaultCommandQueueLength = 50;
        private readonly TimeSpan _defaultDelayBetweenCommands = TimeSpan.FromMilliseconds(10);
        private readonly int _defaultRelayCount = 8;

        public Options(string hostname, int? port = null, TimeSpan? timeout = null, CancellationToken? token = null)
        {
            Hostname = hostname;
            Port = port ?? _defaultPort;
            Timeout = timeout ?? _defaultTimeout;
            CommandQueueLength = _defaultCommandQueueLength;
            DelayBetweenCommands = _defaultDelayBetweenCommands;
            RelayCount = _defaultRelayCount;
            Token = token ?? getCancellation();
        }

        public string Hostname { get; set; }
        public int Port { get; set; }
        public TimeSpan Timeout { get; set; }
        public int CommandQueueLength { get; set; }
        public TimeSpan DelayBetweenCommands { get; set; }
        public int RelayCount { get; set; }
        public CancellationToken Token { get; set; }
        public IPAddress Ip => IPAddress.Parse(Hostname);
        public IPEndPoint Endpoint => new IPEndPoint(Ip, Port);

        private CancellationToken getCancellation()
        {
            var tokenSource = new CancellationTokenSource();
            AssemblyLoadContext.Default.Unloading += context => tokenSource.Cancel();
            return tokenSource.Token;
        }
    }
}
