using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;

namespace REST_API.Repository
{
    public class OfferRepository : IRepository<Offer>
    {

        /// <summary>
        /// Using a single instance to connect to database.
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance().GetConnectionMSQL();

        public void Add(Offer offer)
        {
            string Query = $@"INSERT INTO Offer(
                            WorkField, Title, Description, Province, JobSeekerID)
                            VALUES (@WorkField, @Title, @Description, @Province, @JobSeekerID);";

            con.QueryFirstOrDefault<Offer>(Query,
                new
                {
                    WorkField = offer.WorkField,
                    Title = offer.Title,
                    Description = offer.Description,
                    Province = offer.Province,
                    JobSeekerID = offer.JobSeekerID
                });
            con.Close();
        }

        public List<Offer> Get()
        {
            string Query = $@"SELECT * FROM Offer;";
            List<Offer> Offers = con.Query<Offer>(Query).ToList();
            con.Close();
            return Offers;
        }

        public List<Offer> GetByID(int id)
        {
            string Query = $@"SELECT * FROM Offer WHERE JobSeekerID = @JobSeekerID;";
            IEnumerable<Offer> offer = con.Query<Offer>(Query,
                new
                {
                    JobSeekerID = id
                });
            con.Close();
            return offer.ToList();
        }

        public void Delete(int id)
        {
            string Query = $@"DELETE FROM Offer
                              WHERE OfferID = @OfferID";

            con.Execute(Query, new { OfferID = id });
            con.Close();
        }

        public void Update(Offer offer, int id)
        {
            string Query = $@"UPDATE Offer SET
                             WorkField = @WorkField,
                             Description = @Description,
                             Province = @Province
                             WHERE OfferID = @OfferID";

            con.Execute(Query,
                            new
                            {
                                offer.WorkField,
                                offer.Description,
                                offer.Province,
                                OfferID = id
                            });
            con.Close();
        }

        public bool UserHasOffer(int JobseekerID) 
        {
            string Query = $@"SELECT COUNT(*) 
                             FROM Offer
                             WHERE JobseekerID = @JobseekerID";
            int Result = con.ExecuteScalar<int>(Query, new
            {
                JobSeekerID = JobseekerID
            });

            switch (Result)
            {
                case 0:
                default:
                    return false;
                case 1:
                    return true ;
            }

        }

    }
}
