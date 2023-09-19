using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using userRegistration.Model;
using userRegistration.Repo;

namespace userRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo userRepo;

        public UserController(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll() 
        {
            var _userList = await userRepo.GetAll();
            if (_userList != null)
            {
                return Ok(_userList);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var _user = await userRepo.GetUserById(id);
            if (_user != null)
            {
                return Ok(_user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            var _result = await userRepo.Create(user);
            return Ok(_result);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var _result = await userRepo.Update(id, user);
            return Ok(_result);
        }

        [HttpDelete("RemoveUser")]
        public async Task<ActionResult> RemoveUser(int id)
        {
            var _result = await userRepo.Remove(id);
            return Ok(_result);
        }
    }
}
