﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi_EF.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InfiniteDBEntities : DbContext
    {
        public InfiniteDBEntities()
            : base("name=InfiniteDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientRental> ClientRentals { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSale> ProductSales { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<tblDepartment> tblDepartments { get; set; }
        public virtual DbSet<tblemployee> tblemployees { get; set; }
    
        [DbFunction("InfiniteDBEntities", "fn_GetEmployeesbyGender")]
        public virtual IQueryable<fn_GetEmployeesbyGender_Result> fn_GetEmployeesbyGender(string gender)
        {
            var genderParameter = gender != null ?
                new ObjectParameter("gender", gender) :
                new ObjectParameter("gender", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fn_GetEmployeesbyGender_Result>("[InfiniteDBEntities].[fn_GetEmployeesbyGender](@gender)", genderParameter);
        }
    
        public virtual int Sales(Nullable<int> pid, Nullable<int> qty_to_sell)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("pid", pid) :
                new ObjectParameter("pid", typeof(int));
    
            var qty_to_sellParameter = qty_to_sell.HasValue ?
                new ObjectParameter("qty_to_sell", qty_to_sell) :
                new ObjectParameter("qty_to_sell", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sales", pidParameter, qty_to_sellParameter);
        }
    
        public virtual ObjectResult<UpdateEmployeeSalary_Result> UpdateEmployeeSalary(Nullable<int> empid)
        {
            var empidParameter = empid.HasValue ?
                new ObjectParameter("empid", empid) :
                new ObjectParameter("empid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UpdateEmployeeSalary_Result>("UpdateEmployeeSalary", empidParameter);
        }
    }
}
