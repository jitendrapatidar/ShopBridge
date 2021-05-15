using SB.Repository.Database;
using SB.Repository.GenericRepository;
using SB.Repository.TableModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Repository.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        #region Private member variables
        private DbshopbridgeContext _context = null;
        private string _ConnectionString;
        #endregion
        #region Private Repository  Member variables Objects
        private GenericRepository<tblInventory> _InventoryRepository;
        private GenericRepository<tblUserMaster> _UserMasterRepository;
        #endregion


        #region Constructor
        public UnitOfWork()
        {
            _context = new DbshopbridgeContext();

        }
        #endregion


        #region Public Repository Creation properties
        public string ConnectionString
        {
            get
            {
                this._ConnectionString = _context.Database.CanConnect().ToString();//Connection.ConnectionString;
                return _ConnectionString;
            }
        }

       
        public GenericRepository<tblInventory> InventoryRepository
        {
            get
            {
                if (this._InventoryRepository == null)
                    this._InventoryRepository = new GenericRepository<tblInventory>(_context);

                return _InventoryRepository;
            }
        }

      

        public GenericRepository<tblUserMaster> UserMasterRepository
        {
            get
            {
                if (this._UserMasterRepository == null)
                    this._UserMasterRepository = new GenericRepository<tblUserMaster>(_context);

                return _UserMasterRepository;
            }
        }
 
        #endregion
        #region Public member methods
        /// <summary>
        /// Save method.
        /// </summary>
        public void Commit()
        {
            try
            {
                // _context.Configuration.ValidateOnSaveEnabled = false; //28082014
                _context.SaveChanges();
                //_context.SaveChanges();
            }
            catch //(Exception e)
            {

                //var outputLines = new List<string>();
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                //    }
                //}
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                //throw e;
            }

        }
        public async Task CommitAsync()
        {
            try
            {
                // _context.Configuration.ValidateOnSaveEnabled = false; //28082014
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
            catch //(Exception e)
            {

                //var outputLines = new List<string>();
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                //    }
                //}
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

               // throw e;
            }

        }
       
        #endregion
        #region Implementing IDiosposable

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
