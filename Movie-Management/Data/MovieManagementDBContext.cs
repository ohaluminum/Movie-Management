﻿using Microsoft.EntityFrameworkCore;
using MovieManagement.Models;

namespace MovieManagement.Data
{
    public class MovieManagementDBContext : DbContext
    {
        public MovieManagementDBContext(DbContextOptions<MovieManagementDBContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movie { get; set; }
    }
}
