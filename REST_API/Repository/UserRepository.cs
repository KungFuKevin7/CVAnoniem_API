using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;

namespace REST_API.Repository
{
    public class UserRepository : IRepository<User>
    {

        /// <summary>
        /// Using à single instance to connect to database.
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance().GetConnectionMSQL();

        public static bool UserExist(string email, string password)
        {
            string Query = $@"SELECT COUNT(*) FROM Users
                              WHERE EmailAddress = @EmailAddress AND
                              Password = @Password";
            int exist = con.ExecuteScalar<int>(Query, new
            {
                EmailAddress = email,
                Password = password
            });
            if (exist < 1)
            {
                return false;
            }

            return true;
        }

        public List<User> Get()
        {
            string Query = $@"SELECT * FROM Users;";
            List<User> UsersList = con.Query<User>(Query).ToList();
            con.Close();
            return UsersList;
        }

        public void Add(User user)
        {
            string Query = $@"INSERT INTO Users(
                            EmailAddress, Password, PhoneNumber)
                            VALUES (@EmailAddress, @Password, @PhoneNumber);";

            con.QueryFirstOrDefault<User>(Query,
                new
                {
                    user.EmailAddress,
                    user.Password,
                    user.PhoneNumber
                });
            con.Close();
        }

        public void Update(User user, int id)
        {
            string Query = $@"UPDATE Users SET
                             PhoneNumber = @PhoneNumber,
                             Password = @Password
                             WHERE UserID = @UserID";

            con.Execute(Query,
                            new
                            {
                                user.PhoneNumber,
                                user.Password,
                                UserID = id
                            });
            con.Close();
        }

        public void Delete(int id)
        {
            string Query = $@"DELETE FROM Users
                              WHERE UserID = @UserID";

            con.Execute(Query, new { UserID = id });
            con.Close();
        }

        public User GetByID(int id)
        {
            string Query = $@"SELECT * FROM Users WHERE UserID = @UserID;";

            User UsersList = con.QuerySingle<User>(Query, new
            {
                UserID = id
            });
            con.Close();
            return UsersList;
        }
    }
}
