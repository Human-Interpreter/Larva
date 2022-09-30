using System.IO;
using System.Collections.Generic;
using GameDevWare.Serialization;
using Mirror;

namespace Larva
{
    /// <summary>
    /// Mirror를 통해서 전송되는 다양한 객체들의 직렬화 함수들
    /// </summary>
    public static class CustomReadWriteFunctions
    {
        /// <summary>
        /// 객체를 Mirror을 이용해서 전송하기 위한 직렬화 함수
        /// </summary>
        /// <param name="writer">Network Writer</param>
        /// <param name="value">전송할 객체</param>
        public static void WriteParameters(this NetworkWriter writer, Dictionary<string, object> value)
        {
            // MsgPack을 사용한 객체 직렬화
            var outputStream = new MemoryStream();
            MsgPack.Serialize(value, outputStream);

            // Stream 위치를 제일 처음으로 설정
            outputStream.Position = 0;

            // 직렬화된 객체(바이트 배열)를 네트워크로 전송
            writer.Write(outputStream.ToArray());
        }

        /// <summary>
        /// Mirror를 통해서 전달받은 객체를 역직렬화 하기 위한 함수
        /// </summary>
        /// <param name="reader">Network Reader</param>
        /// <returns>전송받은 객체</returns>
        public static Dictionary<string, object> ReadParameters(this NetworkReader reader)
        {
            // 직렬화된 객체(바이트 배열)를 네트워크로 받음
            var read = reader.Read<byte[]>();

            // 바이트 배열을 MemoryStream으로 읽을 수 있도록 객체 생성
            var readStream = new MemoryStream(read);

            // MsgPack을 사용한 객체 역직렬화
            return MsgPack.Deserialize<Dictionary<string, object>>(readStream);
        }
    }
}
