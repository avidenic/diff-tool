using System.ComponentModel.DataAnnotations;
using Descartes.Demo.Infrastructure;

namespace Descartes.Demo.Models
{
    public class DiffRequest
    {
        [Required(AllowEmptyStrings = false), ShouldBeBase64]
        public string Data { get; set; }
    }
}