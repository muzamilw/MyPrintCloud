using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public sealed class ProductItem
    {
        private int? _invoiceID;
        private string _title;
        private int? _tax1;
        private int? _tax2;
        private int? _tax3;
        private int? _status;
        private int? _qty1;
        private double? _qty1CostCentreProfit;
        private double? _CostCentreProfitBroker;
        private double? _qty1BaseCharge1;
        private double? _baseChargeBroker;
        private int? _qty1MarkUpID1;
        private double? _qty1MarkUpPercentageValue;
        private double? _qty1MarkUp1Value;
        private double? _MarkUpValBroker;
        private double? _qty1NetTotal;
        private double? _NetTotalBroker;
        private double? _qty1Tax1Value;
        private double? _TaxValBroker;
        private double? _qty1Tax2Value;
        private double? _qty1Tax3Value;
        private double? _qty1GrossTotal;
        private double? _grossTotalBroker;
        private int? _refItemID;
        private int? _templateID;
        public int? TopCategoryID;
        public int? _ProductionTime;
        public string _StockName;
        public double? _DiscountedAmount;
        public long? _DiscountedVoucherId;
    

        #region Primitive Properties

        public Int32 ParentCategoryID
        {
            get;
            set;
        }

        public bool AllowBriefAttachments
        {
            get;
            set;
        }

        public string BriefSuccessMessage
        {
            get;
            set;
        }

        public int? TemplateID
        {
            get { return _templateID; }
            set { _templateID = value; }
        }



        public long ItemID
        {
            get;
            set;
        }

        public Int32 SortOrder
        {
            get;
            set;
        }


        public long RelatedItemID
        {
            get;
            set;
        }


        public Nullable<Int64> EstimateID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }



        public string ProductCategoryName
        {
            get;
            set;
        }


        public Nullable<long> ProductCategoryID
        {
            get;
            set;
        }

        public string ImagePath
        {
            get;
            set;
        }

        public string ThumbnailPath
        {
            get;
            set;
        }

        public string IconPath
        {
            get;
            set;
        }

        public int? InvoiceID
        {
            get { return _invoiceID; }
            set { _invoiceID = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int? Tax1
        {
            get { return _tax1; }
            set { _tax1 = value; }
        }

        public int? Tax2
        {
            get { return _tax2; }
            set { _tax2 = value; }
        }
        public string PaperType
        {
            get { return _StockName; }
            set { _StockName = value; }
        }

        public int? Tax3
        {
            get { return _tax3; }
            set { _tax3 = value; }
        }

        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int? Qty1
        {
            get { return _qty1; }
            set { _qty1 = value; }
        }

        public double? Qty1CostCentreProfit
        {
            get { return _qty1CostCentreProfit; }
            set { _qty1CostCentreProfit = value; }
        }

        public double? CostCentreProfitBroker
        {
            get { return _CostCentreProfitBroker; }
            set { _CostCentreProfitBroker = value; }
        }

        public double? Qty1BaseCharge1
        {
            get { return _qty1BaseCharge1; }
            set { _qty1BaseCharge1 = value; }
        }

        public double? BaseChargeBroker
        {
            get { return _baseChargeBroker; }
            set { _baseChargeBroker = value; }
        }

        public int? Qty1MarkUpID1
        {
            get { return _qty1MarkUpID1; }
            set { _qty1MarkUpID1 = value; }
        }

        public double? Qty1MarkUpPercentageValue
        {
            get { return _qty1MarkUpPercentageValue; }
            set { _qty1MarkUpPercentageValue = value; }
        }

        public double? Qty1MarkUp1Value
        {
            get { return _qty1MarkUp1Value; }
            set { _qty1MarkUp1Value = value; }
        }

        public double? MarkUpValBroker
        {
            get { return _MarkUpValBroker; }
            set { _MarkUpValBroker = value; }
        }


        public double? Qty1NetTotal
        {
            get { return _qty1NetTotal; }
            set { _qty1NetTotal = value; }
        }

        public double? NetTotalBroker
        {
            get { return _NetTotalBroker; }
            set { _NetTotalBroker = value; }
        }


        public double? Qty1Tax1Value
        {
            get { return _qty1Tax1Value; }
            set { _qty1Tax1Value = value; }
        }

        public double? TaxValBroker
        {
            get { return _TaxValBroker; }
            set { _TaxValBroker = value; }
        }

        public double? Qty1Tax2Value
        {
            get { return _qty1Tax2Value; }
            set { _qty1Tax2Value = value; }
        }


        public double? Qty1Tax3Value
        {
            get { return _qty1Tax3Value; }
            set { _qty1Tax3Value = value; }
        }

        public double? Qty1GrossTotal
        {
            get { return _qty1GrossTotal; }
            set { _qty1GrossTotal = value; }
        }

        public double? GrossTotalBroker
        {
            get { return _grossTotalBroker; }
            set { _grossTotalBroker = value; }
        }

        public int? RefItemID
        {
            get { return _refItemID; }
            set { _refItemID = value; }
        }


        public double MinPrice
        {
            get;
            set;
        }

        public int? EstimateProductionTime
        {
            get { return _ProductionTime; }
            set { _ProductionTime = value; }
        }

        public Nullable<bool> IsEnabled
        {
            get;
            set;
        }

        public Nullable<int> ProductType
        {
            get;
            set;
        }


        public Nullable<bool> IsSpecialItem
        {
            get;
            set;
        }
        public Nullable<bool> IsPopular
        {
            get;
            set;
        }

        public Nullable<bool> IsFeatured
        {
            get;
            set;
        }

        public Nullable<bool> IsPromotional
        {
            get;
            set;
        }


        public Nullable<bool> IsPublished
        {
            get;
            set;
        }

        public double BrokerMarkup
        {
            get;
            set;
        }

        public double ContactMarkup
        {
            get;
            set;
        }

        public string ProductSpecification
        {
            get;
            set;
        }

        public string ProductWebDescription
        {
            get;
            set;
        }

        public string CompleteSpecification
        {
            get;
            set;
        }

        public string TipsAndHints
        {
            get;
            set;
        }

        public ArtWorkAttatchment Attatchment
        {
            get;
            set;
        }

        public Nullable<bool> IsDisplayToUser
        {
            get;
            set;
        }

        public Nullable<bool> IsQtyRanged
        {
            get;
            set;
        }


        public string NavPath
        {
            get;
            set;
        }


        public Nullable<bool> IsFinishedGoods
        {
            get;
            set;
        }


        public Nullable<int> IsSelectedBizCard
        {
            get;
            set;
        }

        #endregion

        #region OfferProperties

        public Nullable<Int32> OfferID
        {
            get;
            set;
        }

        public Nullable<Int32> OfferType
        {
            get;
            set;
        }

        public Nullable<Int32> ItemType
        {
            get;
            set;
        }

        public double? DiscountedAmount
        {
            get { return _DiscountedAmount; }
            set { _DiscountedAmount = value; }
        }

        public long? DiscountedVoucherId
        {
            get { return _DiscountedVoucherId; }
            set { _DiscountedVoucherId = value; }
        }

        public List<ItemAttachment> OtherItemAttatchments
        {
            get;
            set;
        }
        #endregion
    }
}
