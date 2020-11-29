using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProtoBufferTool
{
    public static IMessage Deserialize(MessageParser _type, byte[] byteData)
    {
        Stream stream = new MemoryStream(byteData);
        if (stream != null)
        {
            IMessage t = _type.ParseFrom(stream);
            stream.Close();
            return t;
        }
        stream.Close();
        return default(IMessage);
    }
    public static byte[] Serialize(IMessage _data)
    {
        MemoryStream stream = new MemoryStream();
        if (stream != null)
        {
            _data.WriteTo(stream);
            byte[] bytes = stream.ToArray();
            stream.Close();
            return bytes;
        }
        stream.Close();
        return null;
    }
}
