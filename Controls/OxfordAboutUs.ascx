<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OxfordAboutUs.ascx.cs" Inherits="Web2Print.UI.Controls.OxfordAboutUs" %>
<div id="footer" class="container footer-content spacer-top">
    <div class="container">
        <p></p>
        <div class="row">
            <div class="col-xs-12 col-md-3">
                <div class="panel panel-info" id="">
                    <div class="panel-heading">
                        <h3 class="panel-title">About MyPrintCloud</h3>
                    </div>
                    <div class="panel-body">
                        <p>MyPrintCloud is one of the most preferred and highly affordable web to print solution with wide range of features and customization for SME printers. With Web2Print Store, your customers can create designs online and place orders 24×7. The solution offers user friendly storefront management, online marketing tools, and reports to reduce operational cost. We understand that we grow only when our clients grow. So we are consistently adding new user friendly features to improve productivity and reduce cost.</p>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-md-9" id="parentCatsContainer" runat="server">
                <div class="page-header">
                    <div class="panel panel-info" id="product_list_footer">
                        <div class="panel-heading">
                            <h3 class="panel-title">Product Category</h3>
                        </div>
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <asp:Repeater ID="parentcateRpt" runat="server" OnItemDataBound="parentcateRpt_ItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <a id="ParentCateLink" runat="server" style="width: 100%;"><i class="icon-th-large"></i>
                                                <span><b id="parentCatName" runat="server">Business Cards</b></span></a><br />
                                            <ul>
                                                <asp:Repeater ID="subCatRpt" runat="server" OnItemDataBound="subCatRpt_ItemDataBound">

                                                    <ItemTemplate>
                                                        <li style="width: 100%;"><a id="subCatLink" runat="server"  style="width: 100%;"><span id="subCatName" runat="server">Metallic Business Cards</span></a></li><br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <p></p>
    </div>
</div>
