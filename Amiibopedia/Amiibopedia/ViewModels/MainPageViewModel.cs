using Amiibopedia.Helpers;
using Amiibopedia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Amiibopedia.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Amiibo> _amiibos;

        public ObservableCollection<Character> Characters { get; set; }
        public ObservableCollection<Amiibo> Amiibos
        {
            get => _amiibos;
            set
            {
                _amiibos = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; set; }

        public MainPageViewModel()
        {
            SearchCommand = new Command(async (text) =>
            {
                await GetAmiibos(text);
            });
        }

        public async Task LoadCharacters()
        {
            IsBusy = true;
            string url = "https://www.amiiboapi.com/api/character/";
            HttpHelper<Characters> service = new HttpHelper<Characters>();
            Characters characters = await service.GetRestServiceDataAsync(url);
            Characters = new ObservableCollection<Character>(characters.amiibo);

            IsBusy = false;
        }

        public async Task GetAmiibos(object param)
        {
            if (param is Character character)
            {
                IsBusy = true;
                string url = $"https://www.amiiboapi.com/api/amiibo/?character={character.name}";
                HttpHelper<Amiibos> service = new HttpHelper<Amiibos>();
                Amiibos amiibos = await service.GetRestServiceDataAsync(url);
                Amiibos = new ObservableCollection<Amiibo>(amiibos.amiibo);

                IsBusy = false;
            }

        }

    }
}
