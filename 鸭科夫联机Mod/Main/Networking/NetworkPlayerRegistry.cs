using System;
using System.Collections.Generic;
using LiteNetLib;
using UnityEngine;

namespace 鸭科夫联机Mod
{
    internal sealed class NetworkPlayerRegistry
    {
        public Dictionary<NetPeer, PlayerStatus> ServerStatuses { get; } = new();
        public Dictionary<NetPeer, GameObject> ServerCharacters { get; } = new();
        public Dictionary<string, PlayerStatus> ClientStatuses { get; } = new();
        public Dictionary<string, GameObject> ClientCharacters { get; } = new();

        public void ResetForNetworkStart()
        {
            ServerStatuses.Clear();
            ServerCharacters.Clear();
            ClientStatuses.Clear();
            ClientCharacters.Clear();
        }

        public void ClearAndDestroyCharacters(Action<GameObject> destroyAction)
        {
            if (destroyAction == null)
            {
                ServerCharacters.Clear();
                ClientCharacters.Clear();
                return;
            }

            foreach (var go in ServerCharacters.Values)
            {
                if (go != null)
                {
                    destroyAction(go);
                }
            }
            ServerCharacters.Clear();

            foreach (var go in ClientCharacters.Values)
            {
                if (go != null)
                {
                    destroyAction(go);
                }
            }
            ClientCharacters.Clear();
        }

        public void ClearAll(Action<GameObject> destroyAction)
        {
            ClearAndDestroyCharacters(destroyAction);
            ServerStatuses.Clear();
            ClientStatuses.Clear();
        }
    }
}

