using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using Mirror;
using GameDevWare.Serialization;

namespace Larva
{
    public static class CustomReadWriteFunctions
    {
        public static void WriteMyType(this NetworkWriter writer, Dictionary<string, object> value)
        {
            var outputStream = new MemoryStream();
            MsgPack.Serialize(value, outputStream);
            outputStream.Position = 0;

            writer.Write(outputStream.ToArray());
        }

        public static Dictionary<string, object> ReadMyType(this NetworkReader reader)
        {
            var read = reader.Read<byte[]>();
            var readStream = new MemoryStream(read);

            return MsgPack.Deserialize<Dictionary<string, object>>(readStream);
        }
    }
}
