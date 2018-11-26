using NightOwl.WindowsForms.Properties;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;


namespace NightOwl.WindowsForms.Components
{
    public class Person
    {
        private static int Count;

        public int ID { get; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }
        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public string LastSeenDate { get; set; }
        public int CreatorID { get; private set; }

        public Person( int CreatorID, string Name, string BirthDate, string MissingDate, string AdditionalInfo="-", double CoordX=0.0, double CoordY=0.0, string LastSeenDate="-")
        {
            ID = ++Count;
            this.CreatorID = CreatorID;
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.MissingDate = MissingDate;
            this.AdditionalInfo = AdditionalInfo;
            this.CoordX = CoordX;
            this.CoordY = CoordY;
            this.LastSeenDate = LastSeenDate;

           /* string faceFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
     Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath + Name + "/";

            if (!Directory.Exists(faceFile))
            {
                Directory.CreateDirectory(faceFile);
            }
            repImage.Save(faceFile + "rep.bmp");*/
        }

        public override string ToString() { return Name; }

    }

    public class Person2 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string MissingDate { get; set; }

        public string AdditionalInfo { get; set; }

        [Required]
        public User Creator { get; private set; }

        public Person2(User Creator, string Name, string BirthDate, string MissingDate, string AdditionalInfo = "-")
        {
            this.Creator = Creator;
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.MissingDate = MissingDate;
            this.AdditionalInfo = AdditionalInfo;
        }


    }
    }
