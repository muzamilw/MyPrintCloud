<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="ColorPallete.aspx.cs" Inherits="Web2Print.UI.ColorPallete" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadContents">
    <link href="Styles/colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="Styles/layout.css" rel="stylesheet" type="text/css" />
      <style>
        input[type="radio"]{display:none;outline:none !important;-webkit-transition:background-color;-moz-transition:background-color;-o-transition:background-color;-ms-transition:background-color;transition:background-color;}
input[type="radio"] + label{display:inline-block !important;padding:6px 0 6px 45px;line-height:25px;background-image:url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.png?4214");background-image:none,url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.svg?4214");background-position:-108px 0;background-repeat:no-repeat;-webkit-background-size:143px 143px;-moz-background-size:143px 143px;background-size:143px 143px;overflow:visible;outline:none;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;cursor:pointer;cursor:hand;color:#66615b;outline:none !important}
input[type="radio"]:hover + label,input[type="radio"] + label:hover,input[type="radio"]:hover + label:hover{background-position:-72px -36px;color:#403d39}
input[type="radio"]:checked + label{background-position:-36px -72px;color:#403d39}
input[type="radio"]:checked:hover + label,input[type="radio"]:checked + label:hover,input[type="radio"]:checked:hover + label:hover{background-position:0 -108px;color:#403d39}
    </style>
</asp:Content>
<asp:Content ID="VoucherWiget" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divShd" class="opaqueLayer" style="z-index: 999 !important;">
    </div>
    <div class="content_area container">
        <div class="left_right_padding row">
            <div id="MessgeToDisply" class="rounded_corners" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
            <div class="signin_heading_div float_left_simple dashboard_heading_signin">
               <asp:Label ID="lblHead" runat="server" Text="Store Logo, Background & CSS" CssClass="sign_in_heading"></asp:Label>

            </div>
            <div class="dashBoardRetrunLink">
             <uc2:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                            MyAccountCurrentPage="Store Preferences" MyAccountCurrentPageUrl="BrokerBannerWiget.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
            <%--upload logo div added by naveed--%>
            <div class="white-container-lightgrey-border rounded_corners">
                <div class="pad20"
                    <div>
                        <h3>Logo : 345px x 50px  transparent background recommended (png)
                        </h3>
                    </div>
                    <div class="divHalfLeft800">
                        <div style="width: 800px">

                            <div class="smallContctUsAvenior float_left_simple">
                                Upload your logo
                            </div>
                            <div class="TTL" style="width: 300px">
                                <asp:FileUpload ID="txtLogoUploadFile" runat="server" CssClass="newTxtBox"
                                    TabIndex="10" />


                                <asp:Button ID="btnUploadFile" runat="server" CssClass="start_creating_btn rounded_corners5"
                                    Text="Upload Image" OnClick="btnUploadFile_Click" Style="display: none;" />
                            </div>
                            
                            <div class="clearBoth"></div>
                            <div class="smallContctUsAvenior float_left_simple">
                                Your store's domain e.g. pinkcards.com
                            </div>
                            <div class="TTL" style="width: 300px">
                                 <a id="btnUpdateDomain" runat="server" class="btnupdateDomain cursor_pointer"
                                        onclick="ActiveBox();">Edit</a>
                                <asp:TextBox ID="txtdomain" runat="server" CssClass="disabledTxtBox EnableDisableBoxCs" Enabled="false"></asp:TextBox>
                               
                                <br />
                                <p style="color: red;">Do not type in "www." in the URL name</p>

                            </div>
                            <div class="clearBoth"></div>
                            <div class="smallContctUsAvenior float_left_simple">Temporary store URL
                            </div>
                            <div class="TTL" style="width: 300px; padding-top: 17px;">
                                <asp:Label ID="domainurl" runat="server"></asp:Label>
                            </div>
                            <div class="clearBoth"></div>
                          
                            <div class="lblToolTip lblTipCP">
                                <asp:Image ID="imgCustomerLogo" runat="server" CssClass="Company-logo-Palette" />
                                <br />
                                <b>Important </b>
                                <br />
                                When saving changes to the logo remember to always clear your browser cache to see the changes made.
                                
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                            <br />
                        </div>
                    </div>
                     <div id="Div8" runat="server">
                        <h3 class="headingsAvenior">Tax Settings
                        </h3>
                    </div>
                     <div class="clearBoth">
                        &nbsp;
                    </div>
                     <div id="Div9" runat="server" class="divHalfLeft800">
                           <div class="smallContctUsAvenior float_left_simple" style="margin-bottom: 13px;">
                                VAT Registered
                            </div>
                            <div class="TTL" style="width: 300px; padding-top: 12px;">
                                <asp:RadioButton ID="rdIncVat" runat="server" GroupName="vat" Text="Yes"  onclick="ShowVATPanel();" CssClass="IncVAt"/>
                                <asp:RadioButton ID="rdNoVat" runat="server" GroupName="vat" Text="No" onclick="HideVATPanel();" CssClass="NoVAt" />
                            </div>
                             <div class="clearBoth"></div>
                         <div id="ShowIfVatInc" style="margin-top: 10px; ">
                            <div class="smallContctUsAvenior float_left_simple">
                                VAT Tax Codes
                            </div>
                            <div class="TTL" style="width: 300px;">
                              <asp:DropDownList ID="ddpTaxRates" runat="server" CssClass="newTxtBox"></asp:DropDownList>
                               
                            </div>  
                         <div class="clearBoth"></div>
                            <div id="yourRegNumberlbl" runat="server" class="smallContctUsAvenior float_left_simple">
                                Your registration number
                            </div>
                            <div class="TTL" style="width: 300px">
                                <asp:TextBox ID="txtBoxVatRegNumber" runat="server" CssClass="newTxtBox"></asp:TextBox>

                            </div>
                                <div class="clearBoth"></div>
                     </div>
                        </div>
                    <div class="clearBoth"></div>
                    <div id="SocailNLinkHeading" runat="server" stye="margin-top:10px;">
                        <h3 class="headingsAvenior">Social Network Links
                        </h3>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div id="cntSocailNLink" runat="server" class="divHalfLeft800">
                        <div id="Div1" runat="server" class="smallContctUsAvenior float_left_simple">
                            Twitter URL
                        </div>
                        <div class="TTL" style="width: 300px">
                            <asp:TextBox ID="SNL1" runat="server" CssClass="newTxtBox"></asp:TextBox>

                        </div>
                        <div class="clearBoth"></div>
                        <div id="Div2" runat="server" class="smallContctUsAvenior float_left_simple">
                            Facebook URL
                        </div>
                        <div class="TTL" style="width: 300px">
                            <asp:TextBox ID="SNL2" runat="server" CssClass="newTxtBox"></asp:TextBox>

                        </div>
                        <div class="clearBoth"></div>
                        <div id="Div3" runat="server" class="smallContctUsAvenior float_left_simple">
                            LinkedIn URL
                        </div>
                        <div class="TTL" style="width: 300px">
                            <asp:TextBox ID="SNL3" runat="server" CssClass="newTxtBox"></asp:TextBox>

                        </div>
                        <div class="clearBoth"></div>
                        <%-- <div id="Div4" runat="server" class="smallContctUsAvenior float_left_simple">
                               Social Networking Link4
                            </div>
                             <div class="TTL" style="width: 300px">
                                 <asp:TextBox ID="SNL4" runat="server" CssClass="newTxtBox"></asp:TextBox>
                               
                             </div>
                            <div class="clearBoth"></div>--%>

                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div style="width: 600px; display: none">
                            <div class="Company-logoDiv-Palette">
                                <asp:Image ID="imgBackground" runat="server" CssClass="Company-logo-Palette" />
                            </div>
                            <br />
                            <div class="TLR">
                                Upload your background image
                            </div>
                            <div class="TTL">
                                <asp:FileUpload ID="txtBGImageUpload" runat="server" CssClass="file_upload_box210 rounded_corners5"
                                    TabIndex="11" />
                                <br />
                                <asp:Button ID="btnUploadBGImage" runat="server" CssClass="start_creating_btn rounded_corners5"
                                    Text="Upload Image" OnClick="btnUploadBGImage_Click" Style="display: none;" />
                            </div>
                        </div>
                        <div class="divCpContainer" style="">
                            
                            <div class="clearBoth">
                            </div>
                            <div style="padding-top: 10px; display: none;">
                                <div class="TLR">
                                    Main text heading color
                                </div>
                                <div class="TTL">
                                    <div id="customWidget1">
                                        <div id="colorSelector1" class="colorSelector2">
                                            <div id="Color2" runat="server" clientidmode="Static">
                                            </div>
                                        </div>
                                        <div class="colorpickerHolder2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <div style="padding-top: 10px; display: none;">
                                <div class="TLR">
                                    Highlight box color
                                </div>
                                <div class="TTL">
                                    <div id="customWidget2">
                                        <div id="colorSelector2" class="colorSelector2">
                                            <div id="Color3" runat="server" clientidmode="Static">
                                            </div>
                                        </div>
                                        <div class="colorpickerHolder2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <div style="width: 600px; padding-top: 20px;">
                            <div style="padding-top: 10px;">
                                <div class="TLR" style="display: none;">
                                    Edit your CSS
                                </div>
                                <div class="TTL" style="display: none;">
                                    <asp:TextBox ID="txtCSSEdit" runat="server" CssClass="rounded_corners5" Style="width: 350px; height: 300px"
                                        TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="clearBoth">
                                </div>
                               
                            </div>
                        </div>

                    </div>

                     <div id="Div4" runat="server">
                        <h3 class="headingsAvenior">Store Theme Settings
                        </h3>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>

                        <div id="Div5" runat="server" class="divHalfLeft800">
                            <div id="Div6" runat="server" class="smallContctUsAvenior float_left_simple">
                               Select theme color
                            </div>
                            <div class="TTL" style="width: 300px">
                                 <div id="colorSelector" class="colorSelector2 ">
                                            <div id="Color1" runat="server">
                                            </div>
                                        </div>

                            </div>
                        </div>

                        <div class="clearBoth">
                                </div>
                     
                    <div id="Div7" runat="server" class="divHalfLeft800" style="margin-top:15px;">
                     <div class="LogoProfile_bottom_buttons">
                                    <asp:Button ID="btnSaveCPID" runat="server" CssClass="start_creating_btn rounded_corners5 float_left_simple"
                                        Style="margin-top: 10px; margin-right:10px;" Text="Save" Width="100px" OnClick="btnSaveCPID_Click" />
                                    <%-- <asp:Button ID="btnRestore" runat="server" CausesValidation="False" Text="Restore"
                                        Style="margin-top: 10px; margin-left: 10px;" OnClick="btnRestore_Click" CssClass="start_creating_btn rounded_corners5" />--%>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5"
                                        Text="Cancel" PostBackUrl="~/DashBoard.aspx" TabIndex="15" Width="100px" Style="margin-top: 10px;" />
                                </div>

                        </div>

                  


                </div>
                <div class="clearBoth">
                </div>
                <br />
                <%--<div onclick="ShowPalletePopUp(0,'New');" class="cursor_pointer paddingTop5px" id="AddNewPContainer">
                    <div class="float_left">
                        <img alt="" class="add_new" src="images/AddNew.png" title="Add New Color Pallete" style="margin-left:3px;" /></div>
                    <div class="new_caption">
                        <asp:Label ID="lblNewPalleteText" runat="server" Text="New Color Pallete"></asp:Label>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>--%>
                <%--The Main Container--%>
                <%--<div class="ProductOrderContainer">
                    <asp:GridView ID="grdViewPallete" DataKeyNames="PalleteID" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="grdViewPallete_RowDataBound" OnRowCommand="grdViewPallete_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Palette Name" DataField="PalleteName" HeaderStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderText="1" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div id="GridCP1" class="imgArtWorkIcon rounded_corners5" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div id="GridCP2" class="imgArtWorkIcon rounded_corners5" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="3" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div id="GridCP3" class="imgArtWorkIcon rounded_corners5" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="4" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div id="GridCP4" class="imgArtWorkIcon rounded_corners5" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="5" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div id="GridCP5" class="imgArtWorkIcon rounded_corners5" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="6" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div id="GridCP6" class="imgArtWorkIcon rounded_corners5" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="7" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div id="GridCP7" class="imgArtWorkIcon rounded_corners5" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="textLeftFloating paddingRight3px">
                                        <asp:ImageButton ID="lnkBtnUpdatePalleteDetails" runat="server" ToolTip="Click To edit pallete"
                                            CssClass="rounded_corners" ImageUrl="~/images/edit.png" Height="28" Width="28"
                                            CommandName="UpdatePalleteDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PalleteID")%>' /></div>
                                    <div class="textLeftFloating">
                                        <asp:ImageButton ID="lnkBtnDeletePalleteDetails" runat="server" ToolTip="Click To delete pallete"
                                            CssClass="rounded_corners" ImageUrl="~/images/delete.png" Height="28" Width="28"
                                            CommandName="DeletePalleteDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PalleteID")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>--%>
            </div>
            <div class="clearBoth">
                <br />
            </div>
            <br />
            <%-- <div id="BannerSelectionCon" class="white_back_div rounded_corners" runat="server">
                <div id="Div1" class="Width100Percent" runat="server">
                   
                    <div class="float_left_simple paddingLeft10px">
                        <asp:Label ID="Label1" runat="server" Text="Select color Pallete for home page:"
                            CssClass="textAlignLeft Fsize13 fontSyleBold"></asp:Label>
                        <asp:DropDownList ID="ddColorPalletes" runat="server" CssClass="dropdown200 rounded_corners5"
                            AutoPostBack="true" OnSelectedIndexChanged="ddColorPalletes_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <div style="width: 230px; margin-left: 225px; margin-top: 10px;">
                            <div class="marginRight float_left_simple">
                                <div id="CP1" class="W25H25P rounded_corners5" runat="server">
                                </div>
                            </div>
                            <div class="marginRight float_left_simple">
                                <div id="CP2" class="W25H25P rounded_corners5" runat="server">
                                </div>
                            </div>
                            <div class="marginRight float_left_simple">
                                <div id="CP3" class="W25H25P rounded_corners5" runat="server">
                                </div>
                            </div>
                            <div class="marginRight float_left_simple">
                                <div id="CP4" class="W25H25P rounded_corners5" runat="server">
                                </div>
                            </div>
                            <div class="marginRight float_left_simple">
                                <div id="CP5" class="W25H25P rounded_corners5" runat="server">
                                </div>
                            </div>
                            <div class="marginRight float_left_simple">
                                <div id="CP6" class="W25H25P rounded_corners5" runat="server">
                                </div>
                            </div>
                            <div class="marginRight float_left_simple">
                                <div id="CP7" class="W25H25P rounded_corners5" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="float_left_simple">
                        <asp:Button ID="btnSaveCPID" runat="server" CssClass="start_creating_btn rounded_corners5 MargnRght10"
                            Text="Save" Width="100px" OnClick="btnSaveCPID_Click" />
                    </div>
                    <div class="float_right">
                        <asp:Image ID="HomePageImage" runat="server" CssClass="spacerbottom"
                            Width="410px" Height="200px" />
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
            </div>
            <br />--%>
            <div class="clearBoth">
                <br />
            </div>
        </div>
        <div class="clearBoth">
            <br />
        </div>
    </div>
    <asp:HiddenField ID="hfcolor1" runat="server" />
    <asp:HiddenField ID="hfcolor2" runat="server" />
    <asp:HiddenField ID="hfcolor3" runat="server" />
    <asp:HiddenField ID="hfBGImage" runat="server" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div id="jqwin" class="FileUploaderPopup_Mesgbox rounded_corners IframeCss LCLB"
        style="z-index: 999; position: fixed; display: none;">
    </div>
    <script src="js/colorpicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function HideVATPanel() {
            $("#ShowIfVatInc").css("display", "none");
        }
        function ShowVATPanel() {
            $("#ShowIfVatInc").css("display", "block");
        }

     

        function ShowPalletePopUp(PalleteID, Mode) {
            var popwidth = 710;

            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - popwidth) / 2);
            var top = parseInt((bws.height - 460) / 2);


            //        shadow = null;
            $('#divShd').css("display", "block");
            $('#jqwin').css("width", popwidth);
            $('#jqwin').css("height", 460);
            $('#jqwin').css("top", top);
            $('#jqwin').css("left", left);
            var html = '<div class="closeBtn2" onclick="closeMS()"> </div>';
            $('#jqwin').html(html + '<iframe id="ifrm" width="' + popwidth + '" height="100%" border="0" style="width:' + popwidth + 'px;height:100%;border: none; background-color: white; padding-left: 3px;" class=""></iframe>')
            $('#ifrm').attr('src', '/ColorPalleteManager.aspx?Mode=' + Mode + "&PalleteID=" + PalleteID);
            $('#jqwin').show();
            $(".closeBtn2").css("display", "block");
            return false;

        }
        function closeMS() {
            $('#divShd').css("display", "none");
            $(".closeBtn").css("display", "none");
            $('#jqwin').hide();
            $('#ifrm').attr('src', 'about:blank');
        }

        function ActiveBox() {
            $('.EnableDisableBoxCs').attr("disabled", false);
            $('.EnableDisableBoxCs').css("background-color", "#fffefd !important");
            $("#<%=btnUpdateDomain.ClientID%>").css("display", "none");
            return false;
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            CreateSingleUpload();
            if ($('input:radio[id=MainContent_rdIncVat]').is(':checked') == true) {
                ShowVATPanel();
            } else {
                HideVATPanel();
            }
        });
        function CreateSingleUpload() {
            //set up the file upload
            $("#" + "<%=txtLogoUploadFile.ClientID %>").MultiFile({
                max: 1,
                accept: 'jpg,bmp,png',
                afterFileSelect: function (element, value, master_element) {
                    $('#<%=btnUploadFile.ClientID %>').css('display', 'none');
                },
                afterFileRemove: function (element, value, master_element) {
                    $('#<%=btnUploadFile.ClientID %>').css('display', 'none');
                }
            });

                $("#" + "<%=txtBGImageUpload.ClientID %>").MultiFile({
                max: 1,
                accept: 'jpg,bmp,png',
                afterFileSelect: function (element, value, master_element) {
                    $('#<%=btnUploadBGImage.ClientID %>').css('display', 'none');
                },
                afterFileRemove: function (element, value, master_element) {
                    $('#<%=btnUploadBGImage.ClientID %>').css('display', 'none');
                }
            });
            }
    </script>
    <script type="text/javascript">
        //  Background color.
        $('#colorSelector').ColorPicker({
            color: '',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#<%=Color1.ClientID %>').css("background-color", "#" + hex);
                $('#<%=hfcolor1.ClientID %>').val('#' + hex);

            }
        });
        //  Main Text Heading color 
        $('#colorSelector1').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector1 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor2.ClientID %>').val('#' + hex);
            }
        });
        // High light box color
        $('#colorSelector2').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector2 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor3.ClientID %>').val('#' + hex);
            }
        });
    </script>
</asp:Content>
