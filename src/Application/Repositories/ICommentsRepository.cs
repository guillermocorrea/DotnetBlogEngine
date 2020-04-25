using Domain;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICommentsRepository
    {
        Task CreateAsync(Comment entity);
    }
}
