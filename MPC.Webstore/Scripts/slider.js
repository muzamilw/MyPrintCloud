/**
* @author Alexander Farkas
* v. 1.22
*/
(function ($) {
    if (!document.defaultView || !document.defaultView.getComputedStyle) { // IE6-IE8
        var oldCurCSS = $.curCSS;
        $.curCSS = function (elem, name, force) {
            if (name === 'background-position') {
                name = 'backgroundPosition';
            }
            if (name !== 'backgroundPosition' || !elem.currentStyle || elem.currentStyle[name]) {
                return oldCurCSS.apply(this, arguments);
            }
            var style = elem.style;
            if (!force && style && style[name]) {
                return style[name];
            }
            return oldCurCSS(elem, 'backgroundPositionX', force) + ' ' + oldCurCSS(elem, 'backgroundPositionY', force);
        };
    }

    var oldAnim = $.fn.animate;
    $.fn.animate = function (prop) {
        if ('background-position' in prop) {
            prop.backgroundPosition = prop['background-position'];
            delete prop['background-position'];
        }
        if ('backgroundPosition' in prop) {
            prop.backgroundPosition = '(' + prop.backgroundPosition;
        }
        return oldAnim.apply(this, arguments);
    };

    function toArray(strg) {
        strg = strg.replace(/left|top/g, '0px');
        strg = strg.replace(/right|bottom/g, '100%');
        strg = strg.replace(/([0-9\.]+)(\s|\)|$)/g, "$1px$2");
        var res = strg.match(/(-?[0-9\.]+)(px|\%|em|pt)\s(-?[0-9\.]+)(px|\%|em|pt)/);
        return [parseFloat(res[1], 10), res[2], parseFloat(res[3], 10), res[4]];
    }

    $.fx.step.backgroundPosition = function (fx) {
        if (!fx.bgPosReady) {
            var start = $.curCSS(fx.elem, 'backgroundPosition');
            if (!start) {//FF2 no inline-style fallback
                start = '0px 0px';
            }

            start = toArray(start);
            fx.start = [start[0], start[2]];
            var end = toArray(fx.end);
            fx.end = [end[0], end[2]];

            fx.unit = [end[1], end[3]];
            fx.bgPosReady = true;
        }
        //return;
        var nowPosX = [];
        nowPosX[0] = ((fx.end[0] - fx.start[0]) * fx.pos) + fx.start[0] + fx.unit[0];
        nowPosX[1] = ((fx.end[1] - fx.start[1]) * fx.pos) + fx.start[1] + fx.unit[1];
        fx.elem.style.backgroundPosition = nowPosX[0] + ' ' + nowPosX[1];

    };
})(jQuery);


//settings
var timeout = 5; //timeout between animating  slides (in seconds)
var timebetweenslides = 500; //timeout between removing a slide and showing next one (in miliseconds)
var isAnimating = false;
var totalSlides = 1;

// For S3
var kimgMarginLeft = '2px';
var kimgLeft = '8%';

// For S2
//var kimgMarginLeft = '-482px';
//var kimgLeft = '50%';

var currentSlide = 1;
var currentBGpos = 50;
var lteie8 = false;
if ($.browser.msie) {
    var vers = Number(parseInt($.browser.version, 10));
    if (vers < 9) var lteie8 = true;
}

function nextSlide() {

    if (isAnimating == false) {
        if (currentSlide == totalSlides) {
            currentSlide = 1;
            removeSlide(totalSlides);
        } else {
            removeSlide(currentSlide);
            currentSlide++;
        }
        isAnimating = true;
    }
}
function prevSlide() {
    if (isAnimating == false) {
        if (currentSlide == 1) {
            currentSlide = totalSlides;
            removeSlideBackwards(1);
        } else {
            removeSlideBackwards(currentSlide);
            currentSlide--;
        }
        isAnimating = true;
    }
}

function setSelectedCandy(index) {

    var tempin = index * 2 - 1;
    //$("#slider-nav li a").removeClass("selected");
    //$("#slider-nav li:nth-child(" + tempin + ") a").addClass("selected");
    currentSlide = index;
    isAnimating = false;
}

function showSlide(index) {

    if (isAnimating == false) {
        if (index > currentSlide && (index - 1) == currentSlide) {
            isAnimating = true;
            removeSlide(index - 1);

        } else
            if (index < currentSlide && (index + 1) == currentSlide) {
                isAnimating = true;
                removeSlideBackwards(index + 1);
            } else
                if (index > currentSlide && (index - 1) != currentSlide) {
                    isAnimating = true;
                    if (currentSlide == 1 && index == totalSlides) {
                        removeSlideBackwards(1);
                    }
                }
                else
                    if (index < currentSlide && (index + 1) != currentSlide) {
                        isAnimating = true;
                        if (currentSlide == totalSlides) {
                            removeSlide(totalSlides);
                        }
                    }
    }
}

function insertSlide(index) {
    var thisSlide = "";
    if (index == 1) { thisSlide = "__1" }
    if (index == 2) { thisSlide = "__2" }
    if (index == 3) { thisSlide = "__3" }
    if (index == 4) { thisSlide = "__4" }
    if (index == 5) { thisSlide = "__5" }
    if (index == 6) { thisSlide = "__6" }

    $("#slider section." + thisSlide).fadeIn(200);

    var kimg = $("#slider section." + thisSlide + " img");
    var kh2 = $("#slider section." + thisSlide + " h4");
    var kcopy = $("#slider section." + thisSlide + " .copy");
    var kbutton = $("#slider section." + thisSlide + " .button");

    kh2.animate({ left: kimgLeft, opacity: 1 }, 1000, 'easeInOutExpo');
    kcopy.delay(200).animate({ left: kimgLeft, opacity: 1 }, 800, 'easeInOutExpo');

    kbutton.delay(1400).fadeIn(1200);
    if (!lteie8) {
        kimg.delay(200).animate({ left: kimgLeft, opacity: 1, marginLeft: kimgMarginLeft }, 900, 'easeInOutExpo', function () { setSelectedCandy(index) });
    } else {
        kimg.css("display", "block").delay(200).animate({ left: kimgLeft, marginLeft: kimgMarginLeft }, 900, 'easeInOutExpo', function () { setSelectedCandy(index) });
    }
}



function removeSlide(index) {
    var thisSlide = "";
    if (index == 1) { thisSlide = "__1" }
    if (index == 2) { thisSlide = "__2" }
    if (index == 3) { thisSlide = "__3" }
    if (index == 4) { thisSlide = "__4" }
    if (index == 5) { thisSlide = "__5" }
    if (index == 6) { thisSlide = "__6" }

    var kimg = $("#slider section." + thisSlide + " img");
    var kh2 = $("#slider section." + thisSlide + " h4");
    var kcopy = $("#slider section." + thisSlide + " .copy");
    var kbutton = $("#slider section." + thisSlide + " .button");
    //var ksection = $("#slider section");
    var ksection = $("#slider");
    if (timeout) clearTimeout(timeout);
    var timeout = setTimeout("resetSlide(" + Number(index + 1) + ")", timebetweenslides);
    kh2.stop(true, true).animate({ left: '-1%', opacity: 0 }, 1200, 'easeInOutBack');
    kcopy.stop(true, true).delay(300).animate({ left: '-1%', opacity: 0 }, 1200, 'easeInOutBack');
    kbutton.stop(true, true).fadeOut();
    if (!lteie8) {
        kimg.stop(true, true).delay(400).animate({ left: '-1%', opacity: 0 }, 1200, 'easeInOutExpo');
    } else {
        kimg.stop(true, true).delay(400).animate({ left: '-100%' }, 1200, 'easeInOutExpo', function () { kimg.hide(); });
    }
    ksection.stop(true, true).delay(700).css("backgroundPosition", currentBGpos + "% 0").animate({ backgroundPosition: currentBGpos - 25 + '% 0' }, 900, 'swing');
    currentBGpos -= 25;
}
function removeSlideBackwards(index) {
    var thisSlide = "";
    if (index == 1) { thisSlide = "__1" }
    if (index == 2) { thisSlide = "__2" }
    if (index == 3) { thisSlide = "__3" }
    if (index == 4) { thisSlide = "__4" }
    if (index == 5) { thisSlide = "__5" }
    if (index == 6) { thisSlide = "__6" }

    var kimg = $("#slider section." + thisSlide + " img");
    var kh2 = $("#slider section." + thisSlide + " h4");
    var kcopy = $("#slider section." + thisSlide + " .copy");
    var kbutton = $("#slider section." + thisSlide + " .button");
    //var ksection = $("#slider section");
    var ksection = $("#slider");

    if (timeout) clearTimeout(timeout);
    var timeout = setTimeout("resetSlideBackwards(" + Number(index - 1) + ")", timebetweenslides);

    kh2.stop(true, true).delay(400).animate({ left: '00%', opacity: 0 }, 800, 'easeInOutExpo');
    kcopy.stop(true, true).delay(200).animate({ left: '00%', opacity: 0 }, 700, 'easeInOutExpo');
    kbutton.stop(true, true).fadeOut();
    if (!lteie8) {
        kimg.stop(true, true).animate({ left: '00%', opacity: 0 }, 1200, 'easeInOutBack');
    } else {
        kimg.stop(true, true).animate({ left: '00%' }, 1200, 'easeInOutBack', function () { kimg.hide(); });
    }
    ksection.stop(true, true).delay(700).css("backgroundPosition", currentBGpos + "% 0").animate({ backgroundPosition: currentBGpos + 25 + '% 0' }, 900, 'swing');
    currentBGpos += 25;
}
function resetSlide(index) {
    if (index > totalSlides) index = 1;
    var thisSlide = "";
    if (index == 1) { thisSlide = "__1" }
    if (index == 2) { thisSlide = "__2" }
    if (index == 3) { thisSlide = "__3" }
    if (index == 4) { thisSlide = "__4" }
    if (index == 5) { thisSlide = "__5" }
    if (index == 6) { thisSlide = "__6" }

    //$("#slider section").css("z-index", '1').hide();
    //$("#slider section." + thisSlide).css("z-index", '2').show();

    var kimg = $("#slider section." + thisSlide + " img");
    var kh2 = $("#slider section." + thisSlide + " h4");
    var kcopy = $("#slider section." + thisSlide + " .copy");
    var kbutton = $("#slider section." + thisSlide + " .button");

    kh2.css({ "left": '100%' });
    kcopy.css({ "left": '100%' });
    kbutton.css({ "display": 'none' });
    kimg.css({ "left": '100%', "margin-left": '0' });

    insertSlide(index);
}
function resetSlideBackwards(index) {
    if (index < 1) index = totalSlides;
    var thisSlide = "";
    if (index == 1) { thisSlide = "__1" }
    if (index == 2) { thisSlide = "__2" }
    if (index == 3) { thisSlide = "__3" }
    if (index == 4) { thisSlide = "__4" }
    if (index == 5) { thisSlide = "__5" }
    if (index == 6) { thisSlide = "__6" }


    //$("#slider section").css("z-index", '1').hide();
    //$("#slider section." + thisSlide).css("z-index", '2').show();

    var kimg = $("#slider section." + thisSlide + " img");
    var kh2 = $("#slider section." + thisSlide + " h4");
    var kcopy = $("#slider section." + thisSlide + " .copy");
    var kbutton = $("#slider section." + thisSlide + " .button");

    kh2.css({ "left": '-1%' });
    kcopy.css({ "left": '-1%' });
    kbutton.css({ "display": 'none' });
    kimg.css({ "left": '-80%', "margin-left": -kimg.width() });

    insertSlide(index);
}

$(document).ready(function () {

    //animate slider
    var autoAnimation = window.setInterval("nextSlide()", timeout * 1000);
    $(".slider-container").hover(function () {
        window.clearInterval(autoAnimation);
        if (!lteie8) {
            $(".slider-wrapper .slider-prev").fadeIn();
            $(".slider-wrapper .slider-next").fadeIn();
        } else {
            $(".slider-wrapper .slider-prev").show();
            $(".slider-wrapper .slider-next").show();
        }
    }, function () {
        autoAnimation = window.setInterval("nextSlide()", timeout * 1000);
        if (!lteie8) {
            $(".slider-wrapper .slider-prev").stop(true, true).fadeOut();
            $(".slider-wrapper .slider-next").stop(true, true).fadeOut();
        } else {
            $(".slider-wrapper .slider-prev").hide();
            $(".slider-wrapper .slider-next").hide();
        }
    });
})

//Jquery Easings
eval(function (p, a, c, k, e, r) { e = function (c) { return (c < a ? '' : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36)) }; if (!''.replace(/^/, String)) { while (c--) r[e(c)] = k[c] || e(c); k = [function (e) { return r[e] } ]; e = function () { return '\\w+' }; c = 1 }; while (c--) if (k[c]) p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]); return p } ('h.i[\'1a\']=h.i[\'z\'];h.O(h.i,{y:\'D\',z:9(x,t,b,c,d){6 h.i[h.i.y](x,t,b,c,d)},17:9(x,t,b,c,d){6 c*(t/=d)*t+b},D:9(x,t,b,c,d){6-c*(t/=d)*(t-2)+b},13:9(x,t,b,c,d){e((t/=d/2)<1)6 c/2*t*t+b;6-c/2*((--t)*(t-2)-1)+b},X:9(x,t,b,c,d){6 c*(t/=d)*t*t+b},U:9(x,t,b,c,d){6 c*((t=t/d-1)*t*t+1)+b},R:9(x,t,b,c,d){e((t/=d/2)<1)6 c/2*t*t*t+b;6 c/2*((t-=2)*t*t+2)+b},N:9(x,t,b,c,d){6 c*(t/=d)*t*t*t+b},M:9(x,t,b,c,d){6-c*((t=t/d-1)*t*t*t-1)+b},L:9(x,t,b,c,d){e((t/=d/2)<1)6 c/2*t*t*t*t+b;6-c/2*((t-=2)*t*t*t-2)+b},K:9(x,t,b,c,d){6 c*(t/=d)*t*t*t*t+b},J:9(x,t,b,c,d){6 c*((t=t/d-1)*t*t*t*t+1)+b},I:9(x,t,b,c,d){e((t/=d/2)<1)6 c/2*t*t*t*t*t+b;6 c/2*((t-=2)*t*t*t*t+2)+b},G:9(x,t,b,c,d){6-c*8.C(t/d*(8.g/2))+c+b},15:9(x,t,b,c,d){6 c*8.n(t/d*(8.g/2))+b},12:9(x,t,b,c,d){6-c/2*(8.C(8.g*t/d)-1)+b},Z:9(x,t,b,c,d){6(t==0)?b:c*8.j(2,10*(t/d-1))+b},Y:9(x,t,b,c,d){6(t==d)?b+c:c*(-8.j(2,-10*t/d)+1)+b},W:9(x,t,b,c,d){e(t==0)6 b;e(t==d)6 b+c;e((t/=d/2)<1)6 c/2*8.j(2,10*(t-1))+b;6 c/2*(-8.j(2,-10*--t)+2)+b},V:9(x,t,b,c,d){6-c*(8.o(1-(t/=d)*t)-1)+b},S:9(x,t,b,c,d){6 c*8.o(1-(t=t/d-1)*t)+b},Q:9(x,t,b,c,d){e((t/=d/2)<1)6-c/2*(8.o(1-t*t)-1)+b;6 c/2*(8.o(1-(t-=2)*t)+1)+b},P:9(x,t,b,c,d){f s=1.l;f p=0;f a=c;e(t==0)6 b;e((t/=d)==1)6 b+c;e(!p)p=d*.3;e(a<8.w(c)){a=c;f s=p/4}m f s=p/(2*8.g)*8.r(c/a);6-(a*8.j(2,10*(t-=1))*8.n((t*d-s)*(2*8.g)/p))+b},H:9(x,t,b,c,d){f s=1.l;f p=0;f a=c;e(t==0)6 b;e((t/=d)==1)6 b+c;e(!p)p=d*.3;e(a<8.w(c)){a=c;f s=p/4}m f s=p/(2*8.g)*8.r(c/a);6 a*8.j(2,-10*t)*8.n((t*d-s)*(2*8.g)/p)+c+b},T:9(x,t,b,c,d){f s=1.l;f p=0;f a=c;e(t==0)6 b;e((t/=d/2)==2)6 b+c;e(!p)p=d*(.3*1.5);e(a<8.w(c)){a=c;f s=p/4}m f s=p/(2*8.g)*8.r(c/a);e(t<1)6-.5*(a*8.j(2,10*(t-=1))*8.n((t*d-s)*(2*8.g)/p))+b;6 a*8.j(2,-10*(t-=1))*8.n((t*d-s)*(2*8.g)/p)*.5+c+b},F:9(x,t,b,c,d,s){e(s==u)s=1.l;6 c*(t/=d)*t*((s+1)*t-s)+b},E:9(x,t,b,c,d,s){e(s==u)s=1.l;6 c*((t=t/d-1)*t*((s+1)*t+s)+1)+b},16:9(x,t,b,c,d,s){e(s==u)s=1.l;e((t/=d/2)<1)6 c/2*(t*t*(((s*=(1.B))+1)*t-s))+b;6 c/2*((t-=2)*t*(((s*=(1.B))+1)*t+s)+2)+b},A:9(x,t,b,c,d){6 c-h.i.v(x,d-t,0,c,d)+b},v:9(x,t,b,c,d){e((t/=d)<(1/2.k)){6 c*(7.q*t*t)+b}m e(t<(2/2.k)){6 c*(7.q*(t-=(1.5/2.k))*t+.k)+b}m e(t<(2.5/2.k)){6 c*(7.q*(t-=(2.14/2.k))*t+.11)+b}m{6 c*(7.q*(t-=(2.18/2.k))*t+.19)+b}},1b:9(x,t,b,c,d){e(t<d/2)6 h.i.A(x,t*2,0,c,d)*.5+b;6 h.i.v(x,t*2-d,0,c,d)*.5+c*.5+b}});', 62, 74, '||||||return||Math|function|||||if|var|PI|jQuery|easing|pow|75|70158|else|sin|sqrt||5625|asin|||undefined|easeOutBounce|abs||def|swing|easeInBounce|525|cos|easeOutQuad|easeOutBack|easeInBack|easeInSine|easeOutElastic|easeInOutQuint|easeOutQuint|easeInQuint|easeInOutQuart|easeOutQuart|easeInQuart|extend|easeInElastic|easeInOutCirc|easeInOutCubic|easeOutCirc|easeInOutElastic|easeOutCubic|easeInCirc|easeInOutExpo|easeInCubic|easeOutExpo|easeInExpo||9375|easeInOutSine|easeInOutQuad|25|easeOutSine|easeInOutBack|easeInQuad|625|984375|jswing|easeInOutBounce'.split('|'), 0, {}))