using System.ComponentModel.DataAnnotations;

namespace KKHS_API.Model
{
    public class ProductTest
    {
        /// <summary>
        /// 廠商編號
        /// </summary>
       
        public string? MerchantID { get; set; }

        /// <summary>
        /// 廠商交易編號
        /// </summary>
        
        public string? MerchantTradeNo { get; set; }

        /// <summary>
        /// 物流子類型
        /// </summary>
        
        public string? LogisticsSubType { get; set; }

        /// <summary>
        /// 使用者選擇的超商店舖編號
        /// </summary>
        
        public string? CVSStoreID { get; set; }

        /// <summary>
        /// 使用者選擇的超商店舖名稱
        /// </summary>
       
        public string? CVSStoreName { get; set; }
        
        /// <summary>
        /// 使用者選擇的超商店舖地址
        /// </summary>
        
        public string? CVSAddress { get; set; }

        /// <summary>
        /// 使用者選擇的超商店舖電話
        /// </summary>
        
        public string? CVSTelephone { get; set; }

        /// <summary>
        /// 使用者選擇的超商店舖是否為離島店鋪
        /// </summary>
        /// <remarks>
        /// 0：本島
        /// 1：離島
        /// 僅下列物流子類型會回傳此欄位：
        /// UNIMART：7-ELEVEN超商
        /// UNIMARTC2C：7-ELEVEN超商交貨便
        /// </remarks>
        public string? CVSOutSide { get; set; }

        /// <summary>
        /// 額外資訊
        /// </summary>
       
        public string? ExtraData { get; set; }
    }
}
