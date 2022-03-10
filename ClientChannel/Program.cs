using AudioLib;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientChannel
{
    public class MyCallback : IChatClient
    {
        public void RecievAudio(string user, byte[] message)
        {
            output.Play();
            Console.WriteLine(user);
            int received = message.Length;
            Console.WriteLine("Говорит:" + user);
            bufferStream.AddSamples(message, 0, received);
        }

        public void RecievMessage(string user, string message)
        {
            Console.WriteLine("{0}:{1}", user, message);
        }

        public static string username;
        public static WaveInEvent input;
        //поток для речи собеседника
        public static WaveOutEvent output;
        //буфферный поток для передачи через сеть
        public static BufferedWaveProvider bufferStream;

        //public static InstanceContext context = new InstanceContext(new MyCallback());
        //public static ChatServiceClient server = new ChatServiceClient(context);
        public static InstanceContext context = new InstanceContext(new MyCallback());
        public static DuplexChannelFactory<IChatService> factory;
        public static IChatService server;
        private static void Voice_Input(object sender, WaveInEventArgs e)
        {
            try
            {
                server.SendAudo(e.Buffer, username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " wasdad");
            }
        }
        #region WPF
        /*private delegate void FaultedInvoker();

        void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }
        void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }

        void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }
        private static void HandleProxy(object sender, EventArgs e)
        {
            if (factory != null)
            {
                switch (factory.State)
                {
                    case CommunicationState.Closed:
                        Console.WriteLine("closed");
                        server.Disconect();
                        factory = null;
                        server = null;
                        break;
                    case CommunicationState.Closing:
                        break;
                    case CommunicationState.Created:
                        break;
                    case CommunicationState.Faulted:
                        Console.WriteLine("faulted");
                        server.Disconect();
                        factory.Abort();
                        server = null;
                        break;
                    case CommunicationState.Opening:
                        break;
                    default:
                        Console.WriteLine("Default");
                        break;
                }
            }

        }*/
        #endregion
        static void Main(string[] args)
        {
            NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.Transport,true);
            
           // netTcpBinding.Security.Transport.SslProtocols = System.Security.Authentication.SslProtocols.None;
            factory = new DuplexChannelFactory<IChatService>(context, netTcpBinding, "net.tcp://localhost:7998/ServerHost/Chat");

            factory.Open();

            #region WPFEvent
            /*      proxy.InnerDuplexChannel.Faulted += new EventHandler(InnerDuplexChannel_Faulted);
                    proxy.InnerDuplexChannel.Opened += new EventHandler(InnerDuplexChannel_Opened);
                    proxy.InnerDuplexChannel.Closed += new EventHandler(InnerDuplexChannel_Closed);*/
            #endregion
            server = factory.CreateChannel();

            if (server.GetRoomList().Count == 0)
            {
                server.CreateRoom("Chat1");
            }
            if (server.GetRoomList().Count == 1)
            {
                server.CreateRoom("Chat2");
            }
            
            con:
            Console.Write("Введите имя комнаты:");
            string RoomName = Console.ReadLine();
           // if (Console.ReadLine() == "1") goto con;
            
           
            factory.Close();
            factory = new DuplexChannelFactory<IChatService>(context, netTcpBinding, "net.tcp://localhost:7998/ServerHost/Chat/"+RoomName);
            factory.Open();
            server = factory.CreateChannel();
            // server.CreateRoom("Chat2");

            //ChannelFactory<ICreateRoomService>  factoryCreat = new ChannelFactory<ICreateRoomService>(netTcpBinding, "net.tcp://localhost:7998/WPFChat");
            //ICreateRoomService createRoomService = factoryCreat.CreateChannel();
            //createRoomService.CreateRoom("Lox");
            // server.CreateRoom("Artem");

  
                for (int n = 0; n < WaveIn.DeviceCount; n++)
                {
                    var capabilities = WaveIn.GetCapabilities(n);
                    Console.WriteLine(capabilities.ProductName);
                }

                input = new WaveInEvent();
                input.DeviceNumber = 0;
                //определяем его формат - частота дискретизации 8000 Гц, ширина сэмпла - 16 бит, 1 канал - моно
                input.WaveFormat = new WaveFormat(8000, 16, 1);
                //добавляем код обработки нашего голоса, поступающего на микрофон
                input.DataAvailable += Voice_Input;
                //создаем поток для прослушивания входящего звука
                output = new WaveOutEvent();
                //создаем поток для буферного потока и определяем у него такой же формат как и потока с микрофона
                bufferStream = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
                //привязываем поток входящего звука к буферному потоку
                output.Init(bufferStream);
                Console.Write("Enter username:");
                username = Console.ReadLine();
                server.Join(username);
                Console.WriteLine();
                Console.WriteLine("Аудио 1,Чат 2");
                int type = int.Parse(Console.ReadLine());
                if (type == 1)
                {
                try
                {
                    input.StartRecording();
                    //Console.WriteLine("Enter message");
                    Console.WriteLine("Press Q to Exit");
                    var message = Console.ReadLine();
                    server.Disconect();
                }
                catch (CommunicationObjectAbortedException)
                {
                    Console.WriteLine("Проблемы с сервером, отключение");
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("Проблемы с сервером, отключение");
                }
                }   
                    
                else
                {
                    var message = Console.ReadLine();

                    try
                    {
                        while (message != "Q")
                        {

                            if (!string.IsNullOrEmpty(message))
                            server.SendMessage(message, username);
                            message = Console.ReadLine();


                        }
                        server.Disconect();
                        goto con;
                    }
                    catch (CommunicationObjectFaultedException)
                    {
                       Console.WriteLine("Проблемы с сервером, отключение");
                    factory.Abort();

                    }
                    catch(ObjectDisposedException)
                    {
                        Console.WriteLine("Проблемы с сервером, отключение");
                    factory.Abort();
                }

                }

            Console.ReadLine();
        }

       
    }
}
