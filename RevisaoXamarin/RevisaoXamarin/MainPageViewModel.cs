using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RevisaoXamarin
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            ShowCommand = new Command(ShowMessage);
            CountCommand = new Command(CountCharacters);
            CleanCommand = new Command(CleanConfirmation);
            OptionCommand = new Command(ShowOptions);
        }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
        
        string name = string.Empty;
        string dataAtual = string.Empty;
        public string DisplayName => $"Nome digitado : {Name}";
        public string DisplayMessage => dataAtual;

        public ICommand ShowCommand { get; }
        public ICommand CountCommand { get; }
        public ICommand CleanCommand { get; }
        public ICommand OptionCommand { get; }

        public string Name
        {
            get => name;
            set
            {
                if(name == null)
                {
                    return;
                }

                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string DataAtual
        {
            get => dataAtual;
            set
            {
                if(dataAtual == null)
                {
                    return;
                }

                dataAtual = value;

                OnPropertyChanged(nameof(DataAtual));
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public void ShowMessage()
        {
            dataAtual = $"Boa noite {Name}. Hoje é {DateTime.Now.ToString("dd/MM/yyyy")}.";

            OnPropertyChanged(nameof(DataAtual));
            OnPropertyChanged(nameof(DisplayMessage));
        }

        public void CountCharacters()
        {
            string nameLenght = string.Format("Seu nome tem {0} letras", name.Length);
            MessagingCenter.Send<string>(nameLenght, "Info");
        }

        public void CleanConfirmation()
        {
            MessagingCenter.Send<string>(Name, "Pergunta");
        }

        public void CleanFields()
        {
            Name = string.Empty;
            DataAtual = string.Empty;
            OnPropertyChanged(Name);
            OnPropertyChanged(DataAtual);
        }

        public void ShowOptions()
        {
            MessagingCenter.Send<string>(Name, "Opcoes");
        }
    }
}
