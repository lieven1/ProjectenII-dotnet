using System.Data.SqlClient;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class SessieRepository : ISessieRepository
    {
        private string connectionString = @"Data Source=LAPTOP-3NH1NFIB; Initial Catalog=TaijitanYoshinRyu; User ID=TaijitanAdmin; Password=a";
        private SqlConnection connection;
        private SqlDataAdapter adapter;

        public SessieRepository()
        {
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();
        }


    }
}
