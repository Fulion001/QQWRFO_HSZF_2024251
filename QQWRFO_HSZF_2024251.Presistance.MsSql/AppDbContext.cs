﻿using Microsoft.EntityFrameworkCore;
using QQWRFO_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWRFO_HSZF_2024251.Presistance.MsSql
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Person>        People          { get; set; }
        public virtual DbSet<Transaction>   Transactions    { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=pitcher_launch;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            base.OnConfiguring(optionsBuilder);
        }
    }
}