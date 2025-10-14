using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();

        //Employee? Get(int id);
        //int add(Employee model);
        //int update(Employee model);
        //int delete(Employee model);

        Task<List<Employee>> GetByNameAsync(string name);
    }
}
