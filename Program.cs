using GolfGame.Classes;

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
            //Inicializar a Application
            ApplicationConfiguration.Initialize();

            //Inicializar o GameManager
            GameManager gameManager = GameManager.GetManager();

            //Vou inicializar os valores das op�oes ja guardadas
            GameManager.Instance.LoadOptions();



            //A aplica��o come�a no menu principal
            Application.Run(new MainMenu());
        }
    }
}