var IDofObject = "";
var IDOfObject2 = "";

$(function () {
    GetIDs();
});

function GetIDs() {



    PageMethods.GetIDs(OnGetIDSucceeded, OnFailed);
}

function OnGetIDSucceeded(result, userContext, methodName) {
    if (result != "") {
        var ids = result.split('|');
        IDofObject = ids[0];
        IDOfObject2 = ids[1];
        $("#uploader").pluploadQueue({
            runtimes: 'html5',
            url: 'UploadHandler.ashx',
            max_file_size: '10mb',
            chunk_size: '1mb',
            unique_names: true,
            headers: { "IDofObject1": IDofObject, "IDofObject2": IDOfObject2 },
            multipart_params: { pid: productID, ptype: PType, CustomerName: CustomerID, IsCalledFrom: IsCalledFrom },
            filters: [
               { title: "Files (.jpg, .gif, .png ,.pdf,.svg )", extensions: "jpg,gif,png,pdf,svg" }
            ],
            flash_swf_url: 'js/a12/a99.swf',
            multi_selection:false,
            silverlight_xap_url: 'js/a12/plupload.silverlight.xap'
        });
        $('#uploader_browse').text("Upload Image");
        var uploader = $('#uploader').pluploadQueue();
        uploader.bind('Init', function (up, res) {
            $('a.plupload_start').css('display', 'none');
            $('.plupload_header').css('display', 'none');
            $('.plupload_content').css('color', 'black');
        });
        uploader.bind('QueueChanged', function (up, files) {
            $('a.plupload_start').toggleClass('plupload_disabled', true);
            $('.plupload_buttons').css('display', 'block');
            $('#uploader_browse').attr('disabled', 'disabled');
            $("#uploadImgFileBtn1").click();
        });

        uploader.bind('UploadComplete', function (up, file, res) {
           
            GetIDs();
            $('#uploader_browse').removeAttr("disabled");
        });
        $("#FontUploader").pluploadQueue({
            runtimes: 'html5,flash',
            url: 'FontUploadHandler.ashx',
            max_file_size: '10mb',
            chunk_size: '1mb',
            unique_names: false,

            headers: { "IDofObject1": IDofObject, "IDofObject2": IDOfObject2 },
            multipart_params: { pid: productID, ptype: PType, CustomerName: CustomerID,IsCalledFrom: IsCalledFrom },
            filters: [
               { title: "Files (.ttf, .woff, .eot, )", extensions: "ttf,woff,eot" }
            ],
            flash_swf_url: 'js/a12/a99.swf',
            silverlight_xap_url: 'js/a12/plupload.silverlight.xap'
        });
        var uploader1 = $('#FontUploader').pluploadQueue();
        uploader1.bind('Init', function (up, res) {
            $('a.plupload_start').css('display', 'none');
            $('.plupload_header').css('display', 'none');
            $('.plupload_content').css('color', 'black');
        });
        uploader1.bind('QueueChanged', function (up, files) {
            $('a.plupload_start').toggleClass('plupload_disabled', true);
        });

        uploader1.bind('UploadComplete', function (up, file, res) {
            alert("Font uploaded sucessfully, it is now availble for use in fonts list.");
            GetIDs();
        });
    }
    else {
    }
}


function OnFailed(error, userContext, methodName) {
    if (error !== null) {
      //  alert("An error occurred: " + error.get_message());
    }
}

function SubmitItem(e) {
    var uploader = $('#uploader').pluploadQueue();
    var obj = uploader.settings;
    if (uploader.total.uploaded == 0) {
        if (uploader.files.length > 0) {
            uploader.bind('UploadProgress', function (up, file) {
                // console.log(file.percent);
                pcL36("show", "#divImageEditScreen");
                var progressbar = $("#progressbar");
                $("#progressbar").css("display", "block");
                $(".imageEditScreenContainer").css("display", "none");
              //  var val = "uploading image (" + file.percent + " complete) ";
                progressbar.progressbar("value", file.percent);
                if (uploader.total.uploaded == uploader.files.length)
                    $('form1').submit();

            });
            uploader.bind('UploadComplete', function (up, f) {
                var obj = up.settings;
                for (i = 0; i < f.length; i++) {
                    InsertFileRecord(obj.headers.IDofObject1, obj.headers.IDofObject2, f[i].id, f[i].name);
                }
            });

            uploader.start();
        }
        else {
            alert("No files to upload"); 
        }
    }
}

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
function SubmitFontItem(e) {
    var uploader = $('#FontUploader').pluploadQueue();
    var obj = uploader.settings;
    if (D1IFL == true) {
        if (uploader.total.uploaded == 0 && uploader.files.length == 3 && $("input[name=FontName]").val() != "") {
            if (VarifyFontNames(uploader.files[0].name, uploader.files[1].name, uploader.files[2].name)) {
                uploader.bind('UploadProgress', function () {
                    if (uploader.total.uploaded == uploader.files.length)
                        $('form2').submit();
                });
                uploader.bind('UploadComplete', function (up, f) {
                    var obj = up.settings;
                    var ext1 = f[0].name.substr(f[0].name.lastIndexOf('.') + 1);
                    var fontName = f[0].name;
                    fontName = fontName.replace('.' + ext1, '')
                    InsertFontRecord($("input[name=FontName]").val(), fontName);
                    UpdateFontToUI($("input[name=FontName]").val(), fontName);
                    $("input[name=FontName]").val("");
                });
                uploader.start();
            }
            else {
                alert("Please Upload the files correctly"); 
            }
        }
        else {
            alert("Files are not according to criteria");
        }
    } else {
        alert("Confirm that this font is free from any copyright infringements. ");
    }
}

function UpdateFontToUI(fontName, fontFileName) {
    var Tc1 = CustomerID;
    var Cty;
    var path = "Designer/PrivateFonts/FontFace/"
    if (IsCalledFrom == 1) {
        Tc1 = -1;
    }
    var R = d0("role");
    //alert(R);
    if (R != null)
    {
        if (R == "Designer")
        {
            Tc1 = -999;
        }
        else if (R == "Customer")
        {
            var Customer = d0("customerid"); //alert(Customer);
            Tc1 = parseInt(Customer);
            Cty = "Glo";
        }
    }
    if (Tc1 == -999 || Tc1 == null) {
        // mpc designers  
        path = "Designer/PrivateFonts/FontFace/";
    }
    else {
        if (Cty == "Glo") {
            path = "Designer/PrivateFonts/FontFace/Glo" + Tc1 + "/";
        }
        else {
            path = "Designer/PrivateFonts/FontFace/" + Tc1 + "/";
        }
    }
    //
    var html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".eot" + '); src: url(' + path + fontFileName + ".eot?#iefix" + ') format(" embedded-opentype"), url(' + path + fontFileName + ".woff" + ') format("woff"),  url(' + path + fontFileName + ".ttf" + ') format("truetype");  font-weight: normal; font-style: normal;}</style>';
    $('head').append(html);
    var html1 = '<option  id = ' + fontFileName + ' value="' + fontName + '" >' + fontName + '</option>';
    $('#' + "BtnSelectFonts").append(html1);
}
function InsertFontRecord(fontName, fileName) {

    var Tc1 = CustomerID;
    if (IsCalledFrom == 1) {
        Tc1 = -1;
    }
    PageMethods.InsertFontRecord(fontName, fileName,Tc1);
}
var Count = 0;
function InsertFileRecord(idOfObject1, idOfObject2, fileID, fileName) {
    idOfObject1 = getParameterByName("TemplateID");
    
    if (isBKpnl) {
        PageMethods.InsertFileRecord(idOfObject1, idOfObject2, fileID, fileName, TemplateID, IsCalledFrom, 3, CustomerID, ContactID, OnSucceedFileRecord, OnFailed); 
    } else {
        PageMethods.InsertFileRecord(idOfObject1, idOfObject2, fileID, fileName, TemplateID, IsCalledFrom, 1, CustomerID, ContactID, OnSucceedFileRecord, OnFailed); 
    }
    Count += 1;
}
function OnSucceedFileRecord(result, userContext, methodName) {
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
}
function Arc(angle, id) {
    var $elem = $('#' + id);
    $({ deg: 0 }).animate({ deg: angle }, {
        duration: 400,
        step: function (now) {
            $elem.css({
                transform: 'rotate(' + now + 'deg)'
            });
        }
    });
}