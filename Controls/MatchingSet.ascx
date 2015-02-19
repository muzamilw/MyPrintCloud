<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MatchingSet.ascx.cs"
    Inherits="Web2Print.UI.Controls.MatchingSet" %>
<asp:Label ID="lblMesg" runat="server" Text="No Matching set found" CssClass="errorMsgClss"></asp:Label>
<div id="MainContainerDiv" class="rounded_corners order_container cntMatchingSet" runat="server" style="padding:5px;">
   
    <div class="pad20 col-md-4 col-lg-4 col-xs-12 matchingSetsleftPanel" style="text-align:left;">
        <div class="custom_color confirm_design">
            <asp:Literal ID="ltrlmatchingset" runat="server" Text="Matching Set"></asp:Literal>
        </div>
        <br />
        <asp:Literal ID="ltrlhere" runat="server" Text="Here are other products with the"></asp:Literal>

        <asp:Literal ID="ltrlsameasdesignconcept" runat="server" Text="same design concepts"></asp:Literal>
    </div>
    <div class="white_background rounded_corners pad10 col-md-8 col-lg-8 col-xs-12 matchingSetsrightPanel">
        <asp:Repeater ID="rptMatchingSets" runat="server" OnItemCommand="rptMatchingSets_ItemCommand" OnItemDataBound="rptMatchingSets_ItemDataBound">
            <ItemTemplate>
                <div style="width:200px; float:left; text-align:left;height:225px;">

                
                <asp:LinkButton ID="hlImage1" runat="server" CommandName="DesignOnline" CommandArgument='<%#Eval("ProductID","{0}") %>'>
                    <div id="divShd" class="matching_set_image_container">
                        <asp:Image ID="imgPic1" runat="server" ImageUrl='<%#Eval("Thumbnail","{0}") %>' CssClass="matching_set_image"
                            BorderStyle="None" />
                    </div>
                </asp:LinkButton>
                <asp:HiddenField ID="hfCategoryId" runat="server" />
                <asp:HiddenField ID="hfItemID" runat="server" />
                <br />
                <asp:Label ID="lblProductName" runat="server"></asp:Label><br />
                <div id="DivOfPriceCircle" class="fontSyleBold">
                    <asp:Label ID="lblMinPrice" runat="server"></asp:Label>
                </div>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
     <div class="clearBoth">
        &nbsp;
    </div>
    </div>
    <div class="clearBoth">
        &nbsp;
    </div>
</div>
