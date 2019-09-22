using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Descartes.Demo.Models
{
    public class DiffResponse
    {
        public DiffResponse(IReadOnlyCollection<Difference> differences)
        {
            _differences = differences;
            DiffResultType = _differences switch
            {
                IReadOnlyCollection<Difference> d when d.Count == 0 => DiffResult.Equals,
                null => DiffResult.SizeDoesNotMatch,
                _ => DiffResult.ContentDoesNotMatch
            };
        }

        private readonly IReadOnlyCollection<Difference> _differences;

        [JsonPropertyName("diffs")]
        public IReadOnlyCollection<Difference> Differences
        {
            get
            {
                // only display them if needed
                if (DiffResultType == DiffResult.ContentDoesNotMatch)
                {
                    return _differences;
                }
                return null;
            }
        }
        public DiffResult DiffResultType { get; }
    }
}