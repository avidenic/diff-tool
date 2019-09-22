using System.Collections.Concurrent;
using System.Threading.Tasks;
using Descartes.Demo.Infrastructure;
using Descartes.Demo.Models;

namespace Descartes.Demo.Services
{
    public class InMemoryDiffService : IDiffService
    {
        private static readonly ConcurrentDictionary<int, Comparison> Store = new ConcurrentDictionary<int, Comparison>();

        /// <inheritdoc cref="IDiffService" />
        public Task AddDiff(int id, Side side, DiffRequest request)
        {
            var comparison = Store.GetOrAdd(id, f => new Comparison());
            comparison[side] = request.Data;
            // simulate I/O operations 
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IDiffService" />
        public Task<DiffResponse> GetDiff(int id)
        {
            // if nothing is stored or null value is stored, simulate I/O and return null
            if (!Store.TryGetValue(id, out Comparison comparison) || comparison == null)
            {
                return Task.FromResult(null as DiffResponse);
            }

            try
            {
                // get the differences and return whatever was found
                var differences = comparison.GetDifferences();
                return Task.FromResult(new DiffResponse(differences));
            }
            catch (BusinessRuleException)
            {
                // could not process according to business rules, should return null
                return Task.FromResult(null as DiffResponse);
            }
        }
    }
}