using LumiSoft.Media.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro
{
    class Program
    {
        private static WaveIn m_pSoundReceiver = null;
        
        static void Main(string[] args)
        {
            m_pSoundReceiver = new WaveIn(WaveIn.Devices[0], 8000, 16, 1, 400);
            m_pSoundReceiver.BufferFull += new BufferFullHandler
                                             (m_pSoundReceiver_BufferFull);
            m_pSoundReceiver.Start();
            while (true) { }
        }

        private static void m_pSoundReceiver_BufferFull(byte[] buffer)
        {
            Console.WriteLine(string.Join("", buffer));
            WaveOut m_pWaveOut = new WaveOut(WaveOut.Devices[0], 8000, 16, 1);
            m_pWaveOut.Play(buffer, 0, buffer.Length);
        }
    }
}
