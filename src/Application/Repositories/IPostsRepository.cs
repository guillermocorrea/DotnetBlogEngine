using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Domain.Post;

namespace Application.Repositories
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetByStatusAsync(params PostStatus[] statusList);
        Task<Post> GetAsync(int id);
        Task CreateAsync(Post entity);
        Task UpdateAsync(int id, Post entity);
        Task RemoveAsync(int id);
    }
}
