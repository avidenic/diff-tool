using System.Threading.Tasks;
using Descartes.Demo.Models;

namespace Descartes.Demo.Services
{
    public interface IDiffService
    {
        /// <summary>
        /// Adds diff data asynchroniously to a repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="side"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task AddDiffAsync(int id, Side side, DiffRequest request);

        /// <summary>
        /// Gets diff data asynchroniously from a repository
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Task<DiffResponse> GetDiffAsync(int i);
    }
}