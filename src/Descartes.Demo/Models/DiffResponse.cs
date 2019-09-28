using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Descartes.Demo.Models
{
    /// <summary>
    /// A diff response message
    /// </summary>
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
                // only return differences if needed
                // TODO: check how to configure system.text.json serializer to do this
                if (DiffResultType == DiffResult.ContentDoesNotMatch)
                {
                    return _differences;
                }
                return null;
            }
        }

        /// <summary>
        /// Type of diff calculation result
        /// </summary>
        /// <value></value>
        public DiffResult DiffResultType { get; }
    }
}