using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mic2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DarrenLee.LiveStream.Audio.Receiver audioreceiver = new DarrenLee.LiveStream.Audio.Receiver();
            audioreceiver.Receive("127.0.0.1", 4500);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
