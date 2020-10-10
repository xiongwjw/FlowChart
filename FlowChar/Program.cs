using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

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

            //获取当前登录的Windows用户的标识 
            System.Security.Principal.WindowsIdentity wid = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(wid);
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                Regedit();
            }

            RunApplication(commands);

            //判断当前用户是否是管理员 
            //if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            //{
            //    RunApplication(commands);
            //}
            //else // 用管理员用户运行 
            //{
            //    System.Diagnostics.ProcessStartInfo startInfo = new ProcessStartInfo();
            //    startInfo.FileName = Application.ExecutablePath;
            //    startInfo.Arguments = string.Join(" ", commands);
            //    startInfo.Verb = "runas";
            //    System.Diagnostics.Process.Start(startInfo);
            //    System.Windows.Forms.Application.Exit();
            //} 
        }

        private static void Regedit()
        {
            if (CheckRegedit())
                return;

            try
            {
                
                string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.ico");
                string command = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FlowChar.exe");
                string commandLine = command + " \"%1\"";

                RegistryKey key = Registry.ClassesRoot;
                RegistryKey flow = key.CreateSubKey(".flow");
                flow.SetValue(string.Empty, "wyw_chart_file");
                RegistryKey flowchart = key.CreateSubKey("wyw_chart_file");
                RegistryKey flowchartDefaultIcon = flowchart.CreateSubKey("DefaultIcon");
                flowchartDefaultIcon.SetValue(string.Empty, iconPath);
                RegistryKey regCommand = flowchart.CreateSubKey("Shell\\Open\\Command");
                regCommand.SetValue(string.Empty, commandLine);
                regCommand.Close();
                flowchartDefaultIcon.Close();
                flowchart.Close();
                flow.Close();
                key.Close();
            }
            catch (Exception ex)
            {
                Loger.WriteFile(ex.Message);
            }

        }

        private static bool CheckRegedit()
        {
            RegistryKey key = Registry.ClassesRoot;
            try
            {
                RegistryKey flowKey = key.OpenSubKey(".flow");
                if (flowKey == null)
                {
                    key.Close();
                    return false;
                }
                else
                {
                    flowKey.Close();
                    key.Close();
                    return true;
                }

            }
            catch
            {
                key.Close();
                return false;
            }

        }


        private static void RunApplication(string[] commands)
        {
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