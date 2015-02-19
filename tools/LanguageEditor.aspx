<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LanguageEditor.aspx.cs"
    Inherits="Web2Print.UI.tools.LanguageEditor" ValidateRequest="false" MasterPageFile="~/MasterPages/EmptyTheme.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <link href="MIS.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default,Select,Textbox"
        EnableRoundedCorners="false"></telerik:RadFormDecorator>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest"
        DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadInputManager1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Label1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadInputManager1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Label1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        <img src='<%=ResolveUrl("~/images/asdf.gif") %>' alt="" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadInputManager ID="RadInputManager1" EnableEmbeddedBaseStylesheet="false"
        Skin="" runat="server">
        <telerik:TextBoxSetting BehaviorID="StringBehavior" InitializeOnClient="true" EmptyMessage="type here">
        </telerik:TextBoxSetting>
        <telerik:NumericTextBoxSetting BehaviorID="CurrencyBehavior" EmptyMessage="type.."
            Type="Currency" Validation-IsRequired="true" MinValue="1" InitializeOnClient="true"
            MaxValue="100000">
        </telerik:NumericTextBoxSetting>
        <telerik:NumericTextBoxSetting InitializeOnClient="true" BehaviorID="NumberBehavior"
            EmptyMessage="type.." Type="Number" DecimalDigits="0" MinValue="0" MaxValue="100">
        </telerik:NumericTextBoxSetting>
    </telerik:RadInputManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript"> 
        <!--
            //Custom js code section used to edit records, store changes and switch the visibility of column editors

            //global variables for edited cell and edited rows ids
            var editedCell;
            var arrayIndex = 0;
            var editedItemsIds = [];

            function RowCreated(sender, eventArgs) {
                var dataItem = eventArgs.get_gridDataItem();

                //traverse the cells in the created client row object and attach dblclick handler for each of them
                for (var i = 1; i < dataItem.get_element().cells.length; i++) {
                    var cell = dataItem.get_element().cells[i];
                    if (cell) {
                        $addHandler(cell, "dblclick", Function.createDelegate(cell, ShowColumnEditor));
                    }
                }
            }
            //detach the ondblclick handlers from the cells on row disposing
            function RowDestroying(sender, eventArgs) {
                if (eventArgs.get_id() === "") return;
                var row = eventArgs.get_gridDataItem().get_element();
                var cells = row.cells;
                for (var j = 0, len = cells.length; j < len; j++) {
                    var cell = cells[j];
                    if (cell) {
                        $clearHandlers(cell);
                    }
                }
            }

            function RowClick(sender, eventArgs) {
                if (editedCell) {
                    //if the click target is table cell or span and there is an edited cell, update the value in it
                    //skip update if clicking a span that happens to be a form decorator element (having a class that starts with "rfd")
                    if ((eventArgs.get_domEvent().target.tagName == "TD") ||
                        (eventArgs.get_domEvent().target.tagName == "SPAN" && !eventArgs.get_domEvent().target.className.startsWith("rfd"))) {
                        UpdateValues(sender);
                        editedCell = false;
                    }
                }
            }
            function ShowColumnEditor() {
                editedCell = this;

                //hide text and show column editor in the edited cell
                var cellText = this.getElementsByTagName("span")[0];
                cellText.style.display = "none";

                //display the span which wrapps the hidden checkbox editor
                if (this.getElementsByTagName("span")[1]) {
                    this.getElementsByTagName("span")[1].style.display = "";
                }
                var colEditor = this.getElementsByTagName("input")[0] || this.getElementsByTagName("select")[0];
                //if the column editor is a form decorated select dropdown, show it instead of the original
                if (colEditor.className == "rfdRealInput" && colEditor.tagName.toLowerCase() == "select") colEditor = Telerik.Web.UI.RadFormDecorator.getDecoratedElement(colEditor);
                colEditor.style.display = "";
                colEditor.focus();
            }
            function StoreEditedItemId(editCell) {
                //get edited row key value and add it to the array which holds them
                var gridRow = $find(editCell.parentNode.id);
                var rowKeyValue = gridRow.getDataKeyValue("Name");
                Array.add(editedItemsIds, rowKeyValue);
            }
            function HideEditor(editCell, editorType) {
                //get reference to the label in the edited cell
                var lbl = editCell.getElementsByTagName("span")[0];

                switch (editorType) {
                    case "textbox":
                        var txtBox = editCell.getElementsByTagName("input")[0];
                        if (lbl.innerHTML != txtBox.value) {
                            lbl.innerHTML = txtBox.value;
                            editCell.style.border = "1px dashed";

                            StoreEditedItemId(editCell);
                        }
                        txtBox.style.display = "none";
                        break;
                    case "checkbox":
                        var chkBox = editCell.getElementsByTagName("input")[0];
                        if (lbl.innerHTML.toLowerCase() != chkBox.checked.toString()) {
                            lbl.innerHTML = chkBox.checked;
                            editedCell.style.border = "1px dashed";

                            StoreEditedItemId(editCell);
                        }
                        chkBox.style.display = "none";
                        editCell.getElementsByTagName("span")[1].style.display = "none";
                        break;
                    case "dropdown":
                        var ddl = editCell.getElementsByTagName("select")[0];
                        var selectedValue = ddl.options[ddl.selectedIndex].value;
                        if (lbl.innerHTML != selectedValue) {
                            lbl.innerHTML = selectedValue;
                            editCell.style.border = "1px dashed";

                            StoreEditedItemId(editCell);
                        }
                        //if the form decorator was enabled, hide the decorated dropdown instead of the original.
                        if (ddl.className == "rfdRealInput") ddl = Telerik.Web.UI.RadFormDecorator.getDecoratedElement(ddl);
                        ddl.style.display = "none";
                    default:
                        break;
                }
                lbl.style.display = "inline";
            }
            function UpdateValues(grid) {
                //determine the name of the column to which the edited cell belongs
                var tHeadElement = grid.get_element().getElementsByTagName("thead")[0];
                var headerRow = tHeadElement.getElementsByTagName("tr")[0];
                var colName = grid.get_masterTableView().getColumnUniqueNameByCellIndex(headerRow, editedCell.cellIndex);

                //based on the column name, extract the value from the editor, update the text of the label and switch its visibility with that of the column
                //column. The update happens only when the column editor value is different than the non-editable value. We also set dashed border to indicate
                //that the value in the cell is changed. The logic is isolated in the HideEditor js method
                switch (colName) {
                    case "Name":
                        HideEditor(editedCell, "textbox");
                        break;
                    case "Value":
                        HideEditor(editedCell, "textbox");
                        break;
                    case "Comments":
                        HideEditor(editedCell, "textbox");
                        break;
                    case "UnitsInStock":
                        HideEditor(editedCell, "dropdown");
                        break;
                    case "UnitsOnOrder":
                        HideEditor(editedCell, "textbox");
                        break;
                    case "Discontinued":
                        HideEditor(editedCell, "checkbox");
                        break;
                    default:
                        break;
                }
            }
            function CancelChanges() {
                if (editedItemsIds.length > 0) {
                    $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest("");
                }
                else {
                    alert("No pending changes to be discarded");
                }
                editedItemsIds = [];
            }
            function ProcessChanges() {
                //extract edited rows ids and pass them as argument in the ajaxRequest method of the manager
                if (editedItemsIds.length > 0) {
                    var Ids = "";
                    for (var i = 0; i < editedItemsIds.length; i++) {
                        Ids = Ids + editedItemsIds[i] + ":";
                    }
                    $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest(Ids);
                }
                else {
                    alert("No pending changes to be processed");
                }
                editedItemsIds = [];
                return false;
            }
            function RadGrid1_Command(sender, eventArgs) {
                //Note that this code has to be executed if you postback from external control instead from the grid (intercepting its onclientclick handler for this purpose),
                //otherwise the edited values will be lost or not propagated in the source
                if (editedItemsIds.length > 0) {
                    if (eventArgs.get_commandName() == "Sort" || eventArgs.get_commandName() == "Page" || eventArgs.get_commandName() == "Filter") {
                        if (confirm("Any unsaved edited values will be lost. Choose 'OK' to discard the changes before proceeding or 'Cancel' to abort the action and process them.")) {
                            editedItemsIds = [];
                        }
                        else {
                            //cancel the chosen action
                            eventArgs.set_cancel(true);

                            //process the changes
                            ProcessChanges();
                            editedItemsIds = [];
                        }
                    }
                }
            }

        </script>
       
    </telerik:RadCodeBlock>
    <div id="header">
        <asp:Button ID="btnSave" CssClass="BtnAlignRight rounded_corners5" runat="server"
            Text="Save" OnClientClick="return ProcessChanges();" />
        <asp:Button ID="btnRestore" CssClass="BtnAlignRight rounded_corners5" runat="server"
            Text="Restore to Original Version" OnClientClick="return window.confirm('Are you sure to revert the language file to original version? any changes made will be lost.');" OnClick="btnRestore_Click" />
    </div>
   
    <div id="content">
        <div class="lblMessage">
            <asp:Label ID="Label1" runat="server" EnableViewState="false"></asp:Label>
        </div>
        <div class="paddedContent">
            <telerik:RadGrid ID="RadGrid1" Width="100%" ShowStatusBar="True" AllowSorting="False"
                OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="None" runat="server" AutoGenerateColumns="False"
                OnItemCreated="RadGrid1_ItemCreated">
                <MasterTableView TableLayout="Fixed" DataKeyNames="Name" EditMode="InPlace" ClientDataKeyNames="Name"
                    CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Name" DataField="Name" HeaderText="Control" ReadOnly="True" Display="false"
                            HeaderStyle-Width="15%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="Value" SortExpression="Value" HeaderText="Caption"
                            HeaderStyle-Width="60%">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Value") %>' Width="95%"
                                    Style="display: none"></asp:TextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                         <telerik:GridBoundColumn UniqueName="Comments" DataField="Comments" HeaderText="Details" ReadOnly="True"
                            HeaderStyle-Width="40%">
                        </telerik:GridBoundColumn>
                       <%-- <telerik:GridTemplateColumn UniqueName="Comments" HeaderText="Details" SortExpression="Comments"
                            HeaderStyle-Width="25%">
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                                <asp:TextBox ID="txtBoxComments" runat="server" Text='<%# Bind("Comments") %>' Width="95%"
                                    Style="display: none"></asp:TextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents OnRowCreated="RowCreated" OnRowClick="RowClick" OnCommand="RadGrid1_Command"
                        OnRowDestroying="RowDestroying"></ClientEvents>
                </ClientSettings>
            </telerik:RadGrid>
        </div>
         <script>
             function checkForChanges() {

                 return true;
             }
    </script>
    </div>
</asp:Content>
