function showProgress() {
    var shadow = document.getElementById("divShd");
    var bws = getBrowserHeight();
    shadow.style.width = bws.width + "px";
    shadow.style.height = bws.height + "px";
    var left = parseInt((bws.width - 500) / 2);
    var top = parseInt((bws.height - 200) / 2);
    //shadow = null;
    $('#divShd').css("display", "block");
    


//    $('#loaderpanel').css("top", top);
//    $('#loaderpanel').css("left", left);
    $('#UpdateProgressUserProfile').css("display", "block");
    return true;
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

function showPopup() {
    var shadow = document.getElementById("divShd");
    var bws = getBrowserHeight();
    shadow.style.width = bws.width + "px";
    shadow.style.height = bws.height + "px";
    var left = parseInt((bws.width - 350) / 2);
    var top = parseInt((bws.height - 200) / 2);
    //shadow = null;
    $('#divShd').css("display", "block");
    $('#SaveChangesAlert').css("display", "block");
}

function showEditor() {
    var shadow = document.getElementById("divShd");
    var bws = getBrowserHeight();
    shadow.style.width = bws.width + "px";
    shadow.style.height = bws.height + "px";
    var left = parseInt((bws.width - 350) / 2);
    var top = parseInt((bws.height - 200) / 2);
    //shadow = null;
    $('#divShd').css("display", "block");
    
}