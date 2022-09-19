using System;
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

        /// <summary>
        /// 투표 초기화 될 때 트리거되는 이벤트
        /// </summary>
        public event EventHandler ResetEvent;

        /// <summary>
        /// 플레이어가 투표를 할 때 트리거되는 이벤트
        /// </summary>
        public event EventHandler PlayerVoteEvent;

        /// <summary>
        /// 투표 결과가 공지될 때 트리거되는 이벤트
        /// </summary>
        public event EventHandler VoteResultEvent;

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
        /// 투표 초기화 (새로운 투표 시작)
        /// </summary>
        /// <param name="votableTeam">투표 가능한 팀 설정</param>
        [Server]
        public void Reset(TeamType votableTeam)
        {
            this.voteData.Clear();
            this.IsVotable = true;
            this.VotableTeam = votableTeam;

            // TODO: ResetEvent 이벤트 트리거
        }

        /// <summary>
        /// 플레이어의 투표 요청
        /// </summary>
        /// <param name="identity">네트워크 ID</param>
        /// <param name="fromPlayer">투표한 플레이어</param>
        /// <param name="toPlayer">투표 당한 플레이어</param>
        [Server]
        public void Vote(NetworkIdentity identity, Player fromPlayer, Player toPlayer)
        {
            // [REQUEST FROM]
            // fromPlayer.CmdVote -> 

            // TODO: 요청이 요효한지 확인 (투표가능 여부, 투표가능 팀, 중복투표 확인)
            // TODO: 투표 요청 반영
            // TODO: PlayerVoteEvent 이벤트 트리거

            // [RESPONSE TO]
            // -> fromPlayer.TargetVoteResponse
            // TODO: fromPlayer.TargetVoteResponse()
        }

        /// <summary>
        /// 투표 결과를 플레이어에게 공지
        /// </summary>
        [Server]
        public void VoteResult()
        {
            // [SEND TO]
            // -> this.RpcVoteResult
            // TODO: this.RpcVoteResult()
            // TODO: VoteResultEvent 이벤트 트리거 (for Server)
        }

        /// <summary>
        /// 투표 결과를 모든 플레이어에게 공지
        /// </summary>
        /// <param name="player">최다 득표수 플레이어</param>
        /// <param name="count">득표수</param>
        [ClientRpc]
        public void RpcVoteResult(Player player, int count)
        {
            // [RECEIVE FROM]
            // this.VoteResult -> 
            // TODO: 투표 결과 공지
            // TODO: VoteResultEvent 이벤트 트리거 (for Client)
        }

        /// <summary>
        /// 투표 결과 보관
        /// </summary>
        private Dictionary<Player, uint> voteData = new();
    }
}
