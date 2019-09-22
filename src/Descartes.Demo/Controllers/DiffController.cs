using System.Net;
using System.Threading.Tasks;
using Descartes.Demo.Models;
using Descartes.Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Descartes.Demo.Controllers
{
    [ApiController, Route("v1/diff")]
    public class DiffController : ControllerBase
    {
        private readonly IDiffService _service;
        public DiffController(IDiffService service)
        {
            _service = service;
        }

        [HttpPut("{id:int:min(1)}/{side:side}")]
        public async Task<IActionResult> Put(int id, Side side, [FromBody]DiffRequest diff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.AddDiff(id, side, diff);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            DiffResponse diff;

            diff = await _service.GetDiff(id);
            if (diff == null)
            {
                return NotFound();
            }

            return Ok(diff);
        }
    }
}