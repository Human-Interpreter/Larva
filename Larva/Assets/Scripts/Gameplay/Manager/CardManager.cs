using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    delegate void CardActionEventDelegate(CardObject card);

    public class CardManager : NetworkBehaviour
    {
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

        public void IsEmpty() => this.puttedCards.IsEmpty();

        private PriorityQueue<uint, CardObject> puttedCards = new((a, b) => a.Priority < b.Priority);
    }
}
