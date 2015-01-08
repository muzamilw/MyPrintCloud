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

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ConfirmDeleteItemPopUP(htmlRemove)
{

    var container = htmlRemove; //'<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title">' + Type + '</h4></div><div class="modal-body">' + Message + '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;"><button type="button" class="btn btn-primary" onclick=ConfirmRemove("' + Path + '");  >Yes</button><button type="button" onclick=HideMessagePopUp(); class="btn btn-primary">No</button></div></div></div>';

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
function RedirectToLogin(ReturnURL)
{
   
    window.location.href = "/Login?ReturnURL=" + ReturnURL;
   
}
function RedirectToSignUp(ReturnURL)
{
    window.location.href = "/SignUp?ReturnURL=" + ReturnURL;
}
function ShowLoader() {

    var container = '<div><img src="~/Content/Images/fancybox_loading.gif" /></div>';

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

    document.getElementById("layer").style.display = "block";
    document.getElementById("innerLayer").style.display = "block";
}
function ConfirmRemove(Path)
{
 
    window.location.href = Path;
}