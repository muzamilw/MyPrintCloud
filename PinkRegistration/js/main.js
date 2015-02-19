// ==========
window.Main = {

    // ----------
    init: function () {
        // ___ zooming
        if ("iOS_isiPad" in window && iOS_isiPad())
            iOS_disableZooming();

        // ___ carousels
        var carousels = {
            interface: {
                imageName: "paperInterfaceCarousel",
                count: 4
            },
            sharingCarousel: {
                imageName: "tumblrTheme",
                count: 4
            }
        };

        $.each(carousels, function (name, carousel) {
            carousel.name = name;
            carousel.$el = $("#" + carousel.name);
            if (carousel.$el.length)
                carousel.widget = new Carousel(carousel);
        });

        // ___ tools
        var $examples = $("#toolsExamplesSprite");
        var $tools = $("#toolSpriteHolder");
        if ($examples.length && $tools.length) {
            var textNames = ["draw", "sketch", "fill", "write", "color"];
            var texts = [];
            $.each(textNames, function (a, name) {
                texts.push({
                    $el: $("#" + name + "Text")
                });
            });

            function toolHandler(event) {
                var x = event.pageX - $tools.offset().left;
                var index = Math.floor(x / 79);
                if (index >= 0 && index < 5) {
                    $tools.css({
                        backgroundPosition: (-394 * index) + "px 0px"
                    });

                    $examples.css({
                        backgroundPosition: (-683 * index) + "px 0px"
                    });

                    $.each(texts, function (a, text) {
                        if (a == index)
                            text.$el.show();
                        else
                            text.$el.hide();
                    });
                }
            }

            $tools
                .click(toolHandler)
                .mousemove(toolHandler);
        }
    }
};

// ==========
$(document).ready(function () {
    Main.init();
});

// ==========
// XXX ISCROLL MONKEY PATCH: add an onPageChange event:
if (window.iScroll) {
    var iScroll_scrollTo_orig = iScroll.prototype.scrollTo;
    iScroll.prototype.scrollTo = function () {
        iScroll_scrollTo_orig.apply(this, arguments);
        if (typeof this.onPageChange === 'function') {
            this.onPageChange(this.currPageX, this.currPageY);
        }
    };
}

// ==========
window.Carousel = function (settings) {
    var self = this;

    this.settings = settings;
    this.index = -1;
    this.dragStart = {
        time: 0,
        x: 0
    };
    this.dragDelta = {
        time: 0,
        x: 0
    };

    this.$images = this.settings.$el.find("#" + this.settings.imageName);

    this.$left = this.settings.$el.find(".leftArrow");
    this.$right = this.settings.$el.find(".rightArrow");

    this.$dots = this.settings.$el.find(".carouselButton");
    this.$dots.each(function (a, dot) {
        $(dot)
            .click(function () {
                self.setIndex(a, { animate: true });
            });
    });

    // abstract the dragging/etc. logic via iScroll:
    this.scroller = new iScroll(settings.imageName, {
        snap: true,
        bounce: true,
        momentum: false,
        vScroll: false,
        hScrollbar: false,
        onBeforeScrollStart: onBeforeScrollStart,
        onBeforeScrollEnd: onBeforeScrollEnd
    });

    this.scroller.onPageChange = function (pageX, pageY) {
        if (self.index === pageX)
            return;

        self.index = pageX;

        self.$dots
            .removeClass("carouselButtonActive");

        self.$dots.eq(self.index)
            .addClass("carouselButtonActive");

        self.$left.toggleClass("disabled", self.index == 0);
        self.$right.toggleClass("disabled", self.index == self.settings.count - 1);

        if (self.settings.onUpdate)
            self.settings.onUpdate(self.index);
    };

    function getX(event) {
        var x = event.pageX;
        var touches = event.touches;
        if (touches && touches[0]) {
            x = touches[0].pageX;
        }
        return x;
    }

    this.scroller.onPageChange(0, 0);

    // prevent clicks conflicting with drags:

    function onBeforeScrollStart(event) {
        event.preventDefault();     // XXX needed!
        self.dragStart.time = $.now();
        self.dragStart.x = getX(event);
    }

    function onBeforeScrollEnd(event) {
        self.dragDelta.time = $.now() - self.dragStart.time;
        self.dragDelta.x = Math.abs(getX(event) - self.dragStart.x);
    }

    this.$images
        .click(function (event) {
            if (self.dragDelta.time > 200 || self.dragDelta.x > 10)
                event.preventDefault();
        });
};

// ==========
Carousel.prototype = {
    // ----------
    setIndex: function (value, options) {
        if (value == this.index || value < 0 || value > this.settings.count - 1)
            return;

        // default options:
        options = options || {};

        // use iscroll default animation time (via undefined) if animating
        var animationTime = options.animate ? undefined : 0;
        this.scroller.scrollToPage(value, 0, animationTime);
    },

    // ----------
    next: function (options) {
        this.setIndex(this.index + 1, options);
    },

    // ----------
    previous: function (options) {
        this.setIndex(this.index - 1, options);
    }
};
