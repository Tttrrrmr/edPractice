//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace edPractice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CountryClimate
    {
        public int ID_countryclimate { get; set; }
        public int ID_country { get; set; }
        public int ID_climate { get; set; }
    
        public virtual Climate Climate { get; set; }
        public virtual Country Country { get; set; }
    }
}
