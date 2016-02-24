using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MomoBot
{
    public class Settings
    {
        public static bool Initialized;
        public static string GameWindow;
        public static bool DetectTarget;
        public static List<PixelPattern> TargetPixels;
        public static Keys TargetKey;
        public static List<Keys> AttackKeys;
        public static int MinDelay;
        public static int MaxDelay;
        public static bool DetectHP;
        public static List<PixelPattern> HPPixels;
        public static Keys HPKey;
        public static bool DetectMP;
        public static List<PixelPattern> MPPixels;
        public static Keys MPKey;
        public static bool DetectDeath;
        public static List<PixelPattern> DeathPixels;
        public static List<Coordinate> ReviveClick;
        public static bool DetectRest;
        public static List<PixelPattern> RestPixels;
        public static Keys RestKey;
        public static int RestTime;
        public static bool Waypoints;
        public static List<PixelPattern> MapPixels;
        public static Keys MapKey;
        public static int WaypointTime;
        public static List<Coordinate> WaypointCoords;
        public static Keys StartKey;
        public static Keys StopKey;

        public static void loadFromString(string str)
        {
            foreach (string s in str.Split('\n'))
            {
                string[] strArray = s.Split(':');
                if (strArray.Length > 1)
                {
                    string value = strArray[1];
                    if (s.StartsWith("GameWindow:"))
                        GameWindow = value;
                    else if (s.StartsWith("DetectTarget:"))
                        DetectTarget = Convert.ToBoolean(value);
                    else if (s.StartsWith("TargetPixels:"))
                        TargetPixels = Extensions.StringToPixels(value);
                    else if (s.StartsWith("TargetKey:"))
                        TargetKey = Extensions.StringToKey(value);
                    else if (s.StartsWith("AttackKeys:"))
                        AttackKeys = Extensions.StringToKeys(value);
                    else if (s.StartsWith("MinDelay:"))
                        MinDelay = Extensions.TryParseInt(value);
                    else if (s.StartsWith("MaxDelay:"))
                        MaxDelay = Extensions.TryParseInt(value);
                    else if (s.StartsWith("DetectHP:"))
                        DetectHP = Convert.ToBoolean(value);
                    else if (s.StartsWith("HPPixels:"))
                        HPPixels = Extensions.StringToPixels(value);
                    else if (s.StartsWith("HPKey:"))
                        HPKey = Extensions.StringToKey(value);
                    else if (s.StartsWith("DetectMP:"))
                        DetectMP = Convert.ToBoolean(value);
                    else if (s.StartsWith("MPPixels:"))
                        MPPixels = Extensions.StringToPixels(value);
                    else if (s.StartsWith("MPKey:"))
                        MPKey = Extensions.StringToKey(value);
                    else if (s.StartsWith("DetectDeath:"))
                        DetectDeath = Convert.ToBoolean(value);
                    else if (s.StartsWith("DeathPixels:"))
                        DeathPixels = Extensions.StringToPixels(value);
                    else if (s.StartsWith("ReviveClick:"))
                        ReviveClick = Extensions.StringToCoords(value);
                    else if (s.StartsWith("DetectRest:"))
                        DetectRest = Convert.ToBoolean(value);
                    else if (s.StartsWith("RestPixels:"))
                        RestPixels = Extensions.StringToPixels(value);
                    else if (s.StartsWith("RestKey:"))
                        RestKey = Extensions.StringToKey(value);
                    else if (s.StartsWith("RestTime"))
                        RestTime = Extensions.TryParseInt(value);
                    else if (s.StartsWith("Waypoints:"))
                        Waypoints = Convert.ToBoolean(value);
                    else if (s.StartsWith("MapPixels:"))
                        MapPixels = Extensions.StringToPixels(value);
                    else if (s.StartsWith("MapKey:"))
                        MapKey = Extensions.StringToKey(value);
                    else if (s.StartsWith("WayPointTime:"))
                        WaypointTime = Extensions.TryParseInt(value);
                    else if (s.StartsWith("WaypointCoords:"))
                        WaypointCoords = Extensions.StringToCoords(value);
                    else if (s.StartsWith("StartKey:"))
                        StartKey = Extensions.StringToKey(value);
                    else if (s.StartsWith("StopKey:"))
                        StopKey = Extensions.StringToKey(value);
                }
            }
        }
        public static string serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GameWindow:" + GameWindow + "\n");
            sb.Append("DetectTarget:" + DetectTarget + "\n");
            sb.Append("TargetPixels:" + Extensions.ListToString(TargetPixels) + "\n");
            sb.Append("TargetKey:" + Extensions.KeyToString(TargetKey) + "\n");
            sb.Append("AttackKeys:" + Extensions.KeyListToString(AttackKeys) + "\n");
            sb.Append("MinDelay:" + MinDelay + "\n");
            sb.Append("MaxDelay:" + MaxDelay + "\n");
            sb.Append("DetectHP:" + DetectHP + "\n");
            sb.Append("HPPixels:" + Extensions.ListToString(HPPixels) + "\n");
            sb.Append("HPKey:" + Extensions.KeyToString(HPKey) + "\n");
            sb.Append("DetectMP:" + DetectMP + "\n");
            sb.Append("MPPixels:" + Extensions.ListToString(MPPixels) + "\n");
            sb.Append("MPKey:" + Extensions.KeyToString(MPKey) + "\n");
            sb.Append("DetectDeath:" + DetectDeath + "\n");
            sb.Append("DeathPixels:" + Extensions.ListToString(DeathPixels) + "\n");
            sb.Append("ReviveClick:" + Extensions.ListToString(ReviveClick) + "\n");
            sb.Append("DetectRest:" + DetectRest + "\n");
            sb.Append("RestPixels:" + Extensions.ListToString(RestPixels) + "\n");
            sb.Append("RestKey:" + Extensions.KeyToString(RestKey) + "\n");
            sb.Append("RestTime:" + RestTime + "\n");
            sb.Append("Waypoints:" + Waypoints + "\n");
            sb.Append("MapPixels:" + Extensions.ListToString(MapPixels) + "\n");
            sb.Append("MapKey:" + Extensions.KeyToString(MapKey) + "\n");
            sb.Append("WayPointTime:" + WaypointTime + "\n");
            sb.Append("WaypointCoords:" + Extensions.ListToString(WaypointCoords) + "\n");
            sb.Append("StartKey:" + Extensions.KeyToString(StartKey) + "\n");
            sb.Append("StopKey:" + Extensions.KeyToString(StopKey) + "\n");
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
