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

    function ShowPopUpDesignNow(Type, Message,EditType,ItemID,TemplateID) {
       
       
        if (EditType == "SameTemplate")
        {
            var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick=DesignNow(' + 1 + ',"SameTemplate",' + ItemID + ',' + TemplateID + ');  >Resume</button><button type="button" onclick=DesignNow(' + 2 + ',"SameTemplate",' + ItemID + ',' + TemplateID + '); class="btn btn-primary">Create New</button></div></div>';
        }
        else if(EditType == "SameItem")
        {
            var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick=DesignNow(' + 1 + ',"SameItem",' + ItemID + ',' + TemplateID + ');  >Resume</button><button type="button" onclick=DesignNow(' + 2 + ',"SameItem",'  + ItemID + ',' + TemplateID + '); class="btn btn-primary">Create New</button></div></div>';
        }


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
    var Type = "Alert!";
    var Message = "Are you sure you want to remove this item from your shopping cart?"
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="submit" class="btn btn-primary" onclick=ConfirmRemove('+ItemID+','+OrderID+'); value="Yes" /><button type="button" onclick=HideMessagePopUp(); class="btn btn-primary">No</button></div></div></div>';

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
  
    return false;
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
function ConfirmRemove(ItemID,OrderID)
{
   
    ShowLoader();
    window.location.href = "/RemoveProduct/" + ItemID + "/" + OrderID;
}
function ConfirmRemoveSaveDesign(ItemID) {

    ShowLoader();
    window.location.href = "/RemoveProduct/" + ItemID;
}
function DesignNow(callFrom,EditType,ItemID,TemplateID)
{
 
    if (callFrom == "1") // resume
    {
        window.location.href = "/EditDesign/resume/" + EditType + "/" + ItemID + "/" + TemplateID;
        
    }
    else // create new 
    {
        window.location.href = "/EditDesign/new/" + EditType + "/" + ItemID + "/" + TemplateID;
    }
    
}
var CcQueueItems = null;
var idsToValidate = "";
function ShowCostCentrePopup(CostCentreQueueItems, CostCentreId, ClonedItemId, SelectedCostCentreCheckBoxId, Mode, Currency, ItemPrice) {
    console.log(CostCentreQueueItems);
    CcQueueItems = CostCentreQueueItems;
    var innerHtml = "";
    var Heading = "Please enter the following details of Cost Centre";
    
    if (Mode == "New") { // prompt in case of newly added cost centre
        for (var i = 0; i < CostCentreQueueItems.length; i++) {

            if (CostCentreQueueItems[i].ItemType == 1) { // text box
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + CostCentreQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + CostCentreQueueItems[i].ID;
                }

                innerHtml = innerHtml +'<div class="cost-centre-left-container"><label>' + CostCentreQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + CostCentreQueueItems[i].ID + ' data-id=' + CostCentreQueueItems[i].ID + ' /></div><br/><div class="clearBoth"></div>';
            }

            if (CostCentreQueueItems[i].ItemType == 3) { // drop down

                var OptionHtml = "";
                var matrixTable = CostCentreQueueItems[i].MatrixTable;
                for (var a = 0; a < CostCentreQueueItems[i].AnswersTable.length; a++) {
                    OptionHtml = OptionHtml + '<option data-id=' + CostCentreQueueItems[i].ID + ' value=' + CostCentreQueueItems[i].AnswersTable[a].AnswerString + ' >' + CostCentreQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                }
                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + CostCentreQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><select id=dropdown' + CostCentreQueueItems[i].ID + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
                    + OptionHtml + '</select></div><br/><div class="clearBoth"></div>';
            }
            if (CostCentreQueueItems[i].ItemType == 2) { // radio


                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + CostCentreQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><input type="radio" checked="checked" name="Group2" id=radioNo' + CostCentreQueueItems[i].ID + ' class="cost-centre-radios CostCentreAnswersQueue" /><label for=radioNo' + CostCentreQueueItems[i].ID + ' >No</label><input type="radio" name="Group2" id=radioYes' + CostCentreQueueItems[i].ID + ' class="cost-centre-radios CostCentreAnswersQueue" /><label for=radioYes' + CostCentreQueueItems[i].ID + ' >Yes</label>' +
                    '</div><br/><div class="clearBoth"></div>';
            }
            if (CostCentreQueueItems[i].ItemType == 4) { // formula matrix

                if (idsToValidate == "") {
                    idsToValidate = 'formulaMatrixBox' + CostCentreQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'formulaMatrixBox' + CostCentreQueueItems[i].ID;
                }

                innerHtml = innerHtml +
                    '<div class="cost-centre-left-container"><label>Super Formula Matrix</label></div>' +
                    '<div class="cost-centre-right-container"><input id=formulaMatrixBox' + CostCentreQueueItems[i].ID + ' type="text" disabled="disabled" ' +
                    'style="float:left; margin-right:10px;"  data-id=' + CostCentreQueueItems[i].ID + ' class="CostCentreAnswersQueue" /> ' +
                    '<input type="button" onclick="ShowFormulaMatrix(' + CostCentreQueueItems[i].RowCount + ',' + CostCentreQueueItems[i].ColumnCount + ',' + i + '); return false;" class="Matrix-select-button rounded_corners5 " value="Select" /></div><div class="clearBoth"></div>';
            }
        }
    } else if (Mode == "Modify") { // prompt in case of added cost centre
        Heading = "Edit Cost Centre details";
        for (var i = 0; i < CostCentreQueueItems.length; i++) {

            if (CostCentreQueueItems[i].ItemType == 1) { // text box
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + CostCentreQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + CostCentreQueueItems[i].ID;
                }

                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + CostCentreQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + CostCentreQueueItems[i].ID + ' data-id=' + CostCentreQueueItems[i].ID + ' value=' + CostCentreQueueItems[i].Qty1Answer + ' /></div><br/><div class="clearBoth"></div>';
            }

            if (CostCentreQueueItems[i].ItemType == 3) { // drop down

                var OptionHtml = "";
                var matrixTable = CostCentreQueueItems[i].MatrixTable;
                for (var a = 0; a < CostCentreQueueItems[i].AnswersTable.length; a++) {
                    if (CostCentreQueueItems[i].AnswersTable[a].AnswerString == CostCentreQueueItems[i].Qty1Answer) {
                        OptionHtml = OptionHtml + '<option data-id=' + CostCentreQueueItems[i].ID + ' value=' + CostCentreQueueItems[i].AnswersTable[a].AnswerString + ' selected>' + CostCentreQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                    } else {
                        OptionHtml = OptionHtml + '<option data-id=' + CostCentreQueueItems[i].ID + ' value=' + CostCentreQueueItems[i].AnswersTable[a].AnswerString + ' >' + CostCentreQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                    }
                    
                }
                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + CostCentreQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><select id=dropdown' + CostCentreQueueItems[i].ID + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
                    + OptionHtml + '</select></div><br/><div class="clearBoth"></div>';
            }
            if (CostCentreQueueItems[i].ItemType == 4) { // formula matrix

                if (idsToValidate == "") {
                    idsToValidate = 'formulaMatrixBox' + CostCentreQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'formulaMatrixBox' + CostCentreQueueItems[i].ID;
                }

                innerHtml = innerHtml +
                    '<div class="cost-centre-left-container"><label>Super Formula Matrix</label></div>' +
                    '<div class="cost-centre-right-container"><input id=formulaMatrixBox' + CostCentreQueueItems[i].ID + ' type="text" disabled="disabled" ' +
                    'style="float:left; margin-right:10px;"  data-id=' + CostCentreQueueItems[i].ID + ' class="CostCentreAnswersQueue" value=' + CostCentreQueueItems[i].Qty1Answer + ' /> ' +
                    '<input type="button" onclick="ShowFormulaMatrix(' + CostCentreQueueItems[i].RowCount + ',' + CostCentreQueueItems[i].ColumnCount + ',' + i + '); return false;" class="Matrix-select-button rounded_corners5 " value="Select" /></div><div class="clearBoth"></div>';
            }
        }
    }
   

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title left_align">' + Heading + '</h4></div><div class="modal-body left_align"><div id="CCErrorMesgContainer"></div>' + innerHtml + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick="ValidateCostCentreControl(' + CostCentreId + ',' + ClonedItemId + ',' + SelectedCostCentreCheckBoxId + ',&#34; ' + Currency + '&#34; ,' + ItemPrice + ');">Continue</button></div></div></div>';
   

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
                if (row == 0 || col == 0) {
                    trHtml = trHtml + '<td>' + MatrixItems[GlobalIndex].Value + '</td>';

                } else {
                    trHtml = trHtml + '<td><button type="button" class="MatrixOption" onclick=SetMatrixAnswer(' + MatrixItems[GlobalIndex].Value + ',' + MatrixItems[GlobalIndex].MatrixId + ');>' + MatrixItems[GlobalIndex].Value + '</button></td>';
                }
                
                
                GlobalIndex = parseInt(GlobalIndex) + 1;
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

function SetMatrixAnswer(Answer, MatrixId)
{
    $("#formulaMatrixBox" + MatrixId).val(Answer);
    document.getElementById("FormulaMatrixLayer").style.display = "none";
}

function ValidateCostCentreControl(CostCentreId, ClonedItemId, SelectedCostCentreCheckBoxId, Currency, ItemPrice) {

    var arrayOfIds = idsToValidate.split(",");
    
    var isDisplyEmptyFieldsMesg = 0;

    var isNotValidInput = 0;

    var isFormulaValidationError = 0;
    for (var i = 0; i < arrayOfIds.length; i++) {
        if (arrayOfIds[i].indexOf("formulaMatrixBox") != -1) {
            
            if ($("#" + arrayOfIds[i]).val() == "") {
                isFormulaValidationError = 1;
                $("#" + arrayOfIds[i]).css("border", "1px solid red");
            } else {
                $("#" + arrayOfIds[i]).css("border", "1px solid #a8a8a8");
            }

        } else {
           
            if ($("#" + arrayOfIds[i]).val() == undefined) {
                $("#" + arrayOfIds[i]).css("border", "1px solid #a8a8a8");
            } else {
                if ($("#" + arrayOfIds[i]).val() == "") {
                    $("#" + arrayOfIds[i]).css("border", "1px solid red");
                    isDisplyEmptyFieldsMesg = 1;
                } else if (isNaN($("#" + arrayOfIds[i]).val())) {
                    isNotValidInput = 1;
                    $("#" + arrayOfIds[i]).css("border", "1px solid red");
                } else {
                    $("#" + arrayOfIds[i]).css("border", "1px solid #a8a8a8");
                }

            }
            
        }
      
       
    }

    if (isDisplyEmptyFieldsMesg == 1) {
        $("#CCErrorMesgContainer").css("display", "block");
        if (isNotValidInput == 1) {
            $("#CCErrorMesgContainer").html("Please enter numbers only to proceed.");
            if (isFormulaValidationError == 1) {
                var html = $("#CCErrorMesgContainer").text() + "<br/> Please select value formula values also."
                $("#CCErrorMesgContainer").html(html);
            }
        } else {
           
            $("#CCErrorMesgContainer").html("Please enter in the hightlighted fields.");
        }
        return;
    } else if (isNotValidInput == 1) {
        $("#CCErrorMesgContainer").css("display", "block");
        $("#CCErrorMesgContainer").html("Please enter numbers only to proceed.");
        if (isFormulaValidationError == 1) {
            var html = $("#CCErrorMesgContainer").text() + "<br/> Please select value formula values also."
            $("#CCErrorMesgContainer").html(html);
        }
        return;
    } else if (isFormulaValidationError == 1) {
        $("#CCErrorMesgContainer").html("Please select value formula values ");
        return;
    } else {
        var desriptionOfCostCentre = "";
        $("#CCErrorMesgContainer").css("display", "none");
        $(".CostCentreAnswersQueue").each(function (i, val) {
            if ($(val).attr('data-id') == undefined) {
                var idofDropDown = $(val).attr('id');
                idofDropDown = "select#" + idofDropDown;
                var idOfQuestion = $(idofDropDown + ' option:selected').attr('data-id');
                $(CcQueueItems).each(function (i, QueueItem) {
                    if (QueueItem.ID == idOfQuestion) {

                        QueueItem.Qty1Answer = $(idofDropDown + ' option:selected').val();
                       
                    }
                });
            } else {
                $(CcQueueItems).each(function (i, QueueItem) {
                    if (QueueItem.ID == $(val).attr('data-id')) {
                       
                        QueueItem.Qty1Answer = $(val).val();
                       
                    }
                });
            }
            if (desriptionOfCostCentre == "") {
                desriptionOfCostCentre =  $(val).parent().prev().children().text() + $(val).val();
            } else {
                desriptionOfCostCentre = desriptionOfCostCentre + "  " + $(val).parent().prev().children().text() + $(val).val() + ".";
            }
        });
         
      
        var jsonObjects = JSON.stringify(CcQueueItems, null, 2);
        var populatedQueuItems = $("#costCentreQueueItems").val();
      
        if (populatedQueuItems != "null") {
            populatedQueuItems = populatedQueuItems + jsonObjects;
            $("#costCentreQueueItems").val(populatedQueuItems);
        }
        else {
            populatedQueuItems = jsonObjects;
            $("#costCentreQueueItems").val(populatedQueuItems);
        }
        
        var to;
        to = "/webstoreapi/costCenter/GetDateTimeString?parameter1=" + CostCentreId + "&parameter2=" + ClonedItemId + "&parameter3=" + $("#VMQuantityOrdered").val() + "&parameter4=New";
        var options = {
            type: "POST",
            url: to,
            data: jsonObjects,
            contentType: "application/json",
            async: true,
            success: function (response) {
                
                ShowLoader();
                var updatedAddOns = jQuery.parseJSON($('#VMJsonAddOns').val());
                if (updatedAddOns != null) {

                    for (var i = 0; i < $(updatedAddOns).length; i++) {
                        if ($(updatedAddOns)[i].CostCenterId == CostCentreId) {
                            $(updatedAddOns)[i].ActualPrice = response;
                            $(updatedAddOns)[i].Description = desriptionOfCostCentre;
                            $(updatedAddOns)[i].CostCentreJasonData = jsonObjects;
                            break;
                        }
                    }

                    var JsonToReSubmit = [];
                    var totalVal = 0;
                    for (var i = 0; i < $(updatedAddOns).length; i++) {
                        JsonToReSubmit.push($(updatedAddOns)[i]);
                        if ($(updatedAddOns)[i].Type == 4) {
                            totalVal = parseFloat(totalVal) + parseFloat(response);
                        } else {
                            var actualP = ($("#VMQuantityOrdered").val() * $(updatedAddOns)[i].ActualPrice) + $(updatedAddOns)[i].SetupCost;
                            if (actualP < $(updatedAddOns)[i].MinimumCost && $(updatedAddOns)[i].MinimumCost != 0) {
                                actualP = $(updatedAddOns)[i].MinimumCost;
                            }
                            totalVal = parseFloat(totalVal) + parseFloat(actualP);
                        }
                    }
                    displayTotalPrice(ItemPrice, totalVal);
                    $("#" + SelectedCostCentreCheckBoxId).next().next().html('<label>' + Currency + response + '</label>' + '<a onclick="PromptForValues(' + CostCentreId + ',' + SelectedCostCentreCheckBoxId + ', 1);" >Modify</a> ');
                    $("#VMJsonAddOns").val(JSON.stringify(JsonToReSubmit));
                }
                HideLoader();
            },
            error: function (msg) { alert("Error occured "); console.log(msg); }
        };
        var returnText = $.ajax(options).responseText;
        
    }
   
    idsToValidate = "";

}
function HideLoader() {

    document.getElementById("layer").style.display = "none";
    document.getElementById("innerLayer").style.display = "none";
}

function ShowOrderingPolicyPopUp(title, Tvalue) {
     
   
     //<textarea name="Text1" cols="40" rows="5" class="rounded_corners5 text_boxPolicy" id="txtPolicy" readonly="readonly" >@ViewBag.CorpCompany.CorporateOrderingPolicy</textarea>
   // var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <input type="text" id="txtOrderPolicy"  class="rounded_corners5 text_box" value=' + Tvalue + ' /><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="Order()">Save</button></div></div>';
    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <textarea type="text" id="txtOrderPolicy" cols="40" rows="5"  class="rounded_corners5 text_box" >' + Tvalue + '</textarea><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="Order()">Save</button><button type="button" id="Close" class="btn btn-primary" onclick="Cancel()">Close</button></div></div>';
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
   // document.getElementById("innerLayer").style.height = "170px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ViewOrderPopUp(Type, panelHtml) {

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';

    var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "0px";
    //730
    document.getElementById("innerLayer").style.width = "883px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ShippingBillingDetails(Type, panelHtml) {

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title" >' + Type + '</h4></div><div class="modal-body">' + panelHtml + '</div></div>';

      var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "0px";
    //730
    document.getElementById("innerLayer").style.width = "778px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ShowResetPassword(Type, panelHtml) {
  
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + panelHtml + '</div></div>';

    var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "0px";

    document.getElementById("innerLayer").style.width = "645px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ConfirmDeleteSaveDesignPopup(ItemID) {

    var Path = "/SavedDesigns/RemoveSaveDesign/" + ItemID;
    var Type = "Alert!";
    var Message = "Are you sure you want to delete this Saved Design?"
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="submit" class="btn btn-primary" onclick=ConfirmRemoveSaveDesign(' + ItemID + '); value="Yes" /><button type="button" onclick=HideMessagePopUp(); class="btn btn-primary">No</button></div></div></div>';

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

    return false;
}
function ConfirmPopUpForApprove() {

    var Type = "Alert!";
    var Message = "Are you sure you want to approve order?"
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="button" id="ApproveOk"  class="btn btn-primary" value="Yes" onclick="Show()" /><input type="button" id="ApproveCancel" class="btn btn-primary" value="No" onclick="HideLoader()"></button></div></div></div>';
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

function ConfirmPopUpForReject() {

    var Type = "Alert!";
    var Message = "Are you sure you want to reject order?"
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input id="Save" type="submit" class="btn btn-primary" value="Yes" /><button type="button" id="cancel" class="btn btn-primary">No</button></div></div></div>';

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
function ShowMyPopUp()
{
    var title = 'Alert';
    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <input type="text" id="Po" cols="40" rows="5"  class="rounded_corners5 text_box" ></input><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="Order()">Save</button><button type="button" id="Close" class="btn btn-primary" onclick="Cancel()">Close</button></div></div>';
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
    // document.getElementById("innerLayer").style.height = "170px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowPopUpForConfirmApprove() {

    var title = 'Po number';
    //<textarea name="Text1" cols="40" rows="5" class="rounded_corners5 text_boxPolicy" id="txtPolicy" readonly="readonly" >@ViewBag.CorpCompany.CorporateOrderingPolicy</textarea>
    // var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <input type="text" id="txtOrderPolicy"  class="rounded_corners5 text_box" value=' + Tvalue + ' /><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="Order()">Save</button></div></div>';
    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <input type="text" id="MyPoNo" class="rounded_corners5 text_box" ></input><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderApproveOK" class="btn btn-primary" onclick="oKApproveOrder()">Yes</button><button type="button" id="Close" class="btn btn-primary"  onclick="HideLoader()">No</button></div></div>';
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
    document.getElementById("innerLayer").style.height = "187px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ConfirmPopUpForReject()
{
    var Type = "Alert!";
    var Message = "Are you sure you want to Reject order?"
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="button" id="ApproveOk"  class="btn btn-primary" value="Yes" onclick="RejectOrder()" /><input type="button" id="ApproveCancel" class="btn btn-primary" value="No" onclick="HideLoader()"></button></div></div></div>';
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

function ShowReceiptLoader() {

    var container = '<div class="md-modal md-effect-7 rounded_corners" id="modal-7"><div class="md-content rounded_corners"><div class="modal-body rounded_corners"><div style="text-align:center; margin-bottom:10px;"><img src="/Content/Images/loading.gif" /></div>Your order is being processed.<br/> Check your inbox for your order receipt.</div></div>';
    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 400) / 2);
    var top = parseInt((bws.height - 50) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

    document.getElementById("innerLayer").style.width = "400px";
    document.getElementById("innerLayer").style.height = "50px";

    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "9999";
    document.getElementById("innerLayer").style.borderRadius = "5px";
   
    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}