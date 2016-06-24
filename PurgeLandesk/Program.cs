using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace PurgeLandesk
{
    class Program
    {
        static void Main(string[] args)
        {

            ProcessToKill();
        }

        static void ProcessToKill()
        {
            int timerellapsed = Convert.ToInt32(ConfigurationManager.AppSettings["timerellapsed"].ToString());

            string scanexes = ConfigurationManager.AppSettings["scan"].ToString();// 123
            string[] procs = scanexes.Split(';');

            while (true)
            {
                foreach(string proc in procs)
                {
                    if (!String.IsNullOrEmpty(proc))
                    {
                        foreach(Process p in System.Diagnostics.Process.GetProcessesByName(proc))
                        {
                            try
                            {
                                p.Kill();  
                                p.WaitForExit();
                            }
                            catch (System.ComponentModel.Win32Exception winException)
                            {
                                // Access denied
                            }
                            catch (Exception ex)
                            {
                                // process was terminating or can't be terminated - deal with it
                            }
                        }
                    }
                }
                System.Threading.Thread.Sleep(timerellapsed * 1000);
            }
        }
    }
}
