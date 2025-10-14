using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Company.G01.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Company.G01.PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _EmployeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_EmployeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
             employees = await _unitOfWork.employeeRepository.GetAllAsync();
                
            }
            else
            {
             employees = await _unitOfWork.employeeRepository.GetByNameAsync(SearchInput);

            }
            //ViewData["Message"] = "Hello from viewData";
            //ViewBag.Message= "Hello from viewBag";

            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.departmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto employeeDto)
        {
                

            if (ModelState.IsValid)
            {

                if (employeeDto.Image is not null)
                {
                    
                   employeeDto.ImageName=  DocumentSettings.UploadFile(employeeDto.Image, "images");
                }
                //var employee = new Employee()
                //{
                //   Name=employeeDto.Name,
                //   Adress=employeeDto.Adress,
                //   Age=employeeDto.Age,
                //   CreateAt=employeeDto.CreateAt,
                //   HiringDate=employeeDto.HiringDate,
                //   Email=employeeDto.Email,
                //   IsActive=employeeDto.IsActive,
                //   IsDeleted=employeeDto.IsDeleted,
                //   Phone=employeeDto.Phone,
                //   Salary=employeeDto.Salary,
                //   departmentId=employeeDto.departmentId,

                //};
               var employee= _mapper.Map<Employee>(employeeDto);
                await _unitOfWork.employeeRepository.addAsync(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employeeDto);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = await _unitOfWork.employeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with id : {id} not found" });

            return View(viewName, employee);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id,string viewName="Edit")
        {
            var departments = await _unitOfWork.departmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id");

            var  employee=await _unitOfWork.employeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"department with id : {id} not found" });

            var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);
            //employee.Id = id;

            //var employeeDto = new CreateEmployeeDto()
            //{

            //    Name = employee.Name,
            //    Adress = employee.Adress,
            //    Age = employee.Age,
            //    CreateAt = employee.CreateAt,
            //    HiringDate = employee.HiringDate,
            //    Email = employee.Email,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    departmentId = employee.departmentId,

            //};


            return View(viewName,employeeDto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto employeeDto,string viewName="Edit")
        {

            if (ModelState.IsValid)
            {

                if(employeeDto.ImageName is not null && employeeDto.Image is not null) 
                {
                    DocumentSettings.DeleteFile(employeeDto.ImageName, "images");
                }
                if (employeeDto.Image is not null)
                {
                    
                    employeeDto.ImageName= DocumentSettings.UploadFile(employeeDto.Image, "images");
                }


                //if (id != employee.Id) return BadRequest();
                var employee=_mapper.Map<Employee>(employeeDto);
                   employee.Id = id;

                //var employee = new Employee()
                //{
                //    Id=id,
                //    Name = employeeDto.Name,
                //    Adress = employeeDto.Adress,
                //    Age = employeeDto.Age,
                //    CreateAt = employeeDto.CreateAt,
                //    HiringDate = employeeDto.HiringDate,
                //    Email = employeeDto.Email,
                //    IsActive = employeeDto.IsActive,
                //    IsDeleted = employeeDto.IsDeleted,
                //    Phone = employeeDto.Phone,
                //    Salary = employeeDto.Salary,
                //    departmentId = employeeDto.departmentId,

                //};


                 _unitOfWork.employeeRepository.update(employee);
                var count =await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }


            return View(viewName,employeeDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"department with id : {id} not found" });

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto employeeDto)
        {

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.Id = id;
                //if (id != employee.Id) return BadRequest();
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = employeeDto.Name,
                //    Adress = employeeDto.Adress,
                //    Age = employeeDto.Age,
                //    CreateAt = employeeDto.CreateAt,
                //    HiringDate = employeeDto.HiringDate,
                //    Email = employeeDto.Email,
                //    IsActive = employeeDto.IsActive,
                //    IsDeleted = employeeDto.IsDeleted,
                //    Phone = employeeDto.Phone,
                //    Salary = employeeDto.Salary,
                //    departmentId = employeeDto.departmentId,

                //};


                _unitOfWork.employeeRepository.delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    if (employeeDto.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(employeeDto.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }

            }


            return View(employeeDto);
        }

    }
}
