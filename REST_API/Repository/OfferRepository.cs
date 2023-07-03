using Dapper;
using MySql.Data.MySqlClient;
using REST_API.Model;
using System;

namespace REST_API.Repository
{
    public class OfferRepository : IRepository<Offer>
    {

        /// <summary>
        /// Using a single instance to connect to database.
        /// </summary>
        public static MySqlConnection con = DBConnection.getInstance().GetConnectionMSQL();

        public string Add(Offer offer)
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

            return "Success";
            //con.Close();
        }

        public List<Offer> Get()
        {
            string Query = $@"SELECT * FROM Offer;";
            List<Offer> Offers = con.Query<Offer>(Query).ToList();
            //con.Close();
            return Offers;
        }

        public List<Offer> GetByID(int id)
        {
            string Query = $@"SELECT * FROM Offer WHERE JobSeekerID = @JobSeekerID;";
            try
            {
                IEnumerable<Offer> offer = con.Query<Offer>(Query,
                new
                { 
                    JobSeekerID = id
                });

                //con.close();
                return offer.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Offer>();
            }
            
        }

        public List<Offer> GetByOfferID(int id)
        {
            string Query = $@"SELECT * FROM Offer WHERE OfferID = @OfferID;";
            IEnumerable<Offer> offer = con.Query<Offer>(Query,
                new
                {
                    OfferID = id
                });
            //con.Close();
            return offer.ToList();
        }

        public void Delete(int id)
        {
            string Query = $@"DELETE FROM Offer
                              WHERE OfferID = @OfferID";

            con.Execute(Query, new { OfferID = id });
            //con.Close();
        }

        public void Update(Offer offer, int offerID)
        {
            string Query = $@"UPDATE Offer SET
                             Title = @Title,
                             WorkField = @WorkField,
                             Description = @Description,
                             Province = @Province
                             WHERE OfferID = @OfferID";

            con.Execute(Query,
                            new
                            {
                                offer.Title,
                                offer.WorkField,
                                offer.Description,
                                offer.Province,
                                offerID
                            });
           //con.Close();
        }

        public async Task<int> UserHasOffer(int JobseekerID) 
        {
            string Query = $@"SELECT OfferID 
                             FROM Offer
                             WHERE JobseekerID = @JobseekerID";
            int Result = await con.ExecuteScalarAsync<int>(Query, new
            {
                JobSeekerID = JobseekerID
            });

            return Result;

        }

        public List<Offer> getOffersByName(string input) 
        {
            input = $"%{input}%";

            string Query = $@"SELECT * 
                              FROM Offer
                              WHERE Title LIKE @input
                              OR
                              WorkField LIKE @input
                              OR
                              Description LIKE @input
                              OR
                              Province LIKE @input;";
            IEnumerable<Offer> offer = con.Query<Offer>(Query,
                new
                {
                    input = input
                });
            //con.Close();

            return offer.ToList();
        }
    
    }
}
