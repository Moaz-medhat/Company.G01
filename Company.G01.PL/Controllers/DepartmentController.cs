using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
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

            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = createdepartment.Code,
                    Name = createdepartment.Name,
                    CreateAt = createdepartment.CreateAt
                };
                var count = _departmentRepository.add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createdepartment);
        }

        [HttpGet]
        public IActionResult Details(int? id,string viewName="Details")
        {
            if (id is null) return BadRequest("Invalid Id");

          var department=  _departmentRepository.Get(id.Value);
            if (department is null ) return NotFound(new { StatusCode = 404, message=$"department with id : {id} not found" } );

            return View(viewName,department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"department with id : {id} not found" });
            var createdepartment = new CreatedepartmentDto()
            {
              
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };
            return View(createdepartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,CreatedepartmentDto createdepartment)
        {

            if (ModelState.IsValid)
            {

                //if (id != department.Id) return BadRequest();
                var department = new Department()
                {
                    Id=id,
                    Code = createdepartment.Code,
                    Name = createdepartment.Name,
                    CreateAt = createdepartment.CreateAt
                };

                var count = _departmentRepository.update(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

            }
          

            return View(createdepartment);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"department with id : {id} not found" });

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete ([FromRoute] int id, CreatedepartmentDto createdepartment)
        {

            if (ModelState.IsValid)
            {

                //if (id != department.Id) return BadRequest();
                var department = new Department()
                {
                    Id = id,
                    Code = createdepartment.Code,
                    Name = createdepartment.Name,
                    CreateAt = createdepartment.CreateAt
                };


                var count = _departmentRepository.delete(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }


            return View(createdepartment);
        }




    }
}
