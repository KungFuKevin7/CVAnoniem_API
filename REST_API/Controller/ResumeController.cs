using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using MySqlX.XDevAPI.Common;
using REST_API.Model;
using REST_API.Repository;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Http.Results;
using System.Xml.Linq;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/[action]")]
    public class ResumeController
    {
        public static ResumeRepository ResumeRepo = new ResumeRepository();
        public static OfferRepository OfferRepo = new OfferRepository();

        public static List<Resume> resumeCollection = new List<Resume>();

        [HttpPost]
        [ActionName("resume")]
        public void Add([FromBody] Resume resume)
        {
            resumeCollection.Add(resume);
            ResumeRepo.Add(resume);
        }

        [HttpDelete]
        [ActionName("resume")]
        public void Delete(int id)
        {
            ResumeRepo.Delete(id);
        }

        [HttpPut]
        [ActionName("resume")]
        public void Update(Resume updatedResume, int id)
        {
            ResumeRepo.Update(updatedResume, id);
        }

        [HttpGet]
        [ActionName("resume/full-resume-list")]
        public string GetResumeList()
        {
            resumeCollection = ResumeRepo.Get();
            var json = JsonSerializer.Serialize(resumeCollection);
            return json;
        }

        [HttpGet]
        [ActionName("resume")]
        public IActionResult GetResumeByID(int userID)
        {
            //System.Console.WriteLine("got here");
            //uitgecomment omdat server niet draait op het moment
            int offerID = OfferRepo.UserHasOffer(userID);
            List<Resume> resume = ResumeRepo.GetByID(offerID);

            
            
            var stream = new FileStream(Path.Combine(Environment.CurrentDirectory, resume[0].CensoredResume), FileMode.Open);


            //var stream = new FileStream(Path.Combine(Environment.CurrentDirectory, "PDF-testopslag\\0.pdf"), FileMode.Open);
            
            return new FileStreamResult(stream, "application/pdf");

            

        }


        [HttpGet]
        [ActionName("resume/user-has-resume")]
        public int GetUserHasResume(int userID)
        {
            int offerID = OfferRepo.UserHasOffer(userID);
            return ResumeRepo.GetByID(offerID).Count;
        }


        public int AddTest(IFormFile file, int userID, int offerID)
        {
            Resume resume = new Resume();

            var File = file;
            System.Console.WriteLine(File.FileName + File.ContentType + " " + userID);

            if (File.Length > 2000000)
            {
                return 2;
            }

            if (File.ContentType == "application/pdf")
            {

                if (file.Length > 0)
                {
                    // haal slashes uit de filenaam
                    //string untrustedFileName = Path.GetFileName(File.FileName);
                    string localFilePath = @"PDF-testopslag\" + userID + ".pdf";
                    string filePath = Path.Combine(Environment.CurrentDirectory, localFilePath);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        File.CopyTo(fileStream);
                        fileStream.Close();
                    }
                    resume.FullResume = localFilePath;
                    resume.CensoredResume = localFilePath;
                    resume.OfferID = offerID;
                    System.Console.WriteLine("add");

                    ResumeRepo.Add(resume);
                    // zet filepath en andere data in database

                }
                return 1;
            }

            return 0;
        }

        public int UpdateTest(IFormFile file, int userID, int offerID)
        {
            Resume resume = new Resume();

            var File = file;
            System.Console.WriteLine(File.FileName + File.ContentType + " " + userID);

            if (File.Length > 2000000)
            {
                return 2;
            }

            if (File.ContentType == "application/pdf")
            {

                if (file.Length > 0)
                {
                    // haal slashes uit de filenaam
                    string untrustedFileName = Path.GetFileName(File.FileName);
                    string localFilePath = @"PDF-testopslag\" + userID + ".pdf";
                    string filePath = Path.Combine(Environment.CurrentDirectory, localFilePath);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        File.CopyTo(fileStream);
                        fileStream.Close();
                    }
                    resume.FullResume = localFilePath;
                    resume.CensoredResume = localFilePath;
                    resume.OfferID = offerID;
                    System.Console.WriteLine("update");

                    ResumeRepo.Update(resume, offerID);
                    // zet filepath en andere data in database

                }
                return 1;
            }

            return 0;
        }

        [HttpPost]
        [ActionName("resume/check")]
        public int CheckFile(IFormFile file)
        {
            
            // do checks on file before uploading to server
            var File = file;
            int maxSize = 2000000;
            // size check can use different value
            if (File.Length > maxSize)
            {
                return 2;
            }

            if (File.ContentType == "application/pdf")
            {
                return 1;
            }

            return 0;
        }
    }
}
