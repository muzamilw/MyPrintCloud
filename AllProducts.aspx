<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="AllProducts.aspx.cs" Inherits="Web2Print.UI.AllProducts" %>

<%@ Register Src="Controls/AllProducts.ascx" TagName="AllProducts" TagPrefix="uc1" %>
<asp:Content ID="CatPageHead" ContentPlaceHolderID="HeadContents" runat="server">



</asp:Content>
<asp:Content ID="PageBanner" ContentPlaceHolderID="PageBanner" runat="server">
 <script type="text/javascript">
//     $(function () {
//         $(window).scroll(function () {

//             //             var scrollBottom = $(window).scrollTop() + $(window).height();
//             //             alert($(window).height());

//             //             if ($(this).scrollTop() + $(window).height() != 0) {
//             //                 $('#btnscrollbottomleft').fadeIn();
//             //             } else {
//             //                 $('#btnscrollbottomleft').fadeOut();
//             //             }


//             $(window).scroll(function () {
//                 if ($(window).scrollTop() + $(window).height() == $(document).height()) {
//                     $('#btnscrollbottomleft').fadeOut();
//                     $('#btnscrollbottomright').fadeOut();
//                 }
//                 else {
//                     $('#btnscrollbottomleft').fadeIn();
//                     $('#btnscrollbottomright').fadeIn();
//                 }
//             });



//         });

//         $('#btnscrollbottomleft').click(function () {
//             $('body,html').animate({ scrollTop: $(window).height() }, 'slow');
//         });

//         $('#btnscrollbottomright').click(function () {
//             $('body,html').animate({ scrollTop: $(window).height() }, 'slow');
//         });
//     });
</script>
<div id="btnscrollbottomleft"></div>
<div id="btnscrollbottomright"></div>
</asp:Content>
<asp:Content ID="CatPageMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            <uc1:AllProducts ID="AllProducts12" runat="server" />
        </div>
    </div>
    
</asp:Content>
