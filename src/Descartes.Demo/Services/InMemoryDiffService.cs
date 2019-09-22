using System.Collections.Concurrent;
using System.Threading.Tasks;
using Descartes.Demo.Infrastructure;
using Descartes.Demo.Models;

namespace Descartes.Demo.Services
{
    public class InMemoryDiffService : IDiffService
    {
        private static readonly ConcurrentDictionary<int, Diff> Store = new ConcurrentDictionary<int, Diff>();

        public Task AddDiff(int id, Side side, DiffRequest request)
        {
            var diff = Store.GetOrAdd(id, f => new Diff());
            diff[side] = request.Data;
            return Task.CompletedTask;
        }

        public Task<DiffResponse> GetDiff(int id)
        {
            if (!Store.TryGetValue(id, out Diff diff) || diff == null)
            {
                return Task.FromResult(null as DiffResponse);
            }

            try
            {
                return Task.FromResult(new DiffResponse(diff.CalculateDifferences()));
            }
            catch (BusinessRuleException)
            {
                return Task.FromResult(null as DiffResponse);
            }
        }
    }
}