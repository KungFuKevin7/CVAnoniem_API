using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;

namespace REST_API.Repository
{
    public class ResumeRepository : IRepository<Resume>
    {
        /// <summary>
        /// Using à single instance to connect to database.
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance()
                                                  .GetConnectionMSQL();

        public void Add(Resume resume)
        {
            string Query = $@"INSERT INTO Resume(
                            FullResume, CensoredResume, OfferID)
                            VALUES (@FullResume, @CensoredResume, @OfferID);";

            con.QueryFirstOrDefault<Resume>(Query,
                new
                {
                    FullResume = resume.FullResume,
                    CensoredResume = resume.CensoredResume,
                    OfferID = resume.OfferID
                });
            con.Close();
        }

        public void Delete(int id)
        {
            string Query = $@"DELETE FROM Resume
                              WHERE ResumeID = @ResumeID";

            con.Execute(Query, new { ResumeID = id });
            con.Close();
        }

        public List<Resume> Get()
        {
            string Query = $@"SELECT * FROM Resume;";

            List<Resume> ResumeList = con.Query<Resume>(Query).ToList();
            con.Close();
            return ResumeList;
        }

        public List<Resume> GetByID(int id)
        {
            string Query = $@"SELECT * FROM Resume
                              WHERE ResumeID = @ResumeID;";

            IEnumerable<Resume> UsersList = con.Query<Resume>(Query, new
            {
                ResumeID = id
            });
            con.Close();
            return UsersList.ToList();
        }

        public void Update(Resume updatedResume, int id)
        {
            string Query = $@"UPDATE Resume SET
                             FullResume = @FullResume,
                             CensoredResume = @CensoredResume
                             WHERE ResumeID = @ResumeID";

            con.Execute(Query,
                            new
                            {
                                FullResume = updatedResume.FullResume,
                                CensoredResume = updatedResume.CensoredResume,
                                ResumeID = id
                            });
            con.Close();
        }
    }
}
