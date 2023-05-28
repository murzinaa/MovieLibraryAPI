﻿// <auto-generated />
using FilmsLibrary.SqlRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FilmsLibrary.SqlRepository.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20230122130348_DeletedAgeColumn")]
    partial class DeletedAgeColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FilmsLibrary.Models.Sql.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Actor");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Orlando",
                            Surname = "Bloom"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Johnny",
                            Surname = "Depp"
                        });
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.ActorMovie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("MovieId");

                    b.ToTable("ActorMovie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActorId = 1,
                            MovieId = 1
                        },
                        new
                        {
                            Id = 2,
                            ActorId = 2,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Blacksmith Will Turner teams up with eccentric pirate Captain Jack Sparrow to save his love, the governor's daughter, from Jack's former pirate allies, who are now undead.",
                            GenreId = 0,
                            Rating = 80,
                            ReleaseYear = 2003,
                            Title = "Pirates of the Caribbean: The Curse of the Black Pearl"
                        });
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.ActorMovie", b =>
                {
                    b.HasOne("FilmsLibrary.Models.Sql.Actor", "Actor")
                        .WithMany("ActorMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmsLibrary.Models.Sql.Movie", "Movie")
                        .WithMany("ActorMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.Comment", b =>
                {
                    b.HasOne("FilmsLibrary.Models.Sql.Movie", "Movie")
                        .WithMany("Comments")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.Actor", b =>
                {
                    b.Navigation("ActorMovies");
                });

            modelBuilder.Entity("FilmsLibrary.Models.Sql.Movie", b =>
                {
                    b.Navigation("ActorMovies");

                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
