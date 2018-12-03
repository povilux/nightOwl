using Microsoft.EntityFrameworkCore;
using NightOwl.WebService.DAL;
using NightOwl.WebService.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Drawing;
using System.Collections.Generic;
using NightOwl.WebService.Extensions;
using NightOwl.WebService.Services;
using System.IO;
using System.Text;

namespace NightOwl.WebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public PersonsController(UserManager<User> userManager, DatabaseContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Persons/Get
        [HttpGet]
        public IActionResult Get()
        {
            // join: get person info & person face photos
            return Ok(_context.Persons.ToList());
        }


        // GET: api/Persons/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {            
            // join: get person info & person face photos
            var person = await _context.Persons.FindAsync(id);

            if(person == null)
                return NotFound();
           
            return Ok(person);
        }

        // GET: api/Persons/GetByCreatorId/5
        [HttpGet("{creatorId}")]
        public IActionResult GetByCreatorId([FromRoute]Guid creatorId)
        {
            // join: get person info & person face photos
            var persons = _context.Persons.Where(p => p.Creator.Id.Equals(creatorId.ToString())).ToList();

            if (persons == null)
                return NotFound();

            return Ok(persons);
        }

        // GET: api/Persons/GetPersonsByCreator/
        [HttpGet]
        public IActionResult GetPersonsByCreator()
        {
            // join: get person info & person face photos

            var persons = _userManager.Users.
                GroupJoin(_context.Persons.ToList(),
                u => u.Id,
                p => p.Creator.Id,
                (u, personsGroup) => new
                {
                    u.UserName,
                    Persons = personsGroup
                });

            if (persons == null)
                return NotFound();

            return Ok(persons.ToList());

        }

        // PUT: api/Persons/Put/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Person person, [FromRoute]int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = _context.Persons.Update(person);

                if (updated.Entity == null)
                    return BadRequest("Error while updating.");

                ICloudBlobService cloudBlobService = new CloudBlobService();
                ICollection<Face> faceBlobs = await cloudBlobService.UploadFaceBlobAsync(id, id.ToString() + "-container", person.Photos);

                foreach (Face blob in faceBlobs)
                {
                    var faceBlobCreated = _context.Faces.Add(blob);

                    if (faceBlobCreated.Entity == null)
                        return BadRequest("Error while adding face to database");
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

      
        // POST: api/Persons/Post/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person person)
        {
            if(!ModelState.IsValid)
                return BadRequest(string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));

            var created = _context.Persons.Add(person);

            if(created.Entity == null)
                return BadRequest("Error while adding user to database");
            
            try
            {
                _context.SaveChanges();

                ICloudBlobService cloudBlobService = new CloudBlobService();
                ICollection<Face> faceBlobs = await cloudBlobService.UploadFaceBlobAsync(created.Entity.Id, created.Entity.Id.ToString() + "-container", person.Photos);

                foreach(Face blob in faceBlobs)
                {
                    var faceBlobCreated = _context.Faces.Add(blob);

                    if (faceBlobCreated.Entity == null)
                        return BadRequest("Error while adding face to database");
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Error while saving: " + " " + ex.Message);
            }
            
            return Ok(created.Entity);
        }

        // DELETE: api/Persons/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
                return NotFound();
            
            var deletedPerson = _context.Persons.Remove(person);

            if (deletedPerson.Entity == null)
                return BadRequest("Error while deleting.");

            try
            {
                ICloudBlobService cloudBlobService = new CloudBlobService();
                await cloudBlobService.DeleteFaceBlobAsync(id.ToString() + "-container");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Error while saving changes: " + ex.Message);
            }
            return Ok(true);
        }
    }
}
 