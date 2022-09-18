using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    /// <summary>
    /// 플레이어의 소속 팀을 구분함
    /// </summary>
    public enum TeamType
    {
        /// <summery>
        /// 시민팀 + 마피아팀 모두 포함
        /// </summery>
        Both,

        /// <summery>
        /// 시민팀
        /// </summery>
        Citizen,

        /// <summery>
        /// 마피아팀
        /// </summery>
        Mafia
    }

    /// <summary>
    /// 게임 플레이어
    /// </summary>
    public class Player : NetworkBehaviour
    {
        // ##########################################
        //  Public Properties
        // ##########################################

        /// <summary>
        /// 사용자 이름 (PlayerData로 부터 설정됨)
        /// </summary>
        [SyncVar]
        public string Username;

        /// <summary>
        /// 사용자 개인 색상 (PlayerData로 부터 설정됨)
        /// </summary>
        [SyncVar]
        public Color PersonalColor;

        /// <summary>
        /// 플레이어 소속 팀
        /// </summary>
        [SyncVar]
        public TeamType Team;

        /// <summary>
        /// 플레이어 생존 여부 (true - 생존, false - 사망)
        /// </summary>
        [SyncVar]
        public bool IsAlive;

        /// <summary>
        /// 플레이어가 소유한 카드들
        /// </summary>
        /// <returns></returns>
        public List<CardIdentity> Cards = new();

        // ##########################################
        //  MonoBehaviour Life Cycle
        // ##########################################

        private void Awake()
        {
            if (this.isLocalPlayer)
            {
                // this.Username = PlayerData.Username;
                // this.PersonalColor = PlayerData.PersonalColor;
            }
        }

        // ##########################################
        //  Card
        // ##########################################

        /// <summary>
        /// 플레이어가 새로운 카드를 요청하는 함수
        /// </summary>
        [Command]
        public void CmdGetCard()
        {
            // [REQUEST TO]
            // -> CardManager.GetCard

            CardManager.Singleton.GetCard(this.netIdentity, this);
        }

        /// <summary>
        /// 플레이어가 소유한 카드들을 조합하여 새로운 카드를 요청하는 함수
        /// </summary>
        /// <param name="cards">조합 카드들</param>
        [Command]
        public void CmdGetCardWithCombination(long[] cardIds)
        {
            // [REQUEST TO]
            // -> CardManager.GetCardWithCombination

            CardManager.Singleton.GetCardWithCombination(this.netIdentity, this, cardIds);
        }

        /// <summary>
        /// CmdGetCard, CmdGetCardWithCombination 응답 함수
        /// </summary>
        /// <param name="conn">클라이언트 연결</param>
        /// <param name="cardId">카드 ID (음수인 경우 오류)</param>
        /// <param name="cardName">카드 이름 (오류인 경우 빈 문자열)</param>
        /// <param name="responseMessage">응답 메세지</param>
        [TargetRpc]
        public void TargetGetCardResponse(NetworkConnection conn, long cardId, string cardName, string responseMessage)
        {
            // [RESPONSE FROM]
            // CardManager.GetCard -> 
            // cardManager.GetCardCombination -> 

            if (cardId < 0)
            {
                // TODO: 예외 처리 코드 추가
                Debug.LogWarning("Get Card Failed");
                return;
            }

            // 카드 게임 오브젝트 생성
            var gameObject = new GameObject(cardName);

            var cardIdentity = gameObject.AddComponent<CardIdentity>();
            cardIdentity.CardId = cardId;
            cardIdentity.CardName = cardName;
            cardIdentity.Owner = this;
            
            gameObject.AddComponent<CardObject>();

            // 새로운 카드 추가
            this.Cards.Add(cardIdentity);
        }

        /// <summary>
        /// 플레이어가 소유한 카드를 사용하기 위해 내놓는 함수
        /// </summary>
        /// <param name="card">사용할 카드</param>
        [Command]
        public void CmdPutCard(long cardId)
        {
            // [REQUEST TO]
            // -> CardManager.PutCard

            CardManager.Singleton.PutCard(this.netIdentity, this, cardId);
        }

        /// <summary>
        /// CmdPutCard 응답 함수
        /// </summary>
        /// <param name="conn">클라이언트 연결</param>
        /// <param name="cardIdOrNull">카드 ID 혹은 NULL (NULL인 경우 오류)</param>
        /// <param name="responseMessage">응답 메세지</param>
        [TargetRpc]
        public void TargetPutCardResponse(NetworkConnection conn, long cardIdOrNull, string responseMessage)
        {
            // [RESPONSE FROM]
            // CardManager.PutCard -> 

            if (cardIdOrNull < 0)
            {
                // TODO: 예외 처리 코드 추가
                return;
            }

            // 해당 카드 삭제
            var card = this.Cards.Find(card => card.CardId == cardIdOrNull);
            this.Cards.Remove(card);
        }

        // ##########################################
        //  Vote
        // ##########################################

        /// <summary>
        /// 플레이어의 투표 요청
        /// </summary>
        /// <param name="toPlayer">투표 당한 플레이어</param>
        [Command]
        public void CmdVote(Player toPlayer)
        {
            // [REQUEST TO]
            // -> VoteManager.Vote

            VoteManager.Singleton.Vote(this.netIdentity, this, toPlayer);
        }

        /// <summary>
        /// 플레이어 투표 요청 응답
        /// </summary>
        /// <param name="target">네트워크 Target</param>
        /// <param name="result">투표 요청 결과</param>
        /// <param name="message">에러 메세지</param>
        [TargetRpc]
        public void TargetVoteResponse(NetworkConnection target, bool result, string errorMessage)
        {
            // [RESPONSE FROM]
            // VoteManager.Vote -> 

            // TODO: 투표에 실패한 경우 예외 처리 코드 추가
        }
    }
}
