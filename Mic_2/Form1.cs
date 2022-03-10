using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mic_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DarrenLee.LiveStream.Audio.Receiver audioReceiver = new DarrenLee.LiveStream.Audio.Receiver();
            audioReceiver.Receive("localhost", 13000);
        }
    }
}
