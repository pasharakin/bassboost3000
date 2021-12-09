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
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();

        }

        async private void back_Clicked(object sender, EventArgs e) // Удаление
        {
            Application.Current.Properties.Remove("kartinka");

            Application.Current.Properties.Remove("firstname");

            Application.Current.Properties.Remove("lastname");

            Application.Current.Properties.Remove("patronymic");

            Application.Current.Properties.Remove("date");
            Application.Current.Properties.Remove("age");
            Application.Current.Properties.Remove("pol");

            Application.Current.Properties.Remove("kurs");
            Application.Current.Properties.Remove("forma");
            Application.Current.Properties.Remove("starosta");
            Application.Current.Properties.Remove("mesto");

            Application.Current.Properties.Remove("telefon");
            Application.Current.Properties.Remove("telefon_1");
            Application.Current.Properties.Remove("telefon_2");

            Application.Current.Properties.Remove("studinfo");

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

            Application.Current.Properties["kartinka"] = result.FullPath;
        }

        private async void save_Clicked(object sender, EventArgs e) // добовление в словарь
        {


            Application.Current.Properties["firstname"] = FirstName.Text;

            string value2 = LastName.Text;
            Application.Current.Properties["lastname"] = value2;

            string value3 = Patronymic.Text;
            Application.Current.Properties["patronymic"] = value3;

            Application.Current.Properties["date"] = data.Date;
            Application.Current.Properties["age"] = age.Text;

            Application.Current.Properties["pol"] = (string)pick_pol.SelectedItem;
            Application.Current.Properties["kurs"] = (string)pick_nomer_kursa.SelectedItem;
            Application.Current.Properties["forma"] = (string)pick_form.SelectedItem;
            Application.Current.Properties["starosta"] = (bool)star.On;
            Application.Current.Properties["mesto"] = (bool)projiv.On;

            Application.Current.Properties["telefon"] = a.Text;
            Application.Current.Properties["telefon_1"] = a1.Text;
            Application.Current.Properties["telefon_2"] = a2.Text;

            Application.Current.Properties["studinfo"] = aboutstudent.Text;

            await Application.Current.SavePropertiesAsync(); //Сохранение
            await Navigation.PopModalAsync(); // Назад
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            object value; // Предуствновка из словаря

            if (Application.Current.Properties.TryGetValue("kartinka", out value) == true)
            {
                photo.Source = ((string)value);
            }

            if (Application.Current.Properties.TryGetValue("firstname", out value) == true)
            {
                FirstName.Text = ((string)value);
            }
            if (Application.Current.Properties.TryGetValue("lastname", out value) == true)
            {
                LastName.Text = ((string)value);
            }
            if (Application.Current.Properties.TryGetValue("patronymic", out value) == true)
            {
                Patronymic.Text = ((string)value);
            }
            if (Application.Current.Properties.TryGetValue("date", out value) == true)
                data.Date = (DateTime)value;

            if (Application.Current.Properties.TryGetValue("age", out value) == true)
                age.Text = (string)value;

            if (Application.Current.Properties.TryGetValue("pol", out value) == true)
                pick_pol.SelectedItem = (string)value;
            if (Application.Current.Properties.TryGetValue("kurs", out value) == true)
                pick_nomer_kursa.SelectedItem = (string)value;
            if (Application.Current.Properties.TryGetValue("forma", out value) == true)
                pick_form.SelectedItem = (string)value;

            if (Application.Current.Properties.TryGetValue("staro", out value) == true)
                star.On = (bool)value;
            if (Application.Current.Properties.TryGetValue("mesto", out value) == true)
                projiv.On = (bool)value;

            if (Application.Current.Properties.TryGetValue("telefon", out value) == true)
                a.Text = (string)value;
            if (Application.Current.Properties.TryGetValue("telefon_1", out value) == true)
                a1.Text = (string)value;
            if (Application.Current.Properties.TryGetValue("telefon_2", out value) == true)
                a2.Text = (string)value;

            if (Application.Current.Properties.TryGetValue("studinfo", out value) == true)
                aboutstudent.Text = (string)value; ;
        }
    }
}