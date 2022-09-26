using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
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
        /// 게임에 사용되는 카드 데이터를 보관함.
        /// </summary>
        public List<CardData> CardDeck = new();

        /// <summary>
        /// 플레이어가 소유하고 있는 카드를 보관함.
        /// </summary>
        private Dictionary<long, CardIdentity> playerCards = new();

        /// <summary>
        /// 카드 액션 클래스가 생성될 때 트리거 되는 이벤트
        /// </summary>
        public event EventHandler CardActionEvent;

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
        /// 새로운 카드 식별자를 생성
        /// </summary>
        /// <param name="cardName">카드 이름 (CardData)</param>
        /// <param name="player">플레이어</param>
        /// <returns>카드 식별자</returns>
        [Server]
        private CardIdentity CreateCardIdentity(string cardName, Player player)
        {
            // Add game object
            var gameObject = new GameObject(cardName);
            gameObject.transform.parent = player.transform;

            // Add card identity component
            var cardIdentity = gameObject.AddComponent<CardIdentity>();
            cardIdentity.Owner = player;
            cardIdentity.CardName = cardName;

            // CardId는 현재 UTC시간을 timestamp로 변환한 값을 사용합니다.
            // (Id가 중복되는 현상을 막기 위함)
            cardIdentity.CardId = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            Debug.Log($"CardId: {cardIdentity.CardId}");

            // Add player card
            this.playerCards[cardIdentity.CardId] = cardIdentity;

            // return card identity
            return cardIdentity;
        }

        /// <summary>
        /// 플레이어가 새로운 카드를 요청하는 함수
        /// </summary>
        /// <param name="identity">Player Identity</param>
        /// <param name="player">Player</param>
        [Server]
        public void GetCard(NetworkIdentity identity, Player player)
        {
            // [REQUEST FROM]
            // player.CmdGetCard -> 

            // TODO: 요청에 문제 없는지 확인 후 문제가 있다면 오류 응답을 전송
            // TODO: 카드 추가: this.CreateCardIdentity()

            // [RESPONSE TO]
            // -> player.TargetGetCardResponse
            // TODO: player.TargetGetCardResponse()
        }

        /// <summary>
        /// 플레이어가 소유한 카드들을 조합하여 새로운 카드를 요청하는 함수
        /// </summary>
        /// <param name="identity">Player Identity</param>
        /// <param name="player">Player</param>
        /// <param name="cardIds">조합 카드들 ID</param>
        [Server]
        public void GetCardWithCombination(NetworkIdentity identity, Player player, long[] cardIds)
        {
            // [REQUEST FROM]
            // player.CmdGetCardWithCombination -> 

            // TODO: 요청에 문제 없는지 확인 후 문제가 있다면 오류 응답을 전송
            // TODO: 카드 추가: this.CreateCardIdentity()

            // [RESPONSE TO]
            // -> player.TargetGetCardResponse
            // TODO: player.TargetGetCardResponse()
        }

        /// <summary>
        /// 플레이어가 소유한 카드를 사용하기 위해 내놓은 함수
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="cardId">사용할 카드</param>
        [Server]
        public void PutCard(NetworkIdentity identity, Player player, long cardId)
        {
            // [REQUEST FROM]
            // player.CmdGetCard -> 

            // TODO: 요청에 문제 없는지 확인 후 문제가 있다면 오류 응답을 전송
            // TODO: 대기열에 카드 추가: this.puttedCards.Push()

            // [RESPONSE TO]
            // -> player.TargetPutCardResponse
            // TODO: player.TargetPutCardResponse()
        }

        /// <summary>
        /// 서버에서 사용대기 중인 카드들을 모두 활성화
        /// </summary>
        [Server]
        public void ActiveAllPuttedCards()
        {
            // 대기열에 있는 모든 카드를 조회
            while (!this.IsEmpty())
            {
                // 카드 식별자를 가져옴
                var card = this.puttedCards.Pop().Value;
                // 카드 데이터를 가져옴
                var data = card.GetData();

                // Reflection을 사용해서 카드 액션 클래스를 불러온다.
                Type cardActionType = Type.GetType($"Larva.{data.ActionClassName}");
                
                if (cardActionType == null)
                {
                    // 클래스 명을 찾을 수 없거나 잘못된 클래스 명을 사용.
                    throw new Exception("cardActionType is null");
                }

                if (!typeof(MonoBehaviour).IsAssignableFrom(cardActionType))
                {
                    // 해당 클래스가 MonoBehaviour를 상속 받지 않음.
                    throw new Exception("cardActionType is not assignable from MonoBehaviour");
                }

                // card action 추가
                card.gameObject.AddComponent(cardActionType);

                // TODO: CardActionEvent 이벤트 트리거
            }
        }

        /// <summary>
        /// puttedCards에 남은 카드가 있는지 여부를 반환한다.
        /// </summary>
        /// <returns>남은 카드가 없다면 true 반환</returns>
        public bool IsEmpty() => this.puttedCards.IsEmpty();

        /// <summary>
        /// 사용대기 중인 카드들을 우선순위에 따라서 내림차순 정렬해 저장함.
        /// </summary>
        private PriorityQueue<int, CardIdentity> puttedCards = new((a, b) => a.Priority > b.Priority);
    }
}
