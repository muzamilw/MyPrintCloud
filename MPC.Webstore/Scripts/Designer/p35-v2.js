$(".search").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage1");
});
$(".layout").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage2");
});
$(".QuickTxt").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage3");
});
$(".text").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    if (spPanel != "") {
        $(spPanel).click();
        spPanel = "";
    }
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage4");
    //var D1AO = canvas.getActiveObject();
    //var D1AG = canvas.getActiveGroup();
    //if (D1AG) canvas.discardActiveGroup();
    //if (D1AO) canvas.discardActiveObject();
});
$(".backgrounds").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    isBkPnlUploads = true;
    if (spBkPanel != "") {
        $(spBkPanel).click();
        spBkPanel = "";
    }
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage5");
});
$(".uploads").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    isBkPnlUploads = false;
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage6");
});
$(".layersPanel").click(function (event) {
    m0();
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    isBkPnlUploads = true;
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage8");
});
$(".layoutsPanel").click(function (event) {
    $(".stage6 #selectedTab").css("top", ""); pcL36('hide', '#DivColorPickerDraggable');
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage10");
    l2_temp();
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
document.getElementById('BtnImgRotateLeftRetail').onclick = function (ev) {
    pcL16();
}

document.getElementById('BtnImgRotateRightRetail').onclick = function (ev) {
    pcL17();
}
document.getElementById('BtnImageArrangeOrdr1Retail').onclick = function (ev) {
    pcL26();
}
document.getElementById('BtnImageArrangeOrdr4Retail').onclick = function (ev) {
    pcL25();
}
document.getElementById('BtnImageArrangeOrdr2Retail').onclick = function (ev) {
    pcL18();
}

document.getElementById('BtnImageArrangeOrdr3Retail').onclick = function (ev) {
    pcL19();
}
document.getElementById('BtnRotateTxtLftRetail').onclick = function (ev) {
    pcL11();
}
document.getElementById('BtnRotateTxtRightRetail').onclick = function (ev) {
    pcL12();
}
document.getElementById('BtnTxtarrangeOrder1Retail').onclick = function (ev) {
    pcL26();
}
document.getElementById('BtnTxtarrangeOrder2Retail').onclick = function (ev) {
    pcL27();
}
document.getElementById('BtnTxtarrangeOrder3Retail').onclick = function (ev) {
    pcL28();
}
document.getElementById('BtnTxtarrangeOrder4Retail').onclick = function (ev) {
    pcL25();
}
document.getElementById('BtnSelectFontsRetail').onchange = function (ev) {
    pcL04();
}
document.getElementById('btnDeleteTxt').onclick = function (ev) {
    pcL03();
  
}
document.getElementById('BtnBoldTxtRetail').onclick = function (ev) {
    pcL05();

}
document.getElementById('BtnItalicTxtRetail').onclick = function (ev) {
    pcL06();
}
document.getElementById('BtnJustifyTxt1Retail').onclick = function (ev) {
    pcL07();
}

document.getElementById('BtnJustifyTxt2Retail').onclick = function (ev) {
    pcL08();
}
document.getElementById('BtnJustifyTxt3Retail').onclick = function (ev) {
    pcL09();
}
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
    pcL29_pcRestore(7);
});
$(".btnBkmyBk").click(function (event) {
    fu13(2, 2, 1, 2);
    pcL29_pcRestore(7);
});
$(".btnBkTempBk").click(function (event) {
    fu13(2, 2, 1, 3);
    pcL29_pcRestore(7);
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
});
$(".btnFreeImgs").click(function (event) {
    fu13(2, 1, 1, 1);
    pcL29_pcMove(3);
    spPanel = ".btnBackFromImgs , .btnBackGlImgs";
});
$(".btnBackGlImgs").click(function (event) {
    fu13(2, 1, 1, 1);
    pcL29_pcRestore(3);
});
$(".btnIllustrations").click(function (event) {
    fu13(2, 1, 1, 2);
    pcL29_pcMove(4); spPanel = ".btnBackFromImgs , .btnBackMyImg";
});
$(".btnBackMyImg").click(function (event) {
    fu13(2, 1, 1, 2);
    pcL29_pcRestore(4);
});
$(".btnFrames").click(function (event) {
    fu13(2, 1, 1, 3);
    pcL29_pcMove(5); spPanel = ".btnBackFromImgs , .btnBackMyLogos";
});
$(".btnBackMyLogos").click(function (event) {
    fu13(2, 1, 1, 3);
    pcL29_pcRestore(5);
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
$(".btnBackTimgs").click(function (event) {
    fu13(2, 1, 3, 1);
    pcL29_pcRestore(6);
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
    pcL29_pcRestore(2);
});
$(".btnBackFromTxt").click(function (event) {
    fu13(2, 4, 1, 2);
    pcL29_pcRestore(1);
    
});
$(".btnAFrames").click(function (event) {
    fu13(2, 4, 1, 3);
    pcL29_pcMove(2);
    spPanel = ".btnBackFromImgs";
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
    fu13(2, 4, 1, 3);
    pcL29_pcMove(2);
    $('.btnAdd').click();
});
$('#editorLogo').click(function () {
    StartLoader("Saving and generating preview, Please wait...");
 //   parent.Next(); // webstore caller function
    fu12("preview", $("#txtTemplateTitle").val());
    return false;
});
$('.mainLeftMenu li').click(function () {
    if ($(this).attr("class").indexOf("backgrounds") != 1) {
        isBKpnl = true;
    } else {
        isBKpnl = false;
    }
});
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
        parent.email1 = email1;
        parent.email2 = email2;
        parent.IsRoundedCorners = IsBCRoundCorners;
        parent.SaveAttachments();
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
    $.getJSON("services/imageSvcDam/" + imgSelected + "," + imType + "," + title + "," + desc + "," + keywords,
	function (DT) {
	    StopLoader();
	   if (IsCalledFrom == 3) {
	        if (imType == 15 || imType == 1) {
	            k27();
	        }
	    }
	    if (imgLoaderSection == 1) {
	            $(".search").click();
	    } else if (imgLoaderSection == 2) {
	        $(".backgrounds").click();
	    } else {
	        $(".uploads").click();
	    }

	});

    return false;
});
$(".returnToLib").click(function (event) {
    if (imgLoaderSection == 1) {
        $(".text").click();
    } else if (imgLoaderSection == 2) {
        $(".text").click();
    } else {
        $(".text").click();
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
    var DIA0 = canvas.getActiveObject();
    if (DIA0 && DIA0.isEditing) {
        return
    } else {
        l3(e);
    }
});

$('body').keyup(function (event) {
    var DIA0 = canvas.getActiveObject();
    if (DIA0 && DIA0.isEditing) {
        return
    } else {
        l2(event);
    }

});

$("#uploadImages , #uploadImagesMB, #uploadLogos").click(function (event) {
    isBKpnl = false;
    $("#uploadBackground").click();
});
$("#uploadBackgroundMn").click(function (event) {
    isBKpnl = true;
     $("#uploadBackground").click();
});
document.getElementById('BtnAlignObjCenter').onclick = function (ev) {
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
}

document.getElementById('BtnAlignObjLeft').onclick = function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var mintop = 0;
            var left = 0
            mintop = D1AG[0].top;
            minID = D1AG[0].ObjectID;
            left = D1AG[0].left - D1AG[0].currentWidth / 2;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].left = left + D1AG[i].currentWidth / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
}

document.getElementById('BtnAlignObjRight').onclick = function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var mintop = 0;
            var left = 0
            mintop = D1AG[0].top;
            minID = D1AG[0].ObjectID;
            left = D1AG[0].left + D1AG[0].currentWidth / 2;

            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].left = left - D1AG[i].currentWidth / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
}

document.getElementById('BtnAlignObjTop').onclick = function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var minLeft = 99999;
            var top = 0
            minLeft = D1AG[0].left;
            minID = D1AG[0].ObjectID;
            top = D1AG[0].top - D1AG[0].currentHeight / 2;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].top = top + D1AG[i].currentHeight / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
}

document.getElementById('BtnAlignObjMiddle').onclick = function (ev) {
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
}

document.getElementById('BtnAlignObjBottom').onclick = function (ev) {
    if (canvas.getActiveGroup()) {
        var D1AG = canvas.getActiveGroup().getObjects();
        if (D1AG) {
            var minID = 0;
            var minLeft = 99999;
            var top = 0
            minLeft = D1AG[0].left;
            minID = D1AG[0].ObjectID;
            top = D1AG[0].top + D1AG[0].currentHeight / 2;
            if (D1AG) {
                for (i = 0; i < D1AG.length; i++) {
                    if (D1AG[i].ObjectID != minID) {
                        D1AG[i].top = top - D1AG[i].currentHeight / 2;
                    }
                }
                canvas.discardActiveGroup();
            }
            canvas.renderAll();
        }
    }
}
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
document.getElementById('BtnBoldTxt').onclick = function (ev) {
    pcL05();
}
var cmdItalicBtn = document.getElementById('BtnItalicTxt');
if (cmdItalicBtn) {
    cmdItalicBtn.onclick = function () {
        pcL06();
    };
}
$('#BtnChngeClr').click(function (event) {
    pcL02_main();
});
document.getElementById('BtnJustifyTxt1').onclick = function (ev) {
    pcL07();
}

document.getElementById('BtnJustifyTxt2').onclick = function (ev) {
    pcL08();
}
document.getElementById('BtnJustifyTxt3').onclick = function (ev) {
    pcL09();
}

document.getElementById('BtnTxtarrangeOrder1').onclick = function (ev) {
    pcL26();
}
document.getElementById('BtnTxtarrangeOrder2').onclick = function (ev) {
    pcL27();
}
document.getElementById('BtnTxtarrangeOrder3').onclick = function (ev) {
    pcL28();
}
document.getElementById('BtnTxtarrangeOrder4').onclick = function (ev) {
    pcL25();
}
document.getElementById('BtnTxtCanvasAlignLeft').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
        if (!IsBC) {
            D1AO.left = Template.CuttingMargin + D1AO.width / 2 + 8.49;
        } else {
            D1AO.left = Template.CuttingMargin + D1AO.width / 2 + 8.49 + 5;
        }
        D1AO.setCoords();
        canvas.renderAll();
    } else {
        if (D1AO) {
            if (!IsBC) {
                D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
            } else {
                D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
            }
            D1AO.setCoords();
            canvas.renderAll();
        }
    }
}
document.getElementById('BtnTxtCanvasAlignCenter').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = canvas.getWidth() / 2;
        D1AO.setCoords();
        canvas.renderAll();
    }
}
document.getElementById('BtnTxtCanvasAlignMiddle').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.top = canvas.getHeight() / 2;
        //  c2(D1AO);
        D1AO.setCoords();
        canvas.renderAll();
    }
}
document.getElementById('BtnTxtCanvasAlignRight').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
        if (!IsBC) {
            D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.width / 2 - 8.49;
        } else {
            D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.width / 2 - 8.49 - 5;
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
}
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
document.getElementById('BtnImageArrangeOrdr1').onclick = function (ev) {
    pcL26();
}
document.getElementById('BtnImageArrangeOrdr2').onclick = function (ev) {
    pcL18();
}

document.getElementById('BtnImageArrangeOrdr3').onclick = function (ev) {
    pcL19();
}
document.getElementById('BtnImageArrangeOrdr4').onclick = function (ev) {
    pcL25();
}
//document.getElementById('BtnImgRotateLeft').onclick = function (ev) {
//    pcL16();
//}

//document.getElementById('BtnImgRotateRight').onclick = function (ev) {
//    pcL17();
//}
document.getElementById('BtnImgCanvasAlignCenter').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = canvas.getWidth() / 2;
        D1AO.setCoords();
        canvas.renderAll();
    }
}
document.getElementById('btnImgCanvasAlignLeft').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
        D1AO.setCoords();
        canvas.renderAll();
    }
}

document.getElementById('BtnImgCanvasAlignRight').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
        D1AO.setCoords();
        canvas.renderAll();
    }
}

document.getElementById('BtnImgCanvasAlignMiddle').onclick = function (ev) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        D1AO.top = canvas.getHeight() / 2;
        D1AO.setCoords();
        canvas.renderAll();
    }
}
var removeSelectedEl = document.getElementById('btnDeleteImage');
removeSelectedEl.onclick = function () {
    pcL21();
    $(".btnAdd").click();
};
$("#clearBackground").click(function (event) {
    $(".bKimgBrowseCategories").removeClass("folderExpanded"); $(".bKimgBrowseCategories ul li").removeClass("folderExpanded");
    $(".BkImgPanels").addClass("disappearing");
    isBkPaCl = false; SelBkCat = "00";
    StartLoader("Loading content please wait...");
    canvas.backgroundColor = "#ffffff";
    canvas.setBackgroundImage(null, function (IOL) { });
    canvas.renderAll(); StopLoader();
    $.each(TP, function (op, IT) {
        if (IT.ProductPageID == SP) {
            if (Template.isCreatedManual == false) {
                IT.BackgroundFileName = tID + "//" + "Side" + IT.PageNo + ".pdf";
                canvas.setBackgroundImage("Designer/Products/" + tID + "//" + "templatImgBk" + IT.PageNo + ".jpg", function (IOL) { canvas.renderAll(); StopLoader(); });
            } else {
                IT.BackgroundFileName = tID + "//" + "Side" + IT.PageNo + ".pdf";// IT.BackgroundFileName = tID + "//" + IT.PageName + IT.PageNo + ".pdf";
            }
            IT.BackGroundType = 1;
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
document.getElementById('BtnGuides').onclick = function (ev) {
    f9();
}
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