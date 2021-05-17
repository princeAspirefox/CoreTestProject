using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityInCore.Models;

namespace IdentityInCore.Models
{
    public class UserContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        /// <summary>
        /// Initializing _options of type DbContextOptions
        /// </summary>
        /// <param name="options">type DbContextOptions</param>
        public UserContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        /// <summary>
        /// overriding OnModelCreating fn  
        /// </summary>
        /// <param name="modelBuilder"> type ModelBuilder//define shape of your entity </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<IdentityInCore.Models.UserDetails> UserDetails { get; set; }
        
        
    }
}
