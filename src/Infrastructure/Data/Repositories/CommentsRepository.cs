using Application.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Comment entity)
        {
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
