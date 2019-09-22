using System.Threading.Tasks;
using Descartes.Demo.Models;

namespace Descartes.Demo.Services
{
    public interface IDiffService
    {
        Task AddDiff(int id, Side side, DiffRequest request);

        Task<DiffResponse> GetDiff(int i);
    }
}