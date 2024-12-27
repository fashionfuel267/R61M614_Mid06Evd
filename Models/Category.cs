//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace R61M614_Mid06Evd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Products = new List<Product>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public Nullable<bool> IsContinued { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:MM-dd-yy}",ApplyFormatInEditMode =true)]
        public Nullable<System.DateTime> EntryDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Product> Products { get; set; }
    }
}
