using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NightOwl.WebService.DAL;
using NightOwl.WebService.Models;

namespace NightOwl.WebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PersonsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Persons
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Persons.ToList());
        }


        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {            
            var person = await _context.Persons.FindAsync(id);

            if(person == null)
                return NotFound();
           
            return Ok(person);
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _context.Persons.Update(person);

            if(updated.Entity == null)
                return BadRequest("Error while updating.");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest("Error while saving");
            }
            return Ok(updated.Entity);
        }

      
        // POST: api/Persons/Post/
        [HttpPost]
        public IActionResult Post([FromBody][Bind(new string[] { "Name", "BirthDate", "MissingDate", "AdditionalInfo", "CreatorID" })]Person person)
        {
            if(!ModelState.IsValid)
               return BadRequest(ModelState);
         
            var created = _context.Persons.Add(person);

            if(created.Entity == null)
                return BadRequest("Error while adding user");

            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Error while saving");
            }
            return Ok(created.Entity);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            Console.WriteLine("ID: " + id);
            var person = await _context.Persons.FindAsync(id);

            Console.WriteLine("Person: " + person.Name);

            if (person == null)
                return NotFound();
            
            var deletedPerson = _context.Persons.Remove(person);

            if (deletedPerson.Entity == null)
                return BadRequest("Error while deleting.");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Error while saving");
            }
            return Ok(deletedPerson.Entity);
        }
    }
}
 