//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CNPM_Quanlybanxedap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BAOHANH
    {
        public int MaBH { get; set; }
        public Nullable<int> MaKH { get; set; }
        public Nullable<int> MaXe { get; set; }
        public Nullable<System.DateTime> NgayBaoHanh { get; set; }
        public string NoiDungBH { get; set; }
        public string TrangThai { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual XE XE { get; set; }
    }
}
