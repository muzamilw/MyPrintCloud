<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="PaymentPreferences.aspx.cs" Inherits="Web2Print.UI.PaymentPrferences" %>

<%@ Register Src="Controls/ProductDetail.ascx" TagName="ProductDetail" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
     <style>
        input[type="checkbox"]{display:none;outline:none !important;-webkit-transition:background-color;-moz-transition:background-color;-o-transition:background-color;-ms-transition:background-color;transition:background-color;}
input[type="checkbox"] + label{display:inline-block !important;padding:6px 0 6px 45px;line-height:25px;background-image:url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.png?4214");background-image:none,url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.svg?4214");background-position:-108px 0;background-repeat:no-repeat;-webkit-background-size:143px 143px;-moz-background-size:143px 143px;background-size:143px 143px;overflow:visible;outline:none;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;cursor:pointer;cursor:hand;color:#66615b;outline:none !important}
input[type="checkbox"]:hover + label,input[type="checkbox"] + label:hover,input[type="checkbox"]:hover + label:hover{background-position:-72px -36px;color:#403d39}
input[type="checkbox"]:checked + label{background-position:-36px -72px;color:#403d39}
input[type="checkbox"]:checked:hover + label,input[type="checkbox"]:checked + label:hover,input[type="checkbox"]:checked:hover + label:hover{background-position:0 -108px;color:#403d39}
    </style>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            
                 <div id="MessgeToDisply" class="rounded_corners" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
             <div class="signin_heading_div float_left_simple dashboard_heading_signin">
              <asp:Label ID="lblTitle" runat="server" Text="Payment Preferences" CssClass="sign_in_heading"></asp:Label>
                       
            </div>
            <div class="dashBoardRetrunLink">
                <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                        MyAccountCurrentPage="Store Banners" MyAccountCurrentPageUrl="BrokerBannerWiget.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
            <div class="white-container-lightgrey-border rounded_corners" style="padding-bottom: 10px;">
                <div class="textAlignLeft paddingLeft10px paddingBottom10px headingsAvenior paddingTop15px  ">
                    <asp:Label ID="Label2" runat="server" Text="View and update payment gateway settings" />
                </div>
                <table cellpadding="0" cellspacing="0" width="100%" style="text-align: left; margin-left: 10px;
                    margin-right: 10px; width: 98%">
                    <tr>
                        <td width="30%" style="display:none;">
                            How do you want to process orders: 
                        </td>
                        <td width="50%" class="tdPaymentPreference">
                            <asp:RadioButton ID="rbPaymentYes" GroupName="RD" runat="server" onclick="PaymentGatewayGridVisible(true);"
                                ClientIDMode="Static" Text="Payment is mandatory excepted through gateway below" /><br />
                            <asp:RadioButton ID="rbPaymentNo" runat="server" GroupName="RD" onclick="PaymentGatewayGridVisible(false);"
                                ClientIDMode="Static" Text="Process orders without payment against account" />
                        </td>
                        <td width="20%">
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            &nbsp;&nbsp;
                        </td>
                        <td width="50%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="divPaymentGatewaySelection" style="margin-top: 10px; display:none;">
                        <td style="display:none;">
                            Store will accept the payments using :
                        </td>
                        <td style="display:none;">
                            <asp:RadioButton ID="rbPaymentgateCentral" GroupName="RD1" runat="server" 
                                ClientIDMode="Static" Text="Central payment gateway" />
                            <asp:RadioButton ID="rbPaymentgatePrivate" runat="server" GroupName="RD1" Checked="true"
                                ClientIDMode="Static" Text="Your paypal account below" />
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr style="margin-top: 10px;">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                           <asp:Button ID="btnSaveRadioBtnChanges" runat="server" CssClass="start_creating_btn rounded_corners5 MTopM10 marginRight"
                                Text="Save" Width="100px" OnClick="btnSaveRadioBtnChanges_Click" /><br />
                        </td>
                        <td align="right">
                            
                        </td>
                    </tr>
                </table>
                <div class="clearBoth">
                    <br />
                </div>
                <br />

            </div>
            <div class="clearBoth">
                <br />
            </div>
            <br />
            <br />
            <div class="rounded_corners" id="BannerContainer" runat="server" clientidmode="Static">
             <%--   <div class="textAlignLeft paddingLeft10px paddingBottom10px product_detail_sub_heading custom_color paddingTop15px Width100Percent">
                    Payment Gateways
                </div>--%>
                <div class="cursor_pointer paddingTop5px" id="AddNewContainer" >
                    <div onclick="ShowBannerPopUp();" class="float_left">
                        <img alt="" class="add_new" src="images/AddNew.png" title="Add new payment gateway" /></div>
                    <div class="new_caption_Payment textAlignLeft" onclick="ShowBannerPopUp();">
                        <asp:Label ID="lblNewBannersText" runat="server" Text="Add new payment gateway" ></asp:Label><br />
                        <asp:Label ID="lblNewPayDes" runat="server" Text="click on email address to edit gateway details" CssClass="textAlignLeft"></asp:Label>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
                <div id="NoBannerCon" class="Width100Percent" runat="server">
                    <div class="textAlignCenter paddingBottom10px product_detail_sub_heading custom_color paddingTop15px">
                        <asp:Label ID="lblMessage" runat="server" Text="No Payment gateways are available"
                            Visible="false" />
                    </div>
                </div>
                <%--The Main Container--%>
                <div class="ProductOrderContainer" style="">
                    <asp:GridView ID="grdViewBrokerBanners" DataKeyNames="PaymentGateWayID" runat="server"
                        Style="width: 100%; margin-bottom: 15px; margin-top:15px;"
                        AutoGenerateColumns="False" OnRowDataBound="grdViewBrokerBanners_RowDataBound" OnRowCreated="grid_RowCreated"
                        OnRowCommand="grdViewBrokerBanners_RowCommand">
                        <Columns>


                         <asp:TemplateField HeaderText="Business Email / Gateway username" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="paddingLeft20px">
                                     <ItemTemplate>
                                        
                                            <asp:LinkButton ID="lnkH" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BusinessEmail")%>' ToolTip="Click To Edit payment gateway details" 
                                                CommandName="editp" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentGateWayID")%>' CssClass="paddingLeft20px" />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                     <ItemTemplate>
                                        <asp:Image id="tickimg" runat="server" ImageUrl="~/App_Themes/S6/Images/approveBtn.png"/>
                                            <asp:LinkButton ID="lblIsActive" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' ToolTip="Click To Edit payment gateway details" 
                                                CommandName="editp" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentGateWayID")%>'  />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>


                          <asp:TemplateField HeaderText="Gateway" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                     <ItemTemplate>
                                        
                                            <asp:LinkButton ID="lblPaymentMethod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PaymentMethodID")%>' ToolTip="Click To Edit payment gateway details" 
                                                CommandName="editp" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentGateWayID")%>'  />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>
                          
                           
                            
                            <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                <ItemTemplate>
                                   
                                    <div class="textLeftFloating">
                                        <asp:ImageButton ID="lnkBtnDeleteBannerDetails" runat="server" ToolTip="Click To delete banner" OnClientClick="return confirm('Are you sure you want to delete this payment gateway?');"
                                            ImageUrl="~/images/delete.png" Height="28" Width="28" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentGateWayID")%>' />
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="mpeBanner" BehaviorID="mpeBanner" TargetControlID="hfTargetPO"
                    PopupControlID="pnlBanners" BackgroundCssClass="ModalPopupBG" runat="server"
                    Drag="true" DropShadow="false" CancelControlID="CancelControl" />
                <input type="hidden" id="hfTargetPO" runat="server" />
                <asp:Panel ID="pnlBanners" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                    Style="display: none; width: 700px;">
                    <div class="white_background pad20">
                          <asp:Label ID="Label1" runat="server" Text="Payment Gateway" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
     
                           <div id="CancelControl" onclick="$find('mpeBanner').hide();" class="MesgBoxBtnsDisplay rounded_corners5" style="">
                            Close
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                            </div>
                            <div class="SolidBorderCS">
                            &nbsp;
                            </div>
                        <div class="smallContctUsAvenior float_left_simple" style="margin-bottom: 15px; margin-top:30px;">
                            <asp:Label ID="lblUploadBanner" runat="server" Text="Payment Gateway"></asp:Label>
                        </div>
                        <div class="TTL" style=" width:375px; margin-bottom: 15px; margin-top:15px;">
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" ValidationGroup="f" CssClass="newTxtBox">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
                                ControlToValidate="ddlPaymentMethod" CssClass="errorMessage" InitialValue="-1"
                                Display="Dynamic" ValidationGroup="f"></asp:RequiredFieldValidator>
                        </div>
                        <div class="smallContctUsAvenior float_left_simple" style="margin-bottom: 15px;">
                            <asp:Label ID="lblHeading" runat="server" Text="Gateway username / email"></asp:Label>
                        </div>
                        <div class="TTL" style="width: 375px; margin-bottom: 15px;">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="newTxtBox" ValidationGroup="f"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required"
                                ControlToValidate="txtUsername" CssClass="errorMessage" Display="Dynamic" ValidationGroup="f"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div class="smallContctUsAvenior float_left_simple" style="margin-bottom: 15px;">
<%--                            <asp:Label ID="lblDescp" runat="server" Text="Active"></asp:Label>--%>
                        </div>
                        <div class="TTL"  style="margin-bottom: 15px;">
                            <input type="checkbox" id="chkIsActive" runat="server" onclick="changeCheckBox();" class="PaymentActiveCkbx" /><label for="chkIsActive" onclick="changeCheckBox();">Active</label>
                            <%--<asp:CheckBox ID="" runat="server" ValidationGroup="f"/>--%>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div class="smallContctUsAvenior float_left_simple" style="margin-bottom: 15px;">
                            &nbsp;
                        </div>
                        <div class="TTL">
                            <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                ValidationGroup="f" Text="Save" Width="100px" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                            
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <asp:HiddenField ID="hfPaymentGatewayID" runat="server" />
    <asp:HiddenField ID="hfChangeScenarioToSaveNewBanner" runat="server" Value="0" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            if ($('#rbPaymentNo').is(':checked'))
                PaymentGatewayGridVisible(false);
            else if ($('#rbPaymentgateCentral').is(':checked')) {
                PaymentGatewayGridVisible(false);

            }
            else {
                PaymentGatewayGridVisible(true);

            }


            if ($('#rbPaymentYes').is(':checked'))
                PaymentGatewaySelectionMode(true);
            else
                PaymentGatewaySelectionMode(false);






            $("#<%= grdViewBrokerBanners.ClientID%> tr").children(":first").css("border-top-left-radius", 10, "-moz-border-top-left-radius", 10,
             "-webkit-border-top-left-radius", 10, "-khtml-border-top-left-radius", 10);

            $("#<%= grdViewBrokerBanners.ClientID%> tr").children(":first").next().next().next().css("border-top-right-radius", 10, "-moz-border-top-right-radius", 10,
              "-webkit-border-top-right-radius", 10, "-khtml-border-top-right-radius", 10);


        });

        function changeCheckBox(){
            if ($('input:checkbox[class=PaymentActiveCkbx]').is(':checked')) {

                $('input:checkbox[class=PaymentActiveCkbx]').attr('checked', false);
                

            } else {

                $('input:checkbox[class=PaymentActiveCkbx]').attr('checked', true);
            }
        }

        function ShowBannerPopUp() {


            $('#<%=txtUsername.ClientID %>').val('');

            $('#<%=btnSave.ClientID %>').val('Save');
            $('#<%=hfChangeScenarioToSaveNewBanner.ClientID %>').val('1');
            $find('mpeBanner').show();
        }

        function PaymentGatewaySelectionMode(val) {
            if (val) {
                $('#divPaymentGatewaySelection').show();
            }
            else {
                $('#divPaymentGatewaySelection').hide();
            }
        }


        function PaymentGatewayGridVisible(val) {
            if (val) {
                $('#BannerContainer').show();
            }
            else {
                $('#BannerContainer').hide();
            }
        }

        function ResetHiddenData() {
            $('#<%=hfChangeScenarioToSaveNewBanner.ClientID %>').val('0');
        }
    </script>
</asp:Content>
