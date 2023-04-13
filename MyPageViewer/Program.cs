using System.Runtime.InteropServices;
using MyPageViewer.Model;

namespace MyPageViewer
{
    internal static partial class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //Associate .piz extension
            FileAssociations.EnsureAssociationsSet();

            
            Application.Run(FormMain.Instance);

            //Save settings
            MyPageSettings.Instance.Save();
        }

        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, Properties.Resources.Text_Hint, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public static void ShowError(string message)
        {
            MessageBox.Show(message, Properties.Resources.Text_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}