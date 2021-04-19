using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlazorDesktop
{
    static class Program
    {

        private static Mutex _mutex;
        private static Form _frmMain;

        [STAThread]
        static void Main()
        {

            CheckPreviousInstance();
            DependencySolver.Start();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _frmMain = DependencySolver.GetInstance<FrmMain>();

            Application.Run(_frmMain);

        }

        private static void CheckPreviousInstance()
        {
            
            try
            {
                _mutex = Mutex.OpenExisting("SINGLEINSTANCE");
                if (_mutex != null)
                {
                    Application.Exit();
                }
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                _mutex = new Mutex(true, "SINGLEINSTANCE");
            }

        }


    }
}
