//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcStock.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.Saleses = new HashSet<Saleses>();
        }
    
        public int ProductID { get; set; }

     // Products tablosu i�in Validation kontrollerini html taraf�nda yap�yoruz ili�kili s�tunlar oldu�u i�in.
        public string ProductName { get; set; }
        public Nullable<short> ProductCategoryID { get; set; }

     
        public Nullable<decimal> ProductCost { get; set; }

       
        public string ProductBrand { get; set; }


        public Nullable<byte> ProductStock { get; set; }
    
        public virtual Categories Categories { get; set; }
        /*
         Navigasyon �zelli�i: public virtual Categories Categories { get; set; } ifadesi, Products ve Categories aras�nda bir ili�ki kurar. 
        Bu navigasyon �zelli�i, Categories verilerini �r�nler ile ili�kilendirmeye olanak tan�r.*/
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Saleses> Saleses { get; set; }
    }
}
