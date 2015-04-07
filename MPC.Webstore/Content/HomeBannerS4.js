/*Slider Bar*/
$(document).ready(function () {
    // adjusting the settings for s4 themes 
    $('.RPL').css('background-color', '#E3E3E3');
    //  $('.bodycolor').css('background-color', 'white');
    $('.RPL').css('width', '960px');
    //rotation speed and timer


    var current = 1;
    var currentslide = 2;
    var count = $('#slides ul li').length;

    $(".introDescription").html($("#slides ul li:nth-child(" + 1 + ") .sliderDescription").html());
    $(".introHeading").html($("#slides ul li:nth-child(" + 1 + ") .sliderTitle").html());
    $(".order-now").attr('href', $("#slides ul li:nth-child(" + 1 + ") .sliderRefrenceUrl").html());
    $(".video-link").attr('href', $("#slides ul li:nth-child(" + 1 + ") .sliderVideoUrl").html());

    var width = 514 * (count);

    $('#slides ul').css('width', width);

    if (typeof (slider_scroll_speed) == 'undefined') {
        var speed = 5000;
    }
    else {
        var speed = slider_scroll_speed;
    }

    $('#slider').css('visibility', 'visible');
    var run = setInterval('rotate()', speed);


    //grab the width and calculate left value
    var item_width = $('#slides li').outerWidth();
    var left_value = item_width * (-1);



    //move the last item before first item, just in case user click prev button
    $('#slides li:first').before($('#slides li:last'));

    //set the default item to the correct position
    $('#slides ul').css({ 'left': left_value });

    //if user clicked on prev button
    $('#prev').click(function () {
        $(".introDescription").animate({ opacity: 0.50 });
        $(".introHeading").animate({ opacity: 0.50 });
        $(".order-now").animate({ opacity: 0.50 });

        //get the right position           
        var left_indent = parseInt($('#slides ul').css('left')) + item_width;

        //slide the item           
        $('#slides ul').animate({ 'left': left_indent }, 1500, function () {

            //move the last item and put it as first item              
            $('#slides li:first').before($('#slides li:last'));

            //set the default item to correct position
            $('#slides ul').css({ 'left': left_value });

            $(".introDescription").html($("#slides ul li:nth-child(" + currentslide + ") .sliderDescription").html());
            $(".introHeading").html($("#slides ul li:nth-child(" + currentslide + ") .sliderTitle").html());
            $(".order-now").attr('href', $("#slides ul li:nth-child(" + currentslide + ") .sliderRefrenceUrl").html());
            $(".video-link").attr('href', $("#slides ul li:nth-child(" + currentslide + ") .sliderVideoUrl").html());

            $(".introDescription").css("opacity", "1");
            $(".introHeading").css("opacity", "1");
            $(".order-now").css("opacity", "1");
            //   $(".introDescription").animate({ opacity: 1 });
            //   $(".introHeading").animate({ opacity: 1 });
            //   $(".order-now").animate({ opacity: 1 });
        });



        //cancel the link behavior           
        return false;

    });


    //if user clicked on next button
    $('#next').click(function () {

        $(".introDescription").animate({ opacity: 0.50 });
        $(".introHeading").animate({ opacity: 0.50 });
        $(".order-now").animate({ opacity: 0.50 });

        //get the right position
        var left_indent = parseInt($('#slides ul').css('left')) - item_width;
        //console.log(left_indent);
        //slide the item
        $('#slides ul').animate({ 'left': left_indent }, 1500, function () {

            //move the first item and put it as last item
            $('#slides li:last').after($('#slides li:first'));

            //set the default item to correct position
            $('#slides ul').css({ 'left': left_value });

            $(".introDescription").html($("#slides ul li:nth-child(" + currentslide + ") .sliderDescription").html());
            $(".introHeading").html($("#slides ul li:nth-child(" + currentslide + ") .sliderTitle").html());
            $(".order-now").attr('href', $("#slides ul li:nth-child(" + currentslide + ") .sliderRefrenceUrl").html());
            $(".video-link").attr('href', $("#slides ul li:nth-child(" + currentslide + ") .sliderVideoUrl").html());

            $(".introDescription").animate({ opacity: 1 });
            $(".introHeading").animate({ opacity: 1 });
            $(".order-now").animate({ opacity: 1 });

        });

        //cancel the link behavior
        return false;

    });

    //if user clicked on next button
    $('#next-fader').click(function () {
        //console.log(current);
        //get the right position
        var left_indent = parseInt($('#slides ul').css('left')) - item_width;

        $('#slides ul').fadeOut(function () {
            //slide the item
            $('#slides ul').animate({ 'left': left_indent }, 200, function () {
                //console.log("123");
                //move the first item and put it as last item
                $('#slides li:last').after($('#slides li:first'));

                //set the default item to correct position
                $('#slides ul').css({ 'left': left_value });

            });

            if (current < count) {
                $('#doc_type_' + current).removeClass('blue-highlight');
                current++;
                $('#doc_type_' + current).addClass('blue-highlight');
            }
            else {
                $('#doc_type_' + current).removeClass('blue-highlight');
                current = 1;
                $('#doc_type_' + current).addClass('blue-highlight');
            }
            $('#slides ul').fadeIn();
        });
        //cancel the link behavior
        return false;

    });

    //if mouse hover, pause the auto rotation, otherwise rotate it
    $('#slides').hover(

            function () {
                clearInterval(run);
            },
            function () {
                run = setInterval('rotate()', speed);
            }
        );



});

//a simple function to click next link
//a timer will call this function, and the rotation will begin :) 
function rotate() {
    if ($('#next-fader').length > 0) {
        $('#next-fader').click();
    }
    else {
        $('#next').click();
    }
}


//fancy box
$(document).ready(function () {

//    jQuery.noConflict();

    $(".video-link").fancybox({
        'autoScale': true,
        'transitionIn': 'none',
        'transitionOut': 'none',
        'type': 'iframe',
        'width': 650,
        'height': 410
    });

});