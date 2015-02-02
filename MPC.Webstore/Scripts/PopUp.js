function ShowPopUp(Type,Message) {
   
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '</div></div>';
   
    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";
    
    var left = parseInt((bws.width - 500) / 2);
    var top = parseInt((bws.height - 170) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

    document.getElementById("innerLayer").style.width = "500px";
    document.getElementById("innerLayer").style.height = "170px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowPopUpDesignNow(Type, Message) {

    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick=DesignNow("1");  >Resume</button><button type="button" onclick=DesignNow("2"); class="btn btn-primary">Create New</button></div></div>';

    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 500) / 2);
    var top = parseInt((bws.height - 170) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

    document.getElementById("innerLayer").style.width = "500px";
    document.getElementById("innerLayer").style.height = "170px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowPopUpMarketingBrief(Type, Message,ProductName,ItemID) {

    ProductName = ProductName.replace('"', '');

 
    ProductName = ProductName.replace(' ', '');
    var ReturnURL = "/MarketingBrief/" + ProductName + "/" + ItemID; 
    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick=RedirectToSignUp("' + ReturnURL + '");  >Register</button><button type="button" onclick=RedirectToLogin("' + ReturnURL + '"); class="btn btn-primary">Login</button></div></div>';

    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 500) / 2);
    var top = parseInt((bws.height - 170) / 2);

    document.getElementById("innerLayer").innerHTML = container;

   // document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

    document.getElementById("innerLayer").style.width = "500px";
    document.getElementById("innerLayer").style.height = "170px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ConfirmDeleteItemPopUP(ItemID,OrderID)
{
    var Path = "/ShopCart/RemoveProduct/" + ItemID + "/" + OrderID;
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick=ConfirmRemove("' + Path + '");  >Yes</button><button type="button" onclick=HideMessagePopUp(); class="btn btn-primary">No</button></div></div></div>';

    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 500) / 2);
    var top = parseInt((bws.height - 170) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

    document.getElementById("innerLayer").style.width = "500px";
    document.getElementById("innerLayer").style.height = "170px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function getBrowserHeight() {
    var intH = 0;
    var intW = 0;
    if (typeof window.innerWidth == 'number') {
        intH = window.innerHeight;
        intW = window.innerWidth;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        intH = document.documentElement.clientHeight;
        intW = document.documentElement.clientWidth;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        intH = document.body.clientHeight;
        intW = document.body.clientWidth;
    }
    return { width: parseInt(intW), height: parseInt(intH) };
}

function HideMessagePopUp() {

    document.getElementById("innerLayer").innerHTML = "";
    document.getElementById("layer").style.display = "none";
    document.getElementById("innerLayer").style.display = "none";

}
function RedirectToLogin(ReturnURL)
{
   
    window.location.href = "/Login?ReturnURL=" + ReturnURL;
   
}
function RedirectToSignUp(ReturnURL)
{
    window.location.href = "/SignUp?ReturnURL=" + ReturnURL;
}
function ShowLoader() {

    var container = '<div class="fancyLoaderCs"><img src="/Content/Images/fancybox_loading.gif" /></div>';

    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 25) / 2);
    var top = parseInt((bws.height - 25) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

    document.getElementById("innerLayer").style.width = "25px";
    document.getElementById("innerLayer").style.height = "25px";

    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ShowArtWorkPopup(Type, panelHtml) {

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + panelHtml + '</div></div>';

    var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "0px";

    document.getElementById("innerLayer").style.width = "730px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ConfirmRemove(Path)
{
 
    window.location.href = Path;
}
function DesignNow(callFrom,EditType,ItemID,TemplateID,TemplateName,ProductName)
{
    if (callFrom == "1") // resume
    {
        window.location.href = "/EditDesign/resume/" + EditType + "/" + ItemID + "/" + TemplateID + "/"  + TemplateName + "/" + ProductName;
        
    }
    else // create new 
    {
        window.location.href = "/EditDesign/new" + EditType + "/" + ItemID + "/" + TemplateID + "/" + TemplateName + "/" + ProductName;
    }
    
}
var CcQueueItems = null;
function ShowCostCentrePopup(CostCentreQueueItems) {
    CcQueueItems = CostCentreQueueItems;
    var innerHtml = "";
   
    for (var i = 0; i < CostCentreQueueItems.length; i++) {

        if (CostCentreQueueItems[i].ItemType == 1) { // text box
            innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + CostCentreQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns" /></div><br/><div class="clearBoth"></div>';
        }

        if (CostCentreQueueItems[i].ItemType == 3) { // drop down
            
            var OptionHtml = "";
            var matrixTable = CostCentreQueueItems[i].MatrixTable;
            for (var a = 0; a < CostCentreQueueItems[i].AnswersTable.length; a++) {
                OptionHtml = OptionHtml + '<option value=' + CostCentreQueueItems[i].AnswersTable[a].QuestionId + ' >' + CostCentreQueueItems[i].AnswersTable[a].AnswerString + '</option>'
            }
            innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                + CostCentreQueueItems[i].VisualQuestion +
                '</label></div><div class="cost-centre-right-container"><select class="cost-centre-dropdowns">'
                + OptionHtml + '</select></div><br/><div class="clearBoth"></div>';
        }
        if(CostCentreQueueItems[i].ItemType == 4) { // formula matrix
            innerHtml = innerHtml +
                '<div class="cost-centre-left-container"><label>Super Formula Matrix</label></div>' +
                '<div class="cost-centre-right-container"><input type="text" disabled="disabled" ' +
                'style="float:left; margin-right:10px;"  /> ' +
                '<input type="button" onclick="ShowFormulaMatrix(' + CostCentreQueueItems[i].RowCount + ',' + CostCentreQueueItems[i].ColumnCount + ',' + i + '); return false;" class="Matrix-select-button rounded_corners5 " value="Select" /></div><div class="clearBoth"></div>';
        }
    }

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title left_align">Please enter the following details of Cost Centre</h4></div><div class="modal-body left_align">'+ innerHtml +'<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" >Continue</button></div></div></div>';


    var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "0px";

    document.getElementById("innerLayer").style.width = "730px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowFormulaMatrix(Rows, Columns, matrixIndex) {
    var MatrixItems = CcQueueItems[matrixIndex].MatrixTable;
    console.log(MatrixItems[0].MatrixId);
    var isFirstSetToEmpty = 0;
    //var container = '  <div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideFormulaPopUp();>&times;</button><h4 class="modal-title left_align">Select Matrix</h4></div><div class="modal-body left_align"><table class="cost-centre-Matrix"><tr><td></td><td>Speed1</td><td>Speed2</td><td>Speed3</td></tr><tr><td>Weight1</td><td><button type="button" class="MatrixOption">Continue</button></td><td><button type="button" class="MatrixOption">Continue</button></td><td><button type="button" class="MatrixOption">Continue</button></td></tr><tr><td>Row2</td><td><button type="button" class="MatrixOption">Continue</button></td><td><button type="button" class="MatrixOption">Continue</button></td><td><button type="button" class="MatrixOption">Continue</button></td></tr><tr><td>Row3</td><td><button type="button" class="MatrixOption">Continue</button></td><td><button type="button" class="MatrixOption">Continue</button></td><td><button type="button" class="MatrixOption">Continue</button></td></tr></table></div></div></div>';
    var GlobalIndex = 0;
    var RowsHtml = "";
    var trHtml = "<tr>";

    for (var row = 0; row < Rows; row++) {
        for (var col = 0; col < Columns; col++) {
            if (col == 0 && isFirstSetToEmpty == 0) {
                isFirstSetToEmpty = 1;
                trHtml = trHtml + '<td></td>'
            } else {
                trHtml = trHtml + '<td><button type="button" class="MatrixOption">' + MatrixItems[col].Value + '</button></td>';
            }
        }
        RowsHtml = RowsHtml + trHtml + "</tr>";
        trHtml = "<tr>";
    }

    var container = '  <div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideFormulaPopUp();>&times;</button><h4 class="modal-title left_align">Select Matrix</h4></div><div class="modal-body left_align"><table class="cost-centre-Matrix">' + RowsHtml + '</table></div></div></div>';

    var bws = getBrowserHeight();

    var shadow = document.getElementById("FormulaMatrixLayer");

   
    var left = parseInt((bws.width - 730) / 2) + 20;

    document.getElementById("FormulaMatrixLayer").innerHTML = container;

    document.getElementById("FormulaMatrixLayer").style.left = left + "px";
    document.getElementById("FormulaMatrixLayer").style.top = "75px";

    document.getElementById("FormulaMatrixLayer").style.width = "700px";
    document.getElementById("FormulaMatrixLayer").style.position = "fixed";
    document.getElementById("FormulaMatrixLayer").style.zIndex = "9999"; 
    document.getElementById("FormulaMatrixLayer").style.boxShadow = "1px 1px 5px #888888";
    document.getElementById("FormulaMatrixLayer").style.display = "block";
}
function HideFormulaPopUp() {

    document.getElementById("FormulaMatrixLayer").style.display = "none";

}