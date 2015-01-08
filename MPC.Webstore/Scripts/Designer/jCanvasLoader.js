$(document).ready(function () {
    //$("p").text("The DOM is now loaded and can be manipulated.");
    //alert('he DOM is now loaded and can be manipulated');

    //{ tags: "cat", tagmode: "any", format: "json" }



    loadFonts();


    //loadTemplate(1278);



    $('#loadTemplate').click(function () {
        canvas.clear();
        //alert('Handler for .click() called.');
        loadTemplate($('#txtTemplateID').val());
    });


});


//{ TemplateID: TemplateID }


function loadFonts() {
    jQuery.cachedScript = function (url, options) {
        // allow user to set any option except for dataType, cache, and url  
        options = $.extend(options || {},        { dataType: "script", cache: true, url: url });
        // Use $.ajax() since it is more flexible than $.getScript  
        // Return the jqXHR object so we can chain callbacks  
        return jQuery.ajax(options);
    };
    // Usage
//    $.cachedScript("js/Fonts/Impact_400.font.js").done(function (script, textStatus) {
//        console.log(textStatus);

//    });

//    $.cachedScript("js/Fonts/Arial_400.font.js").done(function (script, textStatus) {
//        console.log(textStatus);

//    });

//    $.cachedScript("js/Fonts/Myriad_Web_Pro_400-Myriad_Web_Pro_700.font.js").done(function (script, textStatus) {
//        console.log(textStatus);

//    });

//    $.cachedScript("js/Fonts/Arial_400.font.js").done(function (script, textStatus) {
//        console.log(textStatus);

//    });





    loadTemplate(1230);
}



function loadTemplate(TemplateID) {

        
    $.getJSON("http://localhost/DesignerService/TemplateSvc/" + TemplateID,
 function (data) {

     //canvas.setHeight(data.PDFTemplateHeight);
     //canvas.setWidth(data.PDFTemplateWidth);

     loadTemplateObjects(TemplateID);

 });
}



function loadTemplateObjects(TemplateID) {

    $.getJSON("http://localhost/DesignerService/TemplateObjectsSvc/" + TemplateID,

        function (data) {

            $.each(data, function (i, item) {

                if (item.isSide2Object == false) {
                    if (item.ObjectType == 2) {
                        //alert(item.ContentString);

                        //delicious_500
                        AddTextObject(item.ObjectID, item.ContentString, item.PositionX, item.PositionY, item.FontName, item.FontSize, item.RotationAngle, item.ColorHex, item.MaxWidth, item.MaxHeight,item.DisplayOrderPdf);


                    }
                    else if (item.ObjectType == 3) {
                        AddImageObject(item.ObjectID, "../" + item.ContentString, item.PositionX, item.PositionY, item.RotationAngle, item.MaxWidth, item.MaxHeight,item.DisplayOrderPdf);
                    }
                }
            });

            jc.start('canvas_1');


            //alert(data.ObjectID);
            //            $.each(data.items, function (i, item) {

            //                $("#output").val = item.ObjectID;
            ////                $("<img/>").attr("src", item.media.m).appendTo("#images");
            ////                if (i == 3)
            ////                    return false;
            //            });
        });

    }


    function AddTextObject(ObjectId, contentString, positionX, positionY, fontFamily, fontSize, rotationAngle, fontColor, width, height, DisplayOrder) {
//        var textSample = new fabric.Text(contentString, {
//            left: positionX + width/2,
//            top: positionY + height/2,
//            fontFamily: fontFamily,

//            fontSize: fontSize,
//            angle: rotationAngle,
//            fill: fontColor,
//            scaleX: 1,
//            scaleY: 1
//        });
//        //alert('help');
//        textSample.scaleToWidth(width);
//        cCanvas.add(textSample);
        //        //updateComplexity();

        //alert('a');

        var fontx = fontSize + "px " + "Arial";
        //alert(fontx);
        jc.text(contentString, positionX, positionY).id(ObjectId)
                    .draggable()
                    .rotate(rotationAngle, 'center')
                    .level(DisplayOrder)
                    .font(fontx)
                    .color(fontColor); 
    }  // fontStyle:'bold',



    function AddImageObject(ObjectId, imagePath, positionX, positionY, rotationAngle, width, height,DisplayOrder) {
//        fabric.Image.fromURL(imagePath, function (image) {
//            image.set({
//                left: positionX + width/2,
//                top: positionY,
//                angle: rotationAngle
//            });
//            //image.scale(1).setCoords();
//            image.scaleToWidth(width);
//            cCanvas.add(image);
        //        });


        var img = new Image();
        img.src = imagePath;
        img.onload = function () {
            //jc.start(idCanvas);
            jc.image(img, positionX, positionY - height, width, height)
            .id(ObjectId)
            .draggable()
            .rotate(rotationAngle, 'center')
            .level(DisplayOrder);
            //jc.start(idCanvas); 
        }
       

        
    }   