using UnityEngine;

namespace Larva
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Card Data", fileName = "CardData", order = int.MaxValue)]
    public class CardData : ScriptableObject
    {
        public string Name;
        public bool IsRareCard;
        public TeamType Team;
        public Sprite FrontSprite;
        public uint CooldownTurn;
        public int Priority;
    }
}
