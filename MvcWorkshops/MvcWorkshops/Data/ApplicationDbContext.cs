using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcWorkshops.Models;

namespace MvcWorkshops.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		//public DbSet<ApplicationUser> Users { get; set; }

		public DbSet<Channel> Channels { get; set; }

		public DbSet<Message> Messages { get; set; }

		public DbSet<ObservedChannel> ObservedChannels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			builder.Entity<ObservedChannel>()
				.ToTable("ObservedChannels", "dbo")
				.HasKey(uc => new { uc.ApplicationUserId, uc.ChannelId });

			builder.Entity<ObservedChannel>()
				.HasOne(pc => pc.ApplicationUser)
				.WithMany(p => p.ObservedChannels)
				.HasForeignKey(pc => pc.ApplicationUserId);

			builder.Entity<ObservedChannel>()
				.HasOne(pc => pc.Channel)
				.WithMany(c => c.ObservedChannels)
				.HasForeignKey(pc => pc.ChannelId);
		}
	}
}
