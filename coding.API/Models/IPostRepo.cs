using System.Threading.Tasks;
namespace coding.API.Models
{
    public interface IPostRepo
    {
        Task<Post> Create(Post post);
    }
}