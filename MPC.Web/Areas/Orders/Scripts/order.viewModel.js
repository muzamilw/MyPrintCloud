﻿/*
    Module with the view model for the Order.
*/
define("order/order.viewModel",
    ["jquery", "amplify", "ko", "order/order.dataservice", "order/order.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/phraseLibrary.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, phraseLibrary) {
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
                    // Errors List
                    errorList = ko.observableArray([]),
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
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    //Current Screen
                    currentScreen = ko.observable(),
                    //Selected Filter Flag on List View
                    selectedFilterFlag = ko.observable(0),
                    // Active Pre Payment
                    selectedPrePayment = ko.observable(),
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
                        companySelector.show(onSelectCompany, 1, true);
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
                        getBaseForCompany(company.id);
                    },
                    // Add Item
                    addItem = function () {
                        // Open Product Selector Dialog
                    },
                    // Edit Item
                    editItem = function (item) {
                        itemCodeHeader(item.code());
                        selectedProduct(item);
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

                                baseCharge1Total(parseInt(baseCharge1Total()) + parseInt((item.baseCharge1() !== undefined && item.baseCharge1() !== "" ? item.baseCharge1() : 0)) + parseInt(item.qty1Profit()));
                                baseCharge2Total(parseInt(baseCharge2Total()) + parseInt((item.baseCharge2() !== undefined && item.baseCharge2() !== "" ? item.baseCharge2() : 0)) + parseInt(item.qty2Profit()));
                                baseCharge3Total(parseInt(baseCharge3Total()) + parseInt((item.baseCharge3() !== undefined && item.baseCharge3() !== "" ? item.baseCharge3() : 0)) + parseInt(item.qty3Profit()));
                            });
                        }

                    }),
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
                    onChangeQty1MarkUpId = function (qty1Markup) {
                        if (selectedProduct().qty1MarkUpId1() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty1MarkUpId1();
                            });
                            if (markup) {
                                var markupValue = ((parseInt(markup.MarkUpRate) / 100) * baseCharge1Total());
                                selectedProduct().qty1NetTotal(markupValue);
                            }

                        } else {
                            selectedProduct().qty1NetTotal(0);
                        }

                    },
                     onChangeQty2MarkUpId = function (qtyMarkup) {

                         if (selectedProduct().qty2MarkUpId2() !== undefined) {
                             var markup = _.find(markups(), function (item) {
                                 return item.MarkUpId === selectedProduct().qty2MarkUpId2();
                             });
                             if (markup) {
                                 var markupValue = ((parseInt(markup.MarkUpRate) / 100) * baseCharge2Total());
                                 selectedProduct().qty2NetTotal(markupValue);
                             }

                         } else {
                             selectedProduct().qty2NetTotal(0);
                         }
                     },
                      onChangeQty3MarkUpId = function (qtyMarkup) {

                          if (selectedProduct().qty3MarkUpId3() !== undefined) {
                              var markup = _.find(markups(), function (item) {
                                  return item.MarkUpId === selectedProduct().qty3MarkUpId3();
                              });
                              if (markup) {
                                  var markupValue = ((parseInt(markup.MarkUpRate) / 100) * baseCharge3Total());
                                  selectedProduct().qty3NetTotal(markupValue);
                              }

                          } else {
                              selectedProduct().qty3NetTotal(0);
                          }


                      },

                    // #endregion
                    // #region ServiceCalls
                    // Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
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
                                        getBaseForCompany(data.CompanyId);
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
                    getBaseForCompany = function (id) {
                        isCompanyBaseDataLoaded(false);
                        dataservice.getBaseDataForCompany({
                            id: id
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
                                //isLoadingStores(false);
                                toastr.error("Failed to Load Company Products . Error: " + response);
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

                        toastr.success(item.id());
                    },
                    onCloseProductFromRetailStore = function () {
                        view.hideProductFromRetailStoreModal();
                    },
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
                        }
                        else if (selectedDeliverySchedule() !== undefined && selectedDeliverySchedule().itemId() && selectedDeliverySchedule().quantity() !== undefined && selectedDeliverySchedule().quantity() !== "") {
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
                                var netPrice = parseInt(selectedItem.qty1NetTotal());
                                selectedDeliverySchedule().price(perUnitPrice * netPrice);
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
                       }
                       else if (selectedItem && selectedItem.qty1() === undefined && selectedDeliverySchedule().quantity() !== undefined && selectedDeliverySchedule().quantity() > 0) {
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
                    //#endregion
                    //#region Delivery Schedule
                    selectDeliverySchedule: selectDeliverySchedule,
                    addDeliverySchedule: addDeliverySchedule,
                    selectedDeliverySchedule: selectedDeliverySchedule,
                    templateToUseDeliverySchedule: templateToUseDeliverySchedule,
                    onRaised: onRaised,
                    //#endregion
                };
            })()
        };
        return ist.order.viewModel;
    });
