function c5_co(id, value) {
    var objs = canvas.getObjects();

    $.each(objs, function (i, IT) {
        if (IT.ObjectID == id) {
            IT.set("text", value);
            return;
        }
    });

}
function d1ToCanvas(src, x, y, IW, IH) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    var n = src;
    while (n.indexOf('/') != -1)
        n = n.replace("/", "___");
    while (n.indexOf(':') != -1)
        n = n.replace(":", "@@");
    while (n.indexOf('%20') != -1)
        n = n.replace("%20", " ");
    while (n.indexOf('./') != -1)
        n = n.replace("./", "");

    if (src.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        } StartLoader("Placing image on canvas");
        svcCall4_img(n, tID, imgtype);
    } else {
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = x;
        D1NIO.PositionY = y;
        D1NIO.ObjectType = 3;

        D1NIO.MaxHeight = IH;
        D1NIO.Height = IH;
        D1NIO.MaxWidth = IW;
        D1NIO.Width = IW;

        if (IH == 0) {
            D1NIO.MaxHeight = 50;
            D1NIO.Height = 50;
        }
        else if (IW == 0) {
            D1NIO.MaxWidth = 50;
            D1NIO.Width = 50;
        }
        D1NIO.ContentString = src;
        D1NIO.DisplayOrder = TO.length + 1;
        d1(canvas, D1NIO);
        var OBS = canvas.getObjects();
        lAObj = D1NIO.ObjectID;
        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
    }

}
function d1SvgToCCC(src, IW, IH) {
  //  var canvasHeight = Math.floor(canvas.height);
 //   var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    var n = src;
    while (n.indexOf('/') != -1)
        n = n.replace("/", "___");
    while (n.indexOf(':') != -1)
        n = n.replace(":", "@@");
    while (n.indexOf('%20') != -1)
        n = n.replace("%20", " ");
    while (n.indexOf('./') != -1)
        n = n.replace("./", "");
    if (src.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        } StartLoader("Placing image on canvas");
        svcCall4_img(n, tID, imgtype);
    } else {
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = 0;
        D1NIO.PositionY = 0;
        D1NIO.ObjectType = 9;
        D1NIO.ColorC = 0;
        D1NIO.ColorM = 0;
        D1NIO.ColorY = 0;
        D1NIO.ColorK = 100;
        D1NIO.MaxHeight = 100;
        D1NIO.Height = 100;
        D1NIO.MaxWidth = 100;
        D1NIO.Width = 100;

        D1NIO.ContentString = src;
        D1NIO.DisplayOrder = TO.length + 1;
        d1Svg(canvas, D1NIO, true);
        var OBS = canvas.getObjects();
        lAObj = D1NIO.ObjectID;
        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
    }

}
function d1ToCanvasCC(src, IW, IH) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    var n = src;
    while (n.indexOf('/') != -1)
        n = n.replace("/", "___");
    while (n.indexOf(':') != -1)
        n = n.replace(":", "@@");
    while (n.indexOf('%20') != -1)
        n = n.replace("%20", " ");
    while (n.indexOf('./') != -1)
        n = n.replace("./", "");

    if (src.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        }
        StartLoader("Placing image on canvas");
        svcCall4_img(n, tID, imgtype);
  
    } else {
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = 0;
        D1NIO.PositionY = 0;
        D1NIO.ObjectType = 3;

        D1NIO.MaxHeight = IH;
        D1NIO.Height = IH;
        D1NIO.MaxWidth = IW;
        D1NIO.Width = IW;

        if (IH < 50) {
            D1NIO.MaxHeight = 50;
            D1NIO.Height = 50;
        }
        else if (IW < 50) {
            D1NIO.MaxWidth = 50;
            D1NIO.Width = 50;
        }
        D1NIO.ContentString = src;
        D1NIO.DisplayOrder = TO.length + 1;
        d1(canvas, D1NIO, true);
        var OBS = canvas.getObjects();
        lAObj = D1NIO.ObjectID;
        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
    }

}
function d1CompanyLogoToCanvas(x, y) {
    var center = canvas.getCenter();

    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    D1NIO = fabric.util.object.clone(TO[0]);
    D1NIO.ObjectId = --NCI;
    D1NIO.ObjectID = --NCI;
    D1NIO.ColorHex = "#000000";
    D1NIO.IsBold = false;
    D1NIO.IsItalic = false;
    D1NIO.ProductPageId = SP;
    D1NIO.ProductPageID = SP;
    D1NIO.MaxWidth = 100;
    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    D1NIO.PositionX = center.left;
    D1NIO.PositionY = center.top;
    D1NIO.ObjectType = 8;

    D1NIO.MaxHeight = 300;
    D1NIO.Height = 300;
    D1NIO.MaxWidth = 300;
    D1NIO.Width = 300;

    D1NIO.IsQuickText = true;
    D1NIO.ContentString = "/Content/Designer/assets-v2/Imageplaceholder_sim.png";
    D1NIO.DisplayOrder = TO.length + 1;
    D1NIO.left = center.left;
   
    k31(canvas, D1NIO);
    var OBS = canvas.getObjects();

    D1NIO.DisplayOrderPdf = OBS.length;
    canvas.renderAll();
    TO.push(D1NIO);

}
function d1ContactLogoToCanvas(x, y) {
    var center = canvas.getCenter();
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    D1NIO = fabric.util.object.clone(TO[0]);
    D1NIO.ObjectId = --NCI;
    D1NIO.ObjectID = --NCI;
    D1NIO.ColorHex = "#000000";
    D1NIO.IsBold = false;
    D1NIO.IsItalic = false;
    D1NIO.ProductPageId = SP;
    D1NIO.MaxWidth = 100;
    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    D1NIO.PositionX = center.left;
    D1NIO.PositionY = center.top;
    D1NIO.ObjectType = 12;

    D1NIO.MaxHeight = 300;
    D1NIO.Height = 300;
    D1NIO.MaxWidth = 300;
    D1NIO.Width = 300;

    D1NIO.IsQuickText = true;
    D1NIO.ContentString = "/Content/Designer/assets-v2/Imageplaceholder_sim.png";
    D1NIO.DisplayOrder = TO.length + 1;
    k31(canvas, D1NIO);
    var OBS = canvas.getObjects();

    D1NIO.DisplayOrderPdf = OBS.length;
    canvas.renderAll();
    TO.push(D1NIO);

}

function d1PlaceHoldToCanvas() {
    var center = canvas.getCenter();
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    D1NIO = fabric.util.object.clone(TO[0]);
    D1NIO.ObjectId = --NCI;
    D1NIO.ObjectID = --NCI;
    D1NIO.ColorHex = "#000000";
    D1NIO.IsBold = false;
    D1NIO.IsItalic = false;
    D1NIO.ProductPageId = SP;
    D1NIO.MaxWidth = 100;
    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    D1NIO.PositionX = center.left -150;
    D1NIO.PositionY = center.top -150;
    D1NIO.ObjectType = 3;

    D1NIO.MaxHeight = 300;
    D1NIO.Height = 300;
    D1NIO.MaxWidth = 300;
    D1NIO.Width = 300;

    D1NIO.IsQuickText = true;
    D1NIO.ContentString = "/Content/Designer/assets-v2/Imageplaceholder_sim.png";
    D1NIO.DisplayOrder = TO.length + 1;
    k31(canvas, D1NIO);
    var OBS = canvas.getObjects();

    D1NIO.DisplayOrderPdf = OBS.length;
    canvas.renderAll();
    TO.push(D1NIO);

}
function k35_load(DT) {
    // src = DT;
    // StopLoader();
    if (DT.indexOf('.svg') == -1) {
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = 0;
        D1NIO.PositionY = 0;
        D1NIO.ObjectType = 3;


        D1NIO.MaxHeight = IH;
        D1NIO.Height = IH;
        D1NIO.MaxWidth = IW;
        D1NIO.Width = IW;

        if (IH < 50) {
            D1NIO.MaxHeight = 50;
            D1NIO.Height = 50;
        }
        else if (IW < 50) {
            D1NIO.MaxWidth = 50;
            D1NIO.Width = 50;
        }
        D1NIO.ContentString = DT;
        D1NIO.DisplayOrder = TO.length + 1;
        if (D1NIO.ObjectType == 9) {
            d1Svg(canvas, D1NIO, true);
        } else {
            d1(canvas, D1NIO, true);
        }
        var OBS = canvas.getObjects();

        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
        k27();
        lAObj = D1NIO.ObjectID;
    } else 
    {
        var D1NIO = {};
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = 0;
        D1NIO.PositionY = 0;
        D1NIO.ObjectType = 9;
        D1NIO.ColorC = 0;
        D1NIO.ColorM = 0;
        D1NIO.ColorY = 0;
        D1NIO.ColorK = 100;
        D1NIO.MaxHeight = 100;
        D1NIO.Height = 100;
        D1NIO.MaxWidth = 100;
        D1NIO.Width = 100;

        D1NIO.ContentString = DT;
        D1NIO.DisplayOrder = TO.length + 1;
        d1Svg(canvas, D1NIO, true);
        var OBS = canvas.getObjects();
        lAObj = D1NIO.ObjectID;
        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
        //d1SvgToCCC(DT, 50, 50);
    }
    //  $("#ImgCarouselDiv").tabs("option", "active", 1); 
    // $("#BkImgContainer").tabs("option", "active", 1);
}
function d8(mode, dheight, title) {
    if (IsCalledFrom == 2)
    {
        $.getJSON("/designerapi/Template/updateTemplateVariables/" + tID + "/" + CustomerID,
       function (DT) {
           if(DT != true)
              alert("Error while saving field Variables.");
       });
    }
    IsDesignModified = false;
    if (mode == "preview") {
        var ra = fabric.util.getRandomInt(1, 1000);
        $('.frame  img').each(function (i) {
            var s = $(this).attr('src');
            var p = s.split("?");
            var i = p[0];
            i += '?r=' + ra;
            $(this).attr('src', i);
        });
        if (IsBC) {
            $('.thumb').each(function (i) {
                $(this).css('display', 'block');
            });

            $.each(TP, function (i, IT) {
                if (IT.IsPrintable == false) {
                    $('#thumbPage' + IT.ProductPageID).css('display', 'none');
                }
            });
        }
        $('#sliderDesigner  img').each(function (i) {
            var s = $(this).attr('src');
            var p = s.split("?");
            var i = p[0];
            i += '?r=' + ra;
            $(this).attr('src', i);
        });
        if ($('.mcSlc') != undefined) {
            var s = $('.mcSlc').css('background-image');
            if (s != undefined) {
                var p = s.split("?");
                var temp = p[0].split("http://");
                var i = 'url("http://' + temp[1];
                i += '?r=' + ra + '")';
                $('.mcSlc').css('background-image', i);

            }
        }

        if ($('#sliderDesigner') != undefined) {
            var s = $('#sliderDesigner').css('background-image');
            if (s != undefined) {
                var p = s.split("?");
                if (s.indexOf("asset") == -1) {
                    var temp = p[0].split("http://");
                    var i = 'url("http://' + temp[1];
                    i += '?r=' + ra + '")';
                    $('#sliderDesigner').css('background-image', i);
                }
            }
        }
        $.each(TP, function (i, IT) {
            d8_chk(IT.ProductPageID);

        });

        StopLoader();
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            $(".previewerTitle").html('  <span class="lightGray">Approval for :</span>  ' + title + ' ');
        } else {
            $(".previewerTitle").html('  <span class="lightGray">Preview :</span> " ' + Template.ProductName + ' "');
        }
        $('.opaqueLayer').css("display", "block");
        $('.opaqueLayer').css("background-color", "#333537");
        pcL36('show', "#PreviewerContainerDesigner");


        $("#loadingMsg").html("Saving Content");
    }
    else if (mode == "continue") {
        parent.SaveAttachments();
    }
    else if (returnText != '"true"') {
        alert("error z : " + returnText);
        StopLoader();
        $("#loadingMsg").html("Saving Content");
    }
}
function d8_chk(Pid) {
    $(".overlayLayer" + Pid).css("display", "none");
    $("#overlayLayer" + Pid).css("display", "none");
    $.each(TO, function (i, IT) {
        if (IT.IsOverlayObject == true && IT.ProductPageId == Pid) {
            //   $(".overlayLayer" + Pid).css("visibility", "visible");
            $("#overlayLayer" + Pid).css("display", "block");
            $("#overlayLayer" + Pid).css("visibility", "visible");
        }
    });
}

function e3() {
    if (D1CS < 2.9) {
        D1CS = D1CS * D1SF;
        canvas.setHeight(canvas.getHeight() * D1SF);
        canvas.setWidth(canvas.getWidth() * D1SF);
        var OBS = canvas.getObjects();
        for (var i in OBS) {
            var scaleX = OBS[i].scaleX;
            var scaleY = OBS[i].scaleY;
            var left = OBS[i].left;
            var top = OBS[i].top;
            var tempScaleX = scaleX * D1SF;
            var tempScaleY = scaleY * D1SF;
            var tempLeft = left * D1SF;
            var tempTop = top * D1SF;
            OBS[i].scaleX = tempScaleX;
            OBS[i].scaleY = tempScaleY;
            OBS[i].left = tempLeft;
            OBS[i].top = tempTop;
            OBS[i].setCoords();
            if (OBS[i].type == "text" || OBS[i].type == "i-text") {
                dfZ1l = OBS[i].scaleX;
            }
        }
    }
    if (canvas.backgroundImage) {
        canvas.backgroundImage.left = 0;
        canvas.backgroundImage.top = 0;
        canvas.backgroundImage.height = canvas.getHeight();
        canvas.backgroundImage.width = canvas.getWidth();
        canvas.backgroundImage.maxWidth = canvas.getWidth();
        canvas.backgroundImage.maxHeight = canvas.getHeight();
        canvas.backgroundImage.originX = 'left';
        canvas.backgroundImage.originY = 'top';
    }
    $("#zoomText").html(Math.floor(D1CS * 100) + "%");
    $(".page").css("height", ((Template.PDFTemplateHeight * dfZ1l) + 20) + "px");
    $(".page").css("width", ((Template.PDFTemplateWidth * dfZ1l) + 0) + "px");
    var val = $("#canvasDocument").width() - $(".page").width();
    val = val / 2;
    if (val < 0) val = 20;
    $(".page").css("left", val + "px");
}

function e5() {
    if (D1CS > 0.61) {
        D1CS = D1CS / D1SF;
        canvas.setHeight(canvas.getHeight() * (1 / D1SF));
        canvas.setWidth(canvas.getWidth() * (1 / D1SF));
        var OBS = canvas.getObjects();
        for (var i in OBS) {
            var scaleX = OBS[i].scaleX;
            var scaleY = OBS[i].scaleY;
            var left = OBS[i].left;
            var top = OBS[i].top;
            var tempScaleX = scaleX * (1 / D1SF);
            var tempScaleY = scaleY * (1 / D1SF);
            var tempLeft = left * (1 / D1SF);
            var tempTop = top * (1 / D1SF);
            OBS[i].scaleX = tempScaleX;
            OBS[i].scaleY = tempScaleY;
            OBS[i].left = tempLeft;
            OBS[i].top = tempTop;
            OBS[i].setCoords();
            if (OBS[i].type == "text" || OBS[i].type == "i-text") {
                dfZ1l = OBS[i].scaleX;
            }
        }
    }
    if (canvas.backgroundImage) {
        canvas.backgroundImage.left = 0;
        canvas.backgroundImage.top = 0;
        canvas.backgroundImage.height = canvas.getHeight();
        canvas.backgroundImage.width = canvas.getWidth();
        canvas.backgroundImage.maxWidth = canvas.getWidth();
        canvas.backgroundImage.maxHeight = canvas.getHeight();
        canvas.backgroundImage.originX = 'left';
        canvas.backgroundImage.originY = 'top';
    } $("#zoomText").html(Math.floor(D1CS * 100) + "%");
    $("#zoomText").html(Math.floor(D1CS * 100) + "%");
    $(".page").css("height", ((Template.PDFTemplateHeight * dfZ1l) + 20) + "px");
    $(".page").css("width", ((Template.PDFTemplateWidth * dfZ1l) + 0) + "px");
    var val = $("#canvasDocument").width() - $(".page").width();
    val = val / 2;
    if (val < 0) val = 20;
    $(".page").css("left", val + "px");
}
function f2_ChangeSVGColor(pathIndex) {
    selectedPathIndex = pathIndex;
    pcL02_main2();
}
function f2(c, m, y, k, ColorHex, Sname) {

    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        var objectsInGroup = D1AG.getObjects();
        $.each(objectsInGroup, function (j, Obj) {
            if (Obj.type == 'text' || Obj.type == "i-text") {
                Obj.setColor(ColorHex);
                Obj.C = c;
                Obj.M = m;
                Obj.Y = y;
                Obj.K = k;
            } else if (Obj.type == 'ellipse' || Obj.type == 'rect' || D1AO.type == 'path-group' || D1AO.type == 'path') {
                Obj.set('fill', ColorHex);
                Obj.C = c;
                Obj.M = m;
                Obj.Y = y;
                Obj.K = k;
            }

            canvas.renderAll();

        });
        if (IsCalledFrom == 2 || IsCalledFrom == 4) {
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == Obj.ObjectID) {
                    IT.IsSpotColor = true;
                    IT.SpotColorName = Sname;
                    return;
                }
            });
        }

    } else if (D1AO) {
        if (D1AO.type == 'text') {
            D1AO.setColor(ColorHex);
            D1AO.C = c;
            D1AO.M = m;
            D1AO.Y = y;
            D1AO.K = k;
            $("#txtAreaUpdateTxt").css("color", ColorHex);
            var hexStr = D1AO.fill;
            var hex = parseInt(hexStr.substring(1), 16);
            pcL22_Sub(D1AO); $(".BtnChngeClr").css("background-color", ColorHex);
        } else if (D1AO.type == 'i-text') {
            setActiveStyle("color", ColorHex, c, m, y, k);
            pcL22_Sub(D1AO); $(".BtnChngeClr").css("background-color", ColorHex);
        } else if (D1AO.type == 'ellipse' || D1AO.type == 'rect' ) {
            D1AO.set('fill', ColorHex);
            D1AO.C = c;
            D1AO.M = m;
            D1AO.Y = y;
            D1AO.K = k;
            pcL22_Sub(D1AO); $(".BtnChngeClr").css("background-color", ColorHex);
        } else if (D1AO.type == 'path-group' || D1AO.type == 'path') {
            var orignalClr = "";
            $.each(D1AO.customStyles, function (i, IT) {
                if (i == selectedPathIndex) {
                    orignalClr = IT.OriginalColor;
                }

            });
            $.each(D1AO.customStyles, function (i, IT) {
                if (IT.OriginalColor == orignalClr)
                {
                    IT.ModifiedColor = ColorHex;
                    $(".BtnChngeSvgClr" + i).css("background-color", ColorHex);
                }
               
            });
            $.each(D1AO.customStyles, function (j, IT) {
                var clr = IT.OriginalColor;
                if (IT.ModifiedColor != "")
                    clr = IT.ModifiedColor;

                if (D1AO.isSameColor && D1AO.isSameColor() || !D1AO.paths) {
                    D1AO.setFill(clr);
                }
                else if (D1AO.paths) {
                    for (var i = 0; i < D1AO.paths.length; i++) {
                        if (i == j) {
                            D1AO.paths[i].setFill(clr);
                        }
                    }
                }
            });
            //alert();
        }

        canvas.renderAll();
        if (IsCalledFrom == 2 || IsCalledFrom == 4) {
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == D1AO.ObjectID) {
                    IT.IsSpotColor = true;
                    IT.SpotColorName = Sname;
                    return;
                }
            });
        }
        

    } else {
        canvas.backgroundColor = ColorHex;
        canvas.renderAll();

        $.each(TP, function (i, IT) {
            if (IT.ProductPageID == SP) {
                IT.ColorC = c;
                IT.ColorM = m;
                IT.ColorY = y;
                IT.ColorK = k;
                IT.BackGroundType = 2;
                return;
            }
        });
    }
    pcL36('hide', '#DivColorPickerDraggable');
}
function f4() {
    var c = $("#DivColorC").slider("value");
    var m = $("#DivColorM").slider("value");
    var y = $("#DivColorY").slider("value");
    var k = $("#DivColorK").slider("value");
    var hex = getColorHex(c, m, y, k);
    f5(c, m, y, k);
}
function f5(c, m, y, k) {
    var Color = getColorHex(c, m, y, k);
    var html = "<label for='ColorPalle' id ='LblCollarPalet'> Click on button to apply </label><div class ='ColorPalletr btnClrPallet' style='background-color:" + Color + "' onclick='f6(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
    $('#LblDivColorC').html(c + "%");
    $('#LblDivColorM').html(m + "%");
    $('#LblDivColorY').html(y + "%");
    $('#LblDivColorK').html(k + "%");
    $('#ColorPickerPalletContainer').html(html);
    $("#LblCollarPalet").click(function () {

        $(".btnClrPallet").click();
    });
}
function f6(c, m, y, k, Color) {
    var Sname = "";
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        Sname = window.prompt("Enter Spot Color Name Here! (Once a color is created, you cannot change its name or color)", "Spot Color 1");
        if (Sname == null || Sname == "") {
            return false;
        } else {
            $.getJSON("/designerapi/TemplateColorStyles/SaveCorpColor/" + Sname + "/" + c + "/" + m + "/" + y + "/" + k + "/" + CustomerID,
				function (DT) {
				    var PID = DT;
				    var html = "<div id ='pallet" + PID + "' class ='ColorPalletCorp' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;" + ",&quot;" + Sname + "&quot;);'" + "><button  id ='btnClr" + PID + "' class='btnDeactiveColor' title='Deactivate this color' onclick='j7(" + PID + ",&quot;DeActive&quot;);'></button></div><div  id ='textColor" + PID + "' class='ColorPalletCorpName'>" + Sname + "</div>";
				    $('#tabsActiveColors').append(html);

				});
        }
    }
    f2(c, m, y, k, Color, Sname);
}
function f6_1() {
    pcL36('toggle', '#DivAdvanceColorPanel');
    return false;
}
function f9() {
    if (ISG1) {
        ISG1 = false;
        //  $("#BtnGuidesBC").find('span').text("Show Bleed and Trim lines");
    } else {
        ISG1 = true;
        //  $("#BtnGuidesBC").find('span').text(" Hide Bleed and Trim lines");
    }
   // $("#loaderTitleMsg").text('Refreshing Canvas');
    StartLoader('Refreshing Canvas');
    d5(SP,true);
}
function fu11() {
    QuickTxtName = $("#txtQName").val();
    QuickTxtTitle = $("#txtQTitle").val();
    QuickTxtCompanyName = $("#txtQCompanyName").val();
    QuickTxtCompanyMsg = $("#txtQCompanyMessage").val();
    QuickTxtAddress1 = $("#txtQAddressLine1").val();
    QuickTxtTel = $("#txtQPhone").val();
    QuickTxtFax = $("#txtQFax").val();
    QuickTxtEmail = $("#txtQEmail").val();
    QuickTxtWebsite = $("#txtQWebsite").val();
    var QtxtMobile = $("#txtQMobile").val();
    var QtxtTwitter = $("#txtQTwitter").val();
    var QtxtFacebook = $("#txtQFacebook").val();
    var QtxtLinkedin = $("#txtQLinkedIn").val();
    var QtxtOtherID = $("#txtQOtherID").val();
    if ($("#txtQCompanyName").length > 0) {
        if (QuickTxtCompanyName != undefined || QuickTxtCompanyName != null) {
            QTD.Company = (QuickTxtCompanyName == "" ? "Your Company Name" : QuickTxtCompanyName);
        } else {
            QTD.Company = "";
        }
    }
    if ($("#txtQCompanyMessage").length > 0) {
        if (QuickTxtCompanyMsg != undefined || QuickTxtCompanyMsg != null) {
            QTD.CompanyMessage = QuickTxtCompanyMsg == "" ? "Your Company Message" : QuickTxtCompanyMsg;
        } else {
            QTD.CompanyMessage = "";
        }
    }
    if ($("#txtQName").length > 0) {
        if (QuickTxtName != undefined || QuickTxtName != null) {
            QTD.Name = QuickTxtName == "" ? "Your Name" : QuickTxtName;
        } else {
            QTD.Name = "";
        }
    }
    if ($("#txtQTitle").length > 0) {
        if (QuickTxtTitle != undefined || QuickTxtTitle != null) {
            QTD.Title = QuickTxtTitle == "" ? "Your Title" : QuickTxtTitle;
        } else {
            QTD.Title = "";
        }
    }
    if ($("#txtQAddressLine1").length > 0) {
        if (QuickTxtAddress1 != undefined || QuickTxtAddress1 != null) {
            QTD.Address1 = QuickTxtAddress1 == "" ? "Address Line 1" : QuickTxtAddress1;
        } else {
            QTD.Address1 = "";
        }
    }
    if ($("#txtQPhone").length > 0) {
        if (QuickTxtTel != undefined || QuickTxtTel != null) {
            QTD.Telephone = QuickTxtTel == "" ? "Telephone / Other" : QuickTxtTel;
        } else {
            QTD.Telephone = "";
        }
    }
    if ($("#txtQFax").length > 0) {
        if (QuickTxtFax != undefined || QuickTxtFax != null) {
            QTD.Fax = QuickTxtFax == "" ? "Fax / Other" : QuickTxtFax;
        } else {
            QTD.Fax = "";
        }
    }
    if ($("#txtQEmail").length > 0) {
        if (QuickTxtEmail != undefined || QuickTxtEmail != null) {
            QTD.Email = QuickTxtEmail == "" ? "Email address / Other" : QuickTxtEmail;
        } else {
            QTD.Email = "";
        }
    }
    if ($("#txtQWebsite").length > 0) {
        if (QuickTxtWebsite != undefined || QuickTxtWebsite != null) {
            QTD.Website = QuickTxtWebsite == "" ? "Website address" : QuickTxtWebsite;
        } else {
            QTD.Website = "";
        }
    }

    if ($("#txtQMobile").length > 0) {

        if (QtxtMobile != undefined || QtxtMobile != null) {
            QTD.MobileNumber = QtxtMobile == "" ? "Mobile number" : QtxtMobile;
        } else {
            QTD.MobileNumber = "";
        }
    }
    if ($("#txtQTwitter").length > 0) {

        if (QtxtTwitter != undefined || QtxtTwitter != null) {
            QTD.TwitterID = QtxtTwitter == "" ? "Twitter ID" : QtxtTwitter;
        } else {
            QTD.TwitterID = "";
        }
    }
    if ($("#txtQFacebook").length > 0) {

        if (QtxtFacebook != undefined || QtxtFacebook != null) {
            QTD.FacebookID = QtxtFacebook == "" ? "Facebook ID" : QtxtFacebook;
        } else {
            QTD.FacebookID = "";
        }
    }

    if ($("#txtQLinkedIn").length > 0) {

        if (QtxtLinkedin != undefined || QtxtLinkedin != null) {
            QTD.LinkedInID = QtxtLinkedin == "" ? "LinkedIn ID" : QtxtLinkedin;
        } else {
            QTD.LinkedInID = "";
        }
    }
    if ($("#txtQOtherID").length > 0) {

        if (QtxtOtherID != undefined || QtxtOtherID != null) {
            QTD.OtherId = QtxtOtherID == "" ? "Other ID" : QtxtOtherID;
        } else {
            QTD.OtherId = "";
        }
    }
    $.each(TO, function (i, IT) {
        if (IT.IsQuickText == true && IT.ObjectType != 3 && IT.ObjectType != 8 && IT.ObjectType != 12) {
            var id = IT.Name.split(' ').join('');
            id = id.replace(/\W/g, '');
            if ($("#txtQ" + id).length) {
                // if ($("#txtQ" + id).val() != "") {
                var val = $("#txtQ" + id).val();
                if (val == "Your Company Name" || val == "Your Company Message" || val == "Your Name" || val == "Your Title" || val == "Address Line 1" || val == "Telephone / Other" || val == "Fax / Other" || val == "Email address / Other" || val == "Website address") {
                    val = "";
                }
                IT.ContentString = val;
                c5_co(IT.ObjectID, val);
                // }
            }
        }
    });
    d5(SP);

    if (CustomerID != 0 && ContactID != 0) {
        // StartLoader();   dont need to start or stop loader because it is just a save operation
        var jsonObjects = JSON.stringify(QTD, null, 2);
        var to;
        to = "/designerapi/Template/SaveQuickText/";
        var options = {
            type: "POST",
            url: to,
            data: jsonObjects,
            contentType: "application/json",
            async: true,
            success: function (response) {
            },
            error: function (msg) { alert("Error occured "); console.log(msg); }
        };
        var returnText = $.ajax(options).responseText;
        // StopLoader();
    }
}
function fu12(mode, title) {
    c2_v2();
    c2_v2();
    var dheight = $(window).height();
    dheight = dheight - 50;
    var TPOs = [];
    TPOs = TP;
    var it2 = 20000;
    var it3 = 25000;
    var it4 = 5000;
    var it5 = 10000;
    $.each(TO, function (i, item) {
        item.$id = it4;
        it4++;
        if (item.EntityKey) {
            item.EntityKey.$id = it5;
            it5++;
        }
        if(IsCalledFrom == 2)
        {
            item.originalTextStyles = item.textStyles;
            item.originalContentString = item.ContentString;
        }
    });
    $.each(TPOs, function (i, IT) {
        IT.$id = it2;
        it2++;
        if (IT.EntityKey) {
            IT.EntityKey.$id = it3;
            it3++;
        }
        if (IT.BackgroundFileName != null) {
            if (IT.BackgroundFileName.indexOf(productionFolderPath) != -1) {
                var p = IT.BackgroundFileName.split(productionFolderPath);
                IT.BackgroundFileName = p[p.length - 1];
            }
        }
    });
    // saving variables 
    if (IsCalledFrom == 2) {
        save_rs();
    }
    //saving the objects first
    var obSt = {
        printCropMarks: printCropMarks,
        printWaterMarks: printWaterMarks,
        objects: TO,
        orderCode: orderCode,
        CustomerName: CustomerName,
        objPages: TPOs,
        organisationId: organisationId,
        isRoundCornerrs: IsBCRoundCorners,
        isMultiPageProduct: isMultiPageProduct
    }
    var jsonObjects = JSON.stringify(obSt, null, 2);
    var to;
    if (mode == "save")
        to = "services/TemplateSvc/update/";
    else if (mode == "preview")
        to = previewUrl;
    else if (mode == "continue")
        to = "services/TemplateSvc/savecontinue/";
    var options = {
        type: "POST",
        url: to,
        data: jsonObjects,
        contentType: "application/json",

        async: true,
        complete: function (httpresp, returnstatus) {

            if (returnstatus == "success") {
                if (httpresp.responseText == '"True"') {
                    d8(mode, dheight, title);
                }
                else {
                    StopLoader();
                    alert(httpresp.responseText);
                }
            }
        }
    };
    var returnText = $.ajax(options).responseText;
}
function fu13(op, type, r, c) {
    if (type == 1) {
        if (isImgPaCl) {
            $(".ImgsBrowserCategories").removeClass("folderExpanded"); $(".ImgsBrowserCategories ul li").removeClass("folderExpanded");
            $(".ImgPanels").addClass("disappearing");
            isImgPaCl = false;
        }
        if (selCat == ("" + r + "" + c)) {
            selCat = "00";
        } else {
            $(".ImgsBrowserCategories").addClass("folderExpanded");
            $(".ImgLr" + r + "c" + c).addClass("folderExpanded");
            selCat = ("" + r + "" + c);
            $(".Imr" + r + "c" + c).removeClass("disappearing");
            isImgPaCl = true;
        }
    } else if (type == 2) {
        if (isBkPaCl) {
            $(".bKimgBrowseCategories").removeClass("folderExpanded"); $(".bKimgBrowseCategories ul li").removeClass("folderExpanded");
            $(".BkImgPanels").addClass("disappearing");
            isBkPaCl = false;
        }
        if (SelBkCat == ("" + r + "" + c)) {
            SelBkCat = "00";
        } else {
            $(".bKimgBrowseCategories").addClass("folderExpanded");
            $(".bkr" + r + "c" + c).addClass("folderExpanded");
            SelBkCat = ("" + r + "" + c);
            $(".bkImr" + r + "c" + c).removeClass("disappearing");
            isBkPaCl = true;
        }
    } else if (type == 3) {
        if (isUpPaCl) {
            $(".browseCatUploads").removeClass("folderExpanded");
            $(".browseCatUploads ul li").removeClass("folderExpanded");
            $(".UpImgPanels").addClass("disappearing");
            isUpPaCl = false;
        }
        if (SelUpCat == ("" + r + "" + c)) {
            SelUpCat = "00";
        } else {
            $(".browseCatUploads").addClass("folderExpanded");
            $(".Upr" + r + "c" + c).addClass("folderExpanded");
            SelUpCat = ("" + r + "" + c);
            $(".UpImr" + r + "c" + c).removeClass("disappearing");
            isUpPaCl = true;
        }
    } else if (type == 4) {

        //if (r == 1 && c == 2) {
        //    var cls = ".AddBLr1c2"; console.log($(cls).attr("class"));
        //    if ($(cls).attr("class").indexOf("folderExpanded") == -1) {
        //        pcL29_pcMove(1);
        //    }
        //}
        if (isAddPaCl) {
            $(".AddBrowserCategories").removeClass("folderExpanded");
            $(".AddBrowserCategories .UlAddMain li").removeClass("folderExpanded");
            $(".AddPanels").addClass("disappearing");
            isAddPaCl = false;
        }
        if (SelAddCat == ("" + r + "" + c)) {
            SelAddCat = "00";
        } else {
            $(".AddBrowserCategories").addClass("folderExpanded");
            $(".AddBLr" + r + "c" + c).addClass("folderExpanded");
            SelAddCat = ("" + r + "" + c);
            $(".Addr" + r + "c" + c).removeClass("disappearing");
            isAddPaCl = true;
        }
    } else if (type == 5) {
        var box = $('#idShapesPanel');
        if (box.hasClass('hidden')) {
            box.removeClass('hidden');
       } else {
            box.addClass('hidden');
          
        }


    }
}
function g0(left, top, IsQT, QTName, QTSequence, QTWatermark, txt, fontSize, isBold) {
    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "New Text";
    D1NTO.ContentString = txt;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = isBold;
    D1NTO.IsItalic = false;
    D1NTO.LineSpacing = 1.4;
    D1NTO.CharSpacing = 0;
    D1NTO.ProductPageId = SP;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    D1NTO.MaxWidth = 170;
    D1NTO.MaxHeight = 80;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }

    D1NTO.FontSize = fontSize;

    if (IsQT == true) {
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = QTSequence;
        D1NTO.Name = QTName;
        D1NTO.watermarkText = $('#txtQWaterMark').val();
    } else {
        D1NTO.IsQuickText = false;
    }
    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    //D1NTO.IsSpotColor = false;
    canvas.renderAll();
    TO.push(D1NTO);
}
function g2(e) {
    pcL36('hide', '#DivPersonalizeTemplate , #DivAdvanceColorPanel , #quickText');
    pcL13();
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    var lastPanelLocal = D1LP;
    if (D1AO && D1LP != D1AO.type) {
        pcL13();
        pcL36('hide', '#textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText, #addImage , #addText , #DivAlignObjs');

    }
    D1LP = D1AO.type;
    if (D1AG) {
        pcL13();
        pcL36('hide', '#textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText, #addImage , #addText , #DivToolTip');
        pcL36('show', '#DivAlignObjs');
        $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage0");
        if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
            $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
            //  $("#FrontBackOptionPanalSection").addClass("hideRightPropertyPanel");
            $("#FrontBackOptionPanal").css("display", "none");
        }
        $(".collapseDesignerMenu").css("display", "none");
    }

    if (D1AO && D1AO.type === 'text' || D1AO && D1AO.type === 'i-text') {
        // $("#BtnSearchTxt").removeAttr("disabled");
        $("#BtnSelectFontsRetail").fontSelector('option', 'font', D1AO.get('fontFamily'));
        $("#BtnFontSize").val(k13(D1AO.get('fontSize')));
        $("#BtnFontSizeRetail").val(k13(D1AO.get('fontSize')));
        // $("#txtLineHeight").val(D1AO.get('lineHeight'));
        // $("#inputcharSpacing").val(k13(D1AO.get('charSpacing')));
        //  $("#txtAreaUpdateTxtPropPanel").val(D1AO.text);
        //if (D1AO.IsPositionLocked) {
        //    $("#BtnLockTxtPosition").prop('checked', true);
        //}
        //else {
        //    $("#BtnLockTxtPosition").prop('checked', false);
        //}
        //if (D1AO.IsHidden) {
        //    $("#BtnPrintObj").prop('checked', true);
        //}
        //else {
        //    $("#BtnPrintObj").prop('checked', false);
        //} //alert(IsEmbedded + " " +  IT.IsTextEditable);
        //if (D1AO.IsTextEditable) {
        //    $("#BtnAllowOnlyTxtChange").prop('checked', true);
        //}
        //else {
        //    $("#BtnAllowOnlyTxtChange").prop('checked', false);
        //}
        //if (D1AO.AutoShrinkText) {
        //    $("#chkboxAutoShrink").prop('checked', true);
        //}
        //else {
        //    $("#chkboxAutoShrink").prop('checked', false);
        //}

        //if (D1AO.IsOverlayObject) {
        //    $("#chkboxOverlayTxt").prop('checked', true);
        //}
        //else {
        //    $("#chkboxOverlayTxt").prop('checked', false);
        //}
        if (D1AO.IsEditable) {
            $("#BtnLockEditing").prop('checked', false);
            $("#BtnJustifyTxt1").removeAttr("disabled");
            $("#BtnJustifyTxt2").removeAttr("disabled");
            $("#BtnJustifyTxt3").removeAttr("disabled");
            $("#BtnTxtarrangeOrder1").removeAttr("disabled");
            $("#BtnTxtarrangeOrder2").removeAttr("disabled");
            $("#BtnTxtarrangeOrder3").removeAttr("disabled");
            $("#BtnTxtarrangeOrder4").removeAttr("disabled");
            //$("#EditTXtArea").removeAttr("disabled");
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
                $("#BtnLockEditing").attr("disabled", "disabled");
            }
            if (IsEmbedded && D1AO.IsPositionLocked) {
                $("#BtnLockTxtPosition").attr("disabled", "disabled");
            }
            $("#BtnTxtCanvasAlignLeft").removeAttr("disabled");
            $("#BtnTxtCanvasAlignCenter").removeAttr("disabled");
            $("#BtnTxtCanvasAlignRight").removeAttr("disabled");
            $("#BtnTxtCanvasAlignTop").removeAttr("disabled");
            $("#BtnTxtCanvasAlignMiddle").removeAttr("disabled");
            $("#BtnTxtCanvasAlignBottom").removeAttr("disabled");
            //  $("#inputcharSpacing").spinner("option", "disabled", false);
        }
        else {
            //  $("#inputcharSpacing").spinner("option", "disabled", true);
            $("#BtnLockEditing").prop('checked', true);
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
            if (IsCalledFrom != 2) {
                $("#BtnLockTxtPosition").attr("disabled", "disabled");
            }
            $("#BtnPrintObj").attr("disabled", "disabled");

            $("#BtnTxtCanvasAlignLeft").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignCenter").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignRight").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignTop").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignMiddle").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignBottom").attr("disabled", "disabled");

        }


        $("#BtnChngeClr").css("visibility", "hidden");
        pcL36('hide', '#divBCMenuPresets , #DivCropToolContainer');
        var l = $("#canvas").offset().left - $("#divTxtPropPanelRetail").width() + D1AO.left + D1AO.width;
        var h = $("#canvas").offset().top - $("#divTxtPropPanelRetail").height() + D1AO.top - D1AO.height / 2 - 20;
        l = l - 430 + 128;
        h -= 30;
        $("#divTxtPropPanelRetail").css("display", "block");
        $(".spanRectColour").css("background-color", D1AO.fill);
        $(".spanTxtcolour").css("background-color", D1AO.fill);
        if (D1AO.fontWeight == "bold") {
            $(".textToolbarBold").addClass("propOn");
        } else {
            $(".textToolbarBold").removeClass("propOn");
        }
        if (D1AO.fontStyle == "italic")
            $(".textToolbarItalic").addClass("propOn");
        else
            $(".textToolbarItalic").removeClass("propOn");

        $(".textToolbarLeft").removeClass("propOn");
        $(".textToolbarCenter").removeClass("propOn");
        $(".textToolbarRight").removeClass("propOn");
        if (D1AO.textAlign == "left")
            $(".textToolbarLeft").addClass("propOn");
        else if (D1AO.textAlign == "center")
            $(".textToolbarCenter").addClass("propOn");
        else if (D1AO.textAlign == "right")
            $(".textToolbarRight").addClass("propOn");

        $("#divTxtPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
        h -= $("#divTxtPropPanelRetail").height();
        h -= 228;
        l += 179;
        $("#DivColorPickerDraggable").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
        $(".toolbarText").css("display", "none");
        $(".toolbarText").css("opacity", "0");

        pcL36('show', '#divTxtPropPanelRetail');

    }
    else if (D1AO && D1AO.type === 'image') {

        pcL36('hide', '#divBCMenuPresets , #DivCropToolContainer ,#DivColorPickerDraggable');
        var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
        var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
        l = l - 430 + 128;
        h -= 30;
        pcL36('show', '#divImgPropPanelRetail');
        $("#divImgPropPanelRetail").css("display", "block");
        $("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
        $(".toolbarImage").css("display", "none");
        $(".toolbarImage").css("opacity", "0");
        $(".elementColorImg").css("display", "none"); $(".elementCrop").css("display", "inline-block");
        h -= $("#divTxtPropPanelRetail").height();
        h -= 228;
        l += 179 - 37;
        $("#DivColorPickerDraggable").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
    } else if (D1AO && D1AO.type === 'rect') {
        pcL36('hide', '#divBCMenuPresets , #DivCropToolContainer ,#DivColorPickerDraggable');
        var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
        var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
        l = l - 430 + 128;
        h -= 30;
        pcL36('show', '#divImgPropPanelRetail');
        $("#divImgPropPanelRetail").css("display", "block");
        $("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
        $(".toolbarImage").css("display", "none");
        $(".toolbarImage").css("opacity", "0"); $(".spanRectColour").css("background-color", D1AO.fill);
        $(".elementCrop").css("display", "none"); $(".elementColorImg").css("display", "inline-block");

        h -= $("#divTxtPropPanelRetail").height();
        h -= 228;
        l += 179 - 37;
        $("#DivColorPickerDraggable").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
    } else if (D1AO && D1AO.type === 'ellipse') {
        pcL36('hide', '#divBCMenuPresets , #DivCropToolContainer ,#DivColorPickerDraggable');
        var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
        var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
        l = l - 430 + 128;
        h -= 30;
        pcL36('show', '#divImgPropPanelRetail');
        $("#divImgPropPanelRetail").css("display", "block");
        $("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
        $(".toolbarImage").css("display", "none");
        $(".toolbarImage").css("opacity", "0");
        $(".elementCrop").css("display", "none"); $(".spanRectColour").css("background-color", D1AO.fill);
        $(".elementColorImg").css("display", "inline-block");
        h -= $("#divTxtPropPanelRetail").height();
        h -= 228;
        l += 179 - 37;
        $("#DivColorPickerDraggable").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");

    } else if (D1AO && (D1AO.type === 'path-group' || D1AO.type === 'path')) {
        pcL36('hide', '#divBCMenuPresets , #DivCropToolContainer ,#DivColorPickerDraggable');
        var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
        var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
        l = l - 430 + 128;
        h -= 30;
        pcL36('show', '#divImgPropPanelRetail');
        $("#divImgPropPanelRetail").css("display", "block");
        $("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
        $(".toolbarImage").css("display", "none");
        $(".toolbarImage").css("opacity", "0");
        $(".elementCrop").css("display", "none"); $(".spanRectColour").css("background-color", D1AO.fill);
        $(".elementColorImg").css("display", "inline-block");
        h -= $("#divTxtPropPanelRetail").height();
        h -= 228;
        l += 179 - 37;
        $("#DivColorPickerDraggable").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
    }

}
function g2_1(e) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    var lastPanelLocal = D1LP;
    if (D1AO && D1LP != D1AO.type) {
        pcL13();
        pcL36('hide', '#textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText, #addImage , #addText , #DivAlignObjs');

    }
    D1LP = D1AO.type;
    if (D1AG) {
        pcL13();
        pcL36('hide', '#textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText, #addImage , #addText , #DivToolTip');
        pcL36('show', '#DivAlignObjs');
    }
    k4();

    if (D1AO && D1AO.type === 'text' || D1AO && D1AO.type === 'i-text') {
        if (D1AO.textCase == 3) {//val == '1'
            $(".CaseModeSlider").slider("option", "value", '100');

        } else if (D1AO.textCase == 2) {//val == '34'
            $(".CaseModeSlider").slider("option", "value", '34');
        } else if (D1AO.textCase == 1) {//val == '67'
            $(".CaseModeSlider").slider("option", "value", '67');
        } else {
            $(".CaseModeSlider").slider("option", "value", '1');
            //val=100
        }
        var clr =  D1AO.fill + " !important";
        $(".BtnChngeClr").css("background-color", clr);
        $("#textPropertyPanel").css("display", "block");
        $("#objPropertyPanel").css("display", "none");
        $("#BtnSelectFonts").fontSelector('option', 'font', D1AO.get('fontFamily'));
        $("#BtnFontSize").val(k13(D1AO.get('fontSize')));
        $("#BtnFontSizeRetail").val(k13(D1AO.get('fontSize')));
        $("#txtLineHeight").val(D1AO.get('lineHeight'));
        $("#inputcharSpacing").val(k13(D1AO.get('charSpacing')));
        var text = D1AO.text;
        if (text.length > 40) text = text.substring(0, 40) + "...";
        $(".lblEditTxtTxt").text(text);
        $(".rotateSliderTxt").slider("option", "value", D1AO.getAngle());
        if (D1AO.IsTextEditable) {
            $("#BtnAllowOnlyTxtChange").prop('checked', true);
        } else {
            $("#BtnAllowOnlyTxtChange").prop('checked', false);
        }
        if (D1AO.IsHidden) {
            $("#BtnPrintObj").prop('checked', true);
        } else {
            $("#BtnPrintObj").prop('checked', false);
        }
        if (D1AO.IsOverlayObject) {
            $("#chkboxOverlayTxt").prop('checked', true);
        } else {
            $("#chkboxOverlayTxt").prop('checked', false);
        }
        if (D1AO.IsPositionLocked) {
            $("#BtnLockTxtPosition").prop('checked', true);
        } else {
            $("#BtnLockTxtPosition").prop('checked', false);
        }
        if (D1AO.AutoShrinkText) {
            $("#chkboxAutoShrink").prop('checked', true);
        } else {
            $("#chkboxAutoShrink").prop('checked', false);
        }
        if (D1AO.autoCollapseText) {
            $("#chkboxAutoCollapse").prop('checked', true);
        } else {
            $("#chkboxAutoCollapse").prop('checked', false);
        }
        if (D1AO.IsEditable) {
            $("#BtnLockEditing").prop('checked', false);
        } else {
            $("#BtnLockEditing").prop('checked', true);
        }
        if (D1AO.IsTextEditable != true) {
            $("#BtnJustifyTxt1").removeAttr("disabled");
            $("#BtnJustifyTxt2").removeAttr("disabled");
            $("#BtnJustifyTxt3").removeAttr("disabled");
            $("#BtnTxtarrangeOrder1").removeAttr("disabled");
            $("#BtnTxtarrangeOrder2").removeAttr("disabled");
            $("#BtnTxtarrangeOrder3").removeAttr("disabled");
            $("#BtnTxtarrangeOrder4").removeAttr("disabled");
            $("#BtnSearchTxt").removeAttr("disabled");
            $("#BtnSelectFonts").removeAttr("disabled");
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
                $("#BtnLockEditing").attr("disabled", "disabled");
            }
            if (IsEmbedded && D1AO.IsPositionLocked && IsCalledFrom != 2) {
                $("#BtnLockTxtPosition").attr("disabled", "disabled");
            }
            $(".fontSelector").removeAttr("disabled");
            $("#BtnLockEditing").removeAttr("disabled");
            $("#BtnTxtCanvasAlignLeft").removeAttr("disabled");
            $("#BtnTxtCanvasAlignCenter").removeAttr("disabled");
            $("#BtnTxtCanvasAlignRight").removeAttr("disabled");
            $("#BtnTxtCanvasAlignTop").removeAttr("disabled");
            $("#BtnTxtCanvasAlignMiddle").removeAttr("disabled");
            $("#BtnTxtCanvasAlignBottom").removeAttr("disabled");
            $("#inputcharSpacing").spinner("option", "disabled", false);
            $("#BtnFontSize").spinner("option", "disabled", false);
            $("#txtLineHeight").spinner("option", "disabled", false);
            $("#inputObjectWidthTxt").spinner("option", "disabled", false);
            $("#inputObjectHeightTxt").spinner("option", "disabled", false);
            $("#inputPositionXTxt").spinner("option", "disabled", false);
            $("#inputPositionYTxt").spinner("option", "disabled", false);
            if (D1AO.IsPositionLocked == true && (IsCalledFrom == 3 || IsCalledFrom == 4)) {
                $(".positioningControls").css("display", "none");
            } else {
                $(".positioningControls").css("display", "block");
            }
         
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
            $("#BtnSearchTxt").attr("disabled", "disabled");
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
            $(".positioningControls").css("display", "block");
          
        }
       // g1(D1AO);
    }
    else if (D1AO && D1AO.type === 'image') {
        g2_22(1);
    } else if (D1AO && D1AO.type === 'rect') {
        g2_22(2); var clr = D1AO.fill + " !important";
        $(".BtnChngeClr").css("background-color", clr);
    } else if (D1AO && D1AO.type === 'ellipse') {
        g2_22(2); var clr = D1AO.fill + " !important";
        $(".BtnChngeClr").css("background-color", clr);

    } else if (D1AO && (D1AO.type === 'path-group' || D1AO.type === 'path')) {
        g2_22(3);
       // var clr = D1AO.fill + " !important";
       // $(".BtnChngeClr").css("background-color", clr);
    }

    
    var tp = $("#selectedTab").css("top");
    $("#objectPanel").removeClass("stage0").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage7").removeClass("stage8").removeClass("stage9").removeClass("stage10").addClass("stage9");
    $(".stage6 #selectedTab").css("top", tp);
    $("#FrontBackOptionPanal").css("display", "block");
    $("#FrontBackOptionPanalSection").addClass("showRightPropertyPanel");
}
function g2_22(mode) {
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    $("#textPropertyPanel").css("display", "none");
    $("#objPropertyPanel").css("display", "block");
    $(".inputObjectAlphaSlider").slider("option", "value", (D1AO.getOpacity() * 100));
    if (D1AO.IsEditable) {
        $("#LockImgProperties").prop('checked', true);
    } else {
        $("#LockImgProperties").prop('checked', false);
    }
    if (D1AO.IsHidden) {
        $("#BtnPrintImage").prop('checked', true);
    } else {
        $("#BtnPrintImage").prop('checked', false);
    }
    if (D1AO.IsOverlayObject) {
        $("#chkboxOverlayImg").prop('checked', true);
    } else {
        $("#chkboxOverlayImg").prop('checked', false);
    }
    if (D1AO.IsPositionLocked) {
        $("#LockPositionImg").prop('checked', true);
    } else {
        $("#LockPositionImg").prop('checked', false);
    }
    if (mode == 1) {
      //  if ((IsEmbedded && D1AO.IsTextEditable && (IsCalledFrom == 4))) {
      //  } else {
            $("#imgThumbPreview").attr("src", D1AO.getSrc());
            $(".imgthumbPreviewSlider").css("display", "block");
            $("#BtnCropImg2").css("display", "inline-block");
            $("#AddColorShape").css("display", "none"); $(".OpacityBtn").css("display", "inline-block");
            $(".imgthumbPreviewSliderBtn").css("display", "inline");
            $(".rotateSlider").slider("option", "value", D1AO.getAngle());
            if (IsCalledFrom == 3) {
                $(".toolbarImage").css("display", "none");
                $(".toolbarImage").css("opacity", "0");
                $(".shapeTools").css("display", "none"); $(".imgtool").css("display", "block");
            } else {
                //f0(D1AO);
                //e1("ImagePropertyPanel", lastPanelLocal);
                //pcL36('show', '#ImagePropertyPanel');
                //DisplayDiv('1');
            }
            $(".svgColorPanel").css("display", "none");
       // }
    } else if (mode == 3) {
        if ((D1AO.IsTextEditable && (IsCalledFrom == 4))) {
        } else {
            $(".rotateSlider").slider("option", "value", D1AO.getAngle());
            $("#imgThumbPreview").attr("src", D1AO.toDataURL()); $("#AddColorShape").css("display", "inline-block"); $(".OpacityBtn").css("display", "none");
            $("#BtnCropImg2").css("display", "none");
            $(".imgthumbPreviewSliderBtn").css("display", "none");

            if (IsCalledFrom == 3) {
                $(".toolbarImage").css("display", "none");
                $(".toolbarImage").css("opacity", "0"); $(".spanRectColour").css("background-color", D1AO.fill);
                $(".shapeTools").css("display", "block"); $(".imgtool").css("display", "none");
                m0();
            } 
        }
        $(".svgColorPanel").css("display", "block"); $("#AddColorShape").css("visibility", "hidden");
        $(".svgColorContainer").html("");
        var lstClrs = [];
        if (D1AO.customStyles != null) {
            $.each(D1AO.customStyles, function (i, IT) {

                var clr = IT.OriginalColor;
                if (!inList(lstClrs, clr)) {
                    lstClrs.push(clr);
                    if (IT.ModifiedColor != "")
                        clr = IT.ModifiedColor;
                    $(".svgColorContainer").append('<button id="" class="BtnChngeClr btnChangeShapeColor BtnChngeSvgClr' + i + '" title="Color picker" style="display: inline-block; background-color:' + clr + ' " onclick="f2_ChangeSVGColor(' + IT.PathIndex + ');"> </button>');
                }
            });
        } 
    } else {
        $("#AddColorShape").css("visibility", "visible");
        $(".svgColorPanel").css("display", "none");
        if ((D1AO.IsTextEditable && (IsCalledFrom == 4))) {
        } else {
            $(".rotateSlider").slider("option", "value", D1AO.getAngle());

            //$(".imgthumbPreviewSlider").css("display", "none");
            $("#imgThumbPreview").attr("src", D1AO.toDataURL()); $("#AddColorShape").css("display", "inline-block"); $(".OpacityBtn").css("display", "none");
            $("#BtnCropImg2").css("display", "none"); 
            $(".imgthumbPreviewSliderBtn").css("display", "none");

            if (IsCalledFrom == 3) {
                $(".toolbarImage").css("display", "none");
                $(".toolbarImage").css("opacity", "0"); $(".spanRectColour").css("background-color", D1AO.fill);
                $(".shapeTools").css("display", "block"); $(".imgtool").css("display", "none");
                m0();
            } else {
                //f0(D1AO);
                //e1("ImagePropertyPanel", lastPanelLocal);
                //pcL36('show', '#ImagePropertyPanel');
                //DisplayDiv('2');
            }
        }
    }
    g1_(D1AO);
}
function inList(list,obj) {
    var res = false;
    $.each(list, function (i, IT) {
        if(IT == obj)
        {
            res = true;
            return res;
        }
    });
    return res;
}
function g5(e) {
    IsDesignModified = true;
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        buildUndo(D1AG);
    } else {
        buildUndo(D1AO);
    }

    if (D1AO && showEBtn) {
        g5_2(e); $(".collapseDesignerMenu").css("display", "list-item");
    } else {
        g5_Sel(e); $(".collapseDesignerMenu").css("display", "list-item");
    }

}
function g5_Sel(e) {
    if (panelMode == 1) {
        g5_new(e);
    } else {
        g5_1(e);
    }
}
function g5_new(e) {
    if ($("#FrontBackOptionPanalSection").hasClass("showRightPropertyPanel")) {
        $("#FrontBackOptionPanalSection").removeClass("showRightPropertyPanel");
        //  $("#FrontBackOptionPanalSection").addClass("hideRightPropertyPanel");
        $("#FrontBackOptionPanal").css("display", "none");
    }
    var selName = "select#BtnSelectFonts";
    $(selName).fontSelector('option', 'close', 'Arial Black');
    pcL13();
    pcL36('hide', '#divVariableContainer , #DivPersonalizeTemplate , #DivAdvanceColorPanel , #quickText');
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && D1AG.type === 'group') {
        pcL36('show', '#DivAlignObjs');

    }
    else if (D1AO) { // && (D1AO.IsPositionLocked != true || IsCalledFrom == 2)
        $("#textPropertyPanel, #objPropertyPanel").css("display", "none");
        g2_1(e);
    }
    //} else {
    //    if (D1AO) {
           
    //        pcL13();
    //        pcL36('hide', '#DivAlignObjs , #textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText , #addImage , #addText , #DivToolTip , #DivAlignObjs , #quickTextFormPanel , #DivPersonalizeTemplate ');
    //        $(".layersPanel").click();
    //    }
    //}

}
function g5_1(e) {
    var selName = "select#BtnSelectFontsRetail";
    $(selName).fontSelector('option', 'close', 'Arial Black');
    pcL13();
    pcL36('hide', '#divVariableContainer');
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && D1AG.type === 'group') {
        pcL36('show', '#DivAlignObjs');
    }
    else if (D1AG && D1AG.type === 'image') {
    }
    else if (D1AO && (D1AO.IsPositionLocked != true || IsEmbedded == false)) {
        g2(e);
    } else {
        if (D1AO) {
            pcL13();
            pcL36('hide', '#DivAlignObjs , #textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText , #addImage , #addText , #DivToolTip , #DivAlignObjs , #quickTextFormPanel , #DivPersonalizeTemplate ');

        }
    }
}
function g5_2(e) {
    pcL36();
    var D1AO = canvas.getActiveObject();
    //  if (lAObj == D1AO.ObjectID) {
    g5_Sel();
    //     return false;
    // }
    //    $(".layersPanel").click();
    $("#sortableLayers li").removeClass("selectedItemLayers");
    $("#selobj_" + D1AO.ObjectID).addClass("selectedItemLayers");
}
function g6(e) {
    IsDesignModified = true;
    var X = e.left;
    var Y = e.top;
    if (ISG) {
        var line1 = 0;
        var line2 = 0;
        line1 = SXP[0];
        line2 = SXP[1];
        var iCounter = 1;
        while (iCounter < SXP.length - 1) {
            if (X > line1 && X < line2) {
                X = line1;
                break;
            }
            iCounter++;
            line1 = SXP[iCounter - 1];
            line2 = SXP[iCounter];
        }
        line1 = 0;
        line2 = 0;
        line1 = SYP[0];
        line2 = SYP[1];
        iCounter = 1;
        while (iCounter < SYP.length - 1) {
            if (Y > line1 && Y < line2) {
                Y = line1;
                break;
            }
            iCounter++;
            line1 = SYP[iCounter - 1];
            line2 = SYP[iCounter];
        }
        e.left = X;
        e.top = Y;
    }
}
function g7() {
    var OBS = canvas.getObjects();
    for (i = 0; i < TO.length; i++) {
        OBS.filter(function (obj) {
            if (obj.get('ObjectID') == TO[i].ObjectID) {
                TO[i].DisplayOrderPdf = OBS.indexOf(obj);
            }
        });
    }
}
function h1(left, top) {
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
    D1NTO.ObjectType = 6; //c09
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 200;
    D1NTO.MaxHeight = 200;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    lAObj = D1NTO.ObjectID;
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

    var index;
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        if (IT.ObjectID == ROL.ObjectID) {
            index = i;
        }
    });
    D1NTO.DisplayOrderPdf = index;

    ROL.top = top;
    ROL.left = left;
    D1NTO.PositionX = ROL.left - ROL.maxWidth / 2;
    D1NTO.PositionY = ROL.top - ROL.maxHeight / 2;
    ROL.setCoords();

    ROL.C = "0";
    ROL.M = "0";
    ROL.Y = "0";
    ROL.K = "100";
    canvas.renderAll();
    TO.push(D1NTO);
    canvas.setActiveObject(ROL);
}
function h2(left, top) {
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
    NewCircleObejct.ObjectType = 7; //c07
    NewCircleObejct.ProductPageId = SP;
    NewCircleObejct.MaxWidth = 100;
    NewCircleObejct.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    NewCircleObejct.CircleRadiusX = 50;
    NewCircleObejct.CircleRadiusY = 50;
    NewCircleObejct.Opacity = 1;
    lAObj = NewCircleObejct.ObjectID;
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
    COL.top = top;
    COL.left = left;

    // c08
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
    canvas.setActiveObject(COL);
}
function i7() {
    var OBS = canvas.getObjects();
    var html = '';
    for (var i = OBS.length - 1; i >= 0; i--) {
        //  $.each(OBS, function (i, ite) {
        var ite = OBS[i];
        $.each(TO, function (i, IT) {
            if (ite.ObjectID == IT.ObjectID) {
                var iLock = false;
                if (ite.IsPositionLocked == true) {
                    iLock = true;
                }
                if (ite.type == "image") {

                    html += i9(ite.ObjectID, 'Image Object', ite.type, ite.getSrc(), iLock);
                } else if (ite.type == "text" || ite.type == "i-text") {
                    html += i9(ite.ObjectID, ite.text, ite.type, "./Content/Designer/assets-v2/txtObject.png", iLock);
                } else if (ite.type == "ellipse") {
                    html += i9(ite.ObjectID, 'Ellipse Object', ite.type, "./Content/Designer/assets-v2/circleObject.png", iLock);
                } else {
                    html += i9(ite.ObjectID, 'Shape Object', ite.type, "./Content/Designer/assets-v2/rectObject.png", iLock);
                }

            }
        });
    }
    if (IsCalledFrom != 3) {
        $("#sortableLayers").html(html);

        $("#sortableLayers").sortable({
            placeholder: "ui-state-highlight",
            update: function (event, ui) {
                i8($(ui.item).children(".selectedObjectID").text(), ui.item.index());
            },
            start: function (e, ui) {
                N111a = ui.item.index();
            }
        });
        $("#sortable").disableSelection();
    }
}
function i8(oI, oIn) {
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, ite) {
        if (ite.ObjectID == oI) {
            var dif = oIn - N111a;
            if (dif > 0) {
                for (var i = 0; i < dif; i++) {
                    canvas.sendBackwards(ite);
                }
            } else {
                dif = dif * -1;
                for (var i = 0; i < dif; i++) {
                    canvas.bringForward(ite);
                }
            }
            canvas.renderAll();
            g7();
            return false;
        }
    });
    i7();
}
function j4(e) {
    if ($(e.target).hasClass("ui-button-text") || e.target.id == "btnMoveObjLeftTxt" || e.target.id == "btnMoveObjUpTxt" || e.target.id == "btnMoveObjDownTxt" || e.target.id == "btnMoveObjRightTxt" || e.target.id == "divPositioningPanel" || $(e.target).hasClass("DivTitleLbl")) {
    } else {
        var DIAO = canvas.getActiveObject();
        if (DIAO && (DIAO.type === 'text' || DIAO.type === 'i-text')) {

        } else if (DIAO && DIAO.type === 'image' && DIAO.IsQuickText == true) {
            // show the change image screen
        }
    }
}
function j7(i, n) {

    $.getJSON("/designerapi/TemplateColorStyles/UpdateCorpColor/" + i + "/" + n,
		function (DT) {
		    // var html = "<div class ='ColorPalletCorp' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;" + ",&quot;" + Sname + "&quot;);'" + "></div><div class='ColorPalletCorpName'>" + Sname + "</div>";
		    //$('#DivColorContainer').append(html);
		    if (n == "DeActive") {
		        // var somvar = $("#somediv").html();
		        $("#pallet" + i).clone(true).appendTo('#tabsInActiveColors');
		        $("#textColor" + i).clone(true).appendTo('#tabsInActiveColors');
		        $('#tabsActiveColors #pallet' + i).remove();
		        $('#tabsActiveColors #textColor' + i).remove();
		        $('#btnClr' + i).remove();
		        var html = "<button  id ='btnClr" + i + "' class='btnActiveColor' title='Activate this color' onclick='j7(" + i + ",&quot;Active&quot;);'></button>";
		        $("#pallet" + i).append(html);
		    } else {
		        $("#pallet" + i).clone(true).appendTo('#tabsActiveColors');
		        $("#textColor" + i).clone(true).appendTo('#tabsActiveColors');
		        $('#tabsInActiveColors #pallet' + i).remove();
		        $('#tabsInActiveColors #textColor' + i).remove();
		        $('#btnClr' + i).remove();
		        var html = "<button  id ='btnClr" + i + "' class='btnDeactiveColor' title='Deactivate this color' onclick='j7(" + i + ",&quot;DeActive&quot;);'></button>";
		        $("#pallet" + i).append(html);
		    }
		});
}
function l2(event) {
    if (event.keyCode == ctrlKey) D1CD = false;

    if (event.keyCode == D1SK) D1SD = false


    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();

    if (D1AG && IsInputSelected == false) {
        if (event.keyCode == 38) {
            if (D1SD)
                D1AG.top -= 1;
            else
                D1AG.top -= 5;
        }
        else if (event.keyCode == 37) {
            if (D1SD)
                D1AG.left -= 1;
            else
                D1AG.left -= 5;
        } else if (event.keyCode == 39) {
            if (D1SD)
                D1AG.left += 1;
            else
                D1AG.left += 5;
        }
        else if (event.keyCode == 40) {
            if (D1SD)
                D1AG.top += 1;
            else
                D1AG.top += 5;
        }
        canvas.renderAll();
        if (D1SD == false) {
            if (event.keyCode == 38 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 40) {
                var objectsInGroup = D1AG.getObjects();
                objectsInGroup.forEach(function (OPT) {
                    var clonedItem = fabric.util.object.clone(OPT);
                    clonedItem.left += D1AG.left;
                    clonedItem.top += D1AG.top;
                });
            }
        }
    }
    else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        if (D1AO.isEditing == true) {
            D1AO.onKeyDown(event);
        } else {
            if (event.keyCode == 38) {
                if (D1SD)
                    D1AO.top -= 1;
                else
                    D1AO.top -= 5;
                canvas.renderAll();
            }
            else if (event.keyCode == 37) {
                if (D1SD)
                    D1AO.left -= 1;
                else
                    D1AO.left -= 5;
                canvas.renderAll();
            } else if (event.keyCode == 39) {
                if (D1SD)
                    D1AO.left += 1;
                else
                    D1AO.left += 5;
                canvas.renderAll();
            }
            else if (event.keyCode == 40) {
                if (D1SD)
                    D1AO.top += 1;
                else
                    D1AO.top += 5;
                canvas.renderAll();
            }
        }
    }
    if (event.keyCode == 46 || event.keyCode == 8) {
        if (N1LA != 1 && IsInputSelected == false) {
            var D1AO = canvas.getActiveObject();
            var D1AG = canvas.getActiveGroup();
            if (D1AG) {
                if (confirm("Are you sure you want to Remove this Group from the canvas.")) {
                    var objectsInGroup = D1AG.getObjects();
                    canvas.discardActiveGroup();
                    objectsInGroup.forEach(function (OPT) {
                        c2_del(OPT);
                        canvas.remove(OPT);
                    });
                }
            } else if (D1AO) {
                if (confirm("Are you sure you want to Remove this Object from the canvas.")) {
                    c2_del(D1AO);
                    canvas.remove(D1AO);
                }
            }
        }
    }
}
function l2_temp() {
    var orientation = 2;
    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == SP) {
            orientation = IT.Orientation;
            if (orientation == 1) {
                $(".BtnBCPresetOptionsPort").css("display", "none");
            } else {
                $(".BtnBCPresetOptionsLand").css("display", "none");
            }
            return;
        }
    });

}

$('input, textarea, select').focus(function () {
    IsInputSelected = true;
}).blur(function () {
    IsInputSelected = false;
});
function l3(e) {
    if (e.keyCode == ctrlKey) D1CD = true;
    if (e.keyCode == D1SK) D1SD = true;
    if (e.keyCode >= 37 && e.keyCode <= 40 && IsInputSelected == false) {
        return false
    }
    var sObj = canvas.getActiveObject();
    if (!sObj) {
        if (e.keyCode == 8 && IsInputSelected == false) {
            if (IsDesignModified) {
                if (!confirm("You have unsaved changes. Do you want to leave without saving changes ?")) {
                    return false;
                }
            }
        }
    }
    if (D1SD && (e.keyCode == D1SK)) {
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            var lockedObjectFound = false;
            var objectsInGroup = D1AG.getObjects();
            $.each(objectsInGroup, function (j, Obj) {
                if (Obj.IsPositionLocked == true) {
                    lockedObjectFound = true;
                }
            });
            if (!lockedObjectFound) {
                pcL13();   // show group property panel and hide others
                pcL36('hide', '#textPropertPanel ,#DivAdvanceColorPanel , #DivColorPallet , #DivColorPallet , #ShapePropertyPanel ,#ImagePropertyPanel , #UploadImage , #quickText , #addImage , #addText');
                k4();
                pcL36('show', "#DivAlignObjs");
            } else {
                // hide all panels 
                pcL13();
                pcL36('hide', "#DivAlignObjs");
            }
        }
    }
    if (D1CD && (e.keyCode == cKey)) {
        if (N1LA != 1) {
            var D1AG = canvas.getActiveGroup();
            var D1AO = canvas.getActiveObject();
            D1CO = [];
            if (D1AG) {
                var objectsInGroup = D1AG.getObjects();
                $.each(objectsInGroup, function (j, Obj) {
                    $.each(TO, function (i, IT) {
                        if (IT.ObjectID == Obj.ObjectID) {
                            c2_01(Obj);
                            D1CO.push(IT);
                            return false;
                        }
                    });
                });

            } else if (D1AO) {
                $.each(TO, function (i, IT) {
                    if (IT.ObjectID == D1AO.ObjectID) {
                        c2_01(D1AO);
                        D1CO.push(IT);
                        return false;
                    }
                });
            }
        }
    }
    else if (D1CD && (e.keyCode == vKey) && IsInputSelected == false) //paste
    {
        if (N1LA != 1) {
            var OOID;
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
        }
    }
}
function l5(lID) {
    $.each(llData, function (i, IT) {
        if (IT.LayoutID == lID) {
            //   $("#presetTitle").val(IT.Title);
            l8(parseInt(IT.ImageLogoType));
            //  $("#presetLogo").val(IT.ImageLogoType);
            TempOB = [];
            TempFinO("Name");
            TempFinO("Title");
            TempFinO("CompanyName");
            TempFinO("CompanyMessage");
            TempFinO("AddressLine1");
            TempFinO("Phone");
            TempFinO("Fax");
            TempFinO("Email");
            TempFinO("Website");
            $.each(TempOB, function (i, item) {
                if (item != null && item != "") {
                    l6(IT.LayoutAttributes, item.Name);
                    var Pres1 = br13;
                    if (Pres1 != null && Pres1 != undefined) {
                        item.maxWidth = parseInt(Pres1.maxWidth);
                        item.maxHeight = parseInt(Pres1.maxHeight);
                        item.fontSize = parseInt(Pres1.fontSize);
                        item.textAlign = Pres1.textAlign;
                        item.fontWeight = Pres1.fontWeight;
                        item.top = parseInt(Pres1.topPos) * dfZ1l;
                        item.left = parseInt(Pres1.LeftPos) * dfZ1l;
                        item.setCoords();
                    }

                }
            });
            canvas.renderAll();
        }
    });
}
function l6(array, attr) {
    $.each(array, function (i, item) {
        if (item.FeildName == attr) {
            br13 = item;
            return;
        }
    });
    return null;
}
function l8(mode) {

    if (mode == 1) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset5_2.png");
    } else if (mode == 2) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset5_1.png");
    } else if (mode == 3) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset5.png");
    } else if (mode == 4) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset4.png");
    } else if (mode == 5) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset3.png");
    } else if (mode == 6) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset2.png");
    } else if (mode == 7) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset1.png");
    } else if (mode == 8) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset6.png");
    } else if (mode == 9) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset7.png");
    } else if (mode == 10) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset8.png");
    } else if (mode == 11) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset9.png");
    } else if (mode == 12) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset10.png");
    } else if (mode == 13) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset10_1.png");
    } else if (mode == 14) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/preset10_2.png");
    } else if (mode == 15) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/presets14.png");
    } else if (mode == 16) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/presets-15.png");
    } else if (mode == 17) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/presets16.png");
    } else if (mode == 18) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/presets11.png");
    } else if (mode == 19) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/presets12.png");
    } else if (mode == 20) {
        $("#imgPreviewPreset").prop("src", "/Content/Designer/assets-v2/presets/presets-13.png");
    }
}
function pcL01(mode) {
    if (mode == 1) {
        if ($(".toolbarText").css("opacity") == "0") {
            $(".toolbarText").css("display", "block");
            $(".toolbarText").css("opacity", "1");
        } else {
            $(".toolbarText").css("display", "none");
            $(".toolbarText").css("opacity", "0");
        }
    } else if (mode == 2) {
        $(".toolbarImageTransparency").css("display", "none");
        $(".toolbarImageTransparency").css("opacity", "0");
        if ($(".toolbarImage").css("opacity") == "0") {
            $(".toolbarImage").css("display", "block");
            $(".toolbarImage").css("opacity", "1");
        } else {
            $(".toolbarImage").css("display", "none");
            $(".toolbarImage").css("opacity", "0");
        }
    } else if (mode == 3) {
        $(".toolbarImage").css("display", "none");
        $(".toolbarImage").css("opacity", "0");
        if ($(".toolbarImageTransparency").css("opacity") == "0") {
            $(".toolbarImageTransparency").css("display", "block");
            $(".toolbarImageTransparency").css("opacity", "1");
        } else {
            $(".toolbarImageTransparency").css("display", "none");
            $(".toolbarImageTransparency").css("opacity", "0");
        }
    }
}
function pcL02() {
    $(".paletteToolbarWedge").css("display", "block");
    pcL36('toggle', '#DivColorPickerDraggable');
}
function pcL02_bK() {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    $(".paletteToolbarWedge").css("display", "none");
    pcL36('hide', '#DivAdvanceColorPanel')
    $("#DivColorPickerDraggable").css("-webkit-transform", "matrix(1, 0, 0, 1, 127, 175)");
    pcL36('toggle', '#DivColorPickerDraggable');
}
function pcL02_main() {
    $(".paletteToolbarWedge").css("display", "none");
    //   pcL36('hide', '#DivAdvanceColorPanel')
    $("#DivColorPickerDraggable").css("-webkit-transform", "matrix(1, 0, 0, 1, 127, 258)");
    pcL36('toggle', '#DivColorPickerDraggable');
}
function pcL02_main2() {
    $(".paletteToolbarWedge").css("display", "none");
    //   pcL36('hide', '#DivAdvanceColorPanel')
    $("#DivColorPickerDraggable").css("-webkit-transform", "matrix(1, 0, 0, 1, 127, 206)");
    pcL36('toggle', '#DivColorPickerDraggable');
}
function pcL03() {
    if (confirm("Are you sure you want to Remove this Object from the canvas.")) {
        var D1AO = canvas.getActiveObject(),
        D1AG = canvas.getActiveGroup();
        if (D1AO) {
            //  c2(D1AO, 'delete');
            c2_del(D1AO);
            canvas.remove(D1AO);
        }
        else if (D1AG) {
            var objectsInGroup = D1AG.getObjects();
            canvas.discardActiveGroup();
            objectsInGroup.forEach(function (OPT) {
                //  c2(OPT, 'delete');
                c2_del(OPT);
                canvas.remove(OPT);
            });
        }
        pcL36('hide', '#divTxtPropPanelRetail');
    }
}
function pcL04() {
    var fontFamily = $('#BtnSelectFontsRetail').val();

    fontFamily = $('.fonts .selected').css('font-family');
    if (fontFamily.indexOf("(select)") != -1) {
        fontFamily = "";
    }
    while (fontFamily.indexOf("'") != -1) {
        fontFamily = fontFamily.replace("'", "");
    }
    while (fontFamily.indexOf('"') != -1) {
        fontFamily = fontFamily.replace('"', '');
    }
    if (fontFamily != "") {
        var selectedObject = canvas.getActiveObject();
        if (selectedObject && selectedObject.isEditing == false) {
            if (selectedObject && (selectedObject.type === 'text' || selectedObject.type === 'i-text')) {
                selectedObject.fontFamily = fontFamily;
                $("#txtAreaUpdateTxt").css("font-family", fontFamily);
                //c2(selectedObject);
                canvas.renderAll();
            }
        } else {
            setActiveStyle("font-family", fontFamily);
        }
    }

    selName = "select#BtnSelectFontsRetail";
    $(selName).fontSelector('option', 'close', 'Arial Black');
}
function pcL05() {
    var selectedObject = canvas.getActiveObject();
    if (selectedObject) {
        if (selectedObject.IsTextEditable != true) {
            setActiveStyle('font-Weight', 'bold');
            //  c2(selectedObject);
            pcL22_Sub(selectedObject);
            canvas.renderAll();
        }
    }
}
function pcL06() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        if (D1AO.IsTextEditable != true) {
            setActiveStyle('font-Style', 'italic');
            pcL22_Sub(D1AO);
            // c2(D1AO);
            canvas.renderAll();
        }
    }
}
function pcL06ULine() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        setActiveStyle('textDecoration', 'underline');
        pcL22_Sub(D1AO);
        // c2(D1AO);
        canvas.renderAll();
    }
}
function pcL07() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        if (D1AO.IsTextEditable != true) {
            D1AO.set('textAlign', 'left');
            $("#txtAreaUpdateTxt").css("text-align", 'left');
            //   c2(D1AO);
            pcL22_Sub(D1AO);
            canvas.renderAll();
        }
    }
}
function pcL08() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        if (D1AO.IsTextEditable != true) {
            D1AO.set('textAlign', 'center');
            // c2(D1AO);
            pcL22_Sub(D1AO);
            $("#txtAreaUpdateTxt").css("text-align", 'center');
            canvas.renderAll();
        }
    }
}
function pcL09() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        if (D1AO.IsTextEditable != true) {
            D1AO.set('textAlign', 'right');
            //  c2(D1AO);
            pcL22_Sub(D1AO);
            $("#txtAreaUpdateTxt").css("text-align", 'right');
            canvas.renderAll();
        }
    }
}
function pcL10() {
    pcL36('hide', '#DivLayersPanel');
    var D1AG = canvas.getActiveGroup();
    var D1AO = canvas.getActiveObject();
    D1CO = [];
    if (D1AG) {
        var objectsInGroup = D1AG.getObjects();
        $.each(objectsInGroup, function (j, Obj) {
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == Obj.ObjectID) {
                    c2_01(Obj);
                    D1CO.push(IT);
                    return false;
                }
            });
        });
    } else if (D1AO) {
        $.each(TO, function (i, IT) {
            if (IT.ObjectID == D1AO.ObjectID) {
                c2_01(D1AO);
                D1CO.push(IT);
                return false;
            }
        });
    }
}
function pcL11() {
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var angle = D1AO.getAngle();
    angle = angle - 5;
    D1AO.setAngle(angle);
    // c2(D1AO);
    canvas.renderAll();
}
function pcL12() {
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var angle = D1AO.getAngle();
    angle = angle + 5;
    D1AO.setAngle(angle);
    //  c2(D1AO);
    canvas.renderAll();
}
function pcL13() {
    //close all panels  

    $(".retailPropPanels").css("display", "none");
    $(".retailPropPanelsSubMenu").css("display", "none");
    $(".retailPropPanelsSubMenu").css("opacity", "0");
}
function pcL14() {
    var D1AO = canvas.getActiveObject();
    var oldW = D1AO.getWidth();
    var oldH = D1AO.getHeight();
    var scale = D1AO.get('scaleX') + 0.05;
    D1AO.set('scaleX', scale);
    scale = D1AO.get('scaleY') + 0.05;
    D1AO.set('scaleY', scale);
    var dif = D1AO.getWidth() - oldW;
    dif = dif / 2
    D1AO.left = D1AO.left + dif;
    dif = D1AO.getHeight() - oldH;
    dif = dif / 2
    D1AO.top = D1AO.top + dif;
    // c2(D1AO);
    canvas.renderAll();
}
function pcL15() {
    var D1AO = canvas.getActiveObject();
    var oldW = D1AO.getWidth();
    var oldH = D1AO.getHeight();
    var scale = D1AO.get('scaleX') - 0.05;
    if (scale > 0.10) {
        D1AO.set('scaleX', scale);
    }
    scale = D1AO.get('scaleY') - 0.05;
    if (scale > 0.10) {
        D1AO.set('scaleY', scale);
    }
    var dif = D1AO.getWidth() - oldW;
    dif = dif / 2
    D1AO.left = D1AO.left + dif;
    dif = D1AO.getHeight() - oldH;
    dif = dif / 2
    D1AO.top = D1AO.top + dif;
    //  c2(D1AO);
    canvas.renderAll();
}
function pcL16() {
    var D1AO = canvas.getActiveObject();
    var angle = D1AO.getAngle();
    angle = angle - 5;
    D1AO.setAngle(angle);
    //  c2(D1AO);
    canvas.renderAll();
}
function pcL17() {
    var D1AO = canvas.getActiveObject();
    var angle = D1AO.getAngle();
    angle = angle + 5;
    D1AO.setAngle(angle);
    // c2(D1AO);
    canvas.renderAll();
}
function pcL18() {
    var D1AO = canvas.getActiveObject();
    canvas.bringForward(D1AO);
    canvas.renderAll();
    //  c2(D1AO);
    g7();
}
function pcL19() {
    var D1AO = canvas.getActiveObject();
    canvas.sendBackwards(D1AO);
    canvas.renderAll();
    //  c2(D1AO);
    g7();
}
function pcL20_new() {
    var D1AO = canvas.getActiveObject();
    var src;
    if (D1AO && D1AO.type === 'image' && D1AO) {
        src = D1AO.getSrc();
        if (src.indexOf("Imageplaceholder_sim.png") == -1) {
            $(".cropimage").attr('src', src + "?r=" + CzRnd);
            $(function () {
                $('.cropimage').each(function () {
                    var image = $(this);
                    $(".closePanelButtonCropTool").css("left", (D1AO.getWidth() - 35) + "px");
                    $(".CropControls").css("height", (D1AO.getHeight() + 5) + "px");
                    $(".CropControls").css("width", (D1AO.getWidth() + 5) + "px");
                    $(".NewCropToolCotainer").css("height", $(document).height() + "px");
                    var width = $(".CropControls").width() / 2;
                    var height = $(".CropControls").height() / 2;
                    var l = $("#canvas").offset().left + D1AO.left - D1AO.getWidth() / 2;
                    var h = $("#canvas").offset().top + D1AO.top - D1AO.getHeight() / 2;
                    // l = l - 430 + 128;
                    // h -= 30;
                    if (h < 0)
                    {
                        $(".CroptoolBar").css("transform", "translate3d(-3px, "+(h*-1) +"px, 0px)");
                    } else
                    {
                        $(".CroptoolBar").css("transform", "translate3d(-3px, -47px, 0px)");
                    }
                    $(".CropControls").css("left", (l) + "px");
                    $(".CropControls").css("top", (h) + "px");
                    image.cropbox({ width: D1AO.getWidth(), height: D1AO.getHeight(), showControls: 'auto', xml: D1AO.ImageClippedInfo })
                      .on('cropbox', function (event, results, img) {
                          crX = (results.cropX);
                          crY = (results.cropY);
                          crWd = (results.cropW);
                          crHe = (results.cropH);
                          crv1 = results.crv1;
                          crv2 = results.crv2;
                          crv3 = results.crv3;
                          crv4 = results.crv4;
                          crv5 = results.crv5;
                          pcL20_new_MoveImg(src, results.crv1, results.crv6, results.crv7);
                      });
                    $(".cropButton").click(function (event) {
                        //pcL20();
                        pcL20_newCrop(src);
                    });
                });



            });
            pcL36('hide', '#divPositioningPanel');
            $("#divBkCropTool").css("display", "block");
            // pcL36('toggle', '#divBkCropTool');
            pcL36('toggle', '#NewCropToolCotainer');

        } else {
            if (IsCalledFrom != 4) {
                alert("Please add an image to crop it!");
            }
        }
    }
}
function pcL20_new_MoveImg(src, percent, AcHei, AcWid) {

    $(".imgOrignalCrop").attr("src", src);
    $(".imgOrignalCrop").attr("width", Math.round(AcWid * percent));
    $(".imgOrignalCrop").attr("height", Math.round(AcHei * percent));
    $(".imgOrignalCrop").css("left", $(".cropImage").css("left"));
    $(".imgOrignalCrop").css("top", $(".cropImage").css("top"));
    //  var position = $(".cropImage").offset();
    //   $('.overlayHoverbox').css(position);
}
function pcL20_newCrop() {
    var XML = new XMLWriter();
    XML.BeginNode("Cropped");

    XML.Node("sx", crX.toString());
    XML.Node("sy", crY.toString());
    XML.Node("swidth", crWd.toString());
    XML.Node("sheight", crHe.toString());


    XML.Node("crv1", crv1.toString());
    XML.Node("crv2", crv2.toString());
    XML.Node("crv3", crv3.toString());
    XML.Node("crv4", crv4.toString());
    XML.Node("crv5", crv5.toString());
    XML.EndNode();
    XML.Close();
    var D1AO = canvas.getActiveObject();
    if (D1AO && D1AO.type == 'image') {
        D1AO.ImageClippedInfo = XML.ToString().replace(/</g, "\n<");
        canvas.renderAll();
    }
    pcl20_newCropCls();
}
function pcl20_newCropCls() {
    if (croppedInstance != null) {
        croppedInstance.remove();
        croppedInstance = null;
    }
    $("#divBkCropTool").css("display", "none");
    pcL36('hide', '#NewCropToolCotainer');
}
function pcL21() {
    if (confirm("Are you sure you want to Remove this Object from the canvas.")) {
        var D1AO = canvas.getActiveObject(),
        D1AG = canvas.getActiveGroup();
        if (D1AO) {
            //   c2(D1AO, 'delete');
            c2_del(D1AO);
            canvas.remove(D1AO);
        }
        else if (D1AG) {
            var objectsInGroup = D1AG.getObjects();
            canvas.discardActiveGroup();
            objectsInGroup.forEach(function (OPT) {
                //  c2(OPT, 'delete');
                c2_del(OPT);
                canvas.remove(OPT);
            });
        }
        pcL36('hide', '#ImagePropertyPanel');
    }
}
function pcL22_Sub(D1AO) {
    $(".spanRectColour").css("background-color", D1AO.fill);
    $(".spanTxtcolour").css("background-color", D1AO.fill);
    if (D1AO.fontWeight == "bold") {
        $(".textToolbarBold").addClass("propOn");
    } else {
        $(".textToolbarBold").removeClass("propOn");
    }
    if (D1AO.fontStyle == "italic")
        $(".textToolbarItalic").addClass("propOn");
    else
        $(".textToolbarItalic").removeClass("propOn");

    $(".textToolbarLeft").removeClass("propOn");
    $(".textToolbarCenter").removeClass("propOn");
    $(".textToolbarRight").removeClass("propOn");
    if (D1AO.textAlign == "left")
        $(".textToolbarLeft").addClass("propOn");
    else if (D1AO.textAlign == "center")
        $(".textToolbarCenter").addClass("propOn");
    else if (D1AO.textAlign == "right")
        $(".textToolbarRight").addClass("propOn");
}
function pcL25() {
    var D1AO = canvas.getActiveObject();
    canvas.sendToBack(D1AO);
    canvas.renderAll();
    // c2(D1AO);
    g7();
}
function pcL26() {
    var D1AO = canvas.getActiveObject();
    canvas.bringToFront(D1AO);
    //   c2(D1AO);
    canvas.renderAll();
    g7();
}
function pcL27() {
    var D1AO = canvas.getActiveObject();
    canvas.bringForward(D1AO);
    canvas.renderAll();
    //  c2(D1AO);
    g7();
}
function pcL28() {
    var D1AO = canvas.getActiveObject();
    canvas.sendBackwards(D1AO);
    canvas.renderAll();
    //  c2(D1AO);
    g7();
}
function pcL29(fontSize, isBold, ContentString) {


    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "New Text";
    D1NTO.ContentString = ContentString;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = isBold;
    D1NTO.IsItalic = false;
    D1NTO.LineSpacing = 1.4;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.CharSpacing = 0;
    D1NTO.ProductPageId = SP;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    var text = ContentString;
    var textLength = text.length;
    D1NTO.MaxWidth = 170;
    D1NTO.MaxHeight = 80;

    D1NTO.IsQuickText = false;
    D1NTO.FontSize = fontSize;
    D1NTO.textCase = 0;
    D1NTO.IsUnderlinedText = false;
    var uiTextObject = c0(canvas, D1NTO);
    var center = canvas.getCenter();
    uiTextObject.left = center.left;
    uiTextObject.animate('top', center.top, { onChange: canvas.renderAll.bind(canvas) });
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;

    //canvas.renderAll();
    uiTextObject.setCoords();
    TO.push(D1NTO);
    lAObj = D1NTO.ObjectID;
    canvas.setActiveObject(uiTextObject);
}
var listToPass = [];
function save_rrs_se_se(obj) {
    $.each(TO, function (j, item) {
        if (item.ContentString.indexOf(obj.VariableTag) != -1) {
            listToPass.push(obj);
            return true;
        }
    });
    return false;
}
function save_rs_se(varlist) {
    listToPass = [];
    $.each(varlist, function (j, obj) {
        save_rrs_se_se(obj);
    });
    return listToPass;
}
function save_rs() {
    var to = "/designerApi/SmartForm/SaveTemplateVariables";
    var dList = save_rs_se(varList);
    var jsonObjects = JSON.stringify(dList, null, 2);
    var options = {
        type: "POST",
        url: to,
        data: jsonObjects,
        contentType: "application/json",
        async: true,
        complete: function (httpresp, returnstatus) {
            if (returnstatus == "success") {
                if (httpresp.responseText == 'true') {
                       //do nothing
                }
                else {
                    alert(httpresp.responseText);
                }
            }
        }
    };
    var returnText = $.ajax(options).responseText;
}
function setActiveStyle(styleName, value, c, m, y, k) {
    object = canvas.getActiveObject();
    if (!object) return;
    if (object.setSelectionStyles && object.isEditing) {
        var style = {};
        style[styleName] = value;
        object.setSelectionStyles(style);
        object.setCoords();
        if(styleName = "font-Size")
        {
            object.hasInlineFontStyle = true;
        }
    }
    else {
        if (styleName == "color") {
            styleName = "fill";
            object.setColor(value);
            object.C = c;
            object.M = m;
            object.Y = y;
            object.K = k;
        } else if (styleName == "font-Size") {
            styleName = "fontSize";
            object.fontSize = value;
        } else if (styleName == "font-Weight") {
            styleName = "fontWeight";
            if (object.fontWeight == 'bold') {
                value = 'normal';
            }
            else {
                value = 'bold';
            }
            object.set('fontWeight', value);

        } else if (styleName == "font-Style") {
            styleName = "fontStyle";
            if (object.fontStyle == 'italic') {
                value = 'normal';
            }
            else {
                value = 'italic';
            }
            object[styleName] = value;
        } else if (styleName == "textDecoration") {
            styleName = "textDecoration";
            if (object.textDecoration == 'underline') {
                value = 'initial';
            }
            else {
                value = 'underline';
            }
            object[styleName] = value;
        }

    }

    object.setCoords();
    canvas.renderAll();
}
function setActiveProp(name, value) {
    var object = canvas.getActiveObject();
    if (!object) return;

    object.set(name, value).setCoords();
    canvas.renderAll();
}
function TempFinO(n) {
    var found = false;
    $.each(TO, function (i, item) {
        if (item.Name == n && item.IsQuickText == true && item.ProductPageId == SP) {
            TempFinO2(item.ObjectID, n);
            found = true;
            return false
        }
    });
    if (found == false) {
        TempOB.push("");
    }
}
function TempFinO2(n, no) {
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, item) {
        if (item.ObjectID == n) {
            item.Name = no;
            TempOB.push(item);
            return false;
        }
    });
}
function pcL29_pcMove(type) {
    if (type == 1) {
        // add text 
        $("#pnlAddMain").css("top", "-400px");
        //$(".UlAddMain").css("display", "none");
    } else if (type == 2) {
        // add image main panel  
        $("#pnlAddMain").css("top", "-400px");
        //  $("#pnlAddMain").css("top", "-160px");
        // $(".UlAddMain").css("display", "none");
    } else if (type == 3) {
        // free images 

        $(".ulImagesSecTop").css("display", "none");
    } else if (type == 4) {
        // my images
        $(".ulImagesSecTop").css("display", "none");
    } else if (type == 5) {
        // my logos 
        $(".ulImagesSecTop").css("display", "none");
    } else if (type == 6) {
        // template images 
        $(".ulImagesSecTop").css("display", "none");
    } else if (type == 7) { // all backgrounds
        $(".bkMainPanels").css("display", "none");
    } else if (type == 8) { // all backgrounds
        $(".bkMainPanels").css("display", "none");
    } else if (type == 9) { // go to main panel 
        //$(".TempUlAddMain").classList.add('removed');
        $(".TempUlAddMain").addClass("removed");
        //   $(".TempUlAddMain").css("display", "none");


        //   box.css("display", "none");
    }
}
function pcL29_pcRestore(type) {
    if (type == 1) {
        // add text 
        //  $(".UlAddMain").css("display", "block");
        $("#pnlAddMain").css("top", "0px");
    } else if (type == 2) {
        $("#pnlAddMain").css("top", "0px");
        // add image main panel  
        //$(".UlAddMain").css("display", "block");
        //      $("#resultsText").css("top", "0px");
    } else if (type == 3) {
        // free images 
        $("#resultsText").css("top", "0px");
        //  $(".ulImagesSecTop").css("display", "block");
        // 
    } else if (type == 4) {
        // my images
        $(".ulImagesSecTop").css("display", "block");
    } else if (type == 5) {
        // my logos 
        $(".ulImagesSecTop").css("display", "block");
    } else if (type == 6) {
        // template images 
        $(".ulImagesSecTop").css("display", "block");
    } else if (type == 7) { // all backgrounds
        $(".bkMainPanels").css("display", "block");
    } else if (type == 8) { // go to main panel 
        var box = $('#TempUlAddMain');
        $(".TempUlAddMain").removeClass("removed");
        // box.hidden = false;
        box.removeClass('hidden');
        box.css("display", "block");
        //setTimeout(function () {
        //    box.removeClass('visuallyhidden');
        //}, 20);

    }

}
function pcl42() {
    StartLoader("Applying smart form variables to canvas.");
    if (pcl42_Validate()) {
        c2_v2(); c2_v2();// update template objects 
        if ($("#optionRadioOtherProfile").is(':checked')) {
            pcl42_updateVariables(smartFormData.AllUserScopeVariables[$("#smartFormSelectUserProfile").val()]);
            pcl42_svc(smartFormData.AllUserScopeVariables[$("#smartFormSelectUserProfile").val()], $("#smartFormSelectUserProfile").val());// save variables
        }
        else {
            pcl42_updateVariables(smartFormData.scopeVariables); 
            pcl42_svc(smartFormData.scopeVariables, ContactID);// save variables
        }
        pcl42_UpdateTO();
        bleedPrinted = false;
        d5_sub(SP,true);
    } else
    {
        alert("Variable validation failed");
    } 

    clearInterval(var2);
    $("#MainLoader").css("display", "none");
}
function pcl42_updateVariables(data) {
    
    $.each(data, function (i, IT) {
        if ($("#txtSmart" + IT.VariableId).val() != null && $("#txtSmart" + IT.VariableId).val() != "") {
            IT.Value = $("#txtSmart" + IT.VariableId).val();
        }
    });
}
function pcl42_UpdateTO() {
   
    $.each(TO, function (i, IT) {
        $.each(smartFormData.scopeVariables, function (i, obj) {
            //if(obj.ObjectType == 3)  // replace all the content strings containing variable tag
            //{
                var variableTag = obj.FieldVariable.VariableTag;
                var variableTagUpperCase = "_&*)_*!!£$";  // because we cannot set it to empty otherwise it will go to infinite loop
                if (obj.FieldVariable.VariableTag != null)
                    variableTagUpperCase= obj.FieldVariable.VariableTag.toUpperCase();
                var variableTagLowerCase = "_&*)_*!!£$";// because we cannot set it to empty otherwise it will go to infinite loop
                if (obj.FieldVariable.VariableTag != null)
                    variableTagLowerCase =obj.FieldVariable.VariableTag.toLowerCase();
                if (IT.originalContentString != null) {
                    if (IT.originalContentString.indexOf(variableTag) != -1 || IT.originalContentString.indexOf(variableTagUpperCase) != -1 || IT.originalContentString.indexOf(variableTagLowerCase) != -1) {
                        IT.ContentString = IT.originalContentString;
                        IT.textStyles = IT.originalTextStyles;
                        if (IT.originalTextStyles != null) {
                            IT.textStyles = IT.originalTextStyles;
                        }
                    }
                }
            //}
        });
    });
    if ($("#optionRadioOtherProfile").is(':checked')) {
        $.each(TO, function (i, IT) {
            $.each(smartFormData.AllUserScopeVariables[$("#smartFormSelectUserProfile").val()], function (i, obj) {
              //  if (obj.ObjectType == 3)  // replacing variables
                //    {
                if (obj.Value == null) {
                    obj.Value = "";
                }
                if (obj.Value != null ) {
                    var variableTag = obj.FieldVariable.VariableTag;
                    var variableTagUpperCase = "_&*)_*!!£$";// because we cannot set it to empty otherwise it will go to infinite loop
                    var variableTagLowerCase = "_&*)_*!!£$";// because we cannot set it to empty otherwise it will go to infinite loop
                    if (obj.FieldVariable.VariableTag != null) {
                        variableTagUpperCase = obj.FieldVariable.VariableTag.toUpperCase();
                        variableTagLowerCase = obj.FieldVariable.VariableTag.toLowerCase();
                    }
                   
                    
                    while (IT.ContentString.indexOf(variableTag) != -1)
                        updateTOWithStyles(IT, variableTag, obj.Value);
                    while (IT.ContentString.indexOf(variableTagUpperCase) != -1)
                        updateTOWithStyles(IT, variableTagUpperCase, obj.Value.toUpperCase());
                    while (IT.ContentString.indexOf(variableTagLowerCase) != -1)
                        updateTOWithStyles(IT, variableTagLowerCase, obj.Value.toLowerCase());
                        // IT.ContentString = IT.ContentString.replace(variableTag, obj.Value)
                }
              //  }
            });
        });
    }
    else {
        $.each(TO, function (i, IT) {
            $.each(smartFormData.scopeVariables, function (i, obj) {
                var variableTag = obj.FieldVariable.VariableTag;
                var variableTagUpperCase = "_&*)_*!!£$";// because we cannot set it to empty otherwise it will go to infinite loop
                var variableTagLowerCase = "_&*)_*!!£$";// because we cannot set it to empty otherwise it will go to infinite loop
                if (obj.FieldVariable.VariableTag != null) {
                    variableTagUpperCase = obj.FieldVariable.VariableTag.toUpperCase();
                    variableTagLowerCase = obj.FieldVariable.VariableTag.toLowerCase();
                }
                if (obj.Value == null) {
                    obj.Value = "";
                }
                if (obj.Value != null) {
                    while (IT.ContentString.indexOf(variableTag) != -1) {
                        updateTOWithStyles(IT, variableTag,obj.Value);
                    }
                    while (IT.ContentString.indexOf(variableTagUpperCase) != -1) {
                        updateTOWithStyles(IT, variableTagUpperCase, obj.Value.toUpperCase());
                    }
                    while (IT.ContentString.indexOf(variableTagLowerCase) != -1) {
                        updateTOWithStyles(IT, variableTagLowerCase, obj.Value.toLowerCase());
                    }
//                        IT.ContentString = IT.ContentString.replace(variableTag, obj.Value)
                }
            });
        }); 
    }
  

}
function pcl42_updateTemplate(DT) {
    if (userVariableData != null) {
        $.each(userVariableData, function (i, vari) {
            if (vari.Value != null) {
                var variableTag = vari.FieldVariable.VariableTag;
                $.each(DT, function (i, objDT) {
                    while (objDT.ContentString.indexOf(variableTag) != -1)
                        updateTOWithStyles(objDT, variableTag, vari.Value);
                    while (objDT.ContentString.indexOf(variableTag.toLowerCase()) != -1)
                        updateTOWithStyles(objDT, variableTag.toLowerCase(), vari.Value.toLowerCase());
                    while (objDT.ContentString.indexOf(variableTag.toUpperCase()) != -1)
                        updateTOWithStyles(objDT, variableTag.toUpperCase(), vari.Value.toUpperCase());
                });
            } else {
                var variableTag = vari.FieldVariable.VariableTag;
                var variableTagUpperCase = "_&*)_*!!£$";// because we cannot set it to empty otherwise it will go to infinite loop
                var variableTagLowerCase = "_&*)_*!!£$";// because we cannot set it to empty otherwise it will go to infinite loop
                if (vari.FieldVariable.VariableTag != null)
                {
                    variableTagUpperCase = vari.FieldVariable.VariableTag.toUpperCase();
                    variableTagLowerCase = vari.FieldVariable.VariableTag.toLowerCase();
                }
                $.each(DT, function (i, objDT) {
                    while (objDT.ContentString.indexOf(variableTag) != -1)
                        updateTOWithStyles(objDT, variableTag, "");
                    while (objDT.ContentString.indexOf(variableTagUpperCase) != -1)
                        updateTOWithStyles(objDT, variableTagUpperCase, "");
                    while (objDT.ContentString.indexOf(variableTagLowerCase) != -1)
                        updateTOWithStyles(objDT, variableTagLowerCase, "");
                });
            }
        });
    }
}
function getObjectToRemove(stylesCopy,objStyle){
    var result = null;
    $.each(stylesCopy, function (i, objDT) {
        if(objDT.characterIndex == objStyle.characterIndex)
        {
            result =objDT;
        }
    });
    return result;
}
function isEmptyStyles(customStyles) {
    if (!customStyles) return true;
    var obj = customStyles;

    for (var p1 in obj) {
        for (var p2 in obj[p1]) {
            return false;
        }
    }
    return true;
}
function updateTOWithStyles(obTO, vTag, vVal) {
    // obTO.ContentString = obTO.ContentString.replace(vTag, vVal);
    var objs = obTO.ContentString.split(vTag);
    var variableLength = vTag.length;
    var lengthCount = 0;
    var content = "";
    var styles = JSON.parse( obTO.textStyles);
    var stylesCopy =JSON.parse( obTO.textStyles);
    for (var i = 0; i < objs.length; i++) {
        content += objs[i];
        if ((i + 1) != objs.length) {
            content += vVal;
        }
        lengthCount += objs[i].length;
        var toMove = (i + 1) * variableLength;
        var toCopy = lengthCount;
        var styleExist = false;
        var stylesRemoved = 0;
        var StyleToCopy = null;
        if (styles != null && styles != "") {

            $.each(styles, function (i, objStyle) {

                if (parseInt(objStyle.characterIndex) == toCopy) {
                    styleExist = true;
                    StyleToCopy = objStyle;
                }
                if (parseInt(objStyle.characterIndex) <= (lengthCount + variableLength) && parseInt(objStyle.characterIndex) >= lengthCount) {
                    var objToRemove = getObjectToRemove(stylesCopy, objStyle);
                    if (objToRemove != null) {
                        stylesCopy = $.grep(stylesCopy, function (n, i) {
                            return (n.characterIndex != objToRemove.characterIndex);
                        });
                        stylesRemoved++;
                    }
                }
            });

            var diff = vVal.length - (variableLength);
            $.each(stylesCopy, function (i, objStyle) {
                if (parseInt(objStyle.characterIndex) > (lengthCount + vTag.length)) {
                    objStyle.characterIndex = ((parseInt(objStyle.characterIndex) + diff)).toString();
                }
            });
            if (styleExist) {
                for (var z = 0; z < vVal.length; z++) {
                    var objToAdd = {
                        fontName: StyleToCopy.fontName,
                        fontSize: StyleToCopy.fontSize,
                        fontStyle: StyleToCopy.fontStyle,
                        fontWeight: StyleToCopy.fontWeight,
                        textColor: StyleToCopy.textColor,
                        textCMYK: StyleToCopy.textCMYK,
                        characterIndex: (lengthCount + z).toString()

                    }
                    stylesCopy.push(objToAdd);
                }
            }
        }
      //  styles = new List < InlineTextStyles > (stylesCopy);
        lengthCount += vVal.length;
    }

    obTO.ContentString = content;
    if (styles != null && styles != "")
        obTO.textStyles = JSON.stringify(stylesCopy, null, 2);;
}
function pcl42_Validate() {
    var result = true;
    $(".requiredSFObj").removeClass("requiredSFObj");
    $.each(smartFormData.smartFormObjs, function (i, obj) {
        if (obj.ObjectType == 3)  // replacing variables
        {
            if(obj.IsRequired == true)
            {
                var txt = $("#txtSmart" + obj.VariableId).val();
                if(txt == "" || txt == "null")
                {
                    $("#txtSmart" + obj.VariableId).addClass("requiredSFObj");
                    $("#txtSmart" + obj.VariableId).focus();
                    result =  false;
                }
            }
        }
    });
    return result;
}