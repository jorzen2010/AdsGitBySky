using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AdsEntity;


namespace AdsDal
{
    public class SkyWebContext : DbContext
    {
        public SkyWebContext()
            : base("SkyWebContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Baogao> Baogaos { get; set; }
        public DbSet<AdsBaby> AdsBabys { get; set; }

        public System.Data.Entity.DbSet<AdsEntity.AdsVideo> AdsVideos { get; set; }

        public System.Data.Entity.DbSet<AdsEntity.AdsCustomer> AdsCustomers { get; set; }

        public System.Data.Entity.DbSet<AdsEntity.Scale> Scales { get; set; }

    }
}
