
$(document).ready(function () {
    $("#products .wrapper").jCarouselLite({
        btnNext: "#products .next-btn",
        btnPrev: "#products .prev-btn",
        visible: 6,
        auto: 5000,
        speed: 7000,
        circular: true,
        scroll: 6

    });

});