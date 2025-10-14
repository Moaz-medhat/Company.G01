using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.departmentRepository.GetAllAsync();


            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatedepartmentDto createdepartment)
        {

            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = createdepartment.Code,
                    Name = createdepartment.Name,
                    CreateAt = createdepartment.CreateAt
                };
               await  _unitOfWork.departmentRepository.addAsync(department);
                var count =  await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createdepartment);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id,string viewName="Details")
        {
            if (id is null) return BadRequest("Invalid Id");

          var department= await _unitOfWork.departmentRepository.GetAsync(id.Value);
            if (department is null ) return NotFound(new { StatusCode = 404, message=$"department with id : {id} not found" } );

            return View(viewName,department);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = await _unitOfWork.departmentRepository.GetAsync(id.Value);
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
        public async Task<IActionResult> Edit([FromRoute]int id,CreatedepartmentDto createdepartment)
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

                 _unitOfWork.departmentRepository.update(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

            }
          

            return View(createdepartment);
        }

        //look here again
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department =await _unitOfWork.departmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"department with id : {id} not found" });

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete ([FromRoute] int id, CreatedepartmentDto createdepartment)
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


                 _unitOfWork.departmentRepository.delete(department);
                var count =await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }


            return View(createdepartment);
        }




    }
}
