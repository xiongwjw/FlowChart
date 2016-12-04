using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FlowChar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] commands)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (commands.Length > 0)
            {
                string fileName = string.Empty;
                foreach (string commond in commands)
                {
                    fileName += commond;
                    fileName += " ";
                }
                fileName.Trim();
                Application.Run(new FormWorkFlow(fileName));
            }
            else
            {
                Application.Run(new FormWorkFlow());
            }

        }
    }
}