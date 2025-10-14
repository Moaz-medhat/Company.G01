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
    public class EmployeeReopsitory : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeReopsitory(CompanyDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
           return await _context.employees.Include(e=>e.department).Where(e=>e.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }


        //private readonly CompanyDbContext _context;

        //public EmployeeReopsitory(CompanyDbContext context)
        //{
        //    _context = context;
        //}



        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.employees.ToList();
        //}
        //public Employee? Get(int id)
        //{
        //    return _context.employees.Find(id);
        //}
        //public int add(Employee model)
        //{
        //    _context.employees.Add(model);
        //    return _context.SaveChanges();
        //}
        //public int update(Employee model)
        //{
        //    _context.employees.Update(model);
        //    return _context.SaveChanges();
        //}

        //public int delete(Employee model)
        //{
        //    _context.employees.Remove(model);
        //    return _context.SaveChanges();
        //}




    }
}
