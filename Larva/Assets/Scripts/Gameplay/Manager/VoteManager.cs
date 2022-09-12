using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    /// <summary>
    /// 게임 내 투표를 관리
    /// </summary>
    public class VoteManager : NetworkBehaviour
    {
        /// <summary>
        /// VoteManager Singleton
        /// </summary>
        public static VoteManager Singleton = null;
        
        /// <summary>
        /// 현재 투표 가능 여부
        /// </summary>
        public bool IsVotable = false;

        /// <summary>
        /// 투표 가능한 팀 구분
        /// </summary>
        public TeamType VotableTeam;

        private void Awake()
        {
            // Singleton 패턴
            if (VoteManager.Singleton != null && VoteManager.Singleton == this)
            {
                // 현재 Scene에 이미 VoteManager가 존재한다면 지금 VoteManager는 Destroy함.
                Debug.LogWarning("VoteManager가 이미 존재하기 때문에 현재 VoteManager는 파괴되었습니다.");

                Destroy(this.gameObject);
            }
            else
            {
                // 현재 Scene에 VoteManager가 존재하지 않는다면 지금 VoteManager를 Singleton으로 설정함.
                VoteManager.Singleton = this;
            }
        }

        /// <summary>
        /// 투표 초기화
        /// </summary>
        /// <param name="votableTeam">투표 가능한 팀 설정</param>
        public void Reset(TeamType votableTeam)
        {}

        /// <summary>
        /// 플레이어의 투표 요청
        /// </summary>
        /// <param name="identity">네트워크 ID</param>
        /// <param name="fromPlayer">투표한 플레이어</param>
        /// <param name="toPlayer">투표 당한 플레이어</param>
        [Command]
        public void CmdVote(NetworkIdentity identity, Player fromPlayer, Player toPlayer)
        {}

        /// <summary>
        /// 플레이어 투표 요청 응답
        /// </summary>
        /// <param name="target">네트워크 Target</param>
        /// <param name="result">투표 요청 결과</param>
        /// <param name="message">에러 메세지</param>
        [TargetRpc]
        public void TargetError(NetworkConnection target, bool result, string message)
        {}

        /// <summary>
        /// 투표 결과를 모든 플레이어에게 공지
        /// </summary>
        /// <param name="player">최다 득표수 플레이어</param>
        /// <param name="count">득표수</param>
        [ClientRpc]
        public void RpcVoteResult(Player player, int count)
        {}

        /// <summary>
        /// 투표 결과 보관
        /// </summary>
        private Dictionary<Player, Player> voteData = new();
    }
}
