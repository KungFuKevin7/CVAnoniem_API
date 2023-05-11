using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;

namespace REST_API
{
    public class QueryRepository
    {

        /// <summary>
        /// Using the single instance to connect to database. (for each query)
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance().GetConnectionMSQL();

        public static List<User> GetUsers() 
        {
            string Query = $@"SELECT * FROM Users;";
            List<User> UsersList = con.Query<User>(Query).ToList();
            con.Close();
            return UsersList;
        }

        public static void AddUser(User user) 
        {
            string Query = $@"INSERT INTO Users(
                            EmailAddress, Password, PhoneNumber)
                            VALUES (@EmailAddress, @Password, @PhoneNumber);";

            User addUser = con.QueryFirstOrDefault<User>(Query, 
                new { EmailAddress = user.EmailAddress,
                      Password = user.Password,           
                      PhoneNumber = user.PhoneNumber
                });
            con.Close();
        }

        public static bool UserExist(string email, string password) 
        {
            string Query = $@"SELECT COUNT(*) FROM Users
                              WHERE EmailAddress = @EmailAddress AND
                              Password = @Password";
            int exist = con.ExecuteScalar<int>(Query, new {
                            EmailAddress = email,
                            Password = password
                        });
            if (exist < 1)
            {
                return false;
            }

            return true;
        }

        public static void UpdateUser(User user, int id)
        {
            string Query = $@"UPDATE Users SET
                             PhoneNumber = @PhoneNumber,
                             Password = @Password
                             WHERE UserID = @UserID";

            con.Execute(Query,
                            new
                            {
                                PhoneNumber = user.PhoneNumber,
                                Password = user.Password,
                                UserID = id
                            });
            con.Close();
        }

        public static void RemoveUser(int id) 
        {
            string Query = $@"DELETE FROM Users
                              WHERE UserID = @UserID";

            con.Execute(Query, new { UserID = id });
            con.Close();
        }

        public static List<Offer> GetOffers()
        {
            string Query = $@"SELECT * FROM Offer;";
            List<Offer> OffersList = con.Query<Offer>(Query).ToList();
            con.Close();
            return OffersList;
        }
    }
}
