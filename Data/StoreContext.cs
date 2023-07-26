using WorkoutApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApi.Data
{
    public class StoreContext: DbContext
    {
        //constructor
        public StoreContext(DbContextOptions<StoreContext> options): base(options)
        {
            
        }

        //DbSets
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<UserWorkout> UserWorkout { get; set; }
        public DbSet<UserWorkoutEnrollment> UserWorkoutEnrollments { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<CheatMeal> CheatMeals { get; set; }
        public DbSet<WorkoutPlanItems> WorkoutPlanItems { get; set; }

    }
}