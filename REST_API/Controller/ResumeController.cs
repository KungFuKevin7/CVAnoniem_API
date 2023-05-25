using Microsoft.AspNetCore.Mvc;
using REST_API.Model;
using REST_API.Repository;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace REST_API.Controller
{
    [ApiController]
    [Route("api/[action]")]
    public class ResumeController
    {
        public static ResumeRepository ResumeRepo = new ResumeRepository();

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
        public string GetResumeByID(int id)
        {
            var json = JsonSerializer.Serialize(ResumeRepo.GetByID(id));
            return json;
        }
    }
}
