function StartLoader(msg)
{
    isLoading = true;
	if (msg != undefined) {
	    $("#loadingMsg").innerText = msg;
	}
	else {
	    $("#loadingMsg").innerText = "Loading design, please wait..";
	}
    showLayer();
    $("#loading").show(1000);		
}
function StopLoader()
{
    isLoading = false;
    hideLayer();
    $("#loading").hide(1000);	
}

function getBrowserHeight() { 
    var intH = 0; 
    var intW = 0; 
    if(typeof window.innerWidth == 'number' ) { 
        intH = window.innerHeight; 
        intW = window.innerWidth; 
    } 
    else if(document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        intH = document.documentElement.clientHeight; 
        intW = document.documentElement.clientWidth; 
    } 
    else if(document.body && (document.body.clientWidth || document.body.clientHeight)) {
        intH = document.body.clientHeight; 
        intW = document.body.clientWidth; 
    } 
    return { width: parseInt(intW), height: parseInt(intH) }; 
} 

function setLayerPosition() {
    var shadow = document.getElementById("DivShadow");
    var loader = document.getElementById("DivLoading"); 
    var bws = getBrowserHeight(); 
    shadow.style.width = bws.width + "px"; 
    shadow.style.height = bws.height + "px";
    var left = parseInt((bws.width - 350) / 2);
    var top =  parseInt((bws.height - 298) / 2);
    if (IsEmbedded) {
        top -= 122;
    }
    $("#DivLoading").css('left', left);
    $("#DivLoading").css('top', top);
    $("#DivLoading").css('display', '');
    shadow = null; 
    loader = null; 
} 
function showLayer() { 
	$("#Container").css("opacity","0.8");
	setLayerPosition();
	var shadow = document.getElementById("DivShadow");
	var loading = document.getElementById("DivLoading"); 
    shadow.style.display = "block"; 
    loading.style.display = "block"; 
    shadow = null; 
    loading = null;           
} 
function hideLayer() { 
	$("#Container").css("opacity","1");
	var shadow = document.getElementById("DivShadow");
	var loading = document.getElementById("DivLoading"); 
    shadow.style.display = "none"; 
    loading.style.display = "none"; 
    shadow = null; 
    loading = null; 
} 