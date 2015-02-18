/*
    Module with the view model for the crm
*/
define("crm/crm.viewModel",
    ["jquery", "amplify", "ko", "crm/crm.dataservice", "crm/crm.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm) {
        var ist = window.ist || {};
        ist.crm = {
            viewModel: (function () {
                var //View
                    view,
                //#region ___________ OBSERVABLES ___________
                    // Search filter 
                    searchFilter = ko.observable(),
                    // Pager for pagging
                    pager = ko.observable(),
                    // Sort On
                    sortOn = ko.observable(1),
                    // Sort In Ascending
                    sortIsAsc = ko.observable(true),

                    isEditorVisible = ko.observable(false),
                    //Selected Store
                    selectedStore = ko.observable(),
                    //Search Address Filter
                    searchAddressFilter = ko.observable(),
                    //SearchCompanyContactFilter
                    searchCompanyContactFilter = ko.observable(),
                    //contactCompanyTerritoryFilter
                    contactCompanyTerritoryFilter = ko.observable(),
                    //Address Pager
                    addressPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                    //Contact Company Pager
                    contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                    //Store Image
                    storeImage = ko.observable(),
                    //New Uploaded Media File
                    newUploadedMediaFile = ko.observable(model.MediaLibrary()),
                    //Is Loading stores
                    isLoadingStores = ko.observable(false),

                //#endregion

                //#region ___________ OBSERVABLE ARRAYS _____
                // Customers array for list view
                customersForListView = ko.observableArray(),
                //system Users
                systemUsers = ko.observableArray([]),
                //Contact Company Territories Filter
                contactCompanyTerritoriesFilter = ko.observableArray([]),
                //#endregion

                //#region ___________ LIST VIEW _____________
                // Gets customers for list view
                getCustomers = function () {
                    dataservice.getCustomersForListView({
                        SearchString: searchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    },
                    {
                        success: function (data) {
                            if (data != null) {
                                customersForListView.removeAll();
                                pager().totalCount(data.RowCount);
                                _.each(data.Customers, function (customer) {
                                    var customerModel = new model.customerViewListModel.Create(customer);
                                    customersForListView.push(customerModel);
                                });
                            }
                        },
                        error: function () {
                            toastr.error("Error: Failed To load Customers!");
                        }
                    });
                },

                // Search button handler
                searchButtonHandler = function () {
                    getCustomers();
                },
                //  Reset button handler
                resetButtonHandler = function () {
                    searchFilter(null);
                    getCustomers();
                },
                //// Select Store
                //selectStore = function(store) {
                //    if (selectedStore() !== store) {
                //        selectedStore(store);
                //    }
                //},
                //#endregion
                
                //#region ___________ UTILITY FUNCTIONS ______

                //Create New Store
                onCreateNewStore = function () {
                    var store = new model.Store();
                    selectedStore(store);
                    isEditorVisible(true);
                },

                //Close Edit Dialog
                closeEditDialog = function() {
                    isEditorVisible(false);
                },

                //Get Store For Editting
                getStoreForEditting = function (id) {
                    selectedStore(model.Store());
                    dataservice.getStoreById({
                        companyId: id
                    }, {
                        success: function (data) {
                            //selectedStore(model.Store());
                            if (data != null) {
                                selectedStore(model.Store.Create(data.Company));
                                
                                addressPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().addresses, searchAddress));
                                contactCompanyPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().users, searchCompanyContact));
                                addressPager().totalCount(data.Company.CompanyAddressesCount);
                                contactCompanyPager().totalCount(data.Company.CompanyContactCount);
                                storeImage(data.ImageSource);
                                
                                //Media Library
                                _.each(data.Company.MediaLibraries, function (item) {
                                    selectedStore().mediaLibraries.push(model.MediaLibrary.Create(item));
                                });
                            }
                            newUploadedMediaFile(model.MediaLibrary());

                            selectedStore().reset();
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Stores . Error: " + response);
                        }
                    });
                },

                //Open/Edit Store Dialog
                openEditDialog = function (item) {
                    isEditorVisible(true);
                    getStoreForEditting(item.id());
                    //view.initializeForm();
                    //getBaseData();
                    //view.initializeLabelPopovers();
                },

                //On Edit Click Of Store
                onEditItem = function (item) {
                    openEditDialog(item);
                    sharedNavigationVm.initialize(selectedStore, function (saveCallback) { saveStore(saveCallback); });
                },

                 //Search Address
                searchAddress = function () {
                    dataservice.searchAddress({
                        SearchFilter: searchAddressFilter(),
                        CompanyId: selectedStore().companyId(),
                        TerritoryId: addressTerritoryFilter(),
                        PageSize: addressPager().pageSize(),
                        PageNo: addressPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            selectedStore().addresses.removeAll();
                            addressPager().totalCount(data.RowCount);
                            _.each(data.Addresses, function (addressItem) {
                                var address = new model.Address.Create(addressItem);
                                selectedStore().addresses.push(address);
                            });
                            _.each(edittedAddresses(), function (item) {
                                _.each(selectedStore().addresses(), function (addressItem) {
                                    if (item.addressId() == addressItem.addressId()) {
                                        selectedStore().addresses.remove(addressItem);
                                    }
                                });
                            });
                            _.each(deletedAddresses(), function (item) {
                                _.each(selectedStore().addresses(), function (addressItem) {
                                    if (item.addressId() == addressItem.addressId()) {
                                        selectedStore().addresses.remove(addressItem);
                                    }
                                });
                            });
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Addresses" + response);
                        }
                    });
                },

                //Search Company Contact        
                searchCompanyContact = function () {
                    dataservice.searchCompanyContact({
                        SearchFilter: searchCompanyContactFilter(),
                        CompanyId: selectedStore().companyId(),
                        TerritoryId: contactCompanyTerritoryFilter(),
                        PageSize: companyContactPager().pageSize(),
                        PageNo: companyContactPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            selectedStore().users.removeAll();
                            contactCompanyPager().totalCount(data.RowCount);
                            _.each(data.CompanyContacts, function (companyContactItem) {
                                var companyContact = new model.CompanyContact.Create(companyContactItem);
                                selectedStore().users.push(companyContact);
                            });
                            _.each(edittedCompanyContacts(), function (item) {
                                _.each(selectedStore().users(), function (companyContactItem) {
                                    if (item.contactId() == companyContactItem.contactId()) {
                                        selectedStore().users.remove(companyContactItem);
                                    }
                                });
                            });
                            _.each(deletedCompanyContacts(), function (item) {
                                _.each(selectedStore().users(), function (companyContactItem) {
                                    if (item.contactId() == companyContactItem.contactId()) {
                                        selectedStore().users.remove(companyContactItem);
                                    }
                                });
                            });
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Users" + response);
                        }
                    });
                },

                //#endregion

                //#region ___________ ADDRESS _____________
                //Selected Address
                selectedAddress = ko.observable(),
                //Create Address
                onCreateNewAddress = function () {
                    var address = new model.Address();
                    selectedAddress(address);
                    isSavingNewAddress(true);
                    //Update If Store is creating new and it is Retail then 
                    //Make the first address isBilling and shipping as Default and sets its territory
                    //if (selectedStore().type() == 4 && selectedStore().companyId() == undefined) {
                        if (newAddresses != undefined && newAddresses().length == 0) {
                            if (newCompanyTerritories().length > 0) {
                                selectedAddress().territoryId(newCompanyTerritories()[0].territoryId());
                            }
                            selectedAddress().isDefaultTerrorityBilling(true);
                            selectedAddress().isDefaultTerrorityShipping(true);
                            selectedAddress().isDefaultAddress(true);
                        }
                        if (newAddresses().length > 0) {
                            if (newCompanyTerritories().length > 0) {
                                selectedAddress().territoryId(newCompanyTerritories()[0].territoryId());
                            }
                        }
                    //}
                    if (selectedStore().type() == 3 && selectedStore().companyId() == undefined) {
                        if (newAddresses != undefined && newAddresses().length == 0) {
                            if (newCompanyTerritories().length > 0) {
                                _.each(newCompanyTerritories(), function (territory) {
                                    if (territory.isDefault()) {
                                        selectedAddress().territoryId(territory.territoryId());
                                    }
                                });
                            }
                            selectedAddress().isDefaultTerrorityBilling(true);
                            selectedAddress().isDefaultTerrorityShipping(true);
                            selectedAddress().isDefaultAddress(true);
                        }
                        if (newAddresses().length > 0) {
                            if (newCompanyTerritories().length > 0) {
                                _.each(newCompanyTerritories(), function (territory) {
                                    if (territory.isDefault()) {
                                        selectedAddress().territoryId(territory.territoryId());
                                    }
                                });
                            }
                        }
                    }
                    view.showAddressDialog();
                },
                // Delete Address
                onDeleteAddress = function (address) {
                    if (address.isDefaultTerrorityBilling() || address.isDefaultTerrorityShipping() || address.isDefaultAddress()) {
                        toastr.error("Address can not be deleted as it is either Default Billing/ Default Shipping or is default address");
                        return;
                    } else {
                        //#region Db Saved Record Id > 0
                        if (address.addressId() > 0) {
                            if (address.companyId() > 0 && address.addressId() > 0) {

                                if (!address.isDefaultTerrorityBilling() || !address.isDefaultTerrorityShipping() || !address.isDefaultAddress()) {
                                    dataservice.deleteCompanyAddress({
                                        AddressId: address.addressId()
                                    }, {

                                        success: function (data) {
                                            if (data) {
                                                selectedStore().addresses.remove(address);
                                                toastr.success("Deleted Successfully");
                                                isLoadingStores(false);
                                                //Updating Drop downs
                                                //_.each(addressCompanyTerritoriesFilter(), function (item) {
                                                //    if (item.territoryId() == companyTerritory.territoryId()) {
                                                //        addressCompanyTerritoriesFilter.remove(item);
                                                //    }
                                                //});
                                                bussinessAddresses.remove(address);
                                                shippingAddresses.remove(address);
                                                allCompanyAddressesList.remove(address);
                                            } else {
                                                toastr.error("Address can not be deleted. It might exist in Contact");
                                            }
                                        },
                                        error: function (response) {
                                            isLoadingStores(false);
                                            toastr.error("Error: Failed To Delete Address " + response);
                                        }
                                    });
                                } else {
                                    toastr.error("Address can not be deleted as it contains default values");
                                }
                            }
                        }
                        //#endregion
                        //#region Else Company territry is newly created

                        //check local
                        if (address.addressId() < 0) {
                            var flag = true;

                            _.each(newCompanyContacts(), function (item) {
                                if (item.bussinessAddressId() == address.addressId() || item.shippingAddressId() == address.addressId()) {
                                    flag = false;
                                }
                            });
                            if (flag) {
                                selectedStore().addresses.remove(address);
                            }
                            else {
                                toastr.error("Address can not be deleted as it exist in User");
                            }

                        }
                        //#endregion
                        return;
                    }
                },
                onEditAddress = function (address) {
                    selectedAddress(address);
                    isSavingNewAddress(false);
                    view.showAddressDialog();
                },
                onCloseAddress = function () {
                    selectedAddress(undefined);
                    selectedBussinessAddress(undefined);
                    selectedBussinessAddressId(undefined);
                    selectedShippingAddress(undefined);
                    selectedShippingAddressId(undefined);
                    view.hideAddressDialog();
                    isSavingNewAddress(false);
                },
                //Do Before Save Address
                doBeforeSaveAddress = function () {
                    var flag = true;
                    if (!selectedAddress().isValid()) {
                        selectedAddress().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                
                //#endregion
               
                //#region ____________ INITIALIZE ___________
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   pager(new pagination.Pagination({ PageSize: 5 }, customersForListView, getCustomers));
                   getCustomers();
               };
                //#endregion

                //#region RETURN
                return {
                    initialize: initialize,
                    pager:pager,
                    searchFilter: searchFilter,
                    isEditorVisible: isEditorVisible,
                    customersForListView: customersForListView,
                    searchButtonHandler: searchButtonHandler,
                    resetButtonHandler: resetButtonHandler,
                    sharedNavigationVm: sharedNavigationVm,
                    onCreateNewStore: onCreateNewStore,
                    closeEditDialog: closeEditDialog,
                    //selectStore: selectStore,
                    selectedStore: selectedStore,
                    systemUsers: systemUsers,
                    searchAddressFilter: searchAddressFilter,
                    searchCompanyContactFilter: searchCompanyContactFilter,
                    contactCompanyTerritoriesFilter: contactCompanyTerritoriesFilter,
                    contactCompanyTerritoryFilter: contactCompanyTerritoryFilter,
                    onEditItem: onEditItem,
                    addressPager: addressPager,
                    contactCompanyPager: contactCompanyPager,
                    searchAddress: searchAddress,
                    isLoadingStores: isLoadingStores,
                    onEditAddress: onEditAddress,
                    onCreateNewAddress: onCreateNewAddress,
                    onDeleteAddress: onDeleteAddress,
                    onCloseAddress: onCloseAddress,
                    selectedAddress: selectedAddress,
                    searchCompanyContact: searchCompanyContact
                };
                //#endregion
            })()
        };
        return ist.crm.viewModel;
    });
