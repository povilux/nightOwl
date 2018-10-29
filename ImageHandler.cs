using Emgu.CV;
using Emgu.CV.Structure;
using nightOwl.Data;
using nightOwl.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

        private static readonly string ImageDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
     Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath;

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
            string faceFile = ImageDataPath + name + "/";

            if (!Directory.Exists(faceFile))
            {
                Directory.CreateDirectory(faceFile);
            }
            image = ResizeImage(image);
            int picNumber = 1;
            while(File.Exists(faceFile + picNumber + ".bmp"))
            {
                picNumber++;
            }
            image.Save(faceFile + picNumber + ".bmp");
        }

        public static void SaveGrayFacetoFile(string name, Image<Gray, byte> image)
        {
            Console.WriteLine(ImageDataPath);

            string faceFile = ImageDataPath + name + "/";

            if (!Directory.Exists(faceFile))
            {
                Directory.CreateDirectory(faceFile);
            }
            image = ResizeGrayImage(image);
            int picNumber = 1;
            while (File.Exists(faceFile + picNumber + ".bmp"))
            {
                picNumber++;
            }
            image.Save(faceFile + picNumber + ".bmp");
        }

        public static Image LoadRepresentativePic(string name)             // load a person's representative picture
        {
            try
            {
                Image reppic = Image.FromFile(ImageDataPath + name + "/rep.bmp");
                return reppic;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("This person doesn't exist!");
                return null;
            }
        }

        public static void SaveRepresentativePic(Image image, string name)
        {
            string faceFile = ImageDataPath + name + "/";

            if (!Directory.Exists(faceFile))
            {
                Directory.CreateDirectory(faceFile);
            }
            image.Save(faceFile + "rep.bmp");
        }

        public static int[] GetLabelArrayFromFiles()
        {
            int[] labels;
            /*List<string> names = new List<string>();
            string personName = "";

            foreach (var person in DataManagement.GetInstance().GetPersonsCatalog())
            {
                personName = person.Name;
                personName = personName.Replace(" ", "_");
                names.Add(personName);
            }*/
  
            /*using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                string line;
                string[] splitedLine;

                while (sr.Peek() >= 0)
                {
                    line = sr.ReadLine();
                    splitedLine = line.Split("|".ToCharArray(), StringSplitOptions.None);
                    splitedLine[0] = splitedLine[0].Replace(" ", "_");
                    names.Add(splitedLine[0]);
                }
            }*/
            int labelNumber = 1;
            List<int> labelList = new List<int>();

            string name = "";

            foreach (Person person in DataManagement.GetInstance().GetPersonsCatalog())
            {
                name = person.Name;
                name = name.Replace(" ", "_");
                int picNumber = 1;
                while (File.Exists(ImageDataPath + name + "/" + picNumber + ".bmp"))
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

          /*  List<string> names = new List<string>();
            string personName = "";

            foreach (var person in DataManagement.GetInstance().GetPersonsCatalog())
            {
                personName = person.Name;
                personName = personName.Replace(" ", "_");
                names.Add(personName);
            }*/
            /*using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                string line;
                string[] splitedLine;

                while (sr.Peek() >= 0)
                {
                    line = sr.ReadLine();
                    splitedLine = line.Split("|".ToCharArray(), StringSplitOptions.None);
                    splitedLine[0] = splitedLine[0].Replace(" ", "_");
                    names.Add(splitedLine[0]);
                }
            }*/
            string name = "";

            foreach (Person person in DataManagement.GetInstance().GetPersonsCatalog())
            {
                name = person.Name;
                name = name.Replace(" ", "_");

                int picNumber = 1;
                while (File.Exists(ImageDataPath + name + "/" + picNumber + ".bmp"))
                {
                    faceList.Add(new Image<Bgr, byte>(ImageDataPath + name + "/" + picNumber + ".bmp"));
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

            string name = "";

            foreach (Person person in DataManagement.GetInstance().GetPersonsCatalog())
            {
                name = person.Name;
                name = name.Replace(" ", "_");

                int picNumber = 1;
                while (File.Exists(ImageDataPath + name + "/" + picNumber + ".bmp"))
                {
                    faceList.Add(new Image<Gray, byte>(ImageDataPath + name + "/" + picNumber + ".bmp"));
                    picNumber++;
                }
            }

            faceArray = faceList.ToArray();
            return faceArray;
        }

        public static Image<Bgr, byte> GetFaceFromImage(Image<Bgr, byte> image)
        {
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(ImageDataPath + Settings.Default.FaceInformationFilePath);
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

        /*public static void WriteDataToFile(List<Person> persons)
        {
            if (File.Exists(Application.StartupPath + "/data/names.txt"))
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "/data/names.txt"))
                {
                    var personsQuery =
                        from p in persons
                        select new { p.Name, p.BirthDate, p.MissingDate, p.CoordX, p.CoordY, p.LastSeenDate, p.AdditionalInfo };

                    string line = "";

                    foreach (var person in personsQuery)
                    {
                        line = "{0}|{1}|{2}|{3}|{4}|{5}|{6}";
                        line = String.Format(line, person.Name, person.BirthDate, person.MissingDate, person.CoordX, person.CoordY, person.LastSeenDate, person.AdditionalInfo);
                        sw.WriteLine(line);
                    }
                }
            }
        }*/
        


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
