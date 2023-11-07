using System.Reflection;
using MyPageLib;
using mySharedLib;

namespace MyPageViewer
{
    internal static partial class Program
    {
        //public const string InstanceGuid = "AC9B6BF8-A4A6-4FAD-AC57-856CB01280C}";


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

            if (!SingleInstance.InitInstance(out var message))
            {
                ShowError(message);
                return;
            }

            //Single instance
            var myPageDoc = MyPageDocument.NewFromArgs(Environment.GetCommandLineArgs());
            if (!SingleInstance.Instance.Start())
            {
                SingleInstance.Instance.ShowFirstInstance(myPageDoc?.FilePath);
                return;
            }

            //初始化setting
#if DEBUG
            MyPageSettings.InitInstance("D:\\programs\\_mytool\\myPages\\");

#else
            MyPageSettings.InitInstance(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            //MessageBox.Show(MyPageSettings.Instance.SettingFilePath);
#endif

            //run main form
            FormMain.Instance = new FormMain(myPageDoc);
            Application.Run(FormMain.Instance);

            //Task.Delay(1000).Wait();

            //Save settings
            MyPageSettings.Instance.Save(out _);

            SingleInstance.Instance.Stop();
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