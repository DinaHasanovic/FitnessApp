using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Aplikacija.Models
{
    public class Training : INotifyPropertyChanged
    {
        private int id;
        private string datum;
        private string trajanje;
        private ObservableCollection<Activity> aktivnosti ;
        private string userName;

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
        public string Datum
        {
            get { return datum; }
            set { datum = value; OnPropertyChanged(); }
        }
        public string Trajanje
        {
            get { return trajanje; }
            set { trajanje = value; OnPropertyChanged(); }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Activity> Aktivnosti
        {
            get { return aktivnosti; }
            set { aktivnosti = value; OnPropertyChanged(); }
        }
        public Training()
        {
            Aktivnosti = new ObservableCollection<Activity>();
        }

    }
}
