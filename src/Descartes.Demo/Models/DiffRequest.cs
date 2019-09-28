using System.ComponentModel.DataAnnotations;
using Descartes.Demo.Infrastructure;

namespace Descartes.Demo.Models
{
    /// <summary>
    /// Diff request message
    /// </summary>
    public class DiffRequest
    {
        /// <summary>
        /// Diff data
        /// </summary>
        /// <value></value>
        [Required(AllowEmptyStrings = false), ShouldBeBase64(ErrorMessage = "It should be binary data that is base64 encoded.")]
        public string Data { get; set; }
    }
}