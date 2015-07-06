﻿/*
    Module with the view model for the Order.
*/
define("order/order.viewModel",
    ["jquery", "amplify", "ko", "order/order.dataservice", "order/order.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/stockItem.viewModel", "common/reportManager.viewModel",
        "common/addCostCenter.viewModel", "common/addProduct.viewModel", "common/itemDetail.viewModel", "common/itemDetail.model", "common/phraseLibrary.viewModel"],
// ReSharper disable InconsistentNaming
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, stockDialog, reportManager, addCostCenterVM,
        addProductVm, itemDetailVm, itemModel, phraseLibrary) {
        // ReSharper restore InconsistentNaming
        var ist = window.ist || {};
        ist.order = {
            viewModel: (function () {
                var // the view 
                    view,
                    // #region Arrays
                    // orders
                    orders = ko.observableArray([]),
                    // Cost Centres
                    costCentres = ko.observableArray([]),
                    // Cost Centres Base Data
                    costCentresBaseData = ko.observableArray([]),
                    // flag colors
                    sectionFlags = ko.observableArray([]),
                    sectionFlagsForListView = ko.observableArray([]),
                    // Markups
                    markups = ko.observableArray([]),
                    // Categories
                    categories = ko.observableArray([]),
                    // Inventory Items 
                    inventoryItems = ko.observableArray([]),
                    // company contacts
                    companyContacts = ko.observableArray([]),
                    // Company Addresses
                    companyAddresses = ko.observableArray([]),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    // Pipeline Sources
                    pipelineSources = ko.observableArray([]),
                    // Pipeline Products
                    pipelineProducts = ko.observableArray([]),
                    // Payment Methods
                    paymentMethods = ko.observableArray([]),
                    //Inks
                    inks = ko.observableArray([]),
                    // Ink Coverage Group
                    inkCoverageGroup = ko.observableArray([]),
                    // paper sizes Methods
                    paperSizes = ko.observableArray([]),
                    // Ink Plate Sides Methods
                    inkPlateSides = ko.observableArray([]),
                    //Counter for New Section Id
                    counterForSection = -1000,
                    //
                    selectedCompanyTaxRate = ko.observable(),
                    selectedCompanyJobManagerUser = ko.observable(),
                    // selected Company
                    selectedCompany = ko.observable(),
                    //inquiries
                    inquiries = ko.observableArray([]),
                    // Errors List
                    errorList = ko.observableArray([]),
                    // Estimate Status
                    estimatesStatus = {
                        draftEstimate: 1
                    },
                    // Stock Category 
                    stockCategory = {
                        paper: 1,
                        inks: 2,
                        films: 3,
                        plates: 4
                    },
                    // Job Statuses
                    jobStatuses = ko.observableArray([
                        {
                            StatusId: 11,
                            StatusName: "Need Assigning"
                        },
                        {
                            StatusId: 12,
                            StatusName: "In Studio"
                        },
                        {
                            StatusId: 13,
                            StatusName: "In Print/Press"
                        },
                        {
                            StatusId: 14,
                            StatusName: "In Post Press/Bindery"
                        },
                        {
                            StatusId: 15,
                            StatusName: "Ready for Shipping"
                        },
                        {
                            StatusId: 16,
                            StatusName: "Shipped, Not Invoiced"
                        },
                        {
                            StatusId: 17,
                            StatusName: "Not Progressed to Job"
                        }
                    ]),
                    // Nominal Codes
                    nominalCodes = ko.observableArray([]),
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
                    //Inquiries Section Flags
                    inquiriesSectionFlags = ko.observableArray([]),
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingOrders = ko.observable(false),

                    // Is Order Editor Visible
                    isOrderDetailsVisible = ko.observable(false),

                    // is open report
                     isOpenReport = ko.observable(false),
                      // is open report Email
                     isOpenReportEmail = ko.observable(false),
                    //is Display Inquiry Detail Screen
                    isDisplayInquiryDetailScreen = ko.observable(false),
                    // Is Item Detail Visible
                    isItemDetailVisible = ko.observable(false),
                    // Is Section Detail Visible
                    isSectionDetailVisible = ko.observable(false),
                    // Is Company Base Data Loaded
                    isCompanyBaseDataLoaded = ko.observable(false),
                    // #endregion
                    // #region Observables
                    // Selected Estimate Phrase Container
                    selectedEstimatePhraseContainer = ko.observable(),
                    // filter
                    filterText = ko.observable(),
                    // Selected Product
                    selectedProduct = ko.observable(itemModel.Item.Create({})),
                    // Selected Markup 1
                    selectedMarkup1 = ko.observable(0),
                    // Selected Markup 2
                    selectedMarkup2 = ko.observable(0),
                    // Selected Markup 3
                    selectedMarkup3 = ko.observable(0),
                    // Selected Category Id
                    selectedCategoryId = ko.observable(),
                    //Is Estimate Screen
                    isEstimateScreen = ko.observable(false),
                    // Inventory SearchFilter
                    inventorySearchFilter = ko.observable(),
                    costCentrefilterText = ko.observable(),
                    selectedCostCentre = ko.observable(),
                    orderCodeHeader = ko.observable(''),
                    itemCodeHeader = ko.observable(''),
                    sectionHeader = ko.observable(''),
                    currencySymbol = ko.observable(''),
                    loggedInUser = ko.observable(),
                    itemIdFromDashboard = ko.observable(),
                    inquiryDetailEditorViewModel = new ist.ViewModel(model.InquiryItem),
                    selectedInquiryItem = inquiryDetailEditorViewModel.itemForEditing,
                    //On Order Status change to progress to job that will open wizard
                    selectedItemForProgressToJobWizard = ko.observable(itemModel.Item()),
                    // Active Order
                    selectedOrder = ko.observable(model.Estimate.Create({}, { SystemUsers: systemUsers() })),

                    //Active Inquiry
                    selectedInquiry = ko.observable(model.Inquiry.Create({}), { SystemUsers: systemUsers(), PipelineSources: pipelineSources() }),

                    //Estimate To Be Progressed
                    estimateToBeProgressed = ko.observable(undefined),
                    // Page Header 
                    pageHeader = ko.computed(function () {
                        return selectedOrder() && selectedOrder().name() ? selectedOrder().name() : 'Orders';
                    }),
                    // Sort On
                    sortOn = ko.observable(2),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(false),
                    // Pagination
                    pager = ko.observable(new pagination.Pagination({ PageSize: 5 }, orders)),
                    // Pagination for Categories
                    categoryPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, categories)),
                    // Pagination for Cost Centres
                    costCentrePager = ko.observable(new pagination.Pagination({ PageSize: 5 }, costCentres)),
                    // Default Address
                    defaultAddress = ko.observable(model.Address.Create({})),
                    // Default Company Contact
                    defaultCompanyContact = ko.observable(model.CompanyContact.Create({})),
                    //Inventory Stock Item To Create
                    inventoryStockItemToCreate = ko.observable(),
                    selectedCompanyContactOfInquiry = ko.observable(undefined),
                    //Is Inquiry Base Data Loaded
                    isInquiryBaseDataLoaded = ko.observable(false),
                    inquiryDetailItemCounter = -1,
                    isNewinquiryDetailItem = ko.observable(false),
                    isCopyiedEstimate = ko.observable(false),
                    // #endregion
                    // #region Utility Functions
                    // Select Estimate Phrase Container
                    selectEstimatePhraseContainer = function (data, e) {
                        selectedEstimatePhraseContainer(e.currentTarget.id);
                    },
                    // Open Phrase Library
                    openPhraseLibrary = function () {
                        phraseLibrary.isOpenFromPhraseLibrary(false);
                        phraseLibrary.defaultOpenSectionId(ist.sectionsEnum[0].id);
                        phraseLibrary.show(function (phrase) {
                            updateEstimatePhraseContainer(phrase);
                        });
                    },
                   
                    formatSelection = function (state, event) {
                        return state && state.id === undefined ? "" : "<span style=\"height:20px;width:20px;float:left;margin-right:10px;margin-top:5px;background-color:" + $(state.element).data("color") + "\"></span><span>" + state.text + "</span>";
                    },
                    formatResult = function (state) {
                        return "<div style=\"height:20px;margin-right:10px;width:20px;float:left;background-color:" + $(state.element).data("color") + "\"></div><div>" + state.text + "</div>";
                    },
                    // update Estimate Phrase Container
                    updateEstimatePhraseContainer = function (phrase) {
                        if (!phrase) {
                            return;
                        }

                        // Set Phrase to selected Estimate Phrase Container
                        if (selectedEstimatePhraseContainer() === 'EstimateHeader') {
                            selectedOrder().headNotes(selectedOrder().headNotes() ? selectedOrder().headNotes() + ' ' + phrase : phrase);
                        } else if (selectedEstimatePhraseContainer() === 'EstimateFootNotesTextBox') {
                            selectedOrder().footNotes(selectedOrder().footNotes() ? selectedOrder().footNotes() + ' ' + phrase : phrase);
                        }
                    },
                    // Selected Address
                    selectedAddress = ko.computed(function () {
                        if (!selectedOrder() || !selectedOrder().addressId() || companyAddresses().length === 0) {
                            return defaultAddress();
                        }

                        var addressResult = companyAddresses.find(function (address) {
                            return address.id === selectedOrder().addressId();
                        });

                        return addressResult || defaultAddress();
                    }),

                    // Selected Company Contact
                    selectedCompanyContact = ko.computed(function () {
                        if (!selectedOrder() || !selectedOrder().contactId() || companyContacts().length === 0) {
                            return defaultCompanyContact();
                        }

                        var contactResult = companyContacts.find(function (contact) {
                            return contact.id === selectedOrder().contactId();
                        });

                        return contactResult || defaultCompanyContact();
                    }),
                    // Selected Section
                    selectedSection = ko.observable(),
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    //Current Screen
                    currentScreen = ko.observable(4),
                    //Selected Filter Flag on List View
                    selectedFilterFlag = ko.observable(0),
                    // Active Pre Payment
                    selectedPrePayment = ko.observable(),
                    //Selected item
                    selecteditem = ko.observable(),
                    //Selected Stock Item
                    selectedStockItem = ko.observable(),
                    //Is Cost Center dialog open for shipping
                    isCostCenterDialogForShipping = ko.observable(false),
                    //Is Inventory Dialog is opening from Order Dialog's add Product From Inventory
                    isAddProductFromInventory = ko.observable(false),
                    //Is Inventory Dialog is opening for Section Cost Center
                    isAddProductForSectionCostCenter = ko.observable(false),
                    orderHasChanges = ko.computed(function () {
                        var hasChanges = false;
                        if (selectedOrder()) {
                            hasChanges = selectedOrder().hasChanges();
                        }

                        return hasChanges;
                    }),
                    // Create New Order
                    createOrder = function () {
                        selectedOrder(model.Estimate.Create({}, { SystemUsers: systemUsers() }));
                        selectedOrder().setOrderReportSignedBy(loggedInUser());
                        selectedOrder().setCreditiLimitSetBy(loggedInUser());
                        selectedOrder().setAllowJobWoCreditCheckSetBy(loggedInUser());
                        selectedOrder().setOfficialOrderSetBy(loggedInUser());
                        if (isEstimateScreen()) {
                            selectedOrder().reportSignedBy(loggedInUser());
                        }
                        view.setOrderState(4); // Pending Order
                        selectedOrder().statusId(4);
                        selectedOrder().status("Open/Draft Estimate");
                        $('#orderDetailTabs a[href="#tab-EstimateHeader"]').tab('show');
                        openOrderEditor();
                    },
                    //method to set credit approval fields
                    // ReSharper disable once UnusedLocals
                    updateCreditApprovalFields = ko.computed(function () {
                        if (selectedOrder().isJobAllowedWoCreditCheck()) {
                            selectedOrder().setAllowJobWoCreditCheckSetBy(loggedInUser());
                            selectedOrder().allowJobWoCreditCheckSetOnDateTime(moment().toDate());
                        }
                    }),
                    // Edit Order
                    editOrder = function (data) {
                        getOrderById(data.id(), openOrderEditor);
                        errorList.removeAll();
                        $('#orderDetailTabs a[href="#tab-EstimateHeader"]').tab('show');
                    },
                    // Open Editor
                    openOrderEditor = function () {
                        if (isEstimateScreen() && currentScreen() == 8) {
                            isDisplayInquiryDetailScreen(true);
                        } else {
                            isOrderDetailsVisible(true);
                            if (itemIdFromDashboard() !== 0 && itemIdFromDashboard() !== '') { //here
                                $('#orderDetailTabs a[href="#tab-EstimateHeader"]').tab('show');
                                var product = _.find(selectedOrder().nonDeliveryItems(), function (obj) {
                                    return obj.id() == itemIdFromDashboard();
                                });
                                if (product) {
                                    editItem(product);
                                }
                            }
                        }

                    },
                    // Gross Total
                    grossTotal = ko.computed(function () {
                        var total = 0;
                        if (selectedOrder() != undefined) {
                            _.each(selectedOrder().nonDeliveryItems(), function (item) {
                                var val = item.qty1GrossTotal();
                                total = total + parseFloat(val);
                            });
                            _.each(selectedOrder().deliveryItems(), function (item) {
                                var val = item.qty1GrossTotal();
                                total = total + parseFloat(val);
                            });
                            total = total.toFixed(2);
                            selectedOrder().estimateTotal(total);
                        }

                        return total;
                    }),
                    // On Close Editor
                    onCloseOrderEditor = function () {

                        //$("#dialog-confirm").removeData("modal").modal({ backdrop: 'true' });
                        // $("#dialog-confirm").removeData("modal").modal({ backdrop: 'true' });
                        // $("#dismiss")[0].style.display = 'block';
                        if (selectedOrder().hasChanges() && !(selectedOrder().invoiceStatus() === 20)) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(onSaveOrder);
                            confirmation.afterCancel(function () {
                                resetOrderBreadcrumb();
                                var orderIdFromDashboard = $('#OrderId').val();
                                if (orderIdFromDashboard != 0 && !isEstimateScreen()) {
                                    getOrders();
                                }

                            });
                            confirmation.show();
                            return;
                        }
                        resetOrderBreadcrumb();
                        var orderIdFromDashboardTemp = $('#OrderId').val();
                        if (orderIdFromDashboardTemp != 0 && !isEstimateScreen()) {
                            getOrders();
                        }
                        closeOrderEditor();
                    },
                    resetOrderBreadcrumb = function () {
                        selectedOrder().reset();
                        closeOrderEditor();
                        orderCodeHeader('');
                        sectionHeader('');
                        itemCodeHeader('');
                        isSectionDetailVisible(false);
                        isItemDetailVisible(false);
                    },
                    // Close Editor
                    closeOrderEditor = function () {
                        selectedOrder(model.Estimate.Create({}, { SystemUsers: systemUsers() }));
                        selectedOrder().flagColor(getSectionFlagColor(selectedOrder().sectionFlagId()));
                        if (isEstimateScreen() && currentScreen() == 8) {
                            isDisplayInquiryDetailScreen(false);
                        } else {
                            isOrderDetailsVisible(false);
                        }
                        errorList.removeAll();
                        isCopyiedEstimate(false);
                        selectedCompany(undefined);
                    },
                    // On Archive
                    onArchiveOrder = function (order) {
                        confirmation.afterProceed(function () {
                            archiveOrder(order.id());
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
                        if (!isDisplayInquiryDetailScreen()) {
                            if (selectedOrder().companyId() === company.id) {
                                return;
                            }

                            selectedOrder().companyId(company.id);
                            selectedOrder().companyName(company.name);
                            selectedCompany(company);
                            if (company.isCustomer !== 3 && company.storeId) {
                                selectedOrder().storeId(company.storeId);
                            }
                            else {
                                selectedOrder().storeId(undefined);
                            }
                            // Get Company Address and Contacts
                            getBaseForCompany(company.id, (selectedOrder().storeId() === null || selectedOrder().storeId() === undefined) ? company.id :
                                selectedOrder().storeId());
                        } else if (isDisplayInquiryDetailScreen()) {
                            companyContacts.removeAll();
                            selectedInquiry().companyId(company.id);
                            selectedInquiry().companyName(company.name);
                            if (company.isCustomer !== 3 && company.storeId) {
                                // Get Company Address and Contacts
                                getBaseForInquiry(company.id, (company.storeId === null || company.storeId === undefined) ? company.id :
                                    company.storeId);
                            } else {
                                // Get Company Address and Contacts
                                getBaseForInquiry(company.id, company.id);
                            }

                        }

                    },
                    // Add Item
                    addItem = function () {
                        // Open Product Selector Dialog
                    },
                    // Edit Item
                    editItem = function (item) {
                        var itemHasChanges = item.hasChanges();
                        var orderGotChanges = selectedOrder().hasChanges();
                        itemCodeHeader(item.code());
                        var itemSection = _.find(item.itemSections(), function (itemSec) {
                            return itemSec.flagForAdd() === true;
                        });
                        if (itemSection === undefined) {
                            var itemSectionForAddView = itemModel.ItemSection.Create({});
                            itemSectionForAddView.flagForAdd(true);
                            counterForSection = counterForSection - 1;
                            itemSectionForAddView.id(counterForSection);
                            item.itemSections.push(itemSectionForAddView);
                            if (!itemHasChanges) {
                                item.reset();
                            }
                        }
                        selectedProduct(item);
                        if (!orderGotChanges) {
                            selectedOrder().reset();
                        }
                        var section = selectedProduct() != undefined ? selectedProduct().itemSections()[0] : undefined;
                        editSection(section);
                        openItemDetail();
                    },
                    // Open Item Detail
                    openItemDetail = function () {
                        isItemDetailVisible(true);
                        selectedOrder().taxRate(selectedCompanyTaxRate());
                        itemDetailVm.showItemDetail(selectedProduct(), selectedOrder(), closeItemDetail, isEstimateScreen());
                        view.initializeLabelPopovers();
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
                        selectedOrder().items.remove(item);
                    },

                    sectionFlagMapList = function (observableList, data, factory) {
                        var list = [];
                        _.each(data, function (item) {
                            list.push(factory.Create(item));
                            sectionFlagsForListView.push(factory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(observableList(), list);
                        observableList.valueHasMutated();
                    },

                    // Map List
                    mapList = function (observableList, data, factory) {
                        var list = [];
                        _.each(data, function (item) {
                            list.push(factory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(observableList(), list);
                        observableList.valueHasMutated();
                    },
                    // Select Default Address For Company in case of new order
                    setDefaultAddressForCompany = function () {
                        if (selectedOrder().id() > 0) {
                            return;
                        }
                        selectedOrder().addressId(selectedCompany().addressId || undefined);
                    },
                    // Select Default Contact For Company in case of new order
                    setDefaultContactForCompany = function () {
                        if (selectedOrder().id() > 0) {
                            return;
                        }
                        selectedOrder().contactId(selectedCompany().contactId || undefined);
                    },
                    // Map Orders 
                    mapOrders = function (data) {
                        var ordersList = [];
                        _.each(data, function (order) {
                            order.FlagColor = getSectionFlagColor(order.SectionFlagId);
                            ordersList.push(model.Estimate.Create(order, { SystemUsers: systemUsers() }));
                        });
                        // Push to Original Array
                        ko.utils.arrayPushAll(orders(), ordersList);
                        orders.valueHasMutated();
                    },
                    // Filter Orders
                    filterOrders = function () {
                        // Reset Pager
                        pager().reset();
                        // Get Orders
                        getOrders(currentScreen());
                    },
                    // Reset Filter
                    resetFilter = function () {
                        // Reset Text 
                        filterText(undefined);
                        // Filter Record
                        filterOrders();
                    },
                    // On Save Order
                    onSaveOrder = function (data, event, navigateCallback) {
                        if (currentScreen() == 8) {

                        }
                        removeItemSectionWithAddFlagTrue();
                        if (!doBeforeSave()) {
                            return;
                        }
                        _.each(selectedOrder().prePayments(), function (item) {
                            item.customerId(selectedOrder().companyId());
                        });
                        saveOrder(closeOrderEditor, navigateCallback);
                    },

                    removeItemSectionWithAddFlagTrue = function () {
                        _.each(selectedOrder().items(), function (item) {
                            _.each(item.itemSections(), function (itemSection) {
                                if (itemSection.flagForAdd()) {
                                    item.itemSections.remove(itemSection);
                                }
                            });
                        });
                    },
                    // Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedOrder().isValid()) {
                            selectedOrder().showAllErrors();
                            selectedOrder().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    // On Clone Order
                    onCloneOrder = function (data) {
                        cloneOrder(data, openOrderEditor);
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    // Get Order From list
                    getOrderFromList = function (id) {
                        return orders.find(function (order) {
                            return order.id() === id;
                        });
                    },
                    // Open Stock Item Dialog For Adding product
                    openStockItemDialogForAddingProduct = function () {
                        isAddProductFromInventory(true);
                        isAddProductForSectionCostCenter(false);
                        if (selectedOrder().companyId() === undefined) {
                            toastr.error("Please select customer.");
                        } else {
                            stockDialog.show(function (stockItem) {
                                createNewInventoryProduct(stockItem);
                            }, stockCategory.paper, false, currencySymbol(), selectedCompanyTaxRate());
                        }
                    },
                    // Edit Section
                    editSection = function (item) {
                        sectionHeader("SECTION - " + item.sectionNo());
                        selectedSection(item);
                        openSectionDetail();

                    },
                    // Open Section Detail
                    openSectionDetail = function () {
                        //    isSectionDetailVisible(true);
                        view.initializeLabelPopovers();

                        // Subscribe Section Changes
                        //subscribeSectionChanges();
                    },
                    // Subscribe Dropdown Filter Changes to search on selection change
                    subscribeDropdownFilterChange = function () {
                        orderTypeFilter.subscribe(function (value) {
                            if (value === undefined || value === 0) {
                                return;
                            }
                            getOrdersOfCurrentScreen(currentScreen());
                        });

                        selectedFilterFlag.subscribe(function (value) {
                            if (value === undefined || value === 0) {
                                return;
                            }
                            getOrdersOfCurrentScreen(currentScreen());
                        });
                    },
                    // On Order Status Change
                    onOrderStatusChange = function (status) {
                        // For Cancel Order
                        if (status === 5) {
                            forOrderStatusCancel();

                        } else {
                            status = status === 4 ? status + 6 : (status === 5 ? status + 5 : status + 4);
                            // Cancel to shippend & Invoice
                            if (status == 10 && selectedOrder().statusId() === 9) {
                                statusNavigationBackward(status);
                            } else if (selectedOrder().statusId() < status) {
                                // Before status change to In Production, Items must exist in order

                                statusNavigationForward(status);


                            } else {
                                statusNavigationBackward(status);
                            }
                        }

                    },


                    forOrderStatusCancel = function () {
                        // $("#dialog-confirm").modal({ backdrop: '' });
                        // $("#dialog-confirm").removeData("modal").modal({ backdrop: 'static' });
                        //  $("#dismiss")[0].style.display = 'none';
                        confirmation.messageText("Are you sure you want to cancel this order?");
                        confirmation.afterProceed(function () {
                            onStatusChangeDeliveryNotesCancelled();
                            view.setOrderState(9, selectedOrder().isFromEstimate());
                            selectedOrder().statusId(9);
                            onSaveOrder(selectedOrder(), null, null);
                            //  $("#dismiss")[0].style.display = 'block';
                        });
                        confirmation.afterCancel(function () {

                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        });
                        confirmation.show();
                        return;
                    },
                    statusNavigationBackward = function (status) {
                        // Only move 1 or 2 step backward at a time, if user try to move more than 1 or 2 step then system set 1 step by default
                        if (selectedOrder().statusId() === 10) {
                            if ((selectedOrder().statusId() - 3) !== status && (selectedOrder().statusId() - 4) !== status) {
                                status = selectedOrder().statusId() - 3;

                            } else {
                                if (status === 7) {
                                    status = selectedOrder().statusId() - 3;
                                } else {
                                    status = selectedOrder().statusId() - 4;
                                }
                            }
                        } else if (selectedOrder().statusId() === 9) {
                            // On Take more than two steps
                            if ((selectedOrder().statusId() + 1) !== status && (selectedOrder().statusId() - 2) !== status) {
                                status = selectedOrder().statusId() + 1;

                            } else {
                                // Cancel to ready for shipping 
                                if (status == 7) {
                                    status = selectedOrder().statusId() - 2;
                                } else {
                                    // Cancel to Shipped to invoiced
                                    status = selectedOrder().statusId() + 1;
                                }
                            }
                        } else {
                            if (selectedOrder().statusId() !== 9 && (selectedOrder().statusId() - 1) !== status && (selectedOrder().statusId() - 2) !== status) {
                                status = selectedOrder().statusId() - 1;
                            }
                        }


                        // Shipped & Invoiced to  In Production (1 Step) or Shipped & Invoiced to confirmed start (2 step)
                        if (status === 6 || (selectedOrder().statusId() - 2 === 5)) {
                            showConfirmationMessageForBackwardNavigationOnStatusChange(status);
                        } else {
                            confirmationForBackward(status);
                        }
                    },
                    confirmationForBackward = function (status) {
                        // $("#dialog-confirm").attr('data-backdrop', 'static');
                        // $("#dialog-confirm").removeData("modal").modal({ backdrop: 'static' });
                        // $("#dismiss")[0].style.display = 'none';
                        confirmation.messageText("Are you sure you want to revert status of this order? All posted delivery notes will be cancelled.");
                        confirmation.afterProceed(function () {
                            if (status === 7) {
                                changeStatusOfItemsForReadyForShipping();
                            } else if (status === 10) {
                                changeStatusOfItemsForInvoicedAndShipped();
                            }
                            selectedOrder().statusId(status);
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        });
                        confirmation.afterCancel(function () {
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        });
                        confirmation.show();
                        return;
                    },
                    statusNavigationForward = function (status) {
                        // Only Move one step at a time, if user try to move more than 1 step then system set 1 step by default
                        if (selectedOrder().statusId() !== 7 && (selectedOrder().statusId() + 1) !== status) {
                            status = selectedOrder().statusId() + 1;
                        }
                        // Pending Order to Confirm Start ,In Production to Shipped & Invoiced, Shipped & Invoiced to Cancelled,In Production to
                        if (status !== 6) {

                            showConfirmationMessageForForwardNavigationOnStatusChange(status);

                        }
                            // Confirm Start to In Production
                        else {
                            // $("#dialog-confirm").attr('data-backdrop', 'static');
                            // $("#dismiss")[0].style.display = 'none';
                            confirmation.messageText("Are you sure you want to progress all the un progressed items to jobs?");
                            confirmation.afterProceed(function () {
                                if (selectedOrder().items().length === 0) {
                                    toastr.error("Please first add items.");
                                    view.setOrderState(5, selectedOrder().isFromEstimate());
                                    return;
                                }
                                selectedOrder().statusId(status);
                                view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                                changeAllItemProgressToJob();
                            });
                            confirmation.afterCancel(function () {

                                view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                            });
                            confirmation.show();
                            return;
                        }
                    },
                    progressToJobItemCounter = 0,
                    // Change All items status Progress to job
                    changeAllItemProgressToJob = function () {
                        if (selectedOrder().nonDeliveryItems().length > 0) {
                            selectedItemForProgressToJobWizard(selectedOrder().nonDeliveryItems()[progressToJobItemCounter]);
                            selectedItemForProgressToJobWizard().jobStatusId(jobStatuses()[0].StatusId);
                            selectedItemForProgressToJobWizard().jobEstimatedStartDateTime(moment().toDate());
                            selectedItemForProgressToJobWizard().jobEstimatedCompletionDateTime(moment().add('days', 2).toDate());
                            selectedItemForProgressToJobWizard().jobManagerUser(selectedCompanyJobManagerUser());
                            if (selectedItemForProgressToJobWizard().systemUsers().length === 0) {
                                selectedItemForProgressToJobWizard().systemUsers(systemUsers());
                            }
                            selectedItemForProgressToJobWizard().setJobProgressedBy(loggedInUser());
                            progressToJobItemCounter = progressToJobItemCounter + 1;
                            //Update Order Items On Progress to order
                            //setting job manager and signed by of items on progress to order
                            updateOrderItemsOnProgressToOrder();
                            view.showOrderStatusProgressToJobDialog();
                        }
                    },
                    //Update Order Items On Progress to order
                    //setting job manager and signed by of items on progress to order
                    updateOrderItemsOnProgressToOrder = function() {
                        _.each(selectedOrder().nonDeliveryItems(), function(item) {
                            item.jobManagerId(selectedCompanyJobManagerUser());
                            item.jobSignedBy(loggedInUser());
                        });
                    },
                    clickOnJobToProgressWizard = function () {
                        if (selectedOrder().nonDeliveryItems().length === progressToJobItemCounter) {
                            view.hideOrderStatusProgressToJobDialog();
                            progressToJobItemCounter = 0;
                        } else {
                            changeAllItemProgressToJob();
                        }

                    },
                    // Show Confirmation on forward Navigation of Order Status Change
                    showConfirmationMessageForForwardNavigationOnStatusChange = function (status) {
                        // $("#dialog-confirm").attr('data-backdrop', 'static');
                        // $("#dismiss")[0].style.display = 'none';
                        confirmation.messageText("Are you sure you want to change status of this order?");
                        confirmation.afterProceed(function () {

                            if (status == 7) {
                                changeStatusOfItemsForReadyForShipping();
                            } else if (status == 10) {
                                changeStatusOfItemsForInvoicedAndShipped();
                            }
                            selectedOrder().statusId(status);
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());

                            // $("#dismiss")[0].style.display = 'block';
                        });
                        confirmation.afterCancel(function () {
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                            // $("#dismiss")[0].style.display = 'block';
                        });
                        confirmation.show();
                        return;
                    },

                    changeStatusOfItemsForReadyForShipping = function () {
                        _.each(selectedOrder().items(), function (item) {
                            item.jobStatusId(jobStatuses()[4].StatusId);
                        });
                    },
                    changeStatusOfItemsForInvoicedAndShipped = function () {
                        _.each(selectedOrder().items(), function (item) {
                            item.jobStatusId(jobStatuses()[5].StatusId);
                        });
                    },
                    // Show Confirmation on backward Navigation of Order Status Change
                    showConfirmationMessageForBackwardNavigationOnStatusChange = function (status) {
                        // $("#dialog-confirm").attr('data-backdrop', 'static');
                        // $("#dismiss")[0].style.display = 'none';
                        confirmation.messageText("Are you sure you want to revert status of this order?\nAll posted delivery notes will be cancelled & All item job status will be reset to Un-Assigned. ");
                        confirmation.afterProceed(function () {
                            selectedOrder().statusId(status);
                            onStatusChangeItemResetToUnAssigned();
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        });
                        confirmation.afterCancel(function () {
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        });
                        confirmation.show();
                        return;
                    },

                    onStatusChangeDeliveryNotesCancelled = function () {
                        selectedDeliverySchedule(undefined);
                        var deliveries = [];
                        ko.utils.arrayPushAll(deliveries, selectedOrder().deliverySchedules());
                        _.each(deliveries, function (item) {
                            selectedOrder().deliverySchedules.remove(item);
                        });
                        if (selectedOrder().deliverySchedules().length === 0) {
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        }
                    },
                    onStatusChangeItemResetToUnAssigned = function () {
                        var counter = 0;
                        _.each(selectedOrder().items(), function (item) {
                            item.statusId(jobStatuses()[0].StatusId);
                            counter = counter + 1;
                        });

                        if (selectedOrder().items().length === counter) {
                            onStatusChangeDeliveryNotesCancelled();
                        }

                    },

                    deleteOrderButtonHandler = function () {
                        confirmation.messageText("WARNING - All items will be removed from the system and you won’t be able to recover.  There is no undo");
                        confirmation.afterProceed(deleteOrder);
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    deleteOrder = function () {
                        dataservice.deleteOrder({
                            OrderId: selectedOrder().id()
                        }, {
                            success: function () {
                                toastr.success("Order successfully deleted!");
                                selectedOrder().reset();
                                closeOrderEditor();
                                orderCodeHeader('');
                                sectionHeader('');
                                itemCodeHeader('');
                                isSectionDetailVisible(false);
                                isItemDetailVisible(false);
                            },
                            error: function (response) {
                                toastr.error("Failed to delete order!" + response);
                            }
                        });
                    },
                    selectedSectionCostCenter = ko.observable(),
                    selectedQty = ko.observable(),
                    //Opens Cost Center dialog for Shipping
                    onShippingChargesClick = function () {
                        if (selectedOrder().companyId() === undefined) {
                            toastr.error("Please select customer.");
                        } else {
                            isAddProductFromInventory(false);
                            isCostCenterDialogForShipping(true);
                            onAddCostCenter();
                        }

                    },
                    //Opens Cost Center dialog for Cost Center
                    onCostCenterClick = function () {
                        isAddProductFromInventory(false);
                        isCostCenterDialogForShipping(false);
                        onAddCostCenterForProduct();
                    },

                    afterSelectCostCenter = function (costCenter) {
                        selectedCostCentre(costCenter);
                        //req. change
                        if (isCostCenterDialogForShipping()) {
                            createNewCostCenterProductForShippingCharge();
                            addCostCenterVM.hide();
                        } else {
                            view.showCostCentersQuantityDialog();
                        }
                    },
                    // On Create New Cost Center Product
                    onCreateNewCostCenterProduct = function () {
                        view.hideCostCentersQuantityDialog();
                        if (isCostCenterDialogForShipping()) {
                            createNewCostCenterProduct();
                            return;
                        }
                        addCostCenterVM.executeCostCenter(function (costCenter) {
                            selectedCostCentre(costCenter);
                            createNewCostCenterProduct();
                        });
                    },
                    //Product From Cost Center
                    createNewCostCenterProduct = function () {
                        var item = itemModel.Item.Create({ EstimateId: selectedOrder().id(), RefItemId: selectedCostCentre().id() });
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
                            var deliveryItem = _.find(selectedOrder().items(), function (itemWithType2) {
                                return itemWithType2.itemType() === 2;
                            });
                            if (deliveryItem !== undefined) {
                                selectedOrder().items.remove(deliveryItem);
                            }

                        }

                        selectedOrder().items.splice(0, 0, item);

                        selectedSection(itemSection);

                        //this method is calling to update orders list view total prices etc by trigering computed in item's detail view
                        itemDetailVm.updateOrderData(selectedOrder(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());

                        //Req: Open Edit dialog of product on adding product
                        editItem(item);
                    },

                    //Req. Change: When adding CostCenter Product For shipping charge
                    //Do not show quantity dialog
                    createNewCostCenterProductForShippingCharge = function () {
                        var item = itemModel.Item.Create({ EstimateId: selectedOrder().id(), RefItemId: selectedCostCentre().id() });
                        applyProductTax(item);
                        selectedProduct(item);
                        item.productName(selectedCostCentre().name());
                        //Req. Setting Quantities as 1 after not closing quantity dialog in shipping case
                        item.qty1(1);
                        item.qty2(1);
                        item.qty3(1);
                        item.qty1NetTotal(selectedCostCentre().deliveryCharges());
                        //Req: Item Product code is set to '2', so while editting item's section is non mandatory
                        item.productType(2);

                        var itemSection = itemModel.ItemSection.Create({});
                        counterForSection = counterForSection - 1;
                        itemSection.id(counterForSection);
                        itemSection.name("Text Sheet");
                        //Req. Setting Quantities as 1 after not closing quantity dialog in shipping case
                        itemSection.qty1(1);
                        itemSection.qty2(1);
                        itemSection.qty3(1);
                        //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                        itemSection.productType(2);
                        itemSection.baseCharge1(selectedCostCentre().deliveryCharges());

                        var sectionCostCenter = itemModel.SectionCostCentre.Create({});
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.costCentreId(selectedCostCentre().id());
                        sectionCostCenter.costCentreName(selectedCostCentre().name());
                        sectionCostCenter.name(selectedCostCentre().name());

                        //sectionCostCenter.qty1NetTotal(selectedCostCentre().setupCost());
                        sectionCostCenter.qty1Charge(selectedCostCentre().deliveryCharges());
                        //Req. Setting Quantities as 1 after not closing quantity dialog in shipping case
                        sectionCostCenter.qty1(1);
                        sectionCostCenter.qty2(1);
                        sectionCostCenter.qty3(1);
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
                            var deliveryItem = _.find(selectedOrder().items(), function (itemWithType2) {
                                return itemWithType2.itemType() === 2;
                            });
                            if (deliveryItem !== undefined) {
                                selectedOrder().items.remove(deliveryItem);
                            }

                        }

                        selectedOrder().items.splice(0, 0, item);

                        selectedSection(itemSection);

                        //this method is calling to update orders list view total prices etc by trigering computed in item's detail view
                        itemDetailVm.updateOrderData(selectedOrder(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());
                    },

                    // #endregion
                    //#region ServiceCalls
                    //Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                paperSizes.removeAll();
                                inkPlateSides.removeAll();

                                if (data.SectionFlags) {
                                    mapList(sectionFlags, data.SectionFlags, model.SectionFlag);
                                }
                                if (data.SystemUsers) {
                                    mapList(systemUsers, data.SystemUsers, model.SystemUser);
                                }
                                if (data.PipeLineSources) {
                                    mapList(pipelineSources, data.PipeLineSources, model.PipeLineSource);
                                }
                                if (data.PipeLineProducts) {
                                    mapList(pipelineProducts, data.PipeLineProducts, model.PipeLineProduct);
                                }
                                paymentMethods.removeAll();
                                if (data.PaymentMethods) {
                                    ko.utils.arrayPushAll(paymentMethods(), data.PaymentMethods);
                                    paymentMethods.valueHasMutated();
                                }

                                nominalCodes.removeAll();
                                if (data.ChartOfAccounts) {
                                    _.each(data.ChartOfAccounts, function (item) {
                                        nominalCodes.push(item);
                                    });
                                }

                                costCentresBaseData.removeAll(); //
                                if (data.CostCenters) {
                                    ko.utils.arrayPushAll(costCentresBaseData(), data.CostCenters);
                                    costCentresBaseData.valueHasMutated();
                                }

                                currencySymbol(data.CurrencySymbol);
                                loggedInUser(data.LoggedInUser || '');
                                view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data" + response);
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                    //Get Base Data
                    getBaseDataForEstimate = function () {
                        dataservice.getBaseDataForEstimate({
                            success: function (data) {
                                paperSizes.removeAll();
                                inkPlateSides.removeAll();
                                if (data.SectionFlags) {
                                    sectionFlagMapList(sectionFlags, data.SectionFlags, model.SectionFlag);
                                }
                                if (data.SystemUsers) {
                                    mapList(systemUsers, data.SystemUsers, model.SystemUser);
                                }
                                if (data.PipeLineSources) {
                                    mapList(pipelineSources, data.PipeLineSources, model.PipeLineSource);
                                }
                                if (data.PipeLineProducts) {
                                    mapList(pipelineProducts, data.PipeLineProducts, model.PipeLineProduct);
                                }
                                paymentMethods.removeAll();
                                if (data.PaymentMethods) {
                                    ko.utils.arrayPushAll(paymentMethods(), data.PaymentMethods);
                                    paymentMethods.valueHasMutated();
                                }

                                nominalCodes.removeAll();
                                if (data.ChartOfAccounts) {
                                    _.each(data.ChartOfAccounts, function (item) {
                                        nominalCodes.push(item);
                                    });
                                }

                                costCentresBaseData.removeAll(); //
                                if (data.CostCenters) {
                                    ko.utils.arrayPushAll(costCentresBaseData(), data.CostCenters);
                                    costCentresBaseData.valueHasMutated();
                                }

                                currencySymbol(data.CurrencySymbol);
                                loggedInUser(data.LoggedInUser || '');
                                view.initializeLabelPopovers();
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

                    updateOrderBeforeSaving = function (order) {
                        _.each(selectedOrder().prePayments(), function (item) {
                            order.PrePayments.push(item.convertToServerData());
                        });
                        _.each(selectedOrder().deliverySchedules(), function (item) {
                            order.ShippingInformations.push(item.convertToServerData());
                        });
                        var itemsArray = [];
                        _.each(selectedOrder().items(), function (obj) {
                            var item = obj.convertToServerData(); // item converted 
                            var attArray = [];
                            _.each(item.ItemAttachment, function (att) {
                                var attchment = att.convertToServerData(); // item converted 
                                attchment.ContactId = selectedOrder().contactId();
                                attArray.push(attchment);
                            });
                            item.ItemAttachments = attArray;
                            itemsArray.push(item);
                        });
                        order.Items = itemsArray;
                    },

                    // Save Order
                    saveOrder = function (callback, navigateCallback) {
                        // selectedOrder().statusId(view.orderstate());
                        if (isNaN(view.orderstate()) || view.orderstate() === 0) {
                            selectedOrder().statusId(4); // Pending orders
                        }
                        // If Estimate Screen then set IsEstimate = true
                        if (!selectedOrder().id() && isEstimateScreen()) {
                            selectedOrder().isEstimate(true);
                            selectedOrder().statusId(estimatesStatus.draftEstimate); // Draft Estimate
                        }
                        var order = selectedOrder().convertToServerData();
                        updateOrderBeforeSaving(order);
                        dataservice.saveOrder(order, {
                            success: function (data) {
                                var orderFlag = _.find(sectionFlags(), function (item) {
                                    return item.id == selectedOrder().sectionFlagId();
                                });

                                if (isOpenReport() == true) {
                                    if (isOpenReportEmail() == true) {
                                        if (selectedOrder().isEstimate() == true) {
                                            reportManager.SetOrderData(selectedOrder().orderReportSignedBy(), selectedOrder().contactId(), selectedOrder().id(), 3, selectedOrder().id(), "");
                                            reportManager.OpenExternalReport(ist.reportCategoryEnums.Estimate, 1, selectedOrder().id());
                                        } else {
                                            reportManager.SetOrderData(selectedOrder().orderReportSignedBy(), selectedOrder().contactId(), selectedOrder().id(), 2, selectedOrder().id(), "");
                                            reportManager.OpenExternalReport(ist.reportCategoryEnums.Orders, 1, selectedOrder().id());
                                        }
                                        getOrderById(selectedOrder().id());
                                    }
                                    else {
                                        if (selectedOrder().isEstimate() == true) {
                                            reportManager.OpenExternalReport(ist.reportCategoryEnums.Estimate, 1, selectedOrder().id());
                                            // toastr.success("Saved Successfully.");
                                            getOrderById(selectedOrder().id());
                                        }
                                        else {
                                            reportManager.OpenExternalReport(ist.reportCategoryEnums.Orders, 1, selectedOrder().id());
                                            // toastr.success("Saved Successfully.");
                                            getOrderById(selectedOrder().id());
                                        }
                                    }

                                    isOpenReport(false);
                                }
                                else {

                                    if (!selectedOrder().id()) {
                                        // Update Id
                                        selectedOrder().id(data.EstimateId);
                                        selectedOrder().orderCode(data.OrderCode);
                                        if (isEstimateScreen()) {
                                            selectedOrder().code(data.EstimateCode);
                                        } else {
                                            selectedOrder().orderCode(data.OrderCode);
                                        }
                                        var total1 = (parseFloat((data.EstimateTotal === undefined || data.EstimateTotal === null) ? 0 : data.EstimateTotal)).toFixed(2);
                                        selectedOrder().estimateTotal(total1);
                                        selectedOrder().creationDate(data.CreationDate !== null ? moment(data.CreationDate).toDate() : undefined);
                                        selectedOrder().numberOfItems(data.ItemsCount || 0);
                                        selectedOrder().status(data.Status || '');
                                        if (orderFlag) {
                                            selectedOrder().flagColor(orderFlag.color);
                                        }
                                        // Add to top of list
                                        orders.splice(0, 0, selectedOrder());
                                    } else {
                                        // Get Order
                                        var orderUpdated = getOrderFromList(selectedOrder().id());
                                        if (orderUpdated) {
                                            orderUpdated.creationDate(data.CreationDate !== null ? moment(data.CreationDate).toDate() : undefined);
                                            var total = (parseFloat((data.EstimateTotal === undefined || data.EstimateTotal === null) ? 0 : data.EstimateTotal)).toFixed(2);
                                            orderUpdated.estimateTotal(total);
                                            orderUpdated.name(data.EstimateName);
                                            orderUpdated.numberOfItems(data.ItemsCount || 0);
                                            orderUpdated.status(data.Status || '');
                                            if (orderFlag) {
                                                orderUpdated.flagColor(orderFlag.color);
                                            }

                                        }
                                    }

                                    toastr.success("Saved Successfully.");

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }

                                    if (navigateCallback && typeof navigateCallback === "function") {
                                        navigateCallback();
                                    }
                                    orderCodeHeader('');
                                    var orderIdFromDashboard = $('#OrderId').val();
                                    if (orderIdFromDashboard != 0) {
                                        getOrders();
                                    }
                                }

                            },
                            error: function (response) {
                                toastr.error("Failed to Save Order. Error: " + response);
                            }
                        });
                    },
                    // Clone Order
                    cloneOrder = function (order, callback) {
                        dataservice.cloneOrder({ OrderId: order.id() }, {
                            success: function (data) {
                                if (data) {
                                    var newOrder = model.Estimate.Create(data);
                                    // Add to top of list
                                    orders.splice(0, 0, newOrder);
                                    selectedOrder(newOrder);

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }

                                toastr.success("Cloned Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to Clone Order. Error: " + response);
                            }
                        });
                    },
                    // archive Order
                    archiveOrder = function () {
                        dataservice.archiveOrder({
                            OrderId: selectedOrder().id()
                        }, {
                            success: function () {
                                selectedOrder().isArchived(true);
                                toastr.success("Archived Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to archive Order. Error: " + response);
                            }
                        });
                    },
                    //get Orders Of Current Screen
                    getOrdersOfCurrentScreen = function () {
                        pager().reset();
                        if (!isEstimateScreen()) {
                            getOrders(currentScreen());
                        } else {
                            if (currentScreen() != 8) {
                                getEstimates(currentScreen());
                            } else {
                                getInquiries();
                            }

                        }

                    },
                    //Get Order Tab Changed Event
                    getOrdersOnTabChange = function (currentTab) {

                        if (isEstimateScreen()) {

                            pager(new pagination.Pagination({ PageSize: 5 }, orders, getEstimates));
                            pager().reset();
                            //selectedFilterFlag(0);

                            // Push to Original Array
                            //  inquiriesSectionFlags.removeAll();
                            // sectionFlags.removeAll();
                            selectedFilterFlag(0);
                            // ko.utils.arrayPushAll(sectionFlags(), sectionFlagsForListView());
                            // sectionFlags.valueHasMutated();
                            getEstimates(currentTab);
                        } else {
                            pager().reset();
                            getOrders(currentTab);
                        }

                    },
                    // Get Orders
                    getOrders = function (currentTab) {
                        isLoadingOrders(true);
                        currentScreen(currentTab);
                        dataservice.getOrders({
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
                                orders.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapOrders(data.Orders);
                                    pager().totalCount(data.TotalCount);
                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Failed to load orders" + response);
                            }
                        });
                    },
                    // Map Inquiries
                    mapInquiries = function (data) {
                        var inquiriesList = [];
                        _.each(data, function (inquiry) {
                            inquiry.FlagColor = getSectionFlagColor(inquiry.FlagId);
                            inquiriesList.push(model.Inquiry.Create(inquiry, { SystemUsers: systemUsers(), PipelineSources: pipelineSources() }));
                        });
                        // Push to Original Array
                        ko.utils.arrayPushAll(inquiries(), inquiriesList);
                        inquiries.valueHasMutated();
                    },
                    // Get Order By Id
                    getOrderById = function (id, callback) {
                        isLoadingOrders(true);
                        isCompanyBaseDataLoaded(false);
                        dataservice.getOrder({
                            id: id
                        }, {
                            success: function (data) {
                                if (data) {
                                    setSelectedOrder(data, callback);
                                }
                                isLoadingOrders(false);
                                var code = !selectedOrder().orderCode() ? "ORDER CODE" : selectedOrder().orderCode();
                                orderCodeHeader(code);
                                view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Failed to load order details" + response);
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                    //Set Selected Order
                    setSelectedOrder = function (data, callback) {
                        selectedOrder(model.Estimate.Create(data, { SystemUsers: systemUsers() }));
                        view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        // Get Base Data For Company
                        if (data.CompanyId) {
                            // ReSharper disable AssignedValueIsNeverUsed
                            var storeId = 0;
                            // ReSharper restore AssignedValueIsNeverUsed
                            if (data.IsCustomer !== 3 && data.StoreId) {
                                storeId = data.StoreId;
                                selectedOrder().storeId(storeId);
                            } else {
                                storeId = data.CompanyId;
                                selectedOrder().storeId(undefined);
                            }
                            getBaseForCompany(data.CompanyId, storeId);
                        }
                        // If Signed by is not set in case of online order then set it
                        if (!isEstimateScreen() && !selectedOrder().orderReportSignedBy()) {
                            selectedOrder().setOrderReportSignedBy(loggedInUser());
                        }
                        if (isEstimateScreen() && !selectedOrder().reportSignedBy()) {
                            selectedOrder().reportSignedBy(loggedInUser());
                        }

                        // Reset Order Dirty State
                        selectedOrder().reset();

                        if (callback && typeof callback === "function") {
                            callback();
                        }
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
                                    selectedCompanyJobManagerUser(data.JobManagerId);
                                }
                                isCompanyBaseDataLoaded(true);
                            },
                            error: function (response) {
                                isCompanyBaseDataLoaded(true);
                                toastr.error("Failed to load details for selected company" + response);
                            }
                        });
                    },
                    //Get Inquiry Items 
                    getInquiryItems = function () {
                        if ((selectedOrder() == undefined && selectedOrder().enquiryId() == undefined) || selectedOrder().isInquiryItemLoaded()) {
                            return;
                        } else {
                            dataservice.getInquiryItems({
                                id: selectedOrder().enquiryId()
                            }, {
                                success: function (data) {
                                    selectedOrder().isInquiryItemLoaded(true);
                                    if (data.InquiryItems && data.InquiryItems.length > 0) {
                                        var items = [];
                                        _.each(data.InquiryItems, function (item) {
                                            items.push(model.InquiryItem.Create(item));
                                        });

                                        ko.utils.arrayPushAll(selectedOrder().inquiryItems(), items);
                                        selectedOrder().inquiryItems.valueHasMutated();
                                    }
                                },
                                error: function (response) {
                                    toastr.error('Failed to Load Inquiry Items: ' + response);
                                }
                            });
                        }
                    },
                    // #endregion Service Calls
                    //#region Dialog Product Section
                    orderProductItems = ko.observableArray([]),
                    productQuantitiesList = ko.observableArray([]),
                    //#region Product From Retail Store
                    onCreateNewProductFromRetailStore = function () {

                        if (selectedOrder().companyId() === undefined) {
                            toastr.error("Please select customer.");
                        } else {
                            // ReSharper disable AssignedValueIsNeverUsed
                            var companyId = 0;
                            // ReSharper restore AssignedValueIsNeverUsed
                            if (selectedOrder().storeId()) {
                                companyId = selectedOrder().storeId();
                            } else {
                                companyId = selectedOrder().companyId();
                            }
                            addProductVm.show(addItemFromRetailStore, companyId, costCentresBaseData(), currencySymbol(), selectedOrder().id(), saveSectionCostCenter, createitemForRetailStoreProduct, selectedCompanyTaxRate(), pageHeader(), isEstimateScreen() === true ? 'Estimate' : "Order", selectedOrder().companyName(), 2);
                        }
                        //addProductVm.show(addItemFromRetailStore, companyId, costCentresBaseData(), currencySymbol(), selectedOrder().id(), saveSectionCostCenter, createitemForRetailStoreProduct);
                    },
                    // Add Finished Goods
                     onAddFinishedGoods = function () {

                         if (selectedOrder().companyId() === undefined) {
                             toastr.error("Please select customer.");
                         } else {
                             // ReSharper disable AssignedValueIsNeverUsed
                             var companyId = 0;
                             // ReSharper restore AssignedValueIsNeverUsed
                             if (selectedOrder().storeId()) {
                                 companyId = selectedOrder().storeId();
                             } else {
                                 companyId = selectedOrder().companyId();
                             }
                             addProductVm.show(addItemFromFinishedGoods, companyId, costCentresBaseData(), currencySymbol(), selectedOrder().id(), saveSectionCostCenter, createitemForRetailStoreProduct, selectedCompanyTaxRate(), pageHeader(), isEstimateScreen() === true ? 'Estimate' : "Order", selectedOrder().companyName(), 3);
                         }
                     },

                    //},
                    //addItemFromRetailStore = function (newItem) {
                    //    selectedProduct(newItem);
                    //    selectedOrder().items.splice(0, 0, newItem);
                    //},
                    addItemFromRetailStore = function (newItem) {
                        selectedProduct(newItem);
                        selectedOrder().items.splice(0, 0, newItem);
                        itemDetailVm.updateOrderData(selectedOrder(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());
                        // Set Default Section to be used as a default for new sections in this item
                        itemDetailVm.defaultSection(newItem.itemSections()[0].convertToServerData());
                        //Req: Open Edit dialog of product on adding product
                        editItem(newItem);
                    },
                    addItemFromFinishedGoods = function (newItem) {
                        selectedProduct(newItem);
                        selectedOrder().items.splice(0, 0, newItem);
                        itemDetailVm.updateOrderData(selectedOrder(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());
                        selectedProduct().productType(3);
                        newItem.itemSections()[0].productType(3);
                        // Set Default Section to be used as a default for new sections in this item
                        itemDetailVm.defaultSection(newItem.itemSections()[0].convertToServerData());

                        //itemDetailVm.defaultSection().productType(3);
                        //Req: Open Edit dialog of product on adding product
                        editItem(newItem);
                    },
                    onAddCostCenter = function () {
                        // ReSharper disable AssignedValueIsNeverUsed
                        var companyId = 0;
                        // ReSharper restore AssignedValueIsNeverUsed
                        if (selectedOrder().storeId()) {
                            companyId = selectedOrder().storeId();
                        } else {
                            companyId = selectedOrder().companyId();
                        }
                        addCostCenterVM.show(afterSelectCostCenter, companyId, true, currencySymbol(), selectedCompanyTaxRate());
                    },
                    onAddCostCenterForProduct = function () {
                        getCostCentersForProduct();
                        // view.showCostCentersDialog();
                    },
                    onAddInventoryItem = function () {

                        isAddProductFromInventory(true);
                        openStockItemDialog();
                    },
                    closeCostCenterDialog = function () {
                        view.hideRCostCentersDialog();
                    },
                    getCostCenters = function () {
                        // ReSharper disable AssignedValueIsNeverUsed
                        var companyId = 0;
                        // ReSharper restore AssignedValueIsNeverUsed
                        if (selectedOrder().storeId()) {
                            companyId = selectedOrder().storeId();
                        } else {
                            companyId = selectedOrder().companyId();
                        }
                        dataservice.getCostCenters({
                            CompanyId: companyId,
                            SearchString: costCentrefilterText(),
                            PageSize: costCentrePager().pageSize(),
                            PageNo: costCentrePager().currentPage(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    costCentres.removeAll();
                                    _.each(data.CostCentres, function (item) {
                                        var costCentre = new itemModel.costCentre.Create(item);
                                        costCentres.push(costCentre);
                                    });
                                    costCentrePager().totalCount(data.RowCount);
                                }
                            },
                            error: function (response) {
                                costCentres.removeAll();
                                toastr.error("Failed to Load Cost Centres. Error: " + response);
                            }
                        });
                    },

                    getCostCentersForProduct = function () {
                        addCostCenterVM.show(afterSelectCostCenter, selectedOrder().companyId(), false, currencySymbol(), selectedCompanyTaxRate(), selectedCompanyTaxRate());
                    },
                    //onAddCostCenterCallback = function () {

                    //},
                    resetCostCentrefilter = function () {
                        costCentrefilterText('');
                        getCostCenters();
                    },
                    costCenterClickLIstner = function (costCentre) {
                        selectedCostCentre(costCentre);
                        view.showCostCentersQuantityDialog();
                    },

                    hideCostCentreQuantityDialog = function () {
                        view.hideCostCentersQuantityDialog();
                    },
                    hideCostCentreDialog = function () {
                        view.hideRCostCentersDialog();
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
                    //Method to make by default quantities fields 0 if they are undefined
                    updateQuantitiesValues = function () {
                        var qty1 = selectedCostCentre().quantity1();
                        var qty2 = selectedCostCentre().quantity2();
                        if (qty1 == undefined) {
                            selectedCostCentre().quantity1(0);
                        } if (qty2 == undefined) {
                            selectedCostCentre().quantity2(0);
                        }
                    },
                    onSaveProductInventory = function () {
                        var item = itemModel.Item.Create({ EstimateId: selectedOrder().id(), RefItemId: inventoryStockItemToCreate().id });
                        item.productName(inventoryStockItemToCreate().name);

                        updateQuantitiesValues();
                        item.qty1(selectedCostCentre().quantity1());
                        item.qty2(selectedCostCentre().quantity2());
                        item.qty3(selectedCostCentre().quantity3());
                        //Req: Item Product type is set to '2', so while editting item's section is non mandatory
                        item.productType(2);
                        item.qty1NetTotal(inventoryStockItemToCreate().price * selectedCostCentre().quantity1());
                        //item.qty1GrossTotal(inventoryStockItemToCreate().priceWithTax);
                        applyProductTax(item);

                        selectedProduct(item);
                        var itemSection = itemModel.ItemSection.Create({});
                        counterForSection = counterForSection - 1;
                        itemSection.id(counterForSection);
                        itemSection.name("Text Sheet");
                        itemSection.qty1(selectedCostCentre().quantity1());
                        itemSection.qty2(selectedCostCentre().quantity2());
                        itemSection.qty3(selectedCostCentre().quantity3());
                        itemSection.baseCharge1(inventoryStockItemToCreate().price * selectedCostCentre().quantity1());
                        //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                        itemSection.productType(2);

                        var sectionCostCenter = itemModel.SectionCostCentre.Create({});
                        sectionCostCenter.qty1(selectedCostCentre().quantity1());
                        sectionCostCenter.qty2(selectedCostCentre().quantity2());
                        sectionCostCenter.qty3(selectedCostCentre().quantity3());
                        sectionCostCenter.costCentreId(getStockCostCenterId(139));
                        sectionCostCenter.costCentreName(selectedCostCentre().name());
                        sectionCostCenter.name('Stock(s)');
                        sectionCostCenter.qty1NetTotal(inventoryStockItemToCreate().price * selectedCostCentre().quantity1());
                        sectionCostCenter.qty2NetTotal(0);
                        sectionCostCenter.qty2NetTotal(0);
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.qty1Charge(inventoryStockItemToCreate().price * selectedCostCentre().quantity1());
                        sectionCostCenter.qty2Charge(0);
                        sectionCostCenter.qty3Charge(0);
                        sectionCostCenter.costCentreType('139');

                        var sectionCostCenterDetail = itemModel.SectionCostCenterDetail.Create({});
                        sectionCostCenterDetail.stockName(inventoryStockItemToCreate().name);
                        sectionCostCenterDetail.costPrice(inventoryStockItemToCreate().price);
                        sectionCostCenterDetail.qty1(selectedCostCentre().quantity1());

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
                        selectedOrder().items.splice(0, 0, item);

                        selectedSection(itemSection);


                        //this method is calling to update orders list view total prices etc by trigering computed in item's detail view
                        itemDetailVm.updateOrderData(selectedOrder(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());
                        //Req: Open Edit dialog of product on adding product
                        editItem(item);
                    },
                    //#region product From Retail Store

                    //SelectedStockOption
                    selectedStockOption = ko.observable(),

                    //Selected Stock Option Sequence Number
                    selectedStockOptionSequenceNumber = ko.observable(),

                    //Selected Stock Option Name
                    selectedStockOptionName = ko.observable(),

                    //Selected Product Quanity 
                    selectedProductQuanity = ko.observable(),

                    //Total Product Price
                    totalProductPrice = ko.observable(0).extend({ numberInput: ist.numberFormat }),

                    //#endregion
                    //Get Inventories
                    getInventoriesListItems = function () {
                        dataservice.getInventoriesList({
                            SearchString: inventorySearchFilter(),
                            CategoryId: selectedCategoryId(),
                            PageSize: categoryPager().pageSize(),
                            PageNo: categoryPager().currentPage()
                        }, {
                            success: function (data) {
                                inventoryItems.removeAll();
                                _.each(data.StockItems, function (item) {
                                    var inventory = new model.Inventory.Create(item);
                                    inventoryItems.push(inventory);
                                });
                                categoryPager().totalCount(data.TotalCount);
                            },
                            error: function () {
                                isLoadingInventory(false);
                                toastr.error("Failed to load inventories.");
                            }
                        });
                    },

                    createitemForRetailStoreProduct = function (selectedItem) {
                        if (selectedItem === null || selectedItem === undefined) {
                            // ReSharper disable InconsistentFunctionReturns
                            return;
                            // ReSharper restore InconsistentFunctionReturns
                        }
                        var item = selectedItem.convertToServerData();
                        //item.EstimateId = orderId;
                        selectedSection(undefined);
                        var newItem = itemModel.Item.Create(item);
                        applyProductTax(newItem);
                        return newItem;
                    },
                    saveSectionCostCenter = function (newItem, sectionCostCenter, selectedStockOptionParam, selectedProductQuanityParam) {
                        sectionCostCenter.name('Web Order Cost Center');
                        var qty1Total = sectionCostCenter.qty1Charge();
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);

                        sectionCostCenter.qty2Charge(0);
                        sectionCostCenter.qty3Charge(0);
                        sectionCostCenter.qty1(selectedProductQuanityParam);
                        sectionCostCenter.qty2NetTotal(0);
                        sectionCostCenter.qty3NetTotal(0);
                        sectionCostCenter.qty1NetTotal(qty1Total || 0);

                        //Item's Quantity
                        newItem.qty1(selectedProductQuanityParam);
                        //Item's Section Quantity
                        newItem.itemSections()[0].qty1(selectedProductQuanityParam);

                        newItem.itemSections()[0].sectionCostCentres.push(sectionCostCenter);

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
                                    newItem.itemSections()[0].sectionCostCentres.push(sectionCostCenter);
                                }
                            });
                        }
                        //#endregion
                    },
                    //#endregion
                    //#region Add Blank Print Product
                    onCreateNewBlankPrintProduct = function () {
                        var newItem = itemModel.Item.Create({ EstimateId: selectedOrder().id() });
                        applyProductTax(newItem);
                        //Req: Item Product code is set to '1', so while editting item's section is mandatory
                        newItem.productType(1);
                        newItem.productName("Blank Sheet");
                        newItem.qty1(0);
                        newItem.qty1GrossTotal(0);

                        var itemSection = itemModel.ItemSection.Create({});
                        counterForSection = counterForSection - 1;
                        itemSection.id(counterForSection);
                        itemSection.name("Text Sheet");
                        //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                        itemSection.productType(2);
                        newItem.itemSections.push(itemSection);
                        var itemSectionForAddView = itemModel.ItemSection.Create({});
                        itemSectionForAddView.flagForAdd(true);
                        newItem.itemSections.push(itemSectionForAddView);
                        selectedOrder().items.splice(0, 0, newItem);
                        //Req: Open Edit dialog of product on adding product
                        editItem(newItem);
                    },
                    //#endregion

                    //#region Pre Payment
                    // Flag for to show Add Title In Pre Payment Dialog
                    flagForToShowAddTitle = ko.observable(true),
                    // Show Pre Payment Dialog
                    showOrderPrePaymentModal = function () {
                        selectedPrePayment(model.PrePayment());
                        view.showOrderPrePaymentModal();
                    },
                    hideOrderPrePaymentModal = function () {
                        view.hideOrderPrePaymentModal();
                    },
                    //Create Order Pre Payment
                    onCreateOrderPrePayment = function () {
                        flagForToShowAddTitle(true);
                        showOrderPrePaymentModal();
                    },
                    // Close Order Pre Payment
                    onCancelOrderPrePayment = function () {
                        hideOrderPrePaymentModal();
                    },
                    // Edit Pre Payment
                    onEditPrePayment = function (prePayment) {
                        flagForToShowAddTitle(false);
                        selectedPrePayment(prePayment);
                        view.showOrderPrePaymentModal();
                    },
                    //On Save Pre Payment
                    onSavePrePayment = function (prePayment) {
                        if (dobeforeSavePrePayment()) {
                            var paymentMethod = _.find(paymentMethods(), function (item) {
                                return item.PaymentMethodId === prePayment.paymentMethodId();
                            });
                            if (paymentMethod) {
                                prePayment.paymentMethodName(paymentMethod.MethodName);
                            }
                            if (prePayment.prePaymentId() === undefined) {
                                prePayment.prePaymentId(0);
                                selectedOrder().prePayments.splice(0, 0, prePayment);
                            }
                            hideOrderPrePaymentModal();
                        }
                    },
                    onDeletePrePayment = function (prePayment) {
                        confirmation.messageText("WARNING - All items will be removed from the system and you won’t be able to recover.  There is no undo");
                        confirmation.afterProceed(function () {
                            var index = selectedOrder().prePayments().indexOf(prePayment);
                            selectedOrder().prePayments.remove(selectedOrder().prePayments()[index]);
                            selectedOrder().hasDeletedPrepayments(true);
                            hideOrderPrePaymentModal();
                        });
                        confirmation.show();
                        return;
                    },
                    // Do Before Save
                    dobeforeSavePrePayment = function () {
                        var flag = true;
                        if (!selectedPrePayment().isValid()) {
                            selectedPrePayment().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    //#endregion

                    //#region Delivery Schedule
                    // Active Deliver Schedule
                    selectedDeliverySchedule = ko.observable(),
                    // Add Deliver Schedule
                    addDeliverySchedule = function () {
                        if (selectedOrder().items().length === 0) {
                            toastr.error("Please Add items first.");
                        } else {
                            if (selectedDeliverySchedule() !== undefined && !selectedDeliverySchedule().isValid()) {
                                selectedDeliverySchedule().errors.showAllMessages();
                                return;
                            }
                            if (selectedDeliverySchedule() !== undefined && selectedDeliverySchedule().isValid()) {
                                setDeliveryScheduleFields();
                            }
                            var deliverySchedule = model.ShippingInformation.Create({ EstimateId: selectedOrder().id() });
                            if (selectedOrder().items().length > 0) {
                                var item = selectedOrder().items()[0];
                                deliverySchedule.itemId(item.id());
                                setQuantityOfNewDeliverySchedule(deliverySchedule);
                            }
                            // deliverySchedule.deliveryNoteRaised(true);
                            selectedOrder().deliverySchedules.splice(0, 0, deliverySchedule);
                            selectedDeliverySchedule(selectedOrder().deliverySchedules()[0]);
                        }
                    },
                    // Set  Quantity Of new Added Delivery Schedule
                    setQuantityOfNewDeliverySchedule = function (deliverySchedule) {
                        if (deliverySchedule !== undefined && deliverySchedule !== null && deliverySchedule.itemId() !== undefined) {
                            var quantity = _.find(selectedOrder().items(), function (item) {
                                return item.id() === deliverySchedule.itemId();
                            });

                            if (quantity !== undefined && quantity !== null && quantity.qty1() !== undefined) {
                                var qt1 = parseInt(quantity.qty1());
                                var calculatedQuantity = 0;
                                if (quantity) {
                                    _.each(selectedOrder().deliverySchedules(), function (item) {
                                        if (item.itemId() === deliverySchedule.itemId()) {
                                            calculatedQuantity = calculatedQuantity + parseInt(item.quantity());
                                        }
                                    });
                                    deliverySchedule.quantity(qt1 - calculatedQuantity);
                                }
                            } else {
                                deliverySchedule.quantity(0);
                            }
                        }

                    },
                    // Select Deliver Schedule For Edit
                    selectDeliverySchedule = function (deliverSchedule) {
                        if (selectedDeliverySchedule() !== undefined && !selectedDeliverySchedule().isValid()) {
                            selectedDeliverySchedule().errors.showAllMessages();
                            return;
                        } else if (selectedDeliverySchedule() !== undefined && selectedDeliverySchedule().itemId() && selectedDeliverySchedule().quantity() !== undefined && selectedDeliverySchedule().quantity() !== "") {
                            var selectedItem = _.find(selectedOrder().items(), function (item) {
                                return item.id() === selectedDeliverySchedule().itemId();
                            });
                            if (checkForQuantity(selectedItem)) {
                                if (selectedDeliverySchedule() !== deliverSchedule) {
                                    setDeliveryScheduleFields();
                                    selectedDeliverySchedule(deliverSchedule);
                                }
                            }
                        } else {
                            if (selectedDeliverySchedule() !== deliverSchedule) {
                                setDeliveryScheduleFields();
                                selectedDeliverySchedule(deliverSchedule);
                            }
                        }

                    },
                    //
                    // ReSharper disable UnusedLocals
                    calculateDeliveryShedulePrice = ko.computed(function () {
                        // ReSharper restore UnusedLocals
                        if (selectedDeliverySchedule() !== undefined && selectedDeliverySchedule().itemId() && selectedDeliverySchedule().quantity() !== undefined && selectedDeliverySchedule().quantity() !== "") {
                            var selectedItem = _.find(selectedOrder().items(), function (item) {
                                return item.id() === selectedDeliverySchedule().itemId();
                            });

                            checkForQuantity(selectedItem);
                            // calculate Price Of Delievry Selected Schedule 
                            if (selectedItem && selectedDeliverySchedule().quantity() !== undefined && selectedItem.qty1NetTotal() !== undefined && selectedItem.qty1() !== undefined) {
                                var perUnitPrice = 0;
                                if (selectedItem.qty1() !== 0) {
                                    perUnitPrice = parseInt(selectedDeliverySchedule().quantity()) / parseInt(selectedItem.qty1());
                                }

                                var netPrice = (perUnitPrice * parseInt(selectedItem.qty1NetTotal())).toFixed(2);
                                selectedDeliverySchedule().price(netPrice);
                            }
                        }
                    }),
                    //
                    checkForQuantity = function (selectedItem) {
                        // Check Whether quantity is not greater than selected item Qty1 
                        if (selectedItem && selectedItem.qty1() !== undefined) {
                            var qt1 = parseInt(selectedItem.qty1());
                            var calculatedQuantity = 0;
                            _.each(selectedOrder().deliverySchedules(), function (item) {
                                if (item.itemId() === selectedDeliverySchedule().itemId()) {
                                    calculatedQuantity = parseInt(calculatedQuantity) + parseInt(item.quantity());
                                }
                            });

                            if (parseInt(calculatedQuantity) > qt1) {
                                selectedDeliverySchedule().quantity(0);
                                selectedDeliverySchedule().price(0);
                                toastr.error("Quantity can not be greater than selected Item Quantity.");
                                return false;
                            }
                        } else if (selectedItem && selectedItem.qty1() === undefined && selectedDeliverySchedule().quantity() !== undefined && selectedDeliverySchedule().quantity() > 0) {
                            selectedDeliverySchedule().quantity(0);
                            selectedDeliverySchedule().price(0);
                            toastr.error("Quantity can not be greater than selected Item Quantity.");
                            return false;
                        }
                        return true;
                    },
                    // Set Deliver Schedule Fields Like Item Name, Address Name for List View
                    setDeliveryScheduleFields = function () {
                        if (!selectedDeliverySchedule()) {
                            return;
                        }
                        var selectedItem = _.find(selectedOrder().items(), function (item) {
                            return item.id() === selectedDeliverySchedule().itemId();
                        });
                        if (selectedItem) {
                            selectedDeliverySchedule().itemName(selectedItem.productName());
                        }

                        var selectedAddressItem = _.find(companyAddresses(), function (item) {
                            return item.id === selectedDeliverySchedule().addressId();
                        });
                        if (selectedAddressItem) {
                            selectedDeliverySchedule().addressName(selectedAddressItem.name);
                        }
                    },
                    //Click in raised
                    onRaised = function () {
                        var raisedList = [];
                        // Check whether delivery schedule list is not empty
                        if (selectedOrder().deliverySchedules().length > 0) {
                            var deliveryScheduleItem = _.find(raisedList, function (raisedItem) {
                                return raisedItem.isSelected() === true;
                            });
                            // Check whether any item is selected
                            if (deliveryScheduleItem !== undefined) {
                                _.each(selectedOrder().deliverySchedules(), function (item) {
                                    if (item.isSelected()) {
                                        var deliverySchedule = _.find(raisedList, function (raisedItem) {
                                            return (raisedItem.itemId() === item.itemId() && raisedItem.addressId() === item.addressId());
                                        });
                                        if (deliverySchedule === undefined) {
                                            raisedList.push(item);
                                        }
                                    }
                                });
                            } else {
                                toastr.error("Please select items to add shipping information.");
                            }
                        } else {
                            toastr.error("Please select items to add shipping information.");
                        }

                    },
                    downloadArtwork = function () {
                        isLoadingOrders(true);
                        dataservice.downloadOrderArtwork({
                            OrderId: selectedOrder().id()
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    var host = window.location.host;
                                    var uri = encodeURI("http://" + host + data);
                                    window.open(uri, "_blank");
                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Error: Failed to Download Artwork." + response);
                            }
                        });
                    },




                    // Template Chooser For Delivery Schedule
                    templateToUseDeliverySchedule = function (deliverySchedule) {
                        return (deliverySchedule === selectedDeliverySchedule() ? 'ediDeliverScheduleTemplate' : 'itemDeliverScheduleTemplate');
                    },

                    // Delete Delivery Schedule
                    onDeleteDeliveryScheduleItem = function (deliverySchedule, e) {
                        selectDeliverySchedule(deliverySchedule);
                        if (selectedDeliverySchedule().deliveryNoteRaised()) {
                            toastr.error("Raised item cannot be deleted.");
                        } else {
                            confirmation.messageText("WARNING - All items will be removed from the system and you won’t be able to recover.  There is no undo");
                            confirmation.afterProceed(deleteDeliverySchedule);
                            confirmation.show();
                            return;
                        }
                        e.stopImmediatePropagation();
                    },
                    deleteDeliverySchedule = function () {
                        selectedOrder().deliverySchedules.remove(selectedDeliverySchedule());
                        selectedOrder().hasDeletedDeliverySchedules(true);
                        selectedDeliverySchedule(undefined);
                    },
                    //#endregion
                    //#endregion
                    //#region Estimate Screen

                    // Get Estimates
                    getEstimates = function (currentTab) {
                        isLoadingOrders(true);
                        currentScreen(currentTab);
                        dataservice.getEstimates({
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
                                orders.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapOrders(data.Orders);
                                    pager().totalCount(data.TotalCount);
                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Failed to load orders" + response);
                            }
                        });
                    },
                    openReportsOrder = function (isFromEditor) {
                        //reportManager.show(ist.reportCategoryEnums.Orders, isFromEditor == true ? true : false, 0);
                        reportManager.show(ist.reportCategoryEnums.Orders, 0, 0);
                    },
                    openExternalReportsOrder = function () {

                        //ContactId(oContactId);
                        //RecordId(oRecordId);
                        //CategoryId(oCategoryId);
                        //OrderId(oOrderId);
                        //CriteriaParam(oCriteriaParam);

                        reportManager.outputTo("preview");

                        if (orderHasChanges) {

                            isOpenReport(true);
                            isOpenReportEmail(false);
                            onSaveOrder();
                        }
                        else {
                            if (selectedOrder().isEstimate() == true) {
                                reportManager.OpenExternalReport(ist.reportCategoryEnums.Estimate, 1, selectedOrder().id());
                            }
                            else {
                                reportManager.OpenExternalReport(ist.reportCategoryEnums.Orders, 1, selectedOrder().id());
                            }
                        }






                        //reportManager.SetOrderData(selectedOrder().orderReportSignedBy(), selectedOrder().contactId(), selectedOrder().id(),"");
                        //reportManager.show(ist.reportCategoryEnums.Orders, 1, selectedOrder().id(), selectedOrder().companyName(), selectedOrder().orderCode(), selectedOrder().name());


                    },
                    openExternalEmailOrderReport = function () {
                        reportManager.outputTo("email");


                        if (orderHasChanges) {
                            isOpenReport(true);
                            isOpenReportEmail(true);
                            onSaveOrder();
                        }
                        else {
                            if (selectedOrder().isEstimate() == true) {
                                reportManager.SetOrderData(selectedOrder().orderReportSignedBy(), selectedOrder().contactId(), selectedOrder().id(), 3, selectedOrder().id(), "");
                                reportManager.OpenExternalReport(ist.reportCategoryEnums.Estimate, 1, selectedOrder().id());
                            } else {
                                reportManager.SetOrderData(selectedOrder().orderReportSignedBy(), selectedOrder().contactId(), selectedOrder().id(), 2, selectedOrder().id(), "");
                                reportManager.OpenExternalReport(ist.reportCategoryEnums.Orders, 1, selectedOrder().id());
                            }
                        }


                    },
                    copyEstimate = function() {
                        confirmation.messageText("Proceed To Copy Estimate ?");
                        confirmation.afterProceed(function () {
                            dataservice.copyEstimate({
                                id: selectedOrder().id()
                            }, {
                                success: function (data) {
                                    if (data) {
                                        setSelectedOrder(data);
                                        addItemInListViewOnCopying();
                                        toastr.success("Estimate Copied Successfully");
                                        isCopyiedEstimate(true);
                                    }
                                },
                                error: function (response) {
                                    toastr.error("Failed to Copy Estimate " + response);
                                }
                            });
                        });
                        confirmation.show();
                    },
                    addItemInListViewOnCopying = function () {
                        selectedOrder().flagColor(getSectionFlagColor(selectedOrder().sectionFlagId()));
                        orders.splice(0, 0, selectedOrder());
                    },

                    isCustomerEdittable = ko.computed(function() {
                        if (selectedOrder() !== undefined && (isCopyiedEstimate() || selectedOrder().id() === 0 || selectedOrder().id() === undefined)) {
                            return true;
                        }
                        return false;
                    }),

                    //#endregion
                    //#region Inquiries tab
                    inqiriesTabClick = function () {
                        orders.removeAll();
                        currentScreen(8);
                        pager().reset(0);
                        //
                        pager(new pagination.Pagination({ PageSize: 5 }, inquiries, getInquiries));
                        getInquiries();
                        if (!isInquiryBaseDataLoaded()) {
                            getInquiriesTabBaseData();
                        }
                    },
                    getInquiries = function () {
                        isLoadingOrders(true);
                        dataservice.getInquiries({
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
                                inquiries.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapInquiries(data.Inquiries);
                                    pager().totalCount(data.TotalCount);
                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Failed to load inquiries" + response);
                            }
                        });
                    },
                    createInquiry = function () {
                        selectedInquiry(model.Inquiry.Create({}, { SystemUsers: systemUsers(), PipelineSources: pipelineSources() }));
                        //When creating new inquiry by default the inquiry is "Draft Inquiry" and its status is 25(Status Table)
                        selectedInquiry().status(25);
                        selectedInquiry().isDirectInquiry(true);
                        selectedInquiry().createdBy(loggedInUser());
                        companyContacts.removeAll();
                        openOrderEditor();
                    },
                    itemAttachmentFileLoadedCallback = function (file, data) {
                        //Flag check, whether file is already exist in media libray
                        var flag = true;

                        _.each(selectedProduct().itemAttachments(), function (item) {
                            if (item.fileSourcePath() === data && item.fileName() === file.name) {
                                flag = false;
                            }
                        });

                        if (flag) {
                            var attachment = model.InquiryAttachment.Create({});
                            attachment.attachmentId(undefined);
                            attachment.attachmentPath(data);
                            attachment.orignalFileName(file.name);
                            attachment.extension(file.type);
                            attachment.inquiryId(selectedInquiry().inquiryId());
                            selectedInquiry().inquiryAttachments.push(attachment);
                        }
                    },
                    onCreateNewInquiryDetailItem = function () {
                        selectedInquiryItem(model.InquiryItem.Create({}));
                        selectedInquiryItem().inquiryItemId(inquiryDetailItemCounter);
                        inquiryDetailItemCounter--;
                        view.showInquiryDetailItemDialog();
                        isNewinquiryDetailItem(true);
                    },
                    editInquiry = function (inquiry) {
                        errorList.removeAll();
                        isLoadingOrders(true);
                        isCompanyBaseDataLoaded(false);
                        companyContacts.removeAll();
                        dataservice.getInquiry({
                            id: inquiry.inquiryId()
                        }, {
                            success: function (data) {
                                if (data) {
                                    selectedInquiry(model.Inquiry.Create(data), { SystemUsers: systemUsers(), PipelineSources: pipelineSources() });
                                    openOrderEditor();
                                }
                                getBaseForInquiry(data.CompanyId, data.CompanyId);
                                isLoadingOrders(false);
                                selectedInquiry().reset();
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Failed to load Inquiry details" + response);
                            }
                        });
                    },
                    editInquiryItem = function (inquiryItem) {
                        //selectedInquiryItem(inquiryItem);
                        inquiryDetailEditorViewModel.selectItem(inquiryItem);
                        view.showInquiryDetailItemDialog();
                        isNewinquiryDetailItem(false);
                    },
                    onSaveInquiry = function () {
                        if (!doBeforeSaveInquiry()) {
                            return;
                        }
                        var inquiry = selectedInquiry().convertToServerData();

                        var itemsArray = [];
                        _.each(selectedInquiry().inquiryAttachments(), function (obj) {
                            var item = obj.convertToServerData(); // item converted 
                            var attArray = [];
                            _.each(item.InquiryAttachments, function (att) {
                                var attachment = att.convertToServerData(); // item converted 
                                //attchment.ContactId = selectedOrder().contactId();
                                attArray.push(attachment);
                            });
                            item.InquiryAttachments = attArray;
                            itemsArray.push(item);

                        });

                        inquiry.InquiryAttachments = itemsArray;

                        //_.each(selectedInquiry().inquiryAttachments(), function(item) {
                        //    inquiry.inquiryAttachments.push(item.convertToServerData());
                        //});
                        _.each(selectedInquiry().inquiryItems(), function (item) {
                            inquiry.InquiryItems.push(item.convertToServerData());
                        });
                        dataservice.saveInquiry(inquiry, {
                            success: function (data) {
                                data.CompanyName = selectedInquiry().companyName();
                                selectedInquiry(model.Inquiry.Create(data), { SystemUsers: systemUsers(), PipelineSources: pipelineSources() });
                                inquiries.splice(0, 0, selectedInquiry());
                                toastr.success("Saved Successfully !");
                                closeOrderEditor();
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Inquiry. Error: " + response);
                            }
                        });
                    },
                    doBeforeSaveInquiry = function () {
                        var flag = true;
                        if (!selectedInquiry().isValid()) {
                            selectedInquiry().showAllErrors();
                            selectedInquiry().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    doBeforeInquiryItemDetailSave = function () {
                        var flag = true;
                        if (!selectedInquiryItem().isValid()) {
                            selectedInquiryItem().showAllErrors();
                            //selectedInquiryItem().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    onSaveInquiryDetailItem = function () {
                        if (doBeforeInquiryItemDetailSave()) {
                            if (isNewinquiryDetailItem()) {
                                updateSelectedProductMarketingSource();
                                selectedInquiry().inquiryItems.splice(0, 0, selectedInquiryItem());
                            } else {
                                _.each(selectedInquiry().inquiryItems(), function (inquiryItem) {
                                    if (inquiryItem.inquiryItemId() == selectedInquiryItem().inquiryItemId()) {
                                        selectedInquiry().inquiryItems.remove(inquiryItem);
                                        updateSelectedProductMarketingSource();
                                        selectedInquiry().inquiryItems.splice(0, 0, selectedInquiryItem());
                                    }
                                });
                            }

                            view.hideInquiryDetailItemDialog();
                        }
                    },
                    updateSelectedProductMarketingSource = function () {
                        _.each(pipelineProducts(), function (product) {
                            if (product.id == selectedInquiryItem().productId()) {
                                selectedInquiryItem().marketingSource(product.name);
                            }
                        });
                    },
                    onCloseInquiryDetailItem = function () {
                        view.hideInquiryDetailItemDialog();
                    },
                    //Get Company Base Data
                    getBaseForInquiry = function (id, storeId) {
                        isCompanyBaseDataLoaded(false);
                        dataservice.getBaseDataForCompany({
                            id: id,
                            storeId: storeId
                        }, {
                            success: function (data) {
                                if (data) {
                                    if (data.CompanyContacts) {
                                        mapList(companyContacts, data.CompanyContacts, model.CompanyContact);
                                        setDefaultContactForInquiry();
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
                    //Select Default Contact For Inquiry in case of new order
                    setDefaultContactForInquiry = function () {
                        if (selectedInquiry().inquiryId() > 0) {
                            return;
                        }
                        var defaultContact = companyContacts.find(function (contact) {
                            return contact.isDefault;
                        });
                        if (defaultContact) {
                            selectedInquiry().contactId(defaultContact.id);
                        }
                    },
                    //Update Company Contacts Details 
                    // ReSharper disable once UnusedLocals
                    updateSelectedCompanyContactDetails = ko.computed(function () {
                        if (selectedInquiry() != undefined && selectedInquiry().contactId() != undefined &&
                            companyContacts().length > 0) {
                            _.each(companyContacts(), function (contact) {
                                if (contact.id == selectedInquiry().contactId()) {
                                    selectedCompanyContactOfInquiry(contact);
                                }
                            });
                        }
                    }),
                    onProgressToEstimate = function () {
                        confirmation.afterProceed(progressInquiryToEstimate);
                        confirmation.afterCancel(function () {
                        });
                        confirmation.show();
                        return;
                    },
                    progressInquiryToEstimate = function () {
                        dataservice.progressInquiryToEstimate({
                            InquiryId: selectedInquiry().inquiryId(),
                            CompanyId: selectedInquiry().companyId(),
                            ContactId: selectedInquiry().contactId(),
                            FlagId: sectionFlags().length > 0 ? sectionFlags()[0].id : undefined,
                            Title: selectedInquiry().title()
                        }, {
                            success: function (data) {
                                selectedInquiry().status(26);
                                selectedInquiry().estimateId(data.EstimateId);
                                viewEstimateFromInquiry();
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Order. Error: " + response);
                            }
                        });
                    },
                    viewEstimateFromInquiry = function () {
                        currentScreen(1);
                        isDisplayInquiryDetailScreen(false);
                        var id = selectedInquiry().estimateId();
                        getOrderById(id, openOrderEditor);
                        $('#estimateListTabs a[href="#tab-All"]').tab('show');
                        getOrdersOnTabChange(1);
                    },
                    showEstimateNotes = function () {
                        toastr.success('wow');
                    },
                    getInquiriesTabBaseData = function () {
                        dataservice.getBaseDataForInquiry({}, {
                            success: function (data) {
                                if (data) {
                                    if (data.SectionFlags) {
                                        mapList(inquiriesSectionFlags, data.SectionFlags, model.SectionFlag);
                                    }
                                }
                                isInquiryBaseDataLoaded(true);
                            },
                            error: function (response) {
                                toastr.error("Failed to load details for inquiries" + response);
                            }
                        });
                    },
                    //#endregion
                    //#region Progress To Order
                    progressToOrderHandler = function () {
                        estimateToBeProgressed(model.Estimate.Create(selectedOrder().convertToServerData(), { SystemUsers: systemUsers() }));
                        estimateToBeProgressed().setCreditiLimitSetBy(loggedInUser());
                        estimateToBeProgressed().setAllowJobWoCreditCheckSetBy(loggedInUser());
                        estimateToBeProgressed().setOfficialOrderSetBy(loggedInUser());
                        // Map Items if any
                        if (selectedOrder().items() && selectedOrder().items().length > 0) {
                            var items = [];

                            _.each(selectedOrder().items(), function (item) {
                                //item.id(undefined);
                                item.jobSelectedQty('1');
                                items.push(itemModel.Item.Create(item.convertToServerData()));
                            });

                            // Push to Original Item
                            ko.utils.arrayPushAll(estimateToBeProgressed().items(), items);
                            estimateToBeProgressed().items.valueHasMutated();
                        }
                        view.showProgressToOrderDialog();
                    },

                    onSaveEstimateProgressedToOrder = function () {
                        if (isNaN(view.orderstate()) || view.orderstate() === 0) {
                            estimateToBeProgressed().statusId(4); // Pending orders
                        }
                        var order = estimateToBeProgressed().convertToServerData();

                        // Map Items if any
                        var itemsArray = [];
                        _.each(estimateToBeProgressed().items(), function (obj) {
                            var item = obj.convertToServerData();
                            var attArray = [];
                            _.each(item.ItemAttachment, function (att) {
                                var attchment = att.convertToServerData();
                                attchment.ContactId = selectedOrder().contactId();
                                attArray.push(attchment);
                            });
                            item.ItemAttachments = attArray;
                            itemsArray.push(item);
                        });
                        order.Items = itemsArray;

                        dataservice.progressOrderToEstimate(order, {
                            success: function (data) {
                                view.hideProgressToOrderDialog();
                                //isOrderDetailsVisible(false);
                                selectedOrder().statusId(39);
                                selectedOrder().refEstimateId(data.EstimateId);
                                toastr.success('Estimate Progressed To Order Successfully!');
                                //updateEstimateAndEstimateOnProgress(selectedOrder().id(), data.EstimateId);
                            },
                            error: function (response) {
                                toastr.error("Failed to Progress Estimate to Order. Error: " + response);
                            }
                        });
                    },
                    //Update Qunatities Of items before saving Of Estimate Progress To Order
                    updateQuantities = function (obj, itemToBeUpdated) {
                        if (obj.jobSelectedQty() == 1) {
                            itemToBeUpdated.qty2(0);
                            itemToBeUpdated.qty3(0);
                        } if (obj.jobSelectedQty() == 2) {
                            var quantity2 = obj.qty2();
                            itemToBeUpdated.qty1(quantity2);
                            itemToBeUpdated.qty2(0);
                            itemToBeUpdated.qty3(0);
                        } if (obj.jobSelectedQty() == 3) {
                            var quantity3 = obj.qty3();
                            itemToBeUpdated.qty1(quantity3);
                            itemToBeUpdated.qty2(0);
                            itemToBeUpdated.qty3(0);
                        }
                    },

                    updateEstimateAndEstimateOnProgress = function (estimateId, orderId) {
                        dataservice.progressEstimateToOrder({
                            EstimateId: estimateId,
                            OrderId: orderId
                        }, {
                            success: function (data) {
                                view.hideProgressToOrderDialog();
                                isOrderDetailsVisible(false);
                                selectedOrder().statusId(39);
                                toastr.success('Estimate Progressed To Order Successfully!');
                            },
                            error: function (response) {
                                toastr.error("Failed to Progress Estimate to Order. Error: " + response);
                            }
                        });
                    },
                    //#endregion
                        myVal = ko.observable(),
                    //#region INITIALIZE

                    //Initialize method to call in every screen
                        initializeScreen = function (specifiedView) {
                            view = specifiedView;
                            ko.applyBindings(view.viewModel, view.bindingRoot);

                            categoryPager(new pagination.Pagination({ PageSize: 5 }, categories, getInventoriesListItems));
                            costCentrePager(new pagination.Pagination({ PageSize: 5 }, costCentres, getCostCentersForProduct));

                            // On Dropdown filter selection change get orders
                            subscribeDropdownFilterChange();
                        },
                    // Initialize the view model
                        initialize = function (specifiedView) {
                            initializeScreen(specifiedView);
                            pager(new pagination.Pagination({ PageSize: 10 }, orders, getOrders));
                            isEstimateScreen(false);
                            // Get Base Data
                            getBaseData();
                            var orderIdFromDashboard = $('#OrderId').val();
                            var itemIdFromOrderScreen = $('#ItemId').val();
                            itemIdFromDashboard(itemIdFromOrderScreen);
                            if (orderIdFromDashboard != 0) {
                                editOrder({ id: function () { return orderIdFromDashboard; } });
                            } else {
                                getOrders(4);
                            }
                        },
                    //Initialize Estimate
                        initializeEstimate = function (specifiedView) {
                            initializeScreen(specifiedView);
                            pager(new pagination.Pagination({ PageSize: 5 }, orders, getEstimates));
                            isEstimateScreen(true);
                            var estimateIdFromOrderScreen = $('#OrderId').val();
                            if (estimateIdFromOrderScreen != 0) {
                                editOrder({ id: function () { return estimateIdFromOrderScreen; } });
                            }

                            getEstimates();
                            // Get Base Data For Estimate
                            getBaseDataForEstimate();
                        };
                //#endregion
                return {
                    // #region Observables
                    selectedOrder: selectedOrder,
                    estimateToBeProgressed: estimateToBeProgressed,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoadingOrders: isLoadingOrders,
                    orders: orders,
                    isOrderDetailsVisible: isOrderDetailsVisible,
                    isDisplayInquiryDetailScreen: isDisplayInquiryDetailScreen,
                    isItemDetailVisible: isItemDetailVisible,
                    isSectionDetailVisible: isSectionDetailVisible,
                    pager: pager,
                    costCentrePager: costCentrePager,
                    errorList: errorList,
                    filterText: filterText,
                    pageHeader: pageHeader,
                    shared: shared,
                    selectedAddress: selectedAddress,
                    selectedCompanyContact: selectedCompanyContact,
                    selectedCompanyContactOfInquiry: selectedCompanyContactOfInquiry,
                    companyContacts: companyContacts,
                    companyAddresses: companyAddresses,
                    sectionFlags: sectionFlags,
                    systemUsers: systemUsers,
                    pipelineSources: pipelineSources,
                    selectedProduct: selectedProduct,
                    selectedJobDescription: selectedJobDescription,
                    jobStatuses: jobStatuses,
                    nominalCodes: nominalCodes,
                    initialize: initialize,
                    resetFilter: resetFilter,
                    filterOrders: filterOrders,
                    editOrder: editOrder,
                    createOrder: createOrder,
                    onSaveOrder: onSaveOrder,
                    onCloseOrderEditor: onCloseOrderEditor,
                    onArchiveOrder: onArchiveOrder,
                    gotoElement: gotoElement,
                    onCloneOrder: onCloneOrder,
                    openCompanyDialog: openCompanyDialog,
                    closeItemDetail: closeItemDetail,
                    openItemDetail: openItemDetail,
                    addItem: addItem,
                    editItem: editItem,
                    saveProduct: saveProduct,
                    deleteProduct: deleteProduct,
                    currentScreen: currentScreen,
                    orderType: orderType,
                    orderTypeFilter: orderTypeFilter,
                    flagItem: flagItem,
                    flagSelection: flagSelection,
                    filterFlags: filterFlags,
                    selectedFilterFlag: selectedFilterFlag,
                    orderCodeHeader: orderCodeHeader,
                    itemCodeHeader: itemCodeHeader,
                    sectionHeader: sectionHeader,
                    onAddInventoryItem: onAddInventoryItem,
                    selecteditem: selecteditem,
                    selectedStockItem: selectedStockItem,
                    selectedStockOptionName: selectedStockOptionName,
                    selectedStockOptionSequenceNumber: selectedStockOptionSequenceNumber,
                    selectedStockOption: selectedStockOption,
                    deleteOrderButtonHandler: deleteOrderButtonHandler,
                    productQuantitiesList: productQuantitiesList,
                    openExternalReportsOrder: openExternalReportsOrder,
                    inks: inks,
                    inkCoverageGroup: inkCoverageGroup,
                    selectedSectionCostCenter: selectedSectionCostCenter,
                    selectedQty: selectedQty,
                    selectedProductQuanity: selectedProductQuanity,
                    totalProductPrice: totalProductPrice,
                    onShippingChargesClick: onShippingChargesClick,
                    onCostCenterClick: onCostCenterClick,
                    isAddProductFromInventory: isAddProductFromInventory,
                    isAddProductForSectionCostCenter: isAddProductForSectionCostCenter,
                    pipelineProducts: pipelineProducts,
                    isInquiryBaseDataLoaded: isInquiryBaseDataLoaded,
                    inquiriesSectionFlags: inquiriesSectionFlags,
                    isCustomerEdittable: isCustomerEdittable,
                    //#endregion Utility Methods
                    //#region Estimate Screen
                    initializeEstimate: initializeEstimate,
                    isEstimateScreen: isEstimateScreen,
                    copyEstimate: copyEstimate,
                    //#endregion
                    //#region Dialog Product Section
                    orderProductItems: orderProductItems,
                    getOrders: getOrders,
                    getOrdersOfCurrentScreen: getOrdersOfCurrentScreen,
                    getOrdersOnTabChange: getOrdersOnTabChange,
                    openStockItemDialogForAddingProduct: openStockItemDialogForAddingProduct,
                    //#region Product From Retail Store
                    onCreateNewProductFromRetailStore: onCreateNewProductFromRetailStore,
                    onAddCostCenter: onAddCostCenter,
                    onCloseCostCenterDialog: closeCostCenterDialog,
                    costCentres: costCentres,
                    costCentresBaseData: costCentresBaseData,
                    getCostCenters: getCostCenters,
                    costCentrefilterText: costCentrefilterText,
                    resetCostCentrefilter: resetCostCentrefilter,
                    costCenterClickListner: costCenterClickLIstner,
                    selectedCostCentre: selectedCostCentre,
                    hideCostCentreQuantityDialog: hideCostCentreQuantityDialog,
                    hideCostCentreDialog: hideCostCentreDialog,
                    selectedSection: selectedSection,
                    markups: markups,
                    selectedMarkup1: selectedMarkup1,
                    selectedMarkup2: selectedMarkup2,
                    selectedMarkup3: selectedMarkup3,
                    categories: categories,
                    selectedCategoryId: selectedCategoryId,
                    categoryPager: categoryPager,
                    inventorySearchFilter: inventorySearchFilter,
                    getInventoriesListItems: getInventoriesListItems,
                    vatList: vatList,
                    //#endregion
                    //#region Pre Payment
                    paymentMethods: paymentMethods,
                    onCreateOrderPrePayment: onCreateOrderPrePayment,
                    onCancelOrderPrePayment: onCancelOrderPrePayment,
                    currencySymbol: currencySymbol,
                    selectedPrePayment: selectedPrePayment,
                    onSavePrePayment: onSavePrePayment,
                    onEditPrePayment: onEditPrePayment,
                    inventoryItems: inventoryItems,
                    flagForToShowAddTitle: flagForToShowAddTitle,
                    //#endregion
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,


                    onSaveProductInventory: onSaveProductInventory,
                    //#endregion
                    //#region Delivery Schedule
                    selectDeliverySchedule: selectDeliverySchedule,
                    addDeliverySchedule: addDeliverySchedule,
                    selectedDeliverySchedule: selectedDeliverySchedule,
                    templateToUseDeliverySchedule: templateToUseDeliverySchedule,
                    onRaised: onRaised,
                    onDeleteDeliveryScheduleItem: onDeleteDeliveryScheduleItem,
                    //#endregion
                    //#region Section Detail
                    paperSizes: paperSizes,
                    downloadArtwork: downloadArtwork,
                    //#endregion
                    //#region Inquiries tab
                    inqiriesTabClick: inqiriesTabClick,
                    createInquiry: createInquiry,
                    selectedInquiry: selectedInquiry,
                    itemAttachmentFileLoadedCallback: itemAttachmentFileLoadedCallback,
                    onCreateNewInquiryDetailItem: onCreateNewInquiryDetailItem,
                    onCloseInquiryDetailItem: onCloseInquiryDetailItem,
                    onSaveInquiryDetailItem: onSaveInquiryDetailItem,
                    selectedInquiryItem: selectedInquiryItem,
                    editInquiry: editInquiry,
                    editInquiryItem: editInquiryItem,
                    onSaveInquiry: onSaveInquiry,
                    inquiries: inquiries,
                    onProgressToEstimate: onProgressToEstimate,
                    viewEstimateFromInquiry: viewEstimateFromInquiry,
                    showEstimateNotes: showEstimateNotes,
                    //#endregion
                    //#region Utility Functions
                    createNewCostCenterProductForShippingCharge: createNewCostCenterProductForShippingCharge,
                    progressToOrderHandler: progressToOrderHandler,
                    onSaveEstimateProgressedToOrder: onSaveEstimateProgressedToOrder,
                    getInquiryItems: getInquiryItems,
                    onCreateNewBlankPrintProduct: onCreateNewBlankPrintProduct,
                    grossTotal: grossTotal,
                    onOrderStatusChange: onOrderStatusChange,
                    selectedItemForProgressToJobWizard: selectedItemForProgressToJobWizard,
                    clickOnJobToProgressWizard: clickOnJobToProgressWizard,
                    openReportsOrder: openReportsOrder,
                    orderHasChanges: orderHasChanges,
                    saveSectionCostCenter: saveSectionCostCenter,
                    createitemForRetailStoreProduct: createitemForRetailStoreProduct,
                    editSection: editSection,
                    createNewCostCenterProduct: createNewCostCenterProduct,
                    openExternalEmailOrderReport: openExternalEmailOrderReport,
                    selectedEstimatePhraseContainer: selectedEstimatePhraseContainer,
                    selectEstimatePhraseContainer: selectEstimatePhraseContainer,
                    openPhraseLibrary: openPhraseLibrary,
                      formatSelection: formatSelection,
                    formatResult: formatResult,
                    onDeletePrePayment: onDeletePrePayment,
                    onAddFinishedGoods: onAddFinishedGoods,
                    onCreateNewCostCenterProduct: onCreateNewCostCenterProduct,
                    sectionFlagsForListView: sectionFlagsForListView,
                    //#endregion
                    myVal: myVal
                };
            })()
        };
        return ist.order.viewModel;
    });
