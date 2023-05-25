using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;

namespace REST_API.Repository
{
    public class PermissionRepository : IRepository<Permission>
    {
        /// <summary>
        /// Using a single instance to connect to database.
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance()
                                                  .GetConnectionMSQL();

        public void Add(Permission permission)
        {
            string Query = $@"INSERT INTO Permissions(
                            ResumeID, EmployerID)
                            VALUES (@ResumeID, @EmployerID);";

            con.QueryFirstOrDefault<Permission>(Query,
                new
                {
                    ResumeID = permission.ResumeID,
                    EmployerID = permission.EmployerID
                });
            con.Close();
        }

        public void Delete(int id)
        {
            string Query = $@"DELETE FROM Permissions
                              WHERE PermissionID = @PermissionID";

            con.Execute(Query, new { PermissionID = id });
            con.Close();
        }

        public List<Permission> Get()
        {
            string Query = $@"SELECT * FROM Permissions;";
            List<Permission> permissions = con.Query<Permission>(Query).ToList();
            con.Close();
            return permissions;
        }

        public List<Permission> GetByID(int id)
        {
            string Query = $@"SELECT * FROM Permissions 
                              WHERE PermissionID = @PermissionID;";
            IEnumerable<Permission> permission = con.Query<Permission>(Query,
                new
                {
                    PermissionID = id
                });
            con.Close();
            return permission.ToList();
        }

        public void Update(Permission updatedObject, int id)
        {
            throw new NotImplementedException();
        }
    }
}
