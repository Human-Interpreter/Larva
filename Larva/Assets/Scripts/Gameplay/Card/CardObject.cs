using UnityEngine;
using Mirror;

namespace Larva
{
    class CardObject : NetworkBehaviour
    {
        public CardData Data;
        public Player Owner;

        public virtual void CardAction()
        {}
    }
}
