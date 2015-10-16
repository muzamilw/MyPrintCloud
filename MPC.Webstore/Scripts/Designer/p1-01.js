$(" #IdUploadBackgrounds").click(function () {
    $("#imageUploader").click();
    isBKpnl = false;
   // $("#fontUploader").click();
    //animatedcollapse.toggle('textPropertPanel');
});
$("#uploadBackgroundMn").click(function () {
    $("#imageUploader").click();
    isBKpnl = true;
    // $("#fontUploader").click();
    //animatedcollapse.toggle('textPropertPanel');
});
$("#uploadImages, #uploadLogos,#uploadImagesMB").click(function (event) {
    isBKpnl = false; isBkPnlUploads = false;
        $("#imageUploader").click();
    });
$(".btnAUploadFont").click(function () {
    $("#fontUploader").click();
});
         

$('#imageUploader').change(function () {
    var uploadPath = "Organisation" + organisationId + "/Templates/";
    if (IsCalledFrom == "1" || IsCalledFrom == "2")
    {
        uploadPath = "Organisation" + organisationId + "/Templates/" + "UserImgs/" + CustomerID;
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
        StartLoader();
        var data = new FormData();
        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
        }
        $.ajax({
            type: "POST",
            url: "/designerapi/Upload/PostAsync/" + url,
            contentType: false,
            processData: false,
            data: data,
            success: function (messages) {
                for (i = 0; i < messages.length; i++) {
                    var panelType = 1;
                    if (isBkPnlUploads) {
                        panelType = 3;
                    }
                    var contactIDlocal = ContactID;
                    if (IsCalledFrom == 2)
                        contactIDlocal = CustomerID;
                    $.getJSON("/designerapi/TemplateBackgroundImage/UploadImageRecord/" + messages[i] + "/" + tID + "/" + IsCalledFrom + "/" + contactIDlocal + "/" + organisationId + "/" + panelType + "/" + CustomerID,
                        function (res) {
                            LiImgs.push(res); 
                            var result = res.BackgroundImageAbsolutePath;
                            if (result != "uploadedPDFBK") {
                                imToLoad = res.BackgroundImageAbsolutePath;
                                $("#progressbar").css("display", "none");
                                $(".imageEditScreenContainer").css("display", "block");
                                if (parseInt(result)) {
                                    k26(result, "");
                                } else {
                                    pcL36("show", "#divImageDAM");
                                }
                                k27();
                                isImgUpl = true;
                                StopLoader(); $("#" + res.BackgroundImageAbsolutePath).parent().parent().click();
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
    }else 
    {
       
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
                          UpdateFontToUI(fontDisplayName, fontName);
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
            alert("Please enter valid font files."); StopLoader();
        }
    } else 
    {
        alert("Only 3 font files allowed per font at a time."); StopLoader();
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
    var path = "/MPC_Content/Designer/Organisation" + organisationId + "/WebFonts/" + CustomerID + "/"
    if (IsCalledFrom == 1) {
        Tc1 = -1;
    }
   // T0FN = [];
   // T0FU = [];
   // h8(fontName, path + fontFileName, "");
 
  //  h9_newFont();
    var html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".eot" + '); src: url(' + path + fontFileName + ".eot?#iefix" + ') format(" embedded-opentype"), url(' + path + fontFileName + ".woff" + ') format("woff"),  url(' + path + fontFileName + ".ttf" + ') format("truetype");  font-weight: normal; font-style: normal;}</style>';
    $('head').append(html);

    if ($.browser.msie) {
        $("head").append('<link rel="stylesheet" href="' + (path +fontFileName + ".woff") + '">');
    } else if ($.browser.Chrome) {
        $("head").append('<link rel="stylesheet" href="' + (path  + fontFileName + ".woff") + '">');
    } else if ($.browser.Safari || $.browser.opera || $.browser.mozilla) {
        $("head").append('<link rel="stylesheet" href="' + (path  + fontFileName + ".ttf") + '">');
    } else {
        $("head").append('<link rel="stylesheet" href="' + (path  + fontFileName + ".eot") + '">');
        $("head").append('<link rel="stylesheet" href="' + (path  + fontFileName + ".woff") + '">');
        $("head").append('<link rel="stylesheet" href="' + (path  + fontFileName + ".ttf") + '">');
    }
   
    var html1 = '<option  id = ' + fontFileName + ' value="' + fontName + '" >' + fontName + '</option>';
    console.log(fontName + " " + fontFileName);
    var fname = "'"+ fontName + "'";
    $('#' + "BtnSelectFonts").append(html1);
    var html2 = '<li style="font-family: ' + fname + '">' + fontName + '</li>';
    $(".fonts").append(html2);
    var fname = 'BtnSelectFontsRetail';
    if (panelMode == 1) {
        fname = 'BtnSelectFonts';
    }
    var selName = "#" + fname;
    $(selName).fontSelector({

        fontChange: function (e, ui) {
            // Update page title according to the font that's set in the widget options:
            //pcL04(1);
        },
        styleChange: function (e, ui) {
            // Update page title according to what's set in the widget options:
            // pcL04(1);
        }
    });
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
      Template = DT;
      TP = [];
      $.each(Template.TemplatePages, function (i, IT) {
          TP.push(IT);
      });
      Template.TemplatePages = [];
      CzRnd = fabric.util.getRandomInt(1, 100);
      d5(SP, true);
      StopLoader();
  });
    //fu04
}
