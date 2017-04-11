using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AdsEntity;


namespace AdsDal
{
    public class UnitOfWork : IDisposable
    {
        private SkyWebContext context = new SkyWebContext();

         private GenericRepository<AdsSysUser> adsSysUsersRepository;

         public GenericRepository<AdsSysUser> AdsSysUsersRepository
        {
            get
            {

                if (this.adsSysUsersRepository == null)
                {
                    this.adsSysUsersRepository = new GenericRepository<AdsSysUser>(context);
                }
                return adsSysUsersRepository;
            }
        }

     
        

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}