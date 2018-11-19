using System;
using System.Threading.Tasks;
using System.Web.Http;
using PersonRecognitionService.Components;
using PersonRecognitionService.DAL;
using PersonRecognitionService.Models;
using PersonRecognitionService.Services;

namespace PersonRecognitionService.Controllers
{
    [Route("api/{controller}/{action}")]
    public class FacesController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        private FaceRecognitionService _faceRecognitionService;
        
        public FacesController()
        {
            _faceRecognitionService = new FaceRecognitionService();
        }

        // POST: api/Faces/Train
        [HttpPost]
        public IHttpActionResult Train([FromBody]Trainer trainer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _faceRecognitionService = new FaceRecognitionService(trainer.NumOfComponents, trainer.Threshold);

            try
            {
                _faceRecognitionService.Train();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            return Ok();
        }
        // GET: api/Faces/photo
        [HttpGet]
        public  IHttpActionResult RecognizeFace(Face face)
        {
            if (_faceRecognitionService == null)
                _faceRecognitionService = new FaceRecognitionService();

            string name = null;

            try
            {
                name = _faceRecognitionService.RecognizeFace(face);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

            if (name == null)
                return NotFound();

            return Ok(name);
        }

        // POST: api/Faces
        [HttpPost]
        public async Task<IHttpActionResult> PostFace([FromBody]Face face)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            face.PersonName = face.PersonName.Replace(" ", "_");
            db.Faces.Add(face);

            await db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Faces/photo
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFace([FromBody]int id)
        {
            Face foundedFace = await db.Faces.FindAsync(id);

            if (foundedFace == null)
                return NotFound();
            
            db.Faces.Remove(foundedFace);
            await db.SaveChangesAsync();

            return Ok(foundedFace);
        }
    }
}