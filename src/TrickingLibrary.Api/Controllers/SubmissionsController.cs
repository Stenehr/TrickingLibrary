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
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubmissionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Submission> All() => _context.Submissions;
        
        [HttpGet("{id}")]
        public Submission Get(int id) => _context.Submissions.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public async Task<Submission> Create([FromBody] Submission submission)
        {
            await _context.AddAsync(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        [HttpPut]
        public async Task<Submission> Update([FromBody] Submission submission)
        {
            if (submission?.Id == null)
            {
                return null;
            }

            await _context.AddAsync(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var submission = _context.Submissions.FirstOrDefault(x => x.Id == id);
            submission.Deleted = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}