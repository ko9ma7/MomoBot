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
    public partial class MainForm : Form
    {
        private ProcessesForm processesForm;
        private SelectPixelForm pixelForm;
        private InputKeyForm inputForm;
        private MultiInputForm multiInputForm;
        public enum ButtonTypes
        {
            GameWindow,
            TargetPixel,
            TargetKey,
            AttackKeys,
            HPPixel,
            HPKey,
            MPPixel,
            MPKey,
            DeathPixel,
            DeathCoord,
            RestPixel,
            RestKey,
            MapPixel,
            MapKey,
            MapCoords,
            StartKey,
            Stopkey
        }
        Button[] buttons;
        public MainForm()
        {
            InitializeComponent();
            this.buttons = new Button[] { this.buttonGameWindow, this.buttonTargetPixel, this.buttonTargetKey, this.buttonTargetAttack, this.buttonHPPixel, this.buttonHPKey, this.buttonMPPixel, this.buttonMPKey, this.buttonDeathPixel, this.buttonDeathClick, this.buttonRestPixel, this.buttonRestKey, this.buttonWaypointsPixel, this.buttonWaypointsKey, this.buttonWaypointsCoords, this.buttonStartKey, this.buttonStopKey };
            if (!Settings.Initialized)
                this.updateSettings();
            else
                this.updateForm();
        }
        public void updateButtonText(ButtonTypes type, string str)
        {
            this.buttons[(int)type].Text = str;
        }
        public void updateSettings()
        {
            Settings.GameWindow = this.buttonGameWindow.Text;
            Settings.DetectTarget = this.checkboxTarget.Checked;
            Settings.TargetPixels = Extensions.StringToPixels(this.buttonTargetPixel.Text);
            Settings.TargetKey = Extensions.StringToKey(this.buttonTargetKey.Text);
            Settings.AttackKeys = Extensions.StringToKeys(this.buttonTargetAttack.Text);
            Settings.MinDelay = Extensions.TryParseInt(this.textboxTargetMinDelay.Text);
            Settings.MaxDelay = Extensions.TryParseInt(this.textboxTargetMaxDelay.Text);
            Settings.DetectHP = this.checkboxHP.Checked;
            Settings.HPPixels = Extensions.StringToPixels(this.buttonHPPixel.Text);
            Settings.HPKey = Extensions.StringToKey(this.buttonHPKey.Text);
            Settings.DetectMP = this.checkboxMP.Checked;
            Settings.MPPixels = Extensions.StringToPixels(this.buttonMPPixel.Text);
            Settings.MPKey = Extensions.StringToKey(this.buttonMPKey.Text);
            Settings.DetectDeath = this.checkboxDeath.Checked;
            Settings.DeathPixels = Extensions.StringToPixels(this.buttonDeathPixel.Text);
            Settings.ReviveClick = Extensions.StringToCoords(this.buttonDeathClick.Text);
            Settings.DetectRest = this.checkboxRest.Checked;
            Settings.RestPixels = Extensions.StringToPixels(this.buttonRestPixel.Text);
            Settings.RestKey = Extensions.StringToKey(this.buttonRestKey.Text);
            Settings.RestTime = Extensions.TryParseInt(this.textboxRestTime.Text);
            Settings.Waypoints = this.checkboxWaypoints.Checked;
            Settings.MapPixels = Extensions.StringToPixels(this.buttonWaypointsPixel.Text);
            Settings.MapKey = Extensions.StringToKey(this.buttonWaypointsKey.Text);
            Settings.WaypointTime = Extensions.TryParseInt(this.textboxWaypointsTime.Text);
            Settings.WaypointCoords = Extensions.StringToCoords(this.buttonWaypointsCoords.Text);
            Settings.StartKey = Extensions.StringToKey(this.buttonStartKey.Text);
            Settings.StopKey = Extensions.StringToKey(this.buttonStopKey.Text);
        }
        private void updateForm()
        {
            this.buttonGameWindow.Text = Settings.GameWindow;
            this.checkboxTarget.Checked = Settings.DetectTarget;
            this.buttonTargetPixel.Text = Extensions.ListToString(Settings.TargetPixels);
            this.buttonTargetKey.Text = Extensions.KeyToString(Settings.TargetKey);
            this.buttonTargetAttack.Text = Extensions.KeyListToString(Settings.AttackKeys);
            this.textboxTargetMinDelay.Text = Settings.MinDelay.ToString();
            this.textboxTargetMaxDelay.Text = Settings.MaxDelay.ToString();
            this.checkboxHP.Checked = Settings.DetectHP;
            this.buttonHPPixel.Text = Extensions.ListToString(Settings.HPPixels);
            this.buttonHPKey.Text = Extensions.KeyToString(Settings.HPKey);
            this.checkboxMP.Checked = Settings.DetectMP;
            this.buttonMPPixel.Text = Extensions.ListToString(Settings.MPPixels);
            this.buttonMPKey.Text = Extensions.KeyToString(Settings.MPKey);
            this.checkboxDeath.Checked = Settings.DetectDeath;
            this.buttonDeathPixel.Text = Extensions.ListToString(Settings.DeathPixels);
            this.buttonDeathClick.Text = Extensions.ListToString(Settings.DeathPixels);
            this.checkboxRest.Checked = Settings.DetectRest;
            this.buttonRestPixel.Text = Extensions.ListToString(Settings.RestPixels);
            this.buttonRestKey.Text = Extensions.KeyToString(Settings.RestKey);
            this.textboxRestTime.Text = Settings.RestTime.ToString();
            this.checkboxWaypoints.Checked = Settings.Waypoints;
            this.buttonWaypointsPixel.Text = Extensions.ListToString(Settings.MapPixels);
            this.buttonWaypointsKey.Text = Extensions.KeyToString(Settings.MapKey);
            this.textboxWaypointsTime.Text = Settings.WaypointTime.ToString();
            this.buttonWaypointsCoords.Text = Extensions.ListToString(Settings.WaypointCoords);
            this.buttonStartKey.Text = Extensions.KeyToString(Settings.StartKey);
            this.buttonStopKey.Text = Extensions.KeyToString(Settings.StopKey);
        }
        private void showPixelForm(ButtonTypes type)
        {
            if (this.pixelForm != null)
                this.pixelForm.Close();
            if (Settings.GameWindow != "None")
            {
                this.pixelForm = new SelectPixelForm(this, type, this.buttons[(int)type].Text);
                this.pixelForm.Show();
                this.pixelForm.Location = this.centerPoint(this.pixelForm);
                this.pixelForm.BringToFront();
            }
            else
                MessageBox.Show("No process selected!");
        }
        private void showInputForm(ButtonTypes type)
        {
            if (this.inputForm != null)
                this.inputForm.Close();
            this.inputForm = new InputKeyForm(this, type);
            this.inputForm.Location = this.centerPoint(this.inputForm);
            this.inputForm.ShowDialog();
        }
        private void buttonGameWindow_Click(object sender, EventArgs e)
        {
            if (this.processesForm != null)
                this.processesForm.Close();
            this.processesForm = new ProcessesForm(this);
            this.processesForm.Show();
            this.processesForm.Location = this.centerPoint(this.processesForm);
            this.processesForm.BringToFront();
        }
        private void buttonTargetPixel_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.TargetPixel);
        }

        private void buttonTargetKey_Click(object sender, EventArgs e)
        {
            this.showInputForm(ButtonTypes.TargetKey);
        }
        private Point centerPoint(Form f)
        {
            return new Point(this.Location.X + this.Width / 2 - f.Width / 2, this.Location.Y + this.Height / 2 - f.Height / 2);
        }

        private void buttonTargetAttack_Click(object sender, EventArgs e)
        {
            if (this.multiInputForm != null)
                this.multiInputForm.Close();
            this.multiInputForm = new MultiInputForm(this, ButtonTypes.AttackKeys, this.buttonTargetAttack.Text);
            this.multiInputForm.Location = this.centerPoint(this.multiInputForm);
            this.multiInputForm.ShowDialog();
        }

        private void buttonHPPixel_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.HPPixel);
        }

        private void buttonHPKey_Click(object sender, EventArgs e)
        {
            this.showInputForm(ButtonTypes.HPKey);
        }

        private void buttonMPPixel_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.MPPixel);
        }

        private void buttonMPKey_Click(object sender, EventArgs e)
        {
            this.showInputForm(ButtonTypes.MPKey);
        }

        private void buttonDeathPixel_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.DeathPixel);
        }

        private void buttonDeathClick_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.DeathCoord);
        }

        private void buttonRestPixel_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.RestPixel);
        }

        private void buttonRestKey_Click(object sender, EventArgs e)
        {
            this.showInputForm(ButtonTypes.RestKey);
        }

        private void buttonWaypointsPixel_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.MapPixel);
        }

        private void buttonWaypointsKey_Click(object sender, EventArgs e)
        {
            this.showInputForm(ButtonTypes.MapKey);
        }

        private void buttonWaypointsCoords_Click(object sender, EventArgs e)
        {
            this.showPixelForm(ButtonTypes.MapCoords);
        }

        private void buttonStartKey_Click(object sender, EventArgs e)
        {
            this.showInputForm(ButtonTypes.StartKey);
        }

        private void buttonStopKey_Click(object sender, EventArgs e)
        {
            this.showInputForm(ButtonTypes.Stopkey);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.updateSettings();
            Bot.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Bot.Stop();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            this.textboxExport.Text = Settings.serialize();
        }
        private void buttonImport_Click(object sender, EventArgs e)
        {
            Settings.loadFromString(this.textboxImport.Text);
            this.updateForm();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.updateSettings();
            Settings.save();
        }
    }
}
