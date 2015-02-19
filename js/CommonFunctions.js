/* ShowModalDialogDefault needs no CallbackFunc name, 
it calls default callback function to full fill its need,
this Default callbackFunc calls for both cases IE and Firefox.*/
function ShowModalDialogDefault(Url, height, width, ControlID) {
    //alert("With Default Method");
    Control = ControlID;
    return ShowModalDialogAA(Url, height, width, CallbackFunc);
}

function ShowModalDialogAA(Url, height, width, CallbackFunc) {
    try {
        if (window.navigator.appName == 'Microsoft Internet Explorer') {
            var PostBack = window.showModalDialog(Url, "Dialog Arguments Value", "dialogHeight:" + height + "px; dialogWidth: " + width + "px; edge: Raised; center: Yes; help: No; resizable: No; status: Yes;");
            if (PostBack == 'true') {
                //alert(PostBack);
                CallbackFunc(PostBack);
                return true;
            }
            else {
                //alert(PostBack);
                return false;
            }
        }
        else {
            //alert(CallbackFunc);
            showPopWin(Url, width, height, CallbackFunc);
            return false;
        }
        return false;
    }
    catch (err) {
        alert(err.message);
    }
}

/*Default CallBack Func */
function CallbackFunc(PostBack) {
    if (PostBack == 'true')
        __doPostBack(Control, '');
}
