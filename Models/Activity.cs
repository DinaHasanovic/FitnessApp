using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Aplikacija.Models
{
    public class Activity : INotifyPropertyChanged
    {
        private int id;
        private int trainingId;
        private string trajanje;
        private string intezitet;
        private string vrsta;
        private float kalorije;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        public int TrainingId
        {
            get { return trainingId; }
            set { trainingId = value; OnPropertyChanged(); }
        }
        public string Trajanje
        {
            get { return trajanje; }
            set { trajanje = value; OnPropertyChanged(); }
        }
        public string Intezitet
        {
            get { return intezitet; }
            set { intezitet = value; OnPropertyChanged(); }
        }
        public string Vrsta
        {
            get { return vrsta; }
            set { vrsta = value; OnPropertyChanged(); }
        }
        public float Kalorije
        {
            get { return kalorije; }
            set { kalorije = value; OnPropertyChanged(); }
        }
    }
}
