using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NightOwl.WebService.DAL;

namespace NightOwl.WebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacesController : ControllerBase
    {
        private const string _containerNamePrefix = "-container";

        private readonly DatabaseContext _context;

        public FacesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Faces/GetByPersonId
        [HttpGet("{personId}")]
        public IActionResult GetByPersonId([FromRoute]int personId)
        {
            try
            {
                var faces = _context.Faces.Where(f => f.OwnerId == personId).DefaultIfEmpty();

                if (faces == null)
                    return NotFound();

                return Ok(faces);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }
    }
}