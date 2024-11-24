using System.Collections.Generic;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services
{
    public class BindingTypeService : IBindingTypeService
    {
        public async Task<List<BindingType>> GetAllAsync(){
            using ApplicationDbContext context = new();
            return await context.BindingTypes.ToListAsync();
        }

        public async Task<BindingType?> GetAsyncById(int id){
            using ApplicationDbContext context = new();
            BindingType? bindingType = await context.BindingTypes.FindAsync(id);
            return bindingType;
        }

        public async Task<BindingType> Add(BindingType bindingType)
        {
            using ApplicationDbContext context = new();
            EntityEntry<BindingType> entry = await context.BindingTypes.AddAsync(bindingType);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task Delete(int id)
        {
            using ApplicationDbContext context = new();
            var bindingType = await context.BindingTypes.FindAsync(id);
            if (bindingType is null)
                return;
            context.BindingTypes.Remove(bindingType);
            await context.SaveChangesAsync();
        }

        public async Task Update(BindingType bindingType)
        {
            using ApplicationDbContext context = new();
            context.BindingTypes.Update(bindingType);
            await context.SaveChangesAsync();
        }
    }
}