/*
    Module with the view model for the Order.
*/
define("invoice/invoice.viewModel",
    ["jquery", "amplify", "ko", "invoice/invoice.dataservice", "invoice/invoice.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/phraseLibrary.viewModel", "common/stockItem.viewModel", "common/addCostCenter.viewModel", "common/addProduct.viewModel", "common/itemDetail.viewModel", "common/itemDetail.model", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, phraseLibrary, stockDialog, addCostCenterVM, addProductVm, itemDetailVm, itemModel, reportManager) {
        var ist = window.ist || {};
        ist.invoice = {
            viewModel: (function () {
                var // the view 
                    view,
                    // #region Arrays
                    // invoices
                    invoices = ko.observableArray([]),
                    // flag colors
                    sectionFlags = ko.observableArray([]),
                    // Markups
                    markups = ko.observableArray([]),
                    // company contacts
                    companyContacts = ko.observableArray([]),
                    // Company Addresses
                    companyAddresses = ko.observableArray([]),
                     selectedFilterFlag = ko.observable(0),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    invoiceTypes = ko.observableArray([
                        { Name: "Sale Invoice", Id: "1" },
                        { Name: "Credit Invoice", Id: "2" }
                    ]),
                    selectedCompanyTaxRate = ko.observable(),
                    // Errors List
                    errorList = ko.observableArray([]),
                    //Filter 
                    orderType = ko.observableArray([
                        { name: "ALL", value: "2" },
                        { name: "Direct  Order", value: "0" },
                        { name: "Online Order", value: "1" }
                    ]),
                    flagItem = function (state) {
                        return "<div style=\"height:20px;margin-right:10px;width:25px;float:left;background-color:" + $(state.element).data("color") + "\"></div><div>" + state.text + "</div>";
                    },
                    flagSelection = function (state) {
                        return "<span style=\"height:20px;width:25px;float:left;margin-right:10px;margin-top:5px;background-color:" + $(state.element).data("color") + "\"></span><span>" + state.text + "</span>";
                    },
                    orderTypeFilter = ko.observable(),
                    filterFlags = ko.observableArray([]),
                    invoiceCodeHeader = ko.observable(''),
                    itemCodeHeader = ko.observable(''),
                    // #endregion Arrays

                    // #region Busy Indicators
                    isLoading = ko.observable(false),
                    // Is Invoice Editor Visible
                    isDetailsVisible = ko.observable(false),
                    // Is Item Detail Visible
                    isItemDetailVisible = ko.observable(false),
                    // Is Section Detail Visible
                    isSectionDetailVisible = ko.observable(false),
                    // Is Company Base Data Loaded
                    isCompanyBaseDataLoaded = ko.observable(false),
                    currentScreen = ko.observable(),
                    // True, show status column in list view
                    isShowStatusCloumn = ko.observable(true),
                    // #endregion

                    // #region Observables
                    // Selected Estimate Phrase Container
                    selectedEstimatePhraseContainer = ko.observable(),
                    // filter
                    filterText = ko.observable(),
                    // Selected Product
                    selectedProduct = ko.observable(),
                    // Base Charge 1 Total
                    baseCharge1Total = ko.observable(0),
                    selectedMarkup1 = ko.observable(0),
                    selectedCategoryId = ko.observable(),
                    currencySymbol = ko.observable(''),
                    loggedInUserId = ko.observable(),
                    selectedCostCentre = ko.observable(),
                     sectionHeader = ko.observable(''),
                      counterForSection = -1000,
                      //Is Estimate Screen
                    isEstimateScreen = ko.observable(false),
                    // Active Order
                    selectedInvoice = ko.observable(),
                    selectedInvoiceForListView = ko.observable(),
                    // Page Header 
                    pageHeader = ko.computed(function () {
                        return selectedInvoice() && selectedInvoice().name() ? selectedInvoice().name() : 'Invoices';
                    }),
                     // Page Code 
                    pageCode = ko.computed(function () {
                        return selectedInvoice() && selectedInvoice().code() ? selectedInvoice().code() : '';
                    }),
                     defaultAddress = ko.observable(),
                    // Default Company Contact
                    defaultCompanyContact = ko.observable(),
                    // Sort On
                    sortOn = ko.observable(2),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(false),
                    // Pagination
                    pager = ko.observable(new pagination.Pagination({ PageSize: 10 }, invoices)),
                    selectedSectionCostCenter = ko.observable(),
                    selectedQty = ko.observable(),
                    // Active Order
                    //selectedOrder = ko.observable(model.Estimate.Create({})),
                    // selected Company
                    selectedCompany = ko.observable(),
                    // Selected Section
                    selectedSection = ko.observable(),
                    // Cost Centres Base Data
                    costCentresBaseData = ko.observableArray([]),
                    // Stock Category 
                    stockCategory = {
                        paper: 1,
                        inks: 2,
                        films: 3,
                        plates: 4
                    },
                    //Inventory Stock Item To Create
                    inventoryStockItemToCreate = ko.observable(),
                     //Is Inventory Dialog is opening from Order Dialog's add Product From Inventory
                    isAddProductFromInventory = ko.observable(false),
                    //Is Inventory Dialog is opening for Section Cost Center
                    isAddProductForSectionCostCenter = ko.observable(false),
                    //Is Cost Center dialog open for shipping
                    isCostCenterDialogForShipping = ko.observable(false),
                    //#endregion

                    //#region Utility Functions
                    // Select Estimate Phrase Container
                    selectEstimatePhraseContainer = function (data, e) {
                        selectedEstimatePhraseContainer(e.currentTarget.id);
                    },
                    // Open Phrase Library
                    openPhraseLibrary = function () {
                        phraseLibrary.isOpenFromPhraseLibrary(false);
                        phraseLibrary.show(function (phrase) {
                            updateEstimatePhraseContainer(phrase);
                        });
                    },
                    // update Estimate Phrase Container
                    updateEstimatePhraseContainer = function (phrase) {
                        if (!phrase) {
                            return;
                        }

                        // Set Phrase to selected Estimate Phrase Container
                        if (selectedEstimatePhraseContainer() === 'EstimateHeader') {
                            selectedInvoice().headNotes(selectedInvoice().headNotes() ? selectedInvoice().headNotes() + ' ' + phrase : phrase);
                        }
                        else if (selectedEstimatePhraseContainer() === 'EstimateFootNotesTextBox') {
                            selectedInvoice().footNotes(selectedInvoice().footNotes() ? selectedInvoice().footNotes() + ' ' + phrase : phrase);
                        }
                    },
                    // Selected Address
                    selectedAddress = ko.computed(function () {
                        if (!selectedInvoice() || !selectedInvoice().addressId() || companyAddresses().length === 0) {
                            return defaultAddress();
                        }

                        var addressResult = companyAddresses.find(function (address) {
                            return address.id === selectedInvoice().addressId();
                        });

                        return addressResult || defaultAddress();
                    }),
                     openReport = function (isFromEditor) {
                         reportManager.show(ist.reportCategoryEnums.Invoice, isFromEditor == true ? true : false, 0);
                     },
                    // Selected Company Contact
                    selectedCompanyContact = ko.computed(function () {
                        if (!selectedInvoice() || !selectedInvoice().contactId() || companyContacts().length === 0) {
                            return defaultCompanyContact();
                        }

                        var contactResult = companyContacts.find(function (contact) {
                            return contact.id === selectedInvoice().contactId();
                        });

                        return contactResult || defaultCompanyContact();
                    }),
                    selecteditem = ko.observable(),
                    editInvoice = function (data) {
                        selectedInvoiceForListView(data);
                        getInvoiceById(data.id(), openInvoiceEditor);
                    },
                    // Open Editor
                    openInvoiceEditor = function () {
                        isItemDetailVisible(false);
                        isDetailsVisible(true);
                    },
                    // On Close Editor
                    onCloseInvoiceEditor = function () {
                        if (selectedInvoice().invoiceStatus() === 19 && selectedInvoice().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(onSaveInvoice);
                            confirmation.afterCancel(function () {
                                selectedInvoice().reset();
                                closeInvoiceEditor();
                                invoiceCodeHeader('');
                                isSectionDetailVisible(false);
                                isItemDetailVisible(false);
                            });
                            confirmation.show();
                            return;
                        }
                        closeInvoiceEditor();
                    },
                    // Close Editor
                    closeInvoiceEditor = function () {
                        selectedInvoice(model.Invoice.Create({}));
                        isDetailsVisible(false);
                        errorList.removeAll();
                    },
                    // On Archive
                    onArchiveInvoice = function (invoice) {
                        confirmation.afterProceed(function () {
                            // archiveInvoice(invoice.id());
                        });
                        confirmation.show();
                    },
                    // Open Company Dialog
                    openCompanyDialog = function () {
                        companySelector.show(onSelectCompany, [0, 1, 3], true);
                    },
                    // On Select Company
                    onSelectCompany = function (company) {
                        if (!company) {
                            return;
                        }

                        if (selectedInvoice().companyId() === company.id) {
                            return;
                        }

                        selectedInvoice().companyId(company.id);
                        selectedInvoice().companyName(company.name);
                        selectedCompany(company);
                        if (company.isCustomer !== 3 && company.storeId) {
                            selectedInvoice().storeId(company.storeId);
                        }
                        else {
                            selectedInvoice().storeId(undefined);
                        }
                        // Get Company Address and Contacts
                        getBaseForCompany(company.id, (selectedInvoice().storeId() === null || selectedInvoice().storeId() === undefined) ? company.id :
                            selectedInvoice().storeId());
                    },

                    vatList = ko.observableArray([
                        {
                            name: "VAT Free",
                            id: 1,
                            tax: 0
                        },
                        { name: "VAT 20%", id: 2, tax: 20 },
                        { name: "VAT 10%", id: 3, tax: 10 }
                    ]),
                    // Close Item Detail
                    closeItemDetail = function () {
                        itemCodeHeader('');
                        sectionHeader('');
                        isItemDetailVisible(false);
                        isSectionDetailVisible(false);
                    },
                    // Save Product
                    saveProduct = function () {

                    },
                    // Delete Product
                    deleteProduct = function (item) {
                        selectedInvoice().items.remove(item);
                    },
                    removeItemSectionWithAddFlagTrue = function () {
                        _.each(selectedInvoice().items(), function (item) {
                            if (item.detailType === undefined) {
                                _.each(item.itemSections(), function (itemSection) {
                                    if (itemSection.flagForAdd()) {
                                        item.itemSections.remove(itemSection);
                                    }
                                });
                            }

                        });

                    },
                    // On Save Order
                    onSaveInvoice = function (data, event, navigateCallback) {
                        removeItemSectionWithAddFlagTrue();
                        if (!doBeforeSave()) {
                            return;
                        }

                        var istatus = selectedInvoice().invoiceStatus();
                        if (istatus == 19 && selectedInvoice().id() !== 0 && selectedInvoice().id() !== undefined) //Awaiting Invoice
                        {
                            confirmation.messageText("Do you want to post the invoice.");

                            confirmation.afterProceed(function () {
                                selectedInvoice().invoiceStatus(20); //Posted Invoice                              
                                selectedInvoice().invoicePostedBy(loggedInUserId); //Current user Id                             
                                saveInvoice(closeInvoiceEditor, navigateCallback);
                            });
                            confirmation.afterCancel(function () {
                                saveInvoice(closeInvoiceEditor, navigateCallback);
                            });
                            confirmation.show();
                            return;
                        } else {
                            saveInvoice(closeInvoiceEditor, navigateCallback);
                        }


                    },
                    // Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedInvoice().isValid()) {
                            selectedInvoice().showAllErrors();
                            selectedInvoice().setValidationSummary(errorList);
                            flag = false;
                        }

                        return flag;
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    saveSectionCostCenter = function (newItem, sectionCostCenter, selectedStockOptionParam, selectedProductQuanityParam) {
                        //var orderNewItem = new model.Item.Create(newItem.convertToServerData());
                        //newItem = orderNewItem;
                        sectionCostCenter.name('Web Order Cost Center');
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);

                        sectionCostCenter.qty2Charge(0);
                        sectionCostCenter.qty3Charge(0);
                        sectionCostCenter.qty1(selectedProductQuanityParam);
                        selectedSectionCostCenter(sectionCostCenter);
                        selectedQty(1);

                        //Item's Quantity
                        newItem.qty1(selectedProductQuanityParam);
                        //Item's Section Quantity
                        newItem.itemSections()[0].qty1(selectedProductQuanityParam);
                        newItem.itemSections()[0].sectionCostCentres.push(sectionCostCenter);

                        itemDetailVm.updateOrderData(selectedInvoice(), newItem, selectedSectionCostCenter(), selectedQty(), newItem.itemSections()[0]);

                        //#region Add Selected Addons as Cost Centers
                        if (selectedStockOptionParam != undefined && selectedStockOptionParam.itemAddonCostCentres().length > 0) {
                            _.each(selectedStockOptionParam.itemAddonCostCentres(), function (stockOption) {
                                if (stockOption.isSelected()) {
                                    sectionCostCenter = itemModel.SectionCostCentre.Create({});
                                    sectionCostCenter.costCentreId(stockOption.costCentreId());
                                    sectionCostCenter.name(stockOption.costCentreName());
                                    sectionCostCenter.qty1EstimatedStockCost(0);
                                    sectionCostCenter.qty2EstimatedStockCost(0);
                                    sectionCostCenter.qty3EstimatedStockCost(0);
                                    sectionCostCenter.qty1Charge(stockOption.totalPrice());
                                    sectionCostCenter.qty2Charge(0);
                                    sectionCostCenter.qty3Charge(0);
                                    sectionCostCenter.qty1(1);

                                    sectionCostCenter.qty1NetTotal(stockOption.totalPrice());
                                    selectedSectionCostCenter(sectionCostCenter);
                                    selectedQty(1);
                                    newItem.itemSections()[0].sectionCostCentres.push(sectionCostCenter);
                                }
                            });
                        }
                        //#endregion
                    },
                    createitemForRetailStoreProduct = function (selectedItem) {
                        var item = selectedItem.convertToServerData();
                        //item.EstimateId = orderId;
                        selectedSection(undefined);
                        var newItem = itemModel.Item.Create(item);
                        applyProductTax(newItem);
                        return newItem;
                    },
                    // Edit Item
                    editItem = function (item) {
                        // For Invoice Detail Item
                        if (item.detailType !== undefined) {
                            selectedInvoiceDetail(item);
                            view.showInvoiceDetailDialog();
                        } else {
                            // For Items
                            itemCodeHeader(item.code());
                            var itemSection = _.find(item.itemSections(), function (itemSec) {
                                return itemSec.flagForAdd() === true;
                            });
                            if (itemSection === undefined) {
                                var itemSectionForAddView = itemModel.ItemSection.Create({});
                                counterForSection = counterForSection - 1;
                                itemSectionForAddView.id(counterForSection);
                                itemSectionForAddView.flagForAdd(true);
                                item.itemSections.push(itemSectionForAddView);
                            }
                            selectedProduct(item);
                            var section = selectedProduct() != undefined ? selectedProduct().itemSections()[0] : undefined;
                            editSection(section);
                            openItemDetail();
                        }


                    },
                     // Open Item Detail
                    openItemDetail = function () {
                        isItemDetailVisible(true);
                        itemDetailVm.showItemDetail(selectedProduct(), selectedInvoice(), closeItemDetail, isEstimateScreen());
                        view.initializeLabelPopovers();
                    },
                    // Edit Section
                    editSection = function (item) {
                        sectionHeader("SECTION - " + item.sectionNo());
                        selectedSection(item);
                        openSectionDetail();

                    },
                    // Open Section Detail
                    openSectionDetail = function () {
                        view.initializeLabelPopovers();
                    },
                    //#region Add Blank Print Product
                    onCreateNewBlankPrintProduct = function () {
                        var newItem = itemModel.Item.Create({});
                        //Req: Item Product code is set to '1', so while editting item's section is mandatory
                        newItem.productType(1);
                        newItem.productName("Blank Sheet");
                        newItem.qty1(0);
                        newItem.qty1GrossTotal(0);

                        var itemSection = itemModel.ItemSection.Create({});
                        counterForSection = counterForSection - 1;
                        itemSection.id(counterForSection);
                        //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                        itemSection.productType(2);
                        newItem.itemSections.push(itemSection);
                        selectedInvoice().items.splice(0, 0, newItem);
                        //Req: Open Edit dialog of product on adding product
                        editItem(newItem);
                    },
                    //Product From Cost Center
                     createNewCostCenterProduct = function () {
                         view.hideCostCentersQuantityDialog();
                         //selectedCostCentre(costCenter);
                         var item = itemModel.Item.Create({ EstimateId: selectedInvoice().id(), RefItemId: selectedCostCentre().id() });
                         applyProductTax(item);
                         selectedProduct(item);
                         item.productName(selectedCostCentre().name());
                         item.qty1(selectedCostCentre().quantity1());
                         item.qty2(selectedCostCentre().quantity2());
                         item.qty3(selectedCostCentre().quantity3());
                         item.qty1NetTotal(selectedCostCentre().setupCost());
                         //Req: Item Product code is set to '2', so while editting item's section is non mandatory
                         item.productType(2);

                         var itemSection = itemModel.ItemSection.Create({});
                         counterForSection = counterForSection - 1;
                         itemSection.id(counterForSection);
                         itemSection.name("Text Sheet");
                         itemSection.qty1(selectedCostCentre().quantity1());
                         itemSection.qty2(selectedCostCentre().quantity2());
                         itemSection.qty3(selectedCostCentre().quantity3());
                         //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                         itemSection.productType(2);
                         itemSection.baseCharge1(selectedCostCentre().setupCost());

                         var sectionCostCenter = itemModel.SectionCostCentre.Create({});
                         sectionCostCenter.qty1(selectedCostCentre().quantity1());
                         sectionCostCenter.qty2(selectedCostCentre().quantity2());
                         sectionCostCenter.qty3(selectedCostCentre().quantity3());
                         sectionCostCenter.qty1EstimatedStockCost(0);
                         sectionCostCenter.qty2EstimatedStockCost(0);
                         sectionCostCenter.qty3EstimatedStockCost(0);
                         sectionCostCenter.costCentreId(selectedCostCentre().id());
                         sectionCostCenter.costCentreName(selectedCostCentre().name());
                         sectionCostCenter.name(selectedCostCentre().name());

                         sectionCostCenter.qty1Charge(selectedCostCentre().setupCost());
                         sectionCostCenter.qty1NetTotal(selectedCostCentre().setupCost());

                         selectedSectionCostCenter(sectionCostCenter);
                         selectedQty(1);
                         itemSection.sectionCostCentres.push(sectionCostCenter);

                         item.itemSections.push(itemSection);
                         var itemSectionForAddView = itemModel.ItemSection.Create({});
                         counterForSection = counterForSection - 1;
                         itemSectionForAddView.id(counterForSection);
                         itemSectionForAddView.flagForAdd(true);
                         item.itemSections.push(itemSectionForAddView);
                         if (isCostCenterDialogForShipping()) {
                             item.itemType(2); // Delivery Item
                             var deliveryItem = _.find(selectedInvoice().items(), function (itemWithType2) {
                                 return itemWithType2.itemType() === 2;
                             });
                             if (deliveryItem !== undefined) {
                                 selectedInvoice().items.remove(deliveryItem);
                             }

                         }

                         selectedInvoice().items.splice(0, 0, item);

                         selectedSection(itemSection);

                         //this method is calling to update orders list view total prices etc by trigering computed in item's detail view
                         itemDetailVm.updateOrderData(selectedInvoice(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());
                         if (!isCostCenterDialogForShipping()) {
                             //Req: Open Edit dialog of product on adding product
                             editItem(item);
                         }
                         
                     },
                      // Gross Total
                    grossTotal = ko.computed(function () {
                        var total = 0;
                        if (selectedInvoice() != undefined && selectedInvoice().invoiceDetailItems().length === 0) {
                            _.each(selectedInvoice().items(), function (item) {
                                var val1 = (item.qty1GrossTotal() === undefined || item.qty1GrossTotal() === null) ? 0 : item.qty1GrossTotal();
                                total = total + parseFloat(val1);
                            });
                            selectedInvoice().invoiceTotal(total);
                        }
                        return total;
                    }),
                    //#endregion
                    //#endregion

                    // #region ServiceCalls
                    // Get Base Data
                     getBaseData = function () {
                         dataservice.getBaseData({
                             success: function (data) {
                                 //Section Flags
                                 sectionFlags.removeAll();
                                 ko.utils.arrayPushAll(sectionFlags(), data.SectionFlags);
                                 sectionFlags.valueHasMutated();
                                 //System Users
                                 systemUsers.removeAll();
                                 ko.utils.arrayPushAll(systemUsers(), data.SystemUsers);
                                 systemUsers.valueHasMutated();
                                 currencySymbol(data.CurrencySymbol);
                                 loggedInUserId(data.LoggedInUserId);
                                 costCentresBaseData.removeAll();
                                 if (data.CostCenters) {
                                     ko.utils.arrayPushAll(costCentresBaseData(), data.CostCenters);
                                     costCentresBaseData.valueHasMutated();
                                 }
                                 // view.initializeLabelPopovers();

                             },
                             error: function (response) {
                                 toastr.error("Failed to load base data" + response);
                                 view.initializeLabelPopovers();
                             }
                         });
                     },
                    // Get Section flag color
                    getSectionFlagColor = function (sectionFlagId) {
                        var sectionFlg = sectionFlags.find(function (sectionFlag) {
                            return sectionFlag.id == sectionFlagId;
                        });

                        if (!sectionFlg) {
                            return undefined;
                        }

                        return sectionFlg.color;
                    },
                    // Get Invoice From list
                    getInvoiceFromList = function (id) {
                        return invoices.find(function (invoice) {
                            return invoice.id() === id;
                        });
                    },

                    setInvoiceDetailItems = function () {
                        var removeItems = [];
                        _.each(selectedInvoice().items(), function (item) {
                            if (item.detailType !== undefined) {
                                selectedInvoice().invoiceDetailItems.push(item);
                                removeItems.push(item);
                            }
                        });
                        _.each(removeItems, function (item) {
                            if (item.detailType !== undefined) {
                                selectedInvoice().items.remove(item);
                            }
                        });
                    },


                    // Save Invoice
                    saveInvoice = function (callback, navigateCallback) {
                        setInvoiceDetailItems();
                        var invoice = selectedInvoice().convertToServerData();
                        _.each(selectedInvoice().invoiceDetailItems(), function (inv) {
                            invoice.InvoiceDetails.push(inv.convertToServerData(inv));

                        });

                        var itemsArray = [];
                        _.each(selectedInvoice().items(), function (obj) {
                            var itemObj = obj.convertToServerData(); // item converted 
                            var attArray = [];
                            _.each(itemObj.ItemAttachment, function (att) {
                                var attchment = att.convertToServerData(); // item converted 
                                attchment.ContactId = selectedInvoice().contactId();
                                attArray.push(attchment);
                            });
                            itemObj.ItemAttachments = attArray;
                            itemsArray.push(itemObj);

                        });

                        invoice.Items = itemsArray;
                        dataservice.saveInvoice(invoice, {
                            success: function (data) {
                                if (!selectedInvoice().id()) {
                                    // Update Id
                                    selectedInvoice().id(data.InvoiceId);
                                    selectedInvoice().code(data.InvoiceCode);
                                    var invoiceListViewItem = model.InvoicesListView();
                                    invoiceListViewItem.code(data.InvoiceCode);
                                    invoiceListViewItem.id(selectedInvoice().id());
                                    updateInvoiceLitViewItem(invoiceListViewItem);
                                    // Add to top of list
                                    invoices.splice(0, 0, invoiceListViewItem);
                                } else {
                                    // Get Order
                                    var invoiceUpdated = getInvoiceFromList(selectedInvoice().id());
                                    if (invoiceUpdated) {
                                        // invoiceUpdated.code(data.InvoiceCode);
                                        //invoiceUpdated.name(data.InvoiceName);
                                        updateInvoiceLitViewItem(invoiceUpdated);
                                    }
                                }
                                isDetailsVisible(false);
                                isItemDetailVisible(true);

                                toastr.success("Saved Successfully.");

                                if (callback && typeof callback === "function") {
                                    callback();
                                }

                                if (navigateCallback && typeof navigateCallback === "function") {
                                    navigateCallback();
                                }
                                invoiceCodeHeader('');
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Order. Error: " + response);
                            }
                        });
                    },

                    updateInvoiceLitViewItem = function (invoiceListViewItem) {
                        invoiceListViewItem.name(selectedInvoice().name());
                        invoiceListViewItem.code(selectedInvoice().code());
                        invoiceListViewItem.type(selectedInvoice().type());
                        invoiceListViewItem.companyName(selectedInvoice().companyName());
                        invoiceListViewItem.invoiceDate(selectedInvoice().invoiceDate());
                        invoiceListViewItem.itemsCount(selectedInvoice().items().length + selectedInvoice().invoiceDetailItems().length);
                        var sectionFlagItem = _.find(sectionFlags(), function (sFlag) {
                            return sFlag.SectionFlagId === selectedInvoice().sectionFlagId();
                        });
                        if (sectionFlagItem !== undefined && sectionFlagItem !== null) {
                            invoiceListViewItem.flagColor(sectionFlagItem.FlagColor);
                        }

                        invoiceListViewItem.invoiceTotal(selectedInvoice().invoiceTotal());
                    },
                    //get Invoices Of Current Screen
                    getInvoicesOfCurrentScreen = function () {
                        getInvoices(currentScreen());
                    },
                    //Get Order Tab Changed Event
                    getInvoicesOnTabChange = function (currentTab) {
                        pager().reset();
                        if (currentTab === 0) {
                            isShowStatusCloumn(true);
                        } else {
                            isShowStatusCloumn(false);
                        }
                        getInvoices(currentTab);

                    },
                    // Map Invoices 
                    mapInvoices = function (data) {
                        var invoicesList = [];
                        _.each(data, function (invoice) {
                            //invoice.FlagColor = getSectionFlagColor(invoice.SectionFlagId);
                            invoicesList.push(model.InvoicesListView.Create(invoice));
                        });
                        // Push to Original Array
                        ko.utils.arrayPushAll(invoices(), invoicesList);
                        invoices.valueHasMutated();
                    },
                    // Get Invoices
                    getInvoices = function (currentTab) {
                        isLoading(true);
                        currentScreen(currentTab);
                        dataservice.getInvoices({
                            SearchString: filterText(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            Status: currentScreen(),
                            FilterFlag: selectedFilterFlag(),
                            OrderTypeFilter: orderTypeFilter(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                invoices.removeAll();
                                if (data && data.RowCount > 0) {
                                    mapInvoices(data.Invoices);
                                    pager().totalCount(data.RowCount);
                                }
                                isLoading(false);
                            },
                            error: function (response) {
                                isLoading(false);
                                toastr.error("Failed to load invoices" + response);
                            }
                        });
                    },
                    mapList = function (observableList, data, factory) {
                        var list = [];
                        _.each(data, function (item) {
                            list.push(factory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(observableList(), list);
                        observableList.valueHasMutated();
                    },
                    // Get Invoice By Id
                    getInvoiceById = function (id, callback) {
                        isLoading(true);
                        //isCompanyBaseDataLoaded(false);
                        dataservice.getInvoice({
                            id: id
                        }, {
                            success: function (data) {
                                if (data) {
                                    selectedInvoice(model.Invoice.Create(data));

                                    // Get Base Data For Company
                                    if (data.CompanyId) {
                                        var storeId = 0;
                                        if (data.IsCustomer !== 3 && data.StoreId) {
                                            storeId = data.StoreId;
                                            selectedInvoice().storeId(storeId);
                                        } else {
                                            storeId = data.CompanyId;
                                        }
                                        getBaseForCompany(data.CompanyId, storeId);
                                    }

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }
                                isLoading(false);
                                var code = !selectedInvoice().code() ? "INVOICE CODE" : selectedInvoice().code();
                                //invoiceCodeHeader(code);
                                //view.initializeLabelPopovers();
                                selectedInvoice().reset();
                            },
                            error: function (response) {
                                isLoading(false);
                                toastr.error("Failed to load order details" + response);
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                     // Get Company Base Data
                    getBaseForCompany = function (id, storeId) {
                        isCompanyBaseDataLoaded(false);
                        dataservice.getBaseDataForCompany({
                            id: id,
                            storeId: storeId
                        }, {
                            success: function (data) {
                                companyAddresses.removeAll();
                                companyContacts.removeAll();
                                if (data) {
                                    if (data.CompanyAddresses) {
                                        mapList(companyAddresses, data.CompanyAddresses, model.Address);
                                        setDefaultAddressForCompany();
                                    }
                                    if (data.CompanyContacts) {
                                        mapList(companyContacts, data.CompanyContacts, model.CompanyContact);
                                        setDefaultContactForCompany();
                                    }
                                    selectedCompanyTaxRate(data.TaxRate);
                                }
                                isCompanyBaseDataLoaded(true);
                            },
                            error: function (response) {
                                isCompanyBaseDataLoaded(true);
                                toastr.error("Failed to load details for selected company" + response);
                            }
                        });
                    },
                     // Select Default Address For Company in case of new Invoice
                    setDefaultAddressForCompany = function () {
                        if (selectedInvoice().id() > 0) {
                            return;
                        }
                        selectedInvoice().addressId(selectedCompany().addressId);
                    },
                     // Select Default Contact For Company in case of new Invoice
                    setDefaultContactForCompany = function () {
                        if (selectedInvoice().id() > 0) {
                            return;
                        }
                        selectedInvoice().contactId(selectedCompany().contactId);
                    },
                    createInvoice = function () {
                        selectedInvoice(model.Invoice.Create({}));
                        selectedInvoice().invoiceStatus(19);
                        selectedInvoice().invoiceReportSignedBy(loggedInUserId());
                        selectedInvoice().isPostedInvoice(false);
                        openInvoiceEditor();
                    },
                    //#region Items
                    onCreateNewProductFromRetailStore = function () {
                        if (selectedInvoice().companyId() === undefined) {
                            toastr.error("Please select customer.");
                        } else {
                            var companyId = 0;
                            if (selectedInvoice().storeId()) {
                                companyId = selectedInvoice().storeId();
                            } else {
                                companyId = selectedInvoice().companyId();
                            }
                            addProductVm.show(addItemFromRetailStore, companyId, costCentresBaseData(), currencySymbol(), selectedInvoice().id(), saveSectionCostCenter, createitemForRetailStoreProduct, selectedCompanyTaxRate(), invoiceCodeHeader(), 'Invoice');
                        }
                    },
                     addItemFromRetailStore = function (newItem) {
                         var itemSectionForAddView = itemModel.ItemSection.Create({});
                         counterForSection = counterForSection - 1;
                         itemSectionForAddView.id(counterForSection);
                         itemSectionForAddView.flagForAdd(true);
                         newItem.itemSections.push(itemSectionForAddView);
                         selectedProduct(newItem);
                         selectedInvoice().items.splice(0, 0, newItem);
                         itemDetailVm.updateOrderData(selectedInvoice(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());
                         //Req: Open Edit dialog of product on adding product
                         editItem(newItem);
                     },


                     // Open Stock Item Dialog For Adding product
                     openStockItemDialogForAddingProduct = function () {
                         isAddProductFromInventory(true);
                         isAddProductForSectionCostCenter(false);
                         if (selectedInvoice().companyId() === undefined) {
                             toastr.error("Please select customer.");
                         } else {
                             stockDialog.show(function (stockItem) {
                                 createNewInventoryProduct(stockItem);
                             }, stockCategory.paper, false, currencySymbol(), selectedCompanyTaxRate());
                         }
                     },
                    createNewInventoryProduct = function (stockItem) {
                        var costCenter = itemModel.costCentre.Create({});
                        selectedCostCentre(costCenter);
                        view.showCostCentersQuantityDialog();
                        inventoryStockItemToCreate(stockItem);
                    },
                    getStockCostCenterId = function (type) {
                        var costCentreId;
                        _.each(costCentresBaseData(), function (costCenter) {
                            if (costCenter.Type == type) {
                                costCentreId = costCenter.CostCentreId;
                            }
                        });
                        return costCentreId;
                    },
                    applyProductTax = function (item) {
                        if (item.tax1() === undefined) {
                            if (item.defaultItemTax() !== undefined && item.defaultItemTax() !== null) {
                                item.tax1(item.defaultItemTax());
                            } else if (selectedCompanyTaxRate() !== undefined && selectedCompanyTaxRate() !== null) {
                                item.tax1(selectedCompanyTaxRate());
                            } else {
                                item.tax1(0);
                                item.taxRateIsDisabled(true);
                            }
                        }
                    },
                    onSaveProductInventory = function () {
                        var item = itemModel.Item.Create({ EstimateId: selectedInvoice().id(), RefItemId: inventoryStockItemToCreate().id });
                        applyProductTax(item);
                        item.productName(inventoryStockItemToCreate().name);
                        item.qty1(selectedCostCentre().quantity1());
                        item.qty2(selectedCostCentre().quantity2());
                        item.qty3(selectedCostCentre().quantity3());
                        //Req: Item Product type is set to '2', so while editting item's section is non mandatory
                        item.productType(2);
                        item.qty1NetTotal(inventoryStockItemToCreate().price);
                        item.qty1GrossTotal(inventoryStockItemToCreate().priceWithTax);

                        selectedProduct(item);
                        var itemSection = itemModel.ItemSection.Create({});
                        counterForSection = counterForSection - 1;
                        itemSection.id(counterForSection);
                        itemSection.name("Text Sheet");
                        itemSection.qty1(selectedCostCentre().quantity1());
                        itemSection.qty2(selectedCostCentre().quantity2());
                        itemSection.qty3(selectedCostCentre().quantity3());
                        itemSection.baseCharge1(inventoryStockItemToCreate().price);
                        //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                        itemSection.productType(2);

                        var sectionCostCenter = itemModel.SectionCostCentre.Create({});
                        sectionCostCenter.qty1(selectedCostCentre().quantity1());
                        sectionCostCenter.qty2(selectedCostCentre().quantity2());
                        sectionCostCenter.qty3(selectedCostCentre().quantity3());
                        sectionCostCenter.costCentreId(getStockCostCenterId(139));
                        sectionCostCenter.costCentreName(selectedCostCentre().name());
                        sectionCostCenter.name('Stock(s)');
                        sectionCostCenter.qty1NetTotal(inventoryStockItemToCreate().price);
                        sectionCostCenter.qty2NetTotal(0);
                        sectionCostCenter.qty2NetTotal(0);
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.qty1Charge(inventoryStockItemToCreate().price);
                        sectionCostCenter.qty2Charge(0);
                        sectionCostCenter.qty3Charge(0);
                        sectionCostCenter.costCentreType('139');

                        var sectionCostCenterDetail = itemModel.SectionCostCenterDetail.Create({});
                        sectionCostCenterDetail.stockName(inventoryStockItemToCreate().name);
                        sectionCostCenterDetail.costPrice(inventoryStockItemToCreate().price);
                        sectionCostCenterDetail.qty1(inventoryStockItemToCreate().packageQty);

                        sectionCostCenter.sectionCostCentreDetails.splice(0, 0, sectionCostCenterDetail);

                        selectedSectionCostCenter(sectionCostCenter);
                        selectedQty(1);

                        itemSection.sectionCostCentres.push(sectionCostCenter);
                        item.itemSections.push(itemSection);
                        var itemSectionForAddView = itemModel.ItemSection.Create({});
                        counterForSection = counterForSection - 1;
                        itemSectionForAddView.id(counterForSection);
                        itemSectionForAddView.flagForAdd(true);
                        item.itemSections.push(itemSectionForAddView);

                        view.hideCostCentersQuantityDialog();
                        selectedInvoice().items.splice(0, 0, item);
                        selectedSection(itemSection);
                        //Req: Open Edit dialog of product on adding product
                        editItem(item);

                    },
                      //Opens Cost Center dialog for Shipping
                    onShippingChargesClick = function () {
                        if (selectedInvoice().companyId() === undefined) {
                            toastr.error("Please select customer.");
                        } else {
                            isAddProductFromInventory(false);
                            isCostCenterDialogForShipping(true);
                            onAddCostCenter();
                        }

                    },
                    onAddCostCenter = function () {
                        var companyId = 0;
                        if (selectedInvoice().storeId()) {
                            companyId = selectedInvoice().storeId();
                        } else {
                            companyId = selectedInvoice().companyId();
                        }
                        addCostCenterVM.show(afterSelectCostCenter, companyId, true, currencySymbol(), selectedCompanyTaxRate(), selectedCompanyTaxRate());
                    },
                    //Opens Cost Center dialog for Cost Center
                    onCostCenterClick = function () {
                        isAddProductFromInventory(false);
                        isCostCenterDialogForShipping(false);
                        onAddCostCenterForProduct();
                    },
                    onAddCostCenterForProduct = function () {
                        getCostCentersForProduct();
                        // view.showCostCentersDialog();
                    },
                  getCostCentersForProduct = function () {
                      addCostCenterVM.show(afterSelectCostCenter, selectedInvoice().companyId(), false, currencySymbol(), selectedCompanyTaxRate(), selectedCompanyTaxRate());
                  },
                    afterSelectCostCenter = function (costCenter) {
                        selectedCostCentre(costCenter);
                        view.showCostCentersQuantityDialog();
                    },
                    //#endregion

                    //#region Invoice Detail
                    // Active Invoice Detail Item
                    selectedInvoiceDetail = ko.observable(),
                    counterForInvoiceDetail = 0,
                    // Add New Invoice Detail Item
                    onAddInvoiceDetail = function () {
                        var invoiceDetail = model.InvoiceDetail();
                        if (selectedCompanyTaxRate() !== undefined && selectedCompanyTaxRate() !== null) {
                            invoiceDetail.tax(selectedCompanyTaxRate());
                        } else {
                            invoiceDetail.tax(0);
                        }
                        invoiceDetail.detailType(1);
                        invoiceDetail.itemType(1);
                        selectedInvoiceDetail(invoiceDetail);
                        view.showInvoiceDetailDialog();
                    },
                    // Close Invoice Detail Dialog
                    closeInvoiceDetailDialog = function () {
                        view.hideInvoiceDetailDialog();
                    },
                    // Save Invoice Detail
                    onSaveInvoiceDetail = function (invoiceDetail) {
                        if (!dobeforeSaveInvoiceDetail()) {
                            return;
                        }

                        if (invoiceDetail.id() == undefined) {
                            counterForInvoiceDetail = counterForInvoiceDetail = -1;
                            invoiceDetail.id(counterForInvoiceDetail);
                            selectedInvoice().items.splice(0, 0, invoiceDetail);
                        }
                        view.hideInvoiceDetailDialog();
                    },
                    dobeforeSaveInvoiceDetail = function () {
                        var flag = true;
                        if (!selectedInvoiceDetail().isValid()) {
                            selectedInvoiceDetail().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },

                    calculateInvoiceDetailTaxValue = ko.computed(function () {
                        if (selectedInvoiceDetail() !== undefined) {
                            taxCalculateForInvoiceDetail();
                        }
                    }),
                    taxCalculateForInvoiceDetail = function () {
                        var qty = (selectedInvoiceDetail().qty1() !== undefined && selectedInvoiceDetail().qty1() !== null) ? selectedInvoiceDetail().qty1() : 0;
                        var itemCharge = (selectedInvoiceDetail().itemCharge() !== undefined && selectedInvoiceDetail().itemCharge() !== null) ? selectedInvoiceDetail().itemCharge() : 0;
                        var taxCalculate1 = ((((selectedInvoiceDetail().tax() !== undefined && selectedInvoiceDetail().tax() !== null) ? selectedInvoiceDetail().tax() : 0) / 100) * (itemCharge * qty));
                        selectedInvoiceDetail().itemTaxValue(taxCalculate1);
                        selectedInvoiceDetail().qty1GrossTotal((itemCharge * qty) + taxCalculate1);
                    },
                //#endregion
                //Initialize method to call in every screen
                initializeScreen = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    // Get Base Data
                    getBaseData();
                },
                // Initialize the view model
                initialize = function (specifiedView) {
                    initializeScreen(specifiedView);
                    pager(new pagination.Pagination({ PageSize: 10 }, invoices, getInvoices));
                    getInvoices();
                };

                //#endregion

                return {

                    //#region Observables
                    initialize: initialize,
                    selectedInvoice: selectedInvoice,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoading: isLoading,
                    invoices: invoices,
                    isDetailsVisible: isDetailsVisible,
                    isItemDetailVisible: isItemDetailVisible,
                    pager: pager,
                    errorList: errorList,
                    selectedAddress: selectedAddress,
                    selectedCompanyContact: selectedCompanyContact,
                    companyContacts: companyContacts,
                    companyAddresses: companyAddresses,
                    sectionFlags: sectionFlags,
                    systemUsers: systemUsers,
                    selectedFilterFlag: selectedFilterFlag,
                    orderTypeFilter: orderTypeFilter,
                    invoiceCodeHeader: invoiceCodeHeader,
                    selectedCompany: selectedCompany,
                    selectedCostCentre: selectedCostCentre,
                    currencySymbol: currencySymbol,
                    isAddProductFromInventory: isAddProductFromInventory,
                    isAddProductForSectionCostCenter: isAddProductForSectionCostCenter,
                    sectionHeader: sectionHeader,
                    selectedInvoiceDetail: selectedInvoiceDetail,
                    //selectedOrder: selectedOrder,
                    //#endregion

                    //#region Utility Functions
                    onCreateNewBlankPrintProduct: onCreateNewBlankPrintProduct,
                    getInvoicesOfCurrentScreen: getInvoicesOfCurrentScreen,
                    filterText: filterText,
                    openReport:openReport,
                    orderType: orderType,
                    getBaseData: getBaseData,
                    editInvoice: editInvoice,
                    onCloseInvoiceEditor: onCloseInvoiceEditor,
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    invoiceTypes: invoiceTypes,
                    onSaveInvoice: onSaveInvoice,
                    getInvoicesOnTabChange: getInvoicesOnTabChange,
                    createInvoice: createInvoice,
                    onCreateNewProductFromRetailStore: onCreateNewProductFromRetailStore,
                    createitemForRetailStoreProduct: createitemForRetailStoreProduct,
                    openStockItemDialogForAddingProduct: openStockItemDialogForAddingProduct,
                    onCostCenterClick: onCostCenterClick,
                    openCompanyDialog: openCompanyDialog,
                    onSaveProductInventory: onSaveProductInventory,
                    createNewCostCenterProduct: createNewCostCenterProduct,
                    onShippingChargesClick: onShippingChargesClick,
                    editItem: editItem,
                    grossTotal: grossTotal,
                    onAddInvoiceDetail: onAddInvoiceDetail,
                    closeInvoiceDetailDialog: closeInvoiceDetailDialog,
                    onSaveInvoiceDetail: onSaveInvoiceDetail,
                    gotoElement: gotoElement,
                    isShowStatusCloumn: isShowStatusCloumn,
                    pageHeader: pageHeader,
                    pageCode: pageCode,
                    selectedEstimatePhraseContainer: selectedEstimatePhraseContainer,
                    selectEstimatePhraseContainer: selectEstimatePhraseContainer,
                    openPhraseLibrary: openPhraseLibrary
                    //#endregion
                };
            })()
        };
        return ist.invoice.viewModel;
    });
