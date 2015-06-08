/*
    Module with the view model for the live Jobs.
*/
define("purchaseOrders/purchaseOrders.viewModel",
    ["jquery", "amplify", "ko", "purchaseOrders/purchaseOrders.dataservice", "purchaseOrders/purchaseOrders.model", "common/pagination", "common/companySelector.viewModel", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, companySelector, confirmation) {
        var ist = window.ist || {};
        ist.purchaseOrders = {
            viewModel: (function () {
                var // the view 
                    view,
                    //Currency Symbol
                    currencySymbol = ko.observable(),
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
                    // Purchase Order Types
                    purchaseOrderTypes = ko.observableArray([
                        { id: 1, name: "Product" },
                        { id: 2, name: "Service" }
                    ]),
                    // Delivery Carriers
                    deliveryCarriers = ko.observableArray([]),
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
                    selectedPurchaseOrder = ko.observable(model.DeliveryNote()),
                    // For List View
                    selectedPurchaseOrderForListView = ko.observable(),
                    // Active Delivery Note Detail
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
                                pager().totalCount(data.TotalCount);
                            }

                        },
                        error: function () {
                            toastr.error("Failed to Items.");
                        }
                    });
                },

                // Get Delivery Note By ID
                getDetaildeliveryNote = function (id) {
                    isCompanyBaseDataLoaded(false);
                    dataservice.getDetaildeliveryNote({
                        deliverNoteId: id
                    }, {
                        success: function (data) {
                            if (data !== null && data !== undefined) {
                                var dNote = model.DeliveryNote.Create(data);
                                selectedPurchaseOrder(dNote);
                                selectedPurchaseOrder().companyName(data.CompanyName);
                                // Get Base Data For Company
                                if (data.CompanyId) {
                                    var storeId = 0;
                                    if (data.IsCustomer !== 3 && data.StoreId) {
                                        storeId = data.StoreId;
                                        selectedPurchaseOrder().storeId(storeId);
                                    } else {
                                        storeId = data.CompanyId;
                                    }
                                    selectedPurchaseOrder().reset();
                                    getBaseForCompany(data.CompanyId, storeId);
                                }
                            }
                        },
                        error: function () {
                            toastr.error("Failed to Items.");
                        }
                    });
                },
                        // Get Items
                    downloadArtwork = function () {
                        dataservice.downloadArtwork({
                            success: function (data) {


                            },
                            error: function () {
                                toastr.error("Failed to Items.");
                            }
                        });

                    },
                    searchData = function () {
                        pager().reset();
                        getPurchaseOrders();
                    },
                    onEditPurchaseOrder = function (item) {
                        //    selectedPurchaseOrderForListView(item);
                        //      getDetaildeliveryNote(item.deliveryNoteId());
                        isEditorVisible(true);
                    },
                    onCloseEditor = function () {
                        if (selectedPurchaseOrder().hasChanges() && selectedPurchaseOrder().isStatus() !== 20) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(function () {
                                onSaveDeliveryNotes();
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
                        companySelector.show(onSelectCompany, [0, 1, 3], true);
                    },
                      // On Select Company
                    onSelectCompany = function (company) {
                        if (!company) {
                            return;
                        }
                        if (selectedPurchaseOrder().companyId() === company.id) {
                            return;
                        }

                        selectedPurchaseOrder().companyId(company.id);
                        selectedPurchaseOrder().companyName(company.name);
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
                                    selectedCompanyTaxRate(data.TaxRate);
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
                    }, // Select Default Address For Company in case of new Delivery Note
                    setDefaultAddressForCompany = function () {
                        if (selectedPurchaseOrder().deliveryNoteId() > 0) {
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
                        if (selectedPurchaseOrder().deliveryNoteId() > 0) {
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
                                     ko.utils.arrayPushAll(deliveryCarriers(), data.DeliveryCarriers);
                                     deliveryCarriers.valueHasMutated();
                                 }

                             },
                             error: function (response) {
                                 toastr.error("Failed to load base data" + response);
                             }
                         });
                     },
                     // Add New Delivery Notes
                     addDeliveryNotes = function () {
                         var deliveryNotes = model.DeliveryNote();
                         deliveryNotes.isStatus(19);
                         selectedPurchaseOrder(deliveryNotes);
                         isEditorVisible(true);
                     },
                     // add Delivery Note Detail
                     addDeliveryNoteDetail = function () {
                         var deliveyNoteDetail = model.DeliveryNoteDetail();
                         selectedPurchaseOrderDetail(deliveyNoteDetail);
                         selectedPurchaseOrder().deliveryNoteDetails.splice(0, 0, deliveyNoteDetail);
                     },
                     // Template Chooser For Delivery Note Detail
                    templateToUseDeliveryNoteDetail = function (deliveryNoteDetail) {
                        return (deliveryNoteDetail === selectedPurchaseOrderDetail() ? 'editDeliveryNoteDetailemplate' : 'itemDeliveryNoteDetailTemplate');
                    },
                    selectDeliveryNoteDetail = function (deliveryNoteDetail) {
                        selectedPurchaseOrderDetail(deliveryNoteDetail);
                    },
                    // Delete Delivery Notes
                    onDeleteDeliveryNoteDetail = function (deliveryNoteDetail) {
                        selectedPurchaseOrder().deliveryNoteDetails.remove(deliveryNoteDetail);
                    },
                    // Save Delivery Notes
                    onSaveDeliveryNotes = function (deliveryNote) {
                        if (!dobeforeSave()) {
                            return;
                        }
                        var deliveryNotes = selectedPurchaseOrder().convertToServerData();
                        _.each(selectedPurchaseOrder().deliveryNoteDetails(), function (item) {
                            deliveryNotes.DeliveryNoteDetails.push(item.convertToServerData(item));
                        });
                        saveDeliveryNote(deliveryNotes);
                    },
                    onPostDeliveryNote = function (deliveryNote) {
                        if (!dobeforeSave()) {
                            return;
                        }

                        confirmation.messageText("Are you sure you want to Post Delivery Note?");
                        confirmation.afterProceed(function () {
                            selectedPurchaseOrder().isStatus(20);
                            var deliveryNotes = selectedPurchaseOrder().convertToServerData();
                            _.each(selectedPurchaseOrder().deliveryNoteDetails(), function (item) {
                                deliveryNotes.DeliveryNoteDetails.push(item.convertToServerData(item));
                            });
                            saveDeliveryNote(deliveryNotes);
                        });
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    // Delete Delivry Notes
                onDeleteDeliveryNote = function () {
                    confirmation.afterProceed(function () {
                        deleteDeliveryNote(selectedPurchaseOrder().convertToServerData());
                    });
                    confirmation.afterCancel(function () {

                    });
                    confirmation.show();
                    return;
                },
                deleteDeliveryNote = function (deliveryNote) {
                    dataservice.deleteDeliveryNote(deliveryNote, {
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
                // Save Delivery Notes
                saveDeliveryNote = function (deliveryNote) {
                    dataservice.saveDeliveryNote(deliveryNote, {
                        success: function (data) {
                            //For Add New
                            if (selectedPurchaseOrder().deliveryNoteId() === undefined || selectedPurchaseOrder().deliveryNoteId() === 0) {
                                purchaseOrders.splice(0, 0, model.purchaseOrders.Create(data));
                            } else {
                                selectedPurchaseOrderForListView().deliveryDate(data.DeliveryDate !== null ? moment(data.DeliveryDate).toDate() : undefined);
                                selectedPurchaseOrderForListView().flagId(data.FlagId);
                                selectedPurchaseOrderForListView().contactCompany(data.ContactCompany);
                                selectedPurchaseOrderForListView().companyName(data.CompanyName);
                                selectedPurchaseOrderForListView().flagColor(data.FlagColor);
                                selectedPurchaseOrderForListView().orderReff(data.OrderReff);
                                selectedPurchaseOrderForListView().creationDateTime(data.CreationDateTime !== null ? moment(data.CreationDateTime).toDate() : undefined);
                                if (currentTab() !== data.IsStatus) {
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
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(new pagination.Pagination({ PageSize: 5 }, purchaseOrders, getPurchaseOrders));
                   // getBaseData();
                   // getPurchaseOrders();

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
                    downloadArtwork: downloadArtwork,
                    isEditorVisible: isEditorVisible,
                    onCloseEditor: onCloseEditor,
                    openCompanyDialog: openCompanyDialog,
                    selectedCompany: selectedCompany,
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    companyContacts: companyContacts,
                    companyAddresses: companyAddresses,
                    selectedAddress: selectedAddress,
                    selectedCompanyContact: selectedCompanyContact,
                    // Arrays
                    sectionFlags: sectionFlags,
                    systemUsers: systemUsers,
                    purchaseOrderTypes: purchaseOrderTypes,
                    errorList: errorList,

                    // Utilities
                    getBaseData: getBaseData,
                    addDeliveryNotes: addDeliveryNotes,
                    addDeliveryNoteDetail: addDeliveryNoteDetail,
                    templateToUseDeliveryNoteDetail: templateToUseDeliveryNoteDetail,
                    selectDeliveryNoteDetail: selectDeliveryNoteDetail,
                    onDeleteDeliveryNoteDetail: onDeleteDeliveryNoteDetail,
                    onSaveDeliveryNotes: onSaveDeliveryNotes,
                    gotoElement: gotoElement,
                    deliveryCarriers: deliveryCarriers,
                    onDeleteDeliveryNote: onDeleteDeliveryNote,
                    onPostDeliveryNote: onPostDeliveryNote,
                    currentTab: currentTab,
                    getPurchaseOrdersOnTabChange: getPurchaseOrdersOnTabChange
                };
            })()
        };
        return ist.purchaseOrders.viewModel;
    });
