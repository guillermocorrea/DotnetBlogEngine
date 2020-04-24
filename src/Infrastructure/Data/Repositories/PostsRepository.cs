using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Post;

namespace Infrastructure.Data.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Post entity)
        {
            _context.Posts.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(p => p.User).ToListAsync();
        }

        public async Task<Post> GetAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetByStatusAsync(params PostStatus[] statusList)
        {
            if (statusList == null || statusList.Count() == 0)
            {
                return await GetAllAsync();
            }
            return await _context.Posts
                .Include(p => p.User)
                .Where(p => statusList.Contains(p.Status)).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
            {
                return;
            }
            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Post entity)
        {
            var postFromDb = await GetAsync(id);
            if (postFromDb == null)
            {
                return;
            }
            postFromDb.Title = entity.Title;
            postFromDb.Body = entity.Body;
            postFromDb.Status = entity.Status;
            postFromDb.PublishDate = entity.PublishDate;
            await _context.SaveChangesAsync();
        }
    }
}
