using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RevisaoXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Application.Current.Properties["dtAtual"] = DateTime.Now;

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
