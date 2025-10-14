using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Data.Context;
using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>) await _context.employees.Include(e=>e.department).ToListAsync();
            }
            return  await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.employees.Include(e => e.department).FirstOrDefaultAsync(e=>e.Id==id) as T;
            }
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task addAsync(T model)
        {
           await  _context.Set<T>().AddAsync(model);
            
        }
        public void update(T model)
        {
            _context.Set<T>().Update(model);
            
        }

        public void delete(T model)
        {
            _context.Set<T>().Remove(model);
            
        }



    }
}
