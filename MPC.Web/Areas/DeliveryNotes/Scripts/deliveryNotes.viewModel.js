/*
    Module with the view model for the live Jobs.
*/
define("deliveryNotes/deliveryNotes.viewModel",
    ["jquery", "amplify", "ko", "deliveryNotes/deliveryNotes.dataservice", "deliveryNotes/deliveryNotes.model", "common/pagination", "common/companySelector.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, companySelector) {
        var ist = window.ist || {};
        ist.deliveryNotes = {
            viewModel: (function () {
                var // the view 
                    view,
                    //Currency Symbol
                    currencySymbol = ko.observable(),
                    // #region Arrays
                    //Items
                    deliverNoteListView = ko.observableArray([]),
                        // company contacts
                    companyContacts = ko.observableArray([]),
                    // Company Addresses
                    companyAddresses = ko.observableArray([]),
                     // flag colors
                    sectionFlags = ko.observableArray([]),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    // #endregion
                    // is editor visible 
                    isEditorVisible = ko.observable(false),
                    // selected Cimpnay
                    selectedCompany = ko.observable(),
                    // #region Observables
                    selectedDeliveryNote = ko.observable(),
                    // Search Filter
                    searchFilter = ko.observable(),
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
                        if (!selectedDeliveryNote() || !selectedDeliveryNote().addressId() || companyAddresses().length === 0) {
                            return defaultAddress();
                        }

                        var addressResult = companyAddresses.find(function (address) {
                            return address.id === selectedDeliveryNote().addressId();
                        });

                        return addressResult || defaultAddress();
                    }),
                    // Selected Company Contact
                    selectedCompanyContact = ko.computed(function () {
                        if (!selectedDeliveryNote() || !selectedDeliveryNote().contactId() || companyContacts().length === 0) {
                            return defaultCompanyContact();
                        }

                        var contactResult = companyContacts.find(function (contact) {
                            return contact.id === selectedDeliveryNote().contactId();
                        });

                        return contactResult || defaultCompanyContact();
                    }),
                    // #endregion

                // Get Items
                getdeliveryNotes = function () {
                    dataservice.getdeliveryNotes({
                        SearchString: searchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            deliverNoteListView.removeAll();
                            if (data !== null && data !== undefined) {
                                var itemList = [];
                                _.each(data.DeliveryNotes, function (item) {
                                    itemList.push(model.deliverNoteListView.Create(item));
                                });
                                ko.utils.arrayPushAll(deliverNoteListView(), itemList);
                                deliverNoteListView.valueHasMutated();

                                pager().totalCount(data.TotalCount);
                            }

                        },
                        error: function () {
                            toastr.error("Failed to Items.");
                        }
                    });
                },

                // Get Item to edit 
                getDetaildeliveryNote = function (id) {
                    isCompanyBaseDataLoaded(false);
                    dataservice.getDetaildeliveryNote({
                        deliverNoteId: id
                    }, {
                        success: function (deliveryNote) {
                            if (deliveryNote !== null && deliveryNote !== undefined) {
                                var dNote = model.DeliveryNote.Create(deliveryNote);
                                selectedDeliveryNote(dNote);
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
                        getdeliveryNotes();
                    },
                    onEditDeliverNote = function (item) {
                        getBaseData();
                        getDetaildeliveryNote(item.deliveryNoteId());
                        isEditorVisible(true);
                    },
                    onCloseEditor = function () {
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
                        if (selectedDeliveryNote().companyId() === company.id) {
                            return;
                        }

                        selectedDeliveryNote().companyId(company.id);
                        selectedDeliveryNote().companyName(company.name);
                        selectedCompany(company);

                        // Get Company Address and Contacts
                        getBaseForCompany(company.id, company.id);

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
                     // Map List
                    mapList = function (observableList, data, factory) {
                        var list = [];
                        _.each(data, function (item) {
                            list.push(factory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(observableList(), list);
                        observableList.valueHasMutated();
                    }, // Select Default Address For Company in case of new order
                    setDefaultAddressForCompany = function () {
                        if (selectedDeliveryNote().deliveryNoteId() > 0) {
                            return;
                        }
                        var defaultCompanyAddress = companyAddresses.find(function (address) {
                            return address.isDefault;
                        });
                        if (defaultCompanyAddress) {
                            selectedDeliveryNote().addressId(defaultCompanyAddress.id);
                        }
                    },
                    // Select Default Contact For Company in case of new order
                    setDefaultContactForCompany = function () {
                        if (selectedDeliveryNote().deliveryNoteId() > 0) {
                            return;
                        }
                        var defaultContact = companyContacts.find(function (contact) {
                            return contact.isDefault;
                        });
                        if (defaultContact) {
                            selectedDeliveryNote().contactId(defaultContact.id);
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

                             },
                             error: function (response) {
                                 toastr.error("Failed to load base data" + response);
                             }
                         });
                     },
                     // Add New Delivery Notes
                     addDeliveryNotes = function () {
                         selectedDeliveryNote(model.DeliveryNote());
                         isEditorVisible(true);
                     },
                    //Initialize
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(new pagination.Pagination({ PageSize: 5 }, deliverNoteListView, getdeliveryNotes));
                        getdeliveryNotes();

                    };
                //#endregion 


                return {
                    initialize: initialize,
                    searchFilter: searchFilter,
                    onEditDeliverNote: onEditDeliverNote,
                    searchData: searchData,
                    selectedDeliveryNote: selectedDeliveryNote,
                    pager: pager,
                    deliverNoteListView: deliverNoteListView,
                    getdeliveryNotes: getdeliveryNotes,
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
                    sectionFlags: sectionFlags,
                    systemUsers: systemUsers,
                    getBaseData: getBaseData,
                    addDeliveryNotes:addDeliveryNotes


                };
            })()
        };
        return ist.deliveryNotes.viewModel;
    });
