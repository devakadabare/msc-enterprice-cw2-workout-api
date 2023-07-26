using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorkoutApi.Data;
using WorkoutApi.Entities;
using Microsoft.EntityFrameworkCore;
using WorkoutApi.DTO;
using System.Xml.Linq;

namespace WorkoutApi.Services
{
    public class WorkoutPlanService
    {
        private readonly StoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WorkoutPlanService(StoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<WorkoutPlan>> GetWorkoutPlansAsync()
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string workoutPlanXmlPath = Path.Combine(contentRootPath, "Data/XML/WorkoutPlan.xml");

            XDocument doc = XDocument.Load(workoutPlanXmlPath);
            var workoutPlans = doc.Root
                .Elements("workoutPlan")
                .Select(wp => new WorkoutPlan
                {
                    Id = (int)wp.Element("Id"),
                    Name = (string)wp.Element("Name"),
                    Difficulty = (string)wp.Element("Difficulty"),
                    TotalMET = (double)wp.Element("TotalMET"),
                })
                .ToList();
            return await Task.FromResult(workoutPlans);
        }
        public async Task<List<WorkoutPlan>> GetWorkoutPlansWithWorkoutsAsync()
        {
            return await _context.WorkoutPlans.Include(wp => wp.WorkoutPlanItems).ThenInclude(wpi => wpi.Workout).ToListAsync();

        }

        public async Task<WorkoutPlan> GetWorkoutPlanByIdAsync(int id)
        {
            return await _context.WorkoutPlans.FindAsync(id);
        }
        public async Task<WorkoutPlanWithItemsDTO> GetWorkoutPlanWithWorkoutsByIdAsync(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.Include(wp => wp.WorkoutPlanItems).ThenInclude(wpi => wpi.Workout).FirstOrDefaultAsync(wp => wp.Id == id);

            if (workoutPlan == null)
                return null;

            var workoutPlanDTO = new WorkoutPlanWithItemsDTO
            {
                Id = workoutPlan.Id,
                Name = workoutPlan.Name,
                Difficulty = workoutPlan.Difficulty,
                TotalMET = workoutPlan.TotalMET,
                WorkoutPlanItems = workoutPlan.WorkoutPlanItems.Select(wpi => new WorkoutPlanItemDTO
                {
                    Id = wpi.Id,
                    WorkoutPlanId = wpi.WorkoutPlanId,
                    WorkoutId = wpi.WorkoutId,
                    Order = wpi.Order,
                    Sets = wpi.Sets,
                    Reps = wpi.Reps,
                    Rest = wpi.Rest,
                    Workout = new WorkoutDTO
                    {
                        Id = wpi.Workout.Id,
                        Name = wpi.Workout.Name,
                        Description = wpi.Workout.Description,
                        MET = wpi.Workout.MET
                    }

                }).ToList()
                
            };

            return workoutPlanDTO;
        }


        public async Task<WorkoutPlan> CreateWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Add(workoutPlan);
            await _context.SaveChangesAsync();
            return workoutPlan;
        }

        public async Task<WorkoutPlan> UpdateWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Update(workoutPlan);
            await _context.SaveChangesAsync();
            return workoutPlan;
        }

        public async Task<WorkoutPlan> DeleteWorkoutPlanAsync(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);
            _context.WorkoutPlans.Remove(workoutPlan);
            await _context.SaveChangesAsync();
            return workoutPlan;
        }

    }
}