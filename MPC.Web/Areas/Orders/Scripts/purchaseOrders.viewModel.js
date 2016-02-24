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
                                        loggedInUser = ko.observable(),
                    // company contacts
                    companyContacts = ko.observableArray([]),
                    // Company Addresses
                    companyAddresses = ko.observableArray([]),
                    // flag colors
                    sectionFlags = ko.observableArray([]),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    // Delivery Carriers
                    deliveryCarriers = ko.observableArray([]),
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
                     // Purchase Order Detail Types
                    purchaseOrderDetailTypes = ko.observableArray([
                        { id: 1, name: "Product" },
                        { id: 2, name: "Service" }
                    ]),
                    // Errors List
                    errorList = ko.observableArray([]),
                    // #endregion
                    // is editor visible 
                    isEditorVisible = ko.observable(false),
                    // GRN Editor visiblek
                    isGRNEditorVisible = ko.observable(false),
                     // is open report
                     isOpenReport = ko.observable(false),
                      // is open report Email
                     isOpenReportEmail = ko.observable(false),

                    // selected Cimpnay
                    selectedCompany = ko.observable(),
                    // Default Status of first tab i-e All Purchased Orders
                    currentTab = ko.observable(0),
                    // #region Observables
                    selectedPurchaseOrder = ko.observable(model.Purchase()),
                    // Active GRN
                    selectedGRN = ko.observable(model.GoodsReceivedNote()),
                    // For List View
                    selectedPurchaseOrderForListView = ko.observable(),
                    // Active Purchase Detail
                    selectedPurchaseOrderDetail = ko.observable(),
                    // Active GRN Detail
                    selectedGRNDetail = ko.observable(),
                    // Search Filter
                    searchFilter = ko.observable(),
                    // purchase Order Type Filter, Default is Purchase Order
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
                      formatSelection = function (state) {
                          return "<span style=\"height:20px;width:20px;float:left;margin-right:10px;margin-top:5px;background-color:" + $(state.element).data("color") + "\"></span><span>" + state.text + "</span>";
                      },
                    formatResult = function (state) {
                        return "<div style=\"height:20px;margin-right:10px;width:20px;float:left;background-color:" + $(state.element).data("color") + "\"></div><div>" + state.text + "</div>";
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
                                view.initializeLabelPopovers();
                            },
                            error: function () {
                                toastr.error("Failed to load Purchase Order Detail.");
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                    // Get GRN By Id
                    getGRNById = function (id) {
                        isCompanyBaseDataLoaded(false);
                        dataservice.getGRNById({
                            grnId: id
                        }, {
                            success: function (data) {
                                if (data !== null && data !== undefined) {
                                    var grn = model.GoodsReceivedNote.Create(data);
                                    selectedGRN(grn);
                                    selectedGRN().companyName(data.CompanyName);
                                    // Get Base Data For Company
                                    if (data.SupplierId) {
                                        var storeId = 0;
                                        if (data.IsCustomer !== 3 && data.StoreId) {
                                            storeId = data.StoreId;
                                            selectedGRN().storeId(storeId);
                                        } else {
                                            storeId = data.SupplierId;
                                        }
                                        selectedGRN().reset();
                                        getBaseForCompany(data.SupplierId, storeId);
                                    }
                                }
                                view.initializeLabelPopovers();
                            },
                            error: function () {
                                toastr.error("Failed to load GRN Detail.");
                                view.initializeLabelPopovers();
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
                        if (purchaseOrderTypeFilter() === 1) {
                            getPurchaseOrderById(item.id());
                            isEditorVisible(true);
                            isGRNEditorVisible(false);
                        } else {
                            getGRNById(item.id());
                            isEditorVisible(false);
                            isGRNEditorVisible(true);
                        }
                        view.initializeLabelPopovers();
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
                        companySelector.show(onSelectCompany, [2]);
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

                        //selectedPurchaseOrder().taxRate(company.taxRate !== null ? company.taxRate : 0);

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
                                    if (purchaseOrderTypeFilter() === 1) {
                                        //selectedPurchaseOrder().reset();
                                    } else {
                                       // selectedGRN().reset();
                                    }

                                }
                                isCompanyBaseDataLoaded(true);
                                view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                isCompanyBaseDataLoaded(true);
                                toastr.error("Failed to load details for selected company" + response);
                                view.initializeLabelPopovers();
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
                                if (data.DeliveryCarriers) {
                                    // Push to Original Array
                                    ko.utils.arrayPushAll(deliveryCarriers(), data.DeliveryCarriers);
                                    deliveryCarriers.valueHasMutated();
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
                    // Add New Purchase
                    CreatePurchaseOrder = function () {
                        resetObservable();
                        var purchase = model.Purchase();
                        purchase.status(31);
                        selectedPurchaseOrder(purchase);
                        selectedPurchaseOrder().createdBy(loggedInUser());
                        isEditorVisible(true);
                        view.initializeLabelPopovers();
                    },
                    // Save Purchase Order
                    onSavePurchaseOrder = function (purchase) {
                        errorList.removeAll();
                        if (!dobeforeSave()) {
                            return;
                        }
                        savePurchaseOrder();
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

                    //  report preview
                      // report preview
                    openExternalReportsPurchase = function () {

                        reportManager.outputTo("preview");
                        if (selectedPurchaseOrder().hasChanges()) {
                            isOpenReport(true);
                            isOpenReportEmail(false);
                            onSavePurchaseOrder();
                        }
                        else {
                            reportManager.OpenExternalReport(ist.reportCategoryEnums.PurchaseOrders, 1, selectedPurchaseOrder().id());
                        }

                        //reportManager.SetOrderData(selectedOrder().orderReportSignedBy(), selectedOrder().contactId(), selectedOrder().id(),"");
                        //reportManager.show(ist.reportCategoryEnums.Orders, 1, selectedOrder().id(), selectedOrder().companyName(), selectedOrder().orderCode(), selectedOrder().name());


                    },

                    openExternalEmailPurchaseReport = function () {
                        reportManager.outputTo("email");


                        
                        if (selectedPurchaseOrder().hasChanges()) {
                            isOpenReport(true);
                            isOpenReportEmail(true);
                            onSavePurchaseOrder();
                        }
                        else {
                            reportManager.SetOrderData(selectedPurchaseOrder().createdBy(), selectedPurchaseOrder().contactId(), selectedPurchaseOrder().id(), 6, selectedPurchaseOrder().id(), "");
                            reportManager.OpenExternalReport(ist.reportCategoryEnums.PurchaseOrders, 1, selectedPurchaseOrder().id());

                        }


                       


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
                        confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
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
                                var poId = 0;
                                if (purchaseOrderTypeFilter() === 1 && (currentTab() === 0 || currentTab() === 31)) {
                                    //For Add New and selected category PO and tab must be ALL or Open
                                    if (selectedPurchaseOrder().id() === undefined || selectedPurchaseOrder().id() === 0) {
                                        purchaseOrders.splice(0, 0, model.PurchaseListView.Create(data));
                                    } else {
                                        selectedPurchaseOrderForListView().purchaseOrderDate(data.DatePurchase !== null ? moment(data.DatePurchase).toDate() : undefined);
                                        selectedPurchaseOrderForListView().flagColor(data.FlagColor);
                                        selectedPurchaseOrderForListView().refNo(data.RefNo);
                                        selectedPurchaseOrderForListView().refNo(data.RefNo);
                                        selectedPurchaseOrderForListView().status(data.Status);

                                        if (currentTab() !== 0 && currentTab() !== data.Status) {
                                            purchaseOrders.remove(selectedPurchaseOrderForListView());
                                        }
                                    }
                                }
                               
                                
                                if (isOpenReport() == true) {
                                    poId = (selectedPurchaseOrder().id() === undefined || selectedPurchaseOrder().id() === 0) ? data.PurchaseId : selectedPurchaseOrder().id();
                                    if (isOpenReportEmail() == true) {
                                       
                                        reportManager.SetOrderData(selectedPurchaseOrder().createdBy(), selectedPurchaseOrder().contactId(), poId, 6, poId, "");
                                        reportManager.OpenExternalReport(ist.reportCategoryEnums.PurchaseOrders, 1, poId);
                                    }
                                    else {
                                        reportManager.OpenExternalReport(ist.reportCategoryEnums.PurchaseOrders, 1, poId);
                                    }
                                    getPurchaseOrderById(poId);
                                    isOpenReport(false);
                                }
                                else {

                                    isEditorVisible(false);
                                    toastr.success("Saved Successfully.");
                                    view.initializeLabelPopovers();
                                }
                               
                            },
                            error: function (exceptionMessage, exceptionType) {
                                if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                    toastr.error(exceptionMessage);
                                } else {
                                    toastr.error("Failed to save.");
                                }
                                view.initializeLabelPopovers();
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
                        }, stockCategory.paper, true, currencySymbol(), 0);
                    },
                    //On Save Stock Item From Item Edit Dialog
                    onSaveStockItem = function (stockItem) {
                        selectedPurchaseOrderDetail().itemCode(stockItem.supplierCode);
                        selectedPurchaseOrderDetail().serviceDetail(stockItem.name);
                        selectedPurchaseOrderDetail().packqty(stockItem.packageQty);
                        selectedPurchaseOrderDetail().refItemId(stockItem.id);
                        selectedPurchaseOrderDetail().price(stockItem.price);
                        view.showPurchaseDetailDialog();
                        view.initializeLabelPopovers();

                    },
                    // Add Purchase Detail
                    addPurchaseDetail = function () {
                        if (selectedPurchaseOrder().supplierId() !== undefined) {
                            selectedPurchaseOrderDetail(model.PurchaseDetail());
                          //  selectedPurchaseOrderDetail().taxValue(selectedPurchaseOrder().taxRate());
                            selectedPurchaseOrderDetail().quantity(1);
                           // selectedPurchaseOrderDetail().discount(discountCalculate());
                            selectedPurchaseOrderDetail().productType(selectedPurchaseOrder().isproduct());
                            if (selectedPurchaseOrder().isproduct() === 1) {
                                openStockItemDialogForAddingStock();

                            } else {
                                view.showPurchaseDetailDialog();
                                view.initializeLabelPopovers();
                            }
                        } else {
                            toastr.error("Please select supplier");
                        }
                    },
                    // add To List
                    savePurchaseDetail = function () {
                        if (!dobeforeSavePurchaseDetail()) {
                            return;
                        }

                        if (selectedPurchaseOrderDetail().id() === undefined) {
                            purchaseDetailCounter = purchaseDetailCounter - 1;
                            selectedPurchaseOrderDetail().id(purchaseDetailCounter);
                            selectedPurchaseOrder().purchaseDetails.splice(0, 0, selectedPurchaseOrderDetail());
                        }
                        view.hidePurchaseDetailDialog();
                    },
                      dobeforeSavePurchaseDetail = function () {
                          var flag = true;
                          if (!selectedPurchaseOrderDetail().isValid()) {
                              selectedPurchaseOrderDetail().showAllErrors();
                              flag = false;
                          }
                          return flag;
                      },
                    // Delete Delivry Notes
                    onDeletePurchaseDetail = function (purchaseDetail) {
                        // Delete only in case Opend PO
                        if (selectedPurchaseOrder().status() === 31) {
                            confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
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
                            if (item.productType() === 1)
                                selectedPurchaseOrder().isproduct(1);
                            else
                                selectedPurchaseOrder().isproduct(2);

                            selectedPurchaseOrderDetail(item);
                            view.showPurchaseDetailDialog();
                            view.initializeLabelPopovers();
                            view.initializeLabelPopovers();
                        }

                    },
                    // 
                    setTaxValue = ko.computed(function () {
                        if (selectedPurchaseOrder() !== undefined) {

                            var tPrice = 0;
                            _.each(selectedPurchaseOrder().purchaseDetails(), function (item) {
                                tPrice = tPrice + item.totalPrice();
                            });
                            selectedPurchaseOrder().totalPrice(tPrice);
                            selectedPurchaseOrder().netTotal(tPrice);
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
                        selectedGRN(grn);
                        saveGRN(0);
                    },
                    mapPurachaseOrderToGRN = function (source, target) {
                        target.purchaseId(source.id());
                        target.deliveryDate(source.purchaseDate());
                        target.flagId(source.flagId());
                        target.reffNo(source.reffNo());
                        target.supplierId(source.supplierId());
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

                        _.each(source.purchaseDetails(), function (pDetail) {
                            var grnDetail = model.GoodsReceivedNoteDetail();
                            mapPurchaseDetailToGRNDetail(pDetail, grnDetail);
                            target.goodsReceivedNoteDetails.push(grnDetail);
                        });
                    },
                    mapPurchaseDetailToGRNDetail = function (source, target) {
                        target.quantity(source.quantity());
                        target.price(source.price());
                        target.packqty(source.packqty());
                        target.itemCode(source.itemCode());
                        target.serviceDetail(source.serviceDetail());
                        target.taxValue(source.taxValue());
                        target.totalPrice(source.totalPrice());
                        target.discount(source.discount());
                        target.freeitems(source.freeitems());
                        target.refItemId(source.refItemId());
                        target.productType(source.productType());
                    },
                     dobeforeSaveGRN = function () {
                         var flag = true;
                         if (!selectedGRN().isValid()) {
                             selectedGRN().showAllErrors();
                             selectedGRN().setValidationSummary(errorList);
                             flag = false;
                         }
                         return flag;
                     },
                    // Save GRN
                    onSaveGRN = function (grn) {
                        if (!dobeforeSaveGRN()) {
                            return;
                        }
                        saveGRN(2);
                    },
                    // Post GRN
                    onPostGRN = function (grn) {
                        if (!dobeforeSaveGRN()) {
                            return;
                        }

                        confirmation.messageText("Are you sure you want to Post GRN?");
                        confirmation.afterProceed(function () {
                            selectedGRN().status(32);
                            saveGRN();
                        });
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    // Save GRN
                    saveGRN = function (flag) {
                        //  0 mean Open form Create GRN 
                        // 1 Mean editor open from GRN List
                        // 2 mean just close editor after save
                        var grn = selectedGRN().convertToServerData();
                        _.each(selectedGRN().goodsReceivedNoteDetails(), function (item) {
                            grn.GoodsReceivedNoteDetails.push(item.convertToServerData(item));
                        });
                        dataservice.saveGRN(grn, {
                            success: function (data) {
                                // create GRN and also do changes in created GRN At same time
                                if (purchaseOrderTypeFilter() === 1) {
                                    if (flag == 0) {
                                        selectedGRN().id(data.PurchaseId);
                                        selectedGRN().reset();
                                        isGRNEditorVisible(true);
                                    } else {
                                        isGRNEditorVisible(false);
                                    }

                                } else {
                                    // GRN List is shown and change status to Post or do any change change in GRN
                                    selectedPurchaseOrderForListView().flagColor(data.FlagColor);
                                    if (currentTab() !== 0 && currentTab() !== data.Status) {
                                        purchaseOrders.remove(selectedPurchaseOrderForListView());
                                    }
                                    isGRNEditorVisible(false);
                                }
                                isEditorVisible(false);
                                view.initializeLabelPopovers();
                                toastr.success("Saved Successfully.");
                            },
                            error: function (exceptionMessage, exceptionType) {
                                if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                    toastr.error(exceptionMessage);
                                } else {
                                    toastr.error("Failed to save.");
                                }
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                     // Close GRN editor
                    onCloseGRNEditor = function () {
                        if (selectedGRN().hasChanges() && selectedGRN().status() === 31) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(function () {
                                saveGRN();
                            });
                            confirmation.afterCancel(function () {
                                isEditorVisible(false);
                                isGRNEditorVisible(false);
                            });
                            confirmation.show();
                            return;
                        }
                        isEditorVisible(false);
                        isGRNEditorVisible(false);
                    },
                    // Calculation For GRN
                    setTotalPriceAndNetAndGrandTotal = ko.computed(function () {
                        if (selectedGRN() !== undefined) {
                            var tPrice = 0;
                            _.each(selectedGRN().goodsReceivedNoteDetails(), function (item) {
                                tPrice = tPrice + item.totalPrice();
                            });
                            selectedGRN().totalPrice(tPrice);


                            var tTax = 0;
                            _.each(selectedGRN().goodsReceivedNoteDetails(), function (item) {
                                tTax = tTax + item.netTax();
                            });
                            selectedGRN().netTotal(tPrice + tTax);
                            selectedGRN().totalTax(tTax);
                            // In case of %
                            if (selectedGRN().discountType() === 1) {
                                var discountAmount = ((selectedGRN().discount() / 100) * selectedGRN().totalPrice());
                                selectedGRN().grandTotal(selectedGRN().netTotal() - discountAmount);
                            } else {
                                // In case of currency 
                                selectedGRN().grandTotal(selectedGRN().netTotal() - selectedGRN().discount());
                            }
                        }
                    }),

                    // Edit GRN Detail
                    editGRNDetail = function (item) {
                        // Edit only in case Opend GRN
                        if (selectedGRN().status() === 31) {
                            selectedGRNDetail(item);
                            view.showGRNDetailDialog();
                            view.initializeLabelPopovers();
                        }
                    },
                    // Save GRN Detail
                    saveGRNDetail = function (grn) {
                        view.hideGRNDetailDialog();
                    },
                     // Delete GRN Detail
                    onDeleteGRNDetail = function (grnDetail) {
                        // Delete only in case Opend GRN
                        if (selectedGRN().status() === 31) {
                            confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                            confirmation.afterProceed(function () {
                                selectedGRN().goodsReceivedNoteDetails.remove(grnDetail);
                            });
                            confirmation.afterCancel(function () {

                            });
                            confirmation.show();
                            return;
                        }
                        return;
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
                    isGRNEditorVisible: isGRNEditorVisible,
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
                    onCreateGRN: onCreateGRN,
                    purchaseOrderDetailTypes: purchaseOrderDetailTypes,
                    onSaveGRN: onSaveGRN,
                    onPostGRN: onPostGRN,
                    onCloseGRNEditor: onCloseGRNEditor,
                    selectedGRNDetail: selectedGRNDetail,
                    editGRNDetail: editGRNDetail,
                    deliveryCarriers: deliveryCarriers,
                    saveGRNDetail: saveGRNDetail,
                    onDeleteGRNDetail: onDeleteGRNDetail,
                    openExternalReportsPurchase: openExternalReportsPurchase,
                    openExternalEmailPurchaseReport: openExternalEmailPurchaseReport,
                    formatSelection: formatSelection,
                    formatResult: formatResult,
                    loggedInUser: loggedInUser
                };
            })()
        };
        return ist.purchaseOrders.viewModel;
    });
