using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AudioLib;


namespace Test
{
    class MyCallback : IChatClient
    {
        public void RecievAudio(string user, byte[] message)
        {
            throw new NotImplementedException();
        }

        public void RecievMessage(string user, string message)
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //InstanceContext context = new InstanceContext(new MyCallback());
            //NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None, true);
            //netTcpBinding.Security.Mode = SecurityMode.None;
            //DuplexChannelFactory<IChatService> factory = new DuplexChannelFactory<IChatService>(context, netTcpBinding, "net.tcp://localhost:7998/ServerHost/Chat");
            //IChatService server = factory.CreateChannel();
            //var Room=server.GetRoomList();

            //foreach(string RoomName in Room)
            //{
            //    factory.Close();
            //    factory = new DuplexChannelFactory<IChatService>(context, netTcpBinding, "net.tcp://localhost:7998/ServerHost/Chat/" + RoomName);
            //    server = factory.CreateChannel();

            //    Console.Write("К комнате:"+RoomName+" Подключенны пользователи:");
            //    Console.Write(string.Join(" ", server.GetUserList())+"\n");
            //}

            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7 };

            foreach(int v in a)
            {
                try
                {
                    
                    if (v == 4) throw new Exception();
                    Console.WriteLine(v);
                }
                catch
                {
                    Console.WriteLine("Ошибка");
                }
            }
        }
    }
}
