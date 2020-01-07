using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            System.Diagnostics.Trace.WriteLine($"Before delay ...{Thread.CurrentThread.ManagedThreadId}");

            await DoAsync().ConfigureAwait(true);
            
            button1.BackColor = Color.Red;
            System.Diagnostics.Trace.WriteLine($"After delay ...{Thread.CurrentThread.ManagedThreadId}");
            
            label1.Text = "Done!";
        }

        private async Task DoAsync()
        {
            //listBox1.Items.Add($"Delay start {Thread.CurrentThread.ManagedThreadId}");

            System.Diagnostics.Trace.WriteLine($"DoAsync start ...{Thread.CurrentThread.ManagedThreadId}");
            var clinet = new HttpClient();
            //await clinet.GetAsync("https://codehaks.com");
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                System.Diagnostics.Trace.WriteLine($"DoAsync run ...{Thread.CurrentThread.ManagedThreadId}");
            });
            System.Diagnostics.Trace.WriteLine($"DoAsync end ...{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
