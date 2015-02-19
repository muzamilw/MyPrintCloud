
var ppagepath;
var pclassOverlay;
var pdialogType;

$(document).ready(function () {

    $('a.dialog-box-button').live('click', function (event) {
        event.preventDefault();

        if ($('#dialog-box-overlay').length > 0) {
            $('#dialog-box-overlay').hide().remove();
        }

        $('#dialog-box-parent-container').fadeOut("slow", function () {
            if ($('#dialog-box-parent-container').length > 0) {
                $('#dialog-box-parent-container').remove();
            }
        });

        if ($.browser.msie && $.browser.version.substr(0, 1) < 7) {
            $('#dialog-box-parent-container').hide();
            if ($('#dialog-box-parent-container').length > 0) {
                $('#dialog-box-parent-container').remove();
            }
        }

        return false;
    });

    $('#popup-popper').click(function (event) {

        event.preventDefault();
        show_dialog('The dialog has semi-opaque box around it. This effect is achieved using pure CSS, without the use of images for background.<br /><br /> The content of the dialog box is fully opaque. I used jQuery fade-in effect when making dialog to pop up. The dialog positioned in the middle of viewable area.<br /><br />The dialog is a modal dialog. This is achieved using transparent overlay. Try to click on hyper links when dialog is visible.<br /><br />The dialog has fixed position (when IE 6, it has absolute position), therefore  it stays in the middle of the screen if user scrolls up and down or resizes the screen', 'Attention', 'opacity-00', 'warn');
        return false;
    });


    $(window).resize(function () {
        //if (!$('#dialog-box-parent-container').is(':hidden') && $('#dialog-box-parent-container').length > 0) {
        //    show_dialog(ppagepath,pclassOverlay,pdialogType);
        //}
    });


    $(window).scroll(function () {
        //if ($.browser.msie && $.browser.version.substr(0, 1) < 7) {
        //    if (!$('#dialog-box-parent-container').is(':hidden') && $('#dialog-box-parent-container').length > 0) {
        //        show_dialog(ppagepath, pclassOverlay, pdialogType);
        //    }
        //}
    });

});


function show_dialog(message, title, classOverlay, dialogType) {


    
    var docHeight = $(document).height();
    var winWidth = $(window).width();
    $('#dialog-box-overlay').css({ height: docHeight, width: winWidth }).show(); //puts overlay over the whole document, not just viewable area.

    if ($('#dialog-box-parent-container').length == 0) {
        var dialog_box_html = "<div id=\"dialog-box-parent-container\">";
        dialog_box_html += "<div id=\"dialog-box-transparent-background\" class=\"opacity-70 box-round-all-8px\"></div>";
        dialog_box_html += "<div id=\"dialog-box-header\" class=\"box-round-top-4px\">";
        dialog_box_html += "<h2 class=\"box-round-top-4px " + dialogType + "\"><span class=\"dialog-box-header-content\">" + title + "</span></h2>";
        dialog_box_html += "</div>";
        dialog_box_html += "<div id=\"dialog-box-content\" class=\"box-round-bottom-4px\">";
        dialog_box_html += "<div id=\"dialog-box-text\">" + message + "</div>";
        dialog_box_html += "<div class=\"dialog-box-buttonpane\">";
        dialog_box_html += "<a href=\"#\" class=\"dialog-box-button box-round-default\">Okay</a>";
        dialog_box_html += "</div>";
        dialog_box_html += "</div>";
        dialog_box_html += "</div>";
        $("body").append(dialog_box_html);
    }

    var dialog_box_html = "<div id=\"dialog-box-overlay\" class=\"" + classOverlay + "\"></div>";
    $("body").append(dialog_box_html);

    var maskHeight = $(document).height();
    var maskWidth = $(window).width();
    $('#dialog-box-overlay').css({ height: maskHeight, width: maskWidth }).show();

    if ($.browser.msie && $.browser.version.substr(0, 1) > 6) {
        $('#dialog-box-transparent-background').corner("round 10px");
    }

    $('#dialog-box-parent-container').center().fadeIn("fast");
    $('#dialog-box-text').html(message);
}

function show_dialog(pagepath, classOverlay, dialogType) {

    ppagepath = pagepath;
    pclassOverlay = classOverlay;
    pdialogType = dialogType;

    var docHeight = $(document).height();
    var winWidth = $(window).width();
    $('#dialog-box-overlay').css({ height: docHeight, width: winWidth }).show(); //puts overlay over the whole document, not just viewable area.

    if ($('#dialog-box-parent-container').length == 0) {
        var dialog_box_html = "<div id=\"dialog-box-parent-container\">";
        dialog_box_html += "<div id=\"dialog-box-transparent-background\" class=\"opacity-70 box-round-all-8px\"></div>";
        //dialog_box_html += "<div id=\"dialog-box-header\" class=\"box-round-top-4px\">";
        //dialog_box_html += "<h2 class=\"box-round-top-4px " + dialogType + "\"><span class=\"dialog-box-header-content\">" + title + "</span></h2>";
        //dialog_box_html += "</div>";

        dialog_box_html += "<div id=\"dialog-box-content\" class=\"box-round-bottom-4px box-round-top-4px \">";
        dialog_box_html += "<a id=\"tpClose\" href=\"#\" class=\"dialog-box-button box-round-default\"><img src=\"/images/close-icon.png\"/></a>";
        dialog_box_html += "<div id=\"dialog-box-text\"></div>";
//        dialog_box_html += "<div class=\"dialog-box-buttonpane\">";
        
//        dialog_box_html += "</div>";
        dialog_box_html += "</div>";
        dialog_box_html += "</div>";
        $("body").append(dialog_box_html);
    }

    var dialog_box_html = "<div id=\"dialog-box-overlay\" class=\"" + classOverlay + "\"></div>";
    $("body").append(dialog_box_html);

    var maskHeight = $(document).height();
    var maskWidth = $(window).width();
    $('#dialog-box-overlay').css({ height: maskHeight, width: maskWidth }).show();

    if ($.browser.msie && $.browser.version.substr(0, 1) > 6) {
        $('#dialog-box-transparent-background').corner("round 10px");
    }

    $('#dialog-box-parent-container').center().fadeIn("fast");
    //$('#dialog-box-text').html(message);

    var height = $('#dialog-box-content').height();
    var iframWidth = "100%";
    if ($(window).width() < 481) {
        if ($(window).width() < 340) {
            iframWidth = $(window).width();
            $('#dialog-box-content').css("padding-left", "0px");
            $('#dialog-box-content').css("width", $(window).width() + "px");
        } else {
            iframWidth = $(window).width();
            iframWidth = "335";
            iframWidth = iframWidth + "px";
            $('#dialog-box-content').css("padding-left", "10px");
            $('#dialog-box-content').css("width", "358px");
           
        }

        
       // $('#dialog-box-content').css("padding-left", "0px");
    } else {
        $('#dialog-box-content').css("width", "440px");
    }
    $('#dialog-box-text').html('<iframe id="ifrm" width="' + iframWidth + '" height="' + height + 'px" border="0" style="width:' + iframWidth + ';height:' + height + 'px;border: none; background-color: white;" class=""></iframe>')
    $('#ifrm').attr('src', pagepath);
}

$.fn.center = function (absolute) {
    return this.each(function () {
        var t = jQuery(this);

        pos = absolute ? 'absolute' : 'fixed';

        topPos = '50%';

        if ($.browser.msie && $.browser.version.substr(0, 1) < 7) {
            pos = 'absolute';
            topPos = jQuery(window).height() / 2 - t.height() / 2 + jQuery(window).scrollTop();
        }
        
        var Iframeleft = "50%";
        var IframMargnLeft = '-' + (t.outerWidth() / 2) + 'px';
        var iframMargnTop = '-' + (t.outerHeight() / 2) + 'px';
        if ($(window).width() < 481) {
           
            Iframeleft = (jQuery(window).width() - jQuery(window).width()) / 2;
            IframMargnLeft = "0px";
            iframMargnTop = "0px;"
        }
        

        if ($(window).width() < 961) {
            topPos = '6%';
            iframMargnTop = "0px;"
        }
        t.css({
            position: pos,
            left: Iframeleft,
            top: topPos,
            zIndex: '999'
        }).css({
            marginLeft: IframMargnLeft,
            marginTop: iframMargnTop
        });

        if (absolute) {
            t.css({
                //marginTop: parseInt(t.css('marginTop'), 10) + jQuery(window).scrollTop(),
                //marginLeft: parseInt(t.css('marginLeft'), 10) + jQuery(window).scrollLeft()
            });
        }

    });
};