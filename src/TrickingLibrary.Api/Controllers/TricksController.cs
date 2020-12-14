using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Domain;
using TrickingLibrary.Infrastructure;

namespace TrickingLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TricksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TricksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Trick> All() => _context.Tricks;

        [HttpGet("{id}")]
        public Trick Get(string id) => _context.Tricks.FirstOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) =>
            _context.Submissions.Where(x => x.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase)).ToList();

        [HttpPost]
        public async Task<Trick> Create([FromBody] Trick trick)
        {
            trick.Id = trick.Name.Replace(" ", "-").ToLowerInvariant();

            await _context.AddAsync(trick);
            await _context.SaveChangesAsync();
            return trick;
        }

        [HttpPut]
        public async Task<Trick> Update([FromBody] Trick trick)
        {
            if (string.IsNullOrEmpty(trick?.Id))
            {
                return null;
            }

            await _context.AddAsync(trick);
            await _context.SaveChangesAsync();
            return trick;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trick = _context.Tricks.FirstOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            if (trick == null)
                return NotFound();
            
            trick.Deleted = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
