/*
    Module with the view model for the Order.
*/
define("order/order.viewModel",
    ["jquery", "amplify", "ko", "order/order.dataservice", "order/order.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/stockItem.viewModel", "common/reportManager.viewModel", "common/addCostCenter.viewModel", "common/addProduct.viewModel", "common/itemDetail.viewModel", "common/itemDetail.model"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, stockDialog, reportManager, addCostCenterVM, addProductVm, itemDetailVm, itemModel) {
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
                    //On Order Status change to progress to job that will open wizard
                    selectedItemForProgressToJobWizard = ko.observable(itemModel.Item()),
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
                    // #endregion
                    // #region Utility Functions

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


                    // Create New Order
                    createOrder = function () {
                        selectedOrder(model.Estimate.Create({}));
                        view.setOrderState(4); // Pending Order
                        selectedOrder().statusId(4);
                        $('#orderDetailTabs a[href="#tab-EstimateHeader"]').tab('show');
                        openOrderEditor();
                    },
                    // Edit Order
                    editOrder = function (data) {
                        getOrderById(data.id(), openOrderEditor);
                        errorList.removeAll();
                        $('#orderDetailTabs a[href="#tab-EstimateHeader"]').tab('show');
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
                            total = total.toFixed(2);
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
                                if (orderIdFromDashboard != 0 && !isEstimateScreen()) {
                                    getOrders();
                                }

                            });
                            confirmation.show();
                            return;
                        }
                        var orderIdFromDashboardTemp = $('#OrderId').val();
                        if (orderIdFromDashboardTemp != 0 && !isEstimateScreen()) {
                            getOrders();
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
                        var itemSection = _.find(item.itemSections(), function (itemSec) {
                            return itemSec.flagForAdd() === true;
                        });
                        if (itemSection === undefined) {
                            var itemSectionForAddView = itemModel.ItemSection.Create({});
                            itemSectionForAddView.flagForAdd(true);
                            item.itemSections.push(itemSectionForAddView);
                        }
                        selectedProduct(item);
                        var section = selectedProduct() != undefined ? selectedProduct().itemSections()[0] : undefined;
                        editSection(section);
                        openItemDetail();
                    },
                    // Open Item Detail
                    openItemDetail = function () {
                        isItemDetailVisible(true);
                        itemDetailVm.showItemDetail(selectedProduct(), selectedOrder(), closeItemDetail);
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

                // Map List
                mapList = function (observableList, data, factory) {
                    var list = [];
                    _.each(data, function (item) {
                        list.push(factory.Create(item));
                    });

                    // Push to Original Array
                    ko.utils.arrayPushAll(observableList(), list);
                    observableList.valueHasMutated();
                    setDeliveryScheduleAddressName();
                },
                //In case Of Edit Order set Delivery Schedule Address Name
                setDeliveryScheduleAddressName = function () {
                    _.each(selectedOrder().deliverySchedules(), function (dSchedule) {
                        var selectedAddressItem = _.find(companyAddresses(), function (item) {
                            return item.id === dSchedule.addressId();
                        });
                        if (selectedAddressItem) {
                            dSchedule.addressName(selectedAddressItem.name);
                        }
                    });
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
                    stockDialog.show(function (stockItem) {
                        createNewInventoryProduct(stockItem);
                    }, stockCategory.paper, false, currencySymbol());
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
                        });
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
                        item.statusId(jobStatuses()[0].StatusId);
                        counter = counter + 1;
                    });

                    if (selectedOrder().items().length === counter) {
                        onStatusChangeDeliveryNotesCancelled();
                    }

                },

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

                selectedSectionCostCenter = ko.observable(),
                selectedQty = ko.observable(),

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

                afterSelectCostCenter = function (costCenter) {
                    selectedCostCentre(costCenter);
                    view.showCostCentersQuantityDialog();
                },
                //Product From Cost Center
                createNewCostCenterProduct = function () {
                    view.hideCostCentersQuantityDialog();
                    //selectedCostCentre(costCenter);
                    var item = itemModel.Item.Create({ EstimateId: selectedOrder().id() });
                    applyProductTax(item);
                    selectedProduct(item);
                    item.productName(selectedCostCentre().name());
                    item.qty1(selectedCostCentre().quantity1());
                    item.qty1NetTotal(selectedCostCentre().setupCost());
                    //Req: Item Product code is set to '2', so while editting item's section is non mandatory
                    item.productType(2);

                    var itemSection = itemModel.ItemSection.Create({});
                    itemSection.name("Text Sheet");
                    itemSection.qty1(selectedCostCentre().quantity1());
                    itemSection.qty2(selectedCostCentre().quantity2());
                    itemSection.qty3(selectedCostCentre().quantity3());
                    //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                    itemSection.productType(2);

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

                    //sectionCostCenter.qty1NetTotal(selectedCostCentre().setupCost());
                    sectionCostCenter.qty1Charge(selectedCostCentre().setupCost());

                    selectedSectionCostCenter(sectionCostCenter);
                    selectedQty(1);
                    itemSection.sectionCostCentres.push(sectionCostCenter);

                    item.itemSections.push(itemSection);
                    var itemSectionForAddView = itemModel.ItemSection.Create({});
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

                            nominalCodes.removeAll();
                            if (data.ChartOfAccounts) {
                                _.each(data.ChartOfAccounts, function (item) {
                                    nominalCodes.push(item);
                                });
                            }

                            costCentresBaseData.removeAll();//
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
                                    orderUpdated.code(data.OrderCode);
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
                        getEstimates(currentScreen());
                    }

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

                                // Set Delivey Schedule Item Name
                                setDeliveryScheduleItemName();
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

                //In case Of Edit Order set Delivery Schedule Item Name
                setDeliveryScheduleItemName = function () {
                    _.each(selectedOrder().deliverySchedules(), function (dSchedule) {
                        var selectedItem = _.find(selectedOrder().items(), function (item) {
                            return item.id() === dSchedule.itemId();
                        });
                        if (selectedItem) {
                            dSchedule.itemName(selectedItem.productName());
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
                        addProductVm.show(addItemFromRetailStore, companyId, costCentresBaseData(), currencySymbol(), selectedOrder().id(), saveSectionCostCenter, createitemForRetailStoreProduct);
                    }
                    addProductVm.show(addItemFromRetailStore, companyId, costCentresBaseData(), currencySymbol(), selectedOrder().id(), saveSectionCostCenter, createitemForRetailStoreProduct);
                },

                //},
                //addItemFromRetailStore = function (newItem) {
                //    selectedProduct(newItem);
                //    selectedOrder().items.splice(0, 0, newItem);
                //},
                addItemFromRetailStore = function (newItem) {
                    var itemSectionForAddView = itemModel.ItemSection.Create({});
                    itemSectionForAddView.flagForAdd(true);
                    newItem.itemSections.push(itemSectionForAddView);

                    selectedProduct(newItem);
                    selectedOrder().items.splice(0, 0, newItem);
                    itemDetailVm.updateOrderData(selectedOrder(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());
                },
                onAddCostCenter = function () {
                    // getCostCenters();
                    // view.showCostCentersDialog();
                    var companyId = 0;
                    if (selectedCompany() !== undefined && selectedCompany().isCustomer !== undefined && selectedCompany().isCustomer !== 3 && selectedCompany().storeId !== null) {
                        companyId = selectedCompany().storeId;
                    } else {
                        companyId = selectedOrder().companyId();
                    }
                    //addCostCenterVM.show(createNewCostCenterProduct, companyId, true);
                    addCostCenterVM.show(afterSelectCostCenter, companyId, true);
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
                    addCostCenterVM.show(afterSelectCostCenter, selectedOrder().companyId(), false, currencySymbol());
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
                onSaveProductInventory = function () {
                    var item = itemModel.Item.Create({ EstimateId: selectedOrder().id() });

                    item.productName(inventoryStockItemToCreate().name);
                    item.qty1(selectedCostCentre().quantity1());
                    //Req: Item Product type is set to '2', so while editting item's section is non mandatory
                    item.productType(2);
                    applyProductTax(item);
                    selectedProduct(item);
                    var itemSection = itemModel.ItemSection.Create({});
                    itemSection.name("Text Sheet");
                    itemSection.qty1(selectedCostCentre().quantity1());
                    itemSection.qty2(selectedCostCentre().quantity2());
                    itemSection.qty3(selectedCostCentre().quantity3());
                    //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                    itemSection.productType(2);

                    var sectionCostCenter = itemModel.SectionCostCentre.Create({});
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
                    itemSectionForAddView.flagForAdd(true);
                    item.itemSections.push(itemSectionForAddView);

                    view.hideCostCentersQuantityDialog();
                    selectedOrder().items.splice(0, 0, item);

                    selectedSection(itemSection);
                    //this method is calling to update orders list view total prices etc by trigering computed in item's detail view
                    itemDetailVm.updateOrderData(selectedOrder(), selectedProduct(), selectedSectionCostCenter(), selectedQty(), selectedSection());

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
                totalProductPrice = ko.observable(0).extend({ numberInput: ist.numberFormat }),

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

                            var sectionCostCenter = itemModel.SectionCostCentre.Create({});
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

                createitemForRetailStoreProduct = function (selectedItem) {
                    if (selectedItem === null || selectedItem === undefined) {
                        return;
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
                        var newItem = itemModel.Item.Create({});
                        applyProductTax(newItem);
                        //Req: Item Product code is set to '1', so while editting item's section is mandatory
                        newItem.productType(1);
                        newItem.productName("Blank Sheet");
                        newItem.qty1(0);
                        newItem.qty1GrossTotal(0);

                        var itemSection = itemModel.ItemSection.Create({});
                        itemSection.name("Text Sheet");
                        //Req: Item section Product type is set to '2', so while editting item's section is non mandatory
                        itemSection.productType(2);
                        newItem.itemSections.push(itemSection);
                        var itemSectionForAddView = itemModel.ItemSection.Create({});
                        itemSectionForAddView.flagForAdd(true);
                        newItem.itemSections.push(itemSectionForAddView);
                        selectedOrder().items.splice(0, 0, newItem);

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
                    calculateDeliveryShedulePrice = ko.computed(function () {
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




                    updateSectionFromCostCenterCalculation = function (section) {

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
                        reportManager.show(12, 0, 0);
                    },
                    openExternalReportsOrder = function () {
                        reportManager.show(12, 1, selectedOrder().id(), selectedOrder().companyName(), selectedOrder().orderCode(), selectedOrder().name());
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
                    //#region Utility Functions
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
                    createNewCostCenterProduct: createNewCostCenterProduct
                    //#endregion
                };
            })()
        };
        return ist.order.viewModel;
    });
