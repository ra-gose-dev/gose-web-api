﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SharpDevelopWebApi.Models
{
    public class SDWebApiDbContext : DbContext
    {
        public SDWebApiDbContext() : base("MyAccessDb") 
        {
        }

        // Map model classes to database tables
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
       
    }


}

