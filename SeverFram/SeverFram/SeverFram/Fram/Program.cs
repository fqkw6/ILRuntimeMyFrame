using Google.Protobuf;
using Message;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Role;
namespace ConsoleApplication1
{
    class Program
    {
        private static byte[] result = new byte[1024];
        private const int port = 8088;
        private static string IpStr = "127.0.0.1";
        private static Socket serverSocket;

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse(IpStr);
            IPEndPoint ip_end_point = new IPEndPoint(ip, port);
            //创建服务器Socket对象，并设置相关属性
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定ip和端口
            serverSocket.Bind(ip_end_point);
            //设置最长的连接请求队列长度
            serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //在新线程中监听客户端的连接
            Thread thread = new Thread(ClientConnectListen);
            thread.Start();
            Console.ReadLine();
        }

        /// <summary>
        /// 客户端连接请求监听
        /// </summary>
        private static void ClientConnectListen()
        {
            while (true)
            {
                //为新的客户端连接创建一个Socket对象
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("客户端{0}成功连接", clientSocket.RemoteEndPoint.ToString());
                //向连接的客户端发送连接成功的数据
                ByteBuffer buffer = new ByteBuffer();
                //   buffer.WriteString("Connected Server");
                //   clientSocket.Send(WriteMessage(buffer.ToBytes()));
                //每个客户端连接创建一个线程来接受该客户端发送的消息
                Thread thread = new Thread(RecieveMessage);
                thread.Start(clientSocket);
            }
        }


        private static byte[] CreateData(IMessage pbuf)
        {
            byte[] pbdata = ProtoBufferTool.Serialize(pbuf);
            ByteBuffer buff = new ByteBuffer();
            buff.WriteInt(pbdata.Length);
            buff.WriteBytes(pbdata);
            return buff.ToBytes();
        }
        /// <summary>
        /// 接收指定客户端Socket的消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void RecieveMessage(object clientSocket)
        {
            Socket mClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {

                    int receiveNumber = mClientSocket.Receive(result);
                    Console.WriteLine("接收客户端{0}消息， 长度为{1}", mClientSocket.RemoteEndPoint.ToString(), receiveNumber);
                    ByteBuffer buff = new ByteBuffer(result);
                    int datalength = buff.ReadInt();
                    Console.WriteLine(datalength);

                    byte[] pbdata = buff.ReadBytes(datalength);

                    Console.WriteLine(pbdata.Length + "dddd");
                    // Console.WriteLine(typeId+"typeid");
                    //通过协议号判断选择的解析类

                    NetMessageBase clientReq = ProtoBufferTool.DeserializeNew<NetMessageBase>(NetMessageBase.Parser, pbdata);
                    //ushort typeId = buff.ReadShort();
                    Console.WriteLine(clientReq.MessageId + "typeId");

                    role_info cSrole_info = ProtoBufferTool.DeserializeNew<role_info>(role_info.Parser, clientReq.MessageBoby.ToByteArray());
                    Console.WriteLine("数据内容：Name={0},Level={1}", cSrole_info.Name, cSrole_info.Level);
                    //Console.WriteLine(cSLoginReq.IDList.Count);
                    //Console.WriteLine(cSLoginReq.IDList[1]);


                    NetMessageBase cSMessage = new NetMessageBase();
                    cSMessage.MessageId = 10001;

                    role_info mLoginInfo = new role_info();
                    mLoginInfo.Name = "fuwuqi";
                    byte[] da= ProtoBufferTool.Serialize(mLoginInfo);
                    cSMessage.MessageBoby = ByteString.CopyFrom(da);
                    byte[] data = CreateData(cSMessage);
                    mClientSocket.Send(data);
                    Console.WriteLine(data.Length + "changdu");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    mClientSocket.Shutdown(SocketShutdown.Both);
                    mClientSocket.Close();
                    break;
                }
            }
        }
    }
}
