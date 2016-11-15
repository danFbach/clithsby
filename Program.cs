using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clithsby
{
    class Program
    {
        static void Main(string[] args)
        {
            mainMenu launch = new mainMenu();
            launch.directoryCheck();
            launch.colorSwitch();         
            launch.introMenu();
        }
    }
}
