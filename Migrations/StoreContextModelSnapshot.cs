﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutApi.Data;

#nullable disable

namespace WorkoutApi.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WorkoutApi.Entities.CheatMeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Calories")
                        .HasColumnType("double");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CheatMeals");
                });

            modelBuilder.Entity("WorkoutApi.Entities.Prediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Predictions");
                });

            modelBuilder.Entity("WorkoutApi.Entities.UserWorkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CaloriesBurned")
                        .HasColumnType("int");

                    b.Property<int?>("Count")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("UserWorkout");
                });

            modelBuilder.Entity("WorkoutApi.Entities.UserWorkoutEnrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompletedDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutPlanId");

                    b.ToTable("UserWorkoutEnrollments");
                });

            modelBuilder.Entity("WorkoutApi.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("MET")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutApi.Entities.WorkoutPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("TotalMET")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("WorkoutApi.Entities.WorkoutPlanItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Rest")
                        .HasColumnType("int");

                    b.Property<int>("Sets")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.HasIndex("WorkoutPlanId");

                    b.ToTable("WorkoutPlanItems");
                });

            modelBuilder.Entity("WorkoutApi.Entities.UserWorkout", b =>
                {
                    b.HasOne("WorkoutApi.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("WorkoutApi.Entities.UserWorkoutEnrollment", b =>
                {
                    b.HasOne("WorkoutApi.Entities.WorkoutPlan", "WorkoutPlan")
                        .WithMany("UserWorkoutEnrollment")
                        .HasForeignKey("WorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("WorkoutApi.Entities.WorkoutPlanItems", b =>
                {
                    b.HasOne("WorkoutApi.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApi.Entities.WorkoutPlan", "WorkoutPlan")
                        .WithMany("WorkoutPlanItems")
                        .HasForeignKey("WorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("WorkoutApi.Entities.WorkoutPlan", b =>
                {
                    b.Navigation("UserWorkoutEnrollment");

                    b.Navigation("WorkoutPlanItems");
                });
#pragma warning restore 612, 618
        }
    }
}