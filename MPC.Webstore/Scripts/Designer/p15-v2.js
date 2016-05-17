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
             var res = result.TemplateDimensionConvertionRatio.split("__");
             w = w * res[0];
             h = h * res[0];
             $(".dimentionsBC").html("Trim size -" + " " + w + " *  " + h + " " + res[1]);
             productDimensionUpdated = true;
             item = result;
             if (IsCalledFrom != 2) {
                 if (result.IsTemplateDesignMode == 3) {
                     objectsSelectable = false;
                 }
                 if (item.SmartFormId != null) {

                     if (item.SmartFormId != 0) {
                         $(".QuickTxt").css("visibility", "hidden");
                         $.getJSON("/designerapi/SmartForm/GetSmartFormData/" + ContactID + "/" + item.SmartFormId + "/" + item.ParentTemplateId + "/" + tID,
                           function (DT2) {
                               $(".QuickTxt").css("visibility", "visible");
                               pcl41(DT2);
                               lstVariableExtensions = DT2.variableExtensions;
                               smartFormClicked = false;
                               fu04_TempCbkGen(DT);
                               if (Template.contactId != null && Template.contactId != ContactID) {
                                   $("#smartFormSelectUserProfile").val( Template.contactId);
                                   $("#optionRadioOtherProfile").click();
                               }
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
    if (IsCalledFrom == 2) { // load all async calls here 
        k28();
    }
    fu14();
    fu06(false);
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
                      if (item.companyImageHeight != 0 && item.companyImageWidth != 0) {
                          var obj = {
                              BackgroundImageRelativePath: item.userImage,
                              ImageName: item.userImage,
                              Name: item.userImage,
                              ImageWidth: item.companyImageWidth,
                              ImageHeight: item.companyImageHeight
                          }
                          LiImgs.push(obj);
                      }
                      
                  } else if (IT.ObjectType == 12) {
                      if (item.userImage != "") {
                          IT.ContentString = item.userImage;
                          IT.hasClippingPath = item.isProfileImageClippingPath;
                      }
                      if (item.contactImageHeight != 0 && item.contactImageWidth != 0)
                      {
                          var obj = {
                              BackgroundImageRelativePath: item.userImage,
                              ImageName: item.userImage,
                              Name: item.userImage,
                              ImageWidth: item.contactImageWidth,
                              ImageHeight: item.contactImageHeight
                          }
                          LiImgs.push(obj);
                      }
                  }
              }
          });
          pcl42_updateTemplate(DT);
          TO = DT;
          if(smartFormData != null)
              pcl42_UpdateTO(true);
          fu07();
          h9();
          // if (firstLoad) {
        //  fu05();
          //   }
          $.each(TO, function (i, IT) {
              var obj = fabric.util.object.clone(IT);
              TORestore.push(obj);
          });

          if (Template.realEstateId != null && Template.realEstateId > 0) {
              $.getJSON("/designerapi/TemplateBackgroundImage/getPropertyImages/" + Template.realEstateId,
              function (xdata) {
                  propertyImages = xdata;
                  $(".realEstateImgBtn").css("display", "block");
                  $.each(propertyImages, function (j, IT) {
                      var url = IT.ImageUrl;

                      var title = "LstImg" + IT.ImageId;
                      var draggable = '';
                      var urlThumbnail = url;
                      var ahtml = '<li class="DivCarouselImgContainerStyle2"><a href="#">' + '<img  src="' + url +
                                       '" class="svg imgCarouselDiv ' + draggable + '" style="z-index:1000;" id = "' + title + '" alt="' + url + '"></a><p class="bkFileName">' + title + '</p></li>';

                      $("#divRealEstateImagesContainer").append(ahtml);
                      $("#" + title).click(function (event) {
                          var n = url;
                          while (n.indexOf('/') != -1)
                              n = n.replace("/", "___");
                          while (n.indexOf(':') != -1)
                              n = n.replace(":", "@@");
                          while (n.indexOf('%20') != -1)
                              n = n.replace("%20", " ");
                          while (n.indexOf('./') != -1)
                              n = n.replace("./", "");
                          StartLoader("Placing image on canvas");
                          var imgtype = 2;
                          if (isBKpnl) {
                              imgtype = 4;
                          }
                          $.getJSON("/designerapi/TemplateBackgroundImage/DownloadImageLocally/" + n + "/" + tID + "/" + imgtype + "/" + organisationId,
                         function (DT) {
                             StopLoader();
                             k27();
                             parts = DT.split("MPC_Content/");
                             var imgName = parts[parts.length - 1];
                             while (imgName.indexOf('%20') != -1)
                                 imgName = imgName.replace("%20", " ");

                             var path = imgName;
                             j9(event, path, title);
                         });
                      });
                  });
                  $.each(TO, function (i, objTO) {
                      $.each(propertyImages, function (j, IT) {
                          if (objTO.ContentString.indexOf("{{ListingImage" + (j + 1) + "}}") != -1) {
                              var n = IT.ImageUrl;
                              while (n.indexOf('/') != -1)
                                  n = n.replace("/", "___");
                              while (n.indexOf(':') != -1)
                                  n = n.replace(":", "@@");
                              while (n.indexOf('%20') != -1)
                                  n = n.replace("%20", " ");
                              while (n.indexOf('./') != -1)
                                  n = n.replace("./", "");

                              var imgtype = 2;
                              $.getJSON("/designerapi/TemplateBackgroundImage/DownloadImageLocally/" + n + "/" + tID + "/" + imgtype + "/" + organisationId,
                             function (DT) {
                                 parts = DT.split("MPC_Content/");
                                 var imgName = parts[parts.length - 1];
                                 while (imgName.indexOf('%20') != -1)
                                     imgName = imgName.replace("%20", " ");

                                 var path = imgName;
                                 objTO.ContentString = path;
                                 objTO.originalContentString = path;
                             });
                          }
                      });
                  });
              });
          }

          //dam loader

         
          if (damEnabled == 'True' || damEnabled == 'True#')
          {
             
              $(".damImgDV").css("display", "block");
              damFolders.push(0);
              fudm('0',0,true);
          }
          else
          {

            }
      });
    k0();

}
function fu05_Clload() {
    var Cid = 0;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        Cid = CustomerID;
    }
    $.getJSON("/designerapi/TemplateColorStyles/GetColorStyle/" + tID + "/" + Cid + "/" + userColorTerritoryId,
       function (DT) {
           fu05_svcCall(DT);
       });
}


function fudm(searchText,ParentFolderId,pload)
{
    $("#divDAMImagesContainer").empty();
    $.getJSON("/designerapi/DamImage/GetDAMImages/" + searchText + "/" + CustomerID + "/" + organisationId + "/" + userTerritoryId + "/" + ParentFolderId,
                  function (xdata) {
                      damImages = xdata;

                      $.each(damImages.Folders, function (j, IT) {
                          var url = IT.ImagePath;
                         
                          var title = IT.FolderName;
                          var draggable = '';
                          var urlThumbnail = url;
                          var ahtml = '<li class="DivCarouselImgContainerStyle2"><a href="#">' + '<img  src="' + url +
                                           '" class="svg imgCarouselDiv ' + draggable + '" style="z-index:1000;" data-FolderId="' + IT.FolderId + '" id = "f' + IT.FolderId + '" alt="' + url + '"></a><p class="bkFileName">' + title + '</p></li>';

                         

                          $("#divDAMImagesContainer").append(ahtml);
                          $("#f" + IT.FolderId).click(function (event) {
                              //var n = url;
                              //while (n.indexOf('/') != -1)
                              //    n = n.replace("/", "___");
                              //while (n.indexOf(':') != -1)
                              //    n = n.replace(":", "@@");
                              //while (n.indexOf('%20') != -1)
                              //    n = n.replace("%20", " ");
                              //while (n.indexOf('./') != -1)
                              //    n = n.replace("./", "");
                            
                              var FolderId = this.getAttribute("data-FolderId");
                              StartLoader("Loading");
                              //alert(FolderId);
                              damFolders.push(FolderId);
                             
                              fudm('0', FolderId, false);
                              StopLoader();
                             // var imgtype = 2;
                             // if (isBKpnl) {
                             //     imgtype = 4;
                             // }
                             // $.getJSON("/designerapi/TemplateBackgroundImage/DownloadImageLocally/" + n + "/" + tID + "/" + imgtype + "/" + organisationId,
                             //function (DT) {
                             //    StopLoader();
                             //    k27();
                             //    parts = DT.split("MPC_Content/");
                             //    var imgName = parts[parts.length - 1];
                             //    while (imgName.indexOf('%20') != -1)
                             //        imgName = imgName.replace("%20", " ");

                             //    var path = imgName;
                             //    j9(event, path, title);
                             //});
                          });
                      });
                      $.each(damImages.Assets, function (j, IT) {
                          var url = IT.ImagePath;

                          var title = IT.AssetName;
                          var draggable = '';
                          var urlThumbnail = url;
                          var ahtml = '<li class="DivCarouselImgContainerStyle2"><a href="#">' + '<img  src="' + url +
                                           '" class="svg imgCarouselDiv ' + draggable + '" style="z-index:1000;" id = "as' + IT.AssetId + '" alt="' + url + '"></a><p class="bkFileName">' + title + '</p></li>';

                          $("#divDAMImagesContainer").append(ahtml);
                          $("#as" + IT.AssetId).click(function (event) {

                              //alert(this);
                              var n = url;
                              while (n.indexOf('/') != -1)
                                  n = n.replace("/", "___");
                              while (n.indexOf(':') != -1)
                                  n = n.replace(":", "@@");
                              while (n.indexOf('%20') != -1)
                                  n = n.replace("%20", " ");
                              while (n.indexOf('./') != -1)
                                  n = n.replace("./", "");
                              StartLoader("Placing image on canvas");
                              var imgtype = 2;
                              if (isBKpnl) {
                                  imgtype = 4;
                              }
                            
                                 StopLoader();
                                 k27();
                                 parts = IT.ImagePath.split("/mpc_content/");
                                 var imgName = parts[parts.length - 1];
                                 while (imgName.indexOf('%20') != -1)
                                     imgName = imgName.replace("%20", " ");

                                 var path = imgName;
                                 j9(event, path, "as" + IT.AssetId);
                             
                          });
                      });

                      if ( damImages.Assets.length == 0 &&  damImages.Folders.length == 0)
                      {
                          $(".divDAMImagesContainer").append("<p class='allImgsLoadedMessage'>No assets matches your search criteria. </p>");
                       }

                     
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
    globalTemplateId = gtID;
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
    $.getJSON("/designerapi/TemplateFonts/GetFontsList/" + tID + "/" + CustomerID + "/" + organisationId + "/" + userTerritoryId,
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
    $.getJSON("/designerapi/SmartForm/GetVariablesList/" + ItemId + "/" + CustomerID + "/" + organisationId,
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
         TPRestore = [];
         $.each(Template.TemplatePages, function (i, IT) {
             TP.push(IT);
         });
         $.each(TP, function (i, IT) {
             var obj = fabric.util.object.clone(IT);
             TPRestore.push(obj);
         });
         $.getJSON("/designerapi/TemplateObject/GetTemplateObjects/" + tID,
        function (DT) {
            $.each(DT, function (i, IT) {
                IT.ProductID = IT.ProductId;
                IT.ObjectID = IT.ObjectId;
                IT.ProductPageId = IT.ProductPageId;
            }); 
            TO = DT;
            TORestore = [];
            $.each(TO, function (i, IT) {
                var obj = fabric.util.object.clone(IT);
                TORestore.push(obj);
            });
            fu06(true);
            fu07(true);
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
            if (xdata.length > 1)
                k29("dropDownTerritories", "ter_all" , "All", "territroyContainer");
            $.each(xdata, function (i, item) {
                k29("dropDownTerritories", "ter_" + item.TerritoryId, item.TerritoryName, "territroyContainer");
            });
            $("#ter_all").click(function (event) {
                $('#dropDownTerritories  div :input').each(function (i) {
                    $(this).prop('checked', true);
                });
            });
        });
}
function k29(divID, itemID, itemName, Container) {
    var html = '<div class="checkboxRowsTxt"><input type="checkbox" id="' + itemID + '" class="' + itemID + '" style="  margin-right: 5px;"><label for="' + itemID + '">' + itemName + '</label></div>';
    $('#' + divID).append(html);
    $('#' + Container).css("display", "block");
}
function k30_userData(id) {
    StartLoader("Loading user profile on smart form....");
    $.getJSON("/designerapi/SmartForm/GetUserVariableSmartForm/" + id + "/" + item.SmartFormId + "/" + item.ParentTemplateId + "/" + tID,
                          function (DT2) {
                              pcl40_InsertUserData(DT2);
                              selectedUserProfile = DT2;
                              StopLoader();
                          });
}