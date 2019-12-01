
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTrBox_WebAPI.Model;

namespace MyTrBox_WebAPI.Model
{
    public class AppDbContext : IdentityDbContext<User,UserRole,Guid>
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }


        public DbSet<Artist> Artist { set; get; }
        public DbSet<Category> Category { set; get; }
        public DbSet<Song> Song { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<SongAlbum> SongAlbum { get; set; }
        public DbSet<VideoAlbum> VideoAlbum { get; set; }
    }
}
