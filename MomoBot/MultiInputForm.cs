using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomoBot
{
    public partial class MultiInputForm : Form
    {
        private MainForm main;
        private InputKeyForm inputForm;
        private MainForm.ButtonTypes type;
        private List<string> listItems;

        public MultiInputForm(MainForm main, MainForm.ButtonTypes type, string text)
        {
            this.main = main;
            this.type = type;
            InitializeComponent();
            if (text != "None")
                this.listItems = Extensions.StringToList(text);
            else
                this.listItems = new List<string>();
            this.refreshList();
        }
        public void addKey(string str)
        {
            if (!this.listItems.Contains(str))
            {
                this.listItems.Add(str);
                this.refreshList();
            }
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
            this.list.DisplayMember = "InputKeys";
        }
        private Point centerPoint(Form f)
        {
            return new Point(this.Location.X + this.Width / 2 - f.Width / 2, this.Location.Y + this.Height / 2 - f.Height / 2);
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.inputForm != null)
                this.inputForm.Close();
            this.inputForm = new InputKeyForm(this, this.type);
            this.inputForm.Show();
            this.inputForm.Location = this.centerPoint(this.inputForm);
            this.inputForm.BringToFront();
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
    }
}
