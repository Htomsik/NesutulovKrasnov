using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestAsync
{
    public class TestAs
    {
        [ServiceContract(Name = "IMessageCallback")]
        public interface IAsyncMessageCallback
        {
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginOnMessageAdded(byte[] msg, DateTime timestamp, AsyncCallback callback, object asyncState);
            void EndOnMessageAdded(IAsyncResult result);
        }
        [ServiceContract(CallbackContract = typeof(IAsyncMessageCallback))]
        public interface IMessage
        {
            [OperationContract(IsOneWay = true)]
            void Join(string username);
            [OperationContract]
            void AddMessage(byte[] message);
        }
        [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
        public class Service : IMessage
        {
            Dictionary<IAsyncMessageCallback, string> subscribers = new Dictionary<IAsyncMessageCallback, string>();
            List<IAsyncMessageCallback> list=new List<IAsyncMessageCallback>();
            public void Join(string username)
            {
                var connection = OperationContext.Current.GetCallbackChannel<IAsyncMessageCallback>();
                subscribers[connection] = username;
                list.Add(connection);
                Console.WriteLine("\nConnection:"+ subscribers[connection]);
            }
            public void AddMessage(byte[] message)
            {
                lock (subscribers)
                {
                    IAsyncMessageCallback callback = OperationContext.Current.GetCallbackChannel<IAsyncMessageCallback>();
                    //temp.Remove(connection);
                    Action<IAsyncMessageCallback> invoke = call => call.BeginOnMessageAdded(message, DateTime.Now, delegate (IAsyncResult ar)
                    {
                        call.EndOnMessageAdded(ar);
                    }, null);
                    list.ForEachAsync(invoke, callback);
                    
 

                }



                #region A
                /* IAsyncMessageCallback callback = OperationContext.Current.GetCallbackChannel<IAsyncMessageCallback>();

                 //lock (subscribers)
                // {
                     string user;
                     if (!subscribers.TryGetValue(callback, out user))
                         return;

                     //send the received voice stream to each client
                     foreach (var _subscriber in subscribers.Keys)
                     {
                         Console.WriteLine(_subscriber +" "+callback);
                         if (_subscriber == callback)
                         {
                             //if the person who sent the video is the current subscriber then don't send the video to THAT subscriber
                             // if(_subscriber.ToString()!= "TestAsync.TestAs+IAsyncMessageCallback") Console.WriteLine(_subscriber);
                             Console.WriteLine("не можем отправить"+ user);
                             continue;
                         }
                         try
                         {
                             Console.WriteLine("Можем отправить "+ user);
                             //Console.WriteLine(_subscriber.Key);
                             //Send the received stream to the client asynchronously
                             _subscriber.BeginOnMessageAdded(message, DateTime.Now, delegate (IAsyncResult ar)
                             {
                                 callback.EndOnMessageAdded(ar);
                             }, null);
                         }
                         catch (Exception)
                         {
                             //fault handling
                         }
                   //  }
                 }*/
                #endregion
                //}

            }

        }
        class MyClientCallback : IAsyncMessageCallback
        {
            public IAsyncResult BeginOnMessageAdded(byte[] msg, DateTime timestamp, AsyncCallback callback, object asyncState)
            {
                //msg = new byte[800];
               
                Action<byte[], DateTime> act = (txt, time) => { Console.WriteLine("[{0}] {1}", time, txt); };
                return act.BeginInvoke(msg, timestamp, callback, asyncState);
            }

            public void EndOnMessageAdded(IAsyncResult result)
            {

                Action<byte[], DateTime> act = (Action<byte[], DateTime>)((System.Runtime.Remoting.Messaging.AsyncResult)result).AsyncDelegate;
                act.EndInvoke(result);
            }
        }
        static Binding GetBinding()
        {
            return new NetTcpBinding(SecurityMode.None);
        }
        public static void Main()
        {
            string baseAddress = "net.tcp://" + "localhost" + ":8000/Service";
            
            ServiceHost host = new ServiceHost(typeof(Service), new Uri(baseAddress));
            
            host.AddServiceEndpoint(typeof(IMessage), GetBinding(), "");

            //ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //behavior.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(behavior);
            //host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(),
            //new Uri("http://localhost:8080/bookservice/mex"));
            host.Open();
            Console.WriteLine("Host opened");



            InstanceContext instanceContext = new InstanceContext(new MyClientCallback());
            DuplexChannelFactory<IMessage> factory = new DuplexChannelFactory<IMessage>(instanceContext, GetBinding(), new EndpointAddress(baseAddress));
            IMessage proxy = factory.CreateChannel();
            //proxy.AddMessage("Hello world");

            Console.Write("Press ENTER to close the host");
            Console.ReadLine();
            ((IClientChannel)proxy).Close();
            factory.Close();
            host.Close();
        }
    }
}
