using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LaunchScreen
{
    class Launcher
    {
        public static void PlayGame(string file)
        {
            Process.Start(file);
        }
         public static void LaunchWebsite(string url)
        {
            Process.Start(url);
        }
    }
}
