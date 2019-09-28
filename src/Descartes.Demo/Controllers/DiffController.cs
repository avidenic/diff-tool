using System.Net;
using System.Threading.Tasks;
using Descartes.Demo.Models;
using Descartes.Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Descartes.Demo.Controllers
{
    /// <summary>
    /// Add a diff to be compared or get the comparison
    /// </summary>
    [ApiController, Route("v1/diff")]
    public class DiffController : ControllerBase
    {
        private readonly IDiffService _service;
        public DiffController(IDiffService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates or modifies a part of the diff for comparison
        /// </summary>
        /// <param name="id">Diff id</param>
        /// <param name="side">Diff side</param>
        /// <param name="diff">Diff data</param>
        /// <returns>201 If diff was succsfully added</returns>
        [HttpPut("{id:int:min(1)}/{side:side}")]
        public async Task<IActionResult> Put(int id, Side side, [FromBody]DiffRequest diff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.AddDiffAsync(id, side, diff);
            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Gets the differences between stored diffs. If at leaast one side is missing, returns 404
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            DiffResponse diff = await _service.GetDiffAsync(id);
            if (diff == null)
            {
                return NotFound();
            }

            return Ok(diff);
        }
    }
}