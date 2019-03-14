﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Smeta_DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SmetaEntities : DbContext
    {
        public SmetaEntities()
            : base("name=SmetaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Договор_подряда> Договор_подряда { get; set; }
        public virtual DbSet<Заказчик> Заказчик { get; set; }
        public virtual DbSet<Исполнитель> Исполнитель { get; set; }
        public virtual DbSet<Локальная_смета> Локальная_смета { get; set; }
        public virtual DbSet<Объект> Объект { get; set; }
        public virtual DbSet<Поправочный_коэффициент_по_типу_ПИР> Поправочный_коэффициент_по_типу_ПИР { get; set; }
        public virtual DbSet<Проектная_организация> Проектная_организация { get; set; }
        public virtual DbSet<Справочник_видов_работ> Справочник_видов_работ { get; set; }
        public virtual DbSet<Справочник_расценок> Справочник_расценок { get; set; }
        public virtual DbSet<Ставка_14_го_разряда> Ставка_14_го_разряда { get; set; }
    
        public virtual int sp_InsertProect(string yNP, string adress, string rS, string phone, string mail, string nameProect, ObjectParameter idProect)
        {
            var yNPParameter = yNP != null ?
                new ObjectParameter("YNP", yNP) :
                new ObjectParameter("YNP", typeof(string));
    
            var adressParameter = adress != null ?
                new ObjectParameter("Adress", adress) :
                new ObjectParameter("Adress", typeof(string));
    
            var rSParameter = rS != null ?
                new ObjectParameter("RS", rS) :
                new ObjectParameter("RS", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("Phone", phone) :
                new ObjectParameter("Phone", typeof(string));
    
            var mailParameter = mail != null ?
                new ObjectParameter("Mail", mail) :
                new ObjectParameter("Mail", typeof(string));
    
            var nameProectParameter = nameProect != null ?
                new ObjectParameter("NameProect", nameProect) :
                new ObjectParameter("NameProect", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertProect", yNPParameter, adressParameter, rSParameter, phoneParameter, mailParameter, nameProectParameter, idProect);
        }
    
        public virtual ObjectResult<Nullable<double>> ФормированиеОбщейСтоимостиОбъекта(Nullable<int> шифрОбъекта)
        {
            var шифрОбъектаParameter = шифрОбъекта.HasValue ?
                new ObjectParameter("ШифрОбъекта", шифрОбъекта) :
                new ObjectParameter("ШифрОбъекта", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("ФормированиеОбщейСтоимостиОбъекта", шифрОбъектаParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> ФормированиеОбщейТрудоемкостиОбъекта(Nullable<int> шифрОбъекта)
        {
            var шифрОбъектаParameter = шифрОбъекта.HasValue ?
                new ObjectParameter("ШифрОбъекта", шифрОбъекта) :
                new ObjectParameter("ШифрОбъекта", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("ФормированиеОбщейТрудоемкостиОбъекта", шифрОбъектаParameter);
        }
    }
}
