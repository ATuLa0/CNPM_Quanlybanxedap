using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_Quanlybanxedap.Models
{
    public class XeViewModel
    {
        public List<XE> Top5XeBanChay { get; set; }
        public List<XE> DanhSachXeNgoaiTop5 { get; set; }
        public int TongSoLuongBan { get; set; }
        public Dictionary<int, int> SoLuongBanForEachXe { get; set; }
        public Dictionary<int, int> SoLuongBanForEachXeNgoaiTop5 { get; set; }

    }
}