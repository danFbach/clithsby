using System;
using System.Collections.Generic;
using System.Linq;
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
        string baseDirectory = "C:\\Users\\Dan DCC\\Documents\\Visual Studio 2015\\Projects\\";
        string csvfile = "C:\\Users\\Dan DCC\\ProjectPackage.csv";
        public List<filePack> listAll()
        {
            List<filePack> fileList = new List<filePack>();
            List<string> possibleProjects = Directory.GetDirectories(baseDirectory).ToList();
            int itemIndex = 0;
            Console.WriteLine("Projects: ");
            foreach (var item in possibleProjects)
            {
                filePack newProject = findProject(item);
                if(newProject != null)
                {
                    fileList.Add(newProject);
                    Console.WriteLine("{0}) {1}", itemIndex, newProject.fileName);
                    itemIndex += 1;
                }
            }
            return fileList;
        }
        public filePack findProject(string filePath)
        {
            string fileX = "";
            filePack nextProjectFile = new filePack();
            fileX = filePath.Split('\\').Last();
            List<string> possibleFiles = Directory.GetFiles(filePath).ToList();
            if(possibleFiles.Count() > 0)
            {
                foreach (var file in possibleFiles)
                {
                    string _thisFile = file.Split('\\').Last();
                    string fileType = _thisFile.Split('.').Last();
                    if (fileType == "sln")
                    {
                        nextProjectFile.fileName = _thisFile.Split().First();
                        nextProjectFile.filePath = file;
                        return nextProjectFile;
                    }
                }
            }
            string nextDirectory = filePath + "\\" + fileX;
            if (Directory.Exists(nextDirectory))
            {
                return findProject(nextDirectory);
            }
            else
            {
                return null;
            }
        }
        public void launchProject(List<filePack> files)
        {
            Console.Write("\n\r\'Start\' or \'Add\':");
            string action = Console.ReadLine().ToLower();
            if(action == "add" || action == "start")
            {
                Console.WriteLine("\n\rPlease enter index of desired project.");
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
                        Console.Write("\n\r" + files[indexNum].fileName + " is now being launched.");
                        Thread.Sleep(1500);
                        System.Diagnostics.Process.Start(files[indexNum].filePath);
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
                        Console.Write("\n\r" + item.Split(dataSeperator)[0].Split(')')[1] + " is now being launched.");
                        Thread.Sleep(1500);
                        System.Diagnostics.Process.Start(item.Split(dataSeperator).Last());
                    }
                }
            }
        }
    }
}
