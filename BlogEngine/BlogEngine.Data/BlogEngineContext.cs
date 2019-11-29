using BlogEngine.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data
{
    public class BlogEngineContext : DbContext
    {
        public BlogEngineContext(DbContextOptions<BlogEngineContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }        
    }    
}
