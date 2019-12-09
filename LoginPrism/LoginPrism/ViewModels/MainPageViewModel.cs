using GalaSoft.MvvmLight.Command;
using LoginPrism.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace LoginPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private IPageDialogService _dialogService;

        public string Username { get { return _username; } set { SetProperty(ref _username, value); } }
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Login";
            _dialogService = dialogService;
        }

        public ICommand Autenticate
        {
           get
            {
                return new RelayCommand(Auth);
            }
        }

        private async void Auth()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
             await 
                _dialogService.DisplayAlertAsync("Fallo", "Faltan datos", "ok");
            }
            else
            {
                HttpClient client = new HttpClient();
                User users = new User()
                {
                    username = this.Username,
                    password = this.Password
                };

                string json = JsonConvert.SerializeObject(users);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("url", content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await _dialogService.DisplayAlertAsync("Fallo", "sii", "ok");
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _dialogService.DisplayAlertAsync("Fallo", "Fallo autentificacion", "ok");
                }
            }
        }
    }
}
