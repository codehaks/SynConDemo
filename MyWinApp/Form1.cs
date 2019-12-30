using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager;

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
            listBox1.Items.Add($"Before delay ...{Thread.CurrentThread.ManagedThreadId}");
            //await DoAsync().ConfigureAwait(true);
            await DelayMe.DoAsync().ConfigureAwait(false);
            button1.BackColor = Color.Azure;
            listBox1.Items.Add($"After delay...{Thread.CurrentThread.ManagedThreadId}");
            button1.Text = "Hey";

            label1.Text = "Done!";
        }

        private async Task DoAsync()
        {
            listBox1.Items.Add($"Delay start {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                listBox1.Items.Add($"Run {Thread.CurrentThread.ManagedThreadId}");
            });
            listBox1.Items.Add($"Delay end {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
