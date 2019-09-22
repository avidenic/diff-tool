using System.Threading.Tasks;
using Descartes.Demo.Models;

namespace Descartes.Demo.Services
{
    public interface IDiffService
    {
        /// <summary>
        /// Adds diff data to the repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="side"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task AddDiff(int id, Side side, DiffRequest request);

        /// <summary>
        /// Gets diff data from the repository
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Task<DiffResponse> GetDiff(int i);
    }
}