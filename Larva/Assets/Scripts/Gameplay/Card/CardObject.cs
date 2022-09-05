using UnityEngine;
using Mirror;

namespace Larva
{
    /// <summary>
    /// 카드 게임 오브젝트
    /// </summary>
    public class CardObject : NetworkBehaviour
    {
        /// <summary>
        /// 카드 데이터
        /// </summary>
        public CardData Data;

        /// <summary>
        /// 카드 소유 플레이어
        /// </summary>
        public Player Owner;

        /// <summary>
        /// 카드 액션 함수
        /// </summary>
        public virtual void CardAction()
        {}
    }
}
