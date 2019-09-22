using System.Collections.Generic;

namespace Descartes.Demo.Models
{
    public class DiffResponse
    {
        public DiffResponse(IReadOnlyCollection<Difference> differences)
        {
            Differences = differences;
            DiffResultType = Differences switch
            {
                IReadOnlyCollection<Difference> d when d.Count == 0 => DiffResult.Equals,
                null => DiffResult.SizeDoesNotMatch,
                _ => DiffResult.ContentDoesNotMatch
            };
        }
        public IReadOnlyCollection<Difference> Differences { get; }
        public DiffResult DiffResultType { get; }
    }
}