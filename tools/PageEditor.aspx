<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageEditor.aspx.cs" Inherits="Web2Print.UI.tools.PageEditor"
    ValidateRequest="false" MasterPageFile="~/MasterPages/EmptyTheme.Master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphHeader">
  
    <script src="../Scripts/utilities.js" type="text/javascript"></script>
  <script src="../Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <link href="PageEditor.css" rel="stylesheet" type="text/css" />
    <link href="MIS.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CommonStyles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jdashboard.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
   
    <script language="javascript" type="text/javascript">

    $(document).ready(function () {
        $(".column").sortable({
       
            connectWith: ".column"
        });
     
      //  $(".draggableWidget").draggable({ cursor: "move", cursorAt: { top: 56, left: 56} });
         $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
      .find(".portlet-header")
        .addClass("ui-widget-header ui-corner-all")
        .prepend("<span title=Minimize class='ui-icon ui-icon-minusthick'></span><span title=Delete class='ui-icon-closebtn'></span>")
        .end()
      .find(".portlet-content");

        $(".portlet-header .ui-icon-minusthick").click(function () {
            $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
            $(this).attr("title", "Maximize");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
        });

       
        // this function fires if the delete button clicked
         $(".portlet-header .ui-icon-closebtn").click(function () {


             var result = null;
             result = confirm('Are you sure you want to delete this wiget?');
             if(result == true)
             {
                showProgress();
                $('#<%=hfCheckChanges.ClientID %>').val(1);
                
                var id = $(this).parents(".portlet").attr('id');
                $('#<%=hfDropItemId.ClientID %>').val(id);
                 $('#<%=btnRemoveWiget.ClientID %>').click();
             }
        });


        $(function () {
           $("#jDash").sortable();
            $(".jdash-column").append("<a href='#'>Add a Widget</a>");
        });

        // this function fires when the wiget dragged out for remove
        $("#jDash").sortable({
            receive: function (event, ui) {
                sortableIn = 1;
            },
            over: function (e, ui) { sortableIn = 1; },
            out: function (e, ui) { sortableIn = 0; },
            beforeStop: function (e, ui) {
                if (sortableIn == 0) {
                 var id =  $(ui.item).attr('id');
                 $('#<%=hfCheckChanges.ClientID %>').val(1);
                 $('#<%=hfDropItemId.ClientID %>').val(id);
                 $('#<%=btnRemoveWiget.ClientID %>').click();
                 ui.item.remove();
                }
            }
        });

        // this function fires when the wiget is draaged to add to the right panel
       $( ".draggableWidget" ).draggable({
        connectToSortable: ".column",
        helper: "clone",
        revert: "invalid",
        }); 
      
      // it add the wiget to the right panel
        $(".column").sortable({
            receive: function(event, ui) {
            showProgress();
               var id =  $(ui.item).attr('id');
                $('#<%=hfDropItemId.ClientID %>').val(id);
                $('#<%=hfCheckChanges.ClientID %>').val(1);
                $('#<%=btnAddWigetTPanel.ClientID %>').click();

            }
        }).disableSelection();
        
        $('.ui-icon-Editbtn').click(function() {
          
          $('#<%=div1.ClientID %>').css("display", "block");
          $('#<%=EditorPopUP.ClientID %>').css("display", "inline-block");

          var PageWidgetID = $(this).parent().prev().prev().val();
          var DynamicContent = $(this).parent().prev().val();


           $('#<%=hfDynamicContent.ClientID %>').val(PageWidgetID);
          

          //access FCK editor and pass it the dynamic content, also use the page widget id and send it to the hidden field.

          CKEDITOR.instances['<%=txtEditorDynamic.ClientID%>'].setData(DynamicContent);
        });

        $('#<%=btnCancelMessageBox.ClientID %>').click(function() {
              $('#<%=div1.ClientID %>').css("display", "none");
              $('#<%=EditorPopUP.ClientID %>').css("display", "none");
        });
    });

    // this function fires when save button clicks and save all the wiget id's
    function SaveWigetID() {
    
        var items = $("#jDash").sortable('toArray');
        $('#<%=hfItems.ClientID %>').val(items);
        showProgress();
    }

    function IfAnyChanges(){
    
        var Count = $('#<%=hfCheckChanges.ClientID %>').val();
        if(Count != 0){
            showPopup();
            return false;
        }
        else{
        showProgress();
            return true;
        }
    }

    </script>
    <div id="divShd" class="Layer">
    </div>
    <div id="div1" runat="server" class="LayerSahdow">
    </div>
    <div id="header">
        <asp:Label ID="lblPages" runat="server" CssClass="MarginTop23 btnMargin" Text="Current Page:"></asp:Label>
        <asp:DropDownList ID="ddlPages" runat="server" CssClass="MarginTop23" Style="cursor: pointer;">
        </asp:DropDownList>
        <asp:Label ID="lblStoreMode" runat="server" CssClass="MarginTop23" Text="Store Mode:"></asp:Label>
        <asp:DropDownList ID="ddlStoreModes" runat="server" CssClass="MarginTop23 Disabled"
            Style="cursor: pointer;">
        </asp:DropDownList>
        <asp:Button ID="btnLoadCOntent" runat="server" Text="Load Content" OnClientClick="return IfAnyChanges();"
            OnClick="LoadDropDownContent" CssClass="btnLoadControls rounded_corners5" />
        <asp:Button ID="btnSave" CssClass="BtnAlignRight btn_upload_design_Prod_details rounded_corners5"
            runat="server" Text="Save" OnClientClick="SaveWigetID();" OnClick="btnSave_click" />
    </div>
    <div id="content">
        <div class="lblMessage">
            <asp:Label ID="Label1" runat="server" EnableViewState="false"></asp:Label>
        </div>
        <div class="LeftPanel">
            <p style="text-align: center;">
                All Available Widgets</p>
            <div id="WidgetContainer">
                <asp:Repeater ID="rptWidgets" runat="server" OnItemCommand="rptWidgets_ItemCommand">
                    <ItemTemplate>
                        <div id='<%# Eval("WidgetID") %>' class='draggableWidget' title="Drag wiget to add to the right panel"
                            style="cursor: pointer;">
                            <%# Eval("WidgetName") %></div>
                        <asp:Button ID="btnAddWiget" runat="server" OnClientClick="showProgress();" CssClass="BtnAddImage ButtonAdd"
                            ToolTip="Add wiget" CommandArgument='<%# Eval("WidgetID") %>' CommandName="AddWiget" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div id="RightPanelDiv" class="RightPanel" runat="server">
            <div id="jDash" class="column">
                <asp:PlaceHolder runat="server" ID="pcControlsContainer"></asp:PlaceHolder>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfCheckChanges" runat="server" />
    <asp:HiddenField ID="hfDynamicContent" runat="server" />
    <asp:HiddenField ID="hfItems" runat="server" />
    <asp:HiddenField ID="hfDropItemId" runat="server" />
    <asp:Button ID="btnAddWigetTPanel" runat="server" OnClick="btnAddWigetTPanel_Click"
        Style="display: none;" />
    <asp:Button ID="btnRemoveWiget" runat="server" OnClick="btnRemoveWiget_Click" Style="display: none;" />
    <asp:Button ID="btnMessageBox" runat="server" Style="display: none; width: 0px; height: 0px" />
    <div id="EditorPopUP" class="ckEditorPanel" runat="server">
        <div class="Width100Percent">
            <div class="exit_page_container">
                <div id="btnCancelMessageBox" runat="server" class="exit_popup3">
                </div>
            </div>
        </div>
        <div id="lodingDiv" class="FUPUp LCLB rounded_corners">
            <div style="background-color: White; padding-left: 10px; padding-bottom: 10px; padding-top: 10px;">
                <div id="lodingBar" style="text-align: center;">
                    <CKEditor:CKEditorControl ID="txtEditorDynamic" BasePath="../tools/ckeditor" Height="400"
                        Width="900" runat="server" CssClass="editorClass"></CKEditor:CKEditorControl>
                </div>
                <br />
                <asp:Button ID="btnSaveDynamicText" runat="server" Text="Save" CssClass="start_creating_btn rounded_corners5 float_right MargnRght10"
                    OnClick="btnSaveDynamicText_Click" />
                <div class="clearBoth">
                    &nbsp;
                </div>
            </div>
        </div>
    </div>
</asp:Content>
