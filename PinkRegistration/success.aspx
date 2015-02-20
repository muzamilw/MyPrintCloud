<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="success.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.PinkRegSuccess" %>

<%@ Register Src="PinkRegFooter.ascx" TagName="PinkRegFooter" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div class="content_area">
        <div class="left_right_padding">
            <div class="signin_heading_div PinkRegHead1">
                Acknowledgement - Pinkcards.com
            </div>
            <div class="page_border_div  rounded_corners PinkRegFramePadding">
                <div >
                <div style="padding-right:30px;">
                    <div id="SubHead" runat="server" class="PinkRegHead2" visible="true">
                        Thank you
                    </div>
                    <div class="PinkRegHead3">
                        Pink Partner - Pre-launch registration and reservation
                    </div>
                    
                    <p class="PinkRegP">
                      
Your account manager will call you within 24hrs and activate your free listing and notifications
 
   
Call us on 01753 369 069 for more information.
9 am - 5 pm Monday to Friday (GMT)
</p>
                        </div>
                </div>
                
                <div class="clear">
                </div>
                <uc1:PinkRegFooter ID="PinkRegFooter1" runat="server" />
                <script type="text/javascript">
                    function ChangeCSS() {
                        window.parent.increaseHeight();
                    }


                    function validate(chk) {



                        if ($('#' + chk).is(':checked')) {
                            return true;
                        }
                        else {
                            alert('Please accept terms and conditions to continue');
                            return false;
                        }


                    }
                </script>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphFooter">
    <div class="clearBoth">
        &nbsp;
    </div>
    <br />
    <br />
</asp:Content>
