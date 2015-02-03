using System;
using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class CostCenterService : ICostCentersService
    {
        #region Private

        private readonly ICostCentreRepository _costCenterRepository;
        private readonly IChartOfAccountRepository _chartOfAccountRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ICostCenterTypeRepository _costcentreTypeRepository;
        private readonly IMarkupRepository _markupRepository;
        private readonly ICostCentreVariableRepository _costCentreVariableRepository;
        #endregion

        #region Constructor

        public CostCenterService(ICostCentreRepository costCenterRepository, IChartOfAccountRepository chartOfAccountRepository, ISystemUserRepository systemUserRepository, ICostCenterTypeRepository costCenterTypeRepository,
            IMarkupRepository markupRepository, ICostCentreVariableRepository costCentreVariableRepository)
        {
            if (costCenterRepository == null)
            {
                throw new ArgumentNullException("costCenterRepository");
            }
            if (chartOfAccountRepository == null)
            {
                throw new ArgumentNullException("chartOfAccountRepository");
            }
            if (systemUserRepository == null)
            {
                throw new ArgumentNullException("systemUserRepository");
            }
            if (costCenterTypeRepository == null)
            {
                throw new ArgumentNullException("costCenterTypeRepository");
            }
            if (markupRepository == null)
            {
                throw new ArgumentNullException("markupRepository");
            }
            if (costCentreVariableRepository == null)
            {
                throw new ArgumentNullException("costCentreVariableRepository");
            }
            this._costCenterRepository = costCenterRepository;
            this._chartOfAccountRepository = chartOfAccountRepository;
            this._systemUserRepository = systemUserRepository;
            this._costcentreTypeRepository = costCenterTypeRepository;
            this._markupRepository = markupRepository;
            this._costCentreVariableRepository = costCentreVariableRepository;
        }

        #endregion

        #region Public
        
        public IEnumerable<CostCentre> GetAll(CostCenterRequestModel request)
        {
            return _costCenterRepository.GetAllNonSystemCostCentres();
        }

        public CostCentre Add(CostCentre costcenter)
        {
           // _costCenterRepository.Add(costcenter);
          //  _costCenterRepository.SaveChanges();
            return costcenter;
        }

        public CostCentre Update(CostCentre costcenter)
        {
           // _costCenterRepository.Update(costcenter);
           // _costCenterRepository.SaveChanges();
            return costcenter;
        }
        public bool Delete(long costcenterId)
        {
          //  _costCenterRepository.Delete(GetCostCentreById(costcenterId));
          //  _costCenterRepository.SaveChanges();
            return true;
        }

        public CostCentre GetCostCentreById(long id)
        {
             return _costCenterRepository.GetCostCentreByID(id);
        }
        public CostCentersResponse GetUserDefinedCostCenters(CostCenterRequestModel request)
        {
            return _costCenterRepository.GetUserDefinedCostCenters(request);
        }

        public CostCenterBaseResponse GetBaseData()
        {
            return new CostCenterBaseResponse
            {
                CostCenterCategories = _costcentreTypeRepository.GetAll(),
                CostCenterResources = _systemUserRepository.GetAll(),
                NominalCodes = _chartOfAccountRepository.GetAll(),
                Markups = _markupRepository.GetAll(),
                CostCentreVariables = _costCentreVariableRepository.returnLoadVariableList()
            };
        }


//        private void LoadTree()
//{
//    //Dim oConnection As MySqlConnection
//    try {
//        //    oConnection = BLL.Common.GetConnection


//        DataTable oTblCCCategories = default(DataTable);
//        DataTable oTblCostCentres = default(DataTable);
//        DataTable oTblVarCategories = default(DataTable);
//        DataTable oTblVariables = default(DataTable);
//        DataTable oTBlQuestions = default(DataTable);
//        DataTable oTblMatrices = default(DataTable);
//        DataRow oRow = default(DataRow);
//        DataRow oRow1 = default(DataRow);
//        DataRow[] oRowList = null;

//        RefTree.Nodes.Clear();


//        oTblCCCategories = BLL.CostCentres.CostCentre.ReturnCostCentreCategories(Common.g_GlobalData);
//        oTblCostCentres = BLL.CostCentres.CostCentre.GetCostCentreList(Common.g_GlobalData);
//        //Adding Cost Centre Root Node
//        UltraTreeNode oRootNode = default(UltraTreeNode);
//        UltraTreeNode oCcCatNode = default(UltraTreeNode);
//        UltraTreeNode oCcNode = default(UltraTreeNode);
//        UltraTreeNode oCcParamNode = default(UltraTreeNode);

//        RefTree.Nodes.Add(new UltraTreeNode());
//        RefTree.Nodes(0).Text = "CostCentres";
//        RefTree.Nodes(0).DataKey = "ccroot";
//        RefTree.Nodes(0).Tag = "null";
//        RefTree.Nodes(0).Override.NodeAppearance.Image = new Bitmap(typeof(UI.MainForm), "costcentre.gif");


//        //Dim oStyle As Style

//        //iterating in the top level Categories
//        int iCounter = 0;
//        int ycounter = 0;
//        Bitmap oBitmap = default(Bitmap);


//        foreach ( oRow in oTblCCCategories.Rows) {
//            if (Convert.ToInt32(oRow.Item("TypeID")) == 2) {
//                oBitmap = new Bitmap(typeof(UI.MainForm), "CC-origination.gif");
//            } else if (Convert.ToInt32(oRow.Item("TypeID")) == 4) {
//                oBitmap = new Bitmap(typeof(UI.MainForm), "CC-prePress.gif");
//            } else if (Convert.ToInt32(oRow.Item("TypeID")) == 6) {
//                oBitmap = new Bitmap(typeof(UI.MainForm), "CC-postPress.gif");
//            } else if (Convert.ToInt32(oRow.Item("TypeID")) == 8) {
//                oBitmap = new Bitmap(typeof(UI.MainForm), "CC-others.gif");
//            } else if (Convert.ToInt32(oRow.Item("TypeID")) == 9) {
//                oBitmap = new Bitmap(typeof(UI.MainForm), "CC-bindry.gif");
//            } else if (Convert.ToInt32(oRow.Item("TypeID")) == 10) {
//                oBitmap = new Bitmap(typeof(UI.MainForm), "CC-finishing.gif");
//            } else if (Convert.ToInt32(oRow.Item("TypeID")) == 11) {
//                oBitmap = new Bitmap(typeof(UI.MainForm), "CC-supliyer.gif");
//            }
//            //adding the node
//            RefTree.Nodes(0).Nodes.Add(new UltraTreeNode());
//            RefTree.Nodes(0).Nodes(iCounter).Text = Convert.ToString(oRow.Item("TypeName"));
//            RefTree.Nodes(0).Nodes(iCounter).DataKey = "cccat";
//            RefTree.Nodes(0).Nodes(iCounter).Tag = "null";
//            RefTree.Nodes(0).Nodes(iCounter).Override.NodeAppearance.Image = oBitmap;


//            //finding the costcentres from costcentreTable
//            oRowList = oTblCostCentres.Select("type = " + oRow.Item("TypeID").ToString);

//            ycounter = 0;
//            //'iterating in the searched List of CostcEntres.. and now adding their sub nodes
//            foreach ( oRow1 in oRowList) {
//                RefTree.Nodes(0).Nodes(iCounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Text = Convert.ToString(oRow1.Item("Name"));
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Override.NodeAppearance.Image = oBitmap;

//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).DataKey = "ccname";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Tag = "null";
//                //RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).ImageUrl = "../../images/icons/costcentre.gif"

//                //    'we have one cost centre.. adding 4 sub nodes nodes

//                //adding two properties Total Price and Toral Cost
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(0).Text = "Total Cost";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(0).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(0).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##TotalCost##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(0).Override.NodeAppearance.Image = oBitmap;

//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(1).Text = "Total Price";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(1).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(1).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##TotalPrice##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(1).Override.NodeAppearance.Image = oBitmap;

//                //cost centre Properties w.r.t Price and Cost 

//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(2).Text = "Plant Cost";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(2).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(2).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##PlantCost##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(2).Override.NodeAppearance.Image = oBitmap;

//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(3).Text = "Resource Cost";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(3).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(3).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##ResourceCost##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(3).Override.NodeAppearance.Image = oBitmap;

//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(4).Text = "Stock Cost";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(4).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(4).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##StockCost##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(4).Override.NodeAppearance.Image = oBitmap;

//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(5).Text = "Plant Price";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(5).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(5).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##PlantPrice##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(5).Override.NodeAppearance.Image = oBitmap;

//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(6).Text = "Resource Price";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(6).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(6).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##ResourcePrice##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(6).Override.NodeAppearance.Image = oBitmap;


//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(7).Text = "StockPrice";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(7).DataKey = "ccproperty";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(7).Tag = "{SubCostCentre, ID=##" + oRow1.Item("CostCentreID").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##,ReturnValue=##StockPrice##}";
//                RefTree.Nodes(0).Nodes(iCounter).Nodes(ycounter).Nodes(7).Override.NodeAppearance.Image = oBitmap;


//                ycounter += 1;

//            }

//            //oCCNode.Nodes.Add(New Infragistics.WebUI.UltraWebNavigator.Node(oRow.Item("TypeName"), oStyle))
//            iCounter += 1;
//        }
//        oBitmap = null;
//        oTblCCCategories = null;
//        oTblCostCentres = null;

//        //adding variables

//        RefTree.Nodes.Add(new UltraTreeNode());
//        RefTree.Nodes(1).Text = "Variables";
//        RefTree.Nodes(1).DataKey = "varroot";
//        RefTree.Nodes(1).Tag = "null";
//        RefTree.Nodes(1).Override.NodeAppearance.Image = new Bitmap(typeof(UI.MainForm), "variables.gif");

//        //loading variable Related Information
//        oTblVarCategories = BLL.CostCentres.Variables.returnVariableCateogories(Common.g_GlobalData);
//        oTblVariables = BLL.CostCentres.Variables.returnLoadVariableList(Common.g_GlobalData);

//        //iterating in categories
//        iCounter = 0;
//        foreach ( oRow in oTblVarCategories.Rows) {
//            Bitmap ooBitmap = default(Bitmap);
//            //Booklet 
//            if (Convert.ToInt32(oRow.Item("CategoryID")) == 1) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-booklet.gif");
//            //Charge
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 2) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-charges.gif");
//            //Colors
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 3) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-colors.gif");
//            //Guilotine
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 4) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-guillotine.gif");
//            //Imposition 
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 5) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-imposition.gif");
//            //Item quantity
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 6) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-itemQuantity.gif");
//            //Plate film
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 7) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-filmPlate.gif");
//            //Press Makeready
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 8) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-pressMakeready.gif");
//            //Press Variables
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 9) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-press.gif");
//            //Section
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 10) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-section.gif");
//            //stock
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 11) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-stock.gif");
//            //weight
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 12) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-weight.gif");
//            //costcente
//            } else if (Convert.ToInt32(oRow.Item("CategoryID")) == 13) {
//                ooBitmap = new Bitmap(typeof(UI.MainForm), "V-costcentre.gif");
//            }
//            //adding the node

//            RefTree.Nodes(1).Nodes.Add(new UltraTreeNode());
//            RefTree.Nodes(1).Nodes(iCounter).Text = Convert.ToString(oRow.Item("Name"));
//            RefTree.Nodes(1).Nodes(iCounter).DataKey = "varcat";
//            RefTree.Nodes(1).Nodes(iCounter).Tag = "null";
//            RefTree.Nodes(1).Nodes(iCounter).Override.NodeAppearance.Image = ooBitmap;

//            ///If iCounter = 0 Then
//            ///    RefTree.Nodes(1).Nodes(iCounter).ImageUrl = "../../images/icons/plant-variable.gif"
//            ///ElseIf iCounter = 1 Then
//            ///    RefTree.Nodes(1).Nodes(iCounter).ImageUrl = "../../images/icons/estimate-variable.gif"
//            ///ElseIf iCounter = 2 Then
//            ///    RefTree.Nodes(1).Nodes(iCounter).ImageUrl = "../../images/icons/other-variable.gif"
//            ///Else
//            ///    RefTree.Nodes(1).Nodes(iCounter).ImageUrl = "../../images/icons/other-variable.gif"
//            ///End If

//            //finding the costcentres from costcentreTable
//            oRowList = oTblVariables.Select("CategoryID = " + oRow.Item("CategoryId").ToString);

//            ycounter = 0;
//            //'iterating in the searched List of CostcEntres.. and now adding their sub nodes

//            foreach ( oRow1 in oRowList) {
//                RefTree.Nodes(1).Nodes(iCounter).Nodes.Add(new UltraTreeNode());
//                RefTree.Nodes(1).Nodes(iCounter).Nodes(ycounter).Text = Convert.ToString(oRow1.Item("Name"));
//                RefTree.Nodes(1).Nodes(iCounter).Nodes(ycounter).DataKey = "varname";
//                RefTree.Nodes(1).Nodes(iCounter).Nodes(ycounter).Tag = "{SystemVariable, ID=##" + oRow1.Item("VarId").ToString + "##,Name=##" + Convert.ToString(oRow1.Item("Name")) + "##}";
//                RefTree.Nodes(1).Nodes(iCounter).Nodes(ycounter).Override.NodeAppearance.Image = ooBitmap;

//                //If iCounter = 0 Then
//                //    RefTree.Nodes(1).Nodes(iCounter).Nodes(ycounter).ImageUrl = "../../images/icons/plant-variable.gif"
//                //ElseIf iCounter = 1 Then
//                //    RefTree.Nodes(1).Nodes(iCounter).Nodes(ycounter).ImageUrl = "../../images/icons/estimate-variable.gif"
//                //ElseIf iCounter = 2 Then
//                //    RefTree.Nodes(1).Nodes(iCounter).Nodes(ycounter).ImageUrl = "../../images/icons/other-variable.gif"
//                //End If



//                ycounter += 1;
//            }
//            iCounter += 1;
//            ooBitmap = null;
//        }

//        oTblVarCategories = null;
//        oTblVariables = null;




//        //loading Resources
//        DataTable oTblResources = default(DataTable);
//        oTblResources = BLL.UserManager.Users.GetUserTable(Common.g_GlobalData);

//        RefTree.Nodes.Add(new UltraTreeNode());
//        RefTree.Nodes(2).Text = "Resources";
//        RefTree.Nodes(2).DataKey = "resroot";
//        RefTree.Nodes(2).Tag = "null";
//        Bitmap oBitmapR = new Bitmap(typeof(UI.MainForm), "resourses.gif");
//        RefTree.Nodes(2).Override.NodeAppearance.Image = oBitmapR;
//        //iterating in resources
//        iCounter = 0;
//        foreach ( oRow in oTblResources.Rows) {
//            //adding the node
//            RefTree.Nodes(2).Nodes.Add(new UltraTreeNode());
//            RefTree.Nodes(2).Nodes(iCounter).Text = Convert.ToString(oRow.Item("FullName"));
//            RefTree.Nodes(2).Nodes(iCounter).DataKey = "resname";
//            RefTree.Nodes(2).Nodes(iCounter).Tag = "{resource, ID=##" + oRow.Item("SystemUserID").ToString + "##,Name=##" + oRow.Item("FullName").ToString + "##,returnvalue=##costperhour##}";
//            RefTree.Nodes(2).Nodes(iCounter).Override.NodeAppearance.Image = oBitmapR;
//            iCounter += 1;
//        }

//        oBitmapR = null;

//        oTblResources = null;

//        //adding Questions
//        oTBlQuestions = BLL.CostCentres.Questions.LoadQuestionsList(Common.g_GlobalData);

//        RefTree.Nodes.Add(new UltraTreeNode());
//        RefTree.Nodes(3).Text = "Questions";
//        RefTree.Nodes(3).DataKey = "QuestionRoot";
//        RefTree.Nodes(3).Tag = "questionroot";
//        RefTree.Nodes(3).Key = "QuestionRoot";
//        Bitmap oBitmapQ = new Bitmap(typeof(UI.MainForm), "questions.gif");
//        RefTree.Nodes(3).Override.NodeAppearance.Image = oBitmapQ;

//        //iterating in Questions
//        iCounter = 0;
//        foreach ( oRow in oTBlQuestions.Rows) {
//            //adding the node
//            RefTree.Nodes(3).Nodes.Add(new UltraTreeNode());
//            RefTree.Nodes(3).Nodes(iCounter).Text = Convert.ToString(oRow.Item("QuestionString"));
//            RefTree.Nodes(3).Nodes(iCounter).DataKey = oRow.Item("ID").ToString;
//            RefTree.Nodes(3).Nodes(iCounter).Key = "Q" + oRow.Item("ID").ToString;
//            RefTree.Nodes(3).Nodes(iCounter).Tag = "{question, ID=##" + oRow.Item("ID").ToString + "##,caption=##" + Convert.ToString(oRow.Item("QuestionString")) + "##}";
//            RefTree.Nodes(3).Nodes(iCounter).Override.NodeAppearance.Image = oBitmapQ;
//            iCounter += 1;
//        }

//        oBitmapQ = null;

//        oTBlQuestions = null;


//        oTblMatrices = BLL.CostCentres.CostMatix.getCostMatixiesList(Common.g_GlobalData);
//        //adding CostMatrix Node

//        RefTree.Nodes.Add(new UltraTreeNode());
//        RefTree.Nodes(4).Text = "Matrices";
//        RefTree.Nodes(4).Tag = "matricesroot";
//        RefTree.Nodes(4).DataKey = "matricesroot";
//        RefTree.Nodes(4).Key = "matricesroot";
//        Bitmap oBitmapC = new Bitmap(typeof(UI.MainForm), "matrices.gif");
//        RefTree.Nodes(4).Override.NodeAppearance.Image = oBitmapC;
//        //iterating in Questions
//        iCounter = 0;
//        foreach ( oRow in oTblMatrices.Rows) {
//            //adding the node
//            RefTree.Nodes(4).Nodes.Add(new UltraTreeNode());
//            RefTree.Nodes(4).Nodes(iCounter).Text = Convert.ToString(oRow.Item("Name"));
//            RefTree.Nodes(4).Nodes(iCounter).DataKey = oRow.Item("MatrixID").ToString;
//            RefTree.Nodes(4).Nodes(iCounter).Key = "M" + oRow.Item("MatrixID").ToString;
//            RefTree.Nodes(4).Nodes(iCounter).Tag = "{matrix, ID=##" + oRow.Item("MatrixID").ToString + "##,Name=##" + Convert.ToString(oRow.Item("Name")) + "##}";
//            RefTree.Nodes(4).Nodes(iCounter).Override.NodeAppearance.Image = oBitmapC;
//            iCounter += 1;
//        }

//        oBitmapC = null;
//        oTblMatrices.Dispose();

//        DataTable oTbleClickChargeLookupZone = default(DataTable);
//        oTbleClickChargeLookupZone = BLL.LookupMethods.ClickChargeZone.Read(Common.g_GlobalData);
//        //adding Click charge LookupZone Node

//        RefTree.Nodes.Add(new UltraTreeNode());
//        RefTree.Nodes(5).Text = "Lookup";
//        RefTree.Nodes(5).Tag = "Lookuproot";
//        RefTree.Nodes(5).DataKey = "Lookuproot";
//        RefTree.Nodes(5).Key = "Lookuproot";
//        //Dim oBitmapC As Bitmap = New Bitmap(GetType(UI.MainForm), "matrices.gif")
//        //RefTree.Nodes(4).Override.NodeAppearance.Image = oBitmapC
//        //iterating in Questions
//        iCounter = 0;
//        foreach ( oRow in oTbleClickChargeLookupZone.Rows) {
//            //adding the node
//            RefTree.Nodes(5).Nodes.Add(new UltraTreeNode());
//            RefTree.Nodes(5).Nodes(iCounter).Text = Convert.ToString(oRow.Item("Name"));
//            RefTree.Nodes(5).Nodes(iCounter).DataKey = oRow.Item("MethodID").ToString;
//            RefTree.Nodes(5).Nodes(iCounter).Key = "L" + oRow.Item("MethodID").ToString;
//            RefTree.Nodes(5).Nodes(iCounter).Tag = "clickchargezonelookupmethod";
//            // RefTree.Nodes(5).Nodes(iCounter).Override.NodeAppearance.Image = oBitmapC
//            iCounter += 1;
//        }

//        oBitmapC = null;
//        oTblMatrices.Dispose();


//        //adding Stock Node

//        RefTree.Nodes.Add(new UltraTreeNode());
//        RefTree.Nodes(6).Text = "Stock Items";
//        RefTree.Nodes(6).Tag = "stock";
//        RefTree.Nodes(6).DataKey = "stockroot";
//        RefTree.Nodes(6).Override.NodeAppearance.Image = new Bitmap(typeof(UI.MainForm), "stockItem.png");

//        //RefTree.ExpandAll(ExpandAllType.Always)
//    } 
//    catch (Exception ex) 
//    {
//        throw new Exception("Load tree", ex);
//    }
//        }
        #endregion

    }
}
