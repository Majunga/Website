using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Majunga.Shared.ViewModels;
using Majunga.Shared.Models;
using Majunga.Server.Data.MongoServices;

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

        // GET: api/Files
        [HttpGet]
        public ActionResult<IEnumerable<FileView>> GetFiles()
        {
            return _context.Get().Select(f => new FileView 
            { 
                Id = f.Id, 
                Name = f.Name,
                ShareLink = f.ShareLink
            }).ToList();
        }

        // GET: api/Files/{shareLink}
        [HttpGet("{shareLink}")]
        public ActionResult<File> GetFile(string shareLink)
        {
            var file = _context.Get().FirstOrDefault(e => e.ShareLink == shareLink);

            if (file == null)
            {
                return NotFound();
            }

            return this.File(file.FileBytes, file.ContentType, file.Filename);
        }

        // PUT: api/Files/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutFile(string id, FileView fileViewModel)
        {
            if (id != fileViewModel.Id)
            {
                return BadRequest();
            }

            var file =  _context.Get(id);

            file.ShareLink = fileViewModel.ShareLink;

            _context.Update(id, file);

            return NoContent();
        }

        // POST: api/Files
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<File> PostFile(File file)
        {
            var createdFile = _context.Create(file);

            return CreatedAtAction("GetFile", new { id = createdFile.Id }, createdFile);
        }

        // DELETE: api/Files/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFile(string id)
        {
            var file = _context.Get(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.Remove(file);

            return NoContent();
        }
    }
}
