using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomoBot
{
    public class Bot
    {
        private static Timer timer;
        private static IntPtr windowHandle;
        private static Bitmap screen;
        private static int updateTime = 100;
        private static int transitionTime;
        private static int tabCount;
        private static int attackTime;

        private static Random r;
        private static ScreenCapture capture;

        private static int HPWait;
        private static int MPWait;

        private const int SW_SHOWNORMAL = 1;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hwnd);
        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [STAThread]
        static void Main()
        {
            Settings.load();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        public static void Start()
        {
            foreach (Keys k in Enum.GetValues(typeof(Keys)))
            {
                Console.WriteLine(k.ToString() + (int)k);
            }
            Stop();
            r = new Random();
            transitionTime = 0;
            tabCount = 0;
            attackTime = 0;
            capture = new ScreenCapture();
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
                windowHandle = window.MainWindowHandle;
            timer = new Timer();
            timer.Interval = updateTime;
            timer.Tick += Update;
            timer.Enabled = true;
        }

        public static void Stop()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
        }
            
        
        private static void Update(object source, EventArgs e)
        {
            timer.Stop();
            ShowWindow(windowHandle, SW_SHOWNORMAL);
            SetForegroundWindow(windowHandle);
            screen = capture.CaptureScreen(windowHandle);
            bool isDead = false;
            bool hasTarget = false;
            if (Settings.DetectDeath && !CheckPixels(Settings.DeathPixels))
                isDead = true;
            if (Settings.DetectTarget && CheckPixels(Settings.TargetPixels))
                hasTarget = true;
            if (isDead)
                SendClick(Settings.ReviveClick[0]);
            else
            {
                UpdateTimers();
                if (Settings.DetectHP && HPWait <= 0 && !CheckPixels(Settings.HPPixels))
                {
                    HPWait = 3000;
                    SendKey(Settings.HPKey);
                }
                if (Settings.DetectMP && MPWait <= 0 && !CheckPixels(Settings.MPPixels))
                {
                    MPWait = 3000;
                    SendKey(Settings.MPKey);
                }
                if (attackTime <= 0 && hasTarget)
                {
                    attackTime = Settings.MinDelay + r.Next(Settings.MaxDelay - Settings.MinDelay);
                    SendKey(Settings.AttackKeys[r.Next(Settings.AttackKeys.Count)]);
                }
                if (!hasTarget)
                {
                    if (transitionTime <= 0)
                    {
                        transitionTime = 0;
                        if (Settings.DetectTarget && (!Settings.Waypoints || tabCount < 20))
                        {
                            tabCount++;
                            SendKey(Settings.TargetKey);
                            transitionTime = Settings.MinDelay + r.Next(Settings.MaxDelay - Settings.MinDelay);
                        }
                        else if (Settings.DetectRest && !CheckPixels(Settings.RestPixels))
                        {
                            transitionTime = Settings.RestTime;
                            SendKey(Settings.RestKey);
                        }
                        else if (Settings.Waypoints)
                        {
                            tabCount = 0;
                            while (!CheckPixels(Settings.MapPixels))
                            {
                                SendKey(Settings.MapKey);
                                System.Threading.Thread.Sleep(500);
                            }
                            SendClick(Settings.WaypointCoords[r.Next(Settings.WaypointCoords.Count)]);
                            transitionTime = Settings.WaypointTime;
                        }
                    }
                }
            }
            timer.Start();
        }

        private static void UpdateTimers()
        {
            HPWait -= updateTime; 
            MPWait -= updateTime;
            transitionTime -= updateTime;
            attackTime -= updateTime;
        }
        private static void SendKey(Keys key)
        {
            Console.WriteLine(key.ToString());
        }
        private static void SendClick(Coordinate coord)
        {

        }
        private static bool CheckPixels(List<PixelPattern> list)
        {
            foreach (PixelPattern pixel in list)
            {
                Color c = screen.GetPixel(pixel.coordinates.x, pixel.coordinates.y);
                int rDiff = Math.Abs(c.R - pixel.r);
                int gDiff = Math.Abs(c.G - pixel.g);
                int bDiff = Math.Abs(c.B - pixel.b);
                if (rDiff > PixelPattern.range || gDiff > PixelPattern.range || bDiff > PixelPattern.range)
                    return false;
            }
            return true;
        }



    }
}
