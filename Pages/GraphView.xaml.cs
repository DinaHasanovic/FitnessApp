using Fitness_Aplikacija.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Fitness_Aplikacija.Pages
{
    public partial class GraphView : Window, INotifyPropertyChanged
    {
        private float ukupnoPotroseneKalorije;
        private float prosecnoPotroseneKalorije; 
        private string userName;
        private int trainingId;
        private ObservableCollection<Training> trainings { set; get; }
        public ActivityMonitoring am = new ActivityMonitoring();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        public float UPK
        {
            get { return ukupnoPotroseneKalorije; }
            set { ukupnoPotroseneKalorije = value; OnPropertyChanged(); }
        }
        public float PPK
        {
            get { return prosecnoPotroseneKalorije; }
            set { prosecnoPotroseneKalorije = value; OnPropertyChanged(); }
        }

        public void Ucitaj_kalorije()
        {
            float ukupanBroj=0;
            foreach (Training t in trainings)
            {
                t.Aktivnosti = am.GetActivities(t.Id);
                foreach ( Activity a in t.Aktivnosti)
                {
                    ukupanBroj += float.Parse(a.Kalorije.ToString());
                }
            }
            UPK = ukupanBroj;
            PPK = ukupanBroj / trainings.Count;

        }



        public GraphView(string userName)
        {
            InitializeComponent();
            UserName = userName;

            Load_Page(userName);
            Ucitaj_kalorije();
        }
        //Ucitavanje treninga i njihovo sortiranje od najstarijeg do najmladjeg
        public void Load_Page(string userName)
        {
            trainings = new ObservableCollection<Training>(am.GetTrainings(userName).OrderBy(t => ConvertToDate(t.Datum)));
            //Crtanje tacaka na canvasu koje predstavljaju treninge
            DrawPoints();
        }
        //Funkcija koja omogucava sortiranje treninga
        private DateTime ConvertToDate(string dateString)
        {
            return DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        //Funkcija za crtanje tacaka
        private void DrawPoints()
        {
            if (trainings == null || trainings.Count == 0) return;

            double canvasWidth = slika.Width;
            double canvasHeight = slika.Height;

            double margin = 20;
            double availableWidth = canvasWidth - 2 * margin;
            double availableHeight = canvasHeight - 2 * margin;

            double xStep = availableWidth / (trainings.Count - 1);
            double yStep = availableHeight / 6.0; 
            //Prethodna tacka zbog linije
            Point? previousPoint = null;

            for (int i = 0; i < trainings.Count; i++)
            {
                double x = margin + i * xStep;
                double y = canvasHeight - margin - (ParseTrainingTimeInHours(trainings[i].Trajanje) * yStep);
                //Praavljenje tacke i pozivanje funkcije za njeno crtanje
                var currentPoint = new Point(x, y);
                DrawPoint(x, y);

                if (previousPoint.HasValue)
                {
                    DrawLine(previousPoint.Value, currentPoint);
                }

                previousPoint = currentPoint;
            }
        }
        //Konvertovanje vremena tako da mogu tacke da se rasporede po canvasu
        private double ParseTrainingTimeInHours(string time)
        {
            var parts = time.Split(':');
            if (parts.Length != 2) return 0;
            if (int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
            {
                return hours + minutes / 60.0;
            }
            return 0;
        }
        //Crtanje tacke
        private void DrawPoint(double x, double y)
        {
            Ellipse point = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red
            };

            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);

            slika.Children.Add(point);
        }
        //Crtanje linija izmedju tacaka
        private void DrawLine(Point startPoint, Point endPoint)
        {
            Line line = new Line
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            slika.Children.Add(line);
        }

        private void Zatvori_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
