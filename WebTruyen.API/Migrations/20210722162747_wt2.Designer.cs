﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTruyen.API.Data;

namespace WebTruyen.API.Migrations
{
    [DbContext(typeof(ComicDbContext))]
    [Migration("20210722162747_wt2")]
    partial class wt2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRole");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Bookmark", b =>
                {
                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdComic")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUser", "IdComic");

                    b.HasIndex("IdComic");

                    b.ToTable("Bookmark");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Chapter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeUp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdComic")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdComic");

                    b.ToTable("Chapter");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Comic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnotherNameOfComic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Comic");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.ComicInGenre", b =>
                {
                    b.Property<int>("IdGenre")
                        .HasColumnType("int");

                    b.Property<Guid>("IdComic")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdGenre", "IdComic");

                    b.HasIndex("IdComic");

                    b.HasIndex("IdGenre")
                        .IsUnique();

                    b.ToTable("ComicInGenre");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Chapter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ComicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CommentReplyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeUp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdComic")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdCommentReply")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("CommentReplyId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.HistoryRead", b =>
                {
                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdComic")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUser", "IdComic");

                    b.HasIndex("IdComic");

                    b.ToTable("HistoryRead");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.NewComicAnnouncement", b =>
                {
                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdChapter")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ComicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUser", "IdChapter");

                    b.HasIndex("ComicId");

                    b.HasIndex("IdChapter");

                    b.ToTable("NewComicAnnouncement");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Page", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdChapter")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdChapter");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Report", b =>
                {
                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdChapter")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser", "IdChapter");

                    b.HasIndex("IdChapter");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.TranslationOfUser", b =>
                {
                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdComic")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUser", "IdComic");

                    b.HasIndex("IdComic");

                    b.ToTable("TranslationOfUser");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Fanpage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("sex")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Bookmark", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Comic", "Comic")
                        .WithMany("Bookmarks")
                        .HasForeignKey("IdComic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTruyen.API.Entities.User", "User")
                        .WithMany("Bookmarks")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Chapter", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Comic", "Comic")
                        .WithMany("Chapters")
                        .HasForeignKey("IdComic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.ComicInGenre", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Comic", "Comic")
                        .WithMany("ComicInGenres")
                        .HasForeignKey("IdComic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTruyen.API.Entities.Genre", "Genre")
                        .WithOne("ComicInGenre")
                        .HasForeignKey("WebTruyen.API.Entities.ComicInGenre", "IdGenre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Comment", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId");

                    b.HasOne("WebTruyen.API.Entities.Comment", "CommentReply")
                        .WithMany()
                        .HasForeignKey("CommentReplyId");

                    b.HasOne("WebTruyen.API.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Comic");

                    b.Navigation("CommentReply");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.HistoryRead", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Comic", "Comic")
                        .WithMany("HistoryReads")
                        .HasForeignKey("IdComic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTruyen.API.Entities.User", "User")
                        .WithMany("HistoryReads")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.NewComicAnnouncement", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Comic", null)
                        .WithMany("NewComicAnnouncements")
                        .HasForeignKey("ComicId");

                    b.HasOne("WebTruyen.API.Entities.Chapter", "Chapter")
                        .WithMany("NewComicAnnouncements")
                        .HasForeignKey("IdChapter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTruyen.API.Entities.User", "User")
                        .WithMany("NewComicAnnouncements")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Page", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Chapter", "Chapter")
                        .WithMany("Pages")
                        .HasForeignKey("IdChapter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Report", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Chapter", "Chapter")
                        .WithMany("Reports")
                        .HasForeignKey("IdChapter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTruyen.API.Entities.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.TranslationOfUser", b =>
                {
                    b.HasOne("WebTruyen.API.Entities.Comic", "Comic")
                        .WithMany("TranslationOfUsers")
                        .HasForeignKey("IdComic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTruyen.API.Entities.User", "User")
                        .WithMany("TranslationOfUsers")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Chapter", b =>
                {
                    b.Navigation("NewComicAnnouncements");

                    b.Navigation("Pages");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Comic", b =>
                {
                    b.Navigation("Bookmarks");

                    b.Navigation("Chapters");

                    b.Navigation("ComicInGenres");

                    b.Navigation("HistoryReads");

                    b.Navigation("NewComicAnnouncements");

                    b.Navigation("TranslationOfUsers");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.Genre", b =>
                {
                    b.Navigation("ComicInGenre");
                });

            modelBuilder.Entity("WebTruyen.API.Entities.User", b =>
                {
                    b.Navigation("Bookmarks");

                    b.Navigation("HistoryReads");

                    b.Navigation("NewComicAnnouncements");

                    b.Navigation("Reports");

                    b.Navigation("TranslationOfUsers");
                });
#pragma warning restore 612, 618
        }
    }
}