using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace MomoBot
{
    public partial class SelectPixelForm : Form
    {
        private MainForm main;
        private MainForm.ButtonTypes type;
        private string selectedPixel;
        private List<string> listItems;

        public SelectPixelForm(MainForm main, MainForm.ButtonTypes type, string text)
        {
            this.main = main;
            this.type = type;
            InitializeComponent();
            this.selectedPixel = string.Empty;
            Process[] processes = Process.GetProcesses();
            Process window = null;
            foreach (Process p in processes)
            {
                if (p.MainWindowTitle == Settings.GameWindow)
                {
                    window = p;
                    break;
                }
            }
            if (window != null)
            {
                Bitmap picture = ScreenCapture.PrintWindow(window.MainWindowHandle);
                this.picture.Size = picture.Size;
                this.picture.Image = picture;
            }
            if (text != "None")
                this.listItems = Extensions.StringToList(text);
            else
                this.listItems = new List<string>();
            this.refreshList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.selectedPixel != string.Empty)
            {
                if (!this.listItems.Contains(this.selectedPixel))
                {
                    this.listItems.Add(this.selectedPixel);
                    this.refreshList();
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (this.list.SelectedItem != null)
            {
                if (this.listItems.Contains((string)this.list.SelectedItem)) ;
                this.listItems.Remove((string)this.list.SelectedItem);
                this.refreshList();
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            string final = string.Empty;
            List<string> selections = (List<string>)this.list.DataSource;
            foreach (string s in selections)
                final += s + ",";
            if (final == string.Empty)
                final = "None";
            else
                final = final.Remove(final.Length - 1);
            this.main.updateButtonText(this.type, final);
            base.OnFormClosing(e);
        }
        
        private void refreshList()
        {
            this.list.DataSource = null;
            this.list.DataSource = this.listItems;
            this.list.DisplayMember = "";
            this.list.DisplayMember = "PixelPatterns";
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = this.picture.PointToClient(Cursor.Position);
            Bitmap bmp = (Bitmap)this.picture.Image;
            if (bmp != null)
            {
                Color c = bmp.GetPixel(p.X, p.Y);
                this.selectedPixel = p.X.ToString() + "." + p.Y.ToString() + "." + c.R.ToString() + "." + c.G.ToString() + "." + c.B.ToString();
                Image image = this.selectedColor.Image;
                bmp = new Bitmap(30, 20);
                for (int i = 0; i < 30; i++)
                    for (int j = 0; j < 20; j++)
                        bmp.SetPixel(i, j, c);
                this.selectedColor.Image = bmp;
            }
        }
    }
}
