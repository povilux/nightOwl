using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NightOwl.WebService.Models;

namespace NightOwl.WebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/Users/Get
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userManager.Users.ToList());
        }


        // GET: api/Users/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET: api/Users/GetUserByName/test
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByNameAsync([FromRoute]string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET: api/Users/GetUserByEmail/test@test.gmail.com
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync([FromRoute]string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/Users/Put/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User user, [FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _userManager.FindByIdAsync(id.ToString());

            if (userExists == null)
                return NotFound();

            var updatedPerson = await _userManager.UpdateAsync(user);

            if (!updatedPerson.Succeeded)
                return BadRequest(updatedPerson.Errors);

            return Ok(user);
        }


        // POST: api/Users/Post/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPerson = await _userManager.CreateAsync(user, user.PasswordHash);
       
            if (!createdPerson.Succeeded)
                return BadRequest(createdPerson.Errors);

            return Ok(user);
        }

        // DELETE: api/Users/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return NotFound();

            var deletedPerson = await _userManager.DeleteAsync(user);

            if (!deletedPerson.Succeeded )
                return BadRequest(deletedPerson.Errors);

            return Ok(user);
        }
    }
}