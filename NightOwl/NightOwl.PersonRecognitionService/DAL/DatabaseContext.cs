using NightOwl.PersonRecognitionService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NightOwl.PersonRecognitionService.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { }

        public DbSet<FaceBlob> FaceBlobs { get; set; }
    }
}