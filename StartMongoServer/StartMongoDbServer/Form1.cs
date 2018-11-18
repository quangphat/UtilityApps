using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartMongoDbServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Hide();
        }

        private void StartCmd1()
        {
            Process process = new Process();
            ProcessStartInfo processInfo = new ProcessStartInfo(@"D:\Development\my8\MongoDb\cmd1.bat");
            process.StartInfo = processInfo;
            process.Start();
        }
        private void StartCmd2()
        {
            Process process = new Process();
            ProcessStartInfo processInfo = new ProcessStartInfo(@"D:\Development\my8\MongoDb\cmd2.bat");
            process.StartInfo = processInfo;
            process.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartCmd1();
            var t = Task.Run(async delegate
            {
                await Task.Delay(2000);
            });
            t.Wait();
            StartCmd2();
            label1.Show();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartCmd1();
            var t = Task.Run(async delegate
            {
                await Task.Delay(2000);
            });
            t.Wait();
            StartCmd2();
            label1.Show();
        }
    }
}
