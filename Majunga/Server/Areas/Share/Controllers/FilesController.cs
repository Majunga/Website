using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Majunga.Shared.ViewModels;
using Majunga.Shared.Models;
using Majunga.Server.Data.MongoServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Majunga.Server.Areas.Share.Controllers
{
    [Route("api/share/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileShareService _context;

        public FilesController(FileShareService context)
        {
            _context = context;
        }

        // GET: api/share/Files
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileView>>> GetFiles()
        {
            return (await _context.Get()).Select(f => new FileView 
            { 
                Id = f.Id, 
                Name = f.Name,
                Filename = f.Filename,
                ShareLink = f.ShareLink
            }).ToList();
        }

        
        // GET: api/share/Files/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<File>> GetFile(string id)
        {
            var file = await _context.Get(id);

            if(file == null)
            {
                return NotFound();
            }

            return file;
        }

        // GET: api/share/Files/{shareLink}/{filename}
        [AllowAnonymous]
        [HttpGet("{shareLink}/{filename}")]
        public async Task<ActionResult<File>> GetFile(string shareLink, string filename)
        {
            var file = (await _context.Get()).FirstOrDefault(e => e.ShareLink == shareLink);

            if(file == null || file.Filename != filename)
            {
                return NotFound();
            }

            await _context.GetFile(file);


            return base.File(file.FileBytes, file.ContentType, file.Filename);
        }

        // PUT: api/share/Files/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(string id, FileView fileViewModel)
        {
            if (id != fileViewModel.Id)
            {
                return BadRequest();
            }

            var file =  await _context.Get(id);

            file.ShareLink = fileViewModel.ShareLink;

            await _context.Update(id, file);

            return NoContent();
        }

        // POST: api/share/Files
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequestSizeLimit(1000000000)]
        [DisableRequestSizeLimit()]
        public ActionResult<File> PostFile(File file)
        {
            var createdFile = _context.Create(file);

            return CreatedAtAction("GetFile", new { id = createdFile.Id }, createdFile);
        }

        // DELETE: api/share/Files/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileAsync(string id)
        {
            var file = await _context.Get(id);
            if (file == null)
            {
                return NotFound();
            }

            await _context.Remove(id);

            return NoContent();
        }
    }
}
