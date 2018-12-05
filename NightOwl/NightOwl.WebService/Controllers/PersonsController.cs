using NightOwl.WebService.DAL;
using NightOwl.WebService.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using NightOwl.WebService.Services;

namespace NightOwl.WebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private const int _sectionSize = 5;
        private const string _containerNamePrefix = "-container";

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
            try
            {

                var faces =
                    (from f in _context.Faces
                    group f by f.OwnerId into g
                    select new
                    {
                        OwnerId = g.Key,
                        FacePhoto = g.Select(f => new Face { Id = f.Id, BlobURI = f.BlobURI })
                    }).ToList();

                var result = _context.Persons
                               .Join(faces,
                                 p => p.Id,
                                 f => f.OwnerId,
                                 (p, f) => new { p, f })
                               .Join(_userManager.Users,
                                  pp => pp.p.CreatorId,
                                  u => u.Id,
                                  (pp, u) => new { pp, u })
                               .Select((ppf) =>
                                 new Person
                                 {
                                     Id = ppf.pp.p.Id,
                                     Name = ppf.pp.p.Name,
                                     BirthDate = ppf.pp.p.BirthDate,
                                     MissingDate = ppf.pp.p.MissingDate,
                                     AdditionalInfo = ppf.pp.p.AdditionalInfo,
                                     FacePhotos = ppf.pp.f.FacePhoto,
                                     CreatorId = ppf.pp.p.CreatorId,
                                     CreatorName = ppf.u.UserName,
                                     CreatorEmail = ppf.u.Email,
                                     CreatorPhone = ppf.u.PhoneNumber
                                 }
                              ).ToList();

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }

        // GET: api/Persons/GetPart/sectionNumber
        [HttpGet("{section}")]
        public IActionResult GetPart([FromRoute]int section)
        {
            if (section < 0)
                return BadRequest("Invalid section number (section should be bigger than 0)");

            try
            {
                var faces =
                      (from f in _context.Faces.AsEnumerable()
                      group f by f.OwnerId into g
                      select new
                      {
                          OwnerId = g.Key,
                          FacePhoto = g.Select(f => new Face { Id = f.Id, BlobURI = f.BlobURI })
                      }).ToList();

                var persons = _context.Persons
                   .Join(faces,
                     p => p.Id,
                     f => f.OwnerId,
                     (p, f) => new { p, f })
                   .Join(_userManager.Users,
                      pp => pp.p.CreatorId,
                      u => u.Id,
                      (pp, u) => new { pp, u })
                   .Select((ppf) =>
                         new Person
                         {
                             Id = ppf.pp.p.Id,
                             Name = ppf.pp.p.Name,
                             BirthDate = ppf.pp.p.BirthDate,
                             MissingDate = ppf.pp.p.MissingDate,
                             AdditionalInfo = ppf.pp.p.AdditionalInfo,
                             FacePhotos = ppf.pp.f.FacePhoto,
                             CreatorId = ppf.pp.p.CreatorId,
                             CreatorName = ppf.u.UserName,
                             CreatorEmail = ppf.u.Email,
                             CreatorPhone = ppf.u.PhoneNumber
                         }
                      ).ToList();


                var result = persons.Skip(_sectionSize * section).Take(_sectionSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }

        // GET: api/Persons/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                // join: get person info & person face photos
                var person = await _context.Persons.FindAsync(id);

                if (person == null)
                    return NotFound();

                var user = _userManager.Users.Where(u => u.Id == person.CreatorId).
                    Select(u => new { u.UserName, u.Email, u.PhoneNumber }).FirstOrDefault();

                person.FacePhotos = from f in _context.Faces
                                    where
                                      f.OwnerId == id
                                    select new Face
                                    {
                                        Id = f.Id,
                                        BlobURI = f.BlobURI
                                    };

                person.CreatorName = user.UserName;
                person.CreatorEmail = user.Email;
                person.CreatorPhone = user.PhoneNumber;


                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace);
            }
        }

        // GET: api/Persons/GetByCreatorId/5
        [HttpGet("{creatorId}")]
        public IActionResult GetByCreatorId([FromRoute]Guid creatorId)
        {
            try
            {
                var persons = _context.Persons.Where(p => p.Creator.Id.Equals(creatorId.ToString())).ToList();

                if (persons == null)
                    return NotFound();

                var faces =
                  from f in _context.Faces
                  group f by f.OwnerId into g
                  select new
                  {
                      OwnerId = g.Key,
                      FacePhoto = g.Select(f => new Face { Id = f.Id, BlobURI = f.BlobURI })
                  };

                var result = persons
                               .Join(faces,
                                 p => p.Id,
                                 f => f.OwnerId,
                                 (p, f) => new { p, f })
                               .Select(pf =>
                                 new Person
                                 {
                                     Id = pf.p.Id,
                                     Name = pf.p.Name,
                                     BirthDate = pf.p.BirthDate,
                                     MissingDate = pf.p.MissingDate,
                                     AdditionalInfo = pf.p.AdditionalInfo,
                                     FacePhotos = pf.f.FacePhoto,
                                     CreatorId = pf.p.CreatorId
                                 }
                              );

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }

        // to do: transfer to Users controller
        // GET: api/Persons/GetPersonsByCreator/
        [HttpGet]
        public IActionResult GetPersonsByCreator()
        {
            try
            {
                var faces =
      from f in _context.Faces
      group f by f.OwnerId into g
      select new
      {
          OwnerId = g.Key,
          FacePhoto = g.Select(f => new Face { Id = f.Id, BlobURI = f.BlobURI })
      };

                var result = _context.Persons
                               .Join(faces,
                                 p => p.Id,
                                 f => f.OwnerId,
                                 (p, f) => new { p, f })
                               .Select(pf =>
                                 new Person
                                 {
                                     Id = pf.p.Id,
                                     Name = pf.p.Name,
                                     BirthDate = pf.p.BirthDate,
                                     MissingDate = pf.p.MissingDate,
                                     AdditionalInfo = pf.p.AdditionalInfo,
                                     FacePhotos = pf.f.FacePhoto,
                                     CreatorId = pf.p.CreatorId
                                 }
                              );

                var persons = _userManager.Users.
                    GroupJoin(result,
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
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
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
                ICollection<Face> faceBlobs = await cloudBlobService.UploadFaceBlobAsync(id, id.ToString() + _containerNamePrefix, person.Photos);

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
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
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
                ICollection<Face> faceBlobs = await cloudBlobService.UploadFaceBlobAsync(created.Entity.Id, created.Entity.Id.ToString() + _containerNamePrefix, person.Photos);

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
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
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
                var deletedPerson = _context.Persons.Remove(person);

                if (deletedPerson.Entity == null)
                    return BadRequest("Error while deleting.");

                ICloudBlobService cloudBlobService = new CloudBlobService();
                await cloudBlobService.DeleteFaceBlobAsync(id.ToString() + "-container");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
            return Ok(true);
        }
    }
}
 
 