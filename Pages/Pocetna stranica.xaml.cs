using Fitness_Aplikacija.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Fitness_Aplikacija.Pages
{
    /// <summary>
    /// Interaction logic for Pocetna_stranica.xaml
    /// </summary>
    public partial class Pocetna_stranica : Page, INotifyPropertyChanged
    {
        public ActivityMonitoring am = new ActivityMonitoring();
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Training> Trainings { get; set; }
        private string _userName;
        private Training trening;
        private string day;//dan treninga
        private string month;//mesec treninga
        private string year;//godina treninga
        private string hours;//sati trajanja treninga
        private string minutes;//minuti trajanja treninga

        public string Day
        {
            get { return day; }
            set { day = value; OnPropertyChanged(); }
        }
        public string Month
        {
            get { return month; }
            set { month = value; OnPropertyChanged(); }
        }
        public string Year
        {
            get { return year; }
            set { year = value; OnPropertyChanged(); }
        }
        public string Hours
        {
            get { return hours; }
            set { hours = value; OnPropertyChanged(); }
        }
        public string Minutes
        {
            get { return minutes; }
            set { minutes = value; OnPropertyChanged(); }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }
        //Ucitavanje treninga uz pomoc userName koje je unikatno za svakog korisnika
        public void Load_Trainings( string userName)
        {
            Trainings = am.GetTrainings(userName);
            lvTrainings.ItemsSource = null;
            lvTrainings.ItemsSource = Trainings;
            foreach(Training t in Trainings)
            {
                t.Aktivnosti = am.GetActivities(t.Id);
            }
        }

        public Training Training
        {
            get { return trening;}
            set { trening = value;OnPropertyChanged(); }
        }

        public Pocetna_stranica(string username)
        {
            InitializeComponent();
            UserName = username;
            WelcomeText.Text = "Dobrodosli, " + UserName;
            Training = new Training();
            Load_Trainings(username);
        }
        private void Dodaj_Trening(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!IsValidDate(Day, Month, Year))
            {
                return;
            }

            if (!IsValidTime(Hours, Minutes))
            {
                return;
            }

            Training t = new Training
            {
                Datum = Day + "/" + Month + "/" + Year,
                Trajanje = Hours + ":" + Minutes,
                UserName = UserName,
            };

            am.CreateTraining(t);
            Load_Trainings(UserName);

            Day = string.Empty;
            Month = string.Empty;
            Year = string.Empty;
            Hours = string.Empty;
            Minutes = string.Empty;
        }
        //Provera datuma
        private bool IsValidDate(string day, string month, string year)
        {
            if (string.IsNullOrWhiteSpace(month) || month.Length > 2)
            {
                MessageBox.Show("Unesite ispravno mesec treninga!");
                return false;
            }

            if (month.Length == 1)
            {
                month = "0" + month;
            }

            if (!int.TryParse(month, out int m) || m < 1 || m > 12)
            {
                MessageBox.Show("Neispravno uneti meseci");
                return false;
            }

            if (string.IsNullOrWhiteSpace(day) || day.Length > 2)
            {
                MessageBox.Show("Unesite ispravno dan treninga!");
                return false;
            }

            if (day.Length == 1)
            {
                day = "0" + day;
            }

            if (!int.TryParse(day, out int d) || d < 1 || d > 31)
            {
                MessageBox.Show("Neispravno uneti dan");
                return false;
            }

            if (string.IsNullOrWhiteSpace(year) || year.Length != 4)
            {
                MessageBox.Show("Unesite ispravnu godinu treninga!");
                return false;
            }

            if (!int.TryParse(year, out int y))
            {
                MessageBox.Show("Neispravno uneta godina");
                return false;
            }

            if (!DateTime.TryParse($"{year}-{month}-{day}", out DateTime date))
            {
                MessageBox.Show("Neispravan datum");
                return false;
            }

            return true;
        }
        //Provera vremena
        private bool IsValidTime(string hours, string minutes)
        {
            if (string.IsNullOrWhiteSpace(hours) || hours.Length > 2)
            {
                MessageBox.Show("Unesite ispravno sate treninga!");
                return false;
            }

            if (!int.TryParse(hours, out int h) || h < 0 || h > 6)
            {
                MessageBox.Show("Neispravno uneti sati");
                return false;
            }

            if (string.IsNullOrWhiteSpace(minutes) || minutes.Length > 2)
            {
                MessageBox.Show("Unesite ispravno minute treninga!");
                return false;
            }

            if (!int.TryParse(minutes, out int min) || min < 0 || min > 59)
            {
                MessageBox.Show("Neispravno uneti minuti");
                return false;
            }

            return true;
        }

        private void Izbrisi_trening(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lvTrainings.SelectedItem != null)
            {
                Training selectedTraining = lvTrainings.SelectedItem as Training;
                if (selectedTraining != null)
                {
                    am.DeleteTraining(selectedTraining);
                    Trainings.Remove(selectedTraining);
                    lvTrainings.ItemsSource = null;
                    lvTrainings.ItemsSource=Trainings;
                    Day = string.Empty;
                    Month = string.Empty;
                    Year = string.Empty;
                    Hours = string.Empty;
                    Minutes = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Odaberite trening koji zelite da uklonite");
            }
        }

        //Dupli klik za otvaranje posebnog prozora za trening
        private void lvTrainings_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lvTrainings.SelectedItem != null)
            {
            Training selectedTraining = lvTrainings.SelectedItem as Training;
                if (selectedTraining != null)
                {
                    MainWindow mw = new MainWindow();
                    var win = (Window)this.Parent;
                    ActivitiesPage ap = new ActivitiesPage(selectedTraining);
                    mw.Content = ap;
                    mw.Title = "Detaljniji pregled";
                    win.Close();
                    mw.Show();
                }
            }
        }

        private void Graph_view(object sender, System.Windows.RoutedEventArgs e)
        {
            GraphView gp = new GraphView(UserName);
            gp.Show();
            gp.Focus();


        }

        private void lvTrainings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvTrainings.SelectedIndex > -1)
            {
                Training.Trajanje = (lvTrainings.SelectedItem as Training).Trajanje;
                Training.Datum = (lvTrainings.SelectedItem as Training).Datum;
                Training.UserName = (lvTrainings.SelectedItem as Training).UserName;
                Training.Id = (lvTrainings.SelectedItem as Training).Id;
                Day = Training.Datum.Substring(0, 2);
                Month = Training.Datum.Substring(3, 2);
                Year = Training.Datum.Substring(6, 4);
                Hours = Training.Trajanje.Substring(0, 2);
                Minutes = Training.Trajanje.Substring(3, 2);
            }
            else
            {
                return;
            }

        }
    }
}
