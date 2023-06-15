using MySql.Data.MySqlClient;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace REST_API
{
    ///TODO: Singleton Pattern
    public class DBConnection
    {
        /// <summary>
        /// Current Instance
        /// </summary>
        private static DBConnection MyInstance = new DBConnection();

        /// <summary>
        /// Connection string
        /// </summary>
        private static readonly MySqlConnection conn = new MySqlConnection(
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        /// <summary>
        /// function that returns a MySqlConnection with the right credentials
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnectionMSQL()
        {
            //conn.Open();
            return conn;
        }

        /// <summary>
        /// Using the Singleton Design Pattern, we get the singular instance
        /// </summary>
        /// <returns></returns>
        public static DBConnection getInstance()
        {
            if (MyInstance == null)
            {
                MyInstance = new DBConnection();
            }
            return MyInstance;
        }

    }
}
