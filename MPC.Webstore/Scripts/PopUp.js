function ShowPopUp(Type,Message) {
   
    var container = '<div class="white_background" style="padding:20px;"><div class="Width100Percent popUpsDisply"><div class="exit_container_PopUpMesg"><div class="exit_popup"></div></div></div><label class="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass">' + Type + '</label><div onclick="HideMessagePopUp();" class="MesgBoxBtnsDisplay rounded_corners5">Close</div><div class="clearBoth">&nbsp;</div><div class="SolidBorderCS">&nbsp;</div><div class="pop_body_MesgPopUp"><br /><div class="inner"><label>' + Message + '</label></div></div></div>';
   
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