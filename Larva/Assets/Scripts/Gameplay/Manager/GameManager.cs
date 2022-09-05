using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    [Flags]
    public enum GameState
    {
        Playing = 1,      // 게임 플레이 상태
        Pausing = 2,      // 게임 일지정지 상태 (네트워크 딜레이)
        GameOvered = 4,   // 게임 오버 상태
        Stopped = 8,      // 게임 중단 상태
    }

    public enum GamePhase
    {
        DayDebate,         // 1. 낮 시민토론
        DayVote,           // 2. 낮 시민투표
        DayFinalDebate,    // 3. 낮 추후변론
        DayFinalVote,      // 4. 낮 최후투표
        NightCarding,      // 5. 밤 카드 (마피아 중 한명이 살해할 플레이어를 한명 선택한다)
        NightCardResult    // 6. 밤 카드 결과 확인
    }

    public class GameManager : NetworkBehaviour
    {
        public static GameManager Singleton = null;

        [SyncVar]
        public uint CurrentTurn;

        [SyncVar]
        public GameState State;

        [SyncVar]
        public GamePhase Phase;

        [SyncVar]
        public float PlayTime;
        
        [SyncVar]
        public float PhaseTimeout;

        [SyncVar]
        public CardManager Card;

        [SyncVar]
        public VoteManager Vote;

        private void Awake()
        {
            // Singleton
            if (GameManager.Singleton != null && GameManager.Singleton == this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                GameManager.Singleton = this;
            }
        }
    }
}
