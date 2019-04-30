﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timeify.Infrastructure.Context.App;

namespace Timeify.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190430030822_AddUserTaskAssignment")]
    partial class AddUserTaskAssignment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Timeify.Core.Entities.JobEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerId");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Timeify.Core.Entities.JobTaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTime>("FinishDate");

                    b.Property<int>("JobEntityId");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("JobEntityId");

                    b.ToTable("JobTasks");
                });

            modelBuilder.Entity("Timeify.Core.Entities.JobTaskUserEntity", b =>
                {
                    b.Property<int>("JobTaskId");

                    b.Property<string>("UserName");

                    b.HasKey("JobTaskId", "UserName");

                    b.HasIndex("UserName");

                    b.ToTable("JobTaskUserLink");
                });

            modelBuilder.Entity("Timeify.Core.Entities.RefreshTokenEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Expires");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("RemoteIpAddress");

                    b.Property<string>("Token");

                    b.Property<int?>("UserEntityId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Timeify.Core.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("FirstName");

                    b.Property<string>("IdentityId");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Timeify.Core.Entities.JobTaskEntity", b =>
                {
                    b.HasOne("Timeify.Core.Entities.JobEntity")
                        .WithMany("JobTasks")
                        .HasForeignKey("JobEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Timeify.Core.Entities.JobTaskUserEntity", b =>
                {
                    b.HasOne("Timeify.Core.Entities.JobTaskEntity", "JobTaskEntity")
                        .WithMany("AssignedUsers")
                        .HasForeignKey("JobTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Timeify.Core.Entities.UserEntity", "UserEntity")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("UserName")
                        .HasPrincipalKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Timeify.Core.Entities.RefreshTokenEntity", b =>
                {
                    b.HasOne("Timeify.Core.Entities.UserEntity")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}