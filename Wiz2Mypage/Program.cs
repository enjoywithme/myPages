namespace Wiz2Mypage
{
    internal static class Program
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

            //初始化mypage
            MyPageLib.MyPageSettings.InitInstance(mySharedLib.Utility.GetSetting("MyPageSettingPath", ""));


            Application.Run(new MainForm());
        }
    }
}