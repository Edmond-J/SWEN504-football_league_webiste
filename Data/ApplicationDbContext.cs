﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamPartnerWebApp.Models;

namespace TeamPartnerWebApp.Data {

    public class ApplicationDbContext : IdentityDbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public DbSet<TeamPartnerWebApp.Models.Player> Player { get; set; } = default!;
        public DbSet<TeamPartnerWebApp.Models.Team> Team { get; set; } = default!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //	base.OnModelCreating(modelBuilder);

        //	modelBuilder.Entity<Player>()
        //		.HasOne(p => p.Team) // Player 拥有一个 Team
        //		.WithMany() // Team 拥有多个 Player
        //		.HasForeignKey("TeamId"); // Player 的外键是 TeamId
        //}
    }
}