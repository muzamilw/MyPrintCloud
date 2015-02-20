<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPostCode.ascx.cs"
    Inherits="Web2Print.UI.Controls.SearchPostCode" %>
<script src="/Scripts/input.watermark.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui-1.9.2.custom.min.js"></script>
<link href="/Styles/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />

<script src="/Scripts/toastr.min.js" type="text/javascript"></script>
<link href="/content/toastr.css" rel="stylesheet" type="text/css" />



<style>
    .ui-autocomplete .ui-menu-item {
        text-align: left;
    }

    .ui-autocomplete-loading {
        background: white url('/images/pu_loader.gif') right center no-repeat !important;
    }

    /*.navbar {
        display: none;
        overflow: no-content;
        box-shadow: 0 8px 6px -6px #666;
    }*/



    .container {
        width: 1000px;
        height: 75px;
        margin: auto;
    }

        .container DIV {
            margin-top: 15px;
        }
</style>
<%--<div id="divShd" class="opaqueLayer">
</div>--%>
<%--<script src="../Scripts/bootstrap.min.js"></script>
<link href="../Content/bootstrap.css" rel="stylesheet" />--%>
<div class="navba" runat="server" id="searchpostcodebar" clientidmode="Static" style="">




    <div class="nearestOutlettxt">
        FREE Next Day Delivery* 
    </div>

    <asp:TextBox ID="txtPostCode" runat="server" CssClass="txtPostCode_Header rounded_corners"
        CausesValidation="false" ClientIDMode="Static"></asp:TextBox><br />
    <asp:Button ID="btnFindStore" runat="server" Text="Find your Local Store" CssClass="btnFindStore rounded_corners5"
        OnClientClick="return ChangeCSS();" OnClick="btnFindStore_Click" ClientIDMode="Static"
        CausesValidation="false" UseSubmitBehavior="true" />

    <img id="imgLogo" runat="server" src="/images/cancer_awarness.png" class="cancer_awarness_logo LOgo_ImgeP" alt="" />
    <p class="onStoresPop">* at selected stores</p>


    <div id="empty-message" class="searchPostCodeErr">
    </div>
    <asp:HiddenField ID="hfnoResultF" runat="server" Value="0" />

    <asp:Panel ID="pnlPartners" runat="server" CssClass="pnlPartners"  Visible="false">
        <h2 id="partnermainheading" class="partnerh2">
            Select your nearest Local Store below :
        </h2>
        <ul id="partnerslist">
            <asp:Repeater ID="rptPartners" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="rptPartners_ItemDataBound">
                <ItemTemplate>

                    <li>
                        <asp:ImageButton ID="ImageButton1" Height="50" runat="server" ImageUrl='<%#Eval("logo","{0}") %>' CommandName="selectstore" CommandArgument='<%#Eval("ContactCompanyID","{0}") %>'></asp:ImageButton>
                        <asp:LinkButton ID="ContactCompanyName" runat="server"  CommandName="selectstore" CommandArgument='<%#Eval("ContactCompanyID","{0}") %>'></asp:LinkButton>
                    </li>
                </ItemTemplate>
                <SeparatorTemplate>
                    <li>
                        <hr />
                    </li>
                </SeparatorTemplate>


            </asp:Repeater>
        </ul>
    </asp:Panel>
</div>





<script type="text/javascript">


    function clearResult() {
        $("#txtPostCode").val('');
    }


    //            $("#txtCity").blur(function () {
    //                if ($("#txtCity").val() != '') {
    //                    clearResult();
    //                }
    //            });



    function ChangeCSS() {


        if (validateinput()) {

            //window.parent.increaseHeight();

            if ($("#<%=hfnoResultF.ClientID %>").val() == 1) {
                return false;
            } else {



                resizePopup();

                __doPostBack('<%=btnFindStore.UniqueID%>', '');
                return false;
            }



            //return true;

        }
        else {

            return false;
        }

    }


    function validateinput() {

        if ($("#<%=txtPostCode.ClientID%>").val() == '' || $("#<%=txtPostCode.ClientID%>").val() == 'Enter Postcode') {
            //$("#empty-message").text("Please enter a valid Postcode to find an outlet near you");
            toastr.error('Please enter a valid Postcode to find an outlet near you', 'Error');
            return false;
        }
        else {
            //$("#empty-message").empty();
            return true;
        }


    }

    function ChangeStore(ReturnPath) {
        showProgress();
        window.parent.window.location.href = ReturnPath; // "/default.aspx";
    }


    function resizePopup() {
   
        setTimeout(function () {
            showProgress();
        }
      , 200);
        
        $("#ifrm", parent.document).animate({ height: 500 }, 200);
        if ($(window.parent).width() < 961) {
            
            $("#dialog-box-parent-container", parent.document).animate({ top: '6%' }, 200);
            $("#partnermainheading").addClass('partnerresponsiveh2');
            $("#partnermainheading").removeClass('partnerh2');
            $("#partnerslist li a").css("font-size", "14px");
        }
        else {
            $("#dialog-box-parent-container", parent.document).animate({ top: '38%' }, 200);
            $("#partnermainheading").removeClass('partnerresponsiveh2');
            $("#partnermainheading").addClass('partnerh2');
            $('#partnerslist li a').css("font-size", "18px;");
        }
       
    }



    function displaypostcodenavbar() {


        //$('#searchpostcodebar').slideToggle("slow");


        //setTimeout(function () {
        //    $('#partnerpanel').slideToggle("slow");
        //}
        //, 1000);



    }
</script>

<script type="text/javascript">
    $(document).ready(function () {

        $("#txtPostCode").inputWatermark();

        setTimeout(function () {
            displaypostcodenavbar();
        }
        , 1000);

        if ($("#<%=hfnoResultF.ClientID %>").val() == 1) {
            toastr.error('Please enter a valid Postcode to find an outlet near you', 'Error');
            $("#<%=hfnoResultF.ClientID %>").val(0);
        }

        if ($(window.parent).width() < 961) {
          
            $("#partnermainheading").addClass('partnerresponsiveh2');
            $("#partnermainheading").removeClass('partnerh2');
            $("#partnerslist li a").css("font-size", "14px");
            $("#partnerslist li input").css("max-width", "200px");
            $("#partnerslist li input").css("height", "30px");
        }
        else {
            $("#partnermainheading").removeClass('partnerresponsiveh2');
            $("#partnermainheading").addClass('partnerh2');
            $('#partnerslist li a').css("font-size", "18px;");
        }
    });

</script>

<style>
    #imgLogo.pink_company_top_logo {
        display: none;
    }
</style>

