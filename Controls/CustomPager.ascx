<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPager.ascx.cs" Inherits="Web2Print.UI.Controls.CustomPager" %>

<div id="PnlPagerControl" class="Width100Percent" runat="server">

<table border="0" cellpadding="0" cellspacing="0" class="Width100Percent textAlignRight">
    <tr>
        <td>
            <div id="PagerRighArea">
                <div class="divTblLayout Width100Percent">
                    <div class="tblRow">
                        <div class="divCell">
                            <asp:LinkButton ID="lnkBtnPreviouBullet" runat="server"  CssClass="imgPreviousBullet"
                                Enabled="false"  />
                        </div>
                        <div class="divCell">
                            <div id="PagerPageDisplayInfo">
                               <asp:Label  ID="lblPageInfoDisplay" CssClass="simpleText" runat="server" Text="1 of 1" />
                            </div>
                        </div>
                        <div class="divCell">
                            <asp:LinkButton ID="lnkBtnNextBullet" runat="server"  CssClass="imgNextBullet" Enabled="false" />
                        </div>
                    </div>
                </div>
            </div>
        </td>
    </tr>
</table>
</div>