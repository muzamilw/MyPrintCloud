var loc = window.location.toString();
var locVars = loc.split('/');
var var1 = null;
var var2 = null;
var var3 = 1;
var cID = 0;
var cIDv2 = 0;
var tID = 0;
var temp;
var CustomerID = 0;
var organisationId = 0;
var printCropMarks = false;
var printWaterMarks = false;
var orderCode = null;
var CustomerName = null;
var tFonts = [];
var ContactID = 0;
var ItemId = 0
var TP = [];
var TO = [];
var SP = 0;
var SPN = 0;
var V2Url = "http://designerv2.myprintcloud.com/";
var TempHMM = 0;
var TempWMM = 0;
var T0FN = [];
var T0FU = [];
var D1SF = 1.2;
var D1CS = 1;
var D1CZL = 0; var LIFT = true; var TIC = 0; var TotalImgLoaded = 0; var canvas;
var highlightEditableText = false; var IsInputSelected = false;
var isBkPnlUploads = false;
var TempOB = [];
var imToLoad = "";
//old vars
var hasObjects = false;
var IsEmbedded = true;   //l1
var IsCalledFrom = 3;
var Territory = 0;
var Template;
var TempChkQT = 0;
var isPinkCards = false;
var ShowBleedArea = false;
var TORestore = [];
var TPRestore = [];
var NCI = -1;
var ISG = true;
var ISG1 = true;
var SXP = [];
var SYP = [];
var QTD = 0;
var IH =150;
var IW = 150;
var IC = 0;
var MCL = [];
var ICI = 0;
var N1FL = null;
var TOFZ = 10;
var IsBC = false;
var IsBCAlert = false;
var ssMsg = "Please Confirm spellings and details!";
var IsBCFront = true;
var BCBackSide = 1;
var IsBCRoundCorners = false;
var LiImgs = [];
var TeImC = 1;
var GlImC = 1;
var UsImC = 1;
var TeImCBk = 1;
var GlImCBk = 1;
var GlShpCn = 1;
var GlLogCn = 1;
var GlLogCnP = 1;
var UsImCBk = 1;
var GlIlsC = 1;
var GlframC = 1;
var GlBanC = 1;
var GlImEx = true, TeImCEx = true;
var UsImEx = true;
var TeImExBk = true;
var GlImExBk = true;
var GlShpEx = true;
var GlLogEx = true;
var UsImCBkEx = true;
var GlShpCnEx = true;
var GlLogCnEx = true;
var GlLogCnExP = true;
var GlIllsEx = true;
var GlFramesEx = true;
var GlBannerEx = true;
var isBackgroundImg = false;
var imgSelected = 0;
var imgLoaderSection = 1;
var showIpanel = false;
var isImgUpl = false;
var isBKpnl = false;
var llData = [];
var slLLID = 0;
var isLoading = true;
var udCutMar = 0;
var bleedPrinted = false;
var propertyImages = null;
var D1CD = false;
var D1SD = false;
var D1SK = 16, ctrlKey = 17, vKey = 86, cKey = 67;
var D1CO = [];
var D1IFL;
var IsDesignModified = false;   //c05

// c06
var D1DFT = 1;
var IsCoorporate = 0;
var D1ITE = 0;
var D1LP = "";
// c04

var D1CH;
var D1CCW;
var D1CCML;
var D1CCMT;
var D1CCOI;
var D1CCCO;
var N1LA = 0;
var N111a = 0;
var lCCTxt = "";
var dfZ1l = 1;
var tcListCc = 0, tcRowCount = 0, tcLltemp = 1, tcImHh = 220, tcImThh = 0, tcAllcc = false, CzRnd = 193, selCat = "00", isImgPaCl = false, isBkPaCl = false, SelBkCat = "00";
var isUpPaCl = false, SelUpCat = "00";
var isAddPaCl = false, SelAddCat = "00";
var crX = 0;
var crY = 0;
var crWd = 0;
var crHe = 0;
var crv1 = 0;
var crv2 = 0;
var crv3 = 0;
var crv4 = 0;
var crv5 = 0;
var globalTemplateId = 0;
var showEBtn = true;
var panelMode = 1;
var firstLoad = true, loaderLoading = false,designerFirstLoad = true;
var lAObj = 0;
var spPanel = "";
var spBkPanel = "";
var previewUrl = "/designerapi/Template/Preview/";
var productionFolderPath = "";
var allowPdfDownload = false;
var allowImgDownload = false;
var isMultiPageProduct = false;
var varList = [];// var varExtensions = [];
var isRealestateproduct = false;
var item =  null;
var smartFormData = null;
var selectedUserProfile = null;
var userVariableData = null;
var smartFormClicked = true;
var lstVariableExtensions = null;
var productDimensionUpdated = false;
var objectsSelectable = true;
var selectedPathIndex = 0;
var conversionRatio = 1; // from points to system unit 
var conversionUnit = "Points";
var lastSel = "";
var isImageUploaded = false;
var userTerritoryId = 0;
function buildParams() {
    userTerritoryId = locVars[locVars.length - 1];
	printCropMarks = locVars[locVars.length - 4];
	printWaterMarks = locVars[locVars.length - 3];
	CustomerName =parseInt(  locVars[locVars.length - 8]);
	tID = parseInt(locVars[locVars.length - 10]);
	IsCalledFrom =parseInt(  locVars[locVars.length - 6]);
	IsEmbedded = locVars[locVars.length - 2];
	CustomerID = parseInt( locVars[locVars.length - 8]);
	ContactID =parseInt(  locVars[locVars.length - 7]);
	organisationId = parseInt( locVars[locVars.length - 5]);
	cIDv2 =parseInt( locVars[locVars.length - 11]);
	productionFolderPath = "Organisation" + organisationId + "/Templates/";
	ItemId = parseInt(locVars[locVars.length - 9]);
    //alert(ItemId);
	LoadBasicTemplateSettings();
	var tempName = locVars[locVars.length - 12];
	while (tempName.indexOf('%20') != -1)
	    tempName = tempName.replace("%20", " ");
	$("#txtTemplateTitle").val(tempName);
	//if(IsCalledFrom == 3)
	//{
	//    panelMode = 2;
	//}
	
}
function LoadBasicTemplateSettings() {
    if (cIDv2 == 0)
    {
        $("#Template").css("visibility", "hidden");
        $("#btnAdd").click();
    }
    if(IsCalledFrom == 2)
    {
        //corp admin
     
    } else if (IsCalledFrom == 3 || IsCalledFrom == 4)
    {
        $(".adminControl").css("display", "none");
    } 
}
function restrictControls() {
    $("#btnAdd").css("visibility", "hidden"); 
    $("#Quick").click();
    $("#btnMenuCopy").css("visibility", "hidden");
    $("#btnMenuPaste").css("visibility", "hidden");
    $("#backgrounds").css("visibility", "hidden");
    $("#layersPanel,.btnAUploadFont").css("visibility", "hidden");
    $("#selectedTab").addClass("restrictedSelectedTab");
    $(".btnBackFromImgs").css("visibility", "hidden");
}

var difFound = false;
var reArrangeAttempted = false;
var oldPageId = 0;
var unloadedPageList = [];