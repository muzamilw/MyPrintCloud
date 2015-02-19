<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" EnableEventValidation="false" Inherits="Web2Print.UI.WebForm1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/MessageBoxTwo.ascx" TagName="MessageBoxTwo" TagPrefix="uc6" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Controls/ResetPassword.ascx" TagName="MatchingSet" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui.css" rel="stylesheet" />
    <link href="App_Themes/S6/Site.css" rel="stylesheet" />
    <style type="text/css">
        .textBox_Price
        {
            width: 40px;
            border: none;
            font-size: 11px;
            float: right;
            text-align: right;
            height: 35px;
        }

        .textBox_PriceStocks
        {
            border: none;
            font-size: 11px;
            float: left;
            text-align: left;
            height: 35px;
        }

        .product_detail_HeaderCell_Inline
        {
            font-size: 11px;
            border: 1px solid #3d3d3d;
            text-align: center;
            border-right: none;
        }

        .product_detail_HeaderCell_Repeater
        {
            color: black;
            font-size: 11px;
            border: 1px solid #3d3d3d;
            border-radius: 2px;
            border-top: none;
            border-left: none;
            text-align: right;
            padding: 0px;
            padding-right: 3px;
        }

        .product_detail_Header_Rows
        {
            background: #3d3d3d;
            color: #FFFFFF;
        }

        .product_detail_Repeater_Rows_Alternative
        {
            background: #C0C0C0;
        }

            .product_detail_Repeater_Rows_Alternative input
            {
                background: #C0C0C0;
            }
    </style>

</head>
<body style="background-color: #FFFFFF;">

    <div onclick="closeMS();" class="MesgBoxBtnsDisplay_subscriber rounded_corners">Close</div>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upMessageBox2" runat="server">
            <ContentTemplate>
                <uc6:MessageBoxTwo ID="MessageBox2" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgressUserProfile" runat="server">
            <ProgressTemplate>
                <asp:Panel ID="Panel2" CssClass="loader" runat="server">
                    <div id="lodingDiv" class="FUPUp LCLB">
                        <div style="background-color: White; height: 100px;">
                            <br />
                            <div id="loaderimageDiv" style="padding-top: 15px;">
                                <img src='<%=ResolveUrl("~/images/asdf.gif") %>' alt="" />
                            </div>
                            <br />
                            <div id="lodingBar" style="text-align: center;">
                                Loading please wait....
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:HiddenField runat="server" ID="hfPriceMatrixChange" Value="0" />
        <asp:HiddenField runat="server" ID="hfIsTextBoxChange" Value="0" />
        <div style="background-color: White; min-height: 477px; max-width: 100%; padding: 5px; padding-top: 7px; height: 850px;">
            <div class="left_align Broker_Panel_padding" style="border-bottom: 1px solid #f3f3f3; margin-top: 20px;">
                <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText" Text="Edit Product Detail"></asp:Label>
                <asp:Label ID="errorMesg" runat="server" Style="font-size: 18px; margin-left: 171px;"></asp:Label>
            </div>
            <div class="left_align Broker_Panel_padding" style="padding-right: 0px !important; padding-top: 25px;">
                <asp:Label ID="lblProductName" runat="server" CssClass="FileUploadHeaderText"></asp:Label>

                <asp:Button ID="btnClosePopup" runat="server" ClientIDMode="Static"
                    Style="display: none;" CausesValidation="False" UseSubmitBehavior="False" OnClick="btnClosePopup_Click" />
                <asp:Button ID="btnSaveAdditionalMarkup" runat="server"
                    CssClass="saveProducts rounded_corners" Text="Save" Style="margin-right: 23px !important; float: right; display: block; margin-bottom: 10px;"
                    OnClick="btnSaveAdditionalMarkup_Click" CausesValidation="False" UseSubmitBehavior="False" />


            </div>
            <p style="text-align: left; margin-left: 10px;">
                <span style="font-size: 11px;">* Double click on the price to modify</span>
            </p>

            <input type="hidden" id="txtHiddenValueChanged" value="0"
                runat="server" />

            <div class="textAlignLeft" style="margin-left: 13px;">
                <table width="97.5%" cellpadding="5" cellspacing="0" class="Broker_Panel_padding ">
                    <tr class="product_detail_Header_Rows">
                        <td class="product_detail_HeaderCell_Inline" id="divQuantity"
                            runat="server" style="width: 50px;">
                            <asp:Literal runat="server" ID="ltrlQuantity" Text="Quantity"></asp:Literal>
                        </td>
                        <td class="product_detail_HeaderCell_Inline" id="divQtyTo"
                            runat="server" style="width: 50px; display: none; margin-top: 13px;">
                            <asp:Literal runat="server" ID="ltrQtyTo" Text="Qty To"></asp:Literal>
                        </td>
                        <td id="tdHeadText1" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText1" runat="server" />
                        </td>
                        <td id="tdEndUserPriceMatt" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText1" runat="server" />
                        </td>
                        <td id="tdHeadText2" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText2" runat="server" />
                        </td>
                        <td id="tdEndUserPriceGlossy" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText2" runat="server" />
                        </td>
                        <td id="tdHeadText3" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText3" runat="server" />
                        </td>
                        <td id="tdEndUserPricePremium" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText3" runat="server" />
                        </td>

                        <td id="tdHeadText4" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText4" runat="server" />
                        </td>

                        <td id="tdEndUserPrice4" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText4" runat="server" />
                        </td>

                        <td id="tdHeadText5" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText5" runat="server" />
                        </td>

                        <td id="tdEndUserPrice5" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText5" runat="server" />
                        </td>

                        <td id="tdHeadText6" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText6" runat="server" />
                        </td>

                        <td id="tdEndUserPrice6" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText6" runat="server" />

                        </td>

                        <td id="tdHeadText7" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText7" runat="server" />
                        </td>
                        <td id="tdEndUserPrice7" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText7" runat="server" />
                        </td>

                        <td id="tdHeadText8" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText8" runat="server" />
                        </td>
                        <td id="tdEndUserPrice8" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText8" runat="server" />
                        </td>

                        <td id="tdHeadText9" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText9" runat="server" />
                        </td>
                        <td id="tdEndUserPrice9" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText9" runat="server" />
                        </td>

                        <td id="tdHeadText10" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText10" runat="server" />
                        </td>
                        <td id="tdEndUserPrice10" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText10" runat="server" />
                        </td>

                        <td id="tdHeadText11" runat="server" style="border-right: 1px solid #00967A;" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblHeaderText11" runat="server" />
                        </td>
                        <td id="tdEndUserPrice11" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblEndUSerHeaderText11" runat="server" />
                        </td>
                    </tr>

                    <asp:Repeater ID="rptPriceMatrix" runat="server" OnItemDataBound="rptPriceMatrix_ItemDataBound">
                        <AlternatingItemTemplate>
                            <tr class="product_detail_Repeater_Rows_Alternative">
                                <td class="product_detail_HeaderCell_Repeater" style="text-align: center !important; border-left: 1px solid #3d3d3d; border-right: 1px solid #3d3d3d;">
                                    <input type="hidden" id="txtHiddenPriceMatrixID" value='<%# Eval("PriceMatrixID") %>'
                                        runat="server" />
                                    <input type="hidden" id="txtHiddenIsDiscounted" value='<%# Eval("IsDiscounted") %>'
                                        runat="server" />
                                    <input type="hidden" id="hidCCID" value='<%# Eval("ContactCompanyID") %>' runat="server" />
                                    <input type="hidden" id="hidQuantity" value='<%# Eval("Quantity") %>' runat="server" />
                                    <input type="hidden" id="hidQtyRangeFrom" value='<%# Eval("QtyRangeFrom") %>' runat="server" />
                                    <input type="hidden" id="hidRangeTo" value='<%# Eval("QtyRangeTo") %>' runat="server" />

                                    <input type="hidden" id="hidFlagID" value='<%# Eval("FlagID") %>' runat="server" />
                                    <input type="hidden" id="hidDiscounted" value='<%# Eval("IsDiscounted") %>' runat="server" />
                                    <input type="hidden" id="hidSuppID" value='<%# Eval("SupplierID") %>' runat="server" />
                                    <input type="hidden" id="hidSuppSequence" value='<%# Eval("SupplierSequence") %>' runat="server" />
                                    <input type="hidden" id="hidPrice" value='<%# Eval("Price") %>' runat="server" />
                                    <input type="hidden" id="hidItemID" value='<%# Eval("ItemID") %>' runat="server" />

                                    <div id="divQty" runat="server">
                                        <asp:TextBox ID="txtQuantity" runat="server" Style="width: 50px !important;" onkeypress="return allowOnlyNumber(event);" CssClass="textBox_Price" Text='<%# Eval("Quantity") %>' onchange="TextBoxChanges();" />
                                    </div>
                                    <div id="divQtyFrom" runat="server">
                                        <asp:TextBox ID="txtQtyFrom" runat="server" Style="width: 50px !important;" onkeypress="return allowOnlyNumber(event);" CssClass="textBox_Price" Text='<%# Eval("QtyRangeFrom") %>' onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdQtyTo" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="divQtyToColumn" runat="server">
                                        <asp:TextBox ID="txtQtyTo" runat="server" Style="width: 50px !important;" CssClass="textBox_Price" onkeypress="return allowOnlyNumber(event);" Text='<%# Eval("QtyRangeTo") %>' onchange="TextBoxChanges();" />
                                    </div>

                                </td>
                                <td id="td1" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn1" runat="server">
                                        <asp:Label ID="lblMatt" runat="server" CssClass="GreyText" Visible="false" />

                                        <asp:TextBox ID="txtMatt" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdMattEndUser" runat="server" class="product_detail_HeaderCell_Repeater"
                                    visible="false">
                                    <div id="matrixItemColumn11" runat="server">
                                        <asp:Label ID="lblMattEndUser" runat="server" CssClass="greenText" />

                                    </div>
                                </td>
                                <td id="td2" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn2" runat="server">
                                        <asp:Label ID="lblGlossy" runat="server" CssClass="GreyText" Visible="false" />

                                        <asp:TextBox ID="txtGlossy" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdGlossyEndUser" runat="server" class="product_detail_HeaderCell_Repeater"
                                    visible="false">
                                    <div>
                                        <asp:Label ID="lblGlossyEndUser" runat="server" CssClass="greenText" />

                                    </div>
                                </td>
                                <td id="td3" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn3" runat="server">
                                        <asp:Label ID="lblPremiumMatt" runat="server" CssClass="GreyText" Visible="false" />

                                        <asp:TextBox ID="txtPremiumMatt" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdPremiumMattEndUser" runat="server" class="product_detail_HeaderCell_Repeater"
                                    visible="false">
                                    <div id="Div1" runat="server">
                                        <asp:Label ID="lblPremiumMattEndUser" runat="server" CssClass="greenText" />
                                    </div>
                                </td>
                                <td id="td4" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div4" runat="server">
                                        <asp:Label ID="lblPrice4" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice4" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser4" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div5" runat="server">
                                        <asp:Label ID="lblEndUserPrice4" runat="server" />

                                    </div>
                                </td>

                                <td id="td5" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div6" runat="server">
                                        <asp:Label ID="lblPrice5" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice5" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser5" visible="false" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div7" runat="server">
                                        <asp:Label ID="lblEndUserPrice5" runat="server" />
                                    </div>
                                </td>


                                <td id="td6" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div8" runat="server">
                                        <asp:Label ID="lblPrice6" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice6" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser6" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div9" runat="server">
                                        <asp:Label ID="lblEndUserPrice6" runat="server" />
                                    </div>
                                </td>

                                <td id="td7" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div10" runat="server">
                                        <asp:Label ID="lblPrice7" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice7" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser7" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div11" runat="server">
                                        <asp:Label ID="lblEndUserPrice7" runat="server" />
                                    </div>
                                </td>

                                <td id="td8" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div12" runat="server">
                                        <asp:Label ID="lblPrice8" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice8" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser8" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div13" runat="server">
                                        <asp:Label ID="lblEndUserPrice8" runat="server" />
                                    </div>
                                </td>

                                <td id="td9" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div14" runat="server">
                                        <asp:Label ID="lblPrice9" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice9" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser9" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div15" runat="server">
                                        <asp:Label ID="lblEndUserPrice9" runat="server" />
                                    </div>
                                </td>

                                <td id="td10" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div16" runat="server">
                                        <asp:Label ID="lblPrice10" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice10" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser10" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div17" runat="server">
                                        <asp:Label ID="lblEndUserPrice10" runat="server" />
                                    </div>
                                </td>
                                <td id="td11" runat="server" class="product_detail_HeaderCell_Repeater" style="border-right: 1px solid #00967A;">
                                    <div id="Div18" runat="server">
                                        <asp:Label ID="lblPrice11" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice11" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser11" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div19" runat="server">
                                        <asp:Label ID="lblEndUserPrice11" runat="server" />
                                    </div>
                                </td>
                            </tr>

                        </AlternatingItemTemplate>
                        <ItemTemplate>

                            <tr>
                                <td class="product_detail_HeaderCell_Repeater" style="text-align: center !important; border-left: 1px solid #3d3d3d; border-right: 1px solid #3d3d3d">
                                    <input type="hidden" id="txtHiddenPriceMatrixID" value='<%# Eval("PriceMatrixID") %>'
                                        runat="server" />
                                    <input type="hidden" id="txtHiddenIsDiscounted" value='<%# Eval("IsDiscounted") %>'
                                        runat="server" />
                                    <input type="hidden" id="hidCCID" value='<%# Eval("ContactCompanyID") %>' runat="server" />
                                    <input type="hidden" id="hidQuantity" value='<%# Eval("Quantity") %>' runat="server" />
                                    <input type="hidden" id="hidQtyRangeFrom" value='<%# Eval("QtyRangeFrom") %>' runat="server" />
                                    <input type="hidden" id="hidRangeTo" value='<%# Eval("QtyRangeTo") %>' runat="server" />

                                    <input type="hidden" id="hidFlagID" value='<%# Eval("FlagID") %>' runat="server" />
                                    <input type="hidden" id="hidDiscounted" value='<%# Eval("IsDiscounted") %>' runat="server" />
                                    <input type="hidden" id="hidSuppID" value='<%# Eval("SupplierID") %>' runat="server" />
                                    <input type="hidden" id="hidSuppSequence" value='<%# Eval("SupplierSequence") %>' runat="server" />
                                    <input type="hidden" id="hidPrice" value='<%# Eval("Price") %>' runat="server" />
                                    <input type="hidden" id="hidItemID" value='<%# Eval("ItemID") %>' runat="server" />

                                    <div id="divQty" runat="server">
                                        <asp:TextBox ID="txtQuantity" runat="server" Style="width: 50px !important;" onkeypress="return allowOnlyNumber(event);" CssClass="textBox_Price" Text='<%# Eval("Quantity") %>' onchange="TextBoxChanges();" />
                                    </div>
                                    <div id="divQtyFrom" runat="server">
                                        <asp:TextBox ID="txtQtyFrom" runat="server" Style="width: 50px !important;" onkeypress="return allowOnlyNumber(event);" CssClass="textBox_Price" Text='<%# Eval("QtyRangeFrom") %>' onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdQtyTo" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="divQtyToColumn" runat="server">
                                        <asp:TextBox ID="txtQtyTo" runat="server" Style="width: 50px !important;" onkeypress="return allowOnlyNumber(event);" Text='<%# Eval("QtyRangeTo") %>' CssClass="textBox_Price" onchange="TextBoxChanges();" />
                                    </div>

                                </td>
                                <td id="td1" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn1" runat="server">
                                        <asp:Label ID="lblMatt" runat="server" CssClass="GreyText" Visible="false" />

                                        <asp:TextBox ID="txtMatt" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdMattEndUser" runat="server" class="product_detail_HeaderCell_Repeater"
                                    visible="false">
                                    <div id="matrixItemColumn11" runat="server">
                                        <asp:Label ID="lblMattEndUser" runat="server" CssClass="greenText" />

                                    </div>
                                </td>
                                <td id="td2" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn2" runat="server">
                                        <asp:Label ID="lblGlossy" runat="server" CssClass="GreyText" Visible="false" />

                                        <asp:TextBox ID="txtGlossy" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdGlossyEndUser" runat="server" class="product_detail_HeaderCell_Repeater"
                                    visible="false">
                                    <div>
                                        <asp:Label ID="lblGlossyEndUser" runat="server" CssClass="greenText" />

                                    </div>
                                </td>
                                <td id="td3" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn3" runat="server">
                                        <asp:Label ID="lblPremiumMatt" runat="server" CssClass="GreyText" Visible="false" />

                                        <asp:TextBox ID="txtPremiumMatt" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdPremiumMattEndUser" runat="server" class="product_detail_HeaderCell_Repeater"
                                    visible="false">
                                    <div id="Div1" runat="server">
                                        <asp:Label ID="lblPremiumMattEndUser" runat="server" CssClass="greenText" />
                                    </div>
                                </td>
                                <td id="td4" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div4" runat="server">
                                        <asp:Label ID="lblPrice4" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice4" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser4" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div5" runat="server">
                                        <asp:Label ID="lblEndUserPrice4" runat="server" />

                                    </div>
                                </td>

                                <td id="td5" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div6" runat="server">
                                        <asp:Label ID="lblPrice5" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice5" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser5" visible="false" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div7" runat="server">
                                        <asp:Label ID="lblEndUserPrice5" runat="server" />
                                    </div>
                                </td>


                                <td id="td6" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div8" runat="server">
                                        <asp:Label ID="lblPrice6" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice6" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser6" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div9" runat="server">
                                        <asp:Label ID="lblEndUserPrice6" runat="server" />
                                    </div>
                                </td>

                                <td id="td7" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div10" runat="server">
                                        <asp:Label ID="lblPrice7" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice7" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser7" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div11" runat="server">
                                        <asp:Label ID="lblEndUserPrice7" runat="server" />
                                    </div>
                                </td>

                                <td id="td8" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div12" runat="server">
                                        <asp:Label ID="lblPrice8" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice8" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser8" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div13" runat="server">
                                        <asp:Label ID="lblEndUserPrice8" runat="server" />
                                    </div>
                                </td>

                                <td id="td9" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div14" runat="server">
                                        <asp:Label ID="lblPrice9" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice9" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser9" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div15" runat="server">
                                        <asp:Label ID="lblEndUserPrice9" runat="server" />
                                    </div>
                                </td>

                                <td id="td10" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div16" runat="server">
                                        <asp:Label ID="lblPrice10" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice10" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser10" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div17" runat="server">
                                        <asp:Label ID="lblEndUserPrice10" runat="server" />
                                    </div>
                                </td>
                                <td id="td11" runat="server" class="product_detail_HeaderCell_Repeater" style="border-right: 1px solid #00967A;">
                                    <div id="Div18" runat="server">
                                        <asp:Label ID="lblPrice11" runat="server" Visible="false" />

                                        <asp:TextBox ID="txtPrice11" runat="server" CssClass="textBox_Price" onblur="RoundPriceValues(this.id);" onchange="TextBoxChanges();" />
                                    </div>
                                </td>
                                <td id="tdEndUser11" runat="server" visible="false" class="product_detail_HeaderCell_Repeater">
                                    <div id="Div19" runat="server">
                                        <asp:Label ID="lblEndUserPrice11" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </table>
            </div>
            <div style="text-align: left; border-bottom: 1px solid #f3f3f3;
padding-bottom: 20px;font-size: 12px; color: #756B6B;">

                <asp:Button ID="btnResetPriceMatrix" runat="server" ClientIDMode="Static"
                    CssClass="saveProducts rounded_corners" Text="Reset" Style="float: right; margin-right: 23px; width: 70px; display: block; margin-top: 20px; margin-left: 14px;"
                    OnClientClick="return ConfirmReset();"
                    OnClick="btnResetPriceMatrix_Click" CausesValidation="False" UseSubmitBehavior="False" />
                <div class="clearBoth"></div>
            </div>
            <div style="margin-left: 13px;">
                <p id="hdng" style="font-style: normal; color: rgb(102,102,102); width: 100%; text-align: left; font-size: 22px; line-height: 22px; float: left; margin-top: 20px;"
                    runat="server">
                    Stock Labels
                </p>
                <p style="text-align: left; font-size: 11px;">
                    * Double click on the stock label to modify
                </p>
                <table width="97.5%" cellpadding="5" cellspacing="0" class="Broker_Panel_padding ">

                    <tr class="product_detail_Header_Rows">

                        <td id="tdItemName" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblItemName" Text="Actual Stock" runat="server" />
                        </td>
                        <td id="tdStockLabel" runat="server" class="product_detail_HeaderCell_Inline">
                            <asp:Label ID="lblStockLabel" Text="Stock Label" runat="server" />
                        </td>

                    </tr>


                    <asp:Repeater ID="rptStockLabels" runat="server">
                        <AlternatingItemTemplate>
                            <tr class="product_detail_Repeater_Rows_Alternative">
                                <td class="product_detail_HeaderCell_Repeater" style="text-align: center !important; border-left: 1px solid #3d3d3d; border-right: 1px solid #3d3d3d;">

                                    <input type="hidden" id="hidCCID" value='<%# Eval("ContactCompanyID") %>' runat="server" />
                                    <input type="hidden" id="hidOptionSequence" value='<%# Eval("OptionSequence") %>' runat="server" />
                                    <input type="hidden" id="hidItemStockOptionID" value='<%# Eval("ItemStockOptionID") %>' runat="server" />
                                    <input type="hidden" id="hdStockID" value='<%# Eval("StockID") %>' runat="server" />
                                    <input type="hidden" id="hidItemID" value='<%# Eval("ItemID") %>' runat="server" />

                                    <asp:TextBox ID="txtItem" runat="server" Style="/*width: 488px !important; */ margin-left: 3px;" Enabled="false" CssClass="textBox_PriceStocks" Text='<%# Eval("ItemName")  %>' />


                                </td>

                                <td id="td1" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn1" runat="server">
                                        <asp:TextBox ID="txtStockLabel" runat="server" Style="/*width: 488px !important; */ margin-left: 3px;" Text='<%# Eval("StockLabels") %>' CssClass="textBox_PriceStocks" onchange="TextBoxChangesForStocks();" />
                                    </div>
                                </td>



                            </tr>

                        </AlternatingItemTemplate>
                        <ItemTemplate>

                            <tr>
                                <td class="product_detail_HeaderCell_Repeater" style="text-align: center !important; border-left: 1px solid #3d3d3d; border-right: 1px solid #3d3d3d">

                                    <input type="hidden" id="hidCCID" value='<%# Eval("ContactCompanyID") %>' runat="server" />
                                    <input type="hidden" id="hidOptionSequence" value='<%# Eval("OptionSequence") %>' runat="server" />
                                    <input type="hidden" id="hidItemStockOptionID" value='<%# Eval("ItemStockOptionID") %>' runat="server" />
                                    <input type="hidden" id="hdStockID" value='<%# Eval("StockID") %>' runat="server" />
                                    <input type="hidden" id="hidItemID" value='<%# Eval("ItemID") %>' runat="server" />

                                    <%--<asp:Label ID="lblItem" runat="server" Style="width: 50px !important; text-align:left;"  Text='<%# Eval("ItemName") %>'  />--%>
                                    <asp:TextBox ID="txtItem" runat="server" Style="/*width: 488px !important; */ margin-left: 3px; background-color: white;" Enabled="false" CssClass="textBox_PriceStocks" Text='<%# Eval("ItemName") %>' />


                                </td>


                                <td id="td1" runat="server" class="product_detail_HeaderCell_Repeater">
                                    <div id="matrixItemColumn1" runat="server">
                                        <asp:TextBox ID="txtStockLabel" runat="server" Style="/*width: 488px !important; */ margin-left: 3px;" Text='<%# Eval("StockLabels") %>' CssClass="textBox_PriceStocks" onchange="TextBoxChangesForStocks();" />
                                    </div>
                                </td>


                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                <asp:Button ID="rstStockLabels" runat="server" ClientIDMode="Static"
                    CssClass="saveProducts rounded_corners" Text="Reset" Style="float: right; margin-right: 23px; width: 70px; display: block; margin-top: 20px; margin-left: 14px;"
                    OnClientClick="return ConfirmResetStockLabels();"
                    CausesValidation="False" UseSubmitBehavior="False" OnClick="rstStockLabels_Click" />
                <div class="clearBoth"></div>
            </div>

            <div id="DivSide1" style="clear: both; border-bottom: 1px solid #f3f3f3;
height: 1px;
margin-top: 20px;">
                <%--<asp:Label ID="errorMsgImages"  runat="server" Style="font-size: 14px;"></asp:Label>--%>
                <div style="float: left; display: inline; margin-left: 13px; margin-top:20px;  margin-bottom: 30px;">
                    <p id="productThumb" style="font-style: normal; color: rgb(102,102,102); font-size: 22px; line-height: 22px; float: left; text-align: left;" runat="server">Product Thumbnail <span style="font-size: 12px; font-weight: normal">(png file format, 300 x 200 pixels)</span> </p>
                    <div style="margin-top: 21px; margin-left: 13px">
                        <div style="width: 311px; height: 208px; margin-top: 44px;">
                            <img id="imgProductThumbnail1" runat="server" style="float: left; width: auto; height: 206px; max-width: 311px; margin-top: -10px;" onclick="" />
                        </div>
                        <input id="uploadImage" runat="server" accept=".png" type="file" name="myPhoto" style="margin-top: 0px; margin-right: -54px;" onchange="PreviewImage();" />


                    </div>
                </div>

                <div style="display: block; clear: both; margin-right: 28px; margin-top: 30px;">
                    <p id="productThumb2" style="font-style: normal; color: rgb(102,102,102); font-size: 22px; line-height: 22px; float: left; margin-left: 13px;" runat="server">Product Banner <span style="font-size: 12px; font-weight: normal">(png file format, 1000 x 300 pixels Max)</span></p>
                    <div style="margin-top: 21px; margin-right: 115px;">
                        <div style="width: 311px; height: 208px;">
                            <img id="imgProductDetail" runat="server" style="float: left; width: auto; max-width: 311px; height: 206px; margin-left: 26px; margin-top: 3px;" />
                        </div>
                        <input id="uploadImageDetail" runat="server" accept=".png" type="file" style="margin-left: 29px; margin-top: 46px;" name="myPhoto" onchange="PreviewImageThumbnail();" />
                    </div>
                </div>
                <asp:Button ID="rstImages" runat="server" ClientIDMode="Static"
                    CssClass="saveProducts rounded_corners" Text="Reset" Style="float: right; margin-right: 23px; width: 70px; display: block; margin-top: 20px; margin-left: 14px;"
                    OnClientClick="return ConfirmResetImages();"
                    CausesValidation="False" UseSubmitBehavior="False" OnClick="rstImages_Click" />
                <div class="clearBoth">

                </div>
            </div>

            <div style="clear:both;border-bottom: 1px solid #f3f3f3;
height: 1px;
padding-top: 20px;">
                &nbsp;
            </div>
            <div style="font-style: normal; color: rgb(102,102,102); font-size: 22px; line-height: 22px; clear: both; margin-top: 15px; text-align: left; margin-left: 13px; margin-bottom: 20px;">
                <p id="ProdDescription">Product Description</p>


            </div>
           
            <div id="tabs" style="width: 910px; margin-left: 14px;">

                <ul>
                    <li><a href="#tabs-1">Product Specs</a></li>
                    <li><a href="#tabs-2">Marketing Tips</a></li>
                    <li><a href="#tabs-3">More Ideas</a></li>
                    <li><a href="#tabs-4">Product Videos</a></li>
                </ul>
                <div id="tabs-1">
                    <p>
                        <CKEditor:CKEditorControl ID="ckEdtProductSpec" BasePath="../tools/ckeditor" Height="300"
                            Width="877" runat="server" CssClass="editorClass" Toolbar="Basic" ToolbarBasic="|Source|-|Bold|Italic|Underline|Strike|-|NumberedList|BulletedList|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|
|Link|Unlink|-|TextColor|-|Undo|Redo|Cut|Copy|Paste|PasteText|PasteFromWord|
/
|Find|Replace|SelectAll|-|Image|Table|HorizontalRule|SpecialChar|-|Format||Font||FontSize||Style|"></CKEditor:CKEditorControl>
                    </p>
                </div>
                <div id="tabs-2">
                    <p>
                        <CKEditor:CKEditorControl ID="ckEdtMarketingTips" BasePath="../tools/ckeditor" Height="300"
                            Width="877" runat="server" CssClass="editorClass" Toolbar="Basic" ToolbarBasic="|Source|-|Bold|Italic|Underline|Strike|-|NumberedList|BulletedList|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|
|Link|Unlink|-|TextColor|-|Undo|Redo|Cut|Copy|Paste|PasteText|PasteFromWord|
/
|Find|Replace|SelectAll|-|Image|Table|HorizontalRule|SpecialChar|-|Format||Font||FontSize||Style|"></CKEditor:CKEditorControl>
                    </p>
                </div>
                <div id="tabs-3">
                    <p>
                        <CKEditor:CKEditorControl ID="ckEdtMoreIdeas" BasePath="../tools/ckeditor" Height="300"
                            Width="877" runat="server" CssClass="editorClass" Toolbar="Basic" ToolbarBasic="|Source|-|Bold|Italic|Underline|Strike|-|NumberedList|BulletedList|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|
|Link|Unlink|-|TextColor|-|Undo|Redo|Cut|Copy|Paste|PasteText|PasteFromWord|
/
|Find|Replace|SelectAll|-|Image|Table|HorizontalRule|SpecialChar|-|Format||Font||FontSize||Style|"></CKEditor:CKEditorControl>
                    </p>

                </div>
                <div id="tabs-4">
                    <p>
                        <CKEditor:CKEditorControl ID="ckEdtProductVideos" BasePath="../tools/ckeditor" Height="300"
                            Width="877" runat="server" CssClass="editorClass" Toolbar="Basic" ToolbarBasic="|Source|-|Bold|Italic|Underline|Strike|-|NumberedList|BulletedList|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|
|Link|Unlink|-|TextColor|-|Undo|Redo|Cut|Copy|Paste|PasteText|PasteFromWord|
/
|Find|Replace|SelectAll|-|Image|Table|HorizontalRule|SpecialChar|-|Format||Font||FontSize||Style|"></CKEditor:CKEditorControl>
                    </p>

                </div>

            </div>
            <asp:Button ID="rstProductDescription" runat="server" ClientIDMode="Static"
                CssClass="saveProducts rounded_corners" Text="Reset" Style="float: right; margin-right: 23px; width: 70px; display: block; margin-top: 20px; margin-left: 14px;"
                OnClientClick="return ConfirmResetDescriptions();"
                CausesValidation="False" UseSubmitBehavior="False" OnClick="rstProductDescription_Click" />
            <div class="clearBoth">
                &nbsp;
            </div>
        </div>
        <asp:HiddenField ID="hfIsCompanyItem" runat="server" Value="0" />
        <asp:HiddenField ID="hfResetIds" runat="server" Value="0" />
        <asp:HiddenField ID="hfbrokermarkup" runat="server" Value="0" />
        <asp:HiddenField ID="hfContactmarkup" runat="server" Value="0" />
        <asp:HiddenField ID="hfbrokerItemId" runat="server" Value="0" />
        <asp:HiddenField ID="hfReset" runat="server" ClientIDMode="Predictable" />
        <script src="Scripts/jquery-ui-1.9.0.js" type="text/javascript"></script>
        <script src="Scripts/utilities.js" type="text/javascript"></script>

        <script type="text/javascript">
            $(document).ready(function () { });
            function ValidInputValues() {
                var Input = '';
                if (isNaN(Input) === true) {
                    $("#<%= errorMesg.ClientID %>").text('Please enter numeric characters only.');
                    // ShowPopup('Message', "Please enter numeric characters only.");
                    return false;
                }
                if (Input == 0) {
                    $("#<%= errorMesg.ClientID %>").text('Please enter correct value');
                    //ShowPopup('Message', "Please enter correct quantity.");
                    return false;
                }
            }

            $(function () {
                $("#tabs").tabs();
            });

            function IsInputCorrect() {

                var Input = '';
                alert(Input);
                if (isNaN(Input) === true) {
                    ShowPopup('Message', "Please enter numeric characters only.");
                    return false;
                }
                if (Input == 0) {
                    ShowPopup('Message', "Please enter correct quantity.");
                    return false;
                }


            }
            function RoundPriceValues(id) {
                var hid = document.getElementById("txtHiddenValueChanged");
                hid.value = "1";
                var price = document.getElementById(id).value;
                var fPrice = parseFloat(price).toFixed(2);
                document.getElementById(id).value = fPrice;
            }

            function TextBoxChanges() {
                var parentChange = document.getElementById("hfPriceMatrixChange");
                parentChange.value = "1";

                var btn = document.getElementById("btnSaveAdditionalMarkup");

                //  $("#btnSaveAdditionalMarkup").show();
                $("#btnResetPriceMatrix").css("margin-right", "2px");
                $("#btnResetPriceMatrix").show();
            }
            function TextBoxChangesForStocks() {

                var isChange = document.getElementById("hfIsTextBoxChange");
                isChange.value = "1";
            }

            function ConfirmReset() {
                var hid = document.getElementById("hfResetIds");
                hid.value = "1";
                var Msg = "<%= Resources.MyResource.lblResetPricetoBase%>";
                ShowPopupReset(Msg);

                return false;
            }
            function ConfirmResetStockLabels() {
                var hid = document.getElementById("hfResetIds");
                hid.value = "2";
                var Msg = "<%= Resources.MyResource.lblResetStockLabels%>";
                ShowPopupReset(Msg);

                return false;
            }
            function ConfirmResetImages() {
                var hid = document.getElementById("hfResetIds");
                hid.value = "3";
                var Msg = "<%= Resources.MyResource.lblResetImages%>";
                ShowPopupReset(Msg);

                return false;
            }
            function ConfirmResetDescriptions() {
                var hid = document.getElementById("hfResetIds");
                hid.value = "4";
                var Msg = "<%= Resources.MyResource.lblResetProductDescriptions%>";
                ShowPopupReset(Msg);

                return false;
            }
            function closeMS() {
                var parentChange = document.getElementById("hfPriceMatrixChange");
                //alert(parentChange.value);
                if (parentChange.value == "1") {
                    var Msg = "<%= Resources.MyResource.lblPendingPriceChangesAlert%>";
                    ShowPopupSaveChanges(Msg);
                    return false;

                }
                else {
                    //showProgress();
                    // closePopupNoRefresh();
                    parent.closePopup();
                }


            }

            function closeWithoutLoad() {
                parent.closePopup();
            }

            function closePopup() {
                var shadow = parent.document.getElementById("divShd");
                var bws = getBrowserHeight();
                shadow.style.width = bws.width + "px";
                shadow.style.height = bws.height + "px";
                var left = parseInt((bws.width - 500) / 2);
                var top = parseInt((bws.height - 200) / 2);
                //shadow = null;
                $('#' + shadow.id).css("display", "block");

                $('#UpdateProgressUserProfile').css("display", "block");

                //window.location.reload(true);
                var parentPopup = parent.document.getElementById("jqwin");

                $('#' + parentPopup.id).hide();

                parent.window.location.reload(true);
            }
            function allowOnlyNumber(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            function PreviewImage() {

                //document.getElementById("imgProductThumbnail1").visible = "true";

                var oFReader = new FileReader();
                oFReader.readAsDataURL(document.getElementById("uploadImage").files[0]);

                oFReader.onload = function (oFREvent) {
                    document.getElementById("imgProductThumbnail1").src = oFREvent.target.result;

                };
            };
            function PreviewImageThumbnail() {

                //document.getElementById("imgProductDetail").visible = "true";

                var oFReader = new FileReader();
                oFReader.readAsDataURL(document.getElementById("uploadImageDetail").files[0]);

                oFReader.onload = function (oFREvent) {
                    document.getElementById("imgProductDetail").src = oFREvent.target.result;

                };

            };

        </script>
    </form>
</body>
</html>
