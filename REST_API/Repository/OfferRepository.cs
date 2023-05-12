using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;

namespace REST_API.Repository
{
    public class OfferRepository : IRepository<Offer>
    {

        /// <summary>
        /// Using à single instance to connect to database.
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance().GetConnectionMSQL();

        public void Add(Offer offer)
        {
            string Query = $@"INSERT INTO Offer(
                            WorkField, Description, Province, JobSeekerID)
                            VALUES (@WorkField, @Description, @Province, @JobSeekerID);";

            con.QueryFirstOrDefault<Offer>(Query,
                new
                {
                    offer.WorkField,
                    offer.Description,
                    offer.Province,
                    offer.JobSeekerID
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

        public Offer GetByID(int id)
        {
            string Query = $@"SELECT * FROM Offer WHERE OfferID = @OfferID;";
            Offer offer = con.QuerySingle<Offer>(Query,
                new
                {
                    OfferID = id
                });
            con.Close();
            return offer;
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

    }
}
