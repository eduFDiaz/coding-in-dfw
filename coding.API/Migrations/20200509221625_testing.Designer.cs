﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using coding.API.Models;

namespace coding.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200509221625_testing")]
    partial class testing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("coding.API.Models.Awards.Award", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Company")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Awards");
                });

            modelBuilder.Entity("coding.API.Models.Educations.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DateRange")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SchoolName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("coding.API.Models.Languages.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Langagues");
                });

            modelBuilder.Entity("coding.API.Models.Photos.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsMain")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PublicId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("coding.API.Models.PostTags.PostTag", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("coding.API.Models.Posts.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ReadingTime")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("coding.API.Models.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BodyText")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClientName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Industry")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProductPhoto")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProjectIntro")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("RequirementId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ShortResume")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RequirementId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("coding.API.Models.Products.ProductsRequirements.ProductRequirement", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RequirementId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("ProductId", "RequirementId");

                    b.HasIndex("RequirementId");

                    b.ToTable("ProductRequirements");
                });

            modelBuilder.Entity("coding.API.Models.Products.Requirements.Requirement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Requirements");
                });

            modelBuilder.Entity("coding.API.Models.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Resume")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("coding.API.Models.Skills.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("coding.API.Models.Tags.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("coding.API.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CodepenProfile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FacebookProfile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FullResume")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("GithubUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LinkedInProfile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RedditProfile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ShortResume")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("StackOverflowProfile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TwiterProfile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("coding.API.Models.WorkExperiences.WorkExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Company")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DateRange")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Resume")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("WorkExperiences");
                });

            modelBuilder.Entity("coding.API.Models.Photos.Photo", b =>
                {
                    b.HasOne("coding.API.Models.Users.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("coding.API.Models.PostTags.PostTag", b =>
                {
                    b.HasOne("coding.API.Models.Posts.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coding.API.Models.Tags.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("coding.API.Models.Posts.Post", b =>
                {
                    b.HasOne("coding.API.Models.Users.User", null)
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("coding.API.Models.Products.Product", b =>
                {
                    b.HasOne("coding.API.Models.Products.Requirements.Requirement", "Requirement")
                        .WithMany()
                        .HasForeignKey("RequirementId");

                    b.HasOne("coding.API.Models.Users.User", null)
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("coding.API.Models.Products.ProductsRequirements.ProductRequirement", b =>
                {
                    b.HasOne("coding.API.Models.Products.Product", "Product")
                        .WithMany("ProductRequirements")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coding.API.Models.Products.Requirements.Requirement", "Requirement")
                        .WithMany("ProductRequirements")
                        .HasForeignKey("RequirementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
