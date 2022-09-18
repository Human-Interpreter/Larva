using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    /// <summary>
    /// 카드 액션 이벤트에 호출되는 델리게이트
    /// </summary>
    /// <param name="card">이벤트를 호출한 카드</param>
    public delegate void CardActionEventDelegate(CardObject card);

    /// <summary>
    /// CardManager
    /// 카드 게임에서 딜러 역할을 하는 클래스
    /// (플레이어에게 카드를 나눠주고, 카드를 받아서 액션 함수를 호출함)
    /// </summary>
    public class CardManager : NetworkBehaviour
    {
        /// <summary>
        /// CardManager Singleton
        /// </summary>
        public static CardManager Singleton = null;

        /// <summary>
        /// 카드 액션이 발생할때 트리거되는 이벤트
        /// </summary>
        public event CardActionEventDelegate CardActionEvent;

        /// <summary>
        /// 게임에 사용되는 카드 데이터를 보관함.
        /// </summary>
        public List<CardData> CardDeck = new();

        /// <summary>
        /// 플레이어가 소유하고 있는 카드를 보관함.
        /// </summary>
        private Dictionary<long, CardIdentity> playerCards = new();


        private void Awake()
        {
            // Singleton 패턴
            if (CardManager.Singleton != null && CardManager.Singleton == this)
            {
                // 현재 Scene에 이미 CardManager가 존재한다면 지금 CardManager는 Destroy함.
                Debug.LogWarning("CardManager가 이미 존재하기 때문에 현재 CardManager는 파괴되었습니다.");

                Destroy(this.gameObject);
            }
            else
            {
                // 현재 Scene에 CardManager가 존재하지 않는다면 지금 CardManager를 Singleton으로 설정함.
                CardManager.Singleton = this;
            }
        }

        /// <summary>
        /// 플레이어가 새로운 카드를 요청하는 함수
        /// </summary>
        /// <param name="identity">Player Identity</param>
        /// <param name="player">Player</param>
        [Command]
        public void CmdGetCard(NetworkIdentity identity, Player player)
        {}

        /// <summary>
        /// 플레이어가 소유한 카드들을 조합하여 새로운 카드를 요청하는 함수
        /// </summary>
        /// <param name="identity">Player Identity</param>
        /// <param name="player">Player</param>
        /// <param name="cards">조합 카드들</param>
        [Command]
        public void CmdGetCardWithCombination(NetworkIdentity identity, Player player, CardObject[] cards)
        {}

        /// <summary>
        /// 플레이어가 소유한 카드를 사용하기 위해 내놓은 함수
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="card">사용할 카드</param>
        [Command]
        public void CmdPutCard(Player player, CardObject card)
        {}

        /// <summary>
        /// 사용대기 중인 카드 중에서 가장 우선순위가 높은 카드를 한장 선택하여 액션 함수를 호출함.
        /// </summary>
        /// <param name="card">사용을 위해 선택된 카드</param>
        [ClientRpc]
        public void RpcRunNextCardAction(CardObject card)
        {}

        /// <summary>
        /// 서버에서 사용대기 중인 카드 중에서 가장 우선순휘가 높은 카드를 한장 선택한 다음 
        /// 클라이언트에서 실행될 수 있도록 RpcRunNextCardAction 함수를 호출함.
        /// </summary>
        public void SendNextCardAction()
        {
            // TODO: PriorityQueue.Pop() 도중에 예외가 발생할 수 있으므로 예외 처리 혹은 IsEmpty 함수를 적절히 활용하여 구현
        }

        /// <summary>
        /// puttedCards에 남은 카드가 있는지 여부를 반환한다.
        /// </summary>
        /// <returns>남은 카드가 없다면 true 반환</returns>
        public bool IsEmpty() => this.puttedCards.IsEmpty();

        /// <summary>
        /// 사용대기 중인 카드들을 우선순위에 따라서 오름차순 정렬해 저장함.
        /// </summary>
        private PriorityQueue<uint, CardObject> puttedCards = new((a, b) => a.Priority < b.Priority);
    }
}
