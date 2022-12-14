using UnityEngine;

namespace Larva
{
    /// <summary>
    /// 카드의 정보를 보관 (ScriptableObject)
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Card Data", fileName = "CardData", order = int.MaxValue)]
    public class CardData : ScriptableObject
    {
        /// <summary>
        /// 카드의 이름 (카드의 이름은 고유해야 함)
        /// </summary>
        public string Name;

        /// <summary>
        /// 카드 효과 설명
        /// </summary>
        [TextArea]
        public string Explanation;
        
        /// <summary>
        /// 해당 카드의 레어카드 여부를 저장
        /// (true - 레어카드, false - 일반카드)
        /// </summary>
        public bool IsRareCard;
        
        /// <summary>
        /// 해당 카드를 소유할 수 있는 팀
        /// </summary>
        public TeamType Team;
        
        /// <summary>
        /// 카드의 그림 (네트워크를 통한 공유 금지)
        /// </summary>
        public Sprite FrontSprite;
        
        /// <summary>
        /// 카드가 한 번 사용된 이후 일정 턴이 지난 후에 다시 카드를 사용할 수 있도록 함
        /// </summary>
        public uint CooldownTurn;
        
        /// <summary>
        /// 카드 액션 함수의 실행 우선순위
        /// </summary>
        public int Priority;

        /// <summary>
        /// 카드 액션 클래스 이름
        /// </summary>
        public string ActionClassName;
    }
}
