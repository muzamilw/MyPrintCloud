function ShowPopUp(Type,Message) {
   
    var container = '<div class="md-modal md-effect-7" id="modal-7" style="border-radius:8px;"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="word-wrap:break-word;max-height:116px;overflow-y:auto;overflow-x:hidden;"  >' + Message + '</div></div>';
   
    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    $("#innerLayer").css('border-radius', '10px');
             
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";
    
    var left = 0;
    var top = parseInt((bws.height - 116) / 2);
    if (bws.width <  640) {
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
    } else {
        left = parseInt((bws.width - 500) / 2);
        document.getElementById("innerLayer").style.width = "500px";
    }

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

   
    document.getElementById("innerLayer").style.height = "116px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "999999";

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
    document.getElementById("innerLayer").style.zIndex = "999999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";

   
}

function ShowPopUpMarketingBrief(Type, Message,ProductName,ItemID, CategoryId) {
 
    var ReturnURL = "/MarketingBrief/" + ProductName + "/" + ItemID + "/" + CategoryId;
    
    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="background:white;">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick=RedirectToSignUp("' + ReturnURL + '");  >Register</button><button type="button" onclick=RedirectToLogin("' + ReturnURL + '"); class="btn btn-primary">Login</button></div></div>';

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
    document.getElementById("innerLayer").style.zIndex = "999999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ConfirmDeleteItemPopUP(ItemID,OrderID)
{
  
    var Path = "/ShopCart/RemoveProduct/" + ItemID + "/" + OrderID;
    var Type = "Alert!";
    var Message = "Are you sure you want to remove this item from your cart?"
    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="submit" class="btn btn-primary" onclick=ConfirmRemove('+ItemID+','+OrderID+'); value="Yes" /><button type="button" onclick=HideMessagePopUp(); class="btn btn-primary">No</button></div></div></div>';

    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = 0;
    var top = parseInt((bws.height - 116) / 2);

    if (bws.width < 640) {
        document.getElementById("innerLayer").style.width = (bws.width) + "px";

    } else {
        left = parseInt((bws.width - 500) / 2);
        document.getElementById("innerLayer").style.width = "500px";
    }
    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

    
    document.getElementById("innerLayer").style.height = "116px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "999999";
    
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

    //var container = '<div class="fancyLoaderCs"><img src="/Content/Images/fancybox_loading.gif" /></div>';
    var container = '<div class="mp-onpageLoader mp-loadercenter" style="display: block;">loading...</div>'
    var bws = getBrowserHeight();
    var shadow = document.getElementById("innerLayer");
    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 25) / 2);
    var top = parseInt((bws.height - 25) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.top = top + "px";
    document.getElementById("innerLayer").style.left = left + "px";

  //  document.getElementById("innerLayer").style.width = "25px";
   // document.getElementById("innerLayer").style.height = "25px";

    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "999999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
    document.getElementById("innerLayer").style.background = "transparent";
}
function ShowArtWorkPopup(Type, panelHtml) {

    
    var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = 0;
    var container = "";
    if (bws.width < 700 && bws.width > 640) {
        //left = parseInt((bws.width) / 2);
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
        container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class=""><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="height: 500px; overflow: scroll; overflow-x: hidden;">' + panelHtml + '</div></div>';

    }
    else if (bws.width == 640) {
        //left = parseInt((bws.width) / 2);
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
        //container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:transparent!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="overflow-y:scroll;height:350px;">' + panelHtml + '</div></div>';
        container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class=""><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="overflow-y:scroll;height:350px;">' + panelHtml + '</div></div>';

    }
    else if (bws.width < 640 && bws.width > 600) {
        //left = parseInt((bws.width) / 2);
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
        container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class=""><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="overflow-y:scroll;height:550px;">' + panelHtml + '</div></div>';

    }
    else if (bws.width < 600 && bws.width > 500) {
        //left = parseInt((bws.width) / 2);
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
        container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:transparent!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="overflow-y:scroll;height:350px;">' + panelHtml + '</div></div>';

    } else if (bws.width < 500) {
        //left = parseInt((bws.width) / 2);
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
        container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:transparent!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="overflow-y:scroll;height:400px;">' + panelHtml + '</div></div>';

    }
    else
    {

        left = parseInt((bws.width - 730) / 2);
        document.getElementById("innerLayer").style.width = "730px";
        container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:none!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + panelHtml + '</div></div>';

       
    }

  

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "0px";
    document.getElementById("innerLayer").style.border = "6px solid darkgray";
    
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "999999";

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

    window.location.href = "/RemoveSaveDesign/" + ItemID;
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
var GlobalQuestionQueueItemsList = null; // question queues of disfferent cost centres will be added to this list 
var idsToValidate = ""; // This variable contain ids of text boxes and validate that each text box must have a correct value
var GlobalInputQueueItemsList = null;
function ShowCostCentrePopup(QuestionQueueItems, CostCentreId, ClonedItemId, SelectedCostCentreCheckBoxId, Mode, Currency, ItemPrice, InputQueueObject, CostCentreType, TaxRate, WorkInstructions) {
    console.log("ShowCostCentrePopup function");
    console.log(QuestionQueueItems);
    GlobalQuestionQueueItemsList = QuestionQueueItems;
    GlobalInputQueueItemsList = InputQueueObject;
    var innerHtml = "";
    var Heading = "Add " + $("#" + SelectedCostCentreCheckBoxId).next().html();
    
    if (Mode == "New") { // prompt in case of newly added cost centre
        for (var i = 0; i < QuestionQueueItems.length; i++) {
            
            if (QuestionQueueItems[i].ItemType == 1) { // text box
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + QuestionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + QuestionQueueItems[i].ID;
                }

                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + QuestionQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + QuestionQueueItems[i].ID + ' data-id=' + QuestionQueueItems[i].ID + ' value=' + parseFloat(QuestionQueueItems[i].DefaultAnswer) + ' /></div><br/><div class="clearBoth"></div>';
            }

            if (QuestionQueueItems[i].ItemType == 3) { // drop down

                var OptionHtml = "";
                var matrixTable = QuestionQueueItems[i].MatrixTable;
                for (var a = 0; a < QuestionQueueItems[i].AnswersTable.length; a++) {
                    OptionHtml = OptionHtml + '<option data-id=' + QuestionQueueItems[i].ID + ' value=' + QuestionQueueItems[i].AnswersTable[a].AnswerString + ' >' + QuestionQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                }
                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + QuestionQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><select id=dropdown' + QuestionQueueItems[i].ID + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
                    + OptionHtml + '</select></div><br/><div class="clearBoth"></div>';
            }
            if (QuestionQueueItems[i].ItemType == 2) { // radio


                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + QuestionQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><input type="radio" checked="checked" name="Group2" id=radioNo' + QuestionQueueItems[i].ID + ' class="cost-centre-radios CostCentreAnswersQueue" /><label for=radioNo' + QuestionQueueItems[i].ID + ' >No</label><input type="radio" name="Group2" id=radioYes' + QuestionQueueItems[i].ID + ' class="cost-centre-radios CostCentreAnswersQueue" /><label for=radioYes' + QuestionQueueItems[i].ID + ' >Yes</label>' +
                    '</div><br/><div class="clearBoth"></div>';
            }
            if (QuestionQueueItems[i].ItemType == 4) { // formula matrix

                if (idsToValidate == "") {
                    idsToValidate = 'formulaMatrixBox' + QuestionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'formulaMatrixBox' + QuestionQueueItems[i].ID;
                }

                innerHtml = innerHtml +
                    '<div class="cost-centre-left-container"><label>Super Formula Matrix</label></div>' +
                    '<div class="cost-centre-right-container"><input id=formulaMatrixBox' + QuestionQueueItems[i].ID + ' type="text" disabled="disabled" ' +
                    'style="float:left; margin-right:10px;"  data-id=' + QuestionQueueItems[i].ID + ' class="CostCentreAnswersQueue" /> ' +
                    '<input type="button" onclick="ShowFormulaMatrix(' + QuestionQueueItems[i].RowCount + ',' + QuestionQueueItems[i].ColumnCount + ',' + i + '); return false;" class="Matrix-select-button rounded_corners5 " value="Select" /></div><div class="clearBoth"></div>';
            }
        }
       
    } else if (Mode == "Modify") { // prompt in case of added cost centre
        Heading = "Edit " + $("#" + SelectedCostCentreCheckBoxId).next().html();
        for (var i = 0; i < QuestionQueueItems.length; i++) {

            if (QuestionQueueItems[i].ItemType == 1) { // text box
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + QuestionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + QuestionQueueItems[i].ID;
                }

                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + QuestionQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + QuestionQueueItems[i].ID + ' data-id=' + QuestionQueueItems[i].ID + ' value=' + QuestionQueueItems[i].Qty1Answer + ' /></div><br/><div class="clearBoth"></div>';
            }

            if (QuestionQueueItems[i].ItemType == 3) { // drop down

                var OptionHtml = "";
                var matrixTable = QuestionQueueItems[i].MatrixTable;
                for (var a = 0; a < QuestionQueueItems[i].AnswersTable.length; a++) {
                    if (QuestionQueueItems[i].AnswersTable[a].AnswerString == QuestionQueueItems[i].Qty1Answer) {
                        OptionHtml = OptionHtml + '<option data-id=' + QuestionQueueItems[i].ID + ' value=' + QuestionQueueItems[i].AnswersTable[a].AnswerString + ' selected>' + QuestionQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                    } else {
                        OptionHtml = OptionHtml + '<option data-id=' + QuestionQueueItems[i].ID + ' value=' + QuestionQueueItems[i].AnswersTable[a].AnswerString + ' >' + QuestionQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                    }
                    
                }
                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + QuestionQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><select id=dropdown' + QuestionQueueItems[i].ID + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
                    + OptionHtml + '</select></div><br/><div class="clearBoth"></div>';
            }
            if (QuestionQueueItems[i].ItemType == 4) { // formula matrix

                if (idsToValidate == "") {
                    idsToValidate = 'formulaMatrixBox' + QuestionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'formulaMatrixBox' + QuestionQueueItems[i].ID;
                }

                innerHtml = innerHtml +
                    '<div class="cost-centre-left-container"><label>Super Formula Matrix</label></div>' +
                    '<div class="cost-centre-right-container"><input id=formulaMatrixBox' + QuestionQueueItems[i].ID + ' type="text" disabled="disabled" ' +
                    'style="float:left; margin-right:10px;"  data-id=' + QuestionQueueItems[i].ID + ' class="CostCentreAnswersQueue" value=' + QuestionQueueItems[i].Qty1Answer + ' /> ' +
                    '<input type="button" onclick="ShowFormulaMatrix(' + QuestionQueueItems[i].RowCount + ',' + QuestionQueueItems[i].ColumnCount + ',' + i + '); return false;" class="Matrix-select-button rounded_corners5 " value="Select" /></div><div class="clearBoth"></div>';
            }
        }
    }
   
    for (var w = 0; w < WorkInstructions.length; w++) {

        var WOptionHtml = "";

        for (var c = 0; c < WorkInstructions[w].CostcentreWorkInstructionsChoices.length; c++) {
            WOptionHtml = WOptionHtml + '<option data-id=' + WorkInstructions[w].InstructionId + ' value=' + WorkInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + ' >' + WorkInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + '</option>'
        }
        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
            + WorkInstructions[w].Instruction +
            '</label></div><div class="cost-centre-right-container"><select id=dropdown' + WorkInstructions[w].InstructionId + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
            + WOptionHtml + '</select></div><br/><div class="clearBoth"></div>';

    }

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title left_align">' + Heading + '</h4></div><div class="modal-body left_align"><div id="CCErrorMesgContainer"></div>' + innerHtml + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button  id="btnCostCentreCalculator" type="button" class="btn btn-primary float_right" onclick="ValidateCostCentreControl(' + CostCentreId + ',' + ClonedItemId + ',' + SelectedCostCentreCheckBoxId + ',&#34; ' + Currency + '&#34; ,' + ItemPrice + ',' + CostCentreType + ',' + TaxRate + ');">Continue</button><img src="/Content/Images/costcentreLoader.gif" id="imgCCLoader" style="height: 20px;margin-right: 10px;margin-top:8px; display:none;"/></div></div></div>';
   

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
    document.getElementById("innerLayer").style.zIndex = "999999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowInputCostCentrePopup(InputQueueItems, CostCentreId, ClonedItemId, SelectedCostCentreCheckBoxId, Mode, Currency, ItemPrice, QuestionQueueObject, CostCentreType, TaxRate, WorkInstructions) {
    
    GlobalInputQueueItemsList = InputQueueItems;
    GlobalQuestionQueueItemsList = QuestionQueueObject;
    var innerHtml = "";
    var Heading = "Add " + $("#" + SelectedCostCentreCheckBoxId).next().html();

    if (Mode == "New") { // This condition will execute when first time cost centre is prompting for values
        for (var i = 0; i < InputQueueItems.length; i++) {
            
            if (InputQueueItems[i].ID != 1 && InputQueueItems[i].ID != 2) { // Id 1= setuptime , Id 2 = setup cost
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + InputQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + InputQueueItems[i].ID;
                }
                if (InputQueueItems[i].Value != "") {
                    innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + InputQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + InputQueueItems[i].ID + ' data-id=' + InputQueueItems[i].ID + ' value=' + parseFloat(InputQueueItems[i].Value) + ' /></div><br/><div class="clearBoth"></div>';
                } else {
                    innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + InputQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + InputQueueItems[i].ID + ' data-id=' + InputQueueItems[i].ID + ' /></div><br/><div class="clearBoth"></div>';
                }
                
            }
        }
    } else if (Mode == "Modify") { // This condition will execute when cost centre is already prompted and user clicks to modify the values entered
        Heading = "Edit " + $("#" + SelectedCostCentreCheckBoxId).next().html();
        for (var i = 0; i < InputQueueItems.length; i++) {
            
            if (InputQueueItems[i].ID != 1 && InputQueueItems[i].ID != 2) { // Id 1= setuptime , Id 2 = setup cost
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + InputQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + InputQueueItems[i].ID;
                }
                if (InputQueueItems[i].Value != "") {
                    innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + InputQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + InputQueueItems[i].ID + ' data-id=' + InputQueueItems[i].ID + ' value=' + InputQueueItems[i].Qty1Answer + ' value=' + parseFloat(InputQueueItems[i].Value) + ' /></div><br/><div class="clearBoth"></div>';
                } else {
                    innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + InputQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + InputQueueItems[i].ID + ' data-id=' + InputQueueItems[i].ID + ' value=' + InputQueueItems[i].Qty1Answer + ' /></div><br/><div class="clearBoth"></div>';
                }
                
            }
        }
    }

    for (var w = 0; w < WorkInstructions.length; w++) {

        var WOptionHtml = "";

        for (var c = 0; c < WorkInstructions[w].CostcentreWorkInstructionsChoices.length; c++) {
            WOptionHtml = WOptionHtml + '<option data-id=' + WorkInstructions[w].InstructionId + ' value=' + WorkInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + ' >' + WorkInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + '</option>'
        }
        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
            + WorkInstructions[w].Instruction +
            '</label></div><div class="cost-centre-right-container"><select id=dropdown' + WorkInstructions[w].InstructionId + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
            + WOptionHtml + '</select></div><br/><div class="clearBoth"></div>';

    }

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title left_align">' + Heading + '</h4></div><div class="modal-body left_align"><div id="CCErrorMesgContainer"></div>' + innerHtml + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button id="btnCostCentreCalculator" type="button" class="btn btn-primary float_right" onclick="ValidateCostCentreControl(' + CostCentreId + ',' + ClonedItemId + ',' + SelectedCostCentreCheckBoxId + ',&#34; ' + Currency + '&#34; ,' + ItemPrice + ',' + CostCentreType + ',' + TaxRate + ');">Continue</button><img src="/Content/Images/costcentreLoader.gif" id="imgCCLoader"  style="height: 20px; margin-right: 10px;margin-top:8px; display:none;" /></div></div></div>';


    var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "200px";

    document.getElementById("innerLayer").style.width = "730px";
    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "999999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowFormulaMatrix(Rows, Columns, matrixIndex) {
    var MatrixItems = GlobalQuestionQueueItemsList[matrixIndex].MatrixTable;
   
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

    var container = '  <div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideFormulaPopUp();>&times;</button><h4 class="modal-title left_align">Please select a value from Matrix</h4></div><div class="modal-body left_align"><table class="cost-centre-Matrix">' + RowsHtml + '</table></div></div></div>';

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

function ValidateCostCentreControl(CostCentreId, ClonedItemId, SelectedCostCentreCheckBoxId, Currency, ItemPrice, CostCentreType, TaxRate) {

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
                var html = $("#CCErrorMesgContainer").text() + "<br/> Please select formula values also."
                $("#CCErrorMesgContainer").html(html);
            }
        } else {

            $("#CCErrorMesgContainer").html("Please enter in the highlighted fields.");
        }
        return;
    } else if (isNotValidInput == 1) {
        $("#CCErrorMesgContainer").css("display", "block");
        $("#CCErrorMesgContainer").html("Please enter numbers only to proceed.");
        if (isFormulaValidationError == 1) {
            var html = $("#CCErrorMesgContainer").text() + "<br/> Please select formula values also."
            $("#CCErrorMesgContainer").html(html);
        }
        return;
    } else if (isFormulaValidationError == 1) {
        $("#CCErrorMesgContainer").html("Please select formula values ");
        return;
    } else {

        $("#btnCostCentreCalculator").prop('disabled', 'disabled');
        $("#imgCCLoader").css("display", "block");
        $("#imgCCLoader").css("float", "right");
        var desriptionOfCostCentre = "";
        $("#CCErrorMesgContainer").css("display", "none");
        // Question Queue object items
        $(".CostCentreAnswersQueue").each(function (i, val) {
            if ($(val).attr('data-id') == undefined) {
                var idofDropDown = $(val).attr('id');
                idofDropDown = "select#" + idofDropDown;
                var idOfQuestion = $(idofDropDown + ' option:selected').attr('data-id');
                $(GlobalQuestionQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == idOfQuestion) {

                        QueueItem.Qty1Answer = $(idofDropDown + ' option:selected').val();

                    }
                });

                $(GlobalInputQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == idOfQuestion) {

                        QueueItem.Qty1Answer = $(idofDropDown + ' option:selected').val();

                    }
                });
            } else {
                $(GlobalQuestionQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == $(val).attr('data-id')) {

                        QueueItem.Qty1Answer = $(val).val();

                    }
                });

                $(GlobalInputQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == $(val).attr('data-id')) {

                        QueueItem.Qty1Answer = $(val).val();

                    }
                });
            }
            if (desriptionOfCostCentre == "") {
                desriptionOfCostCentre = $(val).parent().prev().children().text() + " = " + $(val).val() + ". --- ";
            } else {
                desriptionOfCostCentre = desriptionOfCostCentre + "  " + $(val).parent().prev().children().text() + "= " + $(val).val() + ". --- ";
            }
        });
        console.log("vlaidta efun");
        console.log(GlobalQuestionQueueItemsList);
        SetGlobalCostCentreQueue(GlobalQuestionQueueItemsList, GlobalInputQueueItemsList, CostCentreId, CostCentreType, ClonedItemId, SelectedCostCentreCheckBoxId, desriptionOfCostCentre, ItemPrice, Currency, true, TaxRate);

        idsToValidate = "";
    }
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
    document.getElementById("innerLayer").style.zIndex = "999999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowRejectionOrderPopUp(title, Tvalue) {


    //<textarea name="Text1" cols="40" rows="5" class="rounded_corners5 text_boxPolicy" id="txtPolicy" readonly="readonly" >@ViewBag.CorpCompany.CorporateOrderingPolicy</textarea>
    // var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <input type="text" id="txtOrderPolicy"  class="rounded_corners5 text_box" value=' + Tvalue + ' /><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="Order()">Save</button></div></div>';
    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <textarea type="text" id="RejectionReasoan" cols="40" rows="5"  class="rounded_corners5 text_box" >' + Tvalue + '</textarea><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="Rejection()">Save</button><button type="button" id="Close" class="btn btn-primary" onclick="Cancel()">Close</button></div></div>';
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
    document.getElementById("innerLayer").style.zIndex = "999999";


    document.getElementById("innerLayer").style.height = "280px";


    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}

function ShowRejectionPopUpOrderHistory(title, Tvalue) {


    //<textarea name="Text1" cols="40" rows="5" class="rounded_corners5 text_boxPolicy" id="txtPolicy" readonly="readonly" >@ViewBag.CorpCompany.CorporateOrderingPolicy</textarea>
    // var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <input type="text" id="txtOrderPolicy"  class="rounded_corners5 text_box" value=' + Tvalue + ' /><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="Order()">Save</button></div></div>';
    var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"> <textarea type="text" id="RejectionReasoan" cols="40" rows="5"  class="rounded_corners5 text_box" >' + Tvalue + '</textarea><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="Close" class="btn btn-primary" onclick="Cancel()">Close</button></div></div>';
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
    document.getElementById("innerLayer").style.zIndex = "999999";


    document.getElementById("innerLayer").style.height = "280px";


    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}



function ViewOrderPopUp(Type, panelHtml) {


    var bws = getBrowserHeight();

    var shadow = document.getElementById("innerLayer");

    document.getElementById("layer").style.width = bws.width + "px";
    document.getElementById("layer").style.height = bws.height + "px";

    var left = 0;
    var container = "";
    if (bws.width >= 481 && bws.width < 641) {
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
        container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';
        document.getElementById("innerLayer").style.height = "536px";
    }
    else if (bws.width <= 481) {
        document.getElementById("innerLayer").style.width = (bws.width) + "px";
        container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';
        document.getElementById("innerLayer").style.height = "536px";

    } else {
        left = parseInt((bws.width - 730) / 2);
        document.getElementById("innerLayer").style.width = "730px";
        document.getElementById("innerLayer").style.height = "536px";
        container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';

    }
    document.getElementById("innerLayer").innerHTML = container;

    document.getElementById("innerLayer").style.left = left + "px";
    document.getElementById("innerLayer").style.top = "0px";


    document.getElementById("innerLayer").style.position = "fixed";
    document.getElementById("innerLayer").style.zIndex = "999999";

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
    function ViewOrderPopUp(Type, panelHtml) {


        var bws = getBrowserHeight();

        var shadow = document.getElementById("innerLayer");

        document.getElementById("layer").style.width = bws.width + "px";
        document.getElementById("layer").style.height = bws.height + "px";

        var left = 0;
        var container = "";
        if (bws.width >= 481 && bws.width < 641) {
            document.getElementById("innerLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';
            document.getElementById("innerLayer").style.height = "536px";
        }
        else if (bws.width <= 481) {
            document.getElementById("innerLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';
            document.getElementById("innerLayer").style.height = "536px";

        } else {
            left = parseInt((bws.width - 730) / 2);
            document.getElementById("innerLayer").style.width = "730px";
            document.getElementById("innerLayer").style.height = "536px";
            container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';

        }
        document.getElementById("innerLayer").innerHTML = container;

        document.getElementById("innerLayer").style.left = left + "px";
        document.getElementById("innerLayer").style.top = "0px";


        document.getElementById("innerLayer").style.position = "fixed";
        document.getElementById("innerLayer").style.zIndex = "999999";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";
    }
    function AssetsPopUp(Type, panelHtml) {


        var bws = getBrowserHeight();

        var shadow = document.getElementById("innerLayer");

        document.getElementById("layer").style.width = bws.width + "px";
        document.getElementById("layer").style.height = bws.height + "px";

        var left = 0;
        var container = "";
        if (bws.width >= 481 && bws.width < 641) {
            document.getElementById("innerLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';
            document.getElementById("innerLayer").style.height = "536px";
        }
        else if (bws.width <= 481) {
            document.getElementById("innerLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';
            document.getElementById("innerLayer").style.height = "536px";

        } else {
            left = parseInt((bws.width - 730) / 2);
            document.getElementById("innerLayer").style.width = "730px";
            document.getElementById("innerLayer").style.height = "536px";
            container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="ScrollBarOnOrderHistory ">' + panelHtml + '</div></div>';

        }
        document.getElementById("innerLayer").innerHTML = container;

        document.getElementById("innerLayer").style.left = left + "px";
        document.getElementById("innerLayer").style.top = "0px";


        document.getElementById("innerLayer").style.position = "fixed";
        document.getElementById("innerLayer").style.zIndex = "999999";

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

        //var left = parseInt((bws.width - 500) / 2);
        //var top = parseInt((bws.height - 170) / 2);

        var left = 0;
        var top = parseInt((bws.height - 116) / 2);
        if (bws.width < 640) {
            document.getElementById("innerLayer").style.width = (bws.width) + "px";
        } else {
            left = parseInt((bws.width - 500) / 2);
            document.getElementById("innerLayer").style.width = "500px";
        }

        document.getElementById("innerLayer").innerHTML = container;

        document.getElementById("innerLayer").style.top = top + "px";
        document.getElementById("innerLayer").style.left = left + "px";


        document.getElementById("innerLayer").style.height = "116px";
        document.getElementById("innerLayer").style.position = "fixed";
        document.getElementById("innerLayer").style.zIndex = "999999";

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
        document.getElementById("innerLayer").style.zIndex = "999999";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";

    }
    function ConfirmPopUpForRemoveFolder() {

        var Type = "Alert!";
        var Message = "Are you sure you want to remove folder?"
        var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="button" id="ApproveOk"  class="btn btn-primary" value="Yes" onclick="DeleteFolder()" /><input type="button" id="ApproveCancel" class="btn btn-primary" value="No" onclick="No()"></button></div></div></div>';
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
        document.getElementById("innerLayer").style.zIndex = "999999";

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
        document.getElementById("innerLayer").style.zIndex = "999999";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";

    }
    function ShowMyPopUp() {
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
        document.getElementById("innerLayer").style.zIndex = "999999";

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
        document.getElementById("innerLayer").style.zIndex = "999999";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";
    }
    function ConfirmPopUpForReject() {
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
        document.getElementById("innerLayer").style.zIndex = "999999";

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
        document.getElementById("innerLayer").style.zIndex = "999999";
        document.getElementById("innerLayer").style.borderRadius = "5px";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";
    }




    function CustomeAlertBoxDesigner(msg, callbackFuncName) {

        var Type = "Alert!";
        var Message = msg;
        var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header">' +
            '<button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div>' +
            '<div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;">' +
            '<input type="button" id="ApproveOk"  class="btn btn-primary" value="Yes" onclick="' + callbackFuncName + '" />' +
            '<input type="button" id="ApproveCancel" class="btn btn-primary" value="No" onclick="HideLoader()"></button></div></div></div>';
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
        document.getElementById("innerLayer").style.height = "207px";
        document.getElementById("innerLayer").style.position = "fixed";
        document.getElementById("innerLayer").style.zIndex = "100002";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";

    }

    function SetGlobalCostCentreQueue(GlobalQuestionQueueItemsList, GlobalInputQueueItemsList, CostCentreId, CostCentreType, ClonedItemId, SelectedCostCentreCheckBoxId, desriptionOfQuestion, ItemPrice, CurrencyCode, isPromptAQuestion, TaxRate) {
        console.log("SetGlobalCostCentreQueue function");
        debugger;
        var jsonObjectsOfGlobalQueue = null;


        if ($("#costCentreQueueItems").val() == "" || $("#costCentreQueueItems").val() == "null" || $("#costCentreQueueItems").val() == null) {
            console.log("if");
            if (GlobalInputQueueItemsList == null) {
                GlobalInputQueueItemsList = "";
            }
            var InputAndQuestionQueues = {
                QuestionQueues: GlobalQuestionQueueItemsList,
                InputQueues: GlobalInputQueueItemsList
            }

            jsonObjectsOfGlobalQueue = JSON.stringify(InputAndQuestionQueues, null, 2);
            $("#costCentreQueueItems").val(jsonObjectsOfGlobalQueue);

        } else {
            console.log("else");
            var isUpdated = false;
            var InputAndQuestionQueues = JSON.parse($("#costCentreQueueItems").val());

            if (InputAndQuestionQueues.InputQueues == null) {
                InputAndQuestionQueues.InputQueues = [];
                if (GlobalInputQueueItemsList != null) {
                    for (var i = 0; i < GlobalInputQueueItemsList.length; i++) {
                        InputAndQuestionQueues.InputQueues.push(GlobalInputQueueItemsList[i]);
                    }
                }
              
            } else {
                if (GlobalInputQueueItemsList != null) {
                    for (var i = 0; i < GlobalInputQueueItemsList.length; i++) {
                        for (var j = 0; j < InputAndQuestionQueues.InputQueues.length; j++) {

                            if (InputAndQuestionQueues.InputQueues[j].CostCentreID == GlobalInputQueueItemsList[i].CostCentreID && InputAndQuestionQueues.InputQueues[j].ID == GlobalInputQueueItemsList[i].ID) {
                                InputAndQuestionQueues.InputQueues[j].Qty1Answer = GlobalInputQueueItemsList[i].Qty1Answer;
                                isUpdated = true;
                                break;
                            }
                        }

                        if (isUpdated == false) {
                            InputAndQuestionQueues.InputQueues.push(GlobalInputQueueItemsList[i]);
                            isUpdated = false;
                        }
                    }
                }
               
            }

            console.log("GlobalQuestionQueueItemsList " + GlobalQuestionQueueItemsList);

            for (var i = 0; i < GlobalQuestionQueueItemsList.length; i++) {
                for (var j = 0; j < InputAndQuestionQueues.QuestionQueues.length; j++) {
                    if (InputAndQuestionQueues.QuestionQueues[j].CostCentreID == GlobalQuestionQueueItemsList[i].CostCentreID && InputAndQuestionQueues.QuestionQueues[j].ID == GlobalQuestionQueueItemsList[i].ID) {

                        InputAndQuestionQueues.QuestionQueues[j].Qty1Answer = GlobalQuestionQueueItemsList[i].Qty1Answer;
                        isUpdated = true;
                        break;

                    }
                }

                if (isUpdated == false) {
                    if (InputAndQuestionQueues.QuestionQueues == null) {
                        InputAndQuestionQueues.QuestionQueues = [];
                    }
                    InputAndQuestionQueues.QuestionQueues.push(GlobalQuestionQueueItemsList[i]);
                    isUpdated = false;
                }
            }

            $("#costCentreQueueItems").val(JSON.stringify(InputAndQuestionQueues, null, 2));

        }
        console.log("UpdatedGlobalQueueArray");
        
        var UpdatedGlobalQueueArray = JSON.parse($("#costCentreQueueItems").val());
        console.log(UpdatedGlobalQueueArray);
        var CostCentreQueueObjectToSaveInDB = [];

        var to;
        to = "/webstoreapi/costCenter/ExecuteCostCentre?CostCentreId=" + CostCentreId + "&ClonedItemId=" + ClonedItemId + "&OrderedQuantity=" + $("#VMQuantityOrdered").val() + "&CallMode=New";
        var options = {
            type: "POST",
            url: to,
            data: $("#costCentreQueueItems").val(),
            contentType: "application/json",
            async: true,
            success: function (response) {
               
                ShowLoader();

                var updatedAddOns = jQuery.parseJSON($('#VMJsonAddOns').val());

                if (updatedAddOns != null) {

                    for (var i = 0; i < $(updatedAddOns).length; i++) {

                        if ($(updatedAddOns)[i].CostCenterId == CostCentreId) {
                            $(updatedAddOns)[i].ActualPrice = response;
                            $(updatedAddOns)[i].Description = desriptionOfQuestion;
                            $(updatedAddOns)[i].AddOnName =  + $("#" + SelectedCostCentreCheckBoxId).next().html();
                            $(updatedAddOns)[i].Type = CostCentreType;
                            if (CostCentreType == 4) { // question queue
                                for (var j = 0; j < UpdatedGlobalQueueArray.QuestionQueues.length; j++) {
                                    if (UpdatedGlobalQueueArray.QuestionQueues[j].CostCentreID == CostCentreId) {
                                        CostCentreQueueObjectToSaveInDB.push(UpdatedGlobalQueueArray.QuestionQueues[j]);
                                    }
                                }
                            } else { // input queue
                                for (var k = 0; k < UpdatedGlobalQueueArray.InputQueues.length; k++) {

                                    if (UpdatedGlobalQueueArray.InputQueues[k].CostCentreID == CostCentreId) {
                                        CostCentreQueueObjectToSaveInDB.push(UpdatedGlobalQueueArray.InputQueues[k]);
                                    }
                                }
                            }

                            if (CostCentreQueueObjectToSaveInDB.length > 0) {
                                $(updatedAddOns)[i].CostCentreJasonData = JSON.stringify(CostCentreQueueObjectToSaveInDB, null, 2);
                            }


                            break;
                        }
                    }

                    if (UpdatedGlobalQueueArray.QuestionQueues != null) {
                        var QuestionQueueDBObject = [];
                        for (var m = 0; m < UpdatedGlobalQueueArray.QuestionQueues.length; m++) {

                            QuestionQueueDBObject.push(UpdatedGlobalQueueArray.QuestionQueues[m]);

                        }
                        console.log("QuestionQueueDBObject.length popup");
                        console.log(QuestionQueueDBObject.length);
                        if (QuestionQueueDBObject.length > 0) {
                            $("#VMJsonAddOnsQuestionQueue").val(JSON.stringify(QuestionQueueDBObject, null, 2));
                        }
                    }
                    if (UpdatedGlobalQueueArray.InputQueues != null) {
                        var InputQueueDBObject = [];
                        for (var n = 0; n < UpdatedGlobalQueueArray.InputQueues.length; n++) {

                            InputQueueDBObject.push(UpdatedGlobalQueueArray.InputQueues[n]);

                        }

                        if (InputQueueDBObject.length > 0) {
                            $("#VMJsonAddOnsInputQueue").val(JSON.stringify(InputQueueDBObject, null, 2));
                        }
                    }


                    var JsonToReSubmit = [];

                    var totalVal = 0;
                    var TaxAppliedValue = 0;
                    // add checked cost centre values to gross total
                    for (var i = 0; i < $(updatedAddOns).length; i++) {

                        JsonToReSubmit.push($(updatedAddOns)[i]);
                        TaxAppliedValue = parseFloat($(updatedAddOns)[i].ActualPrice);
                        TaxAppliedValue = TaxAppliedValue + ((TaxAppliedValue * TaxRate) / 100);

                        totalVal = parseFloat(totalVal) + parseFloat(TaxAppliedValue);

                    }

                    displayTotalPrice(ItemPrice, totalVal);
                    TaxAppliedValue = response;
                    TaxAppliedValue = TaxAppliedValue + ((TaxAppliedValue * TaxRate) / 100);

                    console.log(isPromptAQuestion + " isPromptAQuestion");

                    if (isPromptAQuestion == true) {
                        $("#" + SelectedCostCentreCheckBoxId).next().next().html('<label>' + CurrencyCode + (TaxAppliedValue).toFixed(2).toString() + '</label>' + '<a class="CCModifyLink" onclick="PromptQuestion(' + CostCentreId + ',' + SelectedCostCentreCheckBoxId + ',' + CostCentreType + ', 1);" >Modify</a> ');
                    } else {
                        $("#" + SelectedCostCentreCheckBoxId).next().next().html('<label>' + CurrencyCode + (TaxAppliedValue).toFixed(2).toString() + '</label>');
                    }
                    $("#VMAddOnrice").val(totalVal);
                    $("#VMJsonAddOns").val(JSON.stringify(JsonToReSubmit));
                }
                HideLoader();
            },
            error: function (msg) {
                console.log(msg); ShowPopUp("Error", "'" + msg.responseText + "'");
            }
        };
        var returnText = $.ajax(options).responseText;


    }
    function ConfirmDeleteArtWorkPopUP(AttachmentID, ItemId, pageType) {

        var Type = "Alert!";
        var Message = "Are you sure you want to remove this design?"
        var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="submit" class="btn btn-primary" onclick=DeleteArtWork(' + AttachmentID + ',' + ItemId + ',' + pageType + '); value="Yes" /><button type="button" onclick=HideMessagePopUp(); class="btn btn-primary">No</button></div></div></div>';

        var bws = getBrowserHeight();
        var shadow = document.getElementById("innerLayer");
        document.getElementById("layer").style.width = bws.width + "px";
        document.getElementById("layer").style.height = bws.height + "px";

        var left = 0;
        var top = parseInt((bws.height - 116) / 2);

        if (bws.width < 640) {
            document.getElementById("innerLayer").style.width = (bws.width) + "px";

        } else {
            left = parseInt((bws.width - 500) / 2);
            document.getElementById("innerLayer").style.width = "500px";
        }
        document.getElementById("innerLayer").innerHTML = container;

        document.getElementById("innerLayer").style.top = top + "px";
        document.getElementById("innerLayer").style.left = left + "px";


        document.getElementById("innerLayer").style.height = "116px";
        document.getElementById("innerLayer").style.position = "fixed";
        document.getElementById("innerLayer").style.zIndex = "999999";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";

        return false;
    }

    function DeleteArtWork(AttachmentId, ItemId, TypeId) {

        var pageType = "Options";
        if (TypeId == 1) {
            pageType = "ProductOptionsAndDetails";
        }
        ShowLoader();

        var to;
        to = "/webstoreapi/DeleteAttachment/DeleteArtworkAttachment?AttachmentId=" + AttachmentId + "&ItemId=" + ItemId + "&pageType=" + pageType;
        var options = {
            type: "POST",
            url: to,
            data: "",
            contentType: "application/json",
            async: true,
            success: function (response) {
                if (response[0] == "Success") {
                    $("#attachmentUploadContainer").html(response[1]);
                    isImageUploadedOnLandingPage = 1;
                    HideLoader();
                } else if (response[0] == "NoFiles") {
                    $("#attachmentUploadContainer").html("");
                    isImageUploadedOnLandingPage = 0;
                    $("#uploadDesignHeadingContainer").css("display", "none");
                    $("#uploadDesignattachmentContainer").css("display", "none");
                    if (isPrintProductFlag == "1") {
                        $("#designNowContainer").css("display", "block");
                        $("#cartOrderDetailContainer").css("display", "none");
                    }

                    HideLoader();
                }

            },
            error: function (msg) {

                console.log(msg); ShowPopUp("Error", "'" + msg.responseText + "'");

            }
        };
        var returnText = $.ajax(options).responseText;
    }

    //requires JQuery , html content ID without #
    function CreateGenericPopup(PopUpTitle, HtmlID) {

        $("#innerLayer").after('<div id="popupLayer"></div>');
        var bws = getBrowserHeight();

        var shadow = document.getElementById("popupLayer");

        document.getElementById("layer").style.width = bws.width + "px";
        document.getElementById("layer").style.height = bws.height + "px";

        var left = 0;
        var container = "";
        if (bws.width < 700 && bws.width > 640) {
            //left = parseInt((bws.width) / 2);
            document.getElementById("popupLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class=""><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUpGeneric(); >&times;</button><h4 class="modal-title" id="popupTitle"></h4></div><div class="modal-body" style="height: 500px; overflow: scroll; overflow-x: hidden;" id="popupContent"></div></div>';
        }
        else if (bws.width == 640) {
            //left = parseInt((bws.width) / 2);
            document.getElementById("popupLayer").style.width = (bws.width) + "px";
            //container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:transparent!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body" style="overflow-y:scroll;height:350px;"></div></div>';
            container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class=""><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUpGeneric(); >&times;</button><h4 class="modal-title" id="popupTitle"></h4></div><div class="modal-body" style="overflow-y:scroll;height:350px;" id="popupContent"></div></div>';

        }
        else if (bws.width < 640 && bws.width > 600) {
            //left = parseInt((bws.width) / 2);
            document.getElementById("popupLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class=""><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUpGeneric(); >&times;</button><h4 class="modal-title" id="popupTitle"></h4></div><div class="modal-body" style="overflow-y:scroll;height:550px; id="popupContent""></div></div>';

        }
        else if (bws.width < 600 && bws.width > 500) {
            //left = parseInt((bws.width) / 2);
            document.getElementById("popupLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:transparent!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUpGeneric(); >&times;</button><h4 class="modal-title" id="popupTitle"></h4></div><div class="modal-body" style="overflow-y:scroll;height:350px;" id="popupContent"></div></div>';

        } else if (bws.width < 500) {
            //left = parseInt((bws.width) / 2);
            document.getElementById("popupLayer").style.width = (bws.width) + "px";
            container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:transparent!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUpGeneric(); >&times;</button><h4 class="modal-title" id="popupTitle"></h4></div><div class="modal-body" style="overflow-y:scroll;height:400px;" id="popupContent"></div></div>';

        }
        else {

            left = parseInt((bws.width - 730) / 2);
            document.getElementById("popupLayer").style.width = "730px";
            container = '<div class="md-modal md-effect-7 col-xs-12" id="modal-7" ><div class="md-content" style="border-style:none!important;border-width:0px!important;border-radius:0px!important;border-color:none!important;"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUpGeneric(); >&times;</button><h4 class="modal-title" id="popupTitle"></h4></div><div class="modal-body" style="overflow-y:scroll;height:580px;" id="popupContent"></div></div>';


        }



        document.getElementById("popupLayer").innerHTML = container;

        document.getElementById("popupLayer").style.left = left + "px";
        document.getElementById("popupLayer").style.top = "0px";
        document.getElementById("popupLayer").style.border = "6px solid darkgray";

        document.getElementById("popupLayer").style.position = "fixed";
        document.getElementById("popupLayer").style.zIndex = "9999";

        document.getElementById("layer").style.display = "block";
        document.getElementById("popupLayer").style.display = "block";



        document.getElementById("popupTitle").innerHTML = PopUpTitle;
        $("#popupContent").append($("#" + HtmlID + ""));
    }

    function HideMessagePopUpGeneric() {
        document.getElementById("layer").style.display = "none";
        document.getElementById("popupLayer").style.display = "none";
    }

    function CustomPOPUP(Header, Body) {

        var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Header + '</h4></div><div class="modal-body" style="text-align:left; font-size: 12px;">' + Body + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><input type="button" class="btn btn-primary" onclick=ContinueOrder(); value="Yes" /><button type="button" onclick=HideMessagePopUp(); class="btn btn-primary">No</button></div></div></div>';

        var bws = getBrowserHeight();
        var shadow = document.getElementById("innerLayer");
        document.getElementById("layer").style.width = bws.width + "px";
        document.getElementById("layer").style.height = bws.height + "px";

        var left = 0;
        var top = parseInt((bws.height - 170) / 2);

        if (bws.width < 640) {
            document.getElementById("innerLayer").style.width = (bws.width) + "px";

        } else {
            left = parseInt((bws.width - 500) / 2);
            document.getElementById("innerLayer").style.width = "500px";
        }
        document.getElementById("innerLayer").innerHTML = container;

        document.getElementById("innerLayer").style.top = top + "px";
        document.getElementById("innerLayer").style.left = left + "px";

        document.getElementById("innerLayer").style.width = "500px";
        document.getElementById("innerLayer").style.height = "170px";
        document.getElementById("innerLayer").style.position = "fixed";
        document.getElementById("innerLayer").style.zIndex = "999999";

        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";
    }


    function EmailProofsPopup(title) {

        var container = '<div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + title + '</h4></div><div class="modal-body"><input type="text" id="Email1"  class="rounded_corners5 text_box" ></input><br/><br/><input type="text" id="Email2"  class="rounded_corners5 text_box" ></input><div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" id="OrderSave" class="btn btn-primary" onclick="SendProofs()">Save</button><button type="button" id="Close" class="btn btn-primary" onclick="Cancel()">Close</button></div></div>';

        
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
        document.getElementById("innerLayer").style.zIndex = "999999";
        document.getElementById("innerLayer").style.height = "280px";
        document.getElementById("layer").style.display = "block";
        document.getElementById("innerLayer").style.display = "block";
    }
