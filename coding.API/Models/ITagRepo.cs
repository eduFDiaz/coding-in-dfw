using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace coding.API.Models
{
    public interface ITagRepo
    {
        Task<Tag> Create(Tag tag);
        Task<List<Tag>> GetAllPostTags(int postid);
        Task<Tag> GetTag(int tagid);
        Task<bool> DeleteTag(int tagid);
        Task<bool> SaveAll();
        Task<List<Tag>> GetAll();
    }
}