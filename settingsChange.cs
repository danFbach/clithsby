using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clithsby
{
    public class settingsChange
    {
        string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\clithsbyData\\";
        
        public void colorChange(string colorSelection, string fg, string bg)
        {
            string colors = userDirectory + "color.csv";
            string[] colorOptions = { "black", "blue", "cyan", "dark blue", "dark cyan", "dark gray", "dark green", "dark magenta", "dark red", "dark yellow", "gray", "green", "magenta", "red", "white", "yellow" };
            bool fgOK = false;
            bool bgOK = false;
            switch (colorSelection.ToLower())
            {
                case "custom":
                    if(fg == "")
                    {
                        Console.WriteLine("\n\rColor Options: Black, Blue, Cyan, Dark Blue, Dark Cyan, Dark Gray, Dark Green, Dark Magenta, Dark Red, Dark Yellow, Gray, Green, Magenta, Red, White or Yellow");
                        Console.Write("\n\rChoose Background Color: ");
                        bg = Console.ReadLine().ToLower();
                        Console.Write("\n\rChoose Foreground Color: ");
                        fg = Console.ReadLine().ToLower();
                    }
                    if(bg == fg)
                    {
                        Console.WriteLine("Background and Foreground must be different colors you foolish bastard! Try Again!");
                        colorChange(colorSelection, "", "");
                    }else
                    {
                        foreach(string color in colorOptions)
                        {
                            if(fg == color)
                            {
                                fgOK = true;
                            }
                            if(bg == color)
                            {
                                bgOK = true;
                            }
                        }
                        if(bgOK != true || fgOK != true)
                        {
                            Console.WriteLine("You have entered an Invalid color selection, try again.");
                            colorChange(colorSelection, "", "");
                        }
                        else
                        {
                            StreamWriter customSave = new StreamWriter(colors);
                            customSave.WriteLine("custom|" + fg + "|" + bg);
                            customSave.Close();
                            customColorBG(fg, bg);
                        }
                    }
                    break;
                case "0":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    break;
                case "1":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Clear();
                    break;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Clear();
                    break;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    break;
                case "4":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    break;
                case "haxr":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    break;
                default:
                    break;
            }
        }
        public void ColorUpdate(string colorSelection)
        {
            string colors = userDirectory + "color.csv";
            StreamWriter updateSettings = new StreamWriter(colors);
            updateSettings.WriteLine(colorSelection);
            updateSettings.Close();
            colorChange(colorSelection, "",  "");
        }
        public void customColorBG(string fg, string bg)
        {
            switch (bg)
            {
                case "black":
                    Console.BackgroundColor = ConsoleColor.Black;
                    customColorFG(fg);
                    break;
                case "blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    customColorFG(fg);
                    break;
                case "cyan":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    customColorFG(fg);
                    break;
                case "dark blue":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    customColorFG(fg);
                    break;
                case "dark cyan":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    customColorFG(fg);
                    break;
                case "dark gray":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    customColorFG(fg);
                    break;
                case "dark green":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    customColorFG(fg);
                    break;
                case "dark magenta":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    customColorFG(fg);
                    break;
                case "dark red":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    customColorFG(fg);
                    break;
                case "dark yellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    customColorFG(fg);
                    break;
                case "gray":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    customColorFG(fg);
                    break;
                case "green":
                    Console.BackgroundColor = ConsoleColor.Green;
                    customColorFG(fg);
                    break;
                case "magenta":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    customColorFG(fg);
                    break;
                case "red":
                    Console.BackgroundColor = ConsoleColor.Red;
                    customColorFG(fg);
                    break;
                case "white":
                    Console.BackgroundColor = ConsoleColor.White;
                    customColorFG(fg);
                    break;
                case "yellow":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    customColorFG(fg);
                    break;
                default:
                    Console.WriteLine(bg + "is not a valid color selection.");
                    break;
            }
        }
        public void customColorFG(string fg)
        {
            switch (fg)
            {
                case "black":
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Clear();
                    break;
                case "dark blue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();
                    break;
                case "dark cyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Clear();
                    break;
                case "dark gray":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Clear();
                    break;
                case "dark green":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Clear();
                    break;
                case "dark magenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Clear();
                    break;
                case "dark red":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Clear();
                    break;
                case "dark yellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Clear();
                    break;
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Clear();
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Clear();
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    break;
                case "white":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine(fg + "is not a valid color selection.");
                    break;
            }
        }
        public void textEditor()
        {
            Console.WriteLine("Switch your default text editor to open docs w/.");
        }
    }
}
