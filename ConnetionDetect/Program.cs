using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConnetionDetect
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            new Application().Run(new MainWindow());
        }
    }
}
