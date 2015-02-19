<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="PostCodesManager.aspx.cs" Inherits="Web2Print.UI.PostCodesManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
<link href="../Styles/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <style>
        .ui-autocomplete-loading
        {
            background: white url('../images/pu_loader.gif') right center no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Post Codes" MyAccountCurrentPageUrl="PostCodesManager.aspx" />
            <div id="MessgeToDisply" class="rounded_corners" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
            <div class="signin_heading_div">
                <div class="float_left_simple">
                    <asp:Label ID="lblTitle" runat="server" Text="Post Codes" CssClass="sign_in_heading"></asp:Label></div>
                <div class="clearBoth">
                </div>
            </div>
            <div class="white_back_div rounded_corners" id="BannerContainer" runat="server">
                <div class="cursor_pointer paddingTop5px" id="AddNewContainer">
                    <div onclick="ShowPostPopUp();" class="float_left">
                        <img alt="" class="add_new" src="images/AddNew.png" title="Add New Post Code" /></div>
                    <div class="new_caption">
                        <asp:Label ID="lblNewBannersText" runat="server" Text="New Post Code"></asp:Label>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
                <%--The Main Container--%>
                <div class="ProductOrderContainer">
                    <div>
                        <asp:GridView ID="grdViewBrokerPostCodes" DataKeyNames="ID" runat="server"
                            AutoGenerateColumns="False" OnRowDataBound="grdViewBrokerPostCodes_RowDataBound"
                            OnRowCommand="grdViewBrokerPostCodes_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="Code Name" DataField="Name" HeaderStyle-HorizontalAlign="Left"/>
                                <asp:BoundField HeaderText="Post Code" DataField="OutPostCode" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Country" DataField="Country" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Status" DataField="PCStatus" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Valid Upto" DataField="ValidityDate" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <div class="textLeftFloating paddingRight3px">
                                            <asp:ImageButton ID="lnkBtnUpdatePostCodesDetails" runat="server" Text="Edit" ToolTip="Click To edit post code details"
                                                CssClass="rounded_corners" ImageUrl="~/images/edit.png" Height="28" Width="28"
                                                CommandName="UpdatePostCodesDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' /></div>
                                        <div class="textLeftFloating">
                                            <asp:ImageButton ID="lnkBtnDeletePostCodesDetails" runat="server" Text="Delete" ToolTip="Click To post code banner"
                                                CssClass="rounded_corners" ImageUrl="~/images/delete.png" Height="28" Width="28"
                                                CommandName="DeletePostCodesDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="mpePostCode" BehaviorID="mpePostCode" TargetControlID="hfTargetPO"
                    PopupControlID="pnlPostCode" BackgroundCssClass="ModalPopupBG" runat="server"
                    Drag="true" DropShadow="false" CancelControlID="btnCancel" />
                <input type="hidden" id="hfTargetPO" runat="server" />
                <asp:Panel ID="pnlPostCode" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                    Style="display: none; width: 700px;">
                    <div style="background: white; padding-top: 5px; padding-bottom: 5px;" class="white_background">
                        <div class="Pad5px left_align">
                            <asp:Label ID="lblPostCode" runat="server" Text="Post Code" CssClass="sign_in_heading left_align"></asp:Label>
                        </div>
                        <div class="Bottom-doted-Cs">
                            <br />
                        </div>
                        <div class="LeftAlignDivsCs_PC">
                            <asp:Label ID="lblCode" runat="server" Text="Post Code:"></asp:Label>
                        </div>
                        <div class="RightAlignDivsCs">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="text_box334 PinkRegInputSmall"></asp:TextBox>
                        </div>
                        <div class="clearBoth mrginBtm">
                            &nbsp;
                        </div>
                        <div class="LeftAlignDivsCs_PC">
                            <asp:Label ID="lblValidDate" runat="server" Text="Validity Date:"></asp:Label>
                        </div>
                        <div class="RightAlignDivsCs">
                            <asp:TextBox ID="txtValidDate" runat="server" CssClass="text_box334"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDatevalidity" runat="server" TargetControlID="txtValidDate">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                        <div class="clearBoth mrginBtm">
                            &nbsp;
                        </div>
                        <div class="LeftAlignDivsCs_PC">
                            <asp:Label ID="lblRegistrtaionType" runat="server" Text="Registration Type:"></asp:Label>
                        </div>
                        <div class="RightAlignDivsCs H4B">
                            <asp:RadioButton ID="RDReservaion" runat="server" GroupName="RegistrationType" Text="Reservation" />
                            <asp:RadioButton ID="RDFreeReg" runat="server" GroupName="RegistrationType" Text="Free Registration" Checked="true" />
                        </div>
                        <div class="clearBoth mrginBtm">
                            &nbsp;
                        </div>
                        <div class="LeftAlignDivsCs_PC">
                            &nbsp;
                        </div>
                        <div class="RightAlignDivsCs">
                            <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                Text="Update" Width="100px" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                Text="Cancel" Width="100px" />
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
    <asp:HiddenField ID="hfPCID" runat="server" />
    <asp:HiddenField ID="hfOuterPostCodeID" runat="server" />
    <asp:HiddenField ID="hfChangeScenarioToSaveNewCode" runat="server" Value="0" />
      <script type="text/javascript" language="javascript">
          $(document).ready(function () {
              $("#<%= grdViewBrokerPostCodes.ClientID%> tr").children(":first").css("border-top-left-radius", 10, "-moz-border-top-left-radius", 10,
             "-webkit-border-top-left-radius", 10, "-khtml-border-top-left-radius", 10);

              $("#<%= grdViewBrokerPostCodes.ClientID%> tr").children(":first").next().next().next().next().next().next().css("border-top-right-radius", 10, "-moz-border-top-right-radius", 10,
              "-webkit-border-top-right-radius", 10, "-khtml-border-top-right-radius", 10);
          });
          $('#<%=txtCode.ClientID %>').autocomplete({

              source: function (request, response) {

                  $.ajax({

                      url: "../services/webstore.svc/SearchPostCode?Postcode=" + $('#<%=txtCode.ClientID %>').val(),

                      success: function (data) {

                          response($.map(data, function (item) {
                              $('#<%=hfOuterPostCodeID.ClientID %>').val(item.OutPostCodeID);
                              return {

                                  label: item.OutPostCode + (item.City ? ", " + item.City : "") + ", " + item.Country,
                                  value: item.OutPostCode
                              }
                          }));
                      }

                  });
              },

              response: function (event, ui) {
                  //alert(ui.content.length);
                  // ui.content is the array that's about to be sent to the response callback.
                  if (ui.content.length == 0) {
                      $("#empty-message1").text("No results found");
                  } else {
                      $("#empty-message1").empty();
                  }
              },

              select: function (event, ui) {
                  
                 
              },

              open: function () {

                  $(this).removeClass("ui-corner-all").addClass("ui-corner-top");

              },

              close: function () {

                  $(this).removeClass("ui-corner-top").addClass("ui-corner-all");

              }

          });
          function ShowPostPopUp() {
              $('#<%=txtCode.ClientID %>').val(''); 
              $('#<%=txtValidDate.ClientID %>').val('');
              $('#<%=txtValidDate.ClientID %>').css("display", "none");
              $('#<%=lblValidDate.ClientID %>').css("display", "none");
              $('#<%=btnSave.ClientID %>').val('Save');
              $('#<%=hfChangeScenarioToSaveNewCode.ClientID %>').val('1');
              $find('mpePostCode').show();
          }
        </script>
</asp:Content>
