using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tools.DataBase.MYSQL
{
    public partial class Blob_ToolsContext : DbContext
    {
        public Blob_ToolsContext()
        {
        }

        public Blob_ToolsContext(DbContextOptions<Blob_ToolsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlueToothUser> BlueToothUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserMessage> UserMessages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=karry.org.cn;database=Blob_Tools;uid=root;password=x14qkrsn", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<BlueToothUser>(entity =>
            {
                entity.HasKey(e => e.BtuId)
                    .HasName("PRIMARY");

                entity.ToTable("blueTooth_User");

                entity.Property(e => e.BtuId)
                    .HasMaxLength(32)
                    .HasColumnName("btuId");

                entity.Property(e => e.BlueToothAddress)
                    .HasMaxLength(50)
                    .HasColumnName("blueToothAddress");

                entity.Property(e => e.BlueToothName)
                    .HasMaxLength(100)
                    .HasColumnName("blueToothName");

                entity.Property(e => e.BlueToothType)
                    .HasMaxLength(50)
                    .HasColumnName("blueToothType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createdTime");

                entity.Property(e => e.Uid)
                    .HasMaxLength(32)
                    .HasColumnName("uid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.UId)
                    .HasMaxLength(32)
                    .HasColumnName("uId");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createdTime");

                entity.Property(e => e.UAccount)
                    .HasMaxLength(32)
                    .HasColumnName("uAccount");

                entity.Property(e => e.UAvatar)
                    .HasMaxLength(250)
                    .HasColumnName("uAvatar");

                entity.Property(e => e.UEmail)
                    .HasMaxLength(32)
                    .HasColumnName("uEmail");

                entity.Property(e => e.UIntroduce)
                    .HasMaxLength(500)
                    .HasColumnName("uIntroduce");

                entity.Property(e => e.UIp)
                    .HasMaxLength(100)
                    .HasColumnName("uIP");

                entity.Property(e => e.UIpAddress)
                    .HasMaxLength(100)
                    .HasColumnName("uIpAddress");

                entity.Property(e => e.UName)
                    .HasMaxLength(50)
                    .HasColumnName("uName");

                entity.Property(e => e.UPassword)
                    .HasMaxLength(32)
                    .HasColumnName("uPassword");

                entity.Property(e => e.UPhone)
                    .HasMaxLength(20)
                    .HasColumnName("uPhone");

                entity.Property(e => e.USex)
                    .HasColumnName("uSex")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.HasKey(e => e.UmId)
                    .HasName("PRIMARY");

                entity.ToTable("userMessages");

                entity.Property(e => e.UmId)
                    .HasMaxLength(32)
                    .HasColumnName("umId");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createdTime");

                entity.Property(e => e.MainInfo)
                    .HasMaxLength(100)
                    .HasColumnName("mainInfo");

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .HasColumnName("message");

                entity.Property(e => e.Target)
                    .HasMaxLength(50)
                    .HasColumnName("target");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
