using System.IO;

using Google.Protobuf;
using Message;
using UnityEngine;
public class TestPB
{

    public static void Start()
    {
        myperson obj = new myperson()
        {
         
            Id = 3222,
        };

        //var mem = new MemoryStream();
        //obj.WriteTo(mem);
        //mem.Position = 0;

        //gx_data obj2 = gx_data.Parser.ParseFrom(mem);
        //Dumper.Dump(obj2);

        byte[] s=ProtoBufferTool .Serialize(obj);

        myperson d = ProtoBufferTool.Deserialize(myperson.Parser, s) as myperson;

        // Dumper.Dump(d);
        Debug.LogError(d.Id+"ddddd");
    }

   

}
