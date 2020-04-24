using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(int id);
        Task<User> GetByUsernameAndPassword(string username, string password);
    }
}
