using UnityEngine;
using Mirror;

namespace Larva
{
    public class CardObject : NetworkBehaviour
    {
        public CardData Data;
        public Player Owner;

        public virtual void CardAction()
        {}
    }
}
