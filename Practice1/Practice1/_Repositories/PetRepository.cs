using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Practice1.Model;

namespace Practice1._Repositories
{
    public class PetRepository : BaseRepository, IPetRepository
    {
        // Constructor
        public PetRepository(string connectionString) 
        { 
            this.connectionString = connectionString;
        }
        // Methods
        public void Add(PetModel petModel)
        {
            using (var conn = new SqlConnection(connectionString))
            //SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4M4Q2KE;Initial Catalog=VeterinaryDb;Integrated Security=True");

            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Pet values (@name, @type, @colour)";
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = petModel.Name;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = petModel.Type;
                cmd.Parameters.Add("@colour", SqlDbType.NVarChar).Value = petModel.Colour;
                cmd.ExecuteNonQuery();

            }
        }
        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            //SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4M4Q2KE;Initial Catalog=VeterinaryDb;Integrated Security=True");

            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "delete from Pet where Pet_Id = @id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();

            }
        }
        public void Edit(PetModel petModel)
        {
            using (var conn = new SqlConnection(connectionString))
            //SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4M4Q2KE;Initial Catalog=VeterinaryDb;Integrated Security=True");

            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"update Pet 
                                    SET Pet_Name = @name, Pet_Type = @type, Pet_Colour = @colour 
                                    WHERE Pet_Id = @id";
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = petModel.Name;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = petModel.Type;
                cmd.Parameters.Add("@colour", SqlDbType.NVarChar).Value = petModel.Colour;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = petModel.Id;
                cmd.ExecuteNonQuery();

            }
        }
        public IEnumerable<PetModel> GetAll()
        {
            var petList = new List<PetModel>();
            using (var conn = new SqlConnection(connectionString))
            //SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4M4Q2KE;Initial Catalog=VeterinaryDb;Integrated Security=True");

            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From Pet order by Pet_Id desc";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var petModel = new PetModel();
                        petModel.Id = (int)reader[0];
                        petModel.Name = (string)reader[1];
                        petModel.Type = (string)reader[2];
                        petModel.Colour = (string)reader[3];
                        petList.Add(petModel);
                    }
                }
            }

            return petList;
        }

        public IEnumerable<PetModel> GetByValue(string value)
        {
            var petList = new List<PetModel>();
            int petId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string petName = value;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"Select * From Pet 
                                    where Pet_Id = @id or Pet_Name like @name +'%'
                                    order by Pet_Id desc";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = petId;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = petName;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var petModel = new PetModel();
                        petModel.Id = (int)reader[0];
                        petModel.Name = (string)reader[1];
                        petModel.Type = (string)reader[2];
                        petModel.Colour = (string)reader[3];
                        petList.Add(petModel);
                    }
                }
            }

            return petList;
        }
    }
}
