using UnityEngine;

namespace Larva
{
    /// <summary>
    /// 게임 내부에서 디버깅 메세지를 확인하기 위한 UI (테스트 용)
    /// </summary>
    public class InGameConsole : MonoBehaviour
    {
        /// <summary>
        /// Console 위치와 크기
        /// </summary>
        public Rect ConsoleRect;

        /// <summary>
        /// 오른쪽 기준 위치
        /// </summary>
        public bool IsRightSide = true;

        /// <summary>
        /// 화면 표시 여부
        /// </summary>
        public bool IsVisible = true;

        /// <summary>
        /// 디버깅 메세지
        /// </summary>
        private string debugMessage = "";

        private void Awake()
        {
            // 이벤트 추가
            Application.logMessageReceived += (string logString, string stackTrace, LogType type) => {
                // 디버깅 메세지 추가
                this.debugMessage += type.ToString() + logString + "\n\n";
            };
        }

        private void OnGUI()
        {
            if (!IsVisible)
            {
                return;
            }

            // Console 위치, 크기 설정
            var rect = new Rect(this.ConsoleRect);

            if (this.IsRightSide)
            {
                // 오른쪽 기준으로 위치 변경
                rect.x = Screen.width - rect.width - rect.x;
            }

            // 화면 표시
            GUI.TextArea(rect, this.debugMessage);
        }
    }
}
