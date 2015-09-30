$(".collapseDesignerMenu").click(function (event) {
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage0");
    if (canvas.getActiveGroup())
        canvas.discardActiveGroup();
    if (canvas.getActiveObject())
        canvas.discardActiveObject();
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        //  $("#FrontBackOptionPanalSection").addClass("hideRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
    $(".collapseDesignerMenu").css("display", "none");
});
$(".search").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup(); canvas.renderAll();
    } else if (D1AO) {
        canvas.discardActiveObject(); canvas.renderAll();
    }
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage1");

    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
    
    $(".collapseDesignerMenu").css("display", "list-item");

});
$(".layout").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup(); canvas.renderAll();
    } else if (D1AO) {
        canvas.discardActiveObject(); canvas.renderAll();
    }
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage2");
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
    $(".collapseDesignerMenu").css("display", "list-item");
});
$(".QuickTxt").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup(); canvas.renderAll();
    } else if (D1AO) {
        canvas.discardActiveObject(); canvas.renderAll();
    }
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage3");
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    } $(".collapseDesignerMenu").css("display", "list-item");
});
$("#btnAdd").click(function (event) {
    isBKpnl = false;
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    if (spPanel != "") {
        $(spPanel).click();
        spPanel = "";
    }
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage4");
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
    try {

        if (canvas && canvas != undefined) {
            var a0 = canvas.getActiveObject();
            if (a0) {
                if (a0.type != "image") {
                    canvas.discardActiveObject();
                }

            }
        } $(".collapseDesignerMenu").css("display", "list-item");
    }
    catch (err) {
      
    }
    //var D1AO = canvas.getActiveObject();
    //var D1AG = canvas.getActiveGroup();
    //if (D1AG) canvas.discardActiveGroup();
    //if (D1AO) canvas.discardActiveObject();
});
$(".backgrounds").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup(); canvas.renderAll();
    } else if (D1AO) {
        canvas.discardActiveObject(); canvas.renderAll();
    }
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    isBkPnlUploads = true;
    isBKpnl = true;
    if (spBkPanel != "") {
        $(spBkPanel).click();
        spBkPanel = "";
    }
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage5");
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
     $(".collapseDesignerMenu").css("display", "list-item");
});
$(".uploads").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    isBkPnlUploads = false; $(".collapseDesignerMenu").css("display", "list-item");
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage6");
});
$(".layersPanel").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup(); canvas.renderAll();
    } else if (D1AO) {
        canvas.discardActiveObject(); canvas.renderAll();
    }
    m0();
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    isBkPnlUploads = true; $(".collapseDesignerMenu").css("display", "list-item");
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage8");
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
});
$(".layoutsPanel").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup(); canvas.renderAll();
    } else if (D1AO) {
        canvas.discardActiveObject(); canvas.renderAll();
    }
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage10");
    l2_temp(); $(".collapseDesignerMenu").css("display", "list-item");
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
    ;
});
$("#BtnCopyObjTxtRetail").click(function (event) {
    pcL10();
});
$("#BtnCopyObjImgRetail").click(function (event) {
    pcL10();
});
$("#btnDelImgRetail").click(function () {
    pcL21();

});
$("#BtnImgScaleINRetail").click(function (event) {
    pcL14();
});

$("#BtnImgScaleOutRetail").click(function (event) {
    pcL15();

});
$('#btnAddRectangle').click(function (event) {
    //c12
    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "rectangle";
    D1NTO.ContentString = "rectangle";
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ObjectType = 6; //rectangle
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 200;
    D1NTO.MaxHeight = 200;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    var ROL = new fabric.Rect({
        left: 0,
        top: 0,
        fill: '#000000',
        width: 100 * dfZ1l,
        height: 100 * dfZ1l,
        opacity: 1
    })

    ROL.maxWidth = 200;
    ROL.maxHeight = 200;
    ROL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });

    ROL.ObjectID = D1NTO.ObjectID;
    canvas.add(ROL);

    canvas.centerObject(ROL);
    // getting object index
    var index;
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        if (IT.ObjectID == ROL.ObjectID) {
            index = i;
        }
    });
    D1NTO.DisplayOrderPdf = index;

    D1NTO.PositionX = ROL.left - ROL.maxWidth / 2;
    D1NTO.PositionY = ROL.top - ROL.maxHeight / 2;
    ROL.setCoords();

    ROL.C = "0";
    ROL.M = "0";
    ROL.Y = "0";
    ROL.K = "100";
    canvas.renderAll();
    TO.push(D1NTO);
    f1(6);
    pcL36('hide', '#divImageDAM');
});

$('#btnAddCircle').click(function (event) {
    //c13
    var NewCircleObejct = {};
    NewCircleObejct = fabric.util.object.clone(TO[0]);
    NewCircleObejct.Name = "Ellipse";
    NewCircleObejct.ContentString = "Ellipse";
    NewCircleObejct.ObjectID = --NCI;
    NewCircleObejct.ColorHex = "#000000";
    NewCircleObejct.ColorC = 0;
    NewCircleObejct.ColorM = 0;
    NewCircleObejct.ColorY = 0;
    NewCircleObejct.ColorK = 100;

    NewCircleObejct.IsItalic = false;
    NewCircleObejct.ObjectType = 7; //ellipse/circle
    NewCircleObejct.ProductPageId = SP;
    NewCircleObejct.MaxWidth = 100;
    NewCircleObejct.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    NewCircleObejct.CircleRadiusX = 50;
    NewCircleObejct.CircleRadiusY = 50;
    NewCircleObejct.Opacity = 1;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        NewCircleObejct.IsSpotColor = true;
        NewCircleObejct.SpotColorName = 'Black';
    }
    var COL = new fabric.Ellipse({
        left: 0,
        top: 0,
        fill: '#000000',
        rx: 50 * dfZ1l,
        ry: 50 * dfZ1l,
        opacity: 1
    })

    COL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });

    COL.ObjectID = NewCircleObejct.ObjectID;
    canvas.add(COL);
    canvas.centerObject(COL);


    // getting object index
    var index;
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        if (IT.ObjectID == COL.ObjectID) {
            index = i;
        }
    });
    NewCircleObejct.DisplayOrderPdf = index;

    NewCircleObejct.PositionX = COL.left - COL.width / 2;
    NewCircleObejct.PositionY = COL.top - COL.width / 2;
    COL.setCoords();
    COL.C = "0";
    COL.M = "0";
    COL.Y = "0";
    COL.K = "100";
    canvas.renderAll();
    TO.push(NewCircleObejct);
    //COL.setFill(""black"");
    //c2(COL,undefined,""abc"");
    // c2(COL);
    pcL36('hide', '#divImageDAM');
});

$("#LockPositionImg").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();

    if (thisCheck.is(':checked')) {
        D1AO.IsPositionLocked = true;
        D1AO.lockMovementX = true;
        D1AO.lockMovementY = true;
        D1AO.lockScalingX = true;
        D1AO.lockScalingY = true;
        D1AO.lockRotation = true;
    }
    else {
        D1AO.IsPositionLocked = false;
        D1AO.lockMovementX = false;
        D1AO.lockMovementY = false;
        D1AO.lockScalingX = false;
        D1AO.lockScalingY = false;
        D1AO.lockRotation = false;
    }

});
$("#LockImgProperties").click(function () {

    var thisCheck = $(this);

    var D1AO = canvas.getActiveObject();
    if (thisCheck.is(':checked')) {
        D1AO.IsTextEditable = true;
      //  D1AO.IsPositionLocked = true;
     //   $("#LockPositionImg").prop('checked', true);
    }
    else {
        D1AO.IsTextEditable = false;
    }
    //  c2(D1AO);
    g1_(D1AO);
});

function g1_(D1AO) {
    $("#BtnSearchTxt").removeAttr("disabled");

    if (D1AO.IsPositionLocked) {
        $("#LockPositionImg").prop('checked', true);
    }
    else {
        $("#LockPositionImg").prop('checked', false);
    }
    if (D1AO.IsHidden) {
        $("#BtnPrintImage").prop('checked', true);
    }
    else {
        $("#BtnPrintImage").prop('checked', false);
    } 
    if (D1AO.IsTextEditable) {
        $("#LockImgProperties").prop('checked', true);
    }
    else {
        $("#LockImgProperties").prop('checked', false);
    }
    if (D1AO.IsOverlayObject) {
        $("#chkboxOverlayImg").prop('checked', true);
    }
    else {
        $("#chkboxOverlayImg").prop('checked', false);
    }
    if (D1AO.IsTextEditable != true) {
        $("#BtnTxtarrangeOrder1").removeAttr("disabled");
        $("#BtnTxtarrangeOrder2").removeAttr("disabled");
        $("#BtnTxtarrangeOrder3").removeAttr("disabled");
        $("#BtnTxtarrangeOrder4").removeAttr("disabled");
        $("#AddColorShape").removeAttr("disabled");
        $("#BtnDeleteTxtObj").removeAttr("disabled");
        $("#BtnImgRotateLeft").removeAttr("disabled");
        $("#BtnImgRotateRight").removeAttr("disabled");
        $(".rotateSlider").removeAttr("disabled");
        $("#LockPositionImg").removeAttr("disabled");
        $("#BtnPrintImage").removeAttr("disabled");
        $("#btnImgCanvasAlignLeft").removeAttr("disabled");
        $("#BtnImgCanvasAlignCenter").removeAttr("disabled");
        $("#BtnImgCanvasAlignMiddle").removeAttr("disabled");
        $("#BtnImgCanvasAlignRight").removeAttr("disabled");
        $("#inputObjectWidth").spinner("option", "disabled", false);
        $("#inputObjectHeight").spinner("option", "disabled", false);
        $("#inputPositionX").spinner("option", "disabled", false);
        $("#inputPositionY").spinner("option", "disabled", false);
        $('.inputObjectAlphaSlider').slider('enable');
        if (D1AO.IsPositionLocked == true && (IsCalledFrom == 3 || IsCalledFrom ==4) ) {
            $("#BtnTxtarrangeOrder1").removeAttr("disabled");
            $("#BtnTxtarrangeOrder2").removeAttr("disabled");
            $("#BtnTxtarrangeOrder3").removeAttr("disabled");
            $("#BtnTxtarrangeOrder4").removeAttr("disabled");
            $("#AddColorShape").removeAttr("disabled");
            $("#BtnDeleteTxtObj").removeAttr("disabled");
            $("#BtnImgRotateLeft").removeAttr("disabled");
            $("#BtnImgRotateRight").removeAttr("disabled");
            $(".rotateSlider").removeAttr("disabled");
            $("#LockPositionImg").removeAttr("disabled");
            $("#BtnPrintImage").removeAttr("disabled");
            $("#btnImgCanvasAlignLeft").removeAttr("disabled");
            $("#BtnImgCanvasAlignCenter").removeAttr("disabled");
            $("#BtnImgCanvasAlignMiddle").removeAttr("disabled");
            $("#BtnImgCanvasAlignRight").removeAttr("disabled");
            $('.inputObjectAlphaSlider').slider('enable');
            
            $(".positioningControls").css("display","none");

        } else {
            $(".positioningControls").css("display", "block");
        }
    }
    else {
        $("#BtnTxtarrangeOrder1").attr("disabled", "disabled");
        $("#BtnTxtarrangeOrder2").attr("disabled", "disabled");
        $("#BtnTxtarrangeOrder3").attr("disabled", "disabled");
        $("#BtnTxtarrangeOrder4").attr("disabled", "disabled");
        $("#AddColorShape").attr("disabled", "disabled");
        $("#BtnDeleteTxtObj").attr("disabled", "disabled");
        $("#BtnImgRotateLeft").attr("disabled", "disabled");
        $("#BtnImgRotateRight").attr("disabled", "disabled");
        $(".rotateSlider").attr("disabled", "disabled");
        $("#LockPositionImg").attr("disabled", "disabled");
        $("#BtnPrintImage").attr("disabled", "disabled");
        $("#btnImgCanvasAlignLeft").attr("disabled", "disabled");
        $("#BtnImgCanvasAlignCenter").attr("disabled", "disabled");
        $("#BtnImgCanvasAlignMiddle").attr("disabled", "disabled");
        $("#BtnImgCanvasAlignRight").attr("disabled", "disabled");
        $("#inputObjectWidth").spinner("option", "disabled", true);
        $("#inputObjectHeight").spinner("option", "disabled", true);
        $("#inputPositionX").spinner("option", "disabled", true);
        $("#inputPositionY").spinner("option", "disabled", true);
        $('.inputObjectAlphaSlider').slider('disable');
        $(".positioningControls").css("display", "block");
    }
}

function g1(D1AO) {
    $("#BtnSearchTxt").removeAttr("disabled");
    if (IsCalledFrom == 3) {
        $("#BtnSelectFontsRetail").fontSelector('option', 'font', D1AO.get('fontFamily'));
    } else {
        $("#BtnSelectFonts").fontSelector('option', 'font', D1AO.get('fontFamily'));
    }
    $("#BtnFontSize").val(k13(D1AO.get('fontSize')));
    $("#BtnFontSizeRetail").val(k13(D1AO.get('fontSize')));
    $("#txtLineHeight").val(D1AO.get('lineHeight'));
    $("#inputcharSpacing").val(k13(D1AO.get('charSpacing')));
    //  $("#txtAreaUpdateTxtPropPanel").val(D1AO.text);
    if (D1AO.IsPositionLocked) {
        $("#BtnLockTxtPosition").prop('checked', true);
    }
    else {
        $("#BtnLockTxtPosition").prop('checked', false);
    }
    if (D1AO.IsHidden) {
        $("#BtnPrintObj").prop('checked', true);
    }
    else {
        $("#BtnPrintObj").prop('checked', false);
    } //alert(IsEmbedded + " " +  IT.IsTextEditable);
    if (D1AO.IsTextEditable) {
        $("#BtnAllowOnlyTxtChange").prop('checked', true);
    }
    else {
        $("#BtnAllowOnlyTxtChange").prop('checked', false);
    }
    if (D1AO.AutoShrinkText) {
        $("#chkboxAutoShrink").prop('checked', true);
    }
    else {
        $("#chkboxAutoShrink").prop('checked', false);
    }
    if (D1AO.autoCollapseText) {
        $("#chkboxAutoCollapse").prop('checked', true);
    }
    else {
        $("#chkboxAutoCollapse").prop('checked', false);
    }
    if (!D1AO.IsEditable) {
        $("#BtnLockEditing").prop('checked', true);
    } else {
        $("#BtnLockEditing").prop('checked', false);
    }
    if (D1AO.IsOverlayObject) {
        $("#chkboxOverlayTxt").prop('checked', true);
    }
    else {
        $("#chkboxOverlayTxt").prop('checked', false);
    }
    if (D1AO.IsTextEditable != true) {
        if (D1AO.IsEditable) {
            $("#BtnLockEditing").prop('checked', false);
        }
        $("#BtnJustifyTxt1").removeAttr("disabled");
        $("#BtnJustifyTxt2").removeAttr("disabled");
        $("#BtnJustifyTxt3").removeAttr("disabled");
        $("#BtnTxtarrangeOrder1").removeAttr("disabled");
        $("#BtnTxtarrangeOrder2").removeAttr("disabled");
        $("#BtnTxtarrangeOrder3").removeAttr("disabled");
        $("#BtnTxtarrangeOrder4").removeAttr("disabled");
        $("#BtnLockEditing").removeAttr("disabled");
        $("#BtnSearchTxt").removeAttr("disabled");
        //$("#BtnUpdateText").removeAttr("disabled");
        $("#BtnSelectFonts").removeAttr("disabled");
        if (IsCalledFrom == 3) {
            $("#BtnSelectFontsRetail").removeAttr("disabled");
        }
        $("#BtnFontSize").removeAttr("disabled");
        $("#BtnFontSizeRetail").removeAttr("disabled");
        $("#BtnBoldTxt").removeAttr("disabled");
        $("#BtnBoldTxtRetail").removeAttr("disabled");
        $("#BtnItalicTxt").removeAttr("disabled");
        $("#BtnItalicTxtRetail").removeAttr("disabled");
        $("#txtLineHeight").removeAttr("disabled");
        $("#BtnChngeClr").removeAttr("disabled");
        $("#BtnDeleteTxtObj").removeAttr("disabled");
        $("#BtnRotateTxtLft").removeAttr("disabled");
        $("#BtnRotateTxtRight").removeAttr("disabled");
        $("#BtnLockTxtPosition").removeAttr("disabled");
        $("#BtnPrintObj").removeAttr("disabled");
        if (IsEmbedded && !D1AO.IsEditable) {
          //  $("#BtnLockEditing").attr("disabled", "disabled");
        }
        if (IsEmbedded && D1AO.IsPositionLocked) {
            $("#BtnLockTxtPosition").attr("disabled", "disabled");
        } $(".fontSelector").removeAttr("disabled");
        $("#BtnTxtCanvasAlignLeft").removeAttr("disabled");
        $("#BtnTxtCanvasAlignCenter").removeAttr("disabled");
        $("#BtnTxtCanvasAlignRight").removeAttr("disabled");
        $("#BtnTxtCanvasAlignTop").removeAttr("disabled");
        $("#BtnTxtCanvasAlignMiddle").removeAttr("disabled");
        $("#BtnTxtCanvasAlignBottom").removeAttr("disabled");
        $("#inputcharSpacing").spinner("option", "disabled", false);
        $("#BtnFontSize").spinner("option", "disabled", false);
        $("#txtLineHeight").spinner("option", "disabled", false);
     //   $("#inputObjectWidthTxt").spinner("option", "disabled", false);
    //    $("#inputObjectHeightTxt").spinner("option", "disabled", false);
     //   $("#inputPositionXTxt").spinner("option", "disabled", false);
        //   $("#inputPositionYTxt").spinner("option", "disabled", false);
       
    }
    else {
        $("#inputcharSpacing").spinner("option", "disabled", true);
        if (!D1AO.IsEditable) {
            $("#BtnLockEditing").prop('checked', true);
        }
        $("#BtnJustifyTxt1").attr("disabled", "disabled");
        $("#BtnJustifyTxt2").attr("disabled", "disabled");
        $("#BtnJustifyTxt3").attr("disabled", "disabled");
        $("#BtnTxtarrangeOrder1").attr("disabled", "disabled");
        $("#BtnTxtarrangeOrder2").attr("disabled", "disabled");
        $("#BtnTxtarrangeOrder3").attr("disabled", "disabled");
        $("#BtnTxtarrangeOrder4").attr("disabled", "disabled");
        //$("#EditTXtArea").attr("disabled", "disabled");
        $("#BtnSearchTxt").attr("disabled", "disabled");
        //$("#BtnUpdateText").attr("disabled", "disabled");
        $("#BtnSelectFonts").attr("disabled", "disabled");
        if (IsCalledFrom == 3) {
            $("#BtnSelectFontsRetail").attr("disabled", "disabled");
        }
        $("#BtnFontSize").attr("disabled", "disabled");
        $("#BtnFontSizeRetail").attr("disabled", "disabled");
        $("#BtnBoldTxt").attr("disabled", "disabled");
        $("#BtnBoldTxtRetail").attr("disabled", "disabled");
        $("#BtnItalicTxt").attr("disabled", "disabled");
        $("#BtnItalicTxtRetail").attr("disabled", "disabled");
        $("#txtLineHeight").attr("disabled", "disabled");
        $("#BtnChngeClr").attr("disabled", "disabled");
        $("#BtnDeleteTxtObj").attr("disabled", "disabled");
        $("#BtnRotateTxtLft").attr("disabled", "disabled");
        $("#BtnRotateTxtRight").attr("disabled", "disabled");
        $("#BtnLockTxtPosition").attr("disabled", "disabled");
        $("#BtnPrintObj").attr("disabled", "disabled");

        $("#BtnTxtCanvasAlignLeft").attr("disabled", "disabled");
        $("#BtnTxtCanvasAlignCenter").attr("disabled", "disabled");
        $("#BtnTxtCanvasAlignRight").attr("disabled", "disabled");
        $("#BtnTxtCanvasAlignTop").attr("disabled", "disabled");
        $("#BtnTxtCanvasAlignMiddle").attr("disabled", "disabled");
        $("#BtnTxtCanvasAlignBottom").attr("disabled", "disabled");
        $("#BtnFontSize").spinner("option", "disabled", true);
        $("#txtLineHeight").spinner("option", "disabled", true);

        $("#inputObjectWidthTxt").spinner("option", "disabled", true);
        $("#inputObjectHeightTxt").spinner("option", "disabled", true);
        $("#inputPositionXTxt").spinner("option", "disabled", true);
        $("#inputPositionYTxt").spinner("option", "disabled", true);
        $(".fontSelector").attr("disabled", "disabled"); 
    }
    //$.each(TO, function (i, IT) {

    //    if (IT.ObjectID == D1AO.ObjectID) {

    //    }
    //});


    if ((IsEmbedded && (IsCalledFrom == 4)) || (IsEmbedded && (IsCalledFrom == 3))) {
        $("#BtnLockTxtPosition").css("visibility", "hidden");
        $("#lblLockPositionTxt").css("visibility", "hidden");
        $("#BtnAllowOnlyTxtChange").css("visibility", "hidden");
        $("#lblAllowOnlyTxtChng").css("visibility", "hidden");
        $("#BtnLockEditing").css("visibility", "hidden");
        $("#lblLockEditingTxt").css("visibility", "hidden");
        $("#BtnPrintObj").css("display", "none");
        $("#lblDoNotPrintTxt").css("display", "none");
        $("#BtnUploadFont").css("visibility", "hidden");
        $("#textPropertPanel").css("height", "330px");
        $("#divSepratrTxtPos").css("visibility", "hidden");
        $("#chkboxAutoShrink").css("visibility", "hidden");
        $("#lblAutoShrink").css("visibility", "hidden");
    }
}
$("#BtnImgRotateLeft").click(function () {
    pcL16();
});

$("#BtnImgRotateRight").click(function () {
    pcL17();
});
$("#BtnImgRotateLeftRetail").click(function () {
    pcL16();
});
$("#BtnImgRotateRightRetail").click(function () {
    pcL17();
});
$("#BtnImageArrangeOrdr1Retail").click(function () {
    pcL26();
});


//document.getElementById('BtnImgRotateLeft').onclick = function (ev) {
//    pcL16();
//}

//document.getElementById('BtnImgRotateRight').onclick = function (ev) {
//    pcL17();
//}
//document.getElementById('BtnImgRotateLeftRetail').onclick = function (ev) {
//    pcL16();
//}

//document.getElementById('BtnImgRotateRightRetail').onclick = function (ev) {
//    pcL17();
//}
//document.getElementById('BtnImageArrangeOrdr1Retail').onclick = function (ev) {
//    pcL26();
//}
//document.getElementById('BtnImageArrangeOrdr4Retail').onclick = function (ev) {
//    pcL25();
//}
//document.getElementById('BtnImageArrangeOrdr2Retail').onclick = function (ev) {
//    pcL18();
//}

//document.getElementById('BtnImageArrangeOrdr3Retail').onclick = function (ev) {
//    pcL19();
//}
//document.getElementById('BtnRotateTxtLft').onclick = function (ev) {
//    pcL11();
//}
//document.getElementById('BtnRotateTxtRight').onclick = function (ev) {
//    pcL12();
//}
//document.getElementById('BtnRotateTxtLftRetail').onclick = function (ev) {
//    pcL11();
//}
//document.getElementById('BtnRotateTxtRightRetail').onclick = function (ev) {
//    pcL12();
//}
//document.getElementById('BtnTxtarrangeOrder1Retail').onclick = function (ev) {
//    pcL26();
//}
//document.getElementById('BtnTxtarrangeOrder2Retail').onclick = function (ev) {
//    pcL27();
//}
//document.getElementById('BtnTxtarrangeOrder3Retail').onclick = function (ev) {
//    pcL28();
//}
//document.getElementById('BtnTxtarrangeOrder4Retail').onclick = function (ev) {
//    pcL25();
//}
//document.getElementById('BtnSelectFontsRetail').onchange = function (ev) {
//    pcL04();
//}
//document.getElementById('btnDeleteTxt').onclick = function (ev) {
//    pcL03();

//}
//document.getElementById('BtnBoldTxtRetail').onclick = function (ev) {
//    pcL05();

//}
//document.getElementById('BtnItalicTxtRetail').onclick = function (ev) {
//    pcL06();
//}
//document.getElementById('BtnJustifyTxt1Retail').onclick = function (ev) {
//    pcL07();
//}

//document.getElementById('BtnJustifyTxt2Retail').onclick = function (ev) {
//    pcL08();
//}
//document.getElementById('BtnJustifyTxt3Retail').onclick = function (ev) {
//    pcL09();
//}
$('#BtnJustifyTxt3Retail').click(function (event) {
    pcL09();
});
$('#BtnJustifyTxt2Retail').click(function (event) {
    pcL08();
});
$('#BtnJustifyTxt1Retail').click(function (event) {
    pcL07();
});
$('#BtnTxtarrangeOrder2Retail').click(function (event) {
    pcL27();
});
$('#BtnTxtarrangeOrder3Retail').click(function (event) {
    pcL28();
});
$('#BtnTxtarrangeOrder4Retail').click(function (event) {
    pcL25();
});
$('#BtnSelectFontsRetail').click(function (event) {
    pcL04();
});
$('#btnDeleteTxt').click(function (event) {
    pcL03();
});
$('#BtnBoldTxtRetail').click(function (event) {
    pcL05();
});
$('#BtnItalicTxtRetail').click(function (event) {
    pcL06();
});
$('#BtnUnderlineTxt').click(function (event) {
   
   // pcL06ULine();
});
$('#BtnImageArrangeOrdr4Retail').click(function (event) {
    pcL25();
});
$('#BtnImageArrangeOrdr2Retail').click(function (event) {
    pcL18();
});
$('#BtnImageArrangeOrdr3Retail').click(function (event) {
    pcL19();
});
$('#BtnRotateTxtLft').click(function (event) {
    pcL11();
});
$('#BtnRotateTxtRight').click(function (event) {
    pcL12();
});
$('#BtnRotateTxtLftRetail').click(function (event) {
    pcL11();
});
$('#BtnRotateTxtRightRetail').click(function (event) {
    pcL12();
});
$('#BtnTxtarrangeOrder1Retail').click(function (event) {
    pcL26();
});

$('#AddColorTxtRetailNew').click(function (event) {
    pcL02();
});
$('#AddColorImgRetailNew').click(function (event) {
    pcL02();
});
$('#AddBkColorRetailNew').click(function (event) {
    pcL02_bK();
});
$("#BtnCropImgRetail").click(function (event) {
    //pcL20();
    pcL20_new();
});
$("#BtnCropImg").click(function (event) {
    pcL20();
});
$("#BtnCropImg2").click(function (event) {
    //pcL20();
    pcL20_new();
});
$(".freeBackgrounds").click(function (event) {
    fu13(2, 2, 1, 1);
    pcL29_pcMove(7);
    spBkPanel = ".btnBkBkimgs";
});
$(".btnBkBkimgs").click(function (event) {
    fu13(2, 2, 1, 1);
    pcL29_pcRestore(7); spBkPanel = "";
});
$(".btnBkmyBk").click(function (event) {
    fu13(2, 2, 1, 2);
    pcL29_pcRestore(7); spBkPanel = "";
});
$(".btnBkTempBk").click(function (event) {
    fu13(2, 2, 1, 3);
    pcL29_pcRestore(7); spBkPanel = "";
});
$(".myBackgrounds").click(function (event) {
    fu13(2, 2, 1, 2);
    pcL29_pcMove(7);
    spBkPanel = ".btnBkmyBk";
});
$(".templateBackgrounds").click(function (event) {
    fu13(2, 2, 1, 3);
    pcL29_pcMove(7); spBkPanel = ".btnBkTempBk";
});
$(".BkColors").click(function (event) {
    fu13(2, 2, 2, 1); spBkPanel = ".BkColors";
    $(".BkColors").toggleClass("SelectBkColorPanel");
});
$(".btnFreeImgs").click(function (event) {
    fu13(2, 1, 1, 1);
    pcL29_pcMove(3);
    spPanel = ".btnBackFromImgs , .btnBackGlImgs";
});
$(".btnBackGlImgs").click(function (event) {
    fu13(2, 1, 1, 1);
    pcL29_pcRestore(4); spPanel = ".btnBackFromImgs ";
});
$(".btnQuickText").click(function (event) {
    //$(".stage3").css("width", "20px");
    //$("#resultsQText").css("display", "none");
    
});
$(".btnIllustrations").click(function (event) {
    fu13(2, 1, 1, 2);
    pcL29_pcMove(4); spPanel = ".btnBackFromImgs , .btnBackMyImg";
});
$(".btnBackMyImg").click(function (event) {
    fu13(2, 1, 1, 2);
    pcL29_pcRestore(4); spPanel = ".btnBackFromImgs ";
});
$(".btnFrames").click(function (event) {
    fu13(2, 1, 1, 3);
    pcL29_pcMove(5); spPanel = ".btnBackFromImgs , .btnBackMyLogos";
});
$(".btnBackMyLogos").click(function (event) {
    fu13(2, 1, 1, 3);
    pcL29_pcRestore(5);
    spPanel = ".btnBackFromImgs ";
});
$(".BtnBanners").click(function (event) {
    fu13(2, 1, 2, 1);
});
$(".btnShapes").click(function (event) {
    fu13(2, 1, 2, 2);
});
$(".btnLogos").click(function (event) {
    fu13(2, 1, 2, 3);
});
$(".btnTemplateImages").click(function (event) {
    fu13(2, 1, 3, 1);
    pcL29_pcMove(6); spPanel = ".btnBackFromImgs , .btnBackTimgs";
});
$(".btnPropertyImages").click(function (event) {
    fu13(2, 1, 2, 1);
    pcL29_pcMove(6); spPanel = ".btnBackMyProperty , .btnBackFromImgs";
});
$(".btnBackMyProperty").click(function (event) {
    //   fu13(2, 1, 1, 1);
    $(".realEstateContainer").addClass("disappearing"); selCat = "00";
    pcL29_pcRestore(4); spPanel = "";
});
$(".btnBackTimgs").click(function (event) {
    fu13(2, 1, 3, 1);
    pcL29_pcRestore(6); spPanel = ".btnBackFromImgs ";
});
$(".yourUploads").click(function (event) {
    fu13(2, 3, 1, 1);
});
$(".btnLogos").click(function (event) {
    fu13(2, 3, 1, 2);
});
//$("#uploadImagesMB").click(function (event) {
//    // fu13(2, 4, 1, 1);
//   // $("#uploadBackground").click(); console.log("called");
//});
$(".btnAtext").click(function (event) {
    fu13(2, 4, 1, 2);
    pcL29_pcMove(1);
    spPanel = ".btnBackFromTxt";

});
$(".btnBackFromImgs").click(function (event) {
    fu13(2, 4, 1, 3);
    pcL29_pcRestore(2); spPanel = "";
});
$(".btnBackFromTxt").click(function (event) {
    fu13(2, 4, 1, 2);
    pcL29_pcRestore(1);

});
$(".btnBackFromShapes").click(function (event) {
    fu13(2, 4, 1, 4);
    pcL29_pcRestore(1);
});
$(".btnAFrames").click(function (event) {
    fu13(2, 4, 1, 3);
    pcL29_pcMove(2);

    spPanel = ".btnBackFromImgs";


});
//$(".btnAShapes").click(function (event) {
//    pcL29_pcMove(1);
//    spPanel = ".btnBackFromShapes";
//    fu13(2, 4, 1, 4);
//    //setTimeout(function () {
//    //    fu13(2, 5, 1, 4);
//    //}, 20);

//});
$(".btnAShapes").click(function (event) {
    fu13(2, 4, 1, 4);
    pcL29_pcMove(1);
    spPanel = ".btnBackFromShapes";

});

$("#zoomIn").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    D1CZL += 1;
    e3();
    canvas.renderAll();
    canvas.calcOffset();
});
//$("#BtnOrignalZoom").click(function (event) {
//    var D1AO = canvas.getActiveObject();
//    var D1AG = canvas.getActiveGroup();
//    if (D1AG) {
//        canvas.discardActiveGroup();
//    } else if (D1AO) {
//        canvas.discardActiveObject();
//    }
//    D1CZL = 0;
//    e0();
//    canvas.renderAll();

//});

$('#zoomOut').click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    D1CZL -= 1;
    e5();

    canvas.renderAll();
    canvas.calcOffset();
});
$('.addTxtHeading').click(function () {
    pcL29(26.67, true, "Add text");
});
$('.addTxtSubtitle').click(function () {
    pcL29(21.33, false, "Add subtitle text");
});
$('.addTxtBody').click(function () {
    pcL29(13.33, false, "Add a little bit of body text");
});
$('#btnAddheading').click(function () {
    pcL29(26.67, true, $("#txtareaAddTxt").val());
    $("#txtareaAddTxt").val("");
});
$('#btnAddSubtitle').click(function () {
    pcL29(21.33, false, $("#txtareaAddTxt").val());
    $("#txtareaAddTxt").val("");
});
$('#btnaddbody').click(function () {
    pcL29(13.33, false, $("#txtareaAddTxt").val());
    $("#txtareaAddTxt").val("");
});
$('#btnReplaceImage').click(function () {
    //fu13(2, 4, 1, 3);
    //pcL29_pcMove(2);
    $('.btnAdd').click();
    $('.btnAFrames').click();

});
$('#editorLogo').click(function () {
    if (smartFormClicked) {
        StartLoader("Generating artwork for approval");
        //   parent.Next(); // webstore caller function
        fu12("preview", $("#txtTemplateTitle").val());
        return false;
    } else 
    {
        $(".messageSmartForm").css("display", "block");
        $("#Quick").click();
        //alert("Please merge smart form details into template before you continue !");
        return false;
    }
});
//$('.mainLeftMenu li').click(function () {
//    if ($(this).attr("class").indexOf("backgrounds") != 1) {
//        isBKpnl = true; alert();
//    } else {
//        isBKpnl = false;
//    }
//});
$('#BtnUndo').click(function (event) {
    if (canvas.getActiveGroup()) canvas.discardActiveGroup();
    if (canvas.getActiveObject()) canvas.discardActiveObject();
    undo();

});
$('#BtnRedo').click(function (event) {
    redo();

});
$("#btnNextProofing").click(function (event) {

    var email1 = $("input[name=userEmail1]").val();
    var email2 = $("input[name=userEmail2]").val();
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (email1 != "") {
        if (!filter.test(email1)) {
            alert("Please enter a valid email address in address 1");
            return false;
        }
    }
    if (email2 != "") {
        if (!filter.test(email2)) {
            alert("Please enter a valid email address in address 2");
            return false;
        }
    }
    $(".loadingLayer").css("z-index", "10000001");
    $(".firstLoadingMsg").css("display", "none");
    if ($("#chkCheckSpelling").is(':checked')) {
        StartLoader("Saving your design, please wait...");

        //var emailParameters = "";
        //if (email1 != "") {
        //    emailParameters += "&pE1=" + email1;
        //}
        //if (email2 != "") {
        //    emailParameters += "&pE2=" + email2;
        //}
        //emailParameters += "&IsRC=" + IsRoundedCorners;
        var designerName = $('#txtTemplateTitle').val();
   
        while (designerName.indexOf('/') != -1)
            designerName = designerName.replace("/", "__");
        while (designerName.indexOf('&') != -1)
            designerName = designerName.replace("&", "__");

        while (designerName.indexOf(' ') != -1)
            designerName = designerName.replace(" ", "-");
        while (designerName.indexOf(';') != -1)
            designerName = designerName.replace(";", "-");
        while (designerName.indexOf('&#34;') != -1)
            designerName = designerName.replace("&#34;", "-");
        while (designerName.indexOf('+') != -1)
            designerName = designerName.replace("+", "-");
      
       
        $.getJSON("/designerapi/Template/SaveDesignAttachments/" + tID + "/" + ItemId + "/" + CustomerID + "/" + designerName + "/designer/" + organisationId,
          function (DT) {
              window.location.href = DT;// + "&CategoryId=" + CategoryId + "&ProductName=" + document.getElementById('txtDesignName').value + emailParameters;
          });
    } else {
        alert(ssMsg);
        return false;
    }
});
$("#btnUpdateImgProp").click(function (event) {
    var title = $("#InputImgTitle").val();
    var desc = $("#InputImgDescription").val();
    var keywords = $("#InputImgKeywords").val();
    while (title.indexOf('/') != -1)
        title = title.replace("/", "___");
    while (desc.indexOf('/') != -1)
        desc = desc.replace("/", "___");
    while (keywords.indexOf('/') != -1)
        keywords = keywords.replace("/", "___");
    while (title.indexOf(',') != -1)
        title = title.replace(",", "__");
    while (desc.indexOf(',') != -1)
        desc = desc.replace(",", "__");
    while (keywords.indexOf(',') != -1)
        keywords = keywords.replace(",", "__");


    while (title.indexOf(' ') != -1)
        title = title.replace(" ", "____");
    while (desc.indexOf(' ') != -1)
        desc = desc.replace(" ", "____");
    while (keywords.indexOf(' ') != -1)
        keywords = keywords.replace(" ", "____");

    if (keywords == "") {
        keywords = title;
    }
    if (desc == "") {
        desc = title;
    }
    var imType = 0;
    if (!isBkPnlUploads) {
        if (IsCalledFrom == 3) {
            imType = 1;
        }
        else if (IsCalledFrom == 1) {
            imType = 1;
        } else if (IsCalledFrom == 2) {
            imType = 1;
        }
        if ($("#radioImageLogo").prop('checked')) {
            imType = 14;
            if (IsCalledFrom == 3) {
                imType = 15;
            }
            if (IsCalledFrom == 2) {
                imType = 17;
            }
        }
        //if ($("#radioImageShape").prop('checked')) {
        //    imType = 13;
        //    if (IsCalledFrom == 2) {
        //        imType = 16;
        //    }
        //}
        //if ($("#radioBtnIllustration").prop('checked')) {
        //    imType = 18;
        //}
        //if ($("#radioBtnFrames").prop('checked')) {
        //    imType = 19;
        //}
        //if ($("#radioBtnBanners").prop('checked')) {
        //    imType = 20;
        //}
    }
    StartLoader("Updating image information, please wait...");
    $.getJSON("/designerApi/TemplateBackgroundImage/UpdateImage/" + imgSelected + "/" + imType + "/" + title + "/" + desc + "/" + keywords + "/",
	function (DT) {
	    StopLoader();
	    if (IsCalledFrom == 3) {
	        if (imType == 15 || imType == 1) {
	            k27();
	        }
            
	    } else if (IsCalledFrom == 2)
	    {
	        var Territories = "_";
	        $('#dropDownTerritories  div :input').each(function (i) {
	            if ($(this).prop('checked')) {
	                var arr = $(this).attr('id').split('_');
	                Territories += arr[arr.length - 1] + "_";
	            }
	        });
	        //if (Territories != "") {
	        StartLoader();
	        $.getJSON("/designerApi/TemplateBackgroundImage/updateImgTerritories/" + imgSelected + "/" + Territories,
            function (DT) {
                StopLoader();
                pcL36('show', '#divImageDAM');
            });
	        // }
	    }
        
	    //if (imgLoaderSection == 1) {
	    //    $(".search").click();
	    //} else if (imgLoaderSection == 2) {
	    //    $(".backgrounds").click();
	    //} else {
	    //    $(".uploads").click();
	    //}
	    $("#btnAdd").click();
	});

    return false;
});
$(".returnToLib").click(function (event) {
    if (imgLoaderSection == 1) {
        $(".text").click();
    } else if (imgLoaderSection == 2) {
        $(".backgrounds").click();
    } else {
        if (isBkPnlUploads){
            $(".backgrounds").click();
        } else {
            $('.btnAdd').click();
            $('.btnAFrames').click();
            $(".btnIllustrations").click();
        }
        
    } 
});
$(".returnToLayers").click(function (event) {
    $(".layersPanel").click();
});
$("#btnDeleteImg").click(function (event) {
    b8(imgSelected, tID);
    if (imgLoaderSection == 1) {
        $(".search").click();
    } else if (imgLoaderSection == 2) {
        $(".backgrounds").click();
    } else {
        $(".uploads").click();
    }
    return false;
});


$('#inputSearchTImg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k22();
        k25Ills();
        k25Frames();
        k25Banners();
        k22Sh();
        k22Log();
        k19();
        if (!isImgPaCl) {
            $(".btnFreeImgs").click();
        }
        return false;
    }
});
$('#inputSearchTBkg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k19Bk();
        k19Bk();
        k25Bk();

        if (!isBkPaCl) {
            $(".freeBackgrounds").click();
        }
        return false;
    }
});
$('#inputSearchPImg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k25();
        k22LogP();
        if (!isUpPaCl) {
            $(".yourUploads").click();
        }
        return false;
    }
});
$('input, textarea, select').focus(function () {
    IsInputSelected = true;
}).blur(function () {
    IsInputSelected = false;
});
$('body').keydown(function (e) {
    try {
        if (canvas && canvas != undefined) {

            var DIA0 = canvas.getActiveObject();
            if (DIA0 && DIA0.isEditing) {
                return
            } else {
                l3(e);
            }
        }
    } catch (err) {
        //document.getElementById("demo").innerHTML = err.message;
    }
});

$('body').keyup(function (event) {
    try {
        if (canvas && canvas != undefined) {
            var DIA0 = canvas.getActiveObject();
            if (DIA0 && DIA0.isEditing) {
                return
            } else {
                l2(event);
            }
        }
    }
    catch (err) {
        //document.getElementById("demo").innerHTML = err.message;
    }

});

//$("#uploadImages , #uploadImagesMB, #uploadLogos").click(function (event) {
//    isBKpnl = false;
//    $("#uploadBackground").click();
//});
//$("#uploadBackgroundMn").click(function (event) {
//    isBKpnl = true;
//    $("#uploadBackground").click();
//});

$("#BtnAlignObjCenter").click(function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            //c17
            var minID = 0;
            var mintop = 0;
            var left = 0;
            mintop = D1AG[0].top;
            minID = D1AG[0].ObjectID;
            left = D1AG[0].left;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].left = left;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
});


$("#BtnAlignObjLeft").click(function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var mintop = 0;
            var left = 0
            mintop = D1AG[0].top;
            minID = D1AG[0].ObjectID;
            left = D1AG[0].left - D1AG[0].getWidth() / 2;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].left = left + D1AG[i].getWidth() / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
});
$("#BtnAlignObjRight").click(function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var mintop = 0;
            var left = 0
            mintop = D1AG[0].top;
            minID = D1AG[0].ObjectID;
            left = D1AG[0].left + D1AG[0].getWidth() / 2;

            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].left = left - D1AG[i].getWidth() / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
});
$("#BtnAlignObjTop").click(function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var minLeft = 99999;
            var top = 0
            minLeft = D1AG[0].left;
            minID = D1AG[0].ObjectID;
            top = D1AG[0].top - D1AG[0].getHeight() / 2;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].top = top + D1AG[i].getHeight() / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
});
$("#BtnAlignObjMiddle").click(function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var minLeft = 99999;
            var top = 0
            minLeft = D1AG[0].left;
            minID = D1AG[0].ObjectID;
            top = D1AG[0].top;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].top = top;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
});
$("#BtnAlignObjBottom").click(function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var minLeft = 99999;
            var top = 0
            minLeft = D1AG[0].left;
            minID = D1AG[0].ObjectID;
            top = D1AG[0].top + D1AG[0].getHeight() / 2;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].top = top - D1AG[i].getHeight() / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
});



//document.getElementById('BtnAlignObjLeft').onclick = function (ev) {
//    if (canvas.getActiveGroup()) {
//        var D1AG = canvas.getActiveGroup().getObjects();
//        if (D1AG) {
//            var minID = 0;
//            var mintop = 0;
//            var left = 0
//            mintop = D1AG[0].top;
//            minID = D1AG[0].ObjectID;
//            left = D1AG[0].left - D1AG[0].currentWidth / 2;
//            if (D1AG) {
//                for (i = 0; i < D1AG.length; i++) {
//                    if (D1AG[i].ObjectID != minID) {
//                        D1AG[i].left = left + D1AG[i].currentWidth / 2;
//                    }
//                }
//                canvas.discardActiveGroup();
//            }
//            canvas.renderAll();
//        }
//    }
//}

//document.getElementById('BtnAlignObjRight').onclick = function (ev) {
//    if (canvas.getActiveGroup()) {
//        var D1AG = canvas.getActiveGroup().getObjects();
//        if (D1AG) {
//            var minID = 0;
//            var mintop = 0;
//            var left = 0
//            mintop = D1AG[0].top;
//            minID = D1AG[0].ObjectID;
//            left = D1AG[0].left + D1AG[0].currentWidth / 2;

//            if (D1AG) {
//                for (i = 0; i < D1AG.length; i++) {
//                    if (D1AG[i].ObjectID != minID) {
//                        D1AG[i].left = left - D1AG[i].currentWidth / 2;
//                    }
//                }
//                canvas.discardActiveGroup();
//            }
//            canvas.renderAll();
//        }
//    }
//}

//document.getElementById('BtnAlignObjTop').onclick = function (ev) {
//    if (canvas.getActiveGroup()) {
//        var D1AG = canvas.getActiveGroup().getObjects();
//        if (D1AG) {
//            var minID = 0;
//            var minLeft = 99999;
//            var top = 0
//            minLeft = D1AG[0].left;
//            minID = D1AG[0].ObjectID;
//            top = D1AG[0].top - D1AG[0].currentHeight / 2;
//            if (D1AG) {
//                for (i = 0; i < D1AG.length; i++) {
//                    if (D1AG[i].ObjectID != minID) {
//                        D1AG[i].top = top + D1AG[i].currentHeight / 2;
//                    }
//                }
//                canvas.discardActiveGroup();
//            }
//            canvas.renderAll();
//        }
//    }
//}

//document.getElementById('BtnAlignObjMiddle').onclick = function (ev) {
//    if (canvas.getActiveGroup()) {
//        var D1AG = canvas.getActiveGroup().getObjects();
//        if (D1AG) {
//            var minID = 0;
//            var minLeft = 99999;
//            var top = 0
//            minLeft = D1AG[0].left;
//            minID = D1AG[0].ObjectID;
//            top = D1AG[0].top;
//            if (D1AG) {
//                for (i = 0; i < D1AG.length; i++) {
//                    if (D1AG[i].ObjectID != minID) {
//                        D1AG[i].top = top;
//                    }
//                }
//                canvas.discardActiveGroup();
//            }
//            canvas.renderAll();
//        }
//    }
//}

//document.getElementById('BtnAlignObjBottom').onclick = function (ev) {
//    if (canvas.getActiveGroup()) {
//        var D1AG = canvas.getActiveGroup().getObjects();
//        if (D1AG) {
//            var minID = 0;
//            var minLeft = 99999;
//            var top = 0
//            minLeft = D1AG[0].left;
//            minID = D1AG[0].ObjectID;
//            top = D1AG[0].top + D1AG[0].currentHeight / 2;
//            if (D1AG) {
//                for (i = 0; i < D1AG.length; i++) {
//                    if (D1AG[i].ObjectID != minID) {
//                        D1AG[i].top = top - D1AG[i].currentHeight / 2;
//                    }
//                }
//                canvas.discardActiveGroup();
//            }
//            canvas.renderAll();
//        }
//    }
//}
function g4(event) {

}
function g3(event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        if (D1SD == false) {
            var objectsInGroup = D1AG.getObjects();
            objectsInGroup.forEach(function (OPT) {
                var clonedItem = fabric.util.object.clone(OPT);
                clonedItem.left += D1AG.left;
                clonedItem.top += D1AG.top;
            });
        }
    } else if (D1AO) {
    }
}
//document.getElementById('BtnBoldTxt').onclick = function (ev) {
//    pcL05();
//}
var cmdItalicBtn = document.getElementById('BtnItalicTxt');
if (cmdItalicBtn) {
    cmdItalicBtn.onclick = function () {
        pcL06();
    };
}
$('#BtnChngeClr').click(function (event) {
    pcL02_main();
});

$("#BtnBoldTxt").click(function (ev) {
    pcL05();
});
$("#BtnBulletedLstTxt").click(function (ev) {

});
$("#BtnJustifyTxt1").click(function (ev) {
    pcL07();
});
$("#BtnJustifyTxt2").click(function (ev) {
    pcL08();
});
$("#BtnJustifyTxt3").click(function (ev) {
    pcL09();
});
$("#BtnTxtarrangeOrder1").click(function (ev) {
    pcL26();
});
$("#BtnTxtarrangeOrder2").click(function (ev) {
    pcL27();
});
$("#BtnTxtarrangeOrder3").click(function (ev) {
    pcL28();
});
$("#BtnTxtarrangeOrder4").click(function (ev) {
    pcL25();
});
$("#BtnTxtCanvasAlignLeft").click(function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
        if (!IsBC) {
            D1AO.left = (Template.CuttingMargin * dfZ1l )+ (D1AO.maxWidth * dfZ1l) / 2 + 8.49;
        } else {
            D1AO.left = (Template.CuttingMargin * dfZ1l) + (D1AO.maxWidth * dfZ1l) / 2 + 8.49 + 5;
        }
        D1AO.setCoords();
        canvas.renderAll();
    } else {
        if (D1AO) {
            if (!IsBC) {
                D1AO.left = (Template.CuttingMargin * dfZ1l )+ (D1AO.currentWidth * dfZ1l) / 2 + 8.49;
            } else {
                D1AO.left = (Template.CuttingMargin * dfZ1l) + (D1AO.currentWidth * dfZ1l) / 2 + 8.49;
            }
            D1AO.setCoords();
            canvas.renderAll();
        }
    }
});
$("#BtnTxtCanvasAlignCenter").click(function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = canvas.getWidth() / 2;
        D1AO.setCoords();
        canvas.renderAll();
    }
});
$("#BtnTxtCanvasAlignMiddle").click(function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.top = canvas.getHeight() / 2;
        //  c2(D1AO);
        D1AO.setCoords();
        canvas.renderAll();
    }
});



//document.getElementById('BtnJustifyTxt1').onclick = function (ev) {
//    pcL07();
//}
//document.getElementById('BtnJustifyTxt2').onclick = function (ev) {
//    pcL08();
//}
//document.getElementById('BtnJustifyTxt3').onclick = function (ev) {
//    pcL09();
//}
//document.getElementById('BtnTxtarrangeOrder1').onclick = function (ev) {
//    pcL26();
//}
//document.getElementById('BtnTxtarrangeOrder2').onclick = function (ev) {
//    pcL27();
//}
//document.getElementById('BtnTxtarrangeOrder3').onclick = function (ev) {
//    pcL28();
//}
//document.getElementById('BtnTxtarrangeOrder4').onclick = function (ev) {
//    pcL25();
//}
//document.getElementById('BtnTxtCanvasAlignLeft').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
//        if (!IsBC) {
//            D1AO.left = Template.CuttingMargin + D1AO.width / 2 + 8.49;
//        } else {
//            D1AO.left = Template.CuttingMargin + D1AO.width / 2 + 8.49 + 5;
//        }
//        D1AO.setCoords();
//        canvas.renderAll();
//    } else {
//        if (D1AO) {
//            if (!IsBC) {
//                D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
//            } else {
//                D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
//            }
//            D1AO.setCoords();
//            canvas.renderAll();
//        }
//    }
//}
//document.getElementById('BtnTxtCanvasAlignCenter').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO) {
//        D1AO.left = canvas.getWidth() / 2;
//        D1AO.setCoords();
//        canvas.renderAll();
//    }
//}
//document.getElementById('BtnTxtCanvasAlignMiddle').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO) {
//        D1AO.top = canvas.getHeight() / 2;
//        //  c2(D1AO);
//        D1AO.setCoords();
//        canvas.renderAll();
//    }
//}

$("#BtnTxtCanvasAlignRight").click(function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
        if (!IsBC) {
            D1AO.left = canvas.getWidth() - (Template.CuttingMargin * dfZ1l ) - (D1AO.width* dfZ1l ) / 2 - 8.49;
        } else {
            D1AO.left = canvas.getWidth() - (Template.CuttingMargin * dfZ1l) - (D1AO.width * dfZ1l) / 2 - 8.49 - 5;
        }
        D1AO.setCoords();
        canvas.renderAll();
    } else {
        if (D1AO) {
            if (!IsBC) {
                D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
            } else {
                D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
            }
            D1AO.setCoords();
            canvas.renderAll();
        }
    }
});
//document.getElementById('BtnTxtCanvasAlignRight').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
//        if (!IsBC) {
//            D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.width / 2 - 8.49;
//        } else {
//            D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.width / 2 - 8.49 - 5;
//        }
//        D1AO.setCoords();
//        canvas.renderAll();
//    } else {
//        if (D1AO) {
//            if (!IsBC) {
//                D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
//            } else {
//                D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
//            }
//            D1AO.setCoords();
//            canvas.renderAll();
//        }
//    }
//}
var removeSelectedEl = document.getElementById('BtnDeleteTxtObj');
removeSelectedEl.onclick = function () {
    pcL03();
    $(".btnAdd").click();
};
$("#BtnImgScaleIN").click(function (event) {
    pcL14();
});

$("#BtnImgScaleOut").click(function (event) {
    pcL15();

});
$('#AddColorShape').click(function (event) {
    pcL02_main2();
});

$('#BtnImageArrangeOrdr1').click(function (event) {
    pcL26();
});
$('#BtnImageArrangeOrdr2').click(function (event) {
    pcL18();
});
$('#BtnImageArrangeOrdr3').click(function (event) {
    pcL19();
});
$('#BtnImageArrangeOrdr4').click(function (event) {
    pcL25();
});
$('#BtnImgCanvasAlignCenter').click(function (event) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = canvas.getWidth() / 2;
        D1AO.setCoords();
        canvas.renderAll();
    }
});
$('#btnImgCanvasAlignLeft').click(function (event) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = (Template.CuttingMargin * dfZ1l) + (D1AO.getWidth()) / 2 + 8.49;
        D1AO.setCoords();
        canvas.renderAll();
    }
});
$('#BtnImgCanvasAlignRight').click(function (event) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = canvas.getWidth() - (Template.CuttingMargin * dfZ1l) - (D1AO.getWidth()) / 2 - 8.49;
        D1AO.setCoords();
        canvas.renderAll();
    }
});


//document.getElementById('BtnImageArrangeOrdr1').onclick = function (ev) {
//    pcL26();
//}
//document.getElementById('BtnImageArrangeOrdr2').onclick = function (ev) {
//    pcL18();
//}

//document.getElementById('BtnImageArrangeOrdr3').onclick = function (ev) {
//    pcL19();
//}
//document.getElementById('BtnImageArrangeOrdr4').onclick = function (ev) {
//    pcL25();
//}
////document.getElementById('BtnImgRotateLeft').onclick = function (ev) {
////    pcL16();
////}

////document.getElementById('BtnImgRotateRight').onclick = function (ev) {
////    pcL17();
////}
//document.getElementById('BtnImgCanvasAlignCenter').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO) {
//        D1AO.left = canvas.getWidth() / 2;
//        D1AO.setCoords();
//        canvas.renderAll();
//    }
//}
//document.getElementById('btnImgCanvasAlignLeft').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO) {
//        D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
//        D1AO.setCoords();
//        canvas.renderAll();
//    }
//}

//document.getElementById('BtnImgCanvasAlignRight').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO) {
//        D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
//        D1AO.setCoords();
//        canvas.renderAll();
//    }
//}

$('#BtnImgCanvasAlignMiddle').click(function (event) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.top = canvas.getHeight() / 2;
        D1AO.setCoords();
        canvas.renderAll();
    }
});

//document.getElementById('BtnImgCanvasAlignMiddle').onclick = function (ev) {
//    var D1AO = canvas.getActiveObject();
//    if (D1AO) {
//        D1AO.top = canvas.getHeight() / 2;
//        D1AO.setCoords();
//        canvas.renderAll();
//    }
//}
$('#btnDeleteImage').click(function (event) {
    pcL21();
    $(".btnAdd").click();
});
//var removeSelectedEl = document.getElementById('btnDeleteImage');
//removeSelectedEl.onclick = function () {

//};
$("#clearBackground").click(function (event) {
    $(".BkColors").removeClass("SelectBkColorPanel");
    $(".bKimgBrowseCategories").removeClass("folderExpanded"); $(".bKimgBrowseCategories ul li").removeClass("folderExpanded");
    $(".BkImgPanels").addClass("disappearing");
    isBkPaCl = false; SelBkCat = "00";
    StartLoader("Clearing background");
    canvas.backgroundColor = "#ffffff";
    canvas.setBackgroundImage(null, function (IOL) { });
    canvas.renderAll(); StopLoader();
    $.each(TP, function (op, IT) {
        if (IT.ProductPageID == SP) {
            //if (Template.isCreatedManual == false) {
            //    IT.BackgroundFileName = tID + "//" + "Side" + IT.PageNo + ".pdf";
            //    canvas.setBackgroundImage("Designer/Products/" + tID + "//" + "templatImgBk" + IT.PageNo + ".jpg", function (IOL) { canvas.renderAll(); StopLoader(); });
            //} else {
            //    IT.BackgroundFileName = tID + "//" + "Side" + IT.PageNo + ".pdf";// IT.BackgroundFileName = tID + "//" + IT.PageName + IT.PageNo + ".pdf";
            //}
            //IT.BackGroundType = 1;

            IT.ColorC = 0;
            IT.ColorM = 0;
            IT.ColorY = 0;
            IT.ColorK = 0;
            IT.BackGroundType = 2;
            return;
        }
    });
});

//document.getElementById('BtnRotateTxtLft').onclick = function (ev) {
//    pcL11();
//}
//document.getElementById('BtnRotateTxtRight').onclick = function (ev) {
//    pcL12();
//}
$('#BtnGuides').click(function (event) {
    f9();
});



$('#btnAddTxt').click(function (event) {
    pcL29(13.33, false, $("#txtareaAddTxt").val());
    $("#txtareaAddTxt").val("");
});
$("#btnMenuCopy").click(function (event) {
    pcL10();
});

$(".CustomRectangleObj").click(function (event) {
    var center = canvas.getCenter();
    h1(center.left, center.top);
});
$(".CustomCircleObj").click(function (event) {
    var center = canvas.getCenter();
    h2(center.left, center.top);
});
$("#btnMenuPaste").click(function (event) {
    var OOID;
    // e0(); // l3
    if (D1CO.length != 0) {
        for (var i = 0; i < D1CO.length; i++) {
            var TG = fabric.util.object.clone(D1CO[i]);
            OOID = TG.ProductPageId;
            TG.ObjectID = --NCI;
            TG.ProductPageId = SP;
            TG.$id = (parseInt(TO[TO.length - 1].$id) + 4);
            if (OOID == SP) {
                TG.PositionX -= 15;
                TG.PositionY += 15;
            }
            if (TG.EntityKey != null) {
                delete TG.EntityKey;
            }
            TO.push(TG);
            if (TG.ObjectType == 2) {
                c0(canvas, TG);
            }
            else if (TG.ObjectType == 3) {
                d1(canvas, TG);
            }
            else if (TG.ObjectType == 6) {
                c9(canvas, TG);
            }
            else if (TG.ObjectType == 7) {
                c8(canvas, TG);
            }
            else if (TG.ObjectType == 9) {
                d4(canvas, TG);
            }
            canvas.renderAll();
        }
    }
});


$("#BtnLockTxtPosition").click(function () {

    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {
        if (thisCheck.is(':checked')) {
            D1AO.IsPositionLocked = true;
            D1AO.lockMovementX = true;
            D1AO.lockMovementY = true;
            D1AO.lockScalingX = true;
            D1AO.lockScalingY = true;
            D1AO.lockRotation = true;
        }
        else {
            D1AO.IsPositionLocked = false;
            D1AO.lockMovementX = false;
            D1AO.lockMovementY = false;
            D1AO.lockScalingX = false;
            D1AO.lockScalingY = false;
            D1AO.lockRotation = false;
        }
        // c2(D1AO);

    }
    else if (D1AO.type === 'group') {
        var objectsInGroup = D1AO.getObjects();
        objectsInGroup.forEach(function (OPT) {
            if (thisCheck.is(':checked')) {
                OPT.IsPositionLocked = true;
                OPT.lockMovementX = true;
                OPT.lockMovementY = true;
                OPT.lockScalingX = true;
                OPT.lockScalingY = true;
                OPT.lockRotation = true;
            }
            else {
                OPT.IsPositionLocked = false;
                OPT.lockMovementX = false;
                OPT.lockMovementY = false;
                OPT.lockScalingX = false;
                OPT.lockScalingY = false;
                OPT.lockRotation = false;
            }
            //  c2(OPT);
        });

    }
});

$("#BtnPrintObj").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {
        if (thisCheck.is(':checked')) {
            D1AO.IsHidden = true;
        }
        else {
            D1AO.IsHidden = false;
        }
        //   c2(D1AO);
    }
    else if (D1AO.type === 'group') {
        var objectsInGroup = D1AO.getObjects();
        objectsInGroup.forEach(function (OPT) {
            if (thisCheck.is(':checked')) {
                D1AO.IsHidden = true;
            }
            else {
                D1AO.IsHidden = false;
            }
        });
    }
});
$("#BtnPrintImage").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (thisCheck.is(':checked')) {
        D1AO.IsHidden = true;
    }
    else {
        D1AO.IsHidden = false;
    }
});
$("#TxtQSequence").keydown(function (event) {
    // Allow: backspace, delete, tab, escape, and enter
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
        // Allow: Ctrl+A
        (event.keyCode == 65 && event.ctrlKey === true) ||
        // Allow: home, end, left, right
        (event.keyCode >= 35 && event.keyCode <= 39)) {
        // let it happen, don't do anything
        return;
    }
    else {
        // Ensure that it is a number and stop the keypress
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault();
        }
    }
});
$("#BtnLockEditing").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {
      
        if (D1AO.get('IsEditable') == true) {
            D1AO.IsEditable = false;
            D1AO.IsTextEditable = false;
            // $("#BtnAllowOnlyTxtChange").prop('checked', false);
            pcL13();
            pcL36('hide', '#divPopupUpdateTxt , #divVariableContainer ');

        }
        else {
            D1AO.IsEditable = true;
        }
        // c2(D1AO);
    }
    g1(D1AO);

});
$("#chkboxAutoShrink").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {

        if (D1AO.get('AutoShrinkText')) {
            D1AO.AutoShrinkText = false;
            $("#chkboxAutoShrink").prop('checked', false);
            pcL13();
        }
        else {
            D1AO.AutoShrinkText = true;
            $("#chkboxAutoShrink").prop('checked', true);
        }
    }

    g1(D1AO);
});
$("#chkboxAutoCollapse").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {

        if (D1AO.get('autoCollapseText')) {
            D1AO.autoCollapseText = false;
            $("#chkboxAutoCollapse").prop('checked', false);
            pcL13();
        }
        else {
            D1AO.autoCollapseText = true;
            $("#chkboxAutoCollapse").prop('checked', true);
        }
    }

    g1(D1AO);
});
$("#chkboxTxtInputMask").click(function () {
    var thisCheck = $(this);
    if (thisCheck.is(':checked')) {
     //   $(".maskingControls").css("display", "block");
    } else {
       // $(".maskingControls").css("display", "none");
    }

   
});
$("#chkboxTxtInputMandatory").click(function () {
    var thisCheck = $(this);
    if (thisCheck.is(':checked')) {
        $(".txtManadatoryControl").css("display", "block");
    } else {
        $(".txtManadatoryControl").css("display", "none");
    }


});
$("#chkboxOverlayTxt").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {

        if (D1AO.get('IsOverlayObject')) {
            D1AO.IsOverlayObject = false;
            $("#chkboxOverlayTxt").prop('checked', false);
            pcL13();
        }
        else {
            D1AO.IsOverlayObject = true;
            $("#chkboxOverlayTxt").prop('checked', true);
        }
    }

    g1(D1AO);
});
$("#chkboxOverlayImg").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.get('IsOverlayObject')) {
        D1AO.IsOverlayObject = false;
        $("#chkboxOverlayImg").prop('checked', false);
        pcL13();
    }
    else {
        D1AO.IsOverlayObject = true;
        $("#chkboxOverlayImg").prop('checked', true);
    }
    //  c2(D1AO);
    g1(D1AO);
});

$("#IsQuickTxtCHK").click(function () {
    var thisCheck = $(this);
    if (thisCheck.is(':checked')) {
        $("#addText").css("height", "308px");
        $("#QtxtINRow").css("display", "block");

        $(".popUpQuickTextPanel").css("top", "248px");
    }
    else {
        $("#addText").css("height", "192px");
        $("#QtxtINRow").css("display", "none");
        $(".popUpQuickTextPanel").css("top", "131px");
    }
});
$("#BtnAllowOnlyTxtChange").click(function () {
    var thisCheck = $(this);
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'i-text') {
        if (D1AO.get('IsTextEditable')) {
            D1AO.IsTextEditable = false;

        }
        else {
            D1AO.IsTextEditable = true;
            //   D1AO.IsEditable = false; 
        }
        //  c2(D1AO);
    }
    g1(D1AO);
    //animatedcollapse.toggle('textPropertPanel');
});
$("#optionsRadioYourProfile,#optionRadioOtherProfile").change(function () {
    if ($("#optionRadioOtherProfile").is(':checked')) {
        $(".otherProfileContainer").css("display", "inline-block");
        var data = smartFormData.AllUserScopeVariables[$("#smartFormSelectUserProfile").val()];
        pcl40_InsertUserData(data);
    }
    else {
        $(".otherProfileContainer").css("display", "none");
        pcl40_InsertUserData(smartFormData.scopeVariables);
    }
});

$("#smartFormSelectUserProfile").change(function () {
    var data = smartFormData.AllUserScopeVariables[$("#smartFormSelectUserProfile").val()];
    pcl40_InsertUserData(data);
});
$("#BtnSmartFormSave").click(function () {
    smartFormClicked = true;
    pcl42();
     $(".messageSmartForm").css("display", "none");
    $("#collapseDesignerMenu").click();
    if(IsCalledFrom != 2)
       pcl45_upData();
});
$("#btnCompanyLogo").click(function () {
    d1CompanyLogoToCanvas();
});
$("#btnContactImage").click(function () {
    d1ContactLogoToCanvas();
});
$("#btnGoToLandingPage").click(function () {
    window.location.href = "/ProductOptions/0/" + item.RefItemId + "/UploadDesign";
});
$("#btnImagePlaceHolder").click(function () {
    d1PlaceHoldToCanvas();
});
$("#btnImagePlaceHolderUser").click(function () {
    d1PlaceHoldToCanvas();
});
$("#Homebtn2").click(function () {
    window.location.href = "/";
});
$("#BtnBulletedLstTxt").click(function () {
    pcl43_bullet();
});
$('#BtnReload').click(function (event) {
    undoArry = [];
    redoArry = [];
    pcl44_rLoad();
});
$(".mainLeftMenu li").click(function (event) {
    $(".multiBackCarouselContainer").css("display", "none");
});
$("#btnDeleteGroupObjs").click(function (event) {
    pcL03();
});

$("#BtnValignTxt1").click(function (ev) {
    pcL07_vAl(1);
});
$("#BtnValignTxt2").click(function (ev) {
    pcL07_vAl(2);
});
$("#BtnValignTxt3").click(function (ev) {
    pcL07_vAl(3);
});