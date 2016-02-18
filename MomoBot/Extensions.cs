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
            sb.Remove(sb.Length - 1, 1);
            string final = sb.ToString();
            if (final == string.Empty)
                return "None";
            return final;
        }
        public static List<string> StringToList(string str)
        {
            List<string> list = new List<string>();
            string[] strArray = str.Split(',');
            foreach (string s in strArray)
                list.Add(s);
            return list;
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
                    return Keys.Return;
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
                    else if (str.Length == 2 && str[0] == 'D' && str[1] >= '0' && str[1] <= '9')
                        return str[1] - '0' + Keys.D0;
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