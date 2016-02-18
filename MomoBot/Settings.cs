using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MomoBot
{
    public class Settings
    {
        public static bool Initialized;
        public static string GameWindow;
        public static bool DetectTarget;
        public static List<PixelPattern> TargetPixels;
        public static string TargetKey;
        public static List<string> AttackKeys;
        public static double MinDelay;
        public static double MaxDelay;
        public static bool DetectHP;
        public static List<PixelPattern> HPPixels;
        public static string HPKey;
        public static bool DetectMP;
        public static List<PixelPattern> MPPixels;
        public static string MPKey;
        public static bool DetectDeath;
        public static List<PixelPattern> DeathPixels;
        public static List<Coordinate> ReviveClick;
        public static bool DetectRest;
        public static List<PixelPattern> RestPixels;
        public static string RestKey;
        public static double RestTime;
        public static bool Waypoints;
        public static List<PixelPattern> MapPixels;
        public static string MapKey;
        public static double WaypointTime;
        public static List<Coordinate> WaypointCoords;
        public static string StartKey;
        public static string StopKey;

        public static void loadFromString(string str)
        {
            foreach (string s in str.Split('\n'))
            {
                if (s.StartsWith("GameWindow:"))
                    GameWindow = s.Split(':')[1];
                else if (s.StartsWith("DetectTarget:"))
                    DetectTarget = Convert.ToBoolean(s.Split(':')[1]);
            }
        }
        public static string serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GameWindow:" + GameWindow + "\n");
            sb.Append("DetectTarget:" + DetectTarget + "\n");
            sb.Append("TargetPixels:" + Extensions.ListToString(TargetPixels) + "\n");
            sb.Append("TargetKey:" + TargetKey + "\n");
            sb.Append("AttackKeys:" + Extensions.ListToString(AttackKeys) + "\n");
            sb.Append("MinDelay:" + MinDelay + "\n");
            sb.Append("MaxDelay:" + MaxDelay + "\n");
            sb.Append("DetectHP:" + DetectHP + "\n");
            sb.Append("HPPixels:" + Extensions.ListToString(HPPixels) + "\n");
            sb.Append("HPKey:" + HPKey + "\n");
            sb.Append("DetectMP:" + DetectMP + "\n");
            sb.Append("MPPixels:" + Extensions.ListToString(MPPixels) + "\n");
            sb.Append("MPKey:" + MPKey + "\n");
            sb.Append("DetectDeath:" + DetectDeath + "\n");
            sb.Append("DeathPixels:" + Extensions.ListToString(DeathPixels) + "\n");
            sb.Append("ReviveClick:" + ReviveClick.ToString() + "\n");
            sb.Append("DetectRest:" + DetectRest + "\n");
            sb.Append("RestPixels:" + Extensions.ListToString(RestPixels) + "\n");
            sb.Append("RestKey:" + RestKey + "\n");
            sb.Append("Waypoints:" + Waypoints + "\n");
            sb.Append("MapPixels:" + Extensions.ListToString(MapPixels) + "\n");
            sb.Append("MapKey:" + MapKey + "\n");
            sb.Append("WayPointTime:" + WaypointTime + "\n");
            sb.Append("WaypointCoords:" + Extensions.ListToString(WaypointCoords) + "\n");
            sb.Append("StartKey:" + StartKey + "\n");
            sb.Append("StopKey:" + StopKey + "\n");
            return sb.ToString();
        }
        public static void load()
        {
            if (File.Exists("settings.bot"))
            {
                StreamReader reader = new StreamReader("settings.bot");
                loadFromString(reader.ReadToEnd());
                reader.Close();
                Initialized = true;
            }
        }
        public static void save()
        {
            string write = serialize();
            StreamWriter file = new StreamWriter("settings.bot", false);
            file.WriteLine(write);
            file.Close();
        }
    }
}
