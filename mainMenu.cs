using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clithsby
{
    public class mainMenu
    {
        settingsChange modify = new settingsChange();
        string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\clithsbyData\\";
        
        public void introMenu(){
            colorSwitch();
            System.Console.Write(
            "\n\r>>>>>>>>>>>>>>>>> Clithsby <<<<<<<<<<<<<<<<< \n\r\n\r" +
                "0 | Your Projects \n\r" +
                "       0.0 |   View (and launch) Current Projects List \n\r" +
                "       0.1 |   Add a New Visual Project To List Using Absolute Filepath \n\r" +
                "       0.2 |   List All Projects (vs2015 and genesis) | Add to List or Launch. \n\r\n\r" +

                //"3 | Launch \n\r" +
                //"       3.0 | Add new Software \n\r" +
                //"       3.1 | Text Editor \n\r \n\r" +


                "1 | Setup Clithsby \n\r" +
                "       1.0 | Clithsby's Colors \n\r"
                //"        3.1 | Default Text Editor \n\r"

                );
            mainMenu runSwitch = new mainMenu();
            string optionChoice = Console.ReadLine();
            runSwitch.menuLogic(optionChoice);
        }
        public void menuLogic(string userInput)
        {
            pathManager manager = new pathManager();
            switch (userInput)
            {
                case "0.0":
                    loadAni();
                    manager.launchProj();
                    introMenu();
                    break;
                case "0.1":
                    loadAni();
                    Console.Write("\n\rPlease supply the path to your project, including the .sln file name: ");
                    manager.newProjectToList(Console.ReadLine());
                    introMenu();
                    break;
                case "0.2":
                    loadAni();
                    List<filePack> projectList = manager.listAll();
                    manager.launchProject(projectList);
                    introMenu();
                    break;
                case "1.0":
                    loadAni();
                    Console.Write(" 0) Default (whtONblk)\n\r 1) grayONblue \n\r 2) grayONred \n\r 3) whtONblue. \n\r 4) whtONred. \n\r OR \'custom\' to pick your own colors: ");
                    string pickColor = Console.ReadLine();
                    modify.ColorUpdate(pickColor);
                    introMenu();
                    break;
                case "1.1":
                    loadAni();
                    introMenu();
                    break;
                case "1.2":
                    loadAni();
                    introMenu();
                    break;
                case "3.0":
                    loadAni();
                    introMenu();
                    break;
                case "3.1":
                    loadAni();
                    introMenu();
                    break;
                default:
                    Console.Clear();
                    introMenu();
                    break; 
            }
        }
        public void colorSwitch()
        {
            string colors = userDirectory + "color.csv";
            if (File.Exists(colors))
            {
                settingsChange modify = new settingsChange();
                StreamReader readSettings = new StreamReader(colors);
                string colorChoice = readSettings.ReadLine();
                readSettings.Close();
                if (colorChoice != null)
                {
                    string[] colorChoiceList = colorChoice.Split('|');
                    if (colorChoiceList[0] == "custom")
                    {
                        modify.colorChange(colorChoiceList[0], colorChoiceList[1], colorChoiceList[2]);
                    }
                    else
                    {
                        modify.colorChange(colorChoice, "", "");
                    }
                }
            }
        }
        public void loadAni()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write(".");
                Thread.Sleep(20);
            }
            Console.Clear();
        }
        public void directoryCheck()
        {
            if (!Directory.Exists(userDirectory))
            {
                Directory.CreateDirectory(userDirectory);
            }
        }
    }
}