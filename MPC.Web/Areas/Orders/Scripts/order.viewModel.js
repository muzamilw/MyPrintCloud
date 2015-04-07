﻿/*
    Module with the view model for the Order.
*/
define("order/order.viewModel",
    ["jquery", "amplify", "ko", "order/order.dataservice", "order/order.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/phraseLibrary.viewModel", "common/stockItem.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, phraseLibrary, stockDialog) {
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
                    // flag colors
                    sectionFlags = ko.observableArray([]),
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
                    //
                    selectedCompanyTaxRate = ko.observable(),
                    // Errors List
                    errorList = ko.observableArray([]),
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
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingOrders = ko.observable(false),
                    // Is Calculating Ptv
                    isPtvCalculationInProgress = ko.observable(false),
                    // Is Order Editor Visible
                    isOrderDetailsVisible = ko.observable(false),
                    // Is Item Detail Visible
                    isItemDetailVisible = ko.observable(false),
                    // Is Section Detail Visible
                    isSectionDetailVisible = ko.observable(false),
                    // Is Company Base Data Loaded
                    isCompanyBaseDataLoaded = ko.observable(false),
                    // #endregion
                    // #region Observables
                    // filter
                    filterText = ko.observable(),
                    // Selected Product
                    selectedProduct = ko.observable(model.Item.Create({})),
                    // Base Charge 1 Total
                    baseCharge1Total = ko.observable(0),
                    // Base Charge 2 Total
                    baseCharge2Total = ko.observable(0),
                    // Base Charge 3 Total
                    baseCharge3Total = ko.observable(0),
                    // Selected Markup 1
                    selectedMarkup1 = ko.observable(0),
                    // Selected Markup 2
                    selectedMarkup2 = ko.observable(0),
                    // Selected Markup 3
                    selectedMarkup3 = ko.observable(0),
                    // Selected Category Id
                    selectedCategoryId = ko.observable(),
                    // Inventory SearchFilter
                    inventorySearchFilter = ko.observable(),
                    costCentrefilterText = ko.observable(),
                    selectedCostCentre = ko.observable(),
                    orderCodeHeader = ko.observable(''),
                    itemCodeHeader = ko.observable(''),
                    sectionHeader = ko.observable(''),
                    currencySymbol = ko.observable(''),
                    // Active Order
                    selectedOrder = ko.observable(model.Estimate.Create({})),
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
                    sectionInkCoverage = ko.observableArray([]),
                    // Available Ink Plate Sides
                    availableInkPlateSides = ko.computed(function () {
                        if (!selectedSection() || (selectedSection().isDoubleSided() === null || selectedSection().isDoubleSided() === undefined)) {
                            return inkPlateSides();
                        }

                        return inkPlateSides.filter(function (inkPlateSide) {
                            return inkPlateSide.isDoubleSided === selectedSection().isDoubleSided();
                        });
                    }),
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    //Current Screen
                    currentScreen = ko.observable(),
                    //Selected Filter Flag on List View
                    selectedFilterFlag = ko.observable(0),
                    // Active Pre Payment
                    selectedPrePayment = ko.observable(),
                    //Selected item
                    selecteditem = ko.observable(),
                    //Selected Stock Item
                    selectedStockItem = ko.observable(),
                    // #endregion
                    // #region Utility Functions
                    // Create New Order
                    createOrder = function () {
                        selectedOrder(model.Estimate.Create({}));
                        openOrderEditor();
                    },
                    // Edit Order
                    editOrder = function (data) {
                        var code = !data.code() ? "ORDER CODE" : data.code();
                        orderCodeHeader(code);
                        getOrderById(data.id(), openOrderEditor);
                    },
                    // Open Editor
                    openOrderEditor = function () {
                        isOrderDetailsVisible(true);
                    },
                    // On Close Editor
                    onCloseOrderEditor = function () {
                        if (selectedOrder().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(onSaveOrder);
                            confirmation.afterCancel(function () {
                                selectedOrder().reset();
                                closeOrderEditor();
                                orderCodeHeader('');
                                sectionHeader('');
                                itemCodeHeader('');
                                isSectionDetailVisible(false);
                                isItemDetailVisible(false);
                            });
                            confirmation.show();
                            return;
                        }
                        closeOrderEditor();
                    },
                    // Close Editor
                    closeOrderEditor = function () {
                        selectedOrder(model.Estimate.Create({}));
                        isOrderDetailsVisible(false);
                        errorList.removeAll();
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
                        companySelector.show(onSelectCompany, [0, 1], true);
                    },
                    // On Select Company
                    onSelectCompany = function (company) {
                        if (!company) {
                            return;
                        }

                        if (selectedOrder().companyId() === company.id) {
                            return;
                        }

                        selectedOrder().companyId(company.id);
                        selectedOrder().companyName(company.name);

                        // Get Company Address and Contacts
                        getBaseForCompany(company.id, company.storeId);
                    },
                    // Add Item
                    addItem = function () {
                        // Open Product Selector Dialog
                    },
                    // Edit Item
                    editItem = function (item) {
                        itemCodeHeader(item.code());
                        selectedProduct(item);
                        if (item.tax1() === undefined) {
                            if (selectedCompanyTaxRate() !== undefined && selectedCompanyTaxRate() !== null) {
                                item.tax1(selectedCompanyTaxRate());
                            } else if (item.defaultItemTax() !== undefined && item.defaultItemTax() !== null) {
                                item.tax1(item.defaultItemTax());
                            } else {
                                item.tax1(0);
                                item.taxRateIsDisabled(true);
                            }
                        }

                        // calculateSectionChargeTotal();
                        openItemDetail();
                    },
                    // Open Item Detail
                    openItemDetail = function () {
                        isItemDetailVisible(true);
                        view.initializeLabelPopovers();
                    },
                    // Calculates Section Charges 
                    calculateSectionChargeTotal = ko.computed(function () {
                        if (selectedProduct().itemSections().length > 0) {
                            baseCharge1Total(0);
                            baseCharge2Total(0);
                            baseCharge3Total(0);
                            _.each(selectedProduct().itemSections(), function (item) {
                                if (item.qty1Profit() === undefined || item.qty1Profit() === "") {
                                    item.qty1Profit(0);
                                }
                                if (item.qty2Profit() === undefined || item.qty2Profit() === "") {
                                    item.qty2Profit(0);
                                }
                                if (item.qty3Profit() === undefined || item.qty3Profit() === "") {
                                    item.qty3Profit(0);
                                }
                                var basCharge1 = parseFloat(((item.baseCharge1() !== undefined && item.baseCharge1() !== "") ? item.baseCharge1() : 0));
                                baseCharge1Total(parseFloat(baseCharge1Total()) + basCharge1 + parseFloat(item.qty1Profit()));
                                baseCharge2Total(parseFloat(baseCharge2Total()) + parseFloat(((item.baseCharge2() !== undefined && item.baseCharge2() !== "") ? item.baseCharge2() : 0)) + parseFloat(item.qty2Profit()));
                                baseCharge3Total(parseFloat(baseCharge3Total()) + parseFloat(((item.baseCharge3() !== undefined && item.baseCharge3() !== "") ? item.baseCharge3() : 0)) + parseFloat(item.qty3Profit()));
                            });
                        }

                    }),
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
                    // Add Section
                    addSection = function () {
                        // Open Product Selector Dialog
                    },
                    // Edit Section
                    editSection = function (item) {
                        sectionHeader("SECTION - " + item.sectionNo());
                        selectedSection(item);
                        openSectionDetail();
                    },
                    // Open Section Detail
                    openSectionDetail = function () {
                        isSectionDetailVisible(true);
                        view.initializeLabelPopovers();
                        
                        // Subscribe Section Changes
                        subscribeSectionChanges();
                    },
                    // Close Section Detail
                    closeSectionDetail = function () {
                        sectionHeader('');
                        isSectionDetailVisible(false);

                    },
                    // Select Job Description
                    selectJobDescription = function (jobDescription, e) {
                        selectedJobDescription(e.currentTarget.id);
                    },
                    // Open Phrase Library
                    openPhraseLibrary = function () {
                        phraseLibrary.isOpenFromPhraseLibrary(false);
                        phraseLibrary.show(function (phrase) {
                            updateJobDescription(phrase);
                        });
                    },
                    // Update Job Description
                    updateJobDescription = function (phrase) {
                        if (!phrase) {
                            return;
                        }

                        // Set Phrase to selected Job Description
                        if (selectedJobDescription() === 'txtDescription1') {
                            selectedProduct().jobDescription1(selectedProduct().jobDescription1() ? selectedProduct().jobDescription1() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription2') {
                            selectedProduct().jobDescription2(selectedProduct().jobDescription2() ? selectedProduct().jobDescription2() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription3') {
                            selectedProduct().jobDescription3(selectedProduct().jobDescription3() ? selectedProduct().jobDescription3() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription4') {
                            selectedProduct().jobDescription4(selectedProduct().jobDescription4() ? selectedProduct().jobDescription4() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription5') {
                            selectedProduct().jobDescription5(selectedProduct().jobDescription5() ? selectedProduct().jobDescription5() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription6') {
                            selectedProduct().jobDescription6(selectedProduct().jobDescription6() ? selectedProduct().jobDescription6() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription7') {
                            selectedProduct().jobDescription7(selectedProduct().jobDescription7() ? selectedProduct().jobDescription7() + ' ' + phrase : phrase);
                        }
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
                    // Map Orders 
                    mapOrders = function (data) {
                        var ordersList = [];
                        _.each(data, function (order) {
                            order.FlagColor = getSectionFlagColor(order.SectionFlagId);
                            ordersList.push(model.Estimate.Create(order));
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
                        if (!doBeforeSave()) {
                            return;
                        }
                        _.each(selectedOrder().prePayments(), function (item) {
                            item.customerId(selectedOrder().companyId());
                        });
                        saveOrder(closeOrderEditor, navigateCallback);
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
                    // Open Stock Item Dialog
                    openStockItemDialog = function () {
                        stockDialog.show(function (stockItem) {
                            selectedSection().selectStock(stockItem);
                        }, stockCategory.paper, false);
                    },
                    // Get Paper Size by id
                    getPaperSizeById = function(id) {
                        return paperSizes.find(function(paperSize) {
                            return paperSize.id === id;
                        });
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        pager(new pagination.Pagination({ PageSize: 5 }, orders, getOrders()));
                        categoryPager(new pagination.Pagination({ PageSize: 5 }, categories, getInventoriesListItems));
                        costCentrePager(new pagination.Pagination({ PageSize: 5 }, costCentres, getCostCenters));

                        // Get Base Data
                        getBaseData();
                        
                        
                    },
                    // Subscribe Section Changes for Ptv Calculation
                    subscribeSectionChanges = function() {
                        // Subscribe change events for ptv calculation
                        selectedSection().isDoubleSided.subscribe(function (value) {
                            if (value !== selectedSection().isDoubleSided()) {
                                selectedSection().isDoubleSided(value);
                            }

                            getPtvCalculation();
                        });

                        // Work n Turn
                        selectedSection().isWorknTurn.subscribe(function (value) {
                            if (value !== selectedSection().isWorknTurn()) {
                                selectedSection().isWorknTurn(value);
                            }

                            getPtvCalculation();
                        });

                        // On Select Sheet Size
                        selectedSection().sectionSizeId.subscribe(function (value) {
                            if (value !== selectedSection().sectionSizeId()) {
                                selectedSection().sectionSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedSection().sectionSizeHeight(paperSize.height);
                                selectedSection().sectionSizeWidth(paperSize.width);

                                // Get Ptv Calculation
                                getPtvCalculation();
                            }
                        });

                        // Section Height
                        selectedSection().sectionSizeHeight.subscribe(function (value) {
                            if (value !== selectedSection().sectionSizeHeight()) {
                                selectedSection().sectionSizeHeight(value);
                            }

                            if (!selectedSection().isSectionSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // Section Width
                        selectedSection().sectionSizeWidth.subscribe(function (value) {
                            if (value !== selectedSection().sectionSizeWidth()) {
                                selectedSection().sectionSizeWidth(value);
                            }

                            if (!selectedSection().isSectionSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });
                        
                        // On Select Item Size
                        selectedSection().itemSizeId.subscribe(function (value) {
                            if (value !== selectedSection().itemSizeId()) {
                                selectedSection().itemSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedSection().itemSizeHeight(paperSize.height);
                                selectedSection().itemSizeWidth(paperSize.width);

                                // Get Ptv Calculation
                                getPtvCalculation();
                            }
                        });

                        // item Height
                        selectedSection().itemSizeHeight.subscribe(function (value) {
                            if (value !== selectedSection().itemSizeHeight()) {
                                selectedSection().itemSizeHeight(value);
                            }

                            if (!selectedSection().isItemSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // item Width
                        selectedSection().itemSizeWidth.subscribe(function (value) {
                            if (value !== selectedSection().itemSizeWidth()) {
                                selectedSection().itemSizeWidth(value);
                            }

                            if (!selectedSection().isItemSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // Include Gutter
                        selectedSection().includeGutter.subscribe(function (value) {
                            if (value !== selectedSection().includeGutter()) {
                                selectedSection().includeGutter(value);
                            }

                            getPtvCalculation();
                        });
                    },
                    // On Change Quantity 1 Markup
                    onChangeQty1MarkUpId = function (qty1Markup) {
                        if (selectedProduct().qty1MarkUpId1() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty1MarkUpId1();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge1Total()).toFixed(2);
                                selectedProduct().qty1NetTotal(parseFloat(markupValue) + parseFloat(baseCharge1Total()));
                            }

                        } else {
                            selectedProduct().qty1NetTotal(0);
                        }

                    },
                    // On Change Quantity 2 Markup
                    onChangeQty2MarkUpId = function (qtyMarkup) {

                        if (selectedProduct().qty2MarkUpId2() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty2MarkUpId2();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge2Total()).toFixed(2);
                                selectedProduct().qty2NetTotal(parseFloat(markupValue) + parseFloat(baseCharge2Total()));
                            }

                        } else {
                            selectedProduct().qty2NetTotal(0);
                        }
                    },
                    // On Change Quantity 3 Markup
                    onChangeQty3MarkUpId = function (qtyMarkup) {

                        if (selectedProduct().qty3MarkUpId3() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty3MarkUpId3();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge3Total()).toFixed(2);
                                selectedProduct().qty3NetTotal(parseFloat(markupValue) + parseFloat(baseCharge3Total()));
                            }

                        } else {
                            selectedProduct().qty3NetTotal(0);
                        }
                    },
                    // Change on Tax Rate
                    calculateTax = ko.computed(function () {
                        var qty1NetTotal = parseFloat((selectedProduct().qty1NetTotal() !== undefined && selectedProduct().qty1NetTotal() !== null) ? selectedProduct().qty1NetTotal() : 0).toFixed(2);
                        var qty2NetTotal = parseFloat((selectedProduct().qty2NetTotal() !== undefined && selectedProduct().qty2NetTotal() !== null) ? selectedProduct().qty2NetTotal() : 0).toFixed(2);
                        var qty3NetTotal = parseFloat((selectedProduct().qty3NetTotal() !== undefined && selectedProduct().qty3NetTotal() !== null) ? selectedProduct().qty3NetTotal() : 0).toFixed(2);

                        var tax = selectedProduct().tax1() !== undefined ? selectedProduct().tax1() : 0;
                        if (selectedProduct().tax1() !== undefined && selectedProduct().tax1() !== null && selectedProduct().tax1() !== "") {
                            var taxCalculate1 = ((tax / 100) * parseFloat(qty1NetTotal)).toFixed(2);
                            var total1 = (parseFloat(taxCalculate1) + parseFloat(qty1NetTotal)).toFixed(2);
                            selectedProduct().qty1GrossTotal(total1);
                            selectedProduct().qty1Tax1Value(taxCalculate1);

                            var taxCalculate2 = ((tax / 100) * (parseFloat(qty2NetTotal))).toFixed(2);
                            var total2 = (parseFloat(taxCalculate2) + parseFloat(qty2NetTotal)).toFixed(2);
                            selectedProduct().qty2GrossTotal(total2);
                            selectedProduct().qty2Tax1Value(taxCalculate2);

                            var taxCalculate3 = ((tax / 100) * parseFloat(qty3NetTotal)).toFixed(2);
                            var total3 = (parseFloat(taxCalculate3) + parseFloat(qty3NetTotal)).toFixed(2);
                            selectedProduct().qty3GrossTotal(total3);
                            selectedProduct().qty3Tax1Value(taxCalculate3);

                        } else {
                            selectedProduct().qty1GrossTotal(qty1NetTotal);
                            selectedProduct().qty2GrossTotal(qty2NetTotal);
                            selectedProduct().qty3GrossTotal(qty3NetTotal);
                            selectedProduct().qty1Tax1Value(0);
                            selectedProduct().qty2Tax1Value(0);
                            selectedProduct().qty3Tax1Value(0);
                        }
                    }),
                    deleteOrderButtonHandler = function () {
                        confirmation.messageText("Are you sure you want to delete order?");
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
                    openInkDialog = function () {
                        if (selectedSection() != undefined && selectedSection().plateInkId() != undefined) {
                            var count = 0;
                                _.each(availableInkPlateSides(), function(item) {
                                    if (item.id == selectedSection().plateInkId()) {
                                        updateSectionInkCoverageLists(item.plateInkSide1, item.plateInkSide2);
                                    }
                                });
                        }
                        view.showInksDialog();
                    },
                    updateSectionInkCoverageLists = function(side1Count, side2Count) {
                        if (getSide1Count() != side1Count) {
                            //If List is less then dropDown (Plate Ink)
                            if (getSide1Count() < side1Count) {
                                addNewFieldsInSectionInkCoverageList(side1Count - getSide1Count(), 1);
                            }
                            //If List is greater then dropDown (Plate Ink)
                            else if (getSide1Count() > side1Count) {
                                removeFieldsInSectionInkCoverageList(getSide1Count() - side1Count, 1);
                            }
                        }
                        if (getSide2Count() != side2Count) {
                            //If List is less then dropDown (Plate Ink)
                            if (getSide2Count() < side1Count) {
                                addNewFieldsInSectionInkCoverageList(side2Count - getSide2Count(), 2);
                            }
                            //If List is greater then dropDown (Plate Ink)
                            else if (getSide2Count() > side1Count) {
                                removeFieldsInSectionInkCoverageList(getSide2Count() - side2Count, 2);
                            }
                        }
                    },
                    getSide1Count = function () {
                        var count = 0;
                        _.each(selectedSection().sectionInkCoverageList(), function(item) {
                            if (item.side == 1) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    getSide2Count = function() {
                        var count = 0;
                        _.each(selectedSection().sectionInkCoverageList(), function (item) {
                            if (item.side == 2) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    addNewFieldsInSectionInkCoverageList = function(addNewCount, side) {
                        var counter = 0;
                        while (counter < addNewCount) {
                            var item = new model.SectionInkCoverage();
                            item.side = side;
                            selectedSection().sectionInkCoverageList.splice(0, 0, item);
                            counter ++;
                        }
                    },
                    removeFieldsInSectionInkCoverageList = function(removeItemCount, side) {
                        var counter = removeItemCount;
                        while (counter != 0) {
                            _.each(selectedSection().sectionInkCoverageList(), function (item) {
                                    if (item.side == side && counter != 0) {
                                        selectedSection().sectionInkCoverageList.remove(item);
                                        counter --;
                                    }
                            }); 
                            //selectedSection().sectionInkCoverageList.remove(selectedSection().sectionInkCoverageList()[0]);
                            //counter--;
                        }
                        //_.each(selectedSection().sectionInkCoverageList(), function (item) {
                          
                        //        if (item.side == side && counter != 0) {
                        //            selectedSection().sectionInkCoverageList.remove(item);
                        //            counter --;
                        //        }
                        //}); 
                    },

                // #endregion
                    // #region ServiceCalls
                    // Get Base Data
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
                                paymentMethods.removeAll();
                                if (data.PaymentMethods) {
                                    ko.utils.arrayPushAll(paymentMethods(), data.PaymentMethods);
                                    paymentMethods.valueHasMutated();
                                }
                                inks.removeAll();
                                if (data.Inks) {
                                    ko.utils.arrayPushAll(inks(), data.Inks);
                                    inks.valueHasMutated();
                                }
                                inkCoverageGroup.removeAll();
                                if (data.InkCoverageGroup) {
                                    ko.utils.arrayPushAll(inkCoverageGroup(), data.InkCoverageGroup);
                                    inkCoverageGroup.valueHasMutated();
                                }
                                markups.removeAll();
                                if (data.Markups) {
                                    _.each(data.Markups, function (item) {
                                        markups.push(item);
                                    });
                                }
                                categories.removeAll();
                                if (data.StockCategories) {
                                    _.each(data.StockCategories, function (item) {
                                        categories.push(item);
                                    });
                                }
                                nominalCodes.removeAll();
                                if (data.ChartOfAccounts) {
                                    _.each(data.ChartOfAccounts, function (item) {
                                        nominalCodes.push(item);
                                    });
                                }

                                // Paper Sizes
                                if (data.PaperSizes) {
                                    mapList(paperSizes, data.PaperSizes, model.PaperSize);
                                }

                                // Ink Plate Sides
                                if (data.InkPlateSides) {
                                    mapList(inkPlateSides, data.InkPlateSides, model.InkPlateSide);
                                }

                                currencySymbol(data.CurrencySymbol);
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
                    // Save Order
                    saveOrder = function (callback, navigateCallback) {
                        selectedOrder().statusId(view.orderstate());
                        var order = selectedOrder().convertToServerData();
                        _.each(selectedOrder().prePayments(), function (item) {
                            order.PrePayments.push(item.convertToServerData());
                        });
                        _.each(selectedOrder().deliverySchedules(), function (item) {
                            order.ShippingInformations.push(item.convertToServerData());
                        });

                        dataservice.saveOrder(order, {
                            success: function (data) {
                                if (!selectedOrder().id()) {
                                    // Update Id
                                    selectedOrder().id(data.OrderId);

                                    // Add to top of list
                                    orders.splice(0, 0, selectedOrder());
                                } else {
                                    // Get Order
                                    var orderUpdated = getOrderFromList(selectedOrder().id());
                                    if (orderUpdated) {
                                        order.orderCode(data.OrderCode);
                                        order.orderName(data.OrderName);
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
                        getOrders(currentScreen());
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
                                    pager().totalCount(data.TotalCount);
                                    mapOrders(data.Orders);
                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Failed to load orders" + response);
                            }
                        });
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
                                    selectedOrder(model.Estimate.Create(data));
                                    _.each(data.PrePayments, function (item) {
                                        selectedOrder().prePayments.push(model.PrePayment.Create(item));
                                    });
                                    view.setOrderState(selectedOrder().statusId());

                                    // Get Base Data For Company
                                    if (data.CompanyId) {
                                        getBaseForCompany(data.CompanyId, 0);
                                    }
                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }
                                isLoadingOrders(false);
                                view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                isLoadingOrders(false);
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
                                    }
                                    if (data.CompanyContacts) {
                                        mapList(companyContacts, data.CompanyContacts, model.CompanyContact);
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
                    // #endregion Service Calls
                    //#region Dialog Product Section
                    orderProductItems = ko.observableArray([]),
                    productQuantitiesList = ko.observableArray([]),
                    //#region Product From Retail Store
                    openProductFromStoreDialog = function () {
                        view.showProductFromRetailStoreModal();

                    },
                    onCreateNewProductFromRetailStore = function () {
                        getItemsByCompanyId();
                        openProductFromStoreDialog();
                    },
                    onAddCostCenter = function () {
                        getCostCenters();
                        view.showCostCentersDialog();
                    },
                    onAddInventoryItem = function () {
                        getInventoriesListItems();
                        view.showInventoryItemDialog();
                    },
                    closeCostCenterDialog = function () {
                        view.hideRCostCentersDialog();
                    },

                    getCostCenters = function () {
                        dataservice.getCostCenters({
                            CompanyId: selectedOrder().companyId(),
                            SearchString: costCentrefilterText(),
                            PageSize: costCentrePager().pageSize(),
                            PageNo: costCentrePager().currentPage(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    costCentrePager().totalCount(data.RowCount);
                                    costCentres.removeAll();
                                    _.each(data.CostCentres, function (item) {
                                        var costCentre = new model.costCentre.Create(item);
                                        costCentres.push(costCentre);
                                    });
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Load Cost Centres. Error: " + response);
                            }
                        });
                    },
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
                    //#region product From Retail Store

                    //Get Items By CompanyId
                    getItemsByCompanyId = function () {
                        dataservice.getItemsByCompanyId({
                            CompanyId: selectedOrder().companyId()
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    orderProductItems.removeAll();
                                    _.each(data.Items, function (item) {
                                        var itemToBePushed = new model.Item.Create(item);
                                        orderProductItems.push(itemToBePushed);
                                    });
                                    //Select First Item by Default if list is not empty
                                    if (orderProductItems().length > 0) {
                                        updateItemsDataOnItemSelection(orderProductItems()[0]);
                                    }
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Load Company Products . Error: " + response);
                            }
                        });
                    },

                    //Update Items Data On Item Selection
                    //Get Item Stock Options and Items Price Matrix against this item's id(itemId)
                    updateItemsDataOnItemSelection = function (item) {
                        dataservice.getItemsDetailsByItemId({
                            itemId: item.id()
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    item.itemStockOptions.removeAll();
                                    item.itemPriceMatrices.removeAll();
                                    productQuantitiesList.removeAll();
                                    _.each(data.ItemStockOptions, function (itemStockoption) {
                                        var itemToBePushed = new model.ItemStockOption.Create(itemStockoption);
                                        item.itemStockOptions.push(itemToBePushed);
                                    });
                                    _.each(data.ItemPriceMatrices, function (itemPriceMatrix) {
                                        var itemToBePushed = new model.ItemPriceMatrix.Create(itemPriceMatrix);
                                        item.itemPriceMatrices.push(itemToBePushed);
                                        if (item.isQtyRanged() == 2) {
                                            productQuantitiesList.push(itemToBePushed.quantity());
                                        }
                                    });

                                    selecteditem(item);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Load Company Products . Error: " + response);
                            }
                        });
                    },
                    onCloseProductFromRetailStore = function () {
                        view.hideProductFromRetailStoreModal();
                    },
                    //Selected Stock Option Name
                    selectedStockOptionName = ko.observable(),
                    //Filtered Item Price matrix List
                    filteredItemPriceMatrixList = ko.observableArray([]),
                    //Selected Stock Option Sequence Number
                    selectedStockOptionSequenceNumber = ko.observable(),
                    //SelectedStockOption
                    selectedStockOption = ko.observable(),
                    //On Product From Retail Store update Item price matrix table and Add on Table 
                    updateViewOnStockOptionChange = ko.computed(function () {
                        if (selecteditem() == undefined || selecteditem().itemStockOptions == undefined) {
                            return;
                        }
                        var count = 0;
                        _.each(selecteditem().itemStockOptions(), function (itemStockOption) {
                            count = count + 1;
                            if (itemStockOption.id() == selectedStockItem()) {
                                selectedStockOptionName(itemStockOption.label());
                                selectedStockOptionSequenceNumber(count);
                                selectedStockOption(itemStockOption);
                            }
                        });
                    }),

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
                                categoryPager().totalCount(data.TotalCount);
                                inventoryItems.removeAll();
                                _.each(data.StockItems, function (item) {
                                    var inventory = new model.Inventory.Create(item);
                                    inventoryItems.push(inventory);
                                });
                            },
                            error: function () {
                                isLoadingInventory(false);
                                toastr.error("Failed to load inventories.");
                            }
                        });
                    },
                    //#endregion
                    //#region Pre Payment
                    showOrderPrePaymentModal = function () {
                        selectedPrePayment(model.PrePayment());
                        view.showOrderPrePaymentModal();
                    },
                    hideOrderPrePaymentModal = function () {
                        view.hideOrderPrePaymentModal();
                    },
                    //Create Order Pre Payment
                    onCreateOrderPrePayment = function () {
                        showOrderPrePaymentModal();
                    },
                    // Close Order Pre Payment
                    onCancelOrderPrePayment = function () {
                        hideOrderPrePaymentModal();
                    },
                    // Edit Pre Payment
                    onEditPrePayment = function (prePayment) {
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
                        if (selectedDeliverySchedule() !== undefined && !selectedDeliverySchedule().isValid()) {
                            selectedDeliverySchedule().errors.showAllMessages();
                            return;
                        }
                        if (selectedDeliverySchedule() !== undefined && selectedDeliverySchedule().isValid()) {
                            setDeliveryScheduleFields();
                        }
                        var deliverySchedule = model.ShippingInformation();
                        if (selectedOrder().items().length > 0) {
                            setQuantityOfNewDeliverySchedule(deliverySchedule);
                        }

                        selectedOrder().deliverySchedules.splice(0, 0, deliverySchedule);
                        selectedDeliverySchedule(selectedOrder().deliverySchedules()[0]);
                    },
                    // Set  Quantity Of new Added Delivery Schedule
                    setQuantityOfNewDeliverySchedule = function (deliverySchedule) {
                        var quantity = selectedOrder().items()[0];
                        if (quantity.qty1() !== undefined) {
                            var qt1 = parseInt(quantity.qty1());
                            var calculatedQuantity = 0;
                            if (quantity) {
                                _.each(selectedOrder().deliverySchedules(), function (item) {
                                    if (item.itemId() === selectedDeliverySchedule().itemId()) {
                                        calculatedQuantity = calculatedQuantity + parseInt(item.quantity());
                                    }
                                });
                                deliverySchedule.quantity(qt1 - calculatedQuantity);
                            }
                        } else {
                            deliverySchedule.quantity(0);
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
                    calculateDeliveryShedulePrice = ko.computed(function () {
                        if (selectedDeliverySchedule() !== undefined && selectedDeliverySchedule().itemId() && selectedDeliverySchedule().quantity() !== undefined && selectedDeliverySchedule().quantity() !== "") {
                            var selectedItem = _.find(selectedOrder().items(), function (item) {
                                return item.id() === selectedDeliverySchedule().itemId();
                            });

                            checkForQuantity(selectedItem);
                            // calculate Price Of Delievry Selected Schedule 
                            if (selectedItem && selectedDeliverySchedule().quantity() !== undefined && selectedItem.qty1NetTotal() !== undefined && selectedItem.qty1() !== undefined) {
                                var perUnitPrice = parseInt(selectedDeliverySchedule().quantity()) / parseInt(selectedItem.qty1());
                                var netPrice = parseFloat(selectedItem.qty1NetTotal()).toFixed(2);
                                selectedDeliverySchedule().price(perUnitPrice * netPrice).toFixed(2);
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
                    side1Image = ko.observable(),
                    side2Image = ko.observable(),
                    itemPlan = ko.observable(),
                    showSide1Image = ko.observable(true),
                    getPtvPlan = function () {
                        isLoadingOrders(true);
                        dataservice.getPTV({
                            orientation: 1,
                            reversRows: 0,
                            revrseCols: 0,
                            isDoubleSided: false,
                            isWorknTurn: false,
                            isWorknTumble: false,
                            applyPress: false,
                            itemHeight: 300,
                            itemWidth: 400,
                            printHeight: 300,
                            printWidth: 400,
                            grip: 1,
                            gripDepth: 0,
                            headDepth: 0,
                            printGutter: 0,
                            horizentalGutter: 0,
                            verticalGutter: 0
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    itemPlan(undefined);
                                    side1Image(undefined);
                                    side2Image(undefined);
                                    side1Image(data.Side1ImageSource);
                                    if (data.Side2ImageSource != "") {
                                        side2Image(data.Side2ImageSource);
                                    }

                                    itemPlan(data.Side1ImageSource);
                                    view.showSheetPlanImageDialog();
                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Error: Failed to Load Sheet Plan. Error: " + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    //Get PTV Calculation
                    getPtvCalculation = function () {
                        if (isPtvCalculationInProgress()) {
                            return;
                        }

                        isPtvCalculationInProgress(true);
                        dataservice.getPTVCalculation({
                            orientation: 1,
                            reversRows: 0,
                            revrseCols: 0,
                            isDoubleSided: selectedSection().isDoubleSided(),
                            isWorknTurn: selectedSection().isWorknTurn(),
                            isWorknTumble: false,
                            applyPress: false,
                            itemHeight: selectedSection().itemSizeHeight(),
                            itemWidth: selectedSection().itemSizeWidth(),
                            printHeight: selectedSection().sectionSizeHeight(),
                            printWidth: selectedSection().sectionSizeWidth(),
                            grip: 1,
                            gripDepth: 0,
                            headDepth: 0,
                            printGutter: selectedSection().includeGutter() ? 1 : 0,
                            horizentalGutter: 0,
                            verticalGutter: 0
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedSection().printViewLayoutLandscape(data.LandscapePTV || 0);
                                    selectedSection().printViewLayoutPortrait(data.PortraitPTV || 0);
                                }
                                isPtvCalculationInProgress(false);
                            },
                            error: function (response) {
                                isPtvCalculationInProgress(false);
                                toastr.error("Error: Failed to Calculate Number up value. Error: " + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    //Side 1 Button Click
                    side1ButtonClick = function () {
                        showSide1Image(true);
                    },
                    //Side 2 Button Click
                    side2ButtonClick = function () {
                        showSide1Image(false);
                    },
                    getBestPress = function () {
                        isLoadingOrders(true);
                        dataservice.getBestPress(selectedSection().convertToServerData(), {
                            success: function (data) {
                                if (data != null) {


                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Error: Failed to Load Best Press List." + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    getSectionSystemCostCenters = function () {
                        isLoadingOrders(true);
                        dataservice.getUpdatedSystemCostCenters({
                            CurrentSection: selectedSection().convertToServerData(),
                            PressId: selectedSection().pressId,
                            AllSectionInks: sectionInkCoverage()
                        }, {
                            success: function (data) {
                                if (data != null) {


                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Error: Failed to Load System Cost Centers." + response);
                            }
                        });
                    },
                    // Template Chooser For Delivery Schedule
                     templateToUseDeliverySchedule = function (deliverySchedule) {
                         return (deliverySchedule === selectedDeliverySchedule() ? 'ediDeliverScheduleTemplate' : 'itemDeliverScheduleTemplate');
                     };
                //#endregion
                //#endregion

                return {
                    // #region Observables
                    selectedOrder: selectedOrder,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoadingOrders: isLoadingOrders,
                    orders: orders,
                    isOrderDetailsVisible: isOrderDetailsVisible,
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
                    addSection: addSection,
                    editSection: editSection,
                    closeSectionDetail: closeSectionDetail,
                    deleteProduct: deleteProduct,
                    selectJobDescription: selectJobDescription,
                    openPhraseLibrary: openPhraseLibrary,
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
                    side1Image: side1Image,
                    side2Image: side2Image,
                    showSide1Image: showSide1Image,
                    inks: inks,
                    inkCoverageGroup: inkCoverageGroup,
                    //#endregion Utility Methods
                    //#region Dialog Product Section
                    orderProductItems: orderProductItems,
                    getOrders: getOrders,
                    getOrdersOfCurrentScreen: getOrdersOfCurrentScreen,
                    //#region Product From Retail Store
                    updateItemsDataOnItemSelection: updateItemsDataOnItemSelection,
                    onCreateNewProductFromRetailStore: onCreateNewProductFromRetailStore,
                    onCloseProductFromRetailStore: onCloseProductFromRetailStore,
                    getItemsByCompanyId: getItemsByCompanyId,
                    onAddCostCenter: onAddCostCenter,
                    onCloseCostCenterDialog: closeCostCenterDialog,
                    costCentres: costCentres,
                    getCostCenters: getCostCenters,
                    costCentrefilterText: costCentrefilterText,
                    resetCostCentrefilter: resetCostCentrefilter,
                    costCenterClickListner: costCenterClickLIstner,
                    selectedCostCentre: selectedCostCentre,
                    hideCostCentreQuantityDialog: hideCostCentreQuantityDialog,
                    hideCostCentreDialog: hideCostCentreDialog,
                    selectedSection: selectedSection,
                    baseCharge1Total: baseCharge1Total,
                    baseCharge2Total: baseCharge2Total,
                    baseCharge3Total: baseCharge3Total,
                    markups: markups,
                    selectedMarkup1: selectedMarkup1,
                    selectedMarkup2: selectedMarkup2,
                    selectedMarkup3: selectedMarkup3,
                    categories: categories,
                    selectedCategoryId: selectedCategoryId,
                    categoryPager: categoryPager,
                    inventorySearchFilter: inventorySearchFilter,
                    getInventoriesListItems: getInventoriesListItems,
                    onChangeQty1MarkUpId: onChangeQty1MarkUpId,
                    onChangeQty2MarkUpId: onChangeQty2MarkUpId,
                    onChangeQty3MarkUpId: onChangeQty3MarkUpId,
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
                    //#endregion
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    side1ButtonClick: side1ButtonClick,
                    side2ButtonClick: side2ButtonClick,
                    getPtvCalculation: getPtvCalculation,
                    openInkDialog: openInkDialog,
                    //#endregion
                    //#region Delivery Schedule
                    selectDeliverySchedule: selectDeliverySchedule,
                    addDeliverySchedule: addDeliverySchedule,
                    selectedDeliverySchedule: selectedDeliverySchedule,
                    templateToUseDeliverySchedule: templateToUseDeliverySchedule,
                    onRaised: onRaised,
                    getPtvPlan: getPtvPlan,
                    getBestPress: getBestPress,
                    //#endregion
                    //#region Section Detail
                    availableInkPlateSides: availableInkPlateSides,
                    paperSizes: paperSizes,
                    openStockItemDialog: openStockItemDialog,
                    getSectionSystemCostCenters: getSectionSystemCostCenters
                    //#endregion
                };
            })()
        };
        return ist.order.viewModel;
    });
