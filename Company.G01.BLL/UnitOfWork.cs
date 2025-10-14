using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepartmentRepository departmentRepository {  get;  }

        public IEmployeeRepository employeeRepository { get; }

        public UnitOfWork(CompanyDbContext Context)
        {
            _context = Context;
            departmentRepository=new DepartmentRepository(_context);  
            employeeRepository= new EmployeeReopsitory(_context);
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
