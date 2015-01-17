$(document).ready(function () {
   $("#content").css("display", "block");
   $("#content").stop().animate({
       opacity: 1
   },1500)
 

    $("#content").addClass("on");
   // $("#MainLoader").css("display", "none");
    StartLoader();
    fu02UI();
    fu02();
  //  stopLoader();
    
   
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

$("#canvaDocument").scroll(function () {

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
