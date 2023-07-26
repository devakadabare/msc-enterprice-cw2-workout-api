using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApi.Data;
using WorkoutApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WorkoutApi.Services
{
    public class CheatMealService
    {
        private readonly StoreContext _context;

        public CheatMealService(StoreContext context)
        {
            _context = context;
        }


        public async Task<CheatMeal> GetCheatMealByIdAsync(int id)
        {
            return await _context.CheatMeals.FindAsync(id);
        }

        public async Task<CheatMeal> CreateCheatMealAsync(CheatMeal cheatMeal)
        {
            object value = _context.CheatMeals.Add(cheatMeal);
            await _context.SaveChangesAsync();
            return cheatMeal;
        }

        public async Task<CheatMeal> UpdateCheatMealAsync(CheatMeal cheatMeal)
        {
            _context.CheatMeals.Update(cheatMeal);
            await _context.SaveChangesAsync();
            return cheatMeal;
        }

        public async Task<CheatMeal> DeleteCheatMealAsync(int id)
        {
            var cheatMeal = await _context.CheatMeals.FindAsync(id);
            _context.CheatMeals.Remove(cheatMeal);
            await _context.SaveChangesAsync();
            return cheatMeal;
        }

        public async Task<ActionResult<List<CheatMeal>>> GetCheatMealsAsync()
        {
            return await _context.CheatMeals.ToListAsync();
        }
    }
}