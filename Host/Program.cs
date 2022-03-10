using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AudioLib;
using System.ServiceModel.Description;
using NConsoleMenu;
using System.Reflection;

namespace Host
{
    class CreateRomm : ICreateRoom
    {

        public string CreatRoomCallback(string name)
        {
            Console.WriteLine("Создание комнаты на сервере");
            string addres = "localhost";
            int port = 7998;
            Uri Server = new Uri("net.tcp://" + addres + ":" + port + "/ServerHost/Chat/");
            //  Uri CreateRoom = new Uri("net.tcp://localhost:7999/CreateRom");
            Uri httpAdrs = new Uri("http://" + addres + ":" +
              (port + 1).ToString() + "/ServerHost/Chat/" + name);

            Uri[] baseAdresses = { Server, httpAdrs };

            ServiceHost host = new ServiceHost(typeof(ChatServiceg), baseAdresses);


            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.Transport, true);
            // tcpBinding.Security.Transport.SslProtocols = System.Security.Authentication.SslProtocols.None;
            //Updated: to enable file transefer of 64 MB
            tcpBinding.MaxBufferPoolSize = (int)67108864;
            tcpBinding.MaxBufferSize = 67108864;
            tcpBinding.MaxReceivedMessageSize = (int)67108864;
            tcpBinding.TransferMode = TransferMode.Buffered;
            tcpBinding.ReaderQuotas.MaxArrayLength = 67108864;
            tcpBinding.ReaderQuotas.MaxBytesPerRead = 67108864;
            tcpBinding.ReaderQuotas.MaxStringContentLength = 67108864;


            tcpBinding.MaxConnections = 100;
            //To maxmize MaxConnections you have to assign another port for mex endpoint

            //and configure ServiceThrottling as well
            ServiceThrottlingBehavior throttle;
            throttle = host.Description.Behaviors.Find<ServiceThrottlingBehavior>();
            if (throttle == null)
            {
                throttle = new ServiceThrottlingBehavior();
                throttle.MaxConcurrentCalls = 100;
                throttle.MaxConcurrentSessions = 100;
                host.Description.Behaviors.Add(throttle);
            }


            //Enable reliable session and keep the connection alive for 20 hours.
            tcpBinding.ReceiveTimeout = new TimeSpan(0, 6, 0);
            tcpBinding.ReliableSession.Enabled = true;
            tcpBinding.ReliableSession.InactivityTimeout = new TimeSpan(0, 5, 0);

            host.AddServiceEndpoint(typeof(IChatService), tcpBinding, name);

            //Define Metadata endPoint, So we can publish information about the service
            ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(mBehave);

            host.AddServiceEndpoint(typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexHttpBinding(),
                "http://" + addres + ":" +
                (port - 1).ToString() + "/Server/mex/" + name);


            try
            {
                host.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
            finally
            {
                if (host.State == CommunicationState.Opened)
                {
                    Console.WriteLine("Opened:" + name);
                    RoomSettings.Rooms.Add(host, name);
                }
            }
            //ServiceDescription desc = host.Description;
            //string info = "";
            //info += "Base addresses:\n";
            //foreach (Uri uri in host.BaseAddresses)
            //{
            //    info += "    " + uri + "\n";
            //}
            //// Enumerate the service endpoints in the service description.
            //info += "Service endpoints:\n";
            //foreach (ServiceEndpoint endpoint in desc.Endpoints)
            //{
            //    info += "    Address:  " + endpoint.Address + "\n";
            //    info += "    Binding:  " + endpoint.Binding.Name + "\n";
            //    info += "    Contract: " + endpoint.Contract.Name + "\n";
            //}
            //Console.WriteLine(info);

            return "sdad";
        }
    }

    static class RoomSettings
    {
        public static Dictionary<ServiceHost, string> Rooms = new Dictionary<ServiceHost, string>();

        public static void RommClose(ServiceHost host)
        {
            if (host != null)
            {
                try
                {
                    host.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                finally
                {
                    if (host.State == CommunicationState.Closed)
                    {
                        Console.WriteLine("Romm closed");
                    }
                }
            }
        }

        public static void RoomList()
        {
            //InstanceContext context = new InstanceContext(new MyCallback());
            //NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None, true);
            //netTcpBinding.Security.Mode = SecurityMode.None;
            //DuplexChannelFactory<IChatService> factory = new DuplexChannelFactory<IChatService>(context, netTcpBinding, "net.tcp://localhost:7998/ServerHost/Chat");
            //IChatService server = factory.CreateChannel();
            //var Room = server.GetRoomList();

            //foreach (string RoomName in Room)
            //{
            //    factory.Close();
            //    factory = new DuplexChannelFactory<IChatService>(context, netTcpBinding, "net.tcp://localhost:7998/ServerHost/Chat/" + RoomName);
            //    server = factory.CreateChannel();

            //    Console.Write("К комнате:" + RoomName + " Подключенны пользователи:");
            //    Console.Write(string.Join(" ", server.GetUserList()) + "\n");
            //}
            //int num = 0;
            //foreach (string RoomL in RoomSettings.Rooms.Values)
            //{
            //    num++;
            //    Console.WriteLine($"{num}. {RoomL}");
            //}
            //num = 0;
        }


    }

    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("==============================================================");
            //ServiceHost host = new ServiceHost(typeof(ChatServiceg));
            //host.Open();
            //Console.WriteLine("Host start:"+DateTime.Now);
            //var a=host.Description;
            string addres = "localhost";
            int port = 7998;
            Uri Server = new Uri("net.tcp://" + addres + ":" + port + "/ServerHost/");
            //  Uri CreateRoom = new Uri("net.tcp://localhost:7999/CreateRom");
            Uri httpAdrs = new Uri("http://" + addres + ":" +
              (port + 1).ToString() + "/ServerHost/");

            Uri[] baseAdresses = { Server, httpAdrs };

            ServiceHost host = new ServiceHost(typeof(ChatServiceg), baseAdresses);

            #region tcpBinding
            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.Transport, true);
            Console.WriteLine(tcpBinding.Security);
            // tcpBinding.Security.Transport.SslProtocols = System.Security.Authentication.SslProtocols.None;
            //Updated: to enable file transefer of 64 MB
            tcpBinding.MaxBufferPoolSize = (int)67108864;
            tcpBinding.MaxBufferSize = 67108864;
            tcpBinding.MaxReceivedMessageSize = (int)67108864;
            tcpBinding.TransferMode = TransferMode.Buffered;
            tcpBinding.ReaderQuotas.MaxArrayLength = 67108864;
            tcpBinding.ReaderQuotas.MaxBytesPerRead = 67108864;
            tcpBinding.ReaderQuotas.MaxStringContentLength = 67108864;


            tcpBinding.MaxConnections = 100;
            //To maxmize MaxConnections you have to assign another port for mex endpoint

            //and configure ServiceThrottling as well
            ServiceThrottlingBehavior throttle;
            throttle = host.Description.Behaviors.Find<ServiceThrottlingBehavior>();
            if (throttle == null)
            {
                throttle = new ServiceThrottlingBehavior();
                throttle.MaxConcurrentCalls = 100;
                throttle.MaxConcurrentSessions = 100;
                host.Description.Behaviors.Add(throttle);
            }


            //Enable reliable session and keep the connection alive for 20 hours.
            //Если включено значение reliableSession, оба значения вступают в силу.
            tcpBinding.ReceiveTimeout = new TimeSpan(25, 0, 0);
            tcpBinding.ReliableSession.Enabled = true;
            tcpBinding.ReliableSession.InactivityTimeout = new TimeSpan(24, 0, 0);

            #region TimeOut
            //Первый таймер периода бездействия находится в надежном сеансе и называется InactivityTimeout.
            //Этот таймер периода бездействия запускается, если в течение времени ожидания сообщения 
            //приложения или инфраструктуры не были получены. Сообщение инфраструктуры — это сообщение, 
            //созданное для одного из протоколов в стеке каналов, например поддержки активности или 
            //подтверждения, оно не содержит данные приложения.

            //Второй таймер периода бездействия находится в службе и использует параметр ReceiveTimeout привязки. 
            //Этот таймер периода бездействия запускается, если в течение времени ожидания сообщения приложения не 
            //были получены.

            //Поскольку подключение сбрасывается, когда включается любой из таймеров, увеличение значения InactivityTimeout, 
            //если оно больше значения ReceiveTimeout, не оказывает никакого влияния. Значение по умолчанию для обоих таймеров 
            //составляет 10 минут, поэтому всегда следует увеличивать значения обоих таймеров, чтобы провести различие при 
            //использовании надежного сеанса.

            //ReceiveTimeout всегда должен быть больше или равен inactivityTimeout
            #endregion

            host.AddServiceEndpoint(typeof(IChatService), tcpBinding, "Chat");
            host.AddServiceEndpoint(typeof(ICreateRoomService), tcpBinding, "CreateRoom");

            //Define Metadata endPoint, So we can publish information about the service
            ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(mBehave);

            host.AddServiceEndpoint(typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexHttpBinding(),
                "http://" + addres + ":" +
                (port - 1).ToString() + "/Server/mex");
            #endregion

            try
            {
                host.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (host.State == CommunicationState.Opened)
                {
                    Console.WriteLine("Main server open");
                }
            }
            #region Info
            ServiceDescription desc = host.Description;
            // Enumerate the base addresses in the service host.
            Console.WriteLine("Server info:");

            Console.WriteLine();
            string info = "";
            info += "Base addresses:\n";
            foreach (Uri uri in host.BaseAddresses)
            {
                info += "    " + uri + "\n";
            }
            // Enumerate the service endpoints in the service description.
            info += "Service endpoints:\n";
            foreach (ServiceEndpoint endpoint in desc.Endpoints)
            {
                info += "    Address:  " + endpoint.Address + "\n";
                info += "    Binding:  " + endpoint.Binding.Name + "\n";
                info += "    Contract: " + endpoint.Contract.Name + "\n";
            }
            Console.WriteLine(info);
            Console.WriteLine("==============================================================");
            #endregion

            InstanceContext context = new InstanceContext(new CreateRomm());

            DuplexChannelFactory<ICreateRoomService> factory = new DuplexChannelFactory<ICreateRoomService>(context, tcpBinding, "net.tcp://localhost:7998/ServerHost/CreateRoom");
            factory.Open();
            ICreateRoomService server = factory.CreateChannel();
            server.ServerJoin("Server 1");



            var menu = new CMenu();
            menu.Add("Rooms", s =>
            {

                int i = 1;
                foreach (string name in RoomSettings.Rooms.Values)
                {
                    Console.WriteLine($"{i}. {name}");
                    i++;
                }


            },"Все открытые комнаты");
            menu.Add("Delete", s => 
            {

                int number = 0;
                Console.Write("Введите номер комнаты:");
                bool res = int.TryParse(Console.ReadLine(), out number);
                if (number > RoomSettings.Rooms.Count)
                {
                    Console.WriteLine("Введённое число больше допустимого");
                    menu.Run();
                }
                if (res)
                {

                    Console.WriteLine($"Удаление комнаты под номером {number}");
                    var tempRoom = RoomSettings.Rooms.Select(kvp => kvp.Key).ToList()[number - 1];
                    tempRoom.Close();
                    RoomSettings.Rooms.Remove(tempRoom);
                }
                else
                {
                    Console.WriteLine("Это не число,Введите снова");
                    
                }

            },"Закрытие комнаты");
            menu.Add("Info", s =>
            {

                int number = 0;
                Console.Write("Введите номер комнаты:");
                bool res = int.TryParse(Console.ReadLine(), out number);
                if (res)
                {

                    if (number > RoomSettings.Rooms.Count)
                    {
                        Console.WriteLine("Введённое число больше допустимого");
                        menu.Run();
                    }
                    var tempRoom = RoomSettings.Rooms.Select(kvp => kvp.Key).ToList()[number - 1];
                    ServiceDescription descr = tempRoom.Description;
                    string infos = "";
                    infos += "Base addresses:\n";
                    foreach (Uri uri in tempRoom.BaseAddresses)
                    {
                        infos += "    " + uri + "\n";
                    }
                    // Enumerate the service endpoints in the service description.
                    infos += "Service endpoints:\n";
                    foreach (ServiceEndpoint endpoint in descr.Endpoints)
                    {
                        infos += "    Address:  " + endpoint.Address + "\n";
                        infos += "    Binding:  " + endpoint.Binding.Name + "\n";
                        infos += "    Contract: " + endpoint.Contract.Name + "\n";
                    }
                    Console.WriteLine(infos);
                }
                else
                {
                    Console.WriteLine("Это не число,Введите снова");

                }

            },"Подробная информация о выбранной комнате");
            menu.Add("Restart", s =>
            {
                System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);

                host.Close();
                foreach(var temp in RoomSettings.Rooms.Keys)
                {
                    try
                    {
                        temp.Close();
                    }
                    catch
                    {
                        temp.Abort();
                    }
                }
                Environment.Exit(0);


            }, "Перезагрузка сервера");


            menu.Run();







            //while (true)
            //{
            //    string input = Console.ReadLine();

            //    switch (input)
            //    {


            //        case "Info":
            //            int i = 1;
            //            foreach (string name in RoomSettings.Rooms.Values)
            //            {
            //                Console.WriteLine($"{i}. {name}");
            //                i++;
            //            }

            //            break;

            //        case "Delete":
            //            {
            //                Del:
            //                Console.Write("Введите номер комнаты которую хотите удалить:");



            //                    int number = 0;

            //                    bool res = int.TryParse(Console.ReadLine(), out number);
            //                    if (res)
            //                    {

            //                        Console.WriteLine($"Удаление комнаты под номером {number}");
            //                        var tempRoom = RoomSettings.Rooms.Select(kvp => kvp.Key).ToList()[number - 1];
            //                        tempRoom.Close();
            //                        RoomSettings.Rooms.Remove(tempRoom);
            //                    }
            //                    else
            //                    {
            //                        Console.WriteLine("Это не число,Введите снова");
            //                    goto Del;
            //                    }

            //                break;
            //            }

            //    }
            //    if (input == "Q") break;


            // }
            Console.WriteLine();
            host.Close();

        }
    }
}