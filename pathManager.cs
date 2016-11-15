using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.IO;

namespace Clithsby
{
    public class pathManager
    {
        string projectList = "";
        char projSeperator = ',';
        char dataSeperator = '|';
        string oscarDirectory = @"\\oscar\_Web\_Genesis";
        string vs2015Directory = @"\\oscar\_Web\_VS2015";
        string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\clithsbyData\\";
        
        public List<filePack> listAll()
        {
            List<filePack> fileList = new List<filePack>();
            List<string> possibleProjects = new List<string>();
            possibleProjects.AddRange(Directory.GetDirectories(vs2015Directory).ToList());
            possibleProjects.AddRange(Directory.GetDirectories(oscarDirectory).ToList());
            int itemIndex = 0;
            Console.Write("Please give me the first letter of the project you are looking for: ");
            char charHint = Console.ReadLine().ToLower().ToCharArray()[0];
            Console.WriteLine("Projects: ");
            foreach (var item in possibleProjects)
            {
                filePack newProject = findProject(item, charHint);
                if(newProject != null)
                {
                    fileList.Add(newProject);
                    Console.WriteLine("{0}) {1}", itemIndex, newProject.fileName);
                    itemIndex += 1;
                }
            }
            return fileList;
        }
        public filePack findProject(string filePath, char charHint)
        {
            string fileX = "";
            filePack nextProjectFile = new filePack();
            fileX = filePath.Split('\\').Last();
            char CompareChar = fileX.ToLower().ToCharArray()[0];
            List<string> possibleFiles = Directory.GetFiles(filePath).ToList();
            if (CompareChar == charHint)
            {
                if (possibleFiles.Count() > 0)
                {
                    foreach (var file in possibleFiles)
                    {
                        string _thisFile = file.Split('\\').Last();
                        string fileType = _thisFile.Split('.').Last();
                        char[] letterTest = _thisFile.ToCharArray();
                        if (fileType == "sln")
                        {
                            nextProjectFile.fileName = _thisFile.Split().First();
                            nextProjectFile.filePath = file;
                            return nextProjectFile;
                        }
                    }
                }
            }
            string nextDirectory = filePath + "\\" + fileX;
            if (Directory.Exists(nextDirectory))
            {
                return findProject(nextDirectory, charHint);
            }
            else
            {
                return null;
            }
        }
        public void launchProject(List<filePack> files)
        {
            Console.Write("\n\r\'Start\' or \'Add\': ");
            string action = Console.ReadLine().ToLower();
            if(action == "add" || action == "start")
            {
                Console.Write("\n\rPlease enter index of desired project: ");
                int indexNum;
                bool result = int.TryParse(Console.ReadLine(), out indexNum);
                if (result == true && indexNum < files.Count())
                {
                    if (action == "add")
                    {
                        newProjectToList(files[indexNum].filePath);
                    }
                    else if (action == "start")
                    {
                        string filePath = files[indexNum].filePath;
                        string vsVersion = filePath.Split('\\')[4].ToLower();
                        if (vsVersion == "_vs2015")
                        {
                            Console.Write("\n\r" + files[indexNum].fileName + " is now being launched.");
                            Thread.Sleep(1500);
                            Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\devenv.exe", filePath);
                        }
                        else if (vsVersion == "_genesis")
                        {
                            Console.Write("\n\r" + files[indexNum].fileName + " is now being launched.");
                            Thread.Sleep(1500);
                            Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 10.0\\Common7\\IDE\\devenv.exe", filePath);
                        }
                    }

                }
                else
                {
                    Console.Write("Invalid Entry.");
                    Thread.Sleep(1500);
                    Console.Clear();
                }
            }
            else
            {
                Console.Write("Invalid Entry.");
                Thread.Sleep(1500);
                Console.Clear();
            }
        }
        public void newProjectToList(string projPath)
        {
            if(projPath == null)
            {
                return;
            }
            string csvfile = userDirectory + "projectPackage.csv";
            List<filePack> fileList = new List<filePack>();
            bool fileExists = false;
            string fName = projPath.Split('\\').Last().Split('.').First();
            if(!File.Exists(csvfile)) {
                StreamWriter newProjFile = new StreamWriter(csvfile);
                newProjFile.Close();
            }
            using (StreamReader findProjects = new StreamReader(csvfile))
            {
                projectList = findProjects.ReadLine();
                findProjects.Close();
            }
            if (projectList != null)
            {
                string[] projects = projectList.Split(projSeperator);
                foreach (var project in projects)
                {
                    filePack _thisFile = new filePack();
                    string projFileName = project.Split(dataSeperator).Last();
                    _thisFile.filePath = projFileName;
                    _thisFile.fileName = project.Split(dataSeperator).First().Split(')')[1];
                    fileList.Add(_thisFile);
                    if (projFileName == projPath)
                    {
                        fileExists = true;
                    }
                    
                }
                if (fileExists == false)
                {
                    filePack newFile = new filePack();
                    newFile.fileName = fName;
                    newFile.filePath = projPath;
                    fileList.Add(newFile);
                    fileList = fileList.OrderBy(x => x.fileName).ToList();
                    using (StreamWriter clearCSV = new StreamWriter(csvfile))
                    {
                        clearCSV.Write("0)" + fileList[0].fileName + "|" + fileList[0].filePath);
                        clearCSV.Close();
                    }
                    using (StreamWriter addProj = new StreamWriter(csvfile, true))
                    {           
                        for(int i = 1; i < fileList.Count(); i++)
                        {
                            addProj.Write("," + i + ")" + fileList[i].fileName + "|" + fileList[i].filePath);
                        }
                        addProj.Close();
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(20);
                    }
                    Console.WriteLine();
                    Console.WriteLine(fName + " has successfully been added to you project list.\n\r");
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("This Project already exists in your list.");
                }
            }
            else
            {
                using (StreamWriter addProj = new StreamWriter(csvfile))
                {
                    addProj.Write("0)" + fName + "|" + projPath);
                    addProj.Close();
                }
                for(int i = 0; i < 20; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(20);
                }
                Console.Write("\n\r" + fName + " has successfully been added to you project list.\n\r");
                Thread.Sleep(1500);
                Console.Clear();
            }
        }
        public void launchProj(){
            Console.WriteLine();
            string csvfile = userDirectory + "projectPackage.csv";
            using (StreamReader findProjects = new StreamReader(csvfile))
            {
                projectList = findProjects.ReadLine();
                findProjects.Close();
            }
            string[] listProjects = projectList.Split(projSeperator);
            foreach(var item in listProjects)
            {
                string[] _thisFile = item.Split(dataSeperator);
                Console.WriteLine(_thisFile[0]);
            }
            Console.Write("\n\rPlease enter the index number of the project you would like to launch or enter HOME to return to main menu: ");
            string fileChoice = Console.ReadLine();
            if (fileChoice.ToLower() != "home")
            {
                foreach (var item in listProjects)
                {
                    if (item.Split(')')[0] == fileChoice)
                    {
                        string filePath = item.Split(dataSeperator).Last();
                        string vsVersion = filePath.Split('\\')[4].ToLower();
                        if (vsVersion == "_vs2015")
                        {
                            Console.Write("\n\r" + item.Split(dataSeperator)[0].Split(')')[1] + " is now being launched.");
                            Thread.Sleep(1500);
                            Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\devenv.exe", filePath);
                        }
                        else if (vsVersion == "_genesis")
                        {
                            Console.Write("\n\r" + item.Split(dataSeperator)[0].Split(')')[1] + " is now being launched.");
                            Thread.Sleep(1500);
                            Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 10.0\\Common7\\IDE\\devenv.exe", filePath);
                        }                        
                    }
                }
            }
        }
    }
}
