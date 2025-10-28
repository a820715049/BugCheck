using System;
using System.Collections.Generic;

namespace 鸭科夫联机Mod
{
    internal sealed class LanDiscoveryController
    {
        private readonly List<string> _hosts = new List<string>();
        private readonly HashSet<string> _hostSet = new HashSet<string>();

        private float _broadcastTimer;

        public bool IsConnecting { get; private set; }
        public string Status { get; private set; } = "未连接";
        public string ManualIP { get; set; } = "127.0.0.1";
        public string ManualPort { get; set; } = "9050";
        public float BroadcastInterval { get; set; } = 5f;

        public IReadOnlyList<string> Hosts => _hosts;
        public int HostCount => _hosts.Count;

        public void ResetForNetworkStart()
        {
            Status = "网络已启动";
            ResetHosts();
            IsConnecting = false;
            _broadcastTimer = 0f;
        }

        public void ResetHosts()
        {
            _hosts.Clear();
            _hostSet.Clear();
        }

        public bool TryAddHost(string host)
        {
            if (string.IsNullOrWhiteSpace(host) || !_hostSet.Add(host))
            {
                return false;
            }

            _hosts.Add(host);
            return true;
        }

        public void Tick(float deltaTime, Action broadcastAction)
        {
            if (IsConnecting)
            {
                return;
            }

            _broadcastTimer += deltaTime;
            if (_broadcastTimer >= BroadcastInterval)
            {
                _broadcastTimer = 0f;
                broadcastAction?.Invoke();
            }
        }

        public void BeginConnecting(string ip, int port)
        {
            Status = $"连接中: {ip}:{port}";
            IsConnecting = true;
        }

        public void SetStatus(string status)
        {
            Status = status;
            IsConnecting = false;
            _broadcastTimer = 0f;
        }

        public void SetError(string status)
        {
            SetStatus(status);
        }
    }
}

