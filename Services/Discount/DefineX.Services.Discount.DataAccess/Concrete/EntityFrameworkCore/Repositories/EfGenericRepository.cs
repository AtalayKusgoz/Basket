using DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Context;
using DefineX.Services.Discount.DataAccess.Interfaces;
using DefineX.Services.Discount.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class, ITable, new()
    {
        DefineXContext _context;
        public EfGenericRepository(DefineXContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().Where(filter).ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }
        public async Task RemoveAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity setEntity)
        {
            TEntity updateEntity = await _context.Set<TEntity>().FindAsync(setEntity.Id);
            if (updateEntity != null)
            {
                _context.Entry(updateEntity).CurrentValues.SetValues(setEntity);
                foreach (var property in _context.Entry(setEntity).Properties)
                {
                    if (property.CurrentValue == null)
                    {
                        _context.Entry(updateEntity).Property(property.Metadata.Name).IsModified = false;
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> keySelector)
        {
            return await _context.Set<TEntity>().Where(filter).OrderByDescending(keySelector).ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return await _context.Set<TEntity>().OrderByDescending(keySelector).ToListAsync();
        }
        public async Task<TEntity> FindByIdAsync(int id)
        {
            var data = await _context.FindAsync<TEntity>(id);
            return data;
        }
    }
}
