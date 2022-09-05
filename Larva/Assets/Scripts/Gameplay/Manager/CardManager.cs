using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    public delegate void CardActionEventDelegate(CardObject card);

    public class CardManager : NetworkBehaviour
    {
        public event CardActionEventDelegate CardActionEvent;

        public List<CardData> CardDeck = new();

        [Command]
        public void CmdGetCard(NetworkIdentity identity, Player player)
        {}

        [Command]
        public void CmdPutCard(Player player, CardObject card)
        {}

        [ClientRpc]
        public void RpcRunNextCardAction(CardObject card)
        {}

        public void SendNextCardAction()
        {}

        public bool IsEmpty() => this.puttedCards.IsEmpty();

        private PriorityQueue<uint, CardObject> puttedCards = new((a, b) => a.Priority < b.Priority);
    }
}
