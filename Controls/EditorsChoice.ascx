<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditorsChoice.ascx.cs" Inherits="Web2Print.UI.Controls.EditorsChoice" %>
<asp:Label ID="lblMesg" runat="server" Text="No template found" CssClass="errorMsgClss"></asp:Label>
<div id="MainContainerDiv" class="EditorsPickContainer rounded_corners" runat="server">
    <div id="TblMS" style="width: 100%;" runat="server">
        <div class="TblMSLeftDiv pad10" style="display: none;">
            <div class="custom_color confirm_design">
                <asp:Literal ID="ltrlEditorsPick" runat="server" Text="Editor's Pick"></asp:Literal>
            </div>
            <br />
            <asp:Literal ID="ltrlhere" runat="server" Text="Here are some products we"></asp:Literal>

            <asp:Literal ID="ltrlsameasdesignconcept" runat="server" Text="recommend for you"></asp:Literal>
        </div>
        <div class=" TblMSRightDiv  rounded_corners ">
            <asp:Repeater ID="dlDesignerTemplates" OnItemDataBound="dlDesignerTemplates_OnItemDataBound"
                runat="server">
                <ItemTemplate>
                    <div class="EditorPickRItemEditorChoice " style="padding-left:0px; padding-right:0px;">
                        <a id="hlImageBtn" runat="server">

                            <div id="divShd" class="matching_set_image_container">
                                <asp:Image ID="imgPic1" runat="server" ImageUrl='<%#Eval("Thumbnail","{0}") %>' CssClass="matching_set_image"
                                    BorderStyle="None" />
                            </div>
                        </a>
                        <asp:HiddenField ID="hfCategoryId" runat="server" />
                        <asp:HiddenField ID="hfItemID" runat="server" />
                        <br />
                        <div>
                            <asp:Label ID="lblProductName" runat="server"></asp:Label>
                        </div>
                        <br />
                        <br />
                        
                    </div>
                </ItemTemplate>

            </asp:Repeater>
        </div>
        <div class="freedesigns hidden-xs hidden-sm">
                            Free designs
        </div>

        <div class="clear"></div>
    </div>

</div>
