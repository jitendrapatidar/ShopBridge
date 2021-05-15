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
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _InventoryService;
        public InventoryController(IInventoryService service)
        {
            _InventoryService = service;
        }

        #region "GET"

        /// <summary>
        /// Get all records      
        /// </summary>
        /// <returns>Common Result</returns>
        [HttpGet("GetAll")]
        public async Task<CommonResult> GetAllInventory()
        {
            CommonResult commonResult = await _InventoryService.GetAllAsync();
            return commonResult;
        }
        /// <summary>
        /// Get all active records
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Common Result</returns>
        [HttpGet("GetById/{Id}")]
        public async Task<CommonResult> GetUserById(int Id)
        {
            CommonResult commonResult = await _InventoryService.GetAsyncById(Id);
            return commonResult;
        }
        [HttpGet("CheckInventoryName/{Name}")]
        public async Task<bool> GetCheckInventoryByName(string Name)
        {
            bool isResult = false;
            CommonResult commonResult = await _InventoryService.GetAsyncByCheckInventoryName(Name);
            if (Convert.ToBoolean(commonResult.Result) == true) isResult = true;
            return isResult;
        }
        

        #endregion

        #region "Insert"

        /// <summary>
        /// Save model
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Common Result</returns>
        [HttpPost("Insert")]
        public async Task<CommonResult> InsertInventory(Inventory model)
        {
            CommonResult commonResult = await _InventoryService.InsertAsync(model);
            return commonResult;
        }

        /// <summary>
        /// Save model list
        /// </summary>
        /// <param name="modelList">Model list</param>
        /// <returns>Common Result</returns>
        [HttpPost("InsertList")]
        public async Task<CommonResult> InsertInventoryList(List<Inventory> modelList)
        {
            CommonResult commonResult = await _InventoryService.InsertAsync(modelList);
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
        public async Task<CommonResult> UpdateInventory(Inventory model)
        {
            CommonResult commonResult = await _InventoryService.UpdateAsync(model);
            return commonResult;
        }

        [HttpPost("UpdateById")]
        public async Task<CommonResult> UpdateInventoryById(Inventory model, int Id)
        {
            CommonResult commonResult = await _InventoryService.UpdateAsync(model, Id);
            return commonResult;
        }

        #endregion#region "Update"

        #region "Delete"

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Common Result</returns>
        [HttpGet("DeleteById/{Id}")]
        
        public async Task<CommonResult> DeleteInventoryById(int Id)
        {
            CommonResult commonResult = await _InventoryService.DeleteAsync(Id);
            return commonResult;
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Common Result</returns>
        [HttpPost("Delete")]
        public async Task<CommonResult> DeleteUser(Inventory model)
        {
            CommonResult commonResult = await _InventoryService.DeleteAsync(model);
            return commonResult;
        }


        #endregion

    }
}
