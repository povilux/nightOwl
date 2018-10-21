using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Face;
using System.IO;
using System.Drawing;
using Emgu.CV.Structure;

namespace nightOwl
{
    public class Recognizer
    {
        public static EigenFaceRecognizer NewEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, double.PositiveInfinity);
            eigenRec.Write(Application.StartupPath + "/data/recognizer.yaml");
            return eigenRec;
        }

        public static EigenFaceRecognizer OldEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, double.PositiveInfinity);
            if (File.Exists(Application.StartupPath + "/data/recognizer.yaml"))
            {
                eigenRec.Read(Application.StartupPath + "/data/recognizer.yaml");
            } else
            {
                eigenRec = NewEigen();
            }
            return eigenRec;
        }

        public static bool TrainRecognizer(EigenFaceRecognizer rec, Image<Gray, byte>[] faceArray, int[] labelArray)
        {
            if(faceArray.Length != labelArray.Length)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < faceArray.Length; i++)
                {
                    faceArray[i] = ImageHandler.ResizeGrayImage(faceArray[i]);
                }
                rec.Train(faceArray, labelArray);
                SaveRecognizer(rec);
                return true;
            }
        }

        public static void SaveRecognizer(EigenFaceRecognizer rec)
        {
            rec.Write(Application.StartupPath + "/data/recognizer.yaml");
        }

        public static int RecognizeFace(Image<Gray, byte> image)
        {
            image = ImageHandler.ResizeGrayImage(image);
            EigenFaceRecognizer eigen = OldEigen();

            try
            {
                var result = eigen.Predict(image);
                return result.Label;
            } catch(Emgu.CV.Util.CvException ex)
            {
                throw;
            }
        }

    }
}
