using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Context
{
    public class FundoContext : DbContext
    {

        public FundoContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<NotesEntity> NotesTable { get; set; }
        public DbSet<CollabEntity> CollabTable { get; set; }

    }
}
