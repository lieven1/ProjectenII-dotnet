using System.Data.SqlClient;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class GebruikerRepositoryExtern : IGebruikerRepositoryExtern
    {
        private string connectionString = @"Data Source=LAPTOP-3NH1NFIB; Initial Catalog=TaijitanYoshinRyu; User ID=TaijitanAdmin; Password=a";
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private SqlDataReader reader;
        SqlCommand command;

        private string getByEmailQuery = "SELECT * FROM Leden WHERE email={0}";

        public GebruikerRepositoryExtern()
        {
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();
        }

        public Gebruiker GetByEmail(string email)
        {
            Gebruiker gebruiker = null;

            string query = string.Format(getByEmailQuery, email);

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (command = new SqlCommand(query, connection))
                {
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Adres adres = new Adres(reader.GetString(8), reader.GetString(7), reader.GetString(6), reader.GetString(4), reader.GetString(5));
                            gebruiker = new Gebruiker(reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(9), reader.GetString(10), adres, reader.GetInt32(0));
                        }
                    }
                }
            }

            return gebruiker;
        }

        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            throw new System.NotImplementedException();
        }
    }
}
