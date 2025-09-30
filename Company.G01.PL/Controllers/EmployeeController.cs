using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _EmployeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {

            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                   Name=employeeDto.Name,
                   Adress=employeeDto.Adress,
                   Age=employeeDto.Age,
                   CreateAt=employeeDto.CreateAt,
                   HiringDate=employeeDto.HiringDate,
                   Email=employeeDto.Email,
                   IsActive=employeeDto.IsActive,
                   IsDeleted=employeeDto.IsDeleted,
                   Phone=employeeDto.Phone,
                   Salary=employeeDto.Salary,
                };
                var count = _EmployeeRepository.add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employeeDto);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = _EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with id : {id} not found" });

            return View(viewName, employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"department with id : {id} not found" });

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {

            if (ModelState.IsValid)
            {

                if (id != employee.Id) return BadRequest();


                var count = _EmployeeRepository.update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }


            return View(employee);
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
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {

            if (ModelState.IsValid)
            {

                if (id != employee.Id) return BadRequest();


                var count = _EmployeeRepository.delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }


            return View(employee);
        }

    }
}
