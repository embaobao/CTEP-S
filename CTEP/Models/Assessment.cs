//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CTEP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Assessment
    {
        public int id { get; set; }
        public Nullable<int> EvaTabId { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<double> AllScore { get; set; }
        public string suggest { get; set; }
        public Nullable<double> TeachObj { get; set; }
        public Nullable<double> TeachMethod { get; set; }
        public Nullable<double> TeachAbility { get; set; }
        public Nullable<double> TeachAttitude { get; set; }
        public Nullable<double> StudentRelation { get; set; }
        public Nullable<int> BandiId { get; set; }
    }
}
