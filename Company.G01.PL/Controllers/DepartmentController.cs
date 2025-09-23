using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
      private readonly  IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedepartmentDto createdepartment)
        {
        
            if(ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = createdepartment.Code,
                    Name = createdepartment.Name,
                    CreateAt = createdepartment.CreateAt
                };
                var count= _departmentRepository.add(department);
                if (count>0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createdepartment);
        }
    }
}
