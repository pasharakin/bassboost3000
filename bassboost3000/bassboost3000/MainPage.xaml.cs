using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace bassboost3000
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void info_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Информация", "Приложение для создание карточек студентов, Ракин Павел ИСП-41 ", "Ок");
        }

        private void ex_Clicked(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private async void create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Page1());
        }
        private async void load_Preferences_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Page2());
        }
        private async void load_Properties_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Page3());
        }


    }
}
