using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    /// <summary>
    /// 게임 상태 표현 (여러 상태가 중첩될 수 있음)
    /// </summary>
    [Flags]
    public enum GameState
    {
        /// <summery>
        /// 게임 플레이 상태
        /// </summery>
        Playing = 1,

        /// <summery>
        /// 게임 일시정지 상태 (네트워크 딜레이)
        /// </summery>
        Pausing = 2,

        /// <summery>
        /// 게임 오버 상태 (곧 게임이 중단됨)
        /// </summery>
        GameOvered = 4,

        /// <summery>
        /// 게임 중단 상태
        /// </summery>
        Stopped = 8,
    }

    /// <summary>
    /// 게임 진행 단계 표현
    /// </summary>
    public enum GamePhase
    {
        /// <summery>
        /// 1. 낮, 시민 토론
        /// </summery>
        DayDebate,

        /// <summery>
        /// 2. 낮, 시민 투표
        /// </summery>
        DayVote,

        /// <summery>
        /// 3. 낮, 최후 변론
        /// </summery>
        DayFinalDebate,

        /// <summery>
        /// 4. 낮, 최종 투표
        /// </summery>
        DayFinalVote,

        /// <summery>
        /// 5. 밤, 카드를 내거나 새로운 카드를 습득
        /// (마피아 중 한 명은 살해할 플레이어를 한 명 선택한다)
        /// </summery>
        NightCarding,

        /// <summery>
        /// 6. 밤, 카드 결과 확인
        /// </summery>
        NightCardResult
    }

    /// <summary>
    /// GameManager (Singleton)
    /// 게임 전반적인 진행과 플레이어 상호작용을 도와주는 기능을 제공함.
    /// </summary>
    public class GameManager : NetworkBehaviour
    {
        /// <summary>
        /// GameManager Singleton
        /// </summary>
        public static GameManager Singleton = null;

        /// <summary>
        /// 현재 게임 턴을 저장함.
        /// </summary>
        [SyncVar]
        public uint CurrentTurn;

        /// <summary>
        /// 게임 상태를 저장함.
        /// </summary>
        [SyncVar]
        public GameState State;

        /// <summary>
        /// 게임 진행도를 저장함.
        /// </summary>
        [SyncVar]
        public GamePhase Phase;

        /// <summary>
        /// 게임 총 플레이 시간을 저장함.
        /// </summary>
        [SyncVar]
        public float PlayTime;
        
        /// <summary>
        /// 다음 Phase까지 남은 시간을 저장함.
        /// (PhaseTimeout이 0이하가 되면 다음 Phase로 전환됨)
        /// </summary>
        [SyncVar]
        public float PhaseTimeout;

        /// <summary>
        /// Turn이 변경될 때 트리거 되는 이벤트
        /// </summary>
        public event EventHandler TurnChangeEvent;

        /// <summary>
        /// State가 변경될 때 트리거되는 이벤트
        /// </summary>
        public event EventHandler StateChangeEvent;

        /// <summary>
        /// Phase가 변경될 때 트리거 되는 이벤트
        /// </summary>
        public event EventHandler PhaseChangeEvent;

        /// <summary>
        /// 플레이어를 찾아주는 함수 (uREPL 명령어)
        /// </summary>
        /// <param name="netId">플레이어 netId (기본값 -1)</param>
        /// <returns>파라미터로 netId을 넣어준 경우에 플레이어 객체 반환, netId가 -1이면 null 반환</returns>
        [uREPL.Command]
        static public Player GetPlayer(int netId = -1)
        {
            var players = FindObjectsOfType<Player>();

            foreach (var player in players)
            {
                if (netId < 0)
                {
                    uREPL.Log.Output(player.netId.ToString());
                }
                else if (netId == player.netId)
                {
                    return player;
                }
            }

            return null;
        }

        private void Awake()
        {
            // Singleton 패턴
            if (GameManager.Singleton != null && GameManager.Singleton == this)
            {
                // 현재 Scene에 이미 GameManager가 존재한다면 지금 GameManaer는 Destroy함.
                Debug.LogWarning("GameManager가 이미 존재하기 때문에 현재 GameManager는 파괴되었습니다.");

                Destroy(this.gameObject);
            }
            else
            {
                // 현재 Scene에 GameManager가 존재하지 않는다면 지금 GameManager를 Singleton으로 설정함.
                GameManager.Singleton = this;
            }
        }

        private void Update()
        {
            if (!this.isServer)
            {
                return;
            }

            // TODO: PlayTime 증가

            // TODO: PhaseTimeout 감소

            // TODO: PhaseTimeout이 0이하가 되면 다음 Phase로 변경
            // TODO: Phase가 변경되면 PhaseChangeEvent 트리거

            // TODO: Phase 사이클이 한 번 돌고 나면 CurrentTurn 증감
            // TODO: CurrentRurn가 변경되면 TurnChangeEvent 트리거

            // TODO: 현재 Phase를 확인하고 각 Phase에 적합한 행동 진행

            // TODO: NightCardResult Phase가 종료되기 전에 모든 Player의 IsProtected를 false로 변경
        }
    }
}
