using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Aplikacija.Models
{
    //Glavna klasa koja upravlja aktivnostima i treninzima
    public class ActivityMonitoring
    {
        private Database database;

        public ActivityMonitoring()
        {
            database = new Database();
        }

        public void CreateTraining(Training t)
        {
            database.CreateTraining(t);
        }
        public void DeleteTraining(Training t)
        {
            database.DeleteTraining(t);
        }
        public ObservableCollection<Training> GetTrainings(string userName) {
            return database.GetTrainings(userName);
        }
        public void CreateActivity(Activity activity) {
            database.CreateActivity(activity);
        }
        public void DeleteActivity(Activity activity) { 
            database.DeleteActivity(activity);
        }
        public void UpdateActivity(Activity activity) {
            database.UpdateActivity(activity);
        }
        public ObservableCollection<Activity> GetActivities(int trainingId)
        {
            return database.GetActivities(trainingId);
        }
    }
}
