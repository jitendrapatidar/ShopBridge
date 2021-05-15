using SB.Model;
using SB.Repository.TableModel;
using SB.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SB.Service
{
    //Inventory
    #region SERVICE
    public class InventoryService : IInventoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public InventoryService()
        {
            _unitOfWork = new UnitOfWork();
        }


        #region GET
         
        public async Task<CommonResult> GetAllAsync()
        {
            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            List<Inventory> dto = new List<Inventory>();
            IEnumerable<tblInventory> obj = await _unitOfWork.InventoryRepository.GetAllAsync();
            if (obj.Any())
            {
                EntityMapper<tblInventory, Inventory> mapObj = new EntityMapper<tblInventory, Inventory>();
                foreach (var item in obj)
                {
                    dto.Add(mapObj.Translate(item));
                }
                oCommonResult = new CommonResult
                {
                    Count = dto.Count(),
                    Message = "Get All Inventory",
                    Status = StatusCode.Sucess,
                    Result = dto
                };
            }
            return oCommonResult;
        }
        
        public virtual async Task<CommonResult> GetAsyncById(int Id)
        {

            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            Inventory dto = new Inventory();
            tblInventory obj = await _unitOfWork.InventoryRepository.GetByIdAsync(Id);//GetByIdAsync(Id);
            if (obj != null)
            {
                EntityMapper<tblInventory, Inventory> mapObj = new EntityMapper<tblInventory, Inventory>();
                dto = mapObj.Translate(obj);
                oCommonResult = new CommonResult
                {
                    Count = 1,
                    Message = "By Id",
                    Status = StatusCode.Sucess,
                    Result = dto
                };
            }
            return oCommonResult;
        }

        public virtual async Task<CommonResult> GetAsyncByCheckInventoryName(string Name)
        {

            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = false
            };

            UserMaster dto = new UserMaster();
            var obj = await _unitOfWork.InventoryRepository.GetManyAsync(e => e.Name.Trim().ToLower().Equals(Name.Trim().ToLower()));//GetByIdAsync(Id);
            if (obj != null)
            {
                if (obj.Count() > 0)
                {
                    oCommonResult = new CommonResult
                    {
                        Count = 1,
                        Message = "Name already existing ",
                        Status = StatusCode.Sucess,
                        Result = true
                    };
                }

            }
            return oCommonResult;
        }

        #endregion

        #region INERT

        public virtual async Task<CommonResult> InsertAsync(Inventory source)
        {

            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            try
            {
                if (source == null)
                    throw new ArgumentNullException("Inventory");
                EntityMapper<Inventory, tblInventory> mapObj = new EntityMapper<Inventory, tblInventory>();
                tblInventory entity = mapObj.Translate(source);
                await _unitOfWork.InventoryRepository.InsertAsync(entity);
                await _unitOfWork.CommitAsync();
                source.Id = entity.Id;
                oCommonResult = new CommonResult
                {
                    Count = 1,
                    Message = "Done",
                    Status = StatusCode.Sucess,
                    Result = entity.Id
                };

            }
            catch (Exception ex)
            {

                oCommonResult = new CommonResult
                {
                    Count = 0,
                    Message = ex.Message + "" + ex.StackTrace,
                    Status = StatusCode.Sucess,
                    Result = null
                };
            }

            return oCommonResult;
        }

        public virtual async Task<CommonResult> InsertAsync(List<Inventory> source)
        {
            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };
            try
            {
                if (source == null)
                    throw new ArgumentNullException("Inventory");
                EntityMapper<List<Inventory>, List<tblInventory>> mapObj = new EntityMapper<List<Inventory>, List<tblInventory>>();
                List<tblInventory> entity = mapObj.Translate(source);
                await _unitOfWork.InventoryRepository.InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                oCommonResult = new CommonResult
                {
                    Count = entity.Count(),
                    Message = "Done",
                    Status = StatusCode.Sucess,
                    Result = null
                };

            }
            catch (Exception ex)
            {

                oCommonResult = new CommonResult
                {
                    Count = 0,
                    Message = ex.Message + "" + ex.StackTrace,
                    Status = StatusCode.Error,
                    Result = null
                };
            }
            return oCommonResult;
        }
        #endregion

        #region UPDATE

       
        public virtual async Task<CommonResult> UpdateAsync(Inventory source)
        {
            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };
            try
            {
                if (source == null)
                    throw new ArgumentNullException("Inventory");

                EntityMapper<Inventory, tblInventory> mapObj = new EntityMapper<Inventory, tblInventory>();
                tblInventory entity = mapObj.Translate(source);
                await _unitOfWork.InventoryRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                source.Id = entity.Id;

                oCommonResult = new CommonResult
                {
                    Count = 1,
                    Message = "Done",
                    Status = StatusCode.Sucess,
                    Result = entity.Id
                };

            }
            catch (Exception ex)
            {

                oCommonResult = new CommonResult
                {
                    Count = 0,
                    Message = ex.Message + "" + ex.StackTrace,
                    Status = StatusCode.Error,
                    Result = null
                };
            }
            return oCommonResult;
        }

        public virtual async Task<CommonResult> UpdateAsync(Inventory source, int id)
        {
            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };
            try
            {
                if (source == null)
                    throw new ArgumentNullException("Inventory");

                EntityMapper<Inventory, tblInventory> mapObj = new EntityMapper<Inventory, tblInventory>();
                tblInventory entity = mapObj.Translate(source);

              
                await _unitOfWork.InventoryRepository.UpdateAsync(entity, id);
                await _unitOfWork.CommitAsync();
             
                source.Id = entity.Id;

                oCommonResult = new CommonResult
                {
                    Count = 1,
                    Message = "Done",
                    Status = StatusCode.Sucess,
                    Result = entity.Id
                };

            }
            catch (Exception ex)
            {

                oCommonResult = new CommonResult
                {
                    Count = 0,
                    Message = ex.Message + "" + ex.StackTrace,
                    Status = StatusCode.Error,
                    Result = null
                };
            }
            return oCommonResult;
        }

        #endregion

        #region DELETE

        

        public virtual async Task<CommonResult> DeleteAsync(Inventory source)
        {
            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            try
            {
                if (source == null)
                    throw new ArgumentNullException("Inventory");
                

                EntityMapper<Inventory, tblInventory> mapObj = new EntityMapper<Inventory, tblInventory>();

                tblInventory entity = mapObj.Translate(source);
                await _unitOfWork.InventoryRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                oCommonResult = new CommonResult
                {
                    Count = 1,
                    Message = "Done",
                    Status = StatusCode.Sucess,
                    Result = null
                };
            }

            catch (Exception ex)
            {
                oCommonResult = new CommonResult
                {
                    Count = 0,
                    Message = ex.Message + "" + ex.StackTrace,
                    Status = StatusCode.Error,
                    Result = null
                };
            }
            return oCommonResult;

        }

        public virtual async Task<CommonResult> DeleteAsync(int Id)
        {


            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            try
            {

                if (Id == 0)
                    throw new ArgumentNullException("Inventory");
                await _unitOfWork.InventoryRepository.DeleteAsync(Id);
                await _unitOfWork.CommitAsync();
                oCommonResult = new CommonResult
                {
                    Count = 1,
                    Message = "Done",
                    Status = StatusCode.Sucess,
                    Result = null
                };
            }

            catch (Exception ex)
            {
                oCommonResult = new CommonResult
                {
                    Count = 0,
                    Message = ex.Message + "" + ex.StackTrace,
                    Status = StatusCode.Error,
                    Result = null
                };
            }
            return oCommonResult;

        }

        #endregion
        
        public async Task<CommonResult> SaveChangesAsync(Inventory source)
        {
            if (source.Id == 0)
            {
                return await Task.Run(() => InsertAsync(source));
            }
            else
            {
                return await Task.Run(() => UpdateAsync(source));
            }
        }

    }

    #endregion
    #region INTERFACE
    public interface IInventoryService
    {


        #region GET
        
        Task<CommonResult> GetAllAsync();
      
        Task<CommonResult> GetAsyncById(int Id);

        Task<CommonResult> GetAsyncByCheckInventoryName(string Name);
        #endregion

        #region INERT


        Task<CommonResult> InsertAsync(Inventory source);

        Task<CommonResult> InsertAsync(List<Inventory> source);
        #endregion

        #region UPDATE

         
        Task<CommonResult> UpdateAsync(Inventory source);
        Task<CommonResult> UpdateAsync(Inventory source, int id);


        #endregion

        #region DELETE

         
        Task<CommonResult> DeleteAsync(Inventory source);

         

        Task<CommonResult> DeleteAsync(int Id);


        #endregion

      
        Task<CommonResult> SaveChangesAsync(Inventory source);
    }
    #endregion

     

}
