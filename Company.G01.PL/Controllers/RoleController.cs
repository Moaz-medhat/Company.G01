using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Company.G01.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Company.G01.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

       

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturn> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturn()
                {
                    Id = U.Id,
                    Name = U.Name,
                    

                });

            }
            else
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturn
                {
                    Id = U.Id,
                    Name= U.Name

                }).Where(U => U.Name.ToLower().Contains(SearchInput.ToLower()));


            }
            //ViewData["Message"] = "Hello from viewData";
            //ViewBag.Message= "Hello from viewBag";

            return View(roles);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturn model)
        {


            if (ModelState.IsValid)
            {

               var role= await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,

                    };
                  var result =   await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return View(model);
        }






        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new { StatusCode = 404, message = $"role with id : {id} not found" });

            var dto = new RoleToReturn()
            {
                Id = role.Id,
                Name = role.Name,
                
            };

            return View(viewName, dto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {


            return await Details(id, "Edit");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturn model)
        {

            if (ModelState.IsValid)
            {

                if (id != model.Id) return BadRequest("invalid operation");



                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("invalid operation");
                var roleResult = await _roleManager.FindByIdAsync(model.Name);

                if (roleResult is  null)
                {
                    role.Name = model.Name;


                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                }
                ModelState.AddModelError("", "Invalid Operation");

               
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"department with id : {id} not found" });

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturn model)
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("invalid operation");



                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("invalid operation");
                

              
                    role.Name = model.Name;


                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                
                ModelState.AddModelError("", "Invalid Operation");

            }


            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null) return NotFound();

            ViewData["roleId"]=roleId;

            var usersInRole= new List<UsersInRoleDto>();
            var users= await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole=new UsersInRoleDto()
                {
                    UserId=user.Id,
                    UserName=user.UserName 
                };
                if (await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userInRole.IsSelected = true;

                }
                else
                {
                    userInRole.IsSelected=false;
                }
                usersInRole.Add(userInRole);

            }
            return View(usersInRole);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UsersInRoleDto> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null) return NotFound();
            if(ModelState.IsValid)
            {

                foreach (var user in users)
                {

                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && ! await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);


                        }

                    }


                }
                return RedirectToAction(nameof(Edit), new {id = roleId});


            }



            return View(users);


        }

    }
}
