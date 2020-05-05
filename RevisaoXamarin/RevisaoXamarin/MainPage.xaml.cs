using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RevisaoXamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = viewModel;

            if (Application.Current.Properties.ContainsKey("dtAtual"))
            {
                DateTime dtAtual = Convert.ToDateTime(Application.Current.Properties["dtAtual"]);

                lblBoasVindas.Text = dtAtual.ToString();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "Info", async (msg) =>
            {
                await DisplayAlert("Informação", msg, "OK");
            });

            MessagingCenter.Subscribe<string>(this, "Pergunta", async (msg) =>
            {
                if(await DisplayAlert("Confirmalção", $"{msg} , confirma limpeza dos dados?","Yes","No"))
                {
                    viewModel.CleanFields();
                    await DisplayAlert("Informação", "Limpeza realizada com sucesso", "OK");
                }
            });

            MessagingCenter.Subscribe<string>(this, "Opcoes", async (msg) =>
            {
                string result;

                result = await DisplayActionSheet($"{msg}, selecione uma opção ", "Cancelar", "Limpar Dados", "Contar Caracteres", "Exibir Saudação");

                if(result != null)
                {
                    if(result.Equals("Limpar Dados"))
                    {
                        viewModel.CleanConfirmation();
                    }

                    if (result.Equals("Contar Caracteres"))
                    {
                        viewModel.CountCharacters();
                    }

                    if (result.Equals("Exibir Saudação"))
                    {
                        viewModel.ShowMessage();
                    }
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "Info");
            MessagingCenter.Unsubscribe<string>(this, "Pergunta");
            MessagingCenter.Unsubscribe<string>(this, "Opcoes");
        }
    }
}
