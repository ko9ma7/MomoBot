using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace MomoBot
{
    public partial class ProcessesForm : Form
    {
        public MainForm main;
        Timer timer;
        string lastSelected;
        public ProcessesForm(MainForm main)
        {
            this.main = main;
            this.timer = new Timer();
            this.lastSelected = string.Empty;
            InitializeComponent();
            this.updateProcesses(null, null);
            this.timer.Interval = 1000;
            this.timer.Tick += this.updateProcesses;
            this.timer.Enabled = true;
        }
        private void updateProcesses(object sender, EventArgs e)
        {
            this.lastSelected = (string)this.list.SelectedItem;
            Process[] processes = Process.GetProcesses();
            List<string> names = new List<string>();
            foreach (Process p in processes)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    names.Add(p.MainWindowTitle);
                }
            }
            this.list.DataSource = names;
            if (names.Contains(this.lastSelected))
                this.list.SelectedItem = this.lastSelected;
        }
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            this.main.updateButtonText(MainForm.ButtonTypes.GameWindow, (string)this.list.SelectedItem);
            Settings.GameWindow = (string)this.list.SelectedItem;
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.timer.Stop();
            base.OnFormClosing(e);
        }
    }
}
