function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}
function PointToPixel(Val)
{
        return Val * 96 / 72;
}
function PixelToPoint(Val)
{
        return Val / 96 * 72;
}
function DisplayDiv(divid) {
    if (divid == 1) {
        $('#AddColorShape').hide();
        $('#AddColorShapeLbl').hide();
        $("#LbImgCrop").css("display", "inline");
        $("#BtnCropImg").css("display", "inline-block");
        $("#BtnCropImg2").css("display", "inline-block");
        $("#BtnFlipImg1").css("display", "block");
        $("#BtnFlipImg2").css("display", "block");
        $("#FlipLbl").css("display", "inline");
        $("#AddColorShapeRetailDiv").css("display", "none");
    } else if (divid == 2) {
        
        $("#LbImgCrop").css("display", "none");
        $("#BtnCropImg").css("display", "none");
        $("#BtnCropImg2").css("display", "none");
        $("#BtnFlipImg1").css("display", "none");
        $("#BtnFlipImg2").css("display", "none");
        $("#FlipLbl").css("display", "none");
        $("#AddColorShapeRetailDiv").css("display", "none");
        $('#AddColorShape').hide();
        $('#AddColorShapeLbl').hide();
        if (IsCalledFrom == 2 || IsCalledFrom == 4) {
            $('#AddColorShape').show();
            $('#AddColorShapeLbl').show();
        } else {
            $("#AddColorShapeRetailDiv").css("display", "block"); 
        }
    }
  
}