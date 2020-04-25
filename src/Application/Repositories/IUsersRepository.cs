using Domain;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(int id);
        Task<User> GetByUsernameAndPassword(string username, string password);
    }
}
