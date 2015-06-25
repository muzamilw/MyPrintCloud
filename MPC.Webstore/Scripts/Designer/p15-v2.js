$(document).ready(function () {
    buildParams();
   $("#content").css("display", "block");
   $("#content").stop().animate({
       opacity: 1
   },1500)
   $("#content").addClass("on");
   if (IsCalledFrom == 3) {
       StartLoader("Select a design to start customizing");
   } else {
       StartLoader();
   }
    fu02UI();
    fu02();
});

$(window).resize(function () {
      var height = $(window).height() ;
    $('.scrollPane').slimscroll({
        height: height
    });
    $('.scrollPane2').slimscroll({
        height: $(window).height()
    });
    $('.resultLayoutsScroller').slimscroll({
        height: height
    }).bind('slimscroll', function (e, pos) {
        if (pos == "bottom") {
            fu09();
        }
    });
});
$(window).scroll(function () {
    canvas.calcOffset();
});

$("#canvasDocument").scroll(function () {

    canvas.calcOffset();
});
//$(window).load(function () {
//    downloadJSAtOnload("js/a12/aj9.js");
//    downloadJSAtOnload("js/a12/aj21-v2.js");
//    downloadJSAtOnload("js/a12/aj12.js");
//    downloadJSAtOnload("js/a12/aj1.js");
//});
$("body").click(function (event) {
    if (event.target.id == "") {
        //  animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
    } else if (event.target.id == "btnNewTxtPanel") {
        // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
    } else if (event.target.id == "btnImgPanel" || event.target.id == "btnBkImgPanel") {
        // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
    } else if (event.target.id == "bd-wrapper" || event.target.id == "canvasSection" || event.target.id == "CanvasContainer") {
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        pcL13();
        pcL36('hide', '#textPropertPanel ,#divPopupUpdateTxt ,#divVariableContainer ,#DivAdvanceColorPanel ,#DivColorPallet ,#ShapePropertyPanel ,#ImagePropertyPanel ,#UploadImage ,#quickText ,#divPositioningPanel ,#DivAlignObjs ,#DivToolTip ,#addText ,#addImage');

    }

});
function b3_1(caller) {
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        var catID = Template.ProductCategoryID;
        var svcURL = "services/layoutsvc/";
        if (IsCalledFrom == 3) {
            catID = cIDv2;
            svcURL = V2Url + "services/layoutsvc/";
        }
        $.getJSON(svcURL + catID,
        function (DT) {
            llData = DT;
            l4(caller);
        });
    }
}

function b8_svc(imageID, productID) {
    $.get("/designerapi/TemplateBackgroundImage/DeleteProductBackgroundImage/" + productID + "/" + imageID + "/" + organisationId,
        function (DT) {
            if (DT != "false") {
                $("#" + imageID).parent().parent().remove();
                i2(DT);
                StopLoader();
                $("#btnAdd").click();
            }
        });
}
function fu03() {
    $.getJSON(V2Url + "services/TemplateSvc/GetCategoryV2/" + cIDv2,
   function (DT) {
       TempHMM = DT.HeightRestriction;
       TempWMM = DT.WidthRestriction;
       fu04();
   });
}
function fu04_1GetItem(DT)
{
    
    $.getJSON("/designerapi/item/GetItem/" + ItemId + "/" + ContactID + "/" + organisationId,
         function (result) {
            
             //if (result.ZoomFactor > 1)
             //{
             //    var zf = parseInt(result.ZoomFactor);
             //    for(var i = 1; i<zf;i++)
             //    {
             //        D1CS = D1CS * D1SF;
             //        dfZ1l = D1CS;
             //    }
             //}

             
             //update dimestions 
             var w = DT.PDFTemplateWidth;
             var h = DT.PDFTemplateHeight;
             h = h / 96 * 72;
             w = w / 96 * 72;
             h = h / 2.834645669;
             w = w / 2.834645669;
             w = w.toFixed(3);
             h = h.toFixed(3);
             h = h - 10;
             w = w - 10;
             //if (result.ScaleFactor != null && result.ScaleFactor != 0) {
             //    w = w * result.ScaleFactor;
             //    h = h * result.ScaleFactor;
             //}
             var res = result.TemplateDimensionConvertionRatio.split("__");
             w = w * res[0];
             h = h * res[0];
             //alert();
             //document.getElementById("DivDimentions").innerHTML = "Product Size <br /><br /><br />" + w + " (w) *  " + h + " (h) mm";
             $(".dimentionsBC").html("Trim size -" + " " + w + " *  " + h + " " + res[1]);
             productDimensionUpdated = true;
           //
             item = result;
             if (IsCalledFrom != 2) {
                 if (result.IsTemplateDesignMode == 3) {
                     objectsSelectable = false;
                 }
                 if (item.SmartFormId != null) {

                     if (item.SmartFormId != 0) {
                         $(".QuickTxt").css("visibility", "hidden");
                         $.getJSON("/designerapi/SmartForm/GetSmartFormData/" + ContactID + "/" + item.SmartFormId + "/" + item.ParentTemplateId,
                           function (DT2) {
                               $(".QuickTxt").css("visibility", "visible");
                               pcl41(DT2);
                               lstVariableExtensions = DT2.variableExtensions;
                               smartFormClicked = false;
                               fu04_TempCbkGen(DT);
                           });
                     } else {
                         fu04_TempCbkGen(DT);
                     }

                 } else {
                     $(".QuickTxt").css("visibility", "hidden");
                     $.getJSON("/designerapi/SmartForm/GetUserVariableData/" + ItemId + "/" + ContactID,
                          function (dt25) {
                              userData = dt25.scopeVariables;
                              lstVariableExtensions = dt25.variableExtensions;
                            //  console.log(userData);
                              userVariableData = userData;
                              fu04_TempCbkGen(DT);
                              if (DT.IsCorporateEditable == false && IsCalledFrom == 4) {
                                  $("#collapseDesignerMenu").click();
                              }
                          });
                 }


                 if (item.allowPdfDownload == true) {
                     $(".previewBtnContainer").css("display", "block");
                     $(".PreviewerDownloadPDF").css("display", "block");
                 }
                 if (item.allowImageDownload == true) {
                     $(".PreviewerDownloadImg").css("display", "block");
                 }
                 if (item.printCropMarks == false) {
                     printCropMarks = false;
                 }
                 if (item.drawWaterMarkTxt == false) {
                     printWaterMarks = false;
                 }
             }
             if (item.isMultipagePDF == true) {
                 isMultiPageProduct = true;
             }
            
            
           
           
         });
}
function fu04_TempCbkGen(DT) {
    DT.ProductID = DT.ProductId;
    $.each(DT.TemplatePages, function (i, IT) {
        IT.ProductID = IT.ProductId;
        IT.ProductPageID = IT.ProductPageId;
    });
    fu04_callBack(DT);
    if (DT.IsCorporateEditable == false && IsCalledFrom == 4) {
        restrictControls();
    }
}
function fu04_1(DT) {
    if (IsCalledFrom == 2) {
        c4_RS();
        $(".QuickTxt").css("visibility", "hidden"); 
        $("#btnGoToLandingPage").css("visibility", "hidden");
        
        fu04_TempCbkGen(DT);
        fu04_1GetItem(DT);
    } else {
        fu04_1GetItem(DT);
    }
}
function fu04() {
    $.getJSON("/designerapi/Template/GetTemplate/" + tID + "/" + cID + "/" + TempHMM + "/" + TempWMM + "/" + organisationId + "/" + ItemId,
       //$.getJSON("/designerapi/Template/GetTemplate/" + tID ,
      function (DT) {
          fu04_1(DT);   
      });
}
function fu04_01() {
    $.getJSON("/designerapi/TemplateObject/GetTemplateObjects/" + tID,
      function (DT) {
          $.each(DT, function (i, IT) {
              IT.ProductID = IT.ProductId;
              IT.ObjectID = IT.ObjectId;
              IT.ProductPageId = IT.ProductPageId;
              if (item != null) {

                  if (IT.ObjectType == 8) {
                      if (item.companyImage != "") {
                          IT.ContentString = item.companyImage;
                      }
                  } else if (IT.ObjectType == 12) {
                      if (item.userImage != "") {
                          IT.ContentString = item.userImage;
                      }
                  }
              }
          });
          pcl42_updateTemplate(DT);
          TO = DT;
          if(smartFormData != null)
              pcl42_UpdateTO();
          fu07();
          fu06();
          // if (firstLoad) {
          fu05();
          //   }
         
      });
    k0();
    if (IsCalledFrom == 2) {
        k28();
    }
}
function fu05_Clload() {
    var Cid = 0;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        Cid = CustomerID;
    }
    $.getJSON("/designerapi/TemplateColorStyles/GetColorStyle/" + tID + "/" + Cid,
       function (DT) {
           fu05_svcCall(DT);
       });
}
function fu05() {

    //$(".QuickTextFields").html("");

    //$.getJSON("/designerapi/template/getQuickText/" + CustomerID + "/" + ContactID,
    //    function (xdata) {
    //        fu05_SvcCallback(xdata);
    //    });
}
function fu09() {
    if (tcAllcc) return;
    tcAllcc = true;

    startInlineLoader(1);
    $.getJSON(V2Url + "services/TemplateSvc/GetCatList/" + cIDv2 + "," + tcListCc + "," + 16,
 function (DT) {
     fu09_SvcCallBack(DT);

 });
}
function svcCall1(ca, gtID) {
    $.getJSON("/designerapi/Template/mergeTemplate/" + gtID + "/" + tID + "/" + organisationId,
          function (xdata) {
            //  console.log("call returned");
              SvcLoad2ndTemplate();
              if (item.SmartFormId != null) {

                  if (item.SmartFormId != 0) {
                      $("#Quick").click();
                  }
              }
          });
}
function svcCall2(n, tID, imgtype) {
    $.getJSON("/designerapi/TemplateBackgroundImage/DownloadImageLocally/" + n + "/" + tID + "/" + imgtype + "/" + organisationId,
    function (DT) {
        j9_21(DT);
    });
}
function svcCall3(imToLoad) {
    $.getJSON("/designerapi/TemplateBackgroundImage/getImage/" + imToLoad + "/" + organisationId,
      function (DT) {
          k26_Dt(DT);
      });
}

function svcCall4(n, tID, imgtype) {
    $.getJSON("/designerapi/TemplateBackgroundImage/DownloadImageLocally/" + n + "/" + tID + "/" + imgtype + "/" + organisationId,
        function (DT) {
            k32_load(DT);
        });
}
function svcCall4_img(n, tID, imgtype) {
   // n = "MPC_Content" + n;
    $.getJSON("/designerapi/TemplateBackgroundImage/DownloadImageLocally/" + n + "/" + tID + "/" + imgtype + "/" + organisationId,
        function (DT) {
            k35_load(DT);
        });
}
function fu06(mode) {
    //CustomerID = parent.CustomerID;
    //ContactID = parent.ContactID;
    var str = '<option value="">(select)</option>';
    var fname = 'BtnSelectFontsRetail';
    if (panelMode == 1) {
        fname = 'BtnSelectFonts';
    }
    $('#' + fname).html(str);
    $.getJSON("/designerapi/TemplateFonts/GetFontsList/" + tID + "/" + CustomerID + "/" + organisationId,
        function (DT) {
            fu06_SvcCallback(DT, fname,mode);
        });
}



function c4_RS() {
    
    $("#divVariableContainer").css("top", "135px");
    $("#divVariableContainer").css("left", ($(window).width() - $('#divVariableContainer').width() -20) + "px");
    $("#divVariableContainer").draggable({

        appendTo: "body",
        cursor: 'move'
    });
    $.getJSON("/designerapi/SmartForm/GetVariablesList/" + isRealestateproduct + "/" + CustomerID + "/" + organisationId,
        function (xdata) {
            pcl40(xdata);
        });
    $.getJSON("/designerapi/SmartForm/GetTemplateVariables/" + tID,
    function (xdata) {
        varList = xdata;
    });
}

function pcl42_svc(data, cId) {
 
    var to = "/designerApi/SmartForm/SaveUserVariables";
    var list = {
        contactId: cId,
        variables: data
    };
    var jsonObjects = JSON.stringify(list, null, 2);
    var options = {
        type: "POST",
        url: to,
        data: jsonObjects,
        contentType: "application/json",
        async: true,
        complete: function (httpresp, returnstatus) {
            if (returnstatus == "success") {
                if (httpresp.responseText == '"true"') {
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

function SvcLoad2ndTemplate() {
   
    $.getJSON("/designerapi/Template/GetTemplate/" + tID + "/" + cID + "/" + TempHMM + "/" + TempWMM + "/" + organisationId + "/" + ItemId,
     function (DT) {
         DT.ProductID = DT.ProductId;
         $.each(DT.TemplatePages, function (i, IT) {
             IT.ProductID = IT.ProductId;
             IT.ProductPageID = IT.ProductPageId;
         });
         Template = DT;
         tID = Template.ProductId;
         TP = [];
         $.each(Template.TemplatePages, function (i, IT) {
             TP.push(IT);
         });
         $.getJSON("/designerapi/TemplateObject/GetTemplateObjects/" + tID,
        function (DT) {
            $.each(DT, function (i, IT) {
                IT.ProductID = IT.ProductId;
                IT.ObjectID = IT.ObjectId;
                IT.ProductPageId = IT.ProductPageId;
            }); 
            TO = DT;
            fu06(true);
            fu07();
        });
         $(".additionalPages").css("visibility", "hidden");
         $.each(TP, function (i, IT) {
             $("#thumbPageSP" + IT.PageNo).css("visibility", "visible");
             $("#thumbPageSP" + IT.PageNo + " .thumb-content p").html(IT.PageName);
         }); 
     });


}

function k28() {

    $.getJSON("/designerapi/TemplateBackgroundImage/GetTerritories/" + CustomerID,
        function (xdata) {
            $.each(xdata, function (i, item) {
                k29("dropDownTerritories", "ter_" + item.TerritoryId, item.TerritoryName, "territroyContainer");
            });
        });
}
function k29(divID, itemID, itemName, Container) {
    var html = '<div class="checkboxRowsTxt"><input type="checkbox" id="' + itemID + '" class="' + itemID + '" style="  margin-right: 5px;"><label for="' + itemID + '">' + itemName + '</label></div>';
    $('#' + divID).append(html);
    $('#' + Container).css("display", "block");
}