using System.Collections.Generic;
using UnityEngine;

namespace Larva
{
    /// <summary>
    /// 카드 식별을 위한 클래스
    /// </summary>
    public class CardIdentity : MonoBehaviour
    {
        /// <summary>
        /// 서버에서 생성된 카드의 번호
        /// </summary>
        public long CardId;

        /// <summary>
        /// 카드 소유자
        /// </summary>
        public Player Owner;

        /// <summary>
        /// 카드 이름 (CardData.Name)
        /// </summary>
        public string CardName;

        /// <summary>
        /// CardName의 카드 데이터를 찾아서 반환
        /// </summary>
        /// <returns>카드 데이터</returns>
        public CardData GetData() => CardManager.Singleton.CardDeck.Find(card => card.Name == this.CardName);

        /// <summary>
        /// 카드 사용에 필요한 데이터
        /// (Mirror를 이용한 네트워크 전송 가능)
        /// </summary>
        public Dictionary<string, object> Parameters = new();
    }
}
