<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="CompanyLogo.aspx.cs" Inherits="Web2Print.UI.CompanyLogo" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Company Logo" MyAccountCurrentPageUrl="CompanyLogo.aspx" />
            <div class="signin_heading_div">
                <asp:Label ID="lblTitle" runat="server" Text="Company Logo" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="white_back_div rounded_corners">
                <div class="pad20">
                <div class="product_detail_sub_heading custom_color floatleft clearBoth">
                            <asp:Literal ID="lblHead" runat="server" Text="View and update your store Logo:"></asp:Literal>
                        </div>
                        <br />
                        <div class="normalTextStyle Company-logoDiv-CS">
                            <asp:Image ID="imgCustomerLogo" runat="server" CssClass="Company-logoDiv-image-CS" />
                        </div>
                        <div class="normalTextStyle Company-logoDiv-CS">
                            <asp:FileUpload ID="txtLogoUploadFile" runat="server" CssClass="file_upload_box210 rounded_corners5"
                                TabIndex="10" />
                            <br />
                            <br />
                            <asp:Button ID="btnUploadFile" runat="server" CssClass="start_creating_btn rounded_corners5"
                                Text="Upload Image" OnClick="btnUploadFile_Click" style="display:none;" />
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                CssClass="start_creating_btn rounded_corners5" PostBackUrl="~/DashBoard.aspx" />
                        </div>
                    <div class="clearBoth">
                    &nbsp;
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <script type="text/javascript">
    $(document).ready(function () {
            CreateSingleUpload();
        });
        function CreateSingleUpload() {
            //set up the file upload
            $("#" + "<%=txtLogoUploadFile.ClientID %>").MultiFile({
                max: 1,
                accept: 'jpg,bmp,png',
                afterFileSelect: function (element, value, master_element) {
                    $('#<%=btnUploadFile.ClientID %>').css('display', 'inline-block');
                },
                afterFileRemove: function (element, value, master_element) {
                    $('#<%=btnUploadFile.ClientID %>').css('display', 'none');
                }
            });
        }
    </script>
</asp:Content>
