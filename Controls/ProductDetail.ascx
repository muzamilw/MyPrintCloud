<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetail.ascx.cs"
    Inherits="Web2Print.UI.Controls.ProductDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script src="../Scripts/utilities.js"></script>
<div id="divShd" class="opaqueLayer" style="z-index: 999 !important;">
</div>
<asp:UpdatePanel ID="brokerbody" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="divSearchBar_corp normalTextStyle usermanager_padding_header rounded_corners "
            style="padding: 15px 15px 15px 15px;">
            <table style="width: auto;">
                <tr>
                    <td>
                        <asp:Literal ID="ltrlSearchrecords" runat="server" Text=" Search Products"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="txtsearch" runat="server" Width="432px" CssClass="text_box150 rounded_corners5" style="margin-left:20px; margin-right:20px;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsearch" runat="server"  Text="Go" CssClass="start_creating_btn rounded_corners5 margin_left"
                            OnClick="btnsearch_Click" Style="margin-right:20px;" />
                    </td>
                    <td>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="start_creating_btn rounded_corners5 margin_left"
                            OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="white_background rounded_corners">

            <div class="PadingTop">
            </div>
            <div class="clearBoth">
            </div>
            <div style="width: auto; height: auto; float: right; padding: 10px 10px 10px 10px">
                <asp:Button ID="btnproductsSave" runat="server" Text="Save" CssClass="start_creating_btn rounded_corners5"
                    OnClick="cmdSave_Click" />
            </div>
            <div class="PadingTop">
            </div>

            <table cellpadding="10" cellspacing="0" style="width: 100%;">
                <tr>
                    <asp:Repeater ID="Product_Repeater" runat="server" OnItemDataBound="Product_Repeater_ItemDataBound"
                        OnItemCommand="getbroker">
                        <HeaderTemplate>
                            <th align="left" style="background-color: #f3f3f3;"></th>
                            <th align="left" style="background-color: #f3f3f3; padding: 5px;">
                                <asp:Label ID="Product_Name" Text="Product Name" runat="server" CssClass="checked_design"></asp:Label>
                            </th>
                            <th class="broker_space" style="background-color: #f3f3f3;"></th>
                            <th align="left" style="background-color: #f3f3f3;">
                                <asp:Label ID="Label5" Text="Quantity" runat="server" CssClass="checked_design"></asp:Label>
                            </th>
                            <th align="left" style="background-color: #f3f3f3;">
                                <asp:Label ID="Label1" Text="Webstore price" runat="server" CssClass="checked_design"></asp:Label>
                            </th>
                            <th align="left" style="background-color: #f3f3f3;">
                                <asp:Label ID="publish" Text="Published" runat="server" CssClass="checked_design"></asp:Label>
                            </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="RowOfProductRecord" class="gridViewRowStyle-PD border_bottom" runat="server" title="Click to view Product Price Matrix">
                                <td style="width: 20%;" class="broker_Product_Name">
                                    <div class="cart_grid_image_container">
                                        <asp:HyperLink ID="lnkPdfFile" runat="server">
                                            <asp:Image ID="imgToShow" runat="server" ImageUrl='<%# Eval("ThumbnailPath") %>'
                                                CssClass="image_stretcher" />
                                        </asp:HyperLink>
                                    </div>
                                </td>
                                <td class="broker_Product_Name cursor_pointer">
                                    <asp:Label ID="lblProductName" Font-Bold="true" runat="server" Text='<%# Eval("ProductName") %>' />
                                </td>

                                <td class="broker_Product_Name broker_space"></td>
                                <td class="broker_Product_Name cursor_pointer">
                                    <asp:Label ID="lblQuantity" Font-Bold="true" runat="server" CssClass="lblMinPRicePD" />
                                </td>
                                <td class="broker_Product_Name ">
                                    <asp:Label ID="lblMinPrice" Font-Bold="true" runat="server" CssClass="lblMinPRicePD" />
                                </td>

                                <td class="broker_Product_Name ">
                                    <asp:CheckBox ID="chkPublished" runat="server" Checked='<%# Eval("IsDisplayToUser") %>'></asp:CheckBox>
                                    <asp:HiddenField ID="hdRecID" runat="server" Value='<%# Eval("ItemID") %>' />
                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </table>
            <asp:HiddenField ID="hfbrokerItemId" runat="server" Value="0" />
            <div style="height: 25px">
            </div>
            <table align="right" style="padding: 10px 10px 10px 10px">
                <tr>
                    <td>
                        <asp:Button ID="cmdsave" runat="server" Text="Save" CssClass="start_creating_btn rounded_corners5"
                            OnClick="cmdSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 30px">
        </div>
     <%--   <div id="PnlPagerControl" class="Width100Percent" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" class="Width100Percent textAlignRight">
                <tr>
                    <td>
                        <div id="PagerRighArea">
                            <div class="divTblLayout Width100Percent">
                                <div class="tblRow">
                                    <div class="divCell">
                                        <asp:LinkButton ID="lnkBtnPreviouBullet" runat="server" CssClass="imgPreviousBullet"
                                            OnClick="lnkBtnPreviouBullet_Click" Visible="false" />
                                    </div>
                                    <div class="divCell">
                                        <div id="PagerPageDisplayInfo">
                                            <asp:Label ID="lblPageInfoDisplay" CssClass="simpleText" runat="server" Text="1 of 1"
                                                Visible="false" />
                                        </div>
                                    </div>
                                    <div class="divCell">
                                        <asp:LinkButton ID="lnkBtnNextBullet" runat="server" CssClass="imgNextBullet" OnClick="lnkBtnNextBullet_Click"
                                            Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>--%>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hfbrokermarkup" runat="server" Value="0" />
<asp:HiddenField ID="hfContactmarkup" runat="server" Value="0" />


<%--<div> <asp:TextBox runat="server" ID="txtTest" Text="Test"></asp:TextBox></div>--%>
<div id="jqwin" class="FileUploaderPopup_Mesgbox rounded_corners IframeCss LCLB">
</div>
<style>
    #jqwin
    {
        /* height: 100%;--%> */
        z-index: 99999;
        position: absolute;
        display: none;
    }
</style>
<script type="text/javascript" language="javascript">
    function DisplayBrokerPrices(itemId, BrokerMarkup, ContactMarkup) {
        var popwidth = 990;
        var shadow = document.getElementById("divShd");
        var bws = getBrowserHeight();
        shadow.style.width = bws.width + "px";
        shadow.style.height = bws.height + "px";
        var left = parseInt((bws.width - popwidth - 30) / 2);
        var top = parseInt((bws.height - 681) / 2);

        $('#divShd').css("display", "block");
        $('#jqwin').css("width", popwidth);
        $('#jqwin').css("height", "681px");
        $('#jqwin').css("top", top);
        $('#jqwin').css("left", left);


        // var html = ' <input type="hidden" id="hfPriceMatrixChanges" value="0" /> <div onclick="closeMS();" class="MesgBoxBtnsDisplay_subscriber rounded_corners">Close</div>';
        $('#jqwin').html('<iframe id="ifrm" width="970px" height="100%" border="0" style="width:970px;height:100%;border: none; background-color: white;" class=""></iframe>')
        //$('#jqwin').html( '<iframe id="ifrm" width="' + popwidth + '" height="100%" border="0" style="width:' + popwidth + 'px;height:100%;border: none; background-color: white;" class=""></iframe>')
        $('#ifrm').attr('src', '/WebForm1.aspx?ItemID=' + itemId + "&BrokerMarkupId=" + BrokerMarkup + "&ContactMarkupId=" + ContactMarkup);
        $('#jqwin').show();
    }
    function closeMS() {

        
        showProgress();
        window.location.reload(true);
        
        $('#jqwin').hide();
   
    }

    function closePopup() {
        $('#jqwin').hide();
        $('#divShd').css("display", "none");
    }
</script>
