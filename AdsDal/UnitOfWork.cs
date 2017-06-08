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

        private GenericRepository<SysUser> SysUsersRepository;

         public GenericRepository<SysUser> sysUsersRepository
        {
            get
            {

                if (this.SysUsersRepository == null)
                {
                    this.SysUsersRepository = new GenericRepository<SysUser>(context);
                }
                return SysUsersRepository;
            }
        }

         private GenericRepository<Setting> SettingsRepository;

         public GenericRepository<Setting> settingsRepository
         {
             get
             {

                 if (this.SettingsRepository == null)
                 {
                     this.SettingsRepository = new GenericRepository<Setting>(context);
                 }
                 return SettingsRepository;
             }
         }

         private GenericRepository<Category> CategorysRepository;

         public GenericRepository<Category> categorysRepository
         {
             get
             {

                 if (this.CategorysRepository == null)
                 {
                     this.CategorysRepository = new GenericRepository<Category>(context);
                 }
                 return CategorysRepository;
             }
         }

         private GenericRepository<AdsVideo> AdsVideosRepository;

         public GenericRepository<AdsVideo> adsVideosRepository
         {
             get
             {

                 if (this.AdsVideosRepository == null)
                 {
                     this.AdsVideosRepository = new GenericRepository<AdsVideo>(context);
                 }
                 return AdsVideosRepository;
             }
         }

         private GenericRepository<AdsCustomer> AdsCustomersRepository;

         public GenericRepository<AdsCustomer> adsCustomersRepository
         {
             get
             {

                 if (this.AdsCustomersRepository == null)
                 {
                     this.AdsCustomersRepository = new GenericRepository<AdsCustomer>(context);
                 }
                 return AdsCustomersRepository;
             }
         }

         private GenericRepository<AdsBaby> AdsBabysRepository;

         public GenericRepository<AdsBaby> adsBabysRepository
         {
             get
             {

                 if (this.AdsBabysRepository == null)
                 {
                     this.AdsBabysRepository = new GenericRepository<AdsBaby>(context);
                 }
                 return AdsBabysRepository;
             }
         }
         private GenericRepository<Pingjia> PingjiaRepository;
         public GenericRepository<Pingjia> pingjiasRepository
         {
             get
             {

                 if (this.PingjiaRepository == null)
                 {
                     this.PingjiaRepository = new GenericRepository<Pingjia>(context);
                 }
                 return PingjiaRepository;
             }
         }

         private GenericRepository<Baogao> BaogaoRepository;

         public GenericRepository<Baogao> baogaoRepository
         {
             get
             {

                 if (this.BaogaoRepository == null)
                 {
                     this.BaogaoRepository = new GenericRepository<Baogao>(context);
                 }
                 return BaogaoRepository;
             }
         }

         private GenericRepository<Scale> ScaleRepository;

         public GenericRepository<Scale> scaleRepository
         {
             get
             {

                 if (this.ScaleRepository == null)
                 {
                     this.ScaleRepository = new GenericRepository<Scale>(context);
                 }
                 return ScaleRepository;
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