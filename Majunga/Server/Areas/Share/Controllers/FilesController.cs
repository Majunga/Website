using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Majunga.Server.Data;
using Majunga.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;

namespace Majunga.Server.Areas.Share.Controllers
{
    [Route("api/share/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileView>>> GetFiles()
        {
            return await _context.Files.Select(f => new FileView 
            { 
                Id = f.Id, 
                Name = f.Name,
                ShareLink = f.ShareLink
            }).ToListAsync();
        }

        // GET: api/Files/{shareLink}
        [HttpGet("{shareLink}")]
        public async Task<ActionResult<File>> GetFile(string shareLink)
        {
            var file = await _context.Files.FirstOrDefaultAsync(e => e.ShareLink == shareLink);

            if (file == null)
            {
                return NotFound();
            }

            return this.File(file.FileBytes, file.ContentType, file.Filename);
        }

        // PUT: api/Files/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(int id, FileView fileViewModel)
        {
            if (id != fileViewModel.Id)
            {
                return BadRequest();
            }

            var file = await _context.Files.FindAsync(id);

            file.ShareLink = fileViewModel.ShareLink;

            _context.Entry(file).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Files
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<File>> PostFile(File file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFile", new { id = file.Id }, file);
        }

        // DELETE: api/Files/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FileExists(int id)
        {
            return _context.Files.Any(e => e.Id == id);
        }
    }
}
