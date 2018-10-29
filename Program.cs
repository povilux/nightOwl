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

            DataManagement DataManagementInstance = DataManagement.GetInstance();
            DataManagementInstance.LoadData();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FirstPageView());
        }

        static void OnProgramExit(object sender, EventArgs e)
        {
            DataManagement DataManagementInstance = DataManagement.GetInstance();
            DataManagementInstance.SaveData();
        }
    }
}
