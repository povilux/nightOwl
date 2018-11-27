using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Http;
using Emgu.CV;
using Emgu.CV.Structure;
using NightOwl.PersonRecognitionService.Components;
using NightOwl.PersonRecognitionService.Services;
using NightOwl.PersonRecognitionWebService.Extensions;

namespace NightOwl.PersonRecognitionService.Controllers
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
                return BadRequest(ex.Message);
            }
            return Ok("Success");
        }

        // POST: api/Faces/RecognizeFace
        [HttpPost]
        public  IHttpActionResult Recognize([FromBody]byte[] photoByteArray)
        {
            try {
                IFaceRecognitionService faceRecognitionService = new FaceRecognitionService();

                string name = faceRecognitionService.RecognizeFace(photoByteArray);
                return Ok(name);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Detect([FromBody]byte[] photo)
        {
            try
            {
                IFaceDetectionService faceDetectionService = new FaceDetectionService();
                return Ok(faceDetectionService.DetectFaces(photo.ByteArrayToImage()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}