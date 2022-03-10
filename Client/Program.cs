using AudioLib;
using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Threading;

namespace Client
{
    #region old
    ////class SoundStream : Stream
    ////{
    ////    private long length;
    ////    private long position;
    ////    private ConcurrentQueue<byte> sampleQueue;
    ////    private AutoResetEvent dataAvailableSignaler = new AutoResetEvent(false);
    ////    private int preloadSize = 200;

    ////    public void Write(IEnumerable<byte> samples)
    ////    {
    ////        //write samples to sample queue
    ////        foreach (var sample in samples)
    ////        {
    ////            sampleQueue.Enqueue(sample);
    ////        }

    ////        //send signal to Read method
    ////        if (sampleQueue.Count >= preloadSize)
    ////            dataAvailableSignaler.Set();
    ////    }

    ////    public SoundStream(int sampleRate = 8000)
    ////    {
    ////        length = int.MaxValue / 2 - 36;
    ////        position = 0;

    ////        //add wav header with too big length (near 70 hours for sample rate 8000)
    ////        sampleQueue = new ConcurrentQueue<byte>(BuildWavHeader((int)length, sampleRate));
    ////    }

    ////    /// <summary>
    ////    /// Write audio samples into stream
    ////    /// </summary>
    ////    public void Write(IEnumerable<short> samples)
    ////    {
    ////        //write samples to sample queue
    ////        foreach (var sample in samples)
    ////        {
    ////            sampleQueue.Enqueue((byte)(sample & 0xFF));
    ////            sampleQueue.Enqueue((byte)(sample >> 8));
    ////        }

    ////        //send signal to Read method
    ////        if (sampleQueue.Count >= preloadSize)
    ////            dataAvailableSignaler.Set();
    ////    }

    ////    /// <summary>
    ////    /// Count of unread bytes in buffer
    ////    /// </summary>
    ////    public int Buffered
    ////    {
    ////        get { return sampleQueue.Count; }
    ////    }

    ////    /// <summary>
    ////    /// Read
    ////    /// </summary>
    ////    public override int Read(byte[] buffer, int offset, int count)
    ////    {
    ////        if (position >= length)
    ////            return 0;

    ////        //wait while data will be available
    ////        if (sampleQueue.Count < preloadSize)
    ////            dataAvailableSignaler.WaitOne();

    ////        var res = 0;

    ////        //copy data from incoming queue to output buffer
    ////        while (count > 0 && sampleQueue.Count > 0)
    ////        {
    ////            byte b;
    ////            if (!sampleQueue.TryDequeue(out b)) return 0;
    ////            buffer[offset + res] = b;
    ////            count--;
    ////            res++;
    ////            position++;
    ////        }

    ////        return res;
    ////    }

    ////    #region WAV header

    ////    public static byte[] BuildWavHeader(int samplesCount, int sampleRate = 8000)
    ////    {
    ////        using (var stream = new MemoryStream())
    ////        {
    ////            var writer = new BinaryWriter(stream);
    ////            short frameSize = (short)(16 / 8);
    ////            writer.Write(0x46464952);
    ////            writer.Write(36 + samplesCount * frameSize);
    ////            writer.Write(0x45564157);
    ////            writer.Write(0x20746D66);
    ////            writer.Write(16);
    ////            writer.Write((short)1);
    ////            writer.Write((short)1);
    ////            writer.Write(sampleRate);
    ////            writer.Write(sampleRate * frameSize);
    ////            writer.Write(frameSize);
    ////            writer.Write((short)16);
    ////            writer.Write(0x61746164);
    ////            writer.Write(samplesCount * frameSize);
    ////            return stream.ToArray();
    ////        }
    ////    }

    ////    #endregion

    ////    #region Stream impl

    ////    public override bool CanRead
    ////    {
    ////        get { return true; }
    ////    }

    ////    public override bool CanSeek
    ////    {
    ////        get { return false; }
    ////    }

    ////    public override bool CanWrite
    ////    {
    ////        get { return false; }
    ////    }

    ////    public override void Flush()
    ////    {
    ////        throw new NotImplementedException();
    ////    }

    ////    public override long Length
    ////    {
    ////        get { return length; }
    ////    }

    ////    public override long Position
    ////    {
    ////        get { return position; }
    ////        set {; }
    ////    }

    ////    public override long Seek(long offset, SeekOrigin origin)
    ////    {
    ////        throw new NotImplementedException();
    ////    }

    ////    public override void SetLength(long value)
    ////    {
    ////        throw new NotImplementedException();
    ////    }

    ////    public override void Write(byte[] buffer, int offset, int count)
    ////    {
    ////        throw new NotImplementedException();
    ////    }
    ////    #endregion
    ////}

    ////public class StreamPlayer : IDisposable
    ////{
    ////    private SoundStream stream;
    ////    private WaveOutEvent waveOut;
    ////    private WaveFileReader reader;

    ////    public StreamPlayer(int sampleRate = 8000)
    ////    {
    ////        stream = new SoundStream(sampleRate);
    ////        waveOut = new WaveOutEvent();
    ////    }
    ////    public void Write(IEnumerable<byte> samples)
    ////    {
    ////        stream.Write(samples);
    ////    }
    ////    /// <summary>
    ////    /// Write audio samples into stream
    ////    /// </summary>
    ////    public void Write(params short[] samples)
    ////    {
    ////        stream.Write(samples);
    ////    }

    ////    /// <summary>
    ////    /// Write audio samples into stream
    ////    /// </summary>
    ////    public void Write(IEnumerable<short> samples)
    ////    {
    ////        stream.Write(samples);
    ////    }

    ////    /// <summary>
    ////    /// Plays sound
    ////    /// </summary>
    ////    public void PlayAsync()
    ////    {
    ////        ThreadPool.QueueUserWorkItem((_) =>
    ////        {
    ////            reader = new WaveFileReader(stream);
    ////            waveOut.Init(reader);
    ////            waveOut.Play();
    ////        });
    ////    }

    ////    /// <summary>
    ////    /// Stop playing
    ////    /// </summary>
    ////    public void Stop()
    ////    {
    ////        waveOut.Stop();
    ////    }

    ////    /// <summary>
    ////    /// Volume
    ////    /// </summary>
    ////    public float Volume
    ////    {
    ////        get { return waveOut.Volume; }
    ////        set { waveOut.Volume = value; }
    ////    }

    ////    /// <summary>
    ////    /// Count of unread bytes in buffer
    ////    /// </summary>
    ////    public int Buffered
    ////    {
    ////        get { return stream.Buffered; }
    ////    }

    ////    public void Dispose()
    ////    {
    ////        waveOut.Dispose();
    ////        reader.Dispose();
    ////        stream.Dispose();
    ////    }
    ////}
    //public class MyCallback : Proxy.IChatServiceCallback
    //{
    //    //byte[] data = new byte[65535];
    //    //int a = 0;


    //    public void RecievAudio(string user, byte[] message)
    //    {
    //        //int x = message.Length;
    //        //var buffer = new byte[x];
    //        //buffer = message;

    //        //Console.WriteLine("Проигрываем");
    //        ////Проиграть входящий звук в колонки (наушники)
    //        //play.Write(buffer.AsEnumerable());

    //        //Двигаем позицию
    //       // memstr.Position = testoffset;

    //        //stream.Write(message, 0, message.Length);
    //        //stream.Close();
    //        //RecievAudio2(stream);
    //        //output.Play();
    //        //Console.WriteLine(user);
    //        int received = message.Length;
    //        //message.CopyTo(data, a);

    //        ////Console.WriteLine("Говорит:" + user);
    //        //MemoryStream memoryStream = new MemoryStream(message);
    //        //SoundPlayer simpleSound = new SoundPlayer(memoryStream);
    //        //// simpleSound.Play();

    //        bufferStream.AddSamples(message, 0, received);
    //        //Console.WriteLine(string.Join("", message));
    //        ////a += 400;
    //        //Console.WriteLine("Пришел ответ ");


    //        // player.PlayAsync();
    //        //// var sh = (short)((b - 127) * 2 * 254);
    //        // player.Write(message);


    //        //player.PlayAsync();

    //        //    var incoming = message;

    //        //    var part = 400;
    //        //    var offset = 0;


    //        //while (true)
    //        //{
    //        //    var buffer = incoming;//.Skip(offset).Take(part).AsEnumerable();

    //        //    player.Write(buffer);


    //        //    Thread.Sleep(20);
    //        //}




    //    }

    //    //public void RecievAudio2(MemoryStream stream)
    //    //{


    //    //}
    //    //private async void Signal_AudioRecover(byte[] buff)
    //    //{
    //    //    await Task.Run(() ==>
    //    //    {

    //    //        MediaElement playbackMediaElement = new MediaElement();
    //    //        var stream = buff.AsBuffer().AsStream().AsRandomAccessStream();
    //    //        playbackMediaElement.SetSource(stream, "");
    //    //        playbackMediaElement.Play();
    //    //    });
    //    //}
    //    //public void Re(byte[] message)
    //    //{
    //    //    bufferStream.AddSamples(message, 0, message.Length);
    //    //}
    //    public void RecievMessage(string user, string message)
    //    {
    //        Console.WriteLine("{0}:{1}",user,message);

    //    }

    //    public static string username;
    //    public static WaveInEvent input;
    //    //поток для речи собеседника
    //    public static WaveOutEvent output;
    //    //буфферный поток для передачи через сеть
    //    public static BufferedWaveProvider bufferStream;
    //    public static InstanceContext context = new InstanceContext(new MyCallback());
    //    public static ChatServiceClient server = new ChatServiceClient(context);
    //    //public static byte[] bt = new byte[320];
    //    private static void Voice_Input(object sender, WaveInEventArgs e)
    //    {
    //        try
    //        {


    //            //bt = sr.ReadBytes(320);
    //            //if (bt.Length != 0)
    //            //{
    //                server.SendAudoAAA(e.Buffer,username);
    //                // testoffset += bt.Length;
    //            //}
    //           // server.SendAudoAAA(e.Buffer,username);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("444444444" + ex.Message);
    //        }
    //    }
    //    //private static BinaryWriter sw;
    //    //private static BinaryReader sr;
    //    //public static WaveInEvent waveSource;
    //    //private static MemoryStream memstr;
    //    //private static int testoffset = 0;
    //    //public static StreamPlayer play=new StreamPlayer();
    //    static void Main(string[] args)
    //    {
    //        #region old
    //        try
    //        {
    //            for (int n = 0; n < WaveIn.DeviceCount; n++)
    //            {
    //                var capabilities = WaveIn.GetCapabilities(n);
    //                Console.WriteLine(capabilities.ProductName);
    //            }
    //            input = new WaveInEvent();
    //            input.DeviceNumber = 0;
    //            //определяем его формат - частота дискретизации 8000 Гц, ширина сэмпла - 16 бит, 1 канал - моно
    //            input.WaveFormat = new WaveFormat(8000, 16, 1);
    //            //добавляем код обработки нашего голоса, поступающего на микрофон
    //            input.DataAvailable += Voice_Input;
    //            //создаем поток для прослушивания входящего звука
    //            output = new WaveOutEvent();
    //            //создаем поток для буферного потока и определяем у него такой же формат как и потока с микрофона
    //            bufferStream = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
    //            bufferStream.DiscardOnBufferOverflow = true;
    //            // bufferStream.BufferDuration = new TimeSpan(0,0 , 5); //BufferDuration, 1 minute
    //            bufferStream.DiscardOnBufferOverflow = true; //The name says

    //            //привязываем поток входящего звука к буферному потоку
    //            output.Init(bufferStream);
    //            Console.Write("Enter username:");
    //            username = Console.ReadLine();
    //            server.Join(username);
    //            Console.WriteLine();
    //            //Console.WriteLine(string.Join(" ", server.GetUserList()));
    //            Console.WriteLine("Аудио 1,Чат 2");
    //            // int type = int.Parse(Console.ReadLine());
    //            //if (type == 1)
    //            // {
    //            input.StartRecording();
    //            //Console.WriteLine("Enter message");
    //            Console.WriteLine("Press Q to Exit");
    //            Console.ReadLine();
    //            server.Disconect();
    //            // }
    //            //else
    //            //{
    //            //    var message = Console.ReadLine();

    //            //    while (message != "Q")
    //            //    {
    //            //        if (!string.IsNullOrEmpty(message))
    //            //            server.SendMessage(message,username);
    //            //        message = Console.ReadLine();
    //            //    }

    //            //}
    //        }
    //        catch { };
    //        #endregion

    //        //waveSource = new WaveInEvent();
    //        //waveSource.WaveFormat = new WaveFormat(8000, 16, 1);
    //        //waveSource.DeviceNumber = 0;
    //        //memstr = new MemoryStream();
    //        //sw = new BinaryWriter(memstr);
    //        //sr = new BinaryReader(memstr);

    //        //waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
    //        //play.PlayAsync();
    //        //// waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped); //Его расписывать не буду, там все понятно.
    //        //var name = Console.ReadLine();
    //        //server.Join(name);

    //        //waveSource.StartRecording();



    //        //while (true)
    //        //{
    //        //    byte[] bt = new byte[320];
    //        //    bt = sr.ReadBytes(320);
    //        //    if (bt.Length != 0)
    //        //    {
    //        //        server.SendAudoAAA(bt, " ");
    //        //        testoffset += bt.Length;
    //        //    }
    //        //}


    //        //Console.WriteLine("Press Q to Exit");
    //        //var message = Console.ReadLine();
    //        //server.Disconect();


    //        //        input.StartRecording();
    //        //        //Console.WriteLine("Enter message");
    //        //        Console.WriteLine("Press Q to Exit");
    //        //        var message = Console.ReadLine();
    //        //        server.Disconect();
    //    }
    //    //static void waveSource_DataAvailable(object sender, WaveInEventArgs e)
    //    //{
    //    //    //Пишем голос с микрофона в поток
    //    //    sw.Write(e.Buffer);

    //    //    sw.Flush();
    //    //}

    //}
    #endregion

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
        static void Main(string[] args)
        {
            NetTcpBinding netTcpBinding = new NetTcpBinding();
            netTcpBinding.Security.Mode = SecurityMode.None;
            netTcpBinding.Security.Transport.SslProtocols = System.Security.Authentication.SslProtocols.None;
            factory =new DuplexChannelFactory<IChatService>(context, netTcpBinding, "net.tcp://localhost:7998/ServerHost/Chat");
            server = factory.CreateChannel();
            
            
           //try
           //{
           //     for (int n = 0; n < WaveIn.DeviceCount; n++)
           //     {
           //         var capabilities = WaveIn.GetCapabilities(n);
           //         Console.WriteLine(capabilities.ProductName);
           //     }

           //     input = new WaveInEvent();
           //     input.DeviceNumber = 0;
           //     //определяем его формат - частота дискретизации 8000 Гц, ширина сэмпла - 16 бит, 1 канал - моно
           //     input.WaveFormat = new WaveFormat(8000, 16, 1);
           //     //добавляем код обработки нашего голоса, поступающего на микрофон
           //     input.DataAvailable += Voice_Input;
           //     //создаем поток для прослушивания входящего звука
           //     output = new WaveOutEvent();
           //     //создаем поток для буферного потока и определяем у него такой же формат как и потока с микрофона
           //     bufferStream = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
           //     //привязываем поток входящего звука к буферному потоку
           //     output.Init(bufferStream);
           //     Console.Write("Enter username:");
           //     username = Console.ReadLine();
           //     server.Join(username);
           //     Console.WriteLine();
           //     Console.WriteLine("Аудио 1,Чат 2");
           //     int type = int.Parse(Console.ReadLine());
           //     if (type == 1)
           //     {
           //         input.StartRecording();
           //         //Console.WriteLine("Enter message");
           //         Console.WriteLine("Press Q to Exit");
           //         var message = Console.ReadLine();
           //         server.Disconect();
           //         server.GetUserList();
           //     }
           //     else
           //     {
           //         var message = Console.ReadLine();

           //         while (message != "Q")
           //         {
           //             if (!string.IsNullOrEmpty(message))
           //                 server.SendMessage(message, username);
           //             message = Console.ReadLine();
           //         }

           //     }
           //}
           //catch { };
           
        }

    }
}
