using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Data.Context;
using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class DepartmentRepository :IDepartmentRepository
    {

        private readonly CompanyDbContext _db;

        public DepartmentRepository()
        {
            _db = new CompanyDbContext();
        }
        public IEnumerable<Department> GetAll()
        {
            return _db.departments.ToList();
        }


        public Department Get(int id)
        {
            return _db.departments.Find(id);
        }
        public int add(Department model)
        {
            _db.departments.Add(model);
            return _db.SaveChanges();
        }
        public int update(Department model)
        {
            _db.departments.Update(model);
            return _db.SaveChanges();
        }
        public int delete(Department model)
        {
            _db.departments.Remove(model);
            return _db.SaveChanges();
        }
    }
}
