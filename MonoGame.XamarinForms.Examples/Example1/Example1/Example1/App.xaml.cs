using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Example1
{
    public partial class App : Application
    {
        public static IGameManager GameManager { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public void MonoGameInitialized()
        {
            // Forward to the MainPage
            ((MainPage)MainPage).MonoGameInitialized();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Console.WriteLine("Start");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            ((MainPage)MainPage).Close();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Console.WriteLine("Resume");
        }
    }
}
