
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarrenLee;

namespace Mic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DarrenLee.LiveStream.Audio.Sender audiosender = new DarrenLee.LiveStream.Audio.Sender();
            audiosender.Send("127.0.0.1", 4500);
        }


        private void button1_Click(object sender, EventArgs e)
        {


        }

    }
}
