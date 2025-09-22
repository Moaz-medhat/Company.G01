using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();

        Department? Get(int id);
        int add(Department model);
        int update(Department model);
        int delete(Department model);
    }
}
