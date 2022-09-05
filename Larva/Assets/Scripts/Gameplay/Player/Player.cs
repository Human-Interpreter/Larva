using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    public enum TeamType
    {
        Both,
        Citizen,
        Mafia
    }

    public class Player : NetworkBehaviour
    {
        [SyncVar]
        public string Username;

        [SyncVar]
        public Color PersonalColor;

        [SyncVar]
        public TeamType Team;

        [SyncVar]
        public bool IsAlive;

        [SyncVar]
        public List<CardObject> Cards = new();

        [TargetRpc]
        public void TargetGetCardResult(NetworkConnection target, CardObject cardOrNull)
        {}
    }
}
