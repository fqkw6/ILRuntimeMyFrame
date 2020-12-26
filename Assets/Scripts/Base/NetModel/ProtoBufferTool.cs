using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProtoBufferTool
{
    /// <summary>
    /// 解析
    ///  NetMessageBase newooo = ProtoBufferTool.Deserialize(NetMessageBase.Parser, by) as NetMessageBase;
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="byteData"></param>
    /// <returns></returns>
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

    public static T DeserializeNew<T>(MessageParser _type, byte[] byteData) where T : IMessage
    {
        Stream stream = new MemoryStream(byteData);
        IMessage message;
        if (stream != null)
        {
            message = _type.ParseFrom(stream);
            stream.Close();
            return (T)message;
        }
        stream.Close();
        return default(T);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="_data"></param>
    /// <returns></returns>
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
