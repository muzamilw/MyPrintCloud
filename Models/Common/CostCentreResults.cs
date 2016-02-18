
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace MPC.Models.Common
{
    /// <summary>
    /// Data modal class for the Price result of Cost centre Execution
    /// </summary>
    /// 
    [Serializable()]
    public class CostCentrePriceResult
    {
        private double _PlantPrice;
        private double _LabourPrice;
        private double _MaterialPrice;

        private double _TotalPrice;
        private double _MinimumCharge;
        private double _SetupCost;

        private double _VA;
        public double TotalPriceQty2 { get; set; }
        public double TotalPriceQty3 { get; set; }
        /// <summary>
        /// default constructor
        /// </summary>

        public CostCentrePriceResult()
        {
            //init
            this._PlantPrice = 0;
            this._LabourPrice = 0;
            this._MaterialPrice = 0;
            this._TotalPrice = 0;

            this._MinimumCharge = 0;
            this._SetupCost = 0;

            this._VA = 0;

        }

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="PlantPrice">PlantPrice</param>
        /// <param name="LabourPrice">LabourPrice</param>
        /// <param name="MaterialPrice">MaterialPrice</param>
        public CostCentrePriceResult(double PlantPrice, double LabourPrice, double MaterialPrice, double TotalPrice, double MinimumCharge, double SetupCost, double VA)
        {
            this._PlantPrice = PlantPrice;
            this._LabourPrice = LabourPrice;
            this._MaterialPrice = MaterialPrice;
            this._TotalPrice = TotalPrice;

            this._MinimumCharge = MinimumCharge;
            this._SetupCost = SetupCost;
            this._VA = VA;
        }

        //public properties
        public double PlantPrice
        {



            get { return this._PlantPrice; }
            set { this._PlantPrice = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double LabourPrice
        {
            get { return this._LabourPrice; }
            set { this._LabourPrice = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double MaterialPrice
        {
            get { return this._MaterialPrice; }
            set { this._MaterialPrice = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double TotalPrice
        {
            get { return this._TotalPrice; }
            set { this._TotalPrice = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double MinimumCharge
        {
            get { return this._MinimumCharge; }
            set { this._MinimumCharge = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double SetupCost
        {
            get { return this._SetupCost; }
            set { this._SetupCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double VA
        {
            get { return this._VA; }
            set { this._VA = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }


    }

    /// <summary>
    /// Data modal class for the Cost result of Cost centre Execution
    /// </summary>
    /// 
    [Serializable()]
    public class CostCentreCostResult
    {
        private double _PlantCost;
        private double _LabourCost;
        private double _MaterialCost;
        private int _Time;
        private double _TotalCost;
        private double _MinimumCharge;
        private bool _IsMinimumChargeApplied = false;

        private double _SetupCost;
        /// <summary>
        /// default constructor
        /// </summary>
        public CostCentreCostResult()
        {
            this._PlantCost = 0;
            this._LabourCost = 0;
            this._MaterialCost = 0;
            this._Time = 0;
            this._TotalCost = 0;
            this._MinimumCharge = 0;
            this._SetupCost = 0;
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="PlantCost">PlantCost</param>
        /// <param name="LabourCost">LabourCost</param>
        /// <param name="MaterialCost">MaterialCost</param>
        public CostCentreCostResult(double PlantCost, double LabourCost, double MaterialCost, double TotalCost, double MinimumCharge, double SetupCost, int Time)
        {
            this._PlantCost = PlantCost;
            this._LabourCost = LabourCost;
            this._MaterialCost = MaterialCost;
            this._Time = Time;
            this._TotalCost = TotalCost;
            this._MinimumCharge = MinimumCharge;
            this._SetupCost = SetupCost;
        }

        //public properties
        public double PlantCost
        {
            get { return this._PlantCost; }
            set { this._PlantCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double LabourCost
        {
            get { return this._LabourCost; }
            set { this._LabourCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double MaterialCost
        {
            get { return this._MaterialCost; }
            set { this._MaterialCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double TotalCost
        {
            get { return this._TotalCost; }
            set { this._TotalCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double MinimumCharge
        {
            get { return this._MinimumCharge; }
            set { this._MinimumCharge = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double SetupCost
        {
            get { return this._SetupCost; }
            set { this._SetupCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public int EstimatedTime
        {
            get { return this._Time; }
            set { this._Time = value; }
        }

        public bool IsMinimumChargeApplied
        {
            get { return this._IsMinimumChargeApplied; }
            set { this._IsMinimumChargeApplied = value; }
        }

    }

    /// <summary>
    /// Data modal class for the Actual Cost result of Cost centre Execution
    /// </summary>
    [Serializable()]
    public class CostCentreActualCostResult
    {
        private double _PlantCost;
        private double _LabourCost;
        private double _MaterialCost;
        private int _Time;
        private double _TotalCost;
        private double _MinimumCharge;

        private double _SetupCost;
        /// <summary>
        /// default constructor
        /// </summary>
        public CostCentreActualCostResult()
        {
            this._PlantCost = 0;
            this._LabourCost = 0;
            this._MaterialCost = 0;
            this._Time = 0;
            this._TotalCost = 0;
            this._MinimumCharge = 0;
            this._SetupCost = 0;
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="PlantCost">PlantCost</param>
        /// <param name="LabourCost">LabourCost</param>
        /// <param name="MaterialCost">MaterialCost</param>
        public CostCentreActualCostResult(double ActualPlantCost, double ActualLabourCost, double ActualMaterialCost, double ActualTotalCost, double MinimumCharge, double SetupCost, int ActualTime)
        {
            this._PlantCost = ActualPlantCost;
            this._LabourCost = ActualLabourCost;
            this._MaterialCost = ActualMaterialCost;
            this._Time = ActualTime;
            this._TotalCost = ActualTotalCost;
            this._MinimumCharge = MinimumCharge;
            this._SetupCost = SetupCost;
        }

        //public properties
        public double ActualPlantCost
        {
            get { return this._PlantCost; }
            set { this._PlantCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double ActualLabourCost
        {
            get { return this._LabourCost; }
            set { this._LabourCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double ActualMaterialCost
        {
            get { return this._MaterialCost; }
            set { this._MaterialCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double ActualTotalCost
        {
            get { return this._TotalCost; }
            set { this._TotalCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double MinimumCharge
        {
            get { return this._MinimumCharge; }
            set { this._MinimumCharge = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public double SetupCost
        {
            get { return this._SetupCost; }
            set { this._SetupCost = Convert.ToDouble((double.IsInfinity(value) ? 0 : (double.IsNaN(value) ? 0 : value))); }
        }

        public int ActualTime
        {
            get { return this._Time; }
            set { this._Time = value; }
        }

    }

    public class QuestionAndInputQueues
    {
        public List<QuestionQueueItem> QuestionQueues;
        public List<InputQueueItem> InputQueues;
    }
}

