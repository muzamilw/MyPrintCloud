$("#uploadImagesMB ,#IdUploadBackgrounds,#uploadBackgroundMn").click(function () {
     $("#imageUploader").click();
   // $("#fontUploader").click();
    //animatedcollapse.toggle('textPropertPanel');
});
$("#uploadImages, #uploadLogos").click(function (event) {
        isBKpnl = false;
        $("#imageUploader").click();
    });
$(".btnAUploadFont").click(function () {
    $("#fontUploader").click();
});
         

$('#imageUploader').change(function () {
    StartLoader();
    var uploadPath = "Organisation" + organisationId + "/Templates/";
    if (IsCalledFrom == "1" || IsCalledFrom == "2")
    {
        uploadPath = "Organisation" + organisationId + "/Templates/" + "UserImgs/" + ContactID;
    }
    else if (IsCalledFrom == "3" || IsCalledFrom == "4")
    {
        uploadPath = "Organisation" + organisationId + "/Templates/" + "UserImgs/Retail/" + ContactID;
    }
    else
    {
        uploadPath += "/" + tID;
    }
    var url = "Designer/" + uploadPath;
    while (url.indexOf('/') != -1)
        url = url.replace("/", "__");
    
    var files = $("#imageUploader").get(0).files;
    if (files.length > 0) {
        var data = new FormData();
        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
        }
       // data.append("pid", "1");
      //  data.append("ItemID", "21");
        $.ajax({
            type: "POST",
            url: "/designerapi/Upload/PostAsync/" + url,
            contentType: false,
            processData: false,
            data: data,
            success: function (messages) {
                for (i = 0; i < messages.length; i++) {
                  //  alert(messages[i]);
                    var panelType = 1;
                    if (isBkPnlUploads) {
                        panelType = 3;
                    }
                    $.getJSON("/designerapi/TemplateBackgroundImage/UploadImageRecord/" + messages[i] + "/" + tID + "/" + IsCalledFrom + "/" + ContactID + "/"+organisationId + "/" + panelType  + "/" + CustomerID ,
                        function (result) {
                            if (result != "uploadedPDFBK") {
                                $("#progressbar").css("display", "none");
                                $(".imageEditScreenContainer").css("display", "block");
                                if (parseInt(result)) {
                                    k26(result, "");
                                } else {
                                    pcL36("show", "#divImageDAM");
                                }
                                k27();
                                isImgUpl = true;
                                if (IsCalledFrom == 1 || IsCalledFrom == 2) {
                                    $("#ImgCarouselDiv").tabs("option", "active", 0);
                                    $("#BkImgContainer").tabs("option", "active", 0);
                                    $('#divGlobalImages').scrollTop();
                                    $('#divGlobalBackg').scrollTop();
                                } else {
                                    $("#ImgCarouselDiv").tabs("option", "active", 2);
                                    $("#BkImgContainer").tabs("option", "active", 2);
                                    $('#divPersonalImages').scrollTop();
                                    $('#divPersonalBkg').scrollTop();
                                }
                                StopLoader();
                            } else {
                                Arc_1();
                            }
                        });
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //alert("Error while invoking the Web API");
                alert(thrownError);
            }
        });
    }
});
$('#fontUploader').change(function () {
    var fontDisplayName = window.prompt("Enter Font name Here! (Once a font is created, you cannot change its name )", "Font name");
    if (fontDisplayName == null || fontDisplayName == "") {
        return false;
    }
    StartLoader();
    var url = "Designer/" + "Organisation" + organisationId + "/WebFonts/" + CustomerID;

    while (url.indexOf('/') != -1)
        url = url.replace("/", "__");
    var files = $("#fontUploader").get(0).files;
    if (files.length > 0 && files.length < 4) {
        var name1 = files[0].name.replace(/C:\\fakepath\\/i, '');
        var name2 = files[1].name.replace(/C:\\fakepath\\/i, '');
        var name3 = files[2].name.replace(/C:\\fakepath\\/i, ''); 
        if (VarifyFontNames(name1, name2, name3)) {
            var data = new FormData();
            for (i = 0; i < files.length; i++) {
                data.append("file" + i, files[i]);
            }
            // data.append("pid", "1");
            //  data.append("ItemID", "21");
            $.ajax({
                type: "POST",
                url: "/designerapi/Upload/PostAsync/" + url,
                contentType: false,
                processData: false,
                data: data,
                success: function (messages) {
                    var ext1 = messages[0].substr(messages[0].lastIndexOf('.') + 1);
                    var fontfile = messages[0];
                    fontfile = fontfile.replace('.' + ext1, '')
                    $.get("/designerapi/TemplateFonts/uploadFontRecord/" + CustomerID + "/" + organisationId + "/" + fontfile + "/" + fontDisplayName,
                      function (DT) {
                          var ext1 = messages[0].substr(messages[0].lastIndexOf('.') + 1);
                          var fontName = messages[0];
                          fontName = fontName.replace('.' + ext1, '')
                          UpdateFontToUI($("input[name=FontName]").val(), fontName);
                          StopLoader();
                      });
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //alert("Error while invoking the Web API");
                    alert(thrownError);
                }
            });
        } else 
        {
            alert("Please enter valid font files.");
        }
    } else 
    {
        alert("Only 3 font files allowed per font at a time.");
    }
});

function VarifyFontNames(font1, font2, font3) {
    var ext1 = font1.substr(font1.lastIndexOf('.') + 1);
    var name1 = font1.replace('.' + ext1, '').toLowerCase();
    var ext2 = font2.substr(font2.lastIndexOf('.') + 1);
    var name2 = font2.replace('.' + ext2, '').toLowerCase();
    var ext3 = font3.substr(font3.lastIndexOf('.') + 1);
    var name3 = font3.replace('.' + ext3, '').toLowerCase();
    ext1 = ext1.toLowerCase();
    ext2 = ext2.toLowerCase();
    ext3 = ext3.toLowerCase();
    if (ext1 == "ttf") {
        if (ext2 == "eot") {
            if (ext3 == "woff") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        } else if (ext2 == "woff") {
            if (ext3 == "eot") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        }
    } else if (ext1 == "eot") {
        if (ext2 == "ttf") {
            if (ext3 == "woff") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        } else if (ext2 == "woff") {
            if (ext3 == "ttf") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        }
    } else if (ext1 == "woff") {
        if (ext2 == "ttf") {
            if (ext3 == "eot") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        } else if (ext2 == "eot") {
            if (ext3 == "ttf") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        }
    }
    return false;
}
function UpdateFontToUI(fontName, fontFileName) {
    var Tc1 = CustomerID;
    var Cty;
    var path = "/MPC_Content/Designer/Organisation" + organisationId + "/WebFonts/" + CustomerID
    if (IsCalledFrom == 1) {
        Tc1 = -1;
    }

    var html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".eot" + '); src: url(' + path + fontFileName + ".eot?#iefix" + ') format(" embedded-opentype"), url(' + path + fontFileName + ".woff" + ') format("woff"),  url(' + path + fontFileName + ".ttf" + ') format("truetype");  font-weight: normal; font-style: normal;}</style>';
    $('head').append(html);
    var html1 = '<option  id = ' + fontFileName + ' value="' + fontName + '" >' + fontName + '</option>';
    $('#' + "BtnSelectFonts").append(html1);
}
function Arc_1() {
    StartLoader("Updating template please wait...");
    $.getJSON("/designerapi/Template/GetTemplate/" + tID + "/" + cID + "/" + TempHMM + "/" + TempWMM + "/" + organisationId + "/" + ItemId,
   //$.getJSON("/designerapi/Template/GetTemplate/" + tID ,
  function (DT) {
      DT.ProductID = DT.ProductId;
      $.each(DT.TemplatePages, function (i, IT) {
          IT.ProductID = IT.ProductId;
          IT.ProductPageID = IT.ProductPageId;
      });
      StopLoader();
  });

}
