namespace GolfGame
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
            Application.Run(
                //AQUI FA�O new FORM() 
                //Isto ir� fazer que a app ao inicializar entre nesta pagina
                );
        }
    }
}