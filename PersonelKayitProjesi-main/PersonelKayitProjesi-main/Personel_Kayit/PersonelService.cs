using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personel_Kayit
{
    public class PersonelService
    {
        public string ConnectionString { get; set; } /*= @"Data Source = (localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|PersonelDb.mdf;Integrated Security = True";*/

        public PersonelService()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.ToLower().Replace("\\bin", "").Replace("\\debug", "").Replace("\\release", "").TrimEnd('\\');

            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=" + path + "\\PersonelDb.mdf;Integrated Security=True";
             this.ConnectionString=conStr;
        
        }

        public List<City> GetAllCity()
        {
            var sql = "SELECT * FROM Cities";
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var affectedRows = connection.Query<City>(sql).ToList();
                Console.WriteLine($"Affected Rows: {affectedRows}");
                connection.Close();

                return affectedRows;
            }
        }

        public List<Personel> GetAllPersonels()
        {
            var sql = "SELECT * FROM Personels";
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var affectedRows = connection.Query<Personel>(sql).ToList();
                Console.WriteLine($"Affected Rows: {affectedRows}");
                connection.Close();

                return affectedRows;

            }
        }

        public Personel GetPersonel(int id)
        {

            var sql = $"SELECT * FROM Personels Where Id={id}";
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var affectedRows = connection.Query<Personel>(sql).FirstOrDefault();
                Console.WriteLine($"Affected Rows: {affectedRows}");
                connection.Close();

                return affectedRows;
            }
        }

        public void DeletePersonel(int id)
        {

            var sql = $"DELETE FROM Personels Where Id={id}";
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var affectedRows = connection.Query(sql);
                Console.WriteLine($"Affected Rows: {affectedRows}");
                connection.Close();

            }
        }

        public void AddPersonel(Personel personel)
        {

            var sql = @"INSERT INTO [dbo].[Personels]([Name], [LastName], [CityId], [Salary], [Job], [MartialStatus]) 
                " + "VALUES (@Name, @LastName, @CityId, @Salary, @Job, @MartialStatus);";
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Execute(sql, new
                {
                    personel.Name,
                    personel.LastName,
                    personel.CityId,
                    personel.Salary,
                    personel.Job,
                    personel.MartialStatus
                });
                connection.Close();


            }

            //var connection = new SqlConnection(this.ConnectionString);
            //SqlCommand cmd = new SqlCommand(sql, connection);

            //cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = personel.Name;
            //cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = personel.LastName;
            //cmd.Parameters.Add("@CityId", SqlDbType.Int, 50).Value = personel.CityId;
            //cmd.Parameters.Add("@Salary", SqlDbType.Int, 50).Value = personel.Salary;
            //cmd.Parameters.Add("@Job", SqlDbType.VarChar, 50).Value = personel.Job;
            //cmd.Parameters.Add("@MartialStatus", SqlDbType.Bit, 50).Value = personel.MartialStatus;

            //connection.Open();
            //cmd.ExecuteNonQuery();
            //connection.Close();
        }


        public void UpdatePersonel(Personel personel)
        {

            var sql = @"UPDATE  [dbo].[Personels] 
            " + " SET [Name] = @Name, [LastName]= @LastName , [CityId]=@CityId, [Salary]=@Salary, [Job]=@Job, [MartialStatus]=@MartialStatus WHERE Id = @Id";


            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var result = connection.Execute(sql, new
                {
                    personel.Id,
                    personel.Name,
                    personel.LastName,
                    personel.CityId,
                    personel.Salary,
                    personel.Job,
                    personel.MartialStatus
                });
                connection.Close();

            }
        }
    }
}
