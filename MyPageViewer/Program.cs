using System.Runtime.InteropServices;

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

            FileAssociations.EnsureAssociationsSet();

            var myPageDoc = MyPageDocument.NewFromArgs(Environment.GetCommandLineArgs());

            if (myPageDoc != null)
            {
                Application.Run(new FormPageViewer(myPageDoc));
                return;
            }

            Application.Run(new FormMain());
        }


    }
}