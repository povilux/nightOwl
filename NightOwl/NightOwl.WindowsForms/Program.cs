using NightOwl.WindowsForms.Components;
using NightOwl.WindowsForms.Data;
using NightOwl.WindowsForms.Services;
using NightOwl.WindowsForms.Views;
using System;
using System.Windows.Forms;

namespace NightOwl.WindowsForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProgramExit);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataManagement DataManagementInstance = DataManagement.Instance;
            DataManagementInstance.LoadData();

            Application.Run(new LoginFormView());
        }

      
        static void OnProgramExit(object sender, EventArgs e)
        {
            DataManagement DataManagementInstance = DataManagement.Instance;
            DataManagementInstance.SaveData();
        }
    }
}
