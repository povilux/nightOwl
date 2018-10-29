using nightOwl.Data;
using nightOwl.Views;
using System;
using System.Windows.Forms;

namespace nightOwl
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

            DataManagement DataManagementInstance = DataManagement.GetInstance();
            DataManagementInstance.LoadData();

            Application.Run(new LoginFormView());
        }

        static void OnProgramExit(object sender, EventArgs e)
        {
            DataManagement DataManagementInstance = DataManagement.GetInstance();
            DataManagementInstance.SaveData();
        }
    }
}
