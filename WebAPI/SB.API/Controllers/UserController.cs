using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SB.Model;
using SB.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SB.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        #region "GET"

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>Common Result</returns>
        [HttpGet("GetAll")]
        public async Task<CommonResult> GetAllUser()
        {
            CommonResult commonResult = await _userService.GetAllAsync();
            return commonResult;
        }
        /// <summary>
        /// Get all active records
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Common Result</returns>
        [HttpGet("GetById")]
        public async Task<CommonResult> GetUserById(int Id)
        {
            CommonResult commonResult = await _userService.GetAsyncById(Id);
            return commonResult;
        }

        /// <returns>Common Result</returns>
        [HttpGet("ExistingUser")]
        public async Task<bool> PostExistingUser(string Username)
        {
            bool isReuslt = false;

            CommonResult commonResult = await _userService.GetAsyncByCheckUserName(Username);
            if (Convert.ToBoolean(commonResult.Result) == true) isReuslt = true;

            return isReuslt;
        }

        [HttpPost("UserLogin")]
        public async Task<CommonResult> PostLogin(UserMaster login)//string Username,string Password)
        {
            CommonResult commonResult = await _userService.GetAsyncLoginUser(login.UserName, login.Password);
            return commonResult;
        }
        
        #endregion

        #region "Insert"

        /// <summary>
        /// Save model
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Common Result</returns>
        [HttpPost("Insert")]
        public async Task<CommonResult> InsertUser(UserMaster model)
        {
            CommonResult commonResult = await _userService.InsertAsync(model);
            return commonResult;
        }

        /// <summary>
        /// Save model list
        /// </summary>
        /// <param name="modelList">Model list</param>
        /// <returns>Common Result</returns>
        [HttpPost("InsertList")]
        public async Task<CommonResult> InsertUserList(List<UserMaster> modelList)
        {
            CommonResult commonResult = await _userService.InsertAsync(modelList);
            return commonResult;
        }

        #endregion

        #region "Update"

        /// <summary>
        /// Update model
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Common Result</returns>
        [HttpPost("Update")]
        public async Task<CommonResult> UpdateUser(UserMaster model)
        {
            CommonResult commonResult = await _userService.UpdateAsync(model);
            return commonResult;
        }

        [HttpPost("UpdateById")]
        public async Task<CommonResult> UpdateUserById(UserMaster model, int Id)
        {
            CommonResult commonResult = await _userService.UpdateAsync(model, Id);
            return commonResult;
        }

        #endregion#region "Update"

        #region "Delete"

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Common Result</returns>
        [HttpPost("DeleteById")]
        public async Task<CommonResult> DeleteUserById(int Id)
        {
            CommonResult commonResult = await _userService.DeleteAsync(Id);
            return commonResult;
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Common Result</returns>
        [HttpPost("Delete")]
        public async Task<CommonResult> DeleteUser(UserMaster model)
        {
            CommonResult commonResult = await _userService.DeleteAsync(model);
            return commonResult;
        }

       
        #endregion
    }
}
