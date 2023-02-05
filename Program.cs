namespace Chrome__
{
    internal static class Program
    {
        [STAThread]
        internal static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Window());
        }
    }
}