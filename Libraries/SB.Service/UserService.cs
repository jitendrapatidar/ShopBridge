using SB.Model;
using SB.Repository.TableModel;
using SB.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Service
{


    #region SERVICE
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }


        #region GET
        public virtual CommonResult GetAll()
        {
            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            List<UserMaster> dto = new List<UserMaster>();
            IEnumerable<tblUserMaster> obj = _unitOfWork.UserMasterRepository.GetAll();
            if (obj.Any())
            {
                EntityMapper<tblUserMaster, UserMaster> mapObj = new EntityMapper<tblUserMaster, UserMaster>();
                foreach (var item in obj)
                {
                    dto.Add(mapObj.Translate(item));
                }
                oCommonResult = new CommonResult
                {
                    Count = dto.Count(),
                    Message = "Get All User Master",
                    Status = StatusCode.Sucess,
                    Result = dto
                };
            }
            return oCommonResult;
        }
        public async Task<CommonResult> GetAllAsync()
        {
            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            List<UserMaster> dto = new List<UserMaster>();
            var obj = await _unitOfWork.UserMasterRepository.GetAllAsync();
            if (obj.Any())
            {
                EntityMapper<tblUserMaster, UserMaster> mapObj = new EntityMapper<tblUserMaster, UserMaster>();

                foreach (var item in obj)
                {
                    dto.Add(mapObj.Translate(item));
                }
                oCommonResult = new CommonResult
                {
                    Count = dto.Count(),
                    Message = "Get All User Master",
                    Status = StatusCode.Sucess,
                    Result = dto
                };
            }
            return oCommonResult;
        }
        public virtual CommonResult GetById(int Id)
        {

            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };
            UserMaster dto = new UserMaster();
            tblUserMaster obj = _unitOfWork.UserMasterRepository.GetById(Id);
            if (obj != null)
            {
                EntityMapper<tblUserMaster, UserMaster> mapObj = new EntityMapper<tblUserMaster, UserMaster>();
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
        public virtual async Task<CommonResult> GetAsyncById(int Id)
        {

            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = null
            };

            UserMaster dto = new UserMaster();
            tblUserMaster obj = await _unitOfWork.UserMasterRepository.GetByIdAsync(Id);//GetByIdAsync(Id);
            if (obj != null)
            {
                EntityMapper<tblUserMaster, UserMaster> mapObj = new EntityMapper<tblUserMaster, UserMaster>();
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

        public virtual async Task<CommonResult> GetAsyncByCheckUserName(string Usrename)
        {

            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = false
            };

            UserMaster dto = new UserMaster();
            var obj = await _unitOfWork.UserMasterRepository.GetManyAsync(e => e.UserName.Trim().ToLower().Equals(Usrename.Trim().ToLower()));//GetByIdAsync(Id);
            if (obj != null)
            {
                if (obj.Count() > 0)
                {
                    oCommonResult = new CommonResult
                    {
                        Count = 1,
                        Message = "UserName already existing ",
                        Status = StatusCode.Sucess,
                        Result = true
                    };
                }

            }
            return oCommonResult;
        }

        public virtual async Task<CommonResult> GetAsyncLoginUser(string Usrename, string Password)
        {

            CommonResult oCommonResult = new CommonResult
            {
                Count = 0,
                Message = string.Empty,
                Status = StatusCode.NoResult,
                Result = false
            };

            UserMaster dto = new UserMaster();
            var obj = await _unitOfWork.UserMasterRepository.GetManyAsync(e => e.UserName.Trim().ToLower().Equals(Usrename.Trim().ToLower()) && e.Password.Trim().ToLower().Equals(Password.Trim().ToLower()));//GetByIdAsync(Id);
            if (obj != null)
            {
                EntityMapper<tblUserMaster, UserMaster> mapObj = new EntityMapper<tblUserMaster, UserMaster>();
                dto = mapObj.Translate(obj.FirstOrDefault());

                if (dto.Id > 0)
                {

                    oCommonResult = new CommonResult
                    {
                        Count = 1,
                        Message = "Login",
                        Status = StatusCode.Sucess,
                        Result = dto
                    };
                }
                else
                {


                    oCommonResult = new CommonResult
                    {
                        Count = 0,
                        Message = "User is not found.",
                        Status = StatusCode.NoResult,
                        Result = false
                    };
                }

            }
            return oCommonResult;
        }

        #endregion

        #region INERT
        public virtual CommonResult Insert(UserMaster source)
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
                    throw new ArgumentNullException("UserMaster");

                source.Id = 0;
                source.OnDate = DateTime.Now;
                EntityMapper<UserMaster, tblUserMaster> mapObj = new EntityMapper<UserMaster, tblUserMaster>();
                tblUserMaster entity = mapObj.Translate(source);
                _unitOfWork.UserMasterRepository.Insert(entity);
                _unitOfWork.Commit();
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

        public virtual CommonResult Insert(List<UserMaster> source)
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
                    throw new ArgumentNullException("UserMaster");
                EntityMapper<List<UserMaster>, List<tblUserMaster>> mapObj = new EntityMapper<List<UserMaster>, List<tblUserMaster>>();
                List<tblUserMaster> entity = mapObj.Translate(source);
                _unitOfWork.UserMasterRepository.Insert(entity);
                _unitOfWork.Commit();

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
                    Status = StatusCode.Sucess,
                    Result = null
                };
            }
            return oCommonResult;
        }

        public virtual async Task<CommonResult> InsertAsync(UserMaster source)
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
                    throw new ArgumentNullException("UserMaster");
                EntityMapper<UserMaster, tblUserMaster> mapObj = new EntityMapper<UserMaster, tblUserMaster>();
                tblUserMaster entity = mapObj.Translate(source);
                await _unitOfWork.UserMasterRepository.InsertAsync(entity);
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

        public virtual async Task<CommonResult> InsertAsync(List<UserMaster> source)
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
                    throw new ArgumentNullException("UserMaster");
                EntityMapper<List<UserMaster>, List<tblUserMaster>> mapObj = new EntityMapper<List<UserMaster>, List<tblUserMaster>>();
                List<tblUserMaster> entity = mapObj.Translate(source);
                await _unitOfWork.UserMasterRepository.InsertAsync(entity);
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

        public virtual CommonResult Update(UserMaster source)
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
                    throw new ArgumentNullException("UserMaster");

                EntityMapper<UserMaster, tblUserMaster> mapObj = new EntityMapper<UserMaster, tblUserMaster>();
                tblUserMaster entity = mapObj.Translate(source);
                _unitOfWork.UserMasterRepository.Update(entity);
                _unitOfWork.Commit();
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
        public virtual async Task<CommonResult> UpdateAsync(UserMaster source)
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
                    throw new ArgumentNullException("UserMaster");

                EntityMapper<UserMaster, tblUserMaster> mapObj = new EntityMapper<UserMaster, tblUserMaster>();
                tblUserMaster entity = mapObj.Translate(source);
                await _unitOfWork.UserMasterRepository.UpdateAsync(entity);
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
        public virtual async Task<CommonResult> UpdateAsync(UserMaster source, int id)
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
                    throw new ArgumentNullException("UserMaster");

                EntityMapper<UserMaster, tblUserMaster> mapObj = new EntityMapper<UserMaster, tblUserMaster>();
                tblUserMaster entity = mapObj.Translate(source);

                //await _cityRepository.UpdateAsync(entity, id);
                //await _unitOfWork.CommitAsync();

                await _unitOfWork.UserMasterRepository.UpdateAsync(entity, id);
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

        public virtual CommonResult Delete(UserMaster source)
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
                    throw new ArgumentNullException("UserMaster");

                EntityMapper<UserMaster, tblUserMaster> mapObj = new EntityMapper<UserMaster, tblUserMaster>();

                tblUserMaster entity = mapObj.Translate(source);
                _unitOfWork.UserMasterRepository.Delete(entity);
                _unitOfWork.Commit();

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
                }; throw;
            }
            return oCommonResult;
        }

        public virtual async Task<CommonResult> DeleteAsync(UserMaster source)
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
                    throw new ArgumentNullException("UserMaster");

                EntityMapper<UserMaster, tblUserMaster> mapObj = new EntityMapper<UserMaster, tblUserMaster>();

                tblUserMaster entity = mapObj.Translate(source);
                await _unitOfWork.UserMasterRepository.DeleteAsync(entity);
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

        public virtual CommonResult Delete(int Id)
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
                    throw new ArgumentNullException("UserMaster");
                _unitOfWork.UserMasterRepository.Delete(Id);
                _unitOfWork.Commit();
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
                    throw new ArgumentNullException("UserMaster");
                await _unitOfWork.UserMasterRepository.DeleteAsync(Id);
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

        public async Task<CommonResult> SaveChangesAsync(UserMaster source)
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
    public interface IUserService
    {


        #region GET
        CommonResult GetAll();
        Task<CommonResult> GetAllAsync();
        CommonResult GetById(int Id);
        Task<CommonResult> GetAsyncById(int Id);
        Task<CommonResult> GetAsyncByCheckUserName(string Usrename);

        Task<CommonResult> GetAsyncLoginUser(string Usrename, string Password);
        #endregion

        #region INERT
        CommonResult Insert(UserMaster source);

        CommonResult Insert(List<UserMaster> source);

        Task<CommonResult> InsertAsync(UserMaster source);

        Task<CommonResult> InsertAsync(List<UserMaster> source);
        #endregion

        #region UPDATE

        CommonResult Update(UserMaster source);
        Task<CommonResult> UpdateAsync(UserMaster source);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CommonResult> UpdateAsync(UserMaster source, int id);

        #endregion

        #region DELETE

        CommonResult Delete(UserMaster source);

        Task<CommonResult> DeleteAsync(UserMaster source);

        CommonResult Delete(int Id);

        Task<CommonResult> DeleteAsync(int Id);


        #endregion


        Task<CommonResult> SaveChangesAsync(UserMaster source);
    }
    #endregion
}
