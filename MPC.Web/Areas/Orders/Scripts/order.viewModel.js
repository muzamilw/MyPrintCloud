/*
    Module with the view model for the Order.
*/
define("order/order.viewModel",
    ["jquery", "amplify", "ko", "order/order.dataservice", "order/order.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/phraseLibrary.viewModel", "common/stockItem.viewModel", "common/reportManager.viewModel", "common/addCostCenter.viewModel", "common/addProduct.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, phraseLibrary, stockDialog, reportManager, addCostCenterVM, addProductVm) {
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
                    // selected Company
                    selectedCompany = ko.observable(),
                    // Errors List
                    errorList = ko.observableArray([]),
                    // Best PressL ist
                    bestPressList = ko.observableArray([]),
                    // User Cost Center List For Run Wizard
                    userCostCenters = ko.observableArray([]),
                    // Selected Cost Center List For Run Wizard
                    selectedCostCenters = ko.observableArray([]),
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
                    //selected Best Press From Wizard
                    selectedBestPressFromWizard = ko.observable(),
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
                    //On Order Status change to progress to job that will open wizard
                    selectedItemForProgressToJobWizard = ko.observable(model.Item()),
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
                    //Inventory Stock Item To Create
                    inventoryStockItemToCreate = ko.observable(),
                    // Stock Item To Create For Stock Cost Center
                    stockItemToCreate = ko.observable(),
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
                    //Is Cost Center dialog open for shipping
                    isCostCenterDialogForShipping = ko.observable(false),
                    //Is Inventory Dialog is opening from Order Dialog's add Product From Inventory
                    isAddProductFromInventory = ko.observable(false),
                    //Is Inventory Dialog is opening for Section Cost Center
                    isAddProductForSectionCostCenter = ko.observable(false),
                    orderHasChanges = ko.computed(function () {
                        var hasChanges = false, productChanges = false, sectionHasChanges = false;
                        if (selectedOrder()) {
                            hasChanges = selectedOrder().hasChanges();
                        }

                        if (selectedProduct()) {
                            productChanges = selectedProduct().hasChanges();
                        }

                        if (selectedSection()) {
                            sectionHasChanges = selectedSection().hasChanges();
                        }

                        return hasChanges || productChanges || sectionHasChanges;
                    }),
                    // #endregion

                    // #region Utility Functions
                    // Create New Order
                    createOrder = function () {
                        selectedOrder(model.Estimate.Create({}));
                        view.setOrderState(4); // Pending Order
                        selectedOrder().statusId(4);
                        openOrderEditor();
                    },
                    // Edit Order
                    editOrder = function (data) {
                        getOrderById(data.id(), openOrderEditor);
                    },
                    // Open Editor
                    openOrderEditor = function () {
                        isOrderDetailsVisible(true);
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
                            selectedOrder().estimateTotal(total);
                        }

                        return total;
                    }),
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
                                var orderIdFromDashboard = $('#OrderId').val();
                                if (orderIdFromDashboard != 0) {
                                    getOrders();
                                }
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
                        companySelector.show(onSelectCompany, [0, 1, 3], true);
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
                        selectedCompany(company);

                        // Get Company Address and Contacts
                        getBaseForCompany(company.id, (company.storeId === null || company.storeId === undefined) ? 0 : company.storeId);
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

                        var section = selectedProduct() != undefined ? selectedProduct().itemSections()[0] : undefined;
                        editSection(section);
                        openItemDetail();
                    },
                    // Open Item Detail
                    openItemDetail = function () {
                        isItemDetailVisible(true);
                        view.initializeLabelPopovers();
                    },
                    // Calculates Section Charges 
                    calculateSectionChargeTotal = ko.computed(function () {
                        baseCharge1Total(0);
                        baseCharge2Total(0);
                        baseCharge3Total(0);

                        //if (selectedProduct().itemSections().length > 0) {

                        //    _.each(selectedProduct().itemSections(), function (item) {
                        //        if (item.qty1Profit() === undefined || item.qty1Profit() === "") {
                        //            item.qty1Profit(0);
                        //        }
                        //        if (item.qty2Profit() === undefined || item.qty2Profit() === "") {
                        //            item.qty2Profit(0);
                        //        }
                        //        if (item.qty3Profit() === undefined || item.qty3Profit() === "") {
                        //            item.qty3Profit(0);
                        //        }
                        //        var basCharge1 = parseFloat(((item.baseCharge1() !== undefined && item.baseCharge1() !== "") ? item.baseCharge1() : 0));
                        //        baseCharge1Total(parseFloat(baseCharge1Total()) + basCharge1 + parseFloat(item.qty1Profit()));
                        //        baseCharge2Total(parseFloat(baseCharge2Total()) + parseFloat(((item.baseCharge2() !== undefined && item.baseCharge2() !== "") ? item.baseCharge2() : 0)) + parseFloat(item.qty2Profit()));
                        //        baseCharge3Total(parseFloat(baseCharge3Total()) + parseFloat(((item.baseCharge3() !== undefined && item.baseCharge3() !== "") ? item.baseCharge3() : 0)) + parseFloat(item.qty3Profit()));
                        //    });
                        //}
                        if (selectedSection() !== undefined && selectedSection().sectionCostCentres().length > 0) {
                            _.each(selectedSection().sectionCostCentres(), function (item) {
                                if (item.qty1NetTotal() === undefined || item.qty1NetTotal() === "" || item.qty1NetTotal() === null || isNaN(item.qty1NetTotal())) {
                                    item.qty1NetTotal(0);
                                }
                                if (item.qty2NetTotal() === undefined || item.qty2NetTotal() === "" || item.qty2NetTotal() === null || isNaN(item.qty2NetTotal())) {
                                    item.qty2NetTotal(0);
                                }
                                if (item.qty3NetTotal() === undefined || item.qty3NetTotal() === "" || item.qty3NetTotal() === null || isNaN(item.qty3NetTotal())) {
                                    item.qty3NetTotal(0);
                                }
                                baseCharge1Total(parseFloat(baseCharge1Total()) + parseFloat(item.qty1NetTotal()));
                                baseCharge2Total(parseFloat(baseCharge2Total()) + parseFloat(item.qty2NetTotal()));
                                baseCharge3Total(parseFloat(baseCharge3Total()) + parseFloat(item.qty3NetTotal()));
                            });
                        }

                        if (selectedSection() !== undefined && selectedSection().similarSections != undefined && selectedSection().similarSections() !== undefined) {
                            if (parseFloat(selectedSection().similarSections()) === 0) {
                                selectedSection().similarSections(1);
                            }
                            calculateBaseChargeBasedOnSimilarSectionsValue();
                        }


                    }),
                    calculateBaseChargeBasedOnSimilarSectionsValue = function () {
                        var newBaseCharge1Totaol = (baseCharge1Total() !== undefined ? baseCharge1Total() : 0) * parseFloat(selectedSection().similarSections());
                        baseCharge1Total(newBaseCharge1Totaol);
                        baseCharge2Total((baseCharge2Total() !== undefined ? baseCharge2Total() : 0) * parseFloat(selectedSection().similarSections()));
                        baseCharge3Total((baseCharge3Total() !== undefined ? baseCharge3Total() : 0) * parseFloat(selectedSection().similarSections()));
                        q1NetTotal();
                        q2NetTotal();
                        q3NetTotal();
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
                        //    isSectionDetailVisible(true);
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
                    // Open Stock Item Dialog For Adding product
                    openStockItemDialogForAddingProduct = function () {
                        isAddProductFromInventory(true);
                        isAddProductForSectionCostCenter(false);
                        stockDialog.show(function (stockItem) {
                            createNewInventoryProduct(stockItem);
                        }, stockCategory.paper, false);
                    },
                    // Open Stock Item Dialog For Adding Stock
                    openStockItemDialogForAddingStock = function () {
                        //view.showCostCentersQuantityDialog();
                        isAddProductFromInventory(false);
                        isAddProductForSectionCostCenter(true);
                        stockDialog.show(function (stockItem) {
                            onSaveStockItem(stockItem);
                        }, stockCategory.paper, false);
                    },

                    // Get Paper Size by id
                    getPaperSizeById = function (id) {
                        return paperSizes.find(function (paperSize) {
                            return paperSize.id === id;
                        });
                    },
                    // Subscribe Dropdown Filter Changes to search on selection change
                    subscribeDropdownFilterChange = function () {
                        orderTypeFilter.subscribe(function () {
                            getOrdersOfCurrentScreen(currentScreen());
                        });

                        selectedFilterFlag.subscribe(function () {
                            getOrdersOfCurrentScreen(currentScreen());
                        });
                    },
                    // On Order Status Change
                    onOrderStatusChange = function (status) {

                        status = status === 4 ? status + 5 : status + 4;
                        if (selectedOrder().statusId() < status) {
                            statusNavigationForward(status);
                        } else {
                            statusNavigationBackward(status);
                        }
                    },
                    statusNavigationBackward = function (status) {
                        // Only move 1 or 2 step backward at a time, if user try to move more than 1 or 2 step then system set 1 step by default
                        if (selectedOrder().statusId() === 9) {
                            if ((selectedOrder().statusId() - 2) !== status && (selectedOrder().statusId() - 3) !== status) {
                                status = selectedOrder().statusId() - 2;
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
                            confirmation.messageText("Are you sure you want to revert status of this order? All posted delivery notes will be cancelled.");
                            confirmation.afterProceed(function () {
                                selectedOrder().statusId(status);
                                onStatusChangeDeliveryNotesCancelled();

                            });
                            confirmation.afterCancel(function () {
                                view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                            });
                            confirmation.show();
                            return;
                        }
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
                            confirmation.messageText("Are you sure you want to progress all the un progressed items to jobs?");
                            confirmation.afterProceed(function () {
                                selectedOrder().statusId(status);
                                view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                                changeAllItemProgressToJob();
                            });
                            confirmation.afterCancel(function () {
                                view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                            })
                            confirmation.show();
                            return;
                        }
                    },
                    progressToJobItemCounter = 0,
                    // Change All items status Progress to job
                    changeAllItemProgressToJob = function () {
                        if (selectedOrder().items().length > 0) {
                            selectedItemForProgressToJobWizard(selectedOrder().items()[progressToJobItemCounter]);
                            selectedItemForProgressToJobWizard().jobStatusId(jobStatuses()[0].StatusId);
                            progressToJobItemCounter = progressToJobItemCounter + 1;
                            view.showOrderStatusProgressToJobDialog();
                        }
                    },
                    clickOnJobToProgressWizard = function () {
                        if (selectedOrder().items().length === progressToJobItemCounter) {
                            view.hideOrderStatusProgressToJobDialog();
                            progressToJobItemCounter = 0;
                        } else {
                            changeAllItemProgressToJob();
                        }

                    },
                    // Show Confirmation on forward Navigation of Order Status Change
                    showConfirmationMessageForForwardNavigationOnStatusChange = function (status) {
                        confirmation.messageText("Are you sure you want to change status of this order?");
                        confirmation.afterProceed(function () {
                            selectedOrder().statusId(status);
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        });
                        confirmation.afterCancel(function () {
                            view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());
                        });
                        confirmation.show();
                        return;
                    },
                    // Show Confirmation on backward Navigation of Order Status Change
                    showConfirmationMessageForBackwardNavigationOnStatusChange = function (status) {
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
                            item.jobStatusId(jobStatuses()[0].StatusId);
                            counter = counter + 1;
                        });

                        if (selectedOrder().items().length === counter) {
                            onStatusChangeDeliveryNotesCancelled();
                        }

                    },


                    // Subscribe Section Changes for Ptv Calculation
                    subscribeSectionChanges = function () {
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

                        q1NetTotal();
                    },
                    q1NetTotal = function () {
                        if (selectedProduct().qty1MarkUpId1() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty1MarkUpId1();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge1Total()).toFixed(2);
                                selectedProduct().qty1NetTotal(parseFloat(markupValue) + parseFloat(baseCharge1Total()));
                            }

                        } else {
                            selectedProduct().qty1NetTotal(baseCharge1Total());
                        }
                    },
                    // On Change Quantity 2 Markup
                    onChangeQty2MarkUpId = function (qtyMarkup) {
                        q2NetTotal();
                    },
                    q2NetTotal = function () {
                        if (selectedProduct().qty2MarkUpId2() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty2MarkUpId2();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge2Total()).toFixed(2);
                                selectedProduct().qty2NetTotal(parseFloat(markupValue) + parseFloat(baseCharge2Total()));
                            }

                        } else {
                            selectedProduct().qty2NetTotal(baseCharge2Total());
                        }
                    },
                    // On Change Quantity 3 Markup
                    onChangeQty3MarkUpId = function (qtyMarkup) {
                        q3NetTotal();
                    },
                    q3NetTotal = function () {
                        if (selectedProduct().qty3MarkUpId3() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty3MarkUpId3();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge3Total()).toFixed(2);
                                selectedProduct().qty3NetTotal(parseFloat(markupValue) + parseFloat(baseCharge3Total()));
                            }

                        } else {
                            selectedProduct().qty3NetTotal(baseCharge3Total());
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
                    availableInkPalteChange = function () {
                        setAvailableInkPlateChange();
                    },
                    setAvailableInkPlateChange = function () {
                        if (selectedSection() != undefined && selectedSection().plateInkId() != undefined) {
                            var count = 0;
                            _.each(availableInkPlateSides(), function (item) {
                                if (item.id == selectedSection().plateInkId()) {
                                    updateSectionInkCoverageLists(item.plateInkSide1, item.plateInkSide2);
                                    selectedSection().side1Inks(item.plateInkSide1);
                                    selectedSection().side2Inks(item.plateInkSide2);
                                }
                            });
                        }
                    },
                    openInkDialog = function () {
                        //if (selectedSection() != undefined && selectedSection().plateInkId() != undefined) {
                        //    var count = 0;
                        //    _.each(availableInkPlateSides(), function (item) {
                        //        if (item.id == selectedSection().plateInkId()) {
                        //            updateSectionInkCoverageLists(item.plateInkSide1, item.plateInkSide2);
                        //            selectedSection().side1Inks(item.plateInkSide1);
                        //            selectedSection().side2Inks(item.plateInkSide2);
                        //        }
                        //    });
                        //}
                        view.showInksDialog();
                    },
                    updateSectionInkCoverageLists = function (side1Count, side2Count) {
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
                            if (getSide2Count() < side2Count) {
                                addNewFieldsInSectionInkCoverageList(side2Count - getSide2Count(), 2);
                            }
                                //If List is greater then dropDown (Plate Ink)
                            else if (getSide2Count() > side2Count) {
                                removeFieldsInSectionInkCoverageList(getSide2Count() - side2Count, 2);
                            }
                        }
                    },
                    getSide1Count = function () {
                        var count = 0;
                        _.each(selectedSection().sectionInkCoverageList(), function (item) {
                            if (item.side() == 1) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    getSide2Count = function () {
                        var count = 0;
                        _.each(selectedSection().sectionInkCoverageList(), function (item) {
                            if (item.side() == 2) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    addNewFieldsInSectionInkCoverageList = function (addNewCount, side) {
                        var counter = 0;
                        while (counter < addNewCount) {
                            var item = new model.SectionInkCoverage();
                            item.side(side);
                            item.sectionId(selectedSection().id());
                            selectedSection().sectionInkCoverageList.splice(0, 0, item);
                            counter++;
                        }
                    },
                    removeFieldsInSectionInkCoverageList = function (removeItemCount, side) {
                        var counter = removeItemCount;
                        while (counter != 0) {
                            _.each(selectedSection().sectionInkCoverageList(), function (item) {
                                if (item.side() == side && counter != 0) {
                                    selectedSection().sectionInkCoverageList.remove(item);
                                    counter--;
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
                    selectedSectionCostCenter = ko.observable(),
                    selectedQty = ko.observable(),
                    //Section Cost Center Dialog
                    openSectionCostCenterDialog = function (costCenter, qty) {
                        selectedSectionCostCenter(costCenter);
                        selectedQty(qty);
                        view.showSectionCostCenterDialogModel();
                    },
                    updateSectionCostCenterDialog = ko.computed(function () {

                        if (selectedSectionCostCenter() != undefined && selectedQty() != undefined
                            //&& selectedSectionCostCenter().qty1MarkUpId() != undefined
                            // && selectedSectionCostCenter().qty2MarkUpId() != undefined && selectedSectionCostCenter().qty3MarkUpId() != undefined
                        ) {
                            var markupValue = 0;
                            if (selectedQty() == 1) {
                                _.each(markups(), function (markup) {
                                    if (markup.MarkUpId == selectedSectionCostCenter().qty1MarkUpId()) {
                                        markupValue = markup.MarkUpRate;
                                        selectedSectionCostCenter().qty1MarkUpValue(markupValue);
                                        var total = parseFloat(selectedSectionCostCenter().qty1Charge()) + (selectedSectionCostCenter().qty1Charge() * (markupValue / 100));
                                        selectedSectionCostCenter().qty1NetTotal(total);
                                    }
                                });
                            }
                            if (selectedQty() == 2) {
                                _.each(markups(), function (markup) {
                                    if (markup.MarkUpId == selectedSectionCostCenter().qty2MarkUpId()) {
                                        markupValue = markup.MarkUpRate;
                                        selectedSectionCostCenter().qty2MarkUpValue(markupValue);
                                        var total = parseFloat(selectedSectionCostCenter().qty2Charge()) + (selectedSectionCostCenter().qty2Charge() * (markupValue / 100));
                                        selectedSectionCostCenter().qty2NetTotal(total);
                                    }
                                });
                            }
                            if (selectedQty() == 3) {
                                _.each(markups(), function (markup) {
                                    if (markup.MarkUpId == selectedSectionCostCenter().qty3MarkUpId()) {
                                        markupValue = markup.MarkUpRate;
                                        selectedSectionCostCenter().qty3MarkUpValue(markupValue);
                                        var total = parseFloat(selectedSectionCostCenter().qty3Charge()) + (selectedSectionCostCenter().qty3Charge() * (markupValue / 100));
                                        selectedSectionCostCenter().qty3NetTotal(total);
                                    }
                                });
                            }
                        }
                    }),
                    //Opens Cost Center dialog for Shipping
                    onShippingChargesClick = function () {
                        if (selectedOrder().companyId() === undefined) {
                            toastr.error("Please select customer.");
                        } else {
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
                                costCentresBaseData.removeAll();
                                if (data.CostCenters) {
                                    ko.utils.arrayPushAll(costCentresBaseData(), data.CostCenters);
                                    costCentresBaseData.valueHasMutated();
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
                        if (isNaN(view.orderstate()) || view.orderstate() === 0) {
                            selectedOrder().statusId(4); // Pending orders
                        }
                        var order = selectedOrder().convertToServerData();
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
                        dataservice.saveOrder(order, {
                            success: function (data) {
                                var orderFlag = _.find(sectionFlags(), function (item) {
                                    return item.id === selectedOrder().sectionFlagId();
                                });

                                if (!selectedOrder().id()) {
                                    // Update Id
                                    selectedOrder().id(data.EstimateId);
                                    selectedOrder().orderCode(data.OrderCode);
                                    var total1 = (parseFloat((data.EstimateTotal === undefined || data.EstimateTotal === null) ? 0 : data.EstimateTotal)).toFixed(2);
                                    selectedOrder().estimateTotal(total1);
                                    selectedOrder().creationDate(data.CreationDate !== null ? moment(data.CreationDate).toDate() : undefined);
                                    selectedOrder().numberOfItems(data.ItemsCount || 0);
                                    if (orderFlag) {
                                        selectedOrder().flagColor(orderFlag.color);
                                    }
                                    // Add to top of list
                                    orders.splice(0, 0, selectedOrder());
                                } else {
                                    // Get Order
                                    var orderUpdated = getOrderFromList(selectedOrder().id());
                                    if (orderUpdated) {
                                        orderUpdated.code(data.OrderCode);
                                        orderUpdated.creationDate(data.CreationDate !== null ? moment(data.CreationDate).toDate() : undefined);
                                        var total = (parseFloat((data.EstimateTotal === undefined || data.EstimateTotal === null) ? 0 : data.EstimateTotal)).toFixed(2);
                                        orderUpdated.estimateTotal(total);
                                        orderUpdated.name(data.OrderName);
                                        orderUpdated.numberOfItems(data.ItemsCount || 0);
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
                    itemAttachmentFileLoadedCallback = function (file, data) {
                        //Flag check, whether file is already exist in media libray
                        var flag = true;

                        _.each(selectedProduct().itemAttachments(), function (item) {
                            if (item.fileSourcePath() === data && item.fileName() === file.name) {
                                flag = false;
                            }
                        });

                        if (flag) {
                            var attachment = model.ItemAttachment.Create({});
                            attachment.id(undefined);
                            attachment.fileSourcePath(data);
                            attachment.fileName(file.name);
                            attachment.companyId(selectedOrder().companyId());
                            attachment.itemId(selectedProduct().id());
                            selectedProduct().itemAttachments.push(attachment);

                        }
                    },
                    //get Orders Of Current Screen
                    getOrdersOfCurrentScreen = function () {
                        getOrders(currentScreen());
                    },
                    //Get Order Tab Changed Event
                    getOrdersOnTabChange = function (currentTab) {
                        pager().reset();
                        if (isEstimateScreen()) {
                            getEstimates(currentTab);
                        } else {
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
                                    view.setOrderState(selectedOrder().statusId(), selectedOrder().isFromEstimate());

                                    // Get Base Data For Company
                                    if (data.CompanyId) {
                                        getBaseForCompany(data.CompanyId, 0);
                                    }
                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
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
                        if (selectedOrder().companyId() === undefined) {
                            toastr.error("Please select customer.");
                        } else {

                            var companyId = 0;
                            if (selectedCompany() !== undefined && selectedCompany().isCustomer !== undefined && selectedCompany().isCustomer !== 3 && selectedCompany().storeId !== null) {
                                companyId = selectedCompany().storeId;
                            } else {
                                companyId = selectedOrder().companyId();
                            }
                            addProductVm.show(addItemFromRetailStore, companyId, costCentresBaseData(), currencySymbol(), selectedOrder().id());
                        }
                    },
                    addItemFromRetailStore = function (newItem) {
                        selectedProduct(newItem);
                        selectedOrder().items.splice(0, 0, newItem);
                    },
                    onAddCostCenter = function () {
                        getCostCenters();
                        view.showCostCentersDialog();
                    },
                    onAddCostCenterForProduct = function () {
                        getCostCentersForProduct();
                        view.showCostCentersDialog();
                    },
                    onAddInventoryItem = function () {

                        isAddProductFromInventory(true);
                        openStockItemDialog();
                    },
                    closeCostCenterDialog = function () {
                        view.hideRCostCentersDialog();
                    },
                    getCostCenters = function () {
                        var companyId = 0;
                        if (selectedCompany() !== undefined && selectedCompany().isCustomer !== undefined && selectedCompany().isCustomer !== 3 && selectedCompany().storeId !== null) {
                            companyId = selectedCompany().storeId;
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
                                        var costCentre = new model.costCentre.Create(item);
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
                        addCostCenterVM.show(createNewCostCenterProduct, selectedOrder().companyId());
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
                        //Product From Cost Center
                    createNewCostCenterProduct = function (costCenter) {
                        selectedCostCentre(costCenter);
                        var item = model.Item.Create({ EstimateId: selectedOrder().id() });
                        selectedProduct(item);
                        item.productName(selectedCostCentre().name());
                        item.qty1(selectedCostCentre().quantity1());
                        item.qty1NetTotal(selectedCostCentre().setupCost());

                        var itemSection = model.ItemSection.Create({});

                        var sectionCostCenter = model.SectionCostCentre.Create({});
                        sectionCostCenter.qty1(selectedCostCentre().quantity1());
                        sectionCostCenter.qty2(selectedCostCentre().quantity2());
                        sectionCostCenter.qty3(selectedCostCentre().quantity3());
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.costCentreId(selectedCostCentre().id());
                        sectionCostCenter.costCentreName(selectedCostCentre().name());
                        sectionCostCenter.name(selectedCostCentre().name());

                        //sectionCostCenter.qty1NetTotal(selectedCostCentre().setupCost());
                        sectionCostCenter.qty1Charge(selectedCostCentre().setupCost());

                        selectedSectionCostCenter(sectionCostCenter);
                        selectedQty(1);


                        itemSection.sectionCostCentres.push(sectionCostCenter);
                        item.itemSections.push(itemSection);

                        if (isCostCenterDialogForShipping()) {
                            item.itemType(2); // Delivery Item
                        }

                        selectedOrder().items.splice(0, 0, item);

                        selectedSection(itemSection);

                    },
                    createNewInventoryProduct = function (stockItem) {
                        var costCenter = model.costCentre.Create({});
                        selectedCostCentre(costCenter);

                        view.showCostCentersQuantityDialog();

                        inventoryStockItemToCreate(stockItem);
                    },
                    //On Save Stock Item From Item Edit Dialog
                    onSaveStockItem = function (stockItem) {
                        var costCenter = model.costCentre.Create({});
                        selectedCostCentre(costCenter);

                        stockItemToCreate(stockItem);

                        view.showCostCentersQuantityDialog();
                        isAddProductFromInventory(false);

                    },
                    onSaveStockitemForSectionCostCenter = function () {
                        var containsStockItem = false;
                        _.each(selectedSection().sectionCostCentres(), function (costCenter) {
                            if (costCenter.costCentreType() == '139') {
                                containsStockItem = true;
                                selectedSectionCostCenter(costCenter);
                            }
                        });

                        var sectionCostCenter = model.SectionCostCentre.Create({ ItemSectionId: selectedSection().id() });
                        if (!containsStockItem) {
                            selectedSectionCostCenter(sectionCostCenter);
                            selectedQty(1);
                        }

                        //sectionCostCenter.name(stockItemToCreate().name);
                        sectionCostCenter.name('Stock(s)');
                        //sectionCostCenter.qty1NetTotal(stockItemToCreate().price);
                        sectionCostCenter.costCentreType('139');
                        //sectionCostCenter.qty1NetTotal(selectedCostCentre().quantity1());
                        //sectionCostCenter.qty2NetTotal(selectedCostCentre().quantity2());
                        //sectionCostCenter.qty2NetTotal(selectedCostCentre().quantity3());
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.qty1Charge(stockItemToCreate().price);
                        sectionCostCenter.qty2Charge(0);
                        sectionCostCenter.qty3Charge(0);
                        view.hideCostCentersQuantityDialog();

                        var sectionCostCenterDetail = model.SectionCostCenterDetail.Create({ SectionCostCentreId: selectedSectionCostCenter().id() });
                        sectionCostCenterDetail.stockName(stockItemToCreate().name);
                        sectionCostCenterDetail.stockId(stockItemToCreate().id);
                        sectionCostCenterDetail.costPrice(stockItemToCreate().price);
                        sectionCostCenterDetail.qty1(selectedCostCentre().quantity1());
                        //sectionCostCenterDetail.qty1NetTotal(selectedCostCentre().quantity1());
                        //sectionCostCenterDetail.qty2NetTotal(selectedCostCentre().quantity2());
                        //sectionCostCenterDetail.qty2NetTotal(selectedCostCentre().quantity3());

                        sectionCostCenter.sectionCostCentreDetails.splice(0, 0, sectionCostCenterDetail);
                        if (!containsStockItem) {
                            selectedSection().sectionCostCentres.splice(0, 0, sectionCostCenter);

                        }
                        else {
                            var newCost = selectedSectionCostCenter().qty1Charge() + sectionCostCenterDetail.costPrice();
                            selectedSectionCostCenter().qty1Charge(newCost);
                            selectedSectionCostCenter().sectionCostCentreDetails.splice(0, 0, sectionCostCenterDetail);
                        }
                    },

                     getStockCostCenterId = function (type) {
                         var costCentreId;
                         _.each(costCentres, function (costCenter) {
                             if (costCenter.Type == type) {
                                 costCentreId = costCenter.CostCentreId;
                             }
                         });
                         return costCentreId;
                     },
                    onSaveProductInventory = function () {
                        var item = model.Item.Create({ EstimateId: selectedOrder().id() });
                        selectedProduct(item);
                        item.productName(inventoryStockItemToCreate().name);
                        var itemSection = model.ItemSection.Create({});
                        var sectionCostCenter = model.SectionCostCentre.Create({});
                        sectionCostCenter.qty1(selectedCostCentre().quantity1());
                        sectionCostCenter.qty2(selectedCostCentre().quantity2());
                        sectionCostCenter.qty3(selectedCostCentre().quantity3());
                        sectionCostCenter.costCentreId(getStockCostCenterId(139));
                        sectionCostCenter.costCentreName(selectedCostCentre().name());
                        sectionCostCenter.name('Stock(s)');
                        //sectionCostCenter.qty1NetTotal(selectedCostCentre().quantity1());
                        //sectionCostCenter.qty2NetTotal(selectedCostCentre().quantity2());
                        //sectionCostCenter.qty2NetTotal(selectedCostCentre().quantity3());
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.qty1Charge(inventoryStockItemToCreate().price);
                        sectionCostCenter.qty2Charge(0);
                        sectionCostCenter.qty3Charge(0);
                        sectionCostCenter.costCentreType('139');

                        var sectionCostCenterDetail = model.SectionCostCenterDetail.Create({});
                        sectionCostCenterDetail.stockName(inventoryStockItemToCreate().name);
                        sectionCostCenterDetail.costPrice(inventoryStockItemToCreate().price);
                        sectionCostCenterDetail.qty1(inventoryStockItemToCreate().packageQty);

                        sectionCostCenter.sectionCostCentreDetails.splice(0, 0, sectionCostCenterDetail);

                        selectedSectionCostCenter(sectionCostCenter);
                        selectedQty(1);

                        itemSection.sectionCostCentres.push(sectionCostCenter);
                        item.itemSections.push(itemSection);
                        view.hideCostCentersQuantityDialog();
                        selectedOrder().items.splice(0, 0, item);

                        selectedSection(itemSection);

                    },
                    onSaveProductCostCenter = function () {
                        createNewCostCenterProduct();
                        hideCostCentreDialog();
                        hideCostCentreQuantityDialog();
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
                    totalProductPrice = ko.observable(0),

                    //Filtered Item Price matrix List
                    filteredItemPriceMatrixList = ko.observableArray([]),


                            //Call Method to update stock cost center
                            //If there is no selected cost center in retail store then add Cost Centers of Type 29 (Web Order Cost Center) and 139 (Stock Type Cost Center)
                            updateStockCostCenter = function (newItem) {

                                //requirement: add in both cases if hasSelectedCostCenter or not hasSelectedCostCenter


                                //var hasSelectedCostCenter = false;
                                //if (selecteditem() != undefined && selecteditem().isQtyRanged() == 2) {
                                //    if (selectedStockOption() != undefined && selectedStockOption().itemAddonCostCentres().length > 0) {
                                //        _.each(selectedStockOption().itemAddonCostCentres(), function (stockOption) {
                                //            if (stockOption.isSelected()) {
                                //                hasSelectedCostCenter = true;
                                //            }
                                //        });
                                //    }
                                //}
                                //else if (selecteditem() != undefined && selecteditem().isQtyRanged() == 1) {
                                //    if (selectedStockOption() != undefined && selectedStockOption().itemAddonCostCentres().length > 0) {
                                //        _.each(selectedStockOption().itemAddonCostCentres(), function (stockOption) {
                                //            if (stockOption.isSelected()) {
                                //                hasSelectedCostCenter = true;
                                //            }
                                //        });
                                //    }
                                //}
                                ////if Not Selected Any Cost Center
                                //if (!hasSelectedCostCenter) {
                                _.each(costCentresBaseData(), function (costCenter) {
                                    if (costCenter.Type == 29 || costCenter.Type == 139) {

                                        var sectionCostCenter = model.SectionCostCentre.Create({});
                                        sectionCostCenter.id(costCenter.CostCentreId);
                                        sectionCostCenter.name('Stock(s)');
                                        sectionCostCenter.qty1EstimatedStockCost(0);
                                        sectionCostCenter.qty2EstimatedStockCost(0);
                                        sectionCostCenter.qty3EstimatedStockCost(0);
                                        sectionCostCenter.qty1Charge(0);
                                        sectionCostCenter.qty2Charge(0);
                                        sectionCostCenter.qty3Charge(0);

                                        sectionCostCenter.costCentreType(costCenter.Type);

                                        newItem.itemSections()[0].sectionCostCentres.push(sectionCostCenter);

                                    }
                                });

                                //}
                            },

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
                    // Copy job Cards
                        copyJobCards = function () {
                            selectedProduct();
                            var conCatJobCards = "";
                            if (selectedProduct().jobDescription1() !== undefined && selectedProduct().jobDescription1().trim() !== "") {
                                conCatJobCards = selectedProduct().jobDescription1();
                            }
                            if (selectedProduct().jobDescription2() !== undefined && selectedProduct().jobDescription2().trim() !== "") {
                                if (conCatJobCards === "") {
                                    conCatJobCards = selectedProduct().jobDescription2();
                                } else {
                                    conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription2();
                                }
                            }
                            if (selectedProduct().jobDescription3() !== undefined && selectedProduct().jobDescription3().trim() !== "") {
                                if (conCatJobCards === "") {
                                    conCatJobCards = selectedProduct().jobDescription3();
                                } else {
                                    conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription3();
                                }
                            }
                            if (selectedProduct().jobDescription4() !== undefined && selectedProduct().jobDescription4().trim() !== "") {
                                if (conCatJobCards === "") {
                                    conCatJobCards = selectedProduct().jobDescription4();
                                } else {
                                    conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription4();
                                }
                            }
                            if (selectedProduct().jobDescription5() !== undefined && selectedProduct().jobDescription5().trim() !== "") {
                                if (conCatJobCards === "") {
                                    conCatJobCards = selectedProduct().jobDescription5();
                                } else {
                                    conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription5();
                                }
                            }
                            if (selectedProduct().jobDescription6() !== undefined && selectedProduct().jobDescription6().trim() !== "") {
                                if (conCatJobCards === "") {
                                    conCatJobCards = selectedProduct().jobDescription6();
                                } else {
                                    conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription6();
                                }
                            }
                            if (selectedProduct().jobDescription7() !== undefined && selectedProduct().jobDescription7().trim() !== "") {
                                if (conCatJobCards === "") {
                                    conCatJobCards = selectedProduct().jobDescription7();
                                } else {
                                    conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription7();
                                }
                            }
                            selectedProduct().invoiceDescription(conCatJobCards);
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
                            var deliverySchedule = model.ShippingInformation.Create({ EstimateId: selectedOrder().id() });
                            if (selectedOrder().items().length > 0) {
                                var item = selectedOrder().items()[0];
                                deliverySchedule.itemId(item.id());
                                setQuantityOfNewDeliverySchedule(deliverySchedule);
                            }
                            // deliverySchedule.deliveryNoteRaised(true);
                            selectedOrder().deliverySchedules.splice(0, 0, deliverySchedule);
                            selectedDeliverySchedule(selectedOrder().deliverySchedules()[0]);
                            selectedDeliverySchedule().estimateId(selectedOrder().id());

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
                                    selectedDeliverySchedule(deliverSchedule);
                                    setDeliveryScheduleFields();

                                }
                            }

                        },
                    //
                        calculateDeliveryShedulePrice = ko.computed(function () {
                            if (selectedDeliverySchedule() !== undefined && selectedDeliverySchedule().itemId() !== undefined && selectedDeliverySchedule().quantity() !== undefined && selectedDeliverySchedule().quantity() !== "") {
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
                            if (selectedSection().itemSizeHeight() == null || selectedSection().itemSizeWidth() == null || selectedSection().sectionSizeHeight() == null || selectedSection().sectionSizeWidth() == null) {
                                return;
                            }
                            isLoadingOrders(true);
                            var orient = selectedSection().printViewLayoutPortrait() >= selectedSection().printViewLayoutLandscape() ? 0 : 1;
                            dataservice.getPTV({
                                orientation: orient,
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
                                printGutter: 5,
                                horizentalGutter: 5,
                                verticalGutter: 5
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        itemPlan(undefined);
                                        side1Image(undefined);
                                        side2Image(undefined);
                                        side1Image(data.Side1ImageSource);
                                        showSide1Image(true);
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
                            if (selectedSection().itemSizeHeight() == null || selectedSection().itemSizeWidth() == null || selectedSection().sectionSizeHeight() == null || selectedSection().sectionSizeWidth() == null) {
                                return;
                            }
                            var orient;
                            if (selectedSection().printViewLayoutPortrait() >= selectedSection().printViewLayoutLandscape()) {
                                orient = 0;
                                selectedSection().isPortrait(true);
                            }
                            else {
                                orient = 1;
                                selectedSection().isPortrait(false);
                            }

                            isPtvCalculationInProgress(true);
                            dataservice.getPTVCalculation({
                                orientation: orient,
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
                                printGutter: 5,
                                horizentalGutter: 5,
                                verticalGutter: 5
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        selectedSection().printViewLayoutLandscape(data.LandscapePTV || 0);
                                        selectedSection().printViewLayoutPortrait(data.PortraitPTV || 0);
                                        // selectedSection().printViewLayout = data.LandscapePTV > data.PortraitPTV ? 1 : 0;
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
                        runWizard = function () {
                            errorList.removeAll();
                            if (!doBeforeRunningWizard()) {
                                selectedSection().errors.showAllMessages();
                                return;
                            }
                            $('#myTab a[href="#tab-recomendation"]').tab('show');
                            // $("#home").removeClass("active");  // this deactivates the home tab
                            // $("#profile").addClass("active");
                            getBestPress();
                        },
                        getBestPress = function () {
                            showEstimateRunWizard();
                            isLoadingOrders(true);
                            bestPressList.removeAll();
                            userCostCenters.removeAll();
                            selectedBestPressFromWizard(undefined);
                            dataservice.getBestPress(selectedSection().convertToServerData(), {
                                success: function (data) {
                                    if (data != null) {
                                        mapBestPressList(data.PressList);
                                        mapUserCostCentersList(data.UserCostCenters);
                                    }
                                    isLoadingOrders(false);
                                },
                                error: function (response) {
                                    isLoadingOrders(false);
                                    toastr.error("Error: Failed to Load Best Press List." + response, "", ist.toastrOptions);
                                }
                            });
                        },
                        doBeforeRunningWizard = function () {
                            var flag = true;
                            if (selectedSection().sectionInkCoverageList().length == 0) {
                                errorList.push({ name: "Please select ink colors.", element: selectedSection().plateInkId.domElement });
                                flag = false;
                            }
                            if (selectedSection().numberUp() <= 0) {
                                errorList.push({ name: "Sheet plan cannot be zero.", element: selectedSection().numberUp.domElement });
                                flag = false;
                            }
                            if (selectedSection().stockItemId() == null) {
                                errorList.push({ name: "Please select stock.", element: selectedSection().stockItemName.domElement });
                                flag = false;
                            }
                            return flag;
                        },
                    // Map Best Press List
                        mapBestPressList = function (data) {
                            var list = [];
                            _.each(data, function (item) {
                                list.push(BestPress.Create(item));
                            });

                            // Push to Original Array
                            ko.utils.arrayPushAll(bestPressList(), list);
                            bestPressList.valueHasMutated();
                            if (selectedSection().pressId() !== undefined) {
                                var bestPress = _.find(bestPressList(), function (item) {
                                    // var id = item.id;
                                    return item.id === selectedSection().pressId();
                                });
                                if (bestPress) {
                                    selectedBestPressFromWizard(bestPress);
                                } else {
                                    if (bestPressList().length > 0) {
                                        selectedBestPressFromWizard(bestPressList()[0]);
                                    }
                                }
                            } else {
                                if (bestPressList().length > 0) {
                                    selectedBestPressFromWizard(bestPressList()[0]);
                                }
                            }

                        },
                    // Map User Cost Centers
                        mapUserCostCentersList = function (data) {
                            var list = [];
                            _.each(data, function (item) {
                                list.push(UserCostCenter.Create(item));
                            });

                            // Push to Original Array
                            ko.utils.arrayPushAll(userCostCenters(), list);
                            userCostCenters.valueHasMutated();
                        },
                        getSectionSystemCostCenters = function () {
                            if (!selectedBestPressFromWizard()) {
                                return;
                            }

                            isLoadingOrders(true);
                            _.each(userCostCenters(), function (item) {
                                if (item.isSelected()) {
                                    var sectionCostCenterItem = model.SectionCostCentre();
                                    sectionCostCenterItem.id(item.id());
                                    sectionCostCenterItem.name(item.name());
                                    selectedSection().sectionCostCentres.push(sectionCostCenterItem);
                                }
                            });

                            var currSec = selectedSection().convertToServerData();
                            currSec.PressId = selectedBestPressFromWizard().id;
                            dataservice.getUpdatedSystemCostCenters(currSec, {
                                success: function (data) {
                                    if (data != null) {
                                        selectedSection(model.ItemSection.Create(data));
                                        hideEstimateRunWizard();
                                        var charge1 = setDecimalPlaceValue(selectedSection().baseCharge1());
                                        var charge2 = setDecimalPlaceValue(selectedSection().baseCharge2());
                                        var charge3 = setDecimalPlaceValue(selectedSection().baseCharge3());
                                        baseCharge1Total(charge1);
                                        baseCharge2Total(charge2);
                                        baseCharge3Total(charge3);

                                    }
                                    isLoadingOrders(false);
                                },
                                error: function (response) {
                                    isLoadingOrders(false);
                                    toastr.error("Error: Failed to Load System Cost Centers." + response);
                                }
                            });
                        },
                        setDecimalPlaceValue = function (chargevalue) {
                            if (chargevalue) {
                                var val = parseFloat(chargevalue);
                                var calc;
                                if (!isNaN(val)) {
                                    calc = (val.toFixed(2));                                    
                                    return calc;
                                }
                                else {
                                    calc = 0.00;
                                    return calc;
                                }
                            }
                            else {
                                return 0.00;
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
                        selectBestPressFromWizard = function (bestPress) {
                            selectedBestPressFromWizard(bestPress);
                            selectedSection().pressId(bestPress.id);
                        },
                        clickOnWizardOk = function () {
                            getSectionSystemCostCenters();
                        },
                    //Show Estimate Run Wizard
                        showEstimateRunWizard = function () {
                            view.showEstimateRunWizard();
                        },
                    //Hide Estimate Run Wizard
                        hideEstimateRunWizard = function () {
                            view.hideEstimateRunWizard();
                        },
                    // Delete Delivery Schedule
                        onDeleteDeliveryScheduleItem = function (deliverySchedule) {
                            if (selectedDeliverySchedule().deliveryNoteRaised()) {
                                toastr.error("Raised item cannot be deleted.");
                            } else {
                                confirmation.messageText("Are you sure you want to delete Delivery Schedule?");
                                confirmation.afterProceed(deleteDeliverySchedule);
                                confirmation.afterCancel(function () {

                                });
                                confirmation.show();
                                return;
                            }

                        },
                        deleteDeliverySchedule = function () {
                            selectedOrder().deliverySchedules.remove(selectedDeliverySchedule());
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
                        openReportsOrder = function () {
                            reportManager.show(12);
                        },
                    //#endregion
                    //#region INITIALIZE

                //Initialize method to call in every screen
                    initializeScreen = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);


                        categoryPager(new pagination.Pagination({ PageSize: 5 }, categories, getInventoriesListItems));
                        costCentrePager(new pagination.Pagination({ PageSize: 5 }, costCentres, getCostCentersForProduct));

                        // Get Base Data
                        getBaseData();

                        // On Dropdown filter selection change get orders
                        subscribeDropdownFilterChange();
                    },
                // Initialize the view model
                    initialize = function (specifiedView) {
                        initializeScreen(specifiedView);
                        pager(new pagination.Pagination({ PageSize: 5 }, orders, getOrders));
                        isEstimateScreen(false);
                        var orderIdFromDashboard = $('#OrderId').val();
                        if (orderIdFromDashboard != 0) {
                            editOrder({ id: function () { return orderIdFromDashboard; } });
                        } else {
                            getOrders();
                        }
                    },
                //Initialize Estimate
                    initializeEstimate = function (specifiedView) {
                        initializeScreen(specifiedView);
                        pager(new pagination.Pagination({ PageSize: 5 }, orders, getEstimates));
                        isEstimateScreen(true);
                        getEstimates();
                    };
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
                    openSectionCostCenterDialog: openSectionCostCenterDialog,
                    selectedSectionCostCenter: selectedSectionCostCenter,
                    selectedQty: selectedQty,
                    selectedProductQuanity: selectedProductQuanity,
                    totalProductPrice: totalProductPrice,
                    onShippingChargesClick: onShippingChargesClick,
                    onCostCenterClick: onCostCenterClick,
                    onSaveProductCostCenter: onSaveProductCostCenter,
                    isAddProductFromInventory: isAddProductFromInventory,
                    isAddProductForSectionCostCenter: isAddProductForSectionCostCenter,
                    onSaveStockitemForSectionCostCenter: onSaveStockitemForSectionCostCenter,
                    //#endregion Utility Methods
                    //#region Estimate Screen
                    initializeEstimate: initializeEstimate,
                    isEstimateScreen: isEstimateScreen,
                    //#endregion
                    //#region Dialog Product Section
                    orderProductItems: orderProductItems,
                    getOrders: getOrders,
                    getOrdersOfCurrentScreen: getOrdersOfCurrentScreen,
                    getOrdersOnTabChange: getOrdersOnTabChange,
                    openStockItemDialogForAddingProduct: openStockItemDialogForAddingProduct,
                    openStockItemDialogForAddingStock: openStockItemDialogForAddingStock,
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
                    flagForToShowAddTitle: flagForToShowAddTitle,
                    //#endregion
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    side1ButtonClick: side1ButtonClick,
                    side2ButtonClick: side2ButtonClick,
                    getPtvCalculation: getPtvCalculation,
                    openInkDialog: openInkDialog,
                    onSaveProductInventory: onSaveProductInventory,
                    //#endregion
                    //#region Delivery Schedule
                    selectDeliverySchedule: selectDeliverySchedule,
                    addDeliverySchedule: addDeliverySchedule,
                    selectedDeliverySchedule: selectedDeliverySchedule,
                    templateToUseDeliverySchedule: templateToUseDeliverySchedule,
                    onRaised: onRaised,
                    getPtvPlan: getPtvPlan,
                    getBestPress: getBestPress,
                    onDeleteDeliveryScheduleItem: onDeleteDeliveryScheduleItem,
                    //#endregion
                    //#region Section Detail
                    availableInkPlateSides: availableInkPlateSides,
                    paperSizes: paperSizes,
                    openStockItemDialog: openStockItemDialog,
                    getSectionSystemCostCenters: getSectionSystemCostCenters,
                    doBeforeRunningWizard: doBeforeRunningWizard,
                    bestPressList: bestPressList,
                    userCostCenters: userCostCenters,
                    selectBestPressFromWizard: selectBestPressFromWizard,
                    selectedBestPressFromWizard: selectedBestPressFromWizard,
                    clickOnWizardOk: clickOnWizardOk,
                    runWizard: runWizard,
                    downloadArtwork: downloadArtwork,
                    //#endregion
                    itemAttachmentFileLoadedCallback: itemAttachmentFileLoadedCallback,
                    copyJobCards: copyJobCards,
                    grossTotal: grossTotal,
                    onOrderStatusChange: onOrderStatusChange,
                    selectedItemForProgressToJobWizard: selectedItemForProgressToJobWizard,
                    clickOnJobToProgressWizard: clickOnJobToProgressWizard,
                    availableInkPalteChange: availableInkPalteChange,
                    openReportsOrder: openReportsOrder,
                    orderHasChanges: orderHasChanges
                };
            })()
        };
        return ist.order.viewModel;
    });
