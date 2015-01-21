$('.imgUploader').change(function () {
    var url = "a"
    var files = $(".imgUploader").get(0).files;
    if (files.length > 0) {
        var data = new FormData();
        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
        }
       // data.append("pid", "1");
      //  data.append("ItemID", "21");
        $.ajax({
            type: "POST",
            url: "/designerAPI/Upload/fileupload/" +url,
            contentType: false,
            processData: false,
            data: data,
            success: function (messages) {
                for (i = 0; i < messages.length; i++) {
                    alert(messages[i]);
                    // save record service 

                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //alert("Error while invoking the Web API");
                alert(thrownError);
            }
        });
    }
});
$('.fontUploader').change(function () {
    var url = "a"
    var files = $(".fontUploader").get(0).files;
    if (files.length > 0 && files.length < 3) {
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
                url: "/designerAPI/Upload/fileupload/" + url,
                contentType: false,
                processData: false,
                data: data,
                success: function (messages) {
                    for (i = 0; i < messages.length; i++) {
                        alert(messages[i]);
                        // save record service 

                    }
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