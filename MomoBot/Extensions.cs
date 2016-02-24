using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomoBot
{
    public class Extensions
    {
        public static string ListToString<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T element in list)
                sb.Append(element.ToString()).Append(",");
            if (list.Count > 0)
                sb.Remove(sb.Length - 1, 1);
            string final = sb.ToString();
            if (final == string.Empty)
                return "None";
            return final;
        }
        public static string KeyListToString(List<Keys> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Keys element in list)
                sb.Append(KeyToString(element)).Append(",");
            if (list.Count > 0)
                sb.Remove(sb.Length - 1, 1);
            string final = sb.ToString();
            if (final == string.Empty)
                return "None";
            return final;
        }
        public static List<string> StringToList(string str)
        {
            List<string> list = new List<string>();
            if (str != "None")
            {
                string[] strArray = str.Split(',');
                foreach (string s in strArray)
                    list.Add(s);
            }
            return list;
        }
        public static List<Keys> StringToKeys(string str)
        {
            List<Keys> keys = new List<Keys>();
            if (str != "None")
            {
                List<string> list = StringToList(str);
                foreach (string s in list)
                    keys.Add(StringToKey(s));
            }
            return keys;
        }
        public static List<Coordinate> StringToCoords(string str)
        {
            List<Coordinate> coords = new List<Coordinate>();
            if (str != "None")
            {
                List<string> list = StringToList(str);
                foreach (string s in list)
                {
                    string[] strArray = s.Split('.');
                    coords.Add(new Coordinate(TryParseInt(strArray[0]), TryParseInt(strArray[1])));
                }
            }
            return coords;
        }
        public static List<PixelPattern> StringToPixels(string str)
        {
            List<PixelPattern> pixels = new List<PixelPattern>();
            if (str != "None")
            {
                List<string> list = StringToList(str);
                foreach (string s in list)
                {
                    string[] strArray = s.Split('.');
                    pixels.Add(new PixelPattern(TryParseInt(strArray[0]), TryParseInt(strArray[1]), TryParseInt(strArray[2]), TryParseInt(strArray[3]), TryParseInt(strArray[1])));
                }
            }
            return pixels;
        }
        public static int TryParseInt(string str)
        {
            int myInt;
            if (int.TryParse(str, out myInt))
                return myInt;
            return 0;
        }
        public static string KeyToString(Keys key)
        {
            switch (key)
            {
                case Keys.Oemtilde:
                    return "`";
                case Keys.Escape:
                    return "Esc";
                case Keys.OemMinus:
                    return "-";
                case Keys.Oemplus:
                    return "+";
                case Keys.OemOpenBrackets:
                    return "[";
                case Keys.OemCloseBrackets:
                    return "]";
                case Keys.Oemcomma:
                    return ",";
                case Keys.OemPeriod:
                    return ".";
                case Keys.CapsLock:
                    return "CapsLock";
                case Keys.OemSemicolon:
                    return ";";
                case Keys.OemQuotes:
                    return "'";
                case Keys.OemQuestion:
                    return "/";
                case Keys.ShiftKey:
                    return "Shift";
                case Keys.Menu:
                    return "Alt";
                case Keys.ControlKey:
                    return "Ctrl";
                case Keys.Return:
                    return "Enter";
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Tab:
                case Keys.Back:
                case Keys.Home:
                case Keys.End:
                case Keys.Insert:
                case Keys.Delete:
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Space:
                    return key.ToString();
                default:
                    if ((key >= Keys.A && key <= Keys.Z) || (key >= Keys.F1 && key <= Keys.F19))
                        return key.ToString();
                    else if (key >= Keys.D0 && key <= Keys.D9)
                        return (key - Keys.D0).ToString();
                    else if (key >= Keys.F1 && key <= Keys.F12)
                        return key.ToString();
                    return string.Empty;
            }
        }
        public static Keys StringToKey(string str)
        {
            switch (str)
            {
                case "`":
                    return Keys.Oemtilde;
                case "Esc":
                    return Keys.Escape;
                case "-":
                    return Keys.OemMinus;
                case "+":
                    return Keys.Oemplus;
                case "[":
                    return Keys.OemOpenBrackets;
                case "]":
                    return Keys.OemCloseBrackets;
                case ",":
                    return Keys.Oemcomma;
                case ".":
                    return Keys.OemPeriod;
                case "CapsLock":
                    return Keys.CapsLock;
                case ";":
                    return Keys.OemSemicolon;
                case "\'":
                    return Keys.OemQuotes;
                case "/":
                    return Keys.OemQuestion;
                case "Shift":
                    return Keys.ShiftKey;
                case "Alt":
                    return Keys.Menu;
                case "Ctrl":
                    return Keys.ControlKey;
                case "Enter":
                    return Keys.Return;
                default:
                    if (str.Length == 1 && str[0] >= 'A' && str[0] <= 'Z')
                        return str[0] - 'A' + Keys.A;
                    else if (str.Length == 1 && str[0] >= '0' && str[0] <= '9')
                        return str[0] - '0' + Keys.D0;
                    else if (str.StartsWith("F"))
                    {
                        if (str.Length == 2)
                            return str[1] - '1' + Keys.F1;
                        else if (str.Length == 3)
                            return str[2] - '0' + 9 + Keys.F1;
                    }
                    else
                    {
                        Keys[] rest = new Keys[] { Keys.PageUp, Keys.PageDown, Keys.Tab, Keys.Back, Keys.Home, Keys.End, Keys.Insert, Keys.Delete, Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space };
                        foreach (Keys key in rest)
                        {
                            if (key.ToString() == str)
                                return key;
                        }
                    }
                    return Keys.None;
            }
        }
    }
}