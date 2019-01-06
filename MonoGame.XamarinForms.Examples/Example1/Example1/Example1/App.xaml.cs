using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Example1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
