using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;

namespace bassboost3000
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public Page1()
        {

            InitializeComponent();

        }

        async private void back_Clicked(object sender, EventArgs e)
        {
            a1.Text = "";
            a2.Text = "";
            await Navigation.PopModalAsync();
        }
        static EntryCell a1 = new EntryCell()
        {
            Placeholder = "Второй номер телефона",
        };
        static EntryCell a2 = new EntryCell()
        {
            Placeholder = "Третий номер телефона",
        };
        //public List<EntryCell> telefon = new List<EntryCell>(2) { a1, a2 };

        int kol = 1;
        private void add_Clicked(object sender, EventArgs e)
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
        private void del_Clicked(object sender, EventArgs e)
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

        private void data_DateSelected(object sender, DateChangedEventArgs e)
        {
            int x;

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
                DisplayAlert("Ошибка", "Студент должен быть старше 12 лет", "Ок");
            }
            else
            {
                age.Text = "Возраст - " + x.ToString();
            }
        }
        async private void photo_Clicked(object sender, EventArgs e)
        {
            var options = new PickOptions
            {
                PickerTitle = "Выберите картинку",
                FileTypes = FilePickerFileType.Images,
            };
            var result = await FilePicker.PickAsync(options);
            photo.Source = result.FullPath;
        }
        private async void save_Clicked(object sender, EventArgs e)
        {

            StreamWriter outFile = new StreamWriter(Path.Combine(folderPath, "file.txt"));
            outFile.WriteLine(FirstName.Text);
            outFile.WriteLine(LastName.Text);
            outFile.WriteLine(Patronymic.Text);

            outFile.WriteLine(data.Date);
            outFile.WriteLine(age.Text);

            outFile.WriteLine(pick_pol.SelectedItem);
            outFile.WriteLine(pick_nomer_kursa.SelectedItem);
            outFile.WriteLine(pick_stependia.SelectedItem);

            outFile.WriteLine(star.On);
            outFile.WriteLine(projiv.On);

            outFile.WriteLine(a.Text);
            outFile.WriteLine(a1.Text);
            outFile.WriteLine(a2.Text);

            outFile.WriteLine(aboutstudent.Text);
            outFile.Close();

            await Navigation.PopModalAsync();
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(folderPath, "file.txt")) == true)
            {
                StreamReader outFile = new StreamReader(Path.Combine(folderPath, "file.txt"));
                photo.Source = outFile.ReadLine();

                FirstName.Text = outFile.ReadLine();
                LastName.Text = outFile.ReadLine();
                Patronymic.Text = outFile.ReadLine();
                //data.Date = outFile.ReadLine();
                age.Text = outFile.ReadLine();
                pick_pol.SelectedItem = outFile.ReadLine();
                pick_nomer_kursa.SelectedItem = outFile.ReadLine();
                pick_stependia.SelectedItem = outFile.ReadLine();
                star.Text = outFile.ReadLine();
                projiv.Text = outFile.ReadLine();
                a.Text = outFile.ReadLine();
                a1.Text = outFile.ReadLine();
                a2.Text = outFile.ReadLine();
                aboutstudent.Text = outFile.ReadLine();
                outFile.Close();
            }
        }



    }
}
