
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Face;
using System.IO;
using Emgu.CV.Structure;
using System.Configuration;

namespace nightOwl
{
    public class Recognizer
    {
        //  private static readonly int threshold = 10000;
        // higher threshold - more chances to recognize a face (sometimes incorrectly);

        private static readonly string RecognizerDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + 
            ConfigurationManager.AppSettings["DataFolderPath"] + ConfigurationManager.AppSettings["RecognizerFilePath"];

        public static EigenFaceRecognizer NewEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, 4000);
            eigenRec.Write(RecognizerDataPath);
            return eigenRec;
        }

        public static EigenFaceRecognizer OldEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, 4000);
            if (File.Exists(RecognizerDataPath))
            {
                try
                {
                    eigenRec.Read(RecognizerDataPath);
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

        public static bool TrainRecognizer()
        {
            EigenFaceRecognizer eigen = NewEigen();

            Image<Gray, byte>[] faceArray = ImageHandler.GetGrayFaceArrayFromFiles();
            int[] labelArray = ImageHandler.GetLabelArrayFromFiles();

            if (faceArray.Length != labelArray.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < faceArray.Length; i++)
                {
                    faceArray[i] = ImageHandler.ResizeGrayImage(faceArray[i]);
                }
                eigen.Train(faceArray, labelArray);
                SaveRecognizer(eigen);
                return true;
            }
        }
      

        public static void SaveRecognizer(EigenFaceRecognizer rec)
        {
            rec.Write(RecognizerDataPath);
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
