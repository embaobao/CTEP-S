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
    
    public partial class CTEPSysCofig
    {
        public int id { get; set; }
        public string AdminId { get; set; }
        public string AdminPw { get; set; }
        public string SysDescription { get; set; }
        public string EmaiTemplate { get; set; }
        public string WebAddress { get; set; }
        public string SysAddress { get; set; }
        public byte[] img { get; set; }
    }
}
