using UnityEngine;

namespace Larva
{
    /// <summary>
    /// 살해 카드 클래스
    /// </summary>
    public class KillCard : MonoBehaviour
    {
        /// <summary>
        /// Card Identity
        /// </summary>
        CardIdentity cardIdentity;

        void Start()
        {
            // CardIdentity component를 가져온다.
            cardIdentity = GetComponent<CardIdentity>();

            // CardIdentity.Parameters 확인
            Debug.Assert(cardIdentity.Parameters.ContainsKey("TargetPlayer"), "KillCard는 TargetPlayer 파라미터를 필요로 합니다.");
            Debug.Assert(cardIdentity.Parameters["TargetPlayer"].GetType() == typeof(Player), "KillCard의 TargetPlayer 파라미터는 Player 클래스를 타입으로 해야 합니다.");

            // 목표 플레이어를 가져온다.
            Player targetPlayer = (Player)cardIdentity.Parameters["TargetPlayer"];

            // 방어 효과 적용여부 판단
            if (!targetPlayer.IsProtected)
            {
                // 목표 플레이어 살해
                targetPlayer.IsAlive = false;

                // 해당 카드 삭제
                Destroy(this.gameObject);
            } else {
                // 목표 플레이어 방어카드 효과 해제
                targetPlayer.IsProtected = false;
            }
        }
    }
}
