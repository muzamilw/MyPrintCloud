/*
    Module with the view model for the Purchase Orders.
*/
define("purchaseOrders/purchaseOrders.viewModel",
    ["jquery", "amplify", "ko", "purchaseOrders/purchaseOrders.dataservice", "purchaseOrders/purchaseOrders.model", "common/pagination", "common/companySelector.viewModel", "common/confirmation.viewModel", "common/reportManager.viewModel", "common/stockItem.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, companySelector, confirmation, reportManager, stockVM) {
        var ist = window.ist || {};
        ist.purchaseOrders = {
            viewModel: (function () {
                var // the view 
                    view,
                    //Currency Symbol
                    currencySymbol = ko.observable(),
                    purchaseDetailCounter = 0,
                    // #region Arrays
                    //Items
                    purchaseOrders = ko.observableArray([]),
                    // company contacts
                    companyContacts = ko.observableArray([]),
                    // Company Addresses
                    companyAddresses = ko.observableArray([]),
                    // flag colors
                    sectionFlags = ko.observableArray([]),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    // Discount Types
                    discountTypes = ko.observableArray([
                        { id: 1, name: "%" },
                        { id: 2, name: "Currency" }
                    ]),
                    // Purchase Order Types
                    purchaseOrderTypes = ko.observableArray([
                        { id: 1, name: "PO" },
                        { id: 2, name: "GRN" }
                    ]),
                    // Errors List
                    errorList = ko.observableArray([]),
                    // #endregion
                    // is editor visible 
                    isEditorVisible = ko.observable(false),
                    // selected Cimpnay
                    selectedCompany = ko.observable(),
                    // Default Status of first tab i-e All Purchased Orders
                    currentTab = ko.observable(0),
                    // #region Observables
                    selectedPurchaseOrder = ko.observable(model.Purchase()),
                    // Active GRN
                    selectedGRN = ko.observable(model.Purchase()),
                    // For List View
                    selectedPurchaseOrderForListView = ko.observable(),
                    // Active Purchase Detail
                    selectedPurchaseOrderDetail = ko.observable(),
                    // Search Filter
                    searchFilter = ko.observable(),
                    // purchase Order Type Filter, Default is Product
                    purchaseOrderTypeFilter = ko.observable(1),
                    //Pager
                    pager = ko.observable(),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    // Is Company Base Data Loaded
                    isCompanyBaseDataLoaded = ko.observable(false),
                    // Tax Rate
                    selectedCompanyTaxRate = ko.observable(),
                    // Default Address
                    defaultAddress = ko.observable(model.Address.Create({})),
                    // Default Company Contact
                    defaultCompanyContact = ko.observable(model.CompanyContact.Create({})),
                    // Selected Address
                    selectedAddress = ko.computed(function () {
                        if (!selectedPurchaseOrder() || !selectedPurchaseOrder().addressId() || companyAddresses().length === 0) {
                            return defaultAddress();
                        }

                        var addressResult = companyAddresses.find(function (address) {
                            return address.id === selectedPurchaseOrder().addressId();
                        });

                        return addressResult || defaultAddress();
                    }),
                     openReport = function (isFromEditor) {
                         reportManager.show(ist.reportCategoryEnums.PurchaseOrders, isFromEditor == true ? true : false, 0);
                     },
                    // Selected Company Contact
                    selectedCompanyContact = ko.computed(function () {
                        if (!selectedPurchaseOrder() || !selectedPurchaseOrder().contactId() || companyContacts().length === 0) {
                            return defaultCompanyContact();
                        }

                        var contactResult = companyContacts.find(function (contact) {
                            return contact.id === selectedPurchaseOrder().contactId();
                        });

                        return contactResult || defaultCompanyContact();
                    }),
                    // #endregion
                    getPurchaseOrdersOnTabChange = function (currentTabStatus) {
                        searchFilter(undefined);
                        currentTab(currentTabStatus);
                        //pager(new pagination.Pagination({ PageSize: 5 }, purchaseOrders, getPurchaseOrders));
                        pager().reset();
                        getPurchaseOrders();
                    },
                    // Get Purchase Orders
                    getPurchaseOrders = function () {
                        dataservice.getPurchaseOrders({
                            SearchString: searchFilter(),
                            PurchaseOrderType: purchaseOrderTypeFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            Status: currentTab(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                purchaseOrders.removeAll();
                                if (data !== null && data !== undefined) {
                                    var itemList = [];
                                    _.each(data.PurchasesList, function (item) {
                                        itemList.push(model.PurchaseListView.Create(item));
                                    });
                                    ko.utils.arrayPushAll(purchaseOrders(), itemList);
                                    purchaseOrders.valueHasMutated();
                                    pager().totalCount(data.RowCount);
                                }

                            },
                            error: function () {
                                toastr.error("Failed to Items.");
                            }
                        });
                    },
                    // Get Purchase Order By Id
                    getPurchaseOrderById = function (id) {
                        isCompanyBaseDataLoaded(false);
                        dataservice.getPurchaseOrderById({
                            purchaseId: id
                        }, {
                            success: function (data) {
                                if (data !== null && data !== undefined) {
                                    var purchase = model.Purchase.Create(data);
                                    selectedPurchaseOrder(purchase);
                                    //selectedPurchaseOrder().companyName(data.CompanyName);
                                    selectedPurchaseOrder().companyName(data.SupplierContactCompany);
                                    // Get Base Data For Company
                                    if (data.SupplierId) {
                                        var storeId = 0;
                                        if (data.IsCustomer !== 3 && data.StoreId) {
                                            storeId = data.StoreId;
                                            selectedPurchaseOrder().storeId(storeId);
                                        } else {
                                            storeId = data.SupplierId;
                                        }
                                        selectedPurchaseOrder().reset();
                                        getBaseForCompany(data.SupplierId, storeId);
                                    }
                                }
                            },
                            error: function () {
                                toastr.error("Failed to Items.");
                            }
                        });
                    },
                    // Search
                    searchData = function () {
                        pager().reset();
                        getPurchaseOrders();
                    },
                    // Edit Purchase Order
                    onEditPurchaseOrder = function (item) {
                        resetObservable();
                        selectedPurchaseOrderForListView(item);
                        getPurchaseOrderById(item.id());
                        isEditorVisible(true);
                    },
                    // Close PO editor
                    onCloseEditor = function () {
                        if (selectedPurchaseOrder().hasChanges() && selectedPurchaseOrder().status() === 31) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(function () {
                                onSavePurchaseOrder();
                            });
                            confirmation.afterCancel(function () {
                                isEditorVisible(false);
                            });
                            confirmation.show();
                            return;
                        }
                        isEditorVisible(false);
                    },
                    // Open Company Dialog
                    openCompanyDialog = function () {
                        companySelector.show(onSelectCompany, [2], true);
                    },
                    // On Select Company
                    onSelectCompany = function (company) {
                        if (!company) {
                            return;
                        }
                        if (selectedPurchaseOrder().supplierId() === company.id) {
                            return;
                        }

                        selectedPurchaseOrder().supplierId(company.id);
                        selectedPurchaseOrder().companyName(company.name);
                        selectedPurchaseOrder().supplierContactCompany(company.name);
                        if (company.taxRate !== null) {
                            selectedPurchaseOrder().taxRate(company.taxRate);
                        }

                        selectedCompany(company);

                        if (company.isCustomer !== 3 && company.storeId) {
                            selectedPurchaseOrder().storeId(company.storeId);
                        }
                        // Get Company Address and Contacts
                        getBaseForCompany(company.id, (selectedPurchaseOrder().storeId() === null || selectedPurchaseOrder().storeId() === undefined) ? company.id :
                            selectedPurchaseOrder().storeId());
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
                                    selectedPurchaseOrder().taxRate(data.TaxRate);
                                    selectedPurchaseOrder().reset();
                                }
                                isCompanyBaseDataLoaded(true);
                            },
                            error: function (response) {
                                isCompanyBaseDataLoaded(true);
                                toastr.error("Failed to load details for selected company" + response);
                            }
                        });
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
                    }, // Select Default Address For Company in case of new Purchase Order
                    setDefaultAddressForCompany = function () {
                        if (selectedPurchaseOrder().id() > 0) {
                            return;
                        }
                        var defaultCompanyAddress = companyAddresses.find(function (address) {
                            return address.isDefault;
                        });
                        if (defaultCompanyAddress) {
                            selectedPurchaseOrder().addressId(defaultCompanyAddress.id);
                        }
                    },
                    // Select Default Contact For Company in case of new Delivery Note
                    setDefaultContactForCompany = function () {
                        if (selectedPurchaseOrder().id() > 0) {
                            return;
                        }
                        var defaultContact = companyContacts.find(function (contact) {
                            return contact.isDefault;
                        });
                        if (defaultContact) {
                            selectedPurchaseOrder().contactId(defaultContact.id);
                        }
                    },
                    getBaseData = function () {
                        dataservice.getBaseData({}, {
                            success: function (data) {

                                if (data.SectionFlags) {
                                    mapList(sectionFlags, data.SectionFlags, model.SectionFlag);
                                }
                                if (data.SystemUsers) {
                                    mapList(systemUsers, data.SystemUsers, model.SystemUser);
                                }
                                currencySymbol(data.CurrencySymbol);
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data" + response);
                            }
                        });
                    },
                    // Add New Purchase
                    CreatePurchaseOrder = function () {
                        resetObservable();
                        var purchase = model.Purchase();
                        purchase.status(31);
                        selectedPurchaseOrder(purchase);
                        isEditorVisible(true);
                    },
                    // Save Purchase Order
                    onSavePurchaseOrder = function (purchase) {
                        if (!dobeforeSave()) {
                            return;
                        }

                        if (selectedPurchaseOrder().id() !== undefined && selectedPurchaseOrder().status() === 31) {
                            confirmation.messageText("Do you want to Post the Purchase Order?");
                            confirmation.afterProceed(function () {
                                selectedPurchaseOrder().status(32);
                                savePurchaseOrder();
                            });
                            confirmation.afterCancel(function () {
                                savePurchaseOrder();
                            });
                            confirmation.show();
                            return;
                        }
                    },
                    // Post PO
                    onPostPurchaseOrder = function (purchase) {
                        if (!dobeforeSave()) {
                            return;
                        }

                        confirmation.messageText("Are you sure you want to Post Purchase Order?");
                        confirmation.afterProceed(function () {
                            selectedPurchaseOrder().status(32);
                            savePurchaseOrder();
                        });
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    // Cancel purchase Order
                    onCancelPurchaseOrder = function (purchase) {
                        if (!dobeforeSave()) {
                            return;
                        }

                        confirmation.messageText("Are you sure you want to Cancel Purchase Order?");
                        confirmation.afterProceed(function () {
                            selectedPurchaseOrder().status(33);
                            savePurchaseOrder();
                        });
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    // Delete Purchase Order
                    onDeletePurchase = function () {
                        confirmation.afterProceed(function () {
                            deletePurchaseOrder(selectedPurchaseOrder().convertToServerData());
                        });
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    deletePurchaseOrder = function (purchase) {
                        dataservice.deletePurchaseOrder(purchase, {
                            success: function (data) {
                                purchaseOrders.remove(selectedPurchaseOrderForListView());
                                isEditorVisible(false);
                                toastr.success("Delete Successfully.");
                            },
                            error: function (exceptionMessage, exceptionType) {
                                if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                    toastr.error(exceptionMessage);
                                } else {
                                    toastr.error("Failed to delete.");
                                }
                            }
                        });
                    },
                    // Save Purchase Order
                    savePurchaseOrder = function () {
                        var purchaseOrder = selectedPurchaseOrder().convertToServerData();
                        _.each(selectedPurchaseOrder().purchaseDetails(), function (item) {
                            purchaseOrder.PurchaseDetails.push(item.convertToServerData(item));
                        });
                        dataservice.savePurchase(purchaseOrder, {
                            success: function (data) {
                                //For Add New
                                if (selectedPurchaseOrder().id() === undefined || selectedPurchaseOrder().id() === 0) {
                                    purchaseOrders.splice(0, 0, model.PurchaseListView.Create(data));
                                } else {
                                    selectedPurchaseOrderForListView().purchaseOrderDate(data.DatePurchase !== null ? moment(data.DatePurchase).toDate() : undefined);
                                    selectedPurchaseOrderForListView().flagColor(data.FlagColor);
                                    selectedPurchaseOrderForListView().refNo(data.RefNo);

                                    if (currentTab() !== 0 && currentTab() !== data.Status) {
                                        purchaseOrders.remove(selectedPurchaseOrderForListView());
                                    }
                                }
                                isEditorVisible(false);
                                toastr.success("Saved Successfully.");
                            },
                            error: function (exceptionMessage, exceptionType) {
                                if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                    toastr.error(exceptionMessage);
                                } else {
                                    toastr.error("Failed to save.");
                                }
                            }
                        });
                    },
                    dobeforeSave = function () {
                        var flag = true;
                        if (!selectedPurchaseOrder().isValid()) {
                            selectedPurchaseOrder().showAllErrors();
                            selectedPurchaseOrder().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    // On Change Purchase Type Filter From Dropdown in List View
                    changePurchaseTypeFilter = function (id) {
                        pager().reset();
                        getPurchaseOrders();
                    },
                    // Stock Category 
                    stockCategory = {
                        paper: 1,
                        inks: 2,
                        films: 3,
                        plates: 4
                    },
                     // Open Stock Item Dialog For Adding Stock
                    openStockItemDialogForAddingStock = function () {
                        stockVM.show(function (stockItem) {
                            onSaveStockItem(stockItem);
                        }, stockCategory.paper, false, currencySymbol(), 0);
                    },
                    //On Save Stock Item From Item Edit Dialog
                    onSaveStockItem = function (stockItem) {
                        selectedPurchaseOrderDetail().itemCode();
                        selectedPurchaseOrderDetail().packqty(stockItem.packageQty);
                        selectedPurchaseOrderDetail().refItemId(stockItem.id);
                        view.showPurchaseDetailDialog();

                    },
                    // Add Purchase Detail
                    addPurchaseDetail = function () {
                        if (selectedPurchaseOrder().supplierId() !== undefined) {
                            selectedPurchaseOrderDetail(model.PurchaseDetail());
                            selectedPurchaseOrderDetail().taxValue(selectedPurchaseOrder().taxRate());
                            selectedPurchaseOrderDetail().quantity(1);
                            selectedPurchaseOrderDetail().discount(discountCalculate());
                            if (selectedPurchaseOrder().isproduct() === 1) {
                                openStockItemDialogForAddingStock();

                            } else {
                                view.showPurchaseDetailDialog();
                            }
                        } else {
                            toastr.error("Please select customer");
                        }
                    },
                    // add To List
                    savePurchaseDetail = function () {
                        if (selectedPurchaseOrderDetail().id() === undefined) {
                            purchaseDetailCounter = purchaseDetailCounter - 1;
                            selectedPurchaseOrderDetail().id(purchaseDetailCounter);
                            selectedPurchaseOrder().purchaseDetails.splice(0, 0, selectedPurchaseOrderDetail());
                        }
                        view.hidePurchaseDetailDialog();
                    },
                    // Delete Delivry Notes
                    onDeletePurchaseDetail = function (purchaseDetail) {
                        // Delete only in case Opend PO
                        if (selectedPurchaseOrder().status() === 31) {
                            confirmation.afterProceed(function () {
                                selectedPurchaseOrder().purchaseDetails.remove(purchaseDetail);
                            });
                            confirmation.afterCancel(function () {

                            });
                            confirmation.show();
                            return;
                        }
                        return;
                    },
                    // Edit Purchase Detail
                    editPurchaseDetail = function (item) {
                        // Edit only in case Opend PO
                        if (selectedPurchaseOrder().status() === 31) {
                            selectedPurchaseOrderDetail(item);
                            view.showPurchaseDetailDialog();
                        }
                    },
                    // 
                    setTaxValue = ko.computed(function () {
                        if (selectedPurchaseOrder() !== undefined) {
                            _.each(selectedPurchaseOrder().purchaseDetails(), function (item) {
                                if (item.taxValue() === null || item.taxValue() === undefined && item.id() < 0) {
                                    item.taxValue(selectedPurchaseOrder().taxRate());
                                }
                            });

                            var tPrice = 0;
                            _.each(selectedPurchaseOrder().purchaseDetails(), function (item) {
                                tPrice = tPrice + item.totalPrice();
                            });
                            selectedPurchaseOrder().totalPrice(tPrice);
                            selectedPurchaseOrder().netTotal(tPrice);

                            var tTax = 0;
                            _.each(selectedPurchaseOrder().purchaseDetails(), function (item) {
                                tTax = tTax + item.netTax();
                            });
                            selectedPurchaseOrder().totalTax(tTax);
                            // In case of %
                            if (selectedPurchaseOrder().discountType() === 1) {
                                var discountAmount = ((selectedPurchaseOrder().discount() / 100) * selectedPurchaseOrder().totalPrice());
                                selectedPurchaseOrder().grandTotal(selectedPurchaseOrder().totalPrice() - discountAmount);
                            } else {
                                // In case of currency 
                                selectedPurchaseOrder().grandTotal(selectedPurchaseOrder().totalPrice() - selectedPurchaseOrder().discount());
                            }
                        }
                    }),
                     // Discount Calculate
                    discountCalculate = function () {
                        if (selectedPurchaseOrder().discountType() === 1) {
                            // In case of %, discount is never greater than 100%
                            if (selectedPurchaseOrder().totalPrice() === 0) {
                                selectedPurchaseOrder().discount(0);
                            }
                            var discountAmount = ((selectedPurchaseOrder().discount() / 100) * selectedPurchaseOrder().totalPrice());
                            if (discountAmount > selectedPurchaseOrder().totalPrice()) {
                                selectedPurchaseOrder().discount(100);
                                return selectedPurchaseOrder().discount();
                            } else {
                                return selectedPurchaseOrder().discount();
                            }
                        } else {
                            // In case of currency, discount is never greater than Total Price
                            if (selectedPurchaseOrder().discount() > selectedPurchaseOrder().totalPrice()) {
                                selectedPurchaseOrder().discount(selectedPurchaseOrder().totalPrice());
                                return 100;
                            } else {
                                return ((selectedPurchaseOrder().discount() / selectedPurchaseOrder().totalPrice()) * 100);
                            }
                        }
                    },
                    // Set USer Discount
                    setDiscount = ko.computed(function () {
                        if (selectedPurchaseOrder() !== undefined) {
                            var discount = discountCalculate();
                            _.each(selectedPurchaseOrder().purchaseDetails(), function (pDetail) {
                                pDetail.discount(discount);
                            });
                        }
                    }),
                    // Reset Observables
                    resetObservable = function () {
                        errorList.removeAll();
                        companyContacts.removeAll();
                        companyAddresses.removeAll();
                    },
                    // Create GRN
                    onCreateGRN = function () {
                        var grn = model.GoodsReceivedNote();
                        mapPurachaseOrderToGRN(selectedPurchaseOrder(), grn);
                    },
                    mapPurachaseOrderToGRN = function (source, target) {
                        target.purchaseId(source.id());
                        target.deliveryDate(source.purchaseDate());
                        target.flagId(source.flagId());
                        target.reffNo(source.reffNo());
                        target.footnote(source.footnote());
                        target.comments(source.comments());
                        target.status(31);
                        target.contactId(source.contactId());
                        target.createdBy(source.createdBy());
                        target.discountType(source.discountType());
                        target.totalPrice(source.totalPrice());
                        target.netTotal(source.netTotal());
                        target.isproduct(source.isproduct());
                        target.totalTax(source.totalTax());
                        target.grandTotal(source.grandTotal());
                        target.discount(source.discount());
                        target.companyName(source.companyName());
                        target.taxRate(source.taxRate());

                    },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(new pagination.Pagination({ PageSize: 5 }, purchaseOrders, getPurchaseOrders));
                    getBaseData();
                    getPurchaseOrders();

                };
                //#endregion 


                return {
                    initialize: initialize,
                    searchFilter: searchFilter,
                    purchaseOrderTypeFilter: purchaseOrderTypeFilter,
                    onEditPurchaseOrder: onEditPurchaseOrder,
                    searchData: searchData,
                    selectedPurchaseOrder: selectedPurchaseOrder,
                    selectedPurchaseOrderDetail: selectedPurchaseOrderDetail,
                    pager: pager,
                    purchaseOrders: purchaseOrders,
                    getPurchaseOrders: getPurchaseOrders,
                    isEditorVisible: isEditorVisible,
                    onCloseEditor: onCloseEditor,
                    openCompanyDialog: openCompanyDialog,
                    selectedCompany: selectedCompany,
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    companyContacts: companyContacts,
                    companyAddresses: companyAddresses,
                    selectedAddress: selectedAddress,
                    selectedCompanyContact: selectedCompanyContact,
                    currencySymbol: currencySymbol,
                    selectedGRN: selectedGRN,
                    // Arrays
                    sectionFlags: sectionFlags,
                    systemUsers: systemUsers,
                    purchaseOrderTypes: purchaseOrderTypes,
                    errorList: errorList,

                    // Utilities
                    getBaseData: getBaseData,
                    CreatePurchaseOrder: CreatePurchaseOrder,
                    onSavePurchaseOrder: onSavePurchaseOrder,
                    gotoElement: gotoElement,
                    onDeletePurchase: onDeletePurchase,
                    onPostPurchaseOrder: onPostPurchaseOrder,
                    currentTab: currentTab,
                    getPurchaseOrdersOnTabChange: getPurchaseOrdersOnTabChange,
                    openReport: openReport,
                    changePurchaseTypeFilter: changePurchaseTypeFilter,
                    discountTypes: discountTypes,
                    addPurchaseDetail: addPurchaseDetail,
                    savePurchaseDetail: savePurchaseDetail,
                    onDeletePurchaseDetail: onDeletePurchaseDetail,
                    editPurchaseDetail: editPurchaseDetail,
                    onCancelPurchaseOrder: onCancelPurchaseOrder,
                    onCreateGRN: onCreateGRN
                };
            })()
        };
        return ist.purchaseOrders.viewModel;
    });
