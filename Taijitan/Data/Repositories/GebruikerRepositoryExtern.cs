using System.Data.SqlClient;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class GebruikerRepositoryExtern : IGebruikerRepositoryExtern
    {
        // connection string aanpassen: servernaam
        private string connectionString = @"Data Source=LAPTOP-3NH1NFIB; Initial Catalog=TaijitanYoshinRyu; User ID=TaijitanAdmin; Password=a";
        private SqlConnection connection;
        private SqlDataReader reader;
        SqlCommand command;

        public GebruikerRepositoryExtern()
        {
            connection = new SqlConnection(connectionString);
        }

        public Gebruiker GetByEmail(string email)
        {
            Gebruiker gebruiker = null;

            string query = "SELECT * FROM Leden WHERE email=@email";

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Adres adres = new Adres(reader.GetString(8), reader.GetString(7), reader.GetString(6), reader.GetString(4), reader.GetString(5));
                            //gebruiker = new Gebruiker(reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(9), reader.GetString(10), adres);
                        }
                    }
                }
            }

            return gebruiker;
        }

        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            string commandStatement = "UPDATE Leden SET naam=@naam, voornaam=@voornaam, geboortedatum=@geboortedatum, telefoonnummer=@telefoonnummer, email=@email, " +
                "straat=@straat, nummer=@nummer, stad=@stad, postcode=@postcode, land=@land  WHERE lidId=@lidId";

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (command = new SqlCommand(commandStatement, connection))
                {
                    command.Parameters.AddWithValue("@naam", gebruiker.Naam);
                    command.Parameters.AddWithValue("@voornaam", gebruiker.Voornaam);
                    command.Parameters.AddWithValue("@geboortedatum", gebruiker.Geboortedatum);
                    command.Parameters.AddWithValue("@telefoonnummer", gebruiker.Telefoonnummer);
                    command.Parameters.AddWithValue("@email", gebruiker.Email);
                    command.Parameters.AddWithValue("@straat", gebruiker.Adres.Straat);
                    command.Parameters.AddWithValue("@nummer", gebruiker.Adres.Nummer);
                    command.Parameters.AddWithValue("@stad", gebruiker.Adres.Stad);
                    command.Parameters.AddWithValue("@postcode", gebruiker.Adres.Postcode);
                    command.Parameters.AddWithValue("@land", gebruiker.Adres.Land);
                    //command.Parameters.AddWithValue("@lidId", gebruiker.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
