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
        private static readonly int threshold = 10000;
        // higher threshold - more chances to recognize a face (sometimes incorrectly);

        public static EigenFaceRecognizer NewEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, 4000);
            eigenRec.Write(Application.StartupPath + "/data/recognizer.yaml");
            return eigenRec;
        }

        public static EigenFaceRecognizer OldEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, 4000);
            if (File.Exists(Application.StartupPath + "/data/recognizer.yaml"))
            {
                try
                {
                    eigenRec.Read(Application.StartupPath + "/data/recognizer.yaml");
                }
                catch
                {

                }
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
            EigenFaceRecognizer.PredictionResult result = eigen.Predict(image);
            
            /*
            if(result.Distance > threshold)
            {
                return result.Label;
            } else
            {
                return 0;
            }
            */        

            return result.Label;
        }

    }
}
