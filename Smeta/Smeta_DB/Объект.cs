//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Объект
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Объект()
        {
            this.Договор_подряда = new HashSet<Договор_подряда>();
            this.Договор_подряда1 = new HashSet<Договор_подряда>();
            this.Локальная_смета = new HashSet<Локальная_смета>();
            this.Локальная_смета1 = new HashSet<Локальная_смета>();
        }
    
        public int Код_коэффициента { get; set; }
        public int Шифр { get; set; }
        public int КодСтавки { get; set; }
        public string Адрес { get; set; }
        public string НаименованиеОбъекта { get; set; }
        public int КодПроектировщика { get; set; }
        public int КодЗаказчик { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Договор_подряда> Договор_подряда { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Договор_подряда> Договор_подряда1 { get; set; }
        public virtual Заказчик Заказчик { get; set; }
        public virtual Заказчик Заказчик1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Локальная_смета> Локальная_смета { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Локальная_смета> Локальная_смета1 { get; set; }
        public virtual Поправочный_коэффициент_по_типу_ПИР Поправочный_коэффициент_по_типу_ПИР { get; set; }
        public virtual Поправочный_коэффициент_по_типу_ПИР Поправочный_коэффициент_по_типу_ПИР1 { get; set; }
        public virtual Проектная_организация Проектная_организация { get; set; }
        public virtual Проектная_организация Проектная_организация1 { get; set; }
        public virtual Ставка_14_го_разряда Ставка_14_го_разряда { get; set; }
        public virtual Ставка_14_го_разряда Ставка_14_го_разряда1 { get; set; }
    }
}
