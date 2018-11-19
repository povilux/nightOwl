using PersonRecognitionService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PersonRecognitionService.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Face> Faces { get; set; }
    }
}