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
        public async Task<IHttpActionResult> Train([FromBody]int threshold)
        {
            if (threshold <= 0)
                return BadRequest("No threshold");

            try
            {
                IFaceRecognitionService faceRecognitionService = new FaceRecognitionService(threshold);
                bool success = await faceRecognitionService.TrainRecognizer();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString() + Environment.NewLine + ex.InnerException.ToString());
            }
            return Ok("Success");
        }

        // POST: api/Faces/RecognizeFace
        [HttpPost]
        public async Task<IHttpActionResult> Recognize([FromBody]byte[] photoByteArray)
        {
            try {
                IFaceRecognitionService faceRecognitionService = new FaceRecognitionService();

                IEnumerable<Person> persons = await faceRecognitionService.RecognizeFace(photoByteArray);

                return Ok(persons);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString() + Environment.NewLine + ex.InnerException.ToString());
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
                return BadRequest(ex.Message.ToString() + Environment.NewLine + ex.InnerException.ToString());
            }
        }


    }
}