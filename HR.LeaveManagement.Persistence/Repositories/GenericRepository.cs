using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public  class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LeaveManagementDbContext _dbcontext;
         
        public GenericRepository(LeaveManagementDbContext context)
        {
            _dbcontext = context;
        }
        public async Task<T> Add(T entity)
        {
            await _dbcontext.AddAsync(entity);
          
            return entity;
        }
        public async Task Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity); 
        }
        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }
        public async Task<T> Get(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }
         
        public async  Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        } 
        public async  Task Update(T entity)
        {
             _dbcontext.Entry(entity).State = EntityState.Modified;
             
        }

      

       
    }
}
