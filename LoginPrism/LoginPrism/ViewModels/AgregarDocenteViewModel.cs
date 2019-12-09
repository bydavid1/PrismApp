using GalaSoft.MvvmLight.Command;
using LoginPrism.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace LoginPrism.ViewModels
{
    public class AgregarDocenteViewModel : BindableBase
    {
        private string _nombre;
        private IPageDialogService _dialogService;

        public string Nombre
        {
            get { return _nombre; }
            set { SetProperty(ref _nombre, value); }
        }

        public AgregarDocenteViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ICommand Add
        {
            get { return new RelayCommand(Register); }
        }

        private async void Register()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                await
                   _dialogService.DisplayAlertAsync("Fallo", "Faltan datos", "ok");
            }
            else
            {
                HttpClient client = new HttpClient();
                Docente users = new Docente()
                {
                    Nombre = this.Nombre
                };

                string json = JsonConvert.SerializeObject(users);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("url", content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await _dialogService.DisplayAlertAsync("Fallo", "sii", "ok");

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _dialogService.DisplayAlertAsync("Fallo", "Fallo ", "ok");
                }
            }
        }
    }
}
