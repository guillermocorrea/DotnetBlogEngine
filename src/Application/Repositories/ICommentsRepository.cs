using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICommentsRepository
    {
        Task CreateAsync(Comment entity);
    }
}
