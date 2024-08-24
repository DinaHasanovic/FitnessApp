using Fitness_Aplikacija.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_Aplikacija.Pages
{
    /// <summary>
    /// Interaction logic for ActivitiesPage.xaml
    /// </summary>
    public partial class ActivitiesPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Activity> Activities { get; set; } = new ObservableCollection<Activity>();
        private Training training;
        private ActivityMonitoring am = new ActivityMonitoring();
        public ObservableCollection<string> Intensities { get; set; } = new ObservableCollection<string> { "Slab", "Srednji", "Jak" };//Ponudjene jacine
        private Activity activity;
        private string hours; //trajanje aktivnosti u satima
        private string minutes;//trajanje aktivnosti u minutima
        private string selectedIntensity;//jacina aktivnosti
        private string vrsta;//vrsta aktivnosti
        private string kalorije;//potrosene kalorije aktivnosti
        public string SelectedIntensity
        {
            get { return selectedIntensity; }
            set
            {
                selectedIntensity = value;
                OnPropertyChanged();
            }
        }

        public Training Training
        {
            get { return training; }
            set { training = value; OnPropertyChanged(); }
        }

        public Activity Activity
        {
            get { return activity; }
            set { activity = value; OnPropertyChanged(); }
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
        public string Vrsta
        {
            get { return vrsta; }
            set { vrsta = value; OnPropertyChanged(); }
        }
        public string Kalorije
        {
            get { return kalorije; }
            set { kalorije = value; OnPropertyChanged(); }
        }
 

        public ActivitiesPage(Training training)
        {
            InitializeComponent();
            DataContext = this;
            trainingTextBlock.Text = "Trening je planiran za " + training.Datum + " u trajanju od " + training.Trajanje + " sata.";
            Training = training;
            Kalorije = string.Empty;//Kalorije se unose kasnije izmenom, a ne kreiranjem treninga
            cbInteziteti.ItemsSource = Intensities;
            //Ucitavanje aktivosti i postavljanje u listView
            Activities = training.Aktivnosti;
            lvActivities.ItemsSource = null;
            lvActivities.ItemsSource = Activities;
        }

        private void Dodaj_aktivnost(object sender, System.Windows.RoutedEventArgs e)
        {
            //Provere da li su vreme,intenzitet i vrsta unete ispravno
            if (!IsValidTime(Hours.Trim(), Minutes.Trim()))
            {
                return;
            }
            if (string.IsNullOrEmpty(SelectedIntensity.Trim()))
            {
                MessageBox.Show("Unesite intenzitet aktivnosti!");
                return;
            }
            if (string.IsNullOrEmpty(Vrsta))
            {
                MessageBox.Show("Unesite vrstu aktivnost!");
                return;
            }
            //Dodavanje 0 ako su sati ili minuti jednocifreni
            if (Hours.Trim().Length == 1)
            {
                Hours = "0" + Hours.Trim();
            }
            if (Minutes.Trim().Length == 1)
            {
                Minutes = "0" + Minutes.Trim();
            }
            //Provera da li ukupna vremena aktivnosti prelaze vreme treninga
            TimeSpan novaAktivnost = new TimeSpan(int.Parse(Hours), int.Parse(Minutes), 0);

            if (IsTotalDurationExceeded(novaAktivnost))
            {
                MessageBox.Show("Ukupno trajanje aktivnosti premašuje trajanje treninga!");
                Hours = string.Empty;
                Minutes = string.Empty;
                return;
            }

            Activity novaAkt = new Activity
            {
                Trajanje = Hours + ":" + Minutes,
                Intezitet = SelectedIntensity,
                TrainingId = Training.Id,
                Vrsta = Vrsta,
                Kalorije = 0
            };

            am.CreateActivity(novaAkt);
            Activities.Add(novaAkt);
            lvActivities.ItemsSource = null;
            lvActivities.ItemsSource = Activities;

            Hours = string.Empty;
            Minutes = string.Empty;
            Vrsta = string.Empty;
            Kalorije = string.Empty;
            SelectedIntensity = Intensities[0];
        }
        //Funkcija za proveru duzine aktivnosti
        private bool IsTotalDurationExceeded(TimeSpan novaAKtivnost)
        {
            TimeSpan totalDuration = TimeSpan.Zero;

            foreach (var activity in Activities)
            {
                totalDuration += TimeSpan.Parse(activity.Trajanje);
            }

            TimeSpan trainingDuration = TimeSpan.Parse(Training.Trajanje);

            return totalDuration + novaAKtivnost > trainingDuration;
        }
        //Funkcija za proveru ispravnosti vremena
        private bool IsValidTime(string hours, string minutes)
        {
            if (string.IsNullOrWhiteSpace(hours) || hours.Length > 2)
            {
                MessageBox.Show("Unesite ispravno sate treninga!");
                return false;
            }

            if (!int.TryParse(hours, out int h) || h < 0 || h > 23)
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
        //Funkcija za promenu aktivnosti
        private void Izmeni_aktivnost(object sender, RoutedEventArgs e)
        {
            if (Activity != null)
            {
                if (!IsValidTime(Hours.Trim(), Minutes.Trim()))
                {
                    return;
                }
                if (string.IsNullOrEmpty(Vrsta))
                {
                    MessageBox.Show("Unesite vrstu aktivnosti");
                    return;
                }
                if (Hours.Trim().Length == 1)
                {
                    Hours = "0" + Hours.Trim();
                }
                if (Minutes.Trim().Length == 1)
                {
                    Minutes = "0" + Minutes.Trim();
                }

                if (!float.TryParse(Kalorije, out float a) || a < 0)
                {
                    MessageBox.Show($"Neispravno unete potrošene kalorije!");
                    return;
                }

                TimeSpan updatedDuration = new TimeSpan(int.Parse(Hours), int.Parse(Minutes), 0);

                TimeSpan totalDuration = TimeSpan.Zero;

                foreach (var act in Activities)
                {
                    if (act != Activity)
                    {
                        totalDuration += TimeSpan.Parse(act.Trajanje);
                    }
                }

                TimeSpan trainingDuration = TimeSpan.Parse(Training.Trajanje);

                if (totalDuration + updatedDuration > trainingDuration)
                {
                    MessageBox.Show("Ukupno trajanje aktivnosti premašuje trajanje treninga!");
                    return;
                }

                int index = Activities.IndexOf(Activity);

                if (index != -1)
                {
                    Activities[index].Trajanje = Hours + ":" + Minutes;
                    Activities[index].Intezitet = SelectedIntensity;
                    Activities[index].Vrsta = Vrsta;
                    Activities[index].Kalorije = float.Parse(Kalorije);

                    am.UpdateActivity(Activities[index]);

                    lvActivities.ItemsSource = null;
                    lvActivities.ItemsSource = Activities;

                    Vrsta = string.Empty;
                    Hours = string.Empty;
                    Kalorije = string.Empty;
                    Minutes = string.Empty;
                    SelectedIntensity = Intensities[0];
                }
            }
            else
            {
                MessageBox.Show("Odaberite aktivnost koju želite da izmenite");
            }
        }

        private void Izbrisi_aktivnost(object sender, RoutedEventArgs e)
        {
            if (Activity != null)
            {
                Activities.Remove(Activity);
                am.DeleteActivity(Activity);
                Activity= null;
                lvActivities.SelectedIndex = -1;
                lvActivities.SelectedItem = null;
                lvActivities.ItemsSource = null;
                lvActivities.ItemsSource = Activities;
            }
            else
            {
                MessageBox.Show("Odaberite aktivnost koju zelite da uklonite");
                return;
            }

        }
        //Promena selektovanog itema u listViewu
        private void lvActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvActivities.SelectedIndex > -1)
            {
                if(lvActivities.SelectedItem != null)
                {
                    Activity = lvActivities.SelectedItem as Activity;
                    Hours = Activity.Trajanje.Substring(0, 2);
                    Minutes = Activity.Trajanje.Substring(3, 2);
                    SelectedIntensity = Activity.Intezitet;
                    Vrsta = Activity.Vrsta;
                    Kalorije = Activity.Kalorije.ToString();
                }
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Pocetna_stranica ps = new Pocetna_stranica(Training.UserName);
            window.Content= ps;
            window.Title = "Pocetna strainica";
            var win = (Window)this.Parent;
            win.Close();
            window.Show();
        }
    }
}
