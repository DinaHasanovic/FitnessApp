using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fitness_Aplikacija.Models
{
    public class Database
    {
        private SqlConnection connection;
        private SqlCommand command;


        public Database()
        {
            connection = new SqlConnection("Server=DESKTOP-8CMNCDQ\\SQLEXPRESS01;Database=FitnessApp;Trusted_Connection=True;");
            command = connection.CreateCommand();
        }
        //Upravljanje korisnicima 
        public void registerUser(User user)
        {
            try
            {
                connection.Open();
                command.CommandText = $"INSERT INTO [dbo].[Users] ([userName],[password]) VALUES ('{user.UserName}','{user.Password}')";
                command.ExecuteNonQuery();
                user.UserName = string.Empty;
                user.Password = string.Empty;
            }
            catch (Exception ex)
            {
                //Provera da li su lozinka i username unikatni
                if (ex.Message == $"Violation of UNIQUE KEY constraint 'UQ__Users__6E2DBEDE39786CCE'. Cannot insert duplicate key in object 'dbo.Users'. The duplicate key value is ({user.Password}).\r\nThe statement has been terminated.")
                { MessageBox.Show("Lozinka je zauzeta"); }
                else if (ex.Message == $"Violation of UNIQUE KEY constraint 'UQ__Users__66DCF95C7A12A2D5'. Cannot insert duplicate key in object 'dbo.Users'. The duplicate key value is ({user.UserName}).\r\nThe statement has been terminated.")
                { MessageBox.Show("Korisnicko  ime je zauzeto"); }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        public User getUser(User user)
        {
            User u = new User();
            try
            {
                connection.Open();
                command.CommandText = $"SELECT * FROM [dbo].[Users] WHERE userName = '{user.UserName}'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    u.UserName = reader["userName"].ToString();
                    u.Password = reader["Password"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                u = null;

            }
            finally
            {
                connection.Close();
            }
            return u;
        }
        //Upravljanje treninzima
        public void CreateTraining(Training training)
        {
            try
            {
                connection.Open();
                command.CommandText = $"INSERT INTO [dbo].[Trainings] ([Datum],[Trajanje],[userName]) VALUES ('{training.Datum}','{training.Trajanje}','{training.UserName}')";
                command.ExecuteNonQuery();
                training.Datum = string.Empty;
                training.Trajanje = string.Empty;
                training.UserName = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        public void DeleteTraining(Training training)
        {
            try
            {
                connection.Open();
                command.CommandText = $"DELETE FROM [dbo].[Activities] WHERE trainingId='{training.Id}'";
                command.ExecuteNonQuery();
                command.CommandText = $"DELETE FROM [dbo].[Trainings] WHERE Id='{training.Id}'";
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public ObservableCollection<Training> GetTrainings(string userName)
        {
            ObservableCollection<Training> trainings = new ObservableCollection<Training>();
            try
            {
                connection.Open();
                command.CommandText = $"SELECT * FROM [dbo].[Trainings] WHERE userName='{userName}'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Training training = new Training {
                        Id = int.Parse(reader["Id"].ToString()),
                        Datum = reader["Datum"].ToString(),
                        Trajanje = reader["Trajanje"].ToString(),
                        UserName = userName
                    };
                    trainings.Add(training);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

            }
            return trainings;
        }
        //Upravljanje Aktivnostima
        public void CreateActivity(Activity activity)
        {
            try
            {
                connection.Open();
                command.CommandText = $"INSERT INTO [dbo].[Activities] ([trainingId],[trajanje],[intenzitet],[vrsta]) VALUES ('{activity.TrainingId}','{activity.Trajanje}','{activity.Intezitet}','{activity.Vrsta}')";
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void DeleteActivity(Activity activity)
        {
            try
            {
                connection.Open();
                command.CommandText = $"DELETE FROM [dbo].[Activities] WHERE Id='{activity.Id}'";
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateActivity(Activity activity)
        {
            try
            {
                connection.Open();
                command.CommandText = $"UPDATE [dbo].[Activities] SET trajanje='{activity.Trajanje}',intenzitet='{activity.Intezitet}',vrsta='{activity.Vrsta}',Potrosene_kalorije='{activity.Kalorije}' WHERE Id='{activity.Id}'";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public ObservableCollection<Activity> GetActivities(int trainingId)
        {
            ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
            try
            {
                connection.Open();
                command.CommandText = $"SELECT * FROM [dbo].[Activities] WHERE trainingId='{trainingId}'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Activity activity = new Activity
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Intezitet = reader["intenzitet"].ToString(),
                        Trajanje = reader["trajanje"].ToString(),
                        Vrsta = reader["vrsta"].ToString(),
                        TrainingId = trainingId,
                        Kalorije = float.Parse(reader["Potrosene_kalorije"].ToString())
                    };
                    activities.Add(activity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

            }
            return activities;
        }
    }
}
