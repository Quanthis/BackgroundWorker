using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorkerExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if(!T2.IsBusy)
            {
                progressBar1.Visible = true;
                T2.RunWorkerAsync();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if(T2.WorkerSupportsCancellation)
            {
                T2.CancelAsync();
            }
        }

        private void T2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            for (int i  =  0; i <= 10; i++)
            {
                if(bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    
                    System.Threading.Thread.Sleep(500);
                    bw.ReportProgress(i * 10);
                }
            }
        }

        private void T2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ResultLabel.Text = (e.ProgressPercentage.ToString() + "%");
            progressBar1.Increment(10);
            progressBar1.Update();
        }

        private void T2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ResultLabel.Text = "Cancelled!";
                ResetProgressBar();
            }
            else if(e.Error != null)
            {
                ResultLabel.Text = "Error has occured";
                ResetProgressBar();
            }
            else
            {
                ResultLabel.Text = "Done";
                ResetProgressBar();
            }
        }

        private void ResetProgressBar()
        {
            progressBar1.Visible = false;
            progressBar1.Value = 0;
            progressBar1.Update();
        }
    }
}
