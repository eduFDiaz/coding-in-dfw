using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;


using coding.API.Models.Entities.WorkExperiences;
using coding.API.Models.Entities.Educations;
using coding.API.Models.Entities.Awards;
using coding.API.Models.Entities.Languages;
using coding.API.Models.Entities.Projects;
using coding.API.Models.Entities.Skills;
using coding.API.Models.Entities.Posts;



using coding.API.Models.Interfaces;


namespace coding.API.Data
{
    public class ResumeRepo 
    {
        
        // private readonly DataContext _context;
        private readonly GenericRepo<Post> _postRepo;

        public ResumeRepo(GenericRepo<Post> postRepo)
        {
            _postRepo = postRepo;
        }
       
        public async Task<bool> SaveAll()
        {
            return true;
        }
       
     
    }
}