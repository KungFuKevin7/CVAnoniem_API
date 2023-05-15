using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;

namespace REST_API.Repository
{
    public class SavedOffersRepository : IRepository<SavedOffers>
    {

        /// <summary>
        /// Using à single instance to connect to database.
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance().GetConnectionMSQL();

        public void Add(SavedOffers offer)
        {
            string Query = $@"INSERT INTO SavedOffers(
                            EmployerID, OfferID)
                            VALUES (@EmployerID, @OfferID);";

            con.QueryFirstOrDefault<SavedOffers>(Query,
                new
                {
                    EmployerID = offer.EmployerID,
                    OfferID = offer.OfferID,
                });
            con.Close();
        }

        public void Delete(int SavedID)
        {
            string Query = $@"DELETE FROM SavedOffers
                              WHERE SavedID = @SavedID";
                             
            con.Execute(Query, new 
            { 
                 SavedID = SavedID,
            });
            con.Close();
        }

        public List<SavedOffers> Get()
        {
            string Query = $@"SELECT * FROM SavedOffers;";
            List<SavedOffers> savedOffers = 
                con.Query<SavedOffers>(Query).ToList();
            con.Close();
            return savedOffers;
        }

        public SavedOffers GetByID(int id)
        {
            string Query = $@"SELECT * FROM SavedOffers
                              WHERE EmployerID = @EmployerID;";
            SavedOffers savedOffers =
                con.QuerySingle<SavedOffers>(Query, new
                {
                    EmployerID = id
                }) ;
            con.Close();
            return savedOffers;
        }

        public void Update(SavedOffers updatedObject, int id)
        {
            throw new NotImplementedException();
        }

  
    }
}
