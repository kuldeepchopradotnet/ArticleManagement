using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AM.Api.Controllers
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string referenceId { get; set; }
        public decimal charges { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRole> _roleManager;

        public ValuesController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET api/values
       // [Authorize]
        //[Route("test")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           

            IList<int> intList = new List<int>() { 10, 20, 30, 40 };

            //Or

            IList<Student> studentList = new List<Student>() {
                new Student(){ StudentID=1, StudentName="Bill",referenceId = "43", charges = 340},
                new Student(){ StudentID=2, StudentName="Steve",referenceId = "43", charges = 340},
                new Student(){ StudentID=3, StudentName="Ram",referenceId = "43", charges = 340},
                new Student(){ StudentID=1, StudentName="Moin",referenceId = "41", charges = 281}
            };


            var a = studentList.GroupBy(x => x.referenceId).Select(x=>x.FirstOrDefault()).Sum(s => s.charges);


            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        //[CusAuth]
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<IEnumerable<string>> Auth()
        {

            return new string[] { "Authorize", "True" };
        }

        [Route("register")]
        [HttpGet]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = username, NormalizedUserName = username, Email = username };

            var result = await _userManager.CreateAsync(user, password);

            string strRole = "Basic User";

            ApplicationUserRole role = new ApplicationUserRole();
            role.Name = strRole;


            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(strRole) == null)
                {
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(user, strRole);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", strRole));

                return Ok();
            }

            return BadRequest(result.Errors);
        }

    }
}
