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

        /* Select statment to get person & creator info. JOIN USAGE
         * select p.Name, p.BirthDate, p.MissingDate, p.AdditionalInfo, c.UserName, c.Email
from dbo.Persons p, dbo.AspNetUsers c
where p.CreatorId = c.[Id];*/

        // GET: api/Persons/Get
        [HttpGet]
        public IActionResult Get()
        {
            // join: get person info & person face photos
            // return Ok(_context.Persons.ToList());

            var faces =
                  from f in _context.Faces.ToList() //.AsEnumerable()
                  group f by f.OwnerId into g
                  select new
                  {
                      OwnerId = g.Key,
                      FacePhoto = g.Select(f => new Face { Id = f.Id, BlobURI = f.BlobURI })
                  };

            
            var result = _context.Persons.Join(faces,
                    p => p.Id,
                    f => f.OwnerId,
                    (p, f) => new Person
                              {
                                Id = p.Id,
                                Name = p.Name,
                                BirthDate = p.BirthDate,
                                MissingDate = p.MissingDate,
                                AdditionalInfo = p.AdditionalInfo,
                                CreatorId = p.CreatorId,
                                FacePhotos = f.FacePhoto.AsEnumerable()
                              }
                );
            return Ok(result);


            /*

            var result = _context.Persons.Join(_context.Faces,
                p => p.Id,
                f => f.OwnerId,
                (p, f) => new { p, f.BlobURI }
           );
           */
            /*  var result = from p in _context.Persons
              select p.Name, p.BirthDate, p.MissingDate, p.AdditionalInfo, c.UserName, c.Email
              from dbo.Persons p, dbo.AspNetUsers c
  where p.CreatorId = c.[Id]*/

            /*
            var eventsWithCount = (from e in context.Events
                                   join c in counts on e.Id equals c.eventId into temp
                                   from c in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       id = e.Id,
                                       name = e.Name,
                                       startTime = e.StartTime,
                                       endTime = e.EndTime,
                                       cooldownLength = e.CooldownLength,
                                       creatorId = e.CreatorId,
                                       participantCount = (c == null) ? 0 : c.count
                                   }).ToList();*/

            /*var result = from p in _context.Persons
                         join f in _context.Faces on p.Id equals f.Owner
                         select new
                         {
                             name = p.Name,
                             bdate= p.BirthDate,
                             mdate= p.MissingDate,
                             add = p.AdditionalInfo,
                             creator = p.CreatorId,
                             faces = p.Photos,
                             id = f.Id
                         };*/
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

            try
            {
              /*  var hist = _context.History
                         .Where(h => h.PersonId == person.Id)
                         .FirstOrDefault<PersonHistory>();*/

                _context.Entry(person).Collection(p => p.History).Load();

                var deletedPerson = _context.Persons.Remove(person);

                if (deletedPerson.Entity == null)
                    return BadRequest("Error while deleting.");

            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source);
            }


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
 
 