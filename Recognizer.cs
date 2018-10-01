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
        public static List<String> names = new List<String>();

        public static void PrepareInformation()
        {
            if(Directory.Exists(Application.StartupPath + "/data"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/data");
            }

            if (!File.Exists(Application.StartupPath + "/data/names.txt"))
            {
                var newFile = File.Create(Application.StartupPath + "/data/names.txt");
                newFile.Close();
            }

            // read names from file to List<String>
            using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    names.Add(sr.ReadLine());
                }
            }
        }
        public static List<String> GetNamesArray()
        {
            return names;
        }

        public static EigenFaceRecognizer NewEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer();
            eigenRec.Write(Application.StartupPath + "/data/recognizer.yaml");
            return eigenRec;
        }

        public static EigenFaceRecognizer OldEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer();
            if (File.Exists(Application.StartupPath + "/data/recognizer.yaml"))
            {
                eigenRec.Read(Application.StartupPath + "/data/recognizer.yaml");
            }
            return eigenRec;
        }

        public static bool TrainRecognizer(EigenFaceRecognizer rec, Image<Bgr, byte>[] faceArray, int[] labelArray)
        {
            if(faceArray.Length != labelArray.Length)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < faceArray.Length; i++)
                {
                    faceArray[i] = ImageHandler.ResizeImage(faceArray[i]);
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
            var result = eigen.Predict(image);
            return result.Label;
        }

    }
}
