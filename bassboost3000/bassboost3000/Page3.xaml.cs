using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bassboost3000
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        public Page3()
        {
            InitializeComponent();

        }

        async private void back_Clicked(object sender, EventArgs e) // Удаление
        {
            Preferences.Remove("kartinka");
            Preferences.Remove("firstname");
            Preferences.Remove("lastname");
            Preferences.Remove("patronymic");

            Preferences.Remove("date");
            Preferences.Remove("age");

            Preferences.Remove("pol");
            Preferences.Remove("kurs");
            Preferences.Remove("forma");

            Preferences.Remove("starosta");
            Preferences.Remove("mesto");

            Preferences.Remove("telefon");
            Preferences.Remove("telefon_1");
            Preferences.Remove("telefon_2");

            Preferences.Remove("studinfo");

            a1.Text = "";
            a2.Text = "";
            await Navigation.PopModalAsync(); // Назад

        }


        static EntryCell a1 = new EntryCell()
        {
            Placeholder = "Второй номер телефона", // поле для телефона
        };

        static EntryCell a2 = new EntryCell()
        {
            Placeholder = "Третий номер телефона", // поле для телефона
        };
        //public List<EntryCell> telefon = new List<EntryCell>(2) { a1, a2 };

        int kol = 1; // Количество телефонов
        private void add_Clicked(object sender, EventArgs e) // Добавление телефона
        {
            if (kol == 1)
            {
                kol++;
                phone.Add(a1);
                return;
            }

            if (kol == 2)
            {
                kol++;
                phone.Add(a2);
                return;
            }

            else
            {
                DisplayAlert("Максимум три номера", "Колличество номеров = " + Convert.ToString(kol), "Ок");
                return;
            }

        }
        private void del_Clicked(object sender, EventArgs e) // Удаление телефонов
        {
            if (kol == 2)
            {
                kol--;
                phone.Remove(a1);
                a1.Text = "";

                return;
            }
            if (kol == 3)
            {
                kol--;
                phone.Remove(a2);
                a2.Text = "";

                return;
            }
            else
            {
                DisplayAlert("Минимум один номера", "Колличество номеров = " + Convert.ToString(kol), "Ок");
            }
        }
        int x;
        private void data_DateSelected(object sender, DateChangedEventArgs e) // Расчёт возраста
        {


            if ((DateTime.Now.Month >= data.Date.Month) && (DateTime.Now.Day >= data.Date.Day))
            {
                x = DateTime.Now.Year - data.Date.Year;
            }
            else
            {
                x = DateTime.Now.Year - data.Date.Year - 1;
            }
            if (x <= 12)
            {
                DisplayAlert("Внимание", "Студент должен быть старше 12 лет", "Ок");
            }
            else
            {
                age.Text = "Возраст - " + x.ToString();
            }


        }

        async private void photo_Clicked(object sender, EventArgs e)
        {
            var options = new PickOptions //Выбор фото
            {
                PickerTitle = "Выберите картинку",
                FileTypes = FilePickerFileType.Images,
            };
            var result = await FilePicker.PickAsync(options);
            photo.Source = result.FullPath;
            Preferences.Set("kartinka", result.FullPath);

        }

        private async void save_Clicked(object sender, EventArgs e) // Сохранение настроек
        {
            Preferences.Set("firstname", (string)FirstName.Text);
            Preferences.Set("lastname", (string)LastName.Text);
            Preferences.Set("patronymic", (string)Patronymic.Text);

            Preferences.Set("date", data.Date);
            Preferences.Set("age", (string)age.Text);

            Preferences.Set("pol", (string)pick_pol.SelectedItem);
            Preferences.Set("kurs", (string)pick_nomer_kursa.SelectedItem);
            Preferences.Set("forma", (string)pick_form.SelectedItem);

            Preferences.Set("starosta", star.On);
            Preferences.Set("mesto", projiv.On);

            Preferences.Set("telefon", (string)a.Text);
            Preferences.Set("telefon_1", (string)a1.Text);
            Preferences.Set("telefon_2", (string)a2.Text);

            Preferences.Set("studinfo", (string)aboutstudent.Text);


            await Navigation.PopModalAsync(); // Назад
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            photo.Source = Preferences.Get("kartinka", "");

            FirstName.Text = Preferences.Get("firstname", "");
            LastName.Text = Preferences.Get("lastname", "");
            Patronymic.Text = Preferences.Get("patronymic", "");

            data.Date = Preferences.Get("date", DateTime.Now);
            age.Text = Preferences.Get("age", "Возраст -");

            pick_pol.SelectedItem = Preferences.Get("pol", "");
            pick_nomer_kursa.SelectedItem = Preferences.Get("kurs", "");
            pick_form.SelectedItem = Preferences.Get("forma", "");

            star.On = Preferences.Get("starosta", false);
            projiv.On = Preferences.Get("mesto", false);

            a.Text = Preferences.Get("telefon", "");
            a1.Text = Preferences.Get("telefon_1", "");
            a2.Text = Preferences.Get("telefon_2", "");

            aboutstudent.Text = Preferences.Get("studinfo", "");
        }
    }
}