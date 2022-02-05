using Microsoft.EntityFrameworkCore;
using System;
using WebAppShared.Models;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL
{
    public partial class AppDataContext : DbContext
    {
        //public AppDataContext()
        //{
        //}

        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DeletedUser> DeletedUsers { get; set; }
        public virtual DbSet<SourcePartner> SourcePartners { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }
        public virtual DbSet<BankingInfo> BankingInfos { get; set; }
        public virtual DbSet<NomineeInfo> NomineeInfos { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<InvestmentModel> InvestmentModels { get; set; }
        public virtual DbSet<InvestmentPayout> InvestmentPayouts { get; set; }
        public virtual DbSet<NotificationModel> NotificationModels { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //// Below error uccur when you are trying to add a User with foreign key SourcePartnerId which is not exist in the SourcePartner table. 
            //// The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Users_SourcePartners_SourcePartnerId". 
            /// The conflict occurred in database "webappdb", table "dbo.SourcePartners", column 'Id'.
            /// The statement has been terminated.

            this.SeedRole(modelBuilder);
            this.SeedUser(modelBuilder);
            this.SeedSourcePartner(modelBuilder);





            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        private void SeedRole(ModelBuilder builder)
        {

            Role role = new Role()
            {
                Id = new Guid("fab4fac1-c546-41de-aebc-a14da6895711"),
                RoleName = "Admin",
                RoleDesc = "With Full Access",
                SortNo = 1,
                IsActive = true
            };


            builder.Entity<Role>().HasData(role);
        }
        private void SeedUser(ModelBuilder builder)
        {
            User user = new User()
            {
                Id = new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                CreatedDate = DateTime.Now,
                EmailAddress = "felix.matusinio@aabqatar.com",
                Password = "964c09cc384b49fca2a5e43510983b54",
                Mobile = "66339940",
                FirstName = "Felix",
                LastName = "Matusinio",
                RoleId = new Guid("fab4fac1-c546-41de-aebc-a14da6895711"),
                SourcePartnerId = 1,
                SortNo = 1,
                IsActive = true
            };


            builder.Entity<User>().HasData(user);
        }

        private void SeedSourcePartner(ModelBuilder builder)
        {

            SourcePartner partner = new SourcePartner()
            {
                Id = 1,
                Name = "Jane",
                Description = "Partner",
                SortNo = 1,
                IsActive = true
            };


            builder.Entity<SourcePartner>().HasData(partner);
        }



    }
}
