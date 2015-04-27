﻿/*
    Module with the view model for the Order.
*/
define("invoice/invoice.viewModel",
    ["jquery", "amplify", "ko", "invoice/invoice.dataservice", "invoice/invoice.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/phraseLibrary.viewModel", "common/stockItem.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, phraseLibrary, stockDialog) {
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
                    // #endregion
                    // #region Observables
                    // filter
                    filterText = ko.observable(),
                    // Selected Product
                    selectedProduct = ko.observable(),
                    // Base Charge 1 Total
                    baseCharge1Total = ko.observable(0),
                    selectedMarkup1 = ko.observable(0),
                    selectedCategoryId = ko.observable(),
                    currencySymbol = ko.observable(''),
                    // Active Order
                    selectedInvoice = ko.observable(),
                    // Page Header 
                    pageHeader = ko.computed(function () {
                        return selectedInvoice() && selectedInvoice().name() ? selectedInvoice().name() : 'Invoices';
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

                    // Edit Order
                    editInvoice = function (data) {
                        getInvoiceById(data.id(), openInvoiceEditor);
                    },
                    // Open Editor
                    openInvoiceEditor = function () {
                        isDetailsVisible(true);
                    },
                    // On Close Editor
                    onCloseInvoiceEditor = function () {
                        if (selectedInvoice().hasChanges()) {
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
                            archiveInvoice(invoice.id());
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
                    
                    
                    // Open Item Detail
                    openItemDetail = function () {
                        isItemDetailVisible(true);
                        view.initializeLabelPopovers();
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
                    
                    // On Save Order
                    onSaveInvoice = function (data, event, navigateCallback) {
                        if (!doBeforeSave()) {
                            return;
                        }
                       
                        var istatus = selectedInvoice().invoiceStatus();
                        if (istatus == 19)//Awaiting Invoice
                        {
                            confirmation.messageText("Do you want to post the invoice.");

                            confirmation.afterProceed(function () {
                                selectedInvoice().invoiceStatus(20);//Posted Invoice                              
                                saveInvoice(closeInvoiceEditor, navigateCallback);
                            });
                            confirmation.afterCancel(function () {
                                saveInvoice(closeInvoiceEditor, navigateCallback);
                            });
                            confirmation.show();
                            return;
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
                                       
                    selectedSectionCostCenter = ko.observable(),
                    selectedQty = ko.observable(),
                   
                    // #endregion
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
                    
                    // Save Invoice
                    saveInvoice = function (callback, navigateCallback) {                        
                        var invoice = selectedInvoice().convertToServerData();                        
                        dataservice.saveInvoice(invoice, {
                            success: function (data) {
                                if (!selectedInvoice().id()) {
                                    // Update Id
                                    selectedInvoice().id(data.InvoiceId);

                                    // Add to top of list
                                    invoices.splice(0, 0, selectedInvoice());
                                } else {
                                    // Get Order
                                    var invoiceUpdated = getInvoiceFromList(selectedInvoice().id());
                                    if (invoiceUpdated) {
                                        selectedInvoice().code(data.InvoiceCode);
                                        selectedInvoice().name(data.InvoiceName);
                                    }
                                }

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
                 
                    //get Invoices Of Current Screen
                    getInvoicesOfCurrentScreen = function () {
                        getInvoices(currentScreen());
                    },
                    //Get Order Tab Changed Event
                    getInvoicesOnTabChange = function (currentTab) {
                        pager().reset();
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
                                        getBaseForCompany(data.CompanyId, 0);
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
                    createInvoice = function () {                     
                        selectedInvoice(model.Invoice.Create({}));
                        selectedInvoice().invoiceStatus(19);
                        selectedInvoice().isPostedInvoice(false);
                        openInvoiceEditor();
                    },

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
                    // #region Observables
                    initialize:initialize,
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
                    getInvoicesOfCurrentScreen: getInvoicesOfCurrentScreen,
                    filterText: filterText,
                    orderType: orderType,
                    getBaseData: getBaseData,
                    editInvoice: editInvoice,
                    onCloseInvoiceEditor: onCloseInvoiceEditor,
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    invoiceTypes: invoiceTypes,
                    onSaveInvoice: onSaveInvoice,
                    getInvoicesOnTabChange: getInvoicesOnTabChange,
                    createInvoice: createInvoice
                };
            })()
        };
        return ist.invoice.viewModel;
    });
