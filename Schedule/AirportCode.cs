//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Schedule
{
    using System;
    using System.Collections.Generic;
    
    public partial class AirportCode
    {
        public int airport_id { get; set; }
        public int type_id { get; set; }
        public string code { get; set; }
    
        public virtual Airport Airport { get; set; }
        public virtual CodeType CodeType { get; set; }
    }
}
