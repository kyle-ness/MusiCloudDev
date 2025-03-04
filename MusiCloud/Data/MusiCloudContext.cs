﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusiCloud.Models;

namespace MusiCloud.Data
{
    public class MusiCloudContext : DbContext
    {
        public MusiCloudContext (DbContextOptions<MusiCloudContext> options)
            : base(options)
        {
        }

        public DbSet<MusiCloud.Models.User> User { get; set; }

        public DbSet<MusiCloud.Models.Playlist> Playlist { get; set; }

        public DbSet<MusiCloud.Models.Artist> Artist { get; set; }

        public DbSet<MusiCloud.Models.Concert> Concert { get; set; }

        public DbSet<MusiCloud.Models.Song> Song { get; set; }

        public DbSet<MusiCloud.Models.SongToPlaylist> SongToPlaylist { get; set; }

        public DbSet<MusiCloud.Models.Admin> Admin { get; set; }

        public DbSet<MusiCloud.Models.Album> Album { get; set; }

    }
}
