using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioLib
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatServiceg : IChatService,ICreateRoomService
    {
        Dictionary<IChatClient, string> _users = new Dictionary<IChatClient, string>();
        

        //List<IChatClient> list = new List<IChatClient>();
        //List<string> UserList = new List<string>();

        public void Join(string username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            _users[connection] = username;
            //list.Add(connection);
            //UserList.Add(username);
            Console.WriteLine("\n Connect:"+username);
        }

        public List<string> GetUserList()
        {
            List<string> values = _users.Values.Select(i => i).ToList();
            return values;
        }
        public List<string> GetRoomList()
        {
            return RoomName;
        }
        public void Disconect()
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            _users.Remove(connection);
        }
        #region Await
        //public async void SendAudio(byte[] message, string name)
        //{
        //    var con = OperationContext.Current.GetCallbackChannel<IChatClient>();
        //    await Task.Run(() => SendAudoAsync(message, name, con));
        //}
        #endregion

        public void SendAudo(byte[] message, string name)
        {
            #region old
            //List<IChatClient> temp = new List<IChatClient>();
            //  temp = new List<IChatClient>(list);

            ////IChatClient connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            ////List<IChatClient> copy = new List<IChatClient>(list);
            ////copy.Remove(connection);

            //////temp.Remove(connection);
            ////Action<IChatClient> invoke = callback =>  callback.RecievAudio(" ", message);
            ////copy.ForEachAsync(invoke).ToArray();

            ////Console.WriteLine("400");



            //Delegate[] subscribers = list.GetInvocationList();
            //Action<Delegate> publish = (subscriber =>
            //        subscriber.DynamicInvoke(message, " "));
            //subscribers.ForEachAsync(publish);
            //list.ForEachAsync(publish);

            //ThreadPool.QueueUserWorkItem(o => connection.RecievAudio(" ", message));
            //list.ForEach(callback =>
            //{
            //    //if (callback != connection) callback.RecievAudio("", message); //THIS OPERATION
            //    if (callback != connection) ThreadPool.QueueUserWorkItem(o => connection.RecievAudio(" ", message));
            //});\




            //Action<IChatClient> invoke = callback => callback.RecievAudio("",message);
            // m_Callbacks.ForEachAsync(invoke);
            #endregion
            lock (_users)
            {
                try
                {
                    IChatClient connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
                    string user;
                    if (!_users.TryGetValue(connection, out user))
                        return;

                        foreach (var other in _users.Keys.ToArray())
                        {
                            try
                            {
                                if (other == connection)
                                    continue;
                                other.RecievAudio(user, message);
                            }
                            catch(CommunicationObjectAbortedException)
                            {
                                
                                
                                Console.WriteLine();
                                Console.WriteLine();
                                _users.Remove(other);
                            }
                        }
                    
 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }


        public void SendMessage(string message, string name)
        {
            lock (_users)
            {
                try
                {
                    var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
                    string user;
                    if (!_users.TryGetValue(connection, out user))
                        return;
                    foreach (var other in _users.Keys.ToArray())
                    {
                        try
                        {
                            if (other == connection)
                                continue;
                            other.RecievMessage(user, message);
                        }
                        catch (CommunicationObjectAbortedException)
                        {
                            _users.Remove(other);
                            foreach (var temp in _users.Keys.ToArray())
                            {

                                    if (temp == connection)
                                        continue;
                                    temp.RecievMessage(user, "отключился");
                                

                            }
                            

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex+"\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        List<string> RoomName = new List<string>();
        public string CreateRoom(string name)
        {
            if (con != null) con.CreatRoomCallback(name);
            RoomName.Add(name);
            return name;
        }
        public static ICreateRoom con;
        public void ServerJoin(string username)
        {
           con = OperationContext.Current.GetCallbackChannel<ICreateRoom>();
        }
    }
}
