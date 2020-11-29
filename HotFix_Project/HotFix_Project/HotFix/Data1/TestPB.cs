using System.IO;

using Google.Protobuf;
using GxTest;

public class TestPB
{

    public static void Start()
    {
        gx_data obj = new gx_data()
        {
            ScBool = true,
            ScFloat = 1.03f,
            ScInt32 = 3222,
            ScString = "khehehe",
            RepInt32 = { 1, 2, 3, 4, 5 }
        };

        //var mem = new MemoryStream();
        //obj.WriteTo(mem);
        //mem.Position = 0;

        //gx_data obj2 = gx_data.Parser.ParseFrom(mem);
        //Dumper.Dump(obj2);

        byte[] s=ProtoBufferTool .Serialize(obj);

        gx_data d = ProtoBufferTool.Deserialize(gx_data.Parser, s) as gx_data;
        Dumper.Dump(d);

    }

   

}
