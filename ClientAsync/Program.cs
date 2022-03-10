using NAudio.Wave;
using System;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel;
using System.Threading;
using ClientAsync;
using System.Configuration;

namespace ClientAsync
{
    public class MyCallback : ProxyAsync.IMessageCallback
    {
        public static WaveInEvent input;
        //поток для речи собеседника
        public static WaveOutEvent output;
        //буфферный поток для передачи через сеть
        public static BufferedWaveProvider bufferStream;
        //создаем поток для записи нашей речи
        public static Socket client;
        public static Thread in_thread;
        public static InstanceContext context = new InstanceContext(new MyCallback());
        public static ProxyAsync.MessageClient server = new ProxyAsync.MessageClient(context);
        private static void Voice_Input(object sender, WaveInEventArgs e)
        {
            try
            {
                float level=0;
                for (int index = 0; index < e.Buffer.Length; index += 2)
                {
                    short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]);

                    float amplitude = sample / 32768f;
                     level= Math.Abs(amplitude); // от 0 до 1

                    Console.WriteLine("Уровень: {0}%.", level * 100);
                }
                Console.WriteLine(ConfigurationManager.AppSettings.Get("Volume"));
                //if(level*100> Convert.ToDouble(ConfigurationManager.AppSettings.Get("Volume"))) 
                    server.AddMessage(e.Buffer);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " wasdad");
            }
        }

        static void Main(string[] args)
        {
            try
            {
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


                #region слушать

                //in_thread = new Thread(new ThreadStart(Dis));

                //in_thread.Start();
                #endregion



                Console.WriteLine("enter username");
                var username = Console.ReadLine();
                server.Join(username);
                
                Console.WriteLine();
                input.StartRecording();
                
                //Console.WriteLine("Enter message");
                //Console.WriteLine("Press Q to Exit");
                while (true) { }
                var message = Console.ReadLine();
                
                // in_thread = new Thread(new ThreadStart(Listening));
                //запускаем его
                //in_thread.Start();

            }
            catch { };
        }
        public void OnMessageAdded(byte[] msg, DateTime timestamp)
        {
            //Console.WriteLine("{0}:{1}", user, message);
            output.Play();
            try
            {
                //получено данных
                int received = msg.Length;
                //добавляем данные в буфер, откуда output будет воспроизводить звук
                bufferStream.AddSamples(msg, 0, received);
            }
            catch
            { }
        }
    }
}
