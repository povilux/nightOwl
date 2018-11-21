using System;
using System.Linq;
using System.Web.Http;
using PersonRecognitionService.Components;
using PersonRecognitionService.Services;

namespace PersonRecognitionService.Controllers
{
    public class FacesController : ApiController
    {        
        public FacesController()
        {
        }

        // POST: api/Faces/Train
        [HttpPost]
        public IHttpActionResult Train([FromBody]Trainer trainer)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));

            try
            {
                IFaceRecognitionService faceRecognitionService = new FaceRecognitionService(trainer.Data, trainer.NumOfComponents, trainer.Threshold);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        // POST: api/Faces/RecognizeFace
        [HttpPost]
        public  IHttpActionResult RecognizeFace([FromBody]byte[] face)
        {
            try { 
                IFaceRecognitionService faceRecognitionService = new FaceRecognitionService();

                string name = faceRecognitionService.RecognizeFace(face);
                return Ok(name);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}