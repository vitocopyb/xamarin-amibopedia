using Amiibopedia.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Amiibopedia
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ViewModel = new MainPageViewModel();
            await ViewModel.LoadCharacters();
            BindingContext = ViewModel;

        }

        private async void ViewCell_Appearing(object sender, EventArgs e)
        {
            ViewCell cell = sender as ViewCell;
            View view = cell.View;

            view.TranslationX = -100;
            view.Opacity = 0;

            await Task.WhenAll<bool>(
                view.TranslateTo(0, 0, 250, Easing.SinIn),
                view.FadeTo(1, 500, Easing.BounceIn)
            );
        }
    }
}
