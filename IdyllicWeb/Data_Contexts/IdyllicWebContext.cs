using IdyllicWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Data_Contexts
{
    public class IdyllicWebContext : DbContext
    {
        public IdyllicWebContext() : base("IdyllicConString")
        {
        }

        #region DbSets

        #region CMS
        public virtual DbSet<MenuItemMaster> MenuItemMstr { get; set; }
        public virtual DbSet<MenuUrlMaster> MenuUrlMstr { get; set; }
        public virtual DbSet<MediaCategoryMaster> MediaCategoryMstr { get; set; }
        public virtual DbSet<MediaGallery> MediaGalleries { get; set; }
        public virtual DbSet<MediaFile> MediaFiles { get; set; }

        #endregion

        #region GST
        public virtual DbSet<ContactUS> ContactUSs { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        #endregion GST

        #region Bridge Entities

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // configures one-to-many relationship
            modelBuilder.Entity<MediaFile>()
            .HasRequired(r => r.MediaGalleries)
            .WithMany(s => s.iMediaFiles)
            .HasForeignKey(f => f.MediaGalleryId);
            #endregion
        }

    }
}