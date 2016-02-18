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
    public partial class InputKeyForm : Form
    {
        private MainForm main;
        private MultiInputForm multiForm;
        private MainForm.ButtonTypes type;
        public InputKeyForm(MainForm main, MainForm.ButtonTypes type)
        {
            this.main = main;
            this.type = type;
            InitializeComponent();
        }
        public InputKeyForm(MultiInputForm multi, MainForm.ButtonTypes type)
        {
            this.multiForm = multi;
            this.type = type;
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            if (key == Keys.Tab || key == Keys.Return || key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right)
            {
                this.InputKeyForm_KeyDown(null, new KeyEventArgs(key));
                return true;
            }
            return base.ProcessCmdKey(ref msg, key);
        }

        private void InputKeyForm_KeyDown(object sender, KeyEventArgs e)
        {
            string name = Extensions.KeyToString(e.KeyCode);
            if (name != string.Empty)
            {
                if (this.main != null)
                    this.main.updateButtonText(this.type, name);
                else if (this.multiForm != null)
                    this.multiForm.addKey(name);
                this.Close();
            }
            e.Handled = true;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
