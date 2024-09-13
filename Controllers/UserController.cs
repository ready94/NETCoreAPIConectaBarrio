using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userSvc;

        public UserController(IUserService userSvc)
        {
            _userSvc = userSvc;
        }

        [HttpPost("createUser")]
        public ActionResult<bool> CreateUser([FromBody] NewUserModel user)
        {
            return Ok(_userSvc.CreateUser(user));
        }

        [HttpPost("updateUser/{idAdmin}/{idUser}")]
        public ActionResult<bool> UpdateUser([FromBody] UserModel user, int idAdmin, int idUser)
        {
            return Ok(_userSvc.UpdateUser(user, idAdmin, idUser));
        }

        [HttpGet("getUserData/{idUser}")]
        public ActionResult<UserDTO> GetUserData(int idUser)
        {
            return Ok(_userSvc.GetUserData(idUser));
        }

        [HttpGet("getAllUsers")]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {
            return Ok(_userSvc.GetAllUsers());
        }

        [HttpGet("getUserRoles")]
        public ActionResult<List<UserRolesDTO>> GetAllUserRoles()
        {
            return Ok(_userSvc.GetAllUserRoles());
        }

    }
}
