<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignToolColorWin.aspx.cs" Inherits="Web2Print.UI.DesignToolColorWin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Colour Picker</title>
    <link href="~/Styles/AdminStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../script/DesignToolLib/color_table.js"></script>
    <script type="text/javascript" src="../../script/DesignToolLib/advanced_color_picker.js"></script>
    
    <script src="../../script/DesignToolLib/LibCrossBrowser.js" type="text/javascript"></script>
<script src="../../script/DesignToolLib/EventHandler.js" type="text/javascript"></script>
<script src="../../script/DesignToolLib/Bs_Slider.class.js" type="text/javascript"></script>
<SCRIPT type="text/javascript"><!--
function init() {
  // - Slider Cyan -----------------------------------------
  mySliderCyan = new Bs_Slider();
  mySliderCyan.attachOnChange(bsSliderChangeCyan);
  mySliderCyan.attachOnSlideMove(setColor);
  mySliderCyan.width         = 200;
  mySliderCyan.height        = 26;
  mySliderCyan.minVal        = 0;
  mySliderCyan.maxVal        = 100;
  mySliderCyan.valueInterval = 1;
  mySliderCyan.arrowAmount   = 2;
	mySliderCyan.arrowKeepFiringTimeout = 500;
  mySliderCyan.valueDefault  =parseInt(document.getElementById("attachedFieldValueCyan").value); // 4;
  mySliderCyan.imgDir   = '../../Images/DesignTool/';
  mySliderCyan.setBackgroundImage('background4.gif', 'no-repeat');
  mySliderCyan.setSliderIcon('slider.gif', 13, 18);
  mySliderCyan.setArrowIconLeft('arrowLeft.gif', 16, 16);
  mySliderCyan.setArrowIconRight('arrowRight.gif', 16, 16);
  mySliderCyan.useInputField = 2;
  mySliderCyan.styleValueFieldClass = 'sliderInput';
  mySliderCyan.colorbar = new Object();
  mySliderCyan.colorbar['color']           = 'Cyan';
  mySliderCyan.colorbar['height']          = 5;
  mySliderCyan.colorbar['widthDifference'] = -12;
  mySliderCyan.colorbar['offsetLeft']      = 9;
  mySliderCyan.colorbar['offsetTop']       = 4;
  mySliderCyan.drawInto('sliderCyan');

  // - Slider Magenta -----------------------------------------
  mySliderMagenta = new Bs_Slider();
  mySliderMagenta.attachOnChange(bsSliderChangeMagenta);
  mySliderMagenta.width         = 200;
  mySliderMagenta.height        = 26;
  mySliderMagenta.minVal        = 0;
  mySliderMagenta.maxVal        = 100;
  mySliderMagenta.valueInterval = 1;
  mySliderMagenta.arrowAmount   = 2;
	mySliderMagenta.arrowKeepFiringTimeout = 500;
  mySliderMagenta.valueDefault  =parseInt(document.getElementById("attachedFieldValueMagenta").value); //4;
  mySliderMagenta.imgDir   = '../../Images/DesignTool/';
  mySliderMagenta.setBackgroundImage('background4.gif', 'no-repeat');
  mySliderMagenta.setSliderIcon('slider.gif', 13, 18);
  mySliderMagenta.setArrowIconLeft('arrowLeft.gif', 16, 16);
  mySliderMagenta.setArrowIconRight('arrowRight.gif', 16, 16);
  mySliderMagenta.useInputField = 2;
  mySliderMagenta.styleValueFieldClass = 'sliderInput';
  mySliderMagenta.colorbar = new Object();
  mySliderMagenta.colorbar['color']           = 'Magenta';
  mySliderMagenta.colorbar['height']          = 5;
  mySliderMagenta.colorbar['widthDifference'] = -12;
  mySliderMagenta.colorbar['offsetLeft']      = 9;
  mySliderMagenta.colorbar['offsetTop']       = 4;
  mySliderMagenta.drawInto('sliderMagenta');
  
  // - Slider Yellow -----------------------------------------
  mySliderYellow = new Bs_Slider();
  mySliderYellow.attachOnChange(bsSliderChangeYellow);
  mySliderYellow.width         = 200;
  mySliderYellow.height        = 26;
  mySliderYellow.minVal        = 0;
  mySliderYellow.maxVal        = 100;
  mySliderYellow.valueInterval = 1;
  mySliderYellow.arrowAmount   = 2;
	mySliderYellow.arrowKeepFiringTimeout = 500;
  mySliderYellow.valueDefault  =parseInt(document.getElementById("attachedFieldValueYellow").value) // 4;
  mySliderYellow.imgDir   = '../../Images/DesignTool/';
  mySliderYellow.setBackgroundImage('background4.gif', 'no-repeat');
  mySliderYellow.setSliderIcon('slider.gif', 13, 18);
  mySliderYellow.setArrowIconLeft('arrowLeft.gif', 16, 16);
  mySliderYellow.setArrowIconRight('arrowRight.gif', 16, 16);
  mySliderYellow.useInputField = 2;
  mySliderYellow.styleValueFieldClass = 'sliderInput';
  mySliderYellow.colorbar = new Object();
  mySliderYellow.colorbar['color']           = 'Yellow';
  mySliderYellow.colorbar['height']          = 5;
  mySliderYellow.colorbar['widthDifference'] = -12;
  mySliderYellow.colorbar['offsetLeft']      = 9;
  mySliderYellow.colorbar['offsetTop']       = 4;
  mySliderYellow.drawInto('sliderYellow');
  
  // - Slider Black -----------------------------------------
  
  mySliderBlack = new Bs_Slider();
  mySliderBlack.attachOnChange(bsSliderChangeBlack);
  mySliderBlack.width         = 200;
  mySliderBlack.height        = 26;
  mySliderBlack.minVal        = 0;
  mySliderBlack.maxVal        = 100;
  mySliderBlack.valueInterval = 1;
  mySliderBlack.arrowAmount   = 2;
	mySliderBlack.arrowKeepFiringTimeout = 500;
  mySliderBlack.valueDefault  =parseInt(document.getElementById("attachedFieldValueBlack").value); // 4;
  mySliderBlack.imgDir   = '../../Images/DesignTool/';
  mySliderBlack.setBackgroundImage('background4.gif', 'no-repeat');
  mySliderBlack.setSliderIcon('slider.gif', 13, 18);
  mySliderBlack.setArrowIconLeft('arrowLeft.gif', 16, 16);
  mySliderBlack.setArrowIconRight('arrowRight.gif', 16, 16);
  mySliderBlack.useInputField = 2;
  mySliderBlack.styleValueFieldClass = 'sliderInput';
  mySliderBlack.colorbar = new Object();
  mySliderBlack.colorbar['color']           = 'Black';
  mySliderBlack.colorbar['height']          = 5;
  mySliderBlack.colorbar['widthDifference'] = -12;
  mySliderBlack.colorbar['offsetLeft']      = 9;
  mySliderBlack.colorbar['offsetTop']       = 4;
  mySliderBlack.drawInto('sliderBlack');
   
 
    setColor();
}

/**
* @param object sliderObj
* @param int val (the value)
*/

function bsSliderChangeCyan(sliderObj, val, newPos){ 

document.getElementById("attachedFieldValueCyan").value=val;
setColor();
  //document.test.attachedFieldValue.value = val;
}
function bsSliderChangeMagenta(sliderObj, val, newPos){ 
document.getElementById("attachedFieldValueMagenta").value=val;
setColor();
  //document.test.attachedFieldValue.value = val;
}
function bsSliderChangeYellow(sliderObj, val, newPos){ 
document.getElementById("attachedFieldValueYellow").value=val;
setColor();
  //document.test.attachedFieldValue.value = val;
}
function bsSliderChangeBlack(sliderObj, val, newPos){ 
document.getElementById("attachedFieldValueBlack").value=val;
setColor();
  //document.test.attachedFieldValue.value = val;
}
/**
* @param object sliderObj
* @param int val (the value)
*/
function bsSliderStart(sliderObj, val, newPos){ 
  alert('Started at '+ val +' (Pos:'+ newPos +')');
}
/**
* @param object sliderObj
* @param int val (the value)
*/
function bsSliderEnd(sliderObj, val, newPos){ 
  sliderObj.valueInterval = 0.25;
  sliderObj.setValue(newPos);
  sliderObj.valueInterval = 0.05;
  document.test.attachedFieldValue.value = sliderObj.getValue();
}



function setColor()
{
var hx= getColorHex(parseInt(document.getElementById("attachedFieldValueCyan").value),parseInt(document.getElementById("attachedFieldValueMagenta").value),parseInt(document.getElementById("attachedFieldValueYellow").value),parseInt(document.getElementById("attachedFieldValueBlack").value));   
document.getElementById("ColorPallet").style.backgroundColor="#"+hx;
}



// --></SCRIPT>
<script language="javascript" type="text/javascript">
 isIE=document.all;
function SelColor()
{
    var tC=document.getElementById("attachedFieldValueCyan").value
    var tM=document.getElementById("attachedFieldValueMagenta").value
    var tY=document.getElementById("attachedFieldValueYellow").value
    var tK=document.getElementById("attachedFieldValueBlack").value;
    var hx= getColorHex(parseInt(tC),parseInt(tM),parseInt(tY),parseInt(tK));
    if(isIE)
    {
        
        window.opener.execScript("SetObjColor2('"+ tC +"','"+ tM +"','"+ tY +"','"+ tK+"','#"+hx+"');","JavaScript");
    }
    else
    {
        window.opener.document.getElementById("tmpColorStr").value= tC + "," + tM + "," + tY + "," + tK + ",#" + hx;
    }
    window.close();
    window.opener.focus();
}
</script>
</head>
<body onload="init();" style="padding:10px">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
    <div>
    <table cellpadding="0" cellspacing="0" border="0">
    <tr><td id="tdcolorpicker" runat="server">
    <h1 style="padding:0px;margin:0px"> Colour Picker</h1>
    </td></tr>
    <tr><td style="height:10px"></td></tr>
    <tr><td style="background-color:White; border:solid 1px black;padding:4px;padding-top:0px;">
    <table cellpadding="0" cellspacing="0" border="0" style="padding:0px;">
    <tr><td>
    <table cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td style="width:70px;padding-top:12px" id="tdcyan" runat="server">Cyan</td>
    <td style="width:250px">
    <table cellpadding="0" cellspacing="0" border="0">
    <tr><td><div id="sliderCyan"></div> </td></tr></table>
        
    </td>
    <td style="padding-top:10px">
    <input runat="server" id="attachedFieldValueCyan"
 value="0" size="2" class="inputInt" style="width:40px;height:18px" maxlength="3" name="attachedFieldValueCyan" onkeyup="mySliderCyan.onChangeByInput(this.value, false, 1);" /> %
    </td>
    </tr>
    
     <tr>
    <td style="width:70px;padding-top:12px" id="tdmagenta" runat="server">Magenta</td>
    <td style="width:250px">
    <table cellpadding="0" cellspacing="0" border="0">
    <tr><td><div id="sliderMagenta"></div> </td></tr></table>
        
    </td>
    <td style="padding-top:10px">
    <input runat="server" id="attachedFieldValueMagenta"
 value="0" size="2" class="inputInt" style="width:40px;height:18px" maxlength="3" name="attachedFieldValueMagenta" onkeyup="mySliderMagenta.onChangeByInput(this.value, false, 1);" /> %
    </td>
    </tr>
     <tr>
    <td style="width:70px;padding-top:12px" id="tdyellow" runat="server">Yellow</td>
    <td style="width:250px">
    <table cellpadding="0" cellspacing="0" border="0">
    <tr><td><div id="sliderYellow"></div> </td></tr></table>
        
    </td>
    <td style="padding-top:10px">
    <input runat="server" id="attachedFieldValueYellow"
 value="0"  class="inputInt" style="width:40px;height:18px" size="2" maxlength="3" name="attachedFieldValueYellow" onkeyup="mySliderYellow.onChangeByInput(this.value, false, 1);" /> %
    </td>
    </tr>
     <tr>
    <td style="width:70px;padding-top:12px" id="tdblack" runat="server">Black</td>
    <td style="width:250px">
    <table cellpadding="0" cellspacing="0" border="0">
    <tr><td><div id="sliderBlack"></div> </td></tr></table>
        
    </td>
    <td style="padding-top:10px">
    <input runat="server" id="attachedFieldValueBlack"
    value="0"  class="inputInt" style="width:40px;height:18px" size="2" maxlength="3" name="attachedFieldValueBlack" onkeyup="mySliderBlack.onChangeByInput(this.value, false, 1);" /> %
    </td>
    </tr>
    </table>
    </td>
    <td style="width:100px;text-align:right;vertical-align:middle;">
    <div style="border:solid 1px black;width:80px;height:80px" id="ColorPallet">
    
    </div>
    </td>
    </tr>
    
    </table>
    </td></tr>
    <tr><td style="height:10px"></td></tr>
    <tr><td align="right" style="background-color:White; border:solid 1px black;padding:3px">
    <input type="button" value="  Ok  " id="btnOk" class="button1" onclick="SelColor()" />
    <input type="button" value="  Cancel  " onclick="window.close()" id="Button1" class="button1" />
    </td></tr>
    </table>
    </div>
    </form>
</body>
</html>
