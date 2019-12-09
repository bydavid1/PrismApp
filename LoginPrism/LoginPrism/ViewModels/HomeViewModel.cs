using LoginPrism.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace LoginPrism.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        ObservableCollection<Materia> _Items1;
        public ObservableCollection<Materia> Items1
        {
            get { return _Items1; }
            set { SetProperty(ref _Items1, value); }
        }

        ObservableCollection<Docente> _Items2;
        public ObservableCollection<Docente> Items2
        {
            get { return _Items2; }
            set { SetProperty(ref _Items2, value); }
        }

        public HomeViewModel()
        {
            GetLists();
        }

        private async void GetLists()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync("url");

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync("url");
                    var lista1 = JsonConvert.DeserializeObject<List<Materia>>(response);
                    Items1 = new ObservableCollection<Materia>(lista1);

                }
                else
                {
                    
                }

                HttpClient client2 = new HttpClient();
                HttpResponseMessage connect2 = await client.GetAsync("url");

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync("url");
                    var lista2 = JsonConvert.DeserializeObject<List<Docente>>(response);
                    Items2 = new ObservableCollection<Docente>(lista2);

                }
                else
                {

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
