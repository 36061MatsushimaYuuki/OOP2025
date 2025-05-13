using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SalesCalculator {
    // 売り上げクラス
    public class Sale {
        // 店舗名
        public string ShopName { get; set; } = string.Empty;
        // 商品カテゴリ
        public string ProductCategory { get; set; } = string.Empty;
        // 売上高
        public int Amount { get; set; }
    }
}
