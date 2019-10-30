﻿using Microsoft.EntityFrameworkCore;
using ServiceRepoDemo.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRepoDemo.Data.SqlServer
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(400);

            modelBuilder.Entity<User>()
                .Property(t => t.PasswordHash)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}
