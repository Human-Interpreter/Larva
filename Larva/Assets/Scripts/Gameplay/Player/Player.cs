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

        /// <summary>
        /// CardManager로 부터 카드를 받은 경우 호출되는 함수
        /// </summary>
        /// <param name="target">TargetRpc target</param>
        /// <param name="cardOrNull">새롭게 받은 카드 (null인 경우 카드 요청에 오류가 있었음을 의미)</param>
        /// <param name="errorMessage">cardOrNull값이 null인 경우 에러 사유를 문자열로 받음</param>
        [TargetRpc]
        public void TargetGetCardResult(NetworkConnection target, CardObject cardOrNull, string errorMessage)
        {}
    }
}
