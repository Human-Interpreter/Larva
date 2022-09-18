using UnityEngine;

namespace Larva
{
    /// <summary>
    /// 게임 내부에서 디버깅 메세지를 확인하기 위한 UI (테스트 용)
    /// </summary>
    public class InGameConsole : MonoBehaviour
    {
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

            // 화면 표시
            GUI.TextArea(new Rect(Screen.width - 510, 10, 500, 300), this.debugMessage);
        }
    }
}
