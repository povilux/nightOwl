using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Emgu.CV;
using Emgu.CV.Structure;
using NightOwl.PersonRecognitionService.Components;
using NightOwl.PersonRecognitionService.DAL;
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
        public async Task<IHttpActionResult> Train([FromBody]Trainer trainer)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));

            try
            {
                IFaceRecognitionService faceRecognitionService = new FaceRecognitionService(null, trainer.NumOfComponents, trainer.Threshold);

               // trainer.Data = await faceRecognitionService.LoadFacesAsync(trainer.Data);
                bool success = await faceRecognitionService.TrainRecognizer();
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
                IFaceRecognitionService faceRecognitionService = new FaceRecognitionService(null);

               // IEnumerable<int> personsId = faceRecognitionService.RecognizeFace(photoByteArray);
                return Ok(3);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Detect([FromBody]byte[] photo)
        {
            if (photo == null)
                return BadRequest("No picture");

            try
            {                
                IFaceDetectionService faceDetectionService = new FaceDetectionService();

                Image<Gray, byte> facePhoto = faceDetectionService.DetectFaceAsGrayImage(photo.ByteArrayToImage());
                return Ok(facePhoto.ToBitmap().ImageToByteArray());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}