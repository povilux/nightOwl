using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl
{

    public class ImageHandler
    {
        private const int imageDimension = 100;       // default image dimension for EigenRecognizer 

        public static Image<Bgr, byte> ResizeImage(Image<Bgr, byte> image)
        {
            Image<Bgr, byte> resizedImage = image.Resize(imageDimension, imageDimension, Emgu.CV.CvEnum.Inter.Cubic);
            return resizedImage;
        }

        public static Image<Gray, byte> ResizeGrayImage(Image<Gray, byte> image)
        {
            Image<Gray, byte> resizedImage = image.Resize(imageDimension, imageDimension, Emgu.CV.CvEnum.Inter.Cubic);
            return resizedImage;
        }

        public static void SaveFacetoFile(string name, Image<Bgr, byte> image)
        {
            if (!Directory.Exists(Application.StartupPath + "/data/" + name + "/"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/data/" + name + "/");
            }
            image = ResizeImage(image);
            int picNumber = 1;
            while(File.Exists(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"))
            {
                picNumber++;
            }
            image.Save(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp");
        }

        public static void SaveGrayFacetoFile(string name, Image<Gray, byte> image)
        {
            if (!Directory.Exists(Application.StartupPath + "/data/" + name + "/"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/data/" + name + "/");
            }
            image = ResizeGrayImage(image);
            int picNumber = 1;
            while (File.Exists(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"))
            {
                picNumber++;
            }
            image.Save(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp");
        }

        public static Image LoadRepresentativePic(string name)             // load a person's representative picture
        {
            try
            {
                Image reppic = Image.FromFile(Application.StartupPath + "/data/" + name + "/rep.bmp");
                return reppic;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Šis asmuo neegzistuoja duomenų bazėje!");
                return null;
            }
        }

        public static void SaveRepresentativePic(Image image, string name)
        {
            if (!Directory.Exists(Application.StartupPath + "/data/" + name + "/"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/data/" + name + "/");
            }
            image.Save(Application.StartupPath + "/data/" + name + "/rep.bmp");
        }

        public static int[] GetLabelArrayFromFiles()
        {
            int[] labels;
            List<string> names = new List<string>();
            using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    names.Add(sr.ReadLine());
                }
            }
            int labelNumber = 1;
            List<int> labelList = new List<int>();

            foreach (string name in names)
            {
                int picNumber = 1;
                while (File.Exists(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"))
                {
                    picNumber++;
                    labelList.Add(labelNumber);
                }
                labelNumber++;
            }
            labels = labelList.ToArray();
            return labels;
        }

        public static Image<Bgr, byte>[] GetFaceArrayFromFiles()
        {
            Image<Bgr, byte>[] faceArray;
            List<Image<Bgr, byte>> faceList = new List<Image<Bgr, byte>>();

            List<string> names = new List<string>();
            using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    names.Add(sr.ReadLine());
                }
            }
            foreach(string name in names)
            {
                int picNumber = 1;
                while (File.Exists(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"))
                {
                    faceList.Add(new Image<Bgr, byte>(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"));
                    picNumber++;
                }
            }

            faceArray = faceList.ToArray();
            return faceArray;
        }

        public static Image<Gray, byte>[] GetGrayFaceArrayFromFiles()
        {
            Image<Gray, byte>[] faceArray;
            List<Image<Gray, byte>> faceList = new List<Image<Gray, byte>>();

            List<string> names = new List<string>();
            using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    names.Add(sr.ReadLine());
                }
            }
            foreach (string name in names)
            {
                int picNumber = 1;
                while (File.Exists(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"))
                {
                    faceList.Add(new Image<Gray, byte>(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"));
                    picNumber++;
                }
            }

            faceArray = faceList.ToArray();
            return faceArray;
        }

        public static Image<Bgr, byte> GetFaceFromImage(Image<Bgr, byte> image)
        {
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
            var grayImage = image.Convert<Gray, byte>();
            var faces = _cascadeClassifier.DetectMultiScale(grayImage, 1.1, 10, Size.Empty);
            if(faces.Length != 1)
            {
                return null;
            } else
            {
                Image<Bgr, byte> faceImage = image.Copy(faces[0]);
                return faceImage;
            }
        }

        public static void WriteNamesToFile(List<string> names)
        {
            if (File.Exists(Application.StartupPath + "/data/names.txt"))
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "/data/names.txt"))
                {
                    foreach(string name in names)
                    {
                        sw.WriteLine(name);
                    }
                }
            }
        }
        

        /* 
         * 
        public List<int> GetLabelList()
        {
            List<int> labelList = new List<int>();
            List<string> names = new List<string>();
            using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    names.Add(sr.ReadLine());
                }
            }
            foreach(string name in names)
            {
                int count = 0;
                int picNumber = 1;
                while(File.Exists(Application.StartupPath + "/data/" + name + "/" + picNumber + ".bmp"))
                {
                    picNumber++;
                    count++;
                }
                labelList.Add(count);
            }
            return labelList;
        }
        */

        // to do later:
        // public static SaveFaceToDB()
    }
}
