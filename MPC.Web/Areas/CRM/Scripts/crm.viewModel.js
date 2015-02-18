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
                //#region ___________ OBSERVABLES ____________
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
                   
                    //Store Image
                    storeImage = ko.observable(),
                    //New Uploaded Media File
                    newUploadedMediaFile = ko.observable(model.MediaLibrary()),
                    //Is Loading stores
                    isLoadingStores = ko.observable(false),
                    //Selected Company Contact
                    selectedCompanyContact = ko.observable(),
                   

                //#endregion

                //#region ___________ OBSERVABLE ARRAYS ______
                // Customers array for list view
                customersForListView = ko.observableArray(),
                //system Users
                systemUsers = ko.observableArray([]),
                //Contact Company Territories Filter
                contactCompanyTerritoriesFilter = ko.observableArray([]),
                // New Company Territories 
                 newCompanyTerritories = ko.observableArray([]),
                //Roles
                roles = ko.observableArray([]),
                //RegistrationQuestions
                registrationQuestions = ko.observableArray([]),
                //Countries
                countries = ko.observableArray([]),
                //States
                states = ko.observableArray([]),
                //Tab User And Addressed, Addresses Section Company Territories Filter
                    addressCompanyTerritoriesFilter = ko.observableArray([]),
                    //Addresses to be used in store users shipping and billing address
                    allCompanyAddressesList = ko.observableArray([]),
                    //Filtered States
                    filteredStates = ko.observableArray([]),
                    // Error List
                    errorList = ko.observableArray([]),
                //#endregion

                //#region ___________ LIST VIEW ______________
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

                //#region ___________ COMPANY TERRITORY ______
                companyTerritoryCounter = -1,
                    addCompanyTerritoryCount = function () {
                        companyTerritoryCounter = (companyTerritoryCounter - 1);
                    },
                    //function to create new default territory for Prospect Or customer screen
                    createNewTerritoryForProspectOrCustomerStore = function () {
                        //selectedStore is new
                        //new CompanyTerritories have no record
                        if (selectedStore() != undefined && newCompanyTerritories != undefined && selectedStore().type() != undefined
                            && selectedStore().companyId() == undefined && newCompanyTerritories().length == 0 && selectedStore().type() == 0) {
                            var companyTerritory = new model.CompanyTerritory();
                            companyTerritory.territoryId(companyTerritoryCounter);
                            addCompanyTerritoryCount();
                            companyTerritory.territoryName('Default Retail Territory Name');
                            companyTerritory.territoryCode('Default Retail Territory Code');
                            companyTerritory.isDefault(true);

                            selectedStore().companyTerritories.splice(0, 0, companyTerritory);
                            newCompanyTerritories.push(companyTerritory);
                        }
                    },

                //#endregion

                // #region _________ADDRESSES _________________

                    //Selected Address
                    selectedAddress = ko.observable(),
                    //SelectedAddressTerritoryFilter
                    addressTerritoryFilter = ko.observable(),
                    //List for Address Territory
                    addressTerritoryList = ko.observableArray([]),
                    //Deleted Address
                    deletedAddresses = ko.observableArray([]),
                    edittedAddresses = ko.observableArray([]),
                    newAddresses = ko.observableArray([]),
                    shippingAddresses = ko.observableArray([]),
                    bussinessAddresses = ko.observableArray([]),
                    selectedBussinessAddress = ko.observable(),
                    selectedShippingAddress = ko.observable(),
                    selectedBussinessAddressId = ko.observable(),
                    selectedShippingAddressId = ko.observable(),
                    //Populate addresses lists
                    populateAddressesList = ko.computed(function () {
                        if (selectedCompanyContact() != undefined && selectedCompanyContact().territoryId() != undefined) {
                            shippingAddresses.removeAll();
                            bussinessAddresses.removeAll();
                            _.each(allCompanyAddressesList(), function (item) {

                                if (item.territoryId() == selectedCompanyContact().territoryId()) {
                                    shippingAddresses.push(item);
                                    bussinessAddresses.push(item);
                                }
                                ////if (item.isDefaultTerrorityShipping() == true && item.territoryId() == selectedCompanyContact().territoryId()) {
                                ////    shippingAddresses.push(item);
                                ////}
                                ////if (item.isDefaultTerrorityBilling() == true && item.territoryId() == selectedCompanyContact().territoryId()) {
                                ////    bussinessAddresses.push(item);
                                ////}
                                ////Updated Requirement ++
                                ////if (item.isDefaultAddress() == true && item.territoryId() == selectedCompanyContact().territoryId()) {
                                //    shippingAddresses.push(item);
                                //    bussinessAddresses.push(item);
                                ////}
                            });
                        }
                    }),
                    selectBussinessAddress = ko.computed(function () {
                        if (selectedCompanyContact() != undefined && selectedCompanyContact().addressId() != undefined) {
                        }
                        if (selectedBussinessAddressId() != undefined) {
                            _.each(allCompanyAddressesList(), function (item) {
                                if (item.addressId() == selectedBussinessAddressId()) {
                                    selectedBussinessAddress(item);
                                    if (item.city() == null) {
                                        selectedBussinessAddress().city(undefined);
                                    }
                                    if (item.state() == null) {
                                        selectedBussinessAddress().state(undefined);
                                    }
                                    if (selectedCompanyContact() != undefined) {
                                        selectedCompanyContact().bussinessAddressId(item.addressId());
                                        selectedCompanyContact().addressId(item.addressId());
                                        _.each(states(), function (state) {
                                            if (state.StateId == item.state()) {
                                                selectedBussinessAddress().stateName(state.StateName);
                                            }
                                        });
                                    }
                                }
                            });
                        }
                        if (selectedBussinessAddressId() == undefined) {
                            selectedBussinessAddress(undefined);
                            if (selectedCompanyContact() != undefined) {
                                //selectedCompanyContact().bussinessAddressId(undefined);
                            }
                        }
                        //if (isSavingNewCompanyContact != undefined && isSavingNewCompanyContact() && selectedStore().companyId() == undefined) {

                        //    _.each(newAddresses(), function (address) {
                        //        if (address.isDefaultTerrorityBilling()) {
                        //            selectedBussinessAddressId(address.addressId());
                        //        }
                        //    });
                        //    _.each(newCompanyTerritories(), function (territory) {
                        //        if (territory.isDefault()) {
                        //            selectedCompanyContact().territoryId(territory.territoryId());
                        //        }
                        //    });
                        //}
                    }),
                    selectShippingAddress = ko.computed(function () {
                        if (selectedShippingAddressId() != undefined) {
                            _.each(allCompanyAddressesList(), function (item) {
                                if (item.addressId() == selectedShippingAddressId()) {
                                    selectedShippingAddress(item);
                                    if (item.city() == null) {
                                        selectedShippingAddress().city(undefined);
                                    }
                                    if (item.state() == null) {
                                        selectedShippingAddress().state(undefined);
                                    }
                                    if (selectedCompanyContact() != undefined) {
                                        selectedCompanyContact().shippingAddressId(item.addressId());
                                        _.each(states(), function (state) {
                                            if (state.StateId == item.state()) {
                                                selectedShippingAddress().stateName(state.StateName);
                                            }
                                        });
                                    }
                                }
                            });
                        }
                        if (selectedShippingAddressId() == undefined) {
                            selectedShippingAddress(undefined);
                            if (selectedCompanyContact() != undefined) {
                                // selectedCompanyContact().shippingAddressId(undefined);
                            }
                        }
                        //if (isSavingNewCompanyContact != undefined && isSavingNewCompanyContact() && selectedStore().companyId() == undefined) {
                        //    _.each(newAddresses(), function (address) {
                        //        if (address.isDefaultTerrorityShipping()) {
                        //            selectedShippingAddressId(address.addressId());
                        //        }
                        //    });
                        //}
                    }),
                    //Get State Name By State Id

                    //Address Pager
                    addressPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                    //Contact Company Pager
                    contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                    //Secondary Page Pager
                    secondaryPagePager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                    //Address Search Filter
                    searchAddressFilter = ko.observable(),
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
                                //check on client side, push all if new added work
                                if (searchAddressFilter() == "" || searchAddressFilter() == undefined) {
                                    _.each(newAddresses(), function (addressItem) {
                                        selectedStore().addresses.push(addressItem);
                                    });
                                }
                                //check on client side, if filter is not null
                                if (searchAddressFilter() != "" && searchAddressFilter() != undefined) {
                                    _.each(newAddresses(), function (addressItem) {
                                        if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                            selectedStore().addresses.push(addressItem);
                                        }
                                    });
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed To Load Addresses" + response);
                            }
                        });
                    },
                    addressTerritoryFilterSelected = ko.computed(function () {
                        if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined && selectedStore().companyId() !== undefined) {
                            searchAddress();
                        }
                    }),
                    //isSavingNewAddress
                    isSavingNewAddress = ko.observable(false),
                    // Template Chooser For Address
                    templateToUseAddresses = function (address) {
                        return (address === selectedAddress() ? 'editAddressTemplate' : 'itemAddressTemplate');
                    },
                    //Create Address
                    onCreateNewAddress = function () {
                        var address = new model.Address();
                        selectedAddress(address);
                        isSavingNewAddress(true);
                        //Update If Store is creating new and it is Retail then 
                        //Make the first address isBilling and shipping as Default and sets its territory
                        if (selectedStore().type() == 0 && selectedStore().companyId() == undefined) {
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
                        }
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
                                } else {
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
                    newSavingAddressIdCount = -1,
                    addNewSavingAddressIdCount = function () {
                        newSavingAddressIdCount = newSavingAddressIdCount - 1;
                    },
                    onSaveAddress = function () {
                        if (doBeforeSaveAddress()) {
                            //#region Company Is Editting Case
                            if (selectedStore().companyId() > 0) {
                                selectedAddress().companyId(selectedStore().companyId());
                                dataservice.saveAddress(
                                    selectedAddress().convertToServerData(),
                                    {
                                        success: function (data) {
                                            if (data) {
                                                var savedAddress = model.Address.Create(data);

                                                _.each(selectedStore().addresses(), function (item) {
                                                    if (savedAddress.isDefaultTerrorityBilling()) {
                                                        if (item.isDefaultTerrorityBilling() == true) {
                                                            item.isDefaultTerrorityBilling(false);
                                                        }
                                                        if (savedAddress.isDefaultTerrorityShipping()) {
                                                            if (item.isDefaultTerrorityShipping() == true) {
                                                                item.isDefaultTerrorityShipping(false);
                                                            }
                                                        }
                                                        if (savedAddress.isDefaultAddress()) {
                                                            if (item.isDefaultAddress() == true) {
                                                                item.isDefaultAddress(false);
                                                            }
                                                        }
                                                    }

                                                });
                                                if (selectedAddress().addressId() <= 0 || selectedAddress().addressId() == undefined) {
                                                    selectedStore().addresses.splice(0, 0, savedAddress);
                                                }


                                                _.each(selectedStore().addresses(), function (item) {
                                                    if (savedAddress.isDefaultTerrorityBilling()) {
                                                        if (item.isDefaultTerrorityBilling() == true && item.territoryId() == savedAddress().territoryId()) {
                                                            item.isDefaultTerrorityBilling(false);
                                                        }
                                                    }
                                                    if (savedAddress.isDefaultTerrorityShipping()) {
                                                        if (item.isDefaultTerrorityShipping() == true && item.territoryId() == savedAddress().territoryId()) {
                                                            item.isDefaultTerrorityShipping(false);
                                                        }
                                                    }
                                                    if (savedAddress.isDefaultAddress()) {
                                                        if (item.isDefaultAddress() == true && item.territoryId() == savedAddress().territoryId()) {
                                                            item.isDefaultAddress(false);
                                                        }
                                                    }
                                                });
                                                toastr.success("Saved Successfully");
                                                //view.hideCompanyTerritoryDialog();
                                            }
                                        },
                                        error: function (response) {
                                            isLoadingStores(false);
                                            toastr.error("Error: Failed To Save Address " + response);
                                        }
                                    });
                            }
                                //#endregion
                                //#region New Company Case 

                            else {
                                if (selectedAddress().addressId() < 0) {
                                    _.each(selectedStore().addresses(), function (item) {
                                        if (selectedAddress().isDefaultTerrorityBilling() && selectedAddress().addressId() != item.addressId()) {
                                            if (item.isDefaultTerrorityBilling() == true && item.territoryId() == selectedAddress().territoryId()) {
                                                item.isDefaultTerrorityBilling(false);
                                            }
                                        }
                                        if (selectedAddress().isDefaultTerrorityShipping() && selectedAddress().addressId() != item.addressId()) {
                                            if (item.isDefaultTerrorityShipping() == true && item.territoryId() == selectedAddress().territoryId()) {
                                                item.isDefaultTerrorityShipping(false);
                                            }
                                        }
                                        if (selectedAddress().isDefaultAddress() && selectedAddress().addressId() != item.addressId()) {
                                            if (item.isDefaultAddress() == true && item.territoryId() == selectedAddress().territoryId()) {
                                                item.isDefaultAddress(false);
                                            }
                                        }
                                    });
                                }
                                if (selectedAddress().addressId() === undefined && isSavingNewAddress() === true) {
                                    selectedAddress().addressId(newSavingAddressIdCount);
                                    addNewSavingAddressIdCount();
                                    _.each(selectedStore().addresses(), function (item) {
                                        if (selectedAddress().isDefaultTerrorityBilling()) {
                                            if (item.isDefaultTerrorityBilling() == true && item.territoryId() == selectedAddress().territoryId()) {
                                                item.isDefaultTerrorityBilling(false);
                                            }
                                        }
                                        if (selectedAddress().isDefaultTerrorityShipping()) {
                                            if (item.isDefaultTerrorityShipping() == true && item.territoryId() == selectedAddress().territoryId()) {
                                                item.isDefaultTerrorityShipping(false);
                                            }
                                        }
                                        if (selectedAddress().isDefaultAddress()) {
                                            if (item.isDefaultAddress() == true && item.territoryId() == selectedAddress().territoryId()) {
                                                item.isDefaultAddress(false);
                                            }
                                        }
                                    });
                                    selectedStore().addresses.splice(0, 0, selectedAddress());
                                    //selectedAddress().territoryName(getTerritoryByTerritoryId(selectedAddress().territoryId()).territoryName());
                                    //selectedAddress().territory(getTerritoryByTerritoryId(selectedAddress().territoryId()));
                                    newAddresses.push(selectedAddress());

                                    bussinessAddresses.push(selectedAddress());
                                    shippingAddresses.push(selectedAddress()); 
                                    allCompanyAddressesList.push(selectedAddress());

                                } else {
                                    //pushing item in editted Addresses List
                                    if (selectedAddress().addressId() > 0) {
                                        var match = ko.utils.arrayFirst(edittedAddresses(), function (item) {
                                            return (selectedAddress().addressId() === item.addressId());
                                        });
                                        if (!match || match == null) {
                                            edittedAddresses.push(selectedAddress());
                                        }
                                    }
                                }
                                if (selectedAddress().addressId() < 0 && isSavingNewAddress() !== true) {
                                    //selectedAddress().territoryName(getTerritoryByTerritoryId(selectedAddress().territoryId()).territoryName());
                                }
                            }
                            //#endregion

                            view.hideAddressDialog();
                        }
                    },
                    getTerritoryByTerritoryId = function (territoryId) {

                        var result = _.find(addressCompanyTerritoriesFilter(), function (territory) {
                            return territory.territoryId() == parseInt(territoryId);
                        });
                        return result;
                    },
                    // #endregion

                // #region _________COMPANY CONTACT ___________

                    //companyContactFilter
                    companyContactFilter = ko.observable(),
                    contactCompanyTerritoryFilter = ko.observable(),
                    //Deleted Company Contact 
                    deletedCompanyContacts = ko.observableArray([]),
                    edittedCompanyContacts = ko.observableArray([]),
                    newCompanyContacts = ko.observableArray([]),
                    //Company Contact  Pager
                    companyContactPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                    //Company Contact Search Filter
                    searchCompanyContactFilter = ko.observable(),
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
                                //check on client side, push all if new added work
                                if (searchCompanyContactFilter() == "" || searchCompanyContactFilter() == undefined) {
                                    _.each(newCompanyContacts(), function (companyContactItem) {
                                        selectedStore().users.push(companyContactItem);
                                    });
                                }
                                //check on client side, if filter is not null
                                if (searchCompanyContactFilter() != "" && searchCompanyContactFilter() != undefined) {
                                    _.each(newCompanyContacts(), function (companyContactItem) {
                                        if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                            selectedStore().users.push(companyContactItem);
                                        }
                                    });
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed To Load Users" + response);
                            }
                        });
                    },
                    companyContactFilterSelected = ko.computed(function () {
                        if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined) {
                            searchCompanyContact();
                        }
                    }),
                    //isSavingNewCompanyContact
                    isSavingNewCompanyContact = ko.observable(false),
                    // Template Chooser For CompanyContact
                    templateToUseCompanyContacts = function (companyContact) {
                        return (companyContact === selectedCompanyContact() ? 'editCompanyContactTemplate' : 'itemCompanyContactTemplate');
                    },
                    
                    //Create CompanyContact
                    onCreateNewCompanyContact = function () {
                        var user = new model.CompanyContact();
                        //selectedBussinessAddressId(undefined);
                        selectedShippingAddressId(undefined);
                        isSavingNewCompanyContact(true);
                        selectedCompanyContact(user);
                        if (selectedStore().type() == 0) {
                            if (newAddresses != undefined && newAddresses().length == 0) {
                                if (newCompanyTerritories.length > 0) {
                                    selectedCompanyContact().territoryId(newCompanyTerritories()[0].territoryId());
                                }
                            }
                        }
                        if (isSavingNewCompanyContact != undefined && isSavingNewCompanyContact() && selectedStore().companyId() == undefined) {
                            _.each(newCompanyTerritories(), function (territory) {
                                if (territory.isDefault()) {
                                    selectedCompanyContact().territoryId(territory.territoryId());
                                }
                            });
                            _.each(newAddresses(), function (address) {
                                //Billing Address Id Selection
                                if (address.isDefaultTerrorityBilling() && address.territoryId() === selectedCompanyContact().territoryId()) {
                                    selectedBussinessAddressId(address.addressId());
                                    selectedCompanyContact().bussinessAddressId(address.addressId());
                                }
                                //Shipping Address Id Selection
                                if (address.isDefaultTerrorityShipping() && address.territoryId() === selectedCompanyContact().territoryId()) {
                                    selectedShippingAddressId(address.addressId());
                                    selectedCompanyContact().shippingAddressId(address.addressId());
                                }
                            });

                            //_.each(newAddresses(), function (address) {
                            //    if (address.isDefaultTerrorityShipping()) {
                            //        selectedShippingAddressId(address.addressId());
                            //        selectedCompanyContact().shippingAddressId(address.addressId());
                            //    }
                            //});

                            //select isDefaultContact for the very first contact by defaault
                            if (newCompanyContacts().length == 0) {
                                selectedCompanyContact().isDefaultContact(true);
                            }
                        }
                        //_.each(newAddresses(), function (address) {
                        //    if (address.isDefaultTerrorityBilling()) {
                        //        selectedBussinessAddressId(address.addressId());
                        //    }
                        //});
                        //_.each(newAddresses(), function (address) {
                        //    if (address.isDefaultTerrorityShipping()) {
                        //        selectedShippingAddressId(address.addressId());
                        //    }
                        //});
                       

                        view.showCompanyContactDialog();
                    },
                    // Delete CompanyContact
                    onDeleteCompanyContact = function (companyContact) { //CompanyContact
                        if (companyContact.isDefaultContact()) {
                            toastr.error("Default Contact Cannot be deleted");
                            return;
                        }
                        //#region Db Saved Record Id > 0
                        if (companyContact.contactId() > 0) {

                            if (companyContact.companyId() > 0 && companyContact.contactId() > 0) {
                                dataservice.deleteCompanyContact({
                                    CompanyContactId: companyContact.contactId()
                                }, {
                                    success: function (data) {
                                        if (data) {
                                            selectedStore().users.remove(companyContact);
                                            toastr.success("Deleted Successfully");
                                            isLoadingStores(false);
                                        } else {
                                            toastr.error("Contact can not be deleted");
                                        }
                                    },
                                    error: function (response) {
                                        isLoadingStores(false);
                                        toastr.error("Error: Failed To Delete Company Contact " + response);
                                    }
                                });
                            }
                        }
                            //#endregion
                        else {
                            if (companyContact.contactId() < 0 || companyContact.contactId() == undefined) {

                                _.each(newCompanyContacts(), function (item) {
                                    if (item.contactId() == companyContact.contactId()) {
                                        newCompanyContacts.remove(companyContact);
                                    }
                                });
                                selectedStore().users.remove(companyContact);
                            }
                        }

                        return;
                    },
                    onEditCompanyContact = function (companyContact) {
                        selectedCompanyContact(companyContact);
                        isSavingNewCompanyContact(false);
                        view.showCompanyContactDialog();
                    },
                    onCloseCompanyContact = function () {
                        selectedCompanyContact(undefined);
                        selectedBussinessAddressId(undefined);
                        view.hideCompanyContactDialog();
                        isSavingNewCompanyContact(false);
                    },
                    //Do Before Save CompanyContact
                    doBeforeSaveCompanyContact = function () {
                        var flag = true;
                        if (!selectedCompanyContact().isValid()) {
                            selectedCompanyContact().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    onSaveCompanyContact = function () {
                        if (doBeforeSaveCompanyContact()) {
                            //#region Editting Company Case companyid > 0
                            if (selectedStore().companyId() > 0) {

                                selectedCompanyContact().companyId(selectedStore().companyId());
                                dataservice.saveCompanyContact(
                                    selectedCompanyContact().convertToServerData(),
                                    {
                                        success: function (data) {
                                            if (data) {
                                                var savedCompanyContact = model.CompanyContact.Create(data);
                                                if (selectedCompanyContact().isDefaultContact()) {
                                                    _.each(selectedStore().users(), function (user) {
                                                        if (user.isDefaultContact()) {
                                                            if (selectedCompanyContact().contactId() != user.contactId()) {
                                                                user.isDefaultContact(false);
                                                            }
                                                        }
                                                    });
                                                }
                                                if (selectedCompanyContact().contactId() <= 0 || selectedCompanyContact().contactId() == undefined) {
                                                    selectedStore().users.splice(0, 0, savedCompanyContact);
                                                }
                                                toastr.success("Saved Successfully");
                                                onCloseCompanyContact();
                                            }
                                        },
                                        error: function (response) {
                                            isLoadingStores(false);
                                            toastr.error("Error: Failed To Save Contact " + response);
                                            onCloseCompanyContact();
                                        }
                                    });

                            }
                                //#endregion
                                //#region New Company Case 
                            else {
                                if (selectedCompanyContact().contactId() === undefined && isSavingNewCompanyContact() === true) {
                                    if (selectedCompanyContact().isDefaultContact()) {
                                        _.each(selectedStore().users(), function (user) {
                                            if (user.isDefaultContact()) {
                                                user.isDefaultContact(false);
                                            }
                                        });
                                    }
                                    selectedStore().users.splice(0, 0, selectedCompanyContact());
                                    if (selectedCompanyContact().bussinessAddressId() != undefined) {
                                        selectedCompanyContact().bussinessAddress(getAddressByAddressId(selectedCompanyContact().bussinessAddressId()));
                                    }
                                    if (selectedCompanyContact().shippingAddressId() != undefined) {
                                        selectedCompanyContact().shippingAddress(getAddressByAddressId(selectedCompanyContact().shippingAddressId()));
                                    }
                                    //updating selected contact rolename
                                    _.each(roles(), function (role) {
                                        if (role.roleId() == selectedCompanyContact().contactRoleId()) {
                                            selectedCompanyContact().roleName(role.roleName());
                                        }
                                    });
                                    newCompanyContacts.push(selectedCompanyContact());
                                    onCloseCompanyContact();
                                } else {
                                    //pushing item in editted CompanyContacts List
                                    if (selectedCompanyContact().contactId() != undefined) {
                                        var match = ko.utils.arrayFirst(edittedCompanyContacts(), function (item) {
                                            return (selectedCompanyContact().contactId() === item.contactId());
                                        });

                                        if (!match || match == null) {
                                            edittedCompanyContacts.push(selectedCompanyContact());
                                        }
                                    }
                                    //updating selected contact rolename
                                    _.each(roles(), function (role) {
                                        if (role.roleId() == selectedCompanyContact().contactRoleId()) {
                                            selectedCompanyContact().roleName(role.roleName());
                                        }
                                    });
                                    onCloseCompanyContact();
                                }
                            }
                            //#endregion

                        }
                    },
                    UserProfileImageFileLoadedCallback = function (file, data) {
                        selectedCompanyContact().image(data);
                        selectedCompanyContact().fileName(file.name);
                    },

                    getAddressByAddressId = function (addressId) {
                        var result = _.find(allCompanyAddressesList(), function (address) {
                            return address.addressId() == parseInt(addressId);
                        });
                        return result;
                    },
                    // #endregion

                //#region ___________ UTILITY FUNCTIONS ______

                onCreateNewStore = function() {
                    onCreateNewStoreForProspectOrCustomerScreen();
                },
                //Create New Store
                onCreateNewStoreForProspectOrCustomerScreen = function () {
                    var store = new model.Store();
                    selectedStore(store);
                    selectedStore().type(0);
                    isEditorVisible(true);
                    createNewTerritoryForProspectOrCustomerStore();
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

                //Get Base Data For New Company
                getBaseDataFornewCompany = function () {
                    dataservice.getBaseData({

                    }, {
                        success: function (data) {
                            if (data != null) {
                                systemUsers.removeAll();
                                addressCompanyTerritoriesFilter.removeAll();
                                contactCompanyTerritoriesFilter.removeAll();
                                addressTerritoryList.removeAll();
                                roles.removeAll();
                                registrationQuestions.removeAll();
                                allCompanyAddressesList.removeAll();
                                _.each(data.SystemUsers, function (item) {
                                    var systemUser = new model.SystemUser.Create(item);
                                    systemUsers.push(systemUser);
                                });

                                _.each(data.CompanyContactRoles, function (item) {
                                    var role = new model.Role.Create(item);
                                    roles.push(role);
                                });
                                _.each(data.RegistrationQuestions, function (item) {
                                    var registrationQuestion = new model.RegistrationQuestion.Create(item);
                                    registrationQuestions.push(registrationQuestion);
                                });

                                //Countries 
                                countries.removeAll();
                                ko.utils.arrayPushAll(countries(), data.Countries);
                                countries.valueHasMutated();
                                //States 
                                states.removeAll();
                                ko.utils.arrayPushAll(states(), data.States);
                                states.valueHasMutated();

                                    
                                //Sefault Sprite Image
                                //selectedStore().userDefinedSpriteImageSource(data.DefaultSpriteImageSource);
                                //selectedStore().userDefinedSpriteImageFileName("default.jpg");
                                //selectedStore().defaultSpriteImageSource(data.DefaultSpriteImageSource);
                                //selectedStore().customCSS(data.DefaultCompanyCss);
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Stores . Error: " + response);
                        }
                    });
                },

                //Filter States based on Country
                filterStates = ko.computed(function () {
                    if (selectedAddress() !== undefined && selectedAddress().country() !== undefined) {
                        filteredStates.removeAll();
                        _.each(states(), function (item) {
                            if (item.CountryId === selectedAddress().country()) {
                                filteredStates.push(item);
                            }
                        });
                    }
                }, this),
                //Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedStore().isValid()) {
                            selectedStore().errors.showAllMessages();
                            setValidationSummary(selectedStore());
                            flag = false;
                        }
                        //1- New saving company should have 1 address and 1 user
                        //2- if company is editting then company should have a 1 address and 1 user in database after saving
                        //1
                        if (!(newAddresses().length - deletedAddresses().length) > 1 || (selectedStore().addresses().length == 0 && newAddresses().length == 0 && deletedAddresses().length == 0)) {
                            errorList.push({ name: "At least one address required.", element: searchAddressFilter.domElement });
                            flag = false;
                        }
                        if (!(newCompanyContacts().length - deletedCompanyContacts().length) > 1 || (selectedStore().users().length == 0 && newCompanyContacts().length == 0 && deletedCompanyContacts().length == 0)) {
                            errorList.push({ name: "At least one user required.", element: searchCompanyContactFilter.domElement });
                            flag = false;
                        }

                        if (productPriorityRadioOption() === "1" && selectedItemsForOfferList().length === 0) {
                            errorList.push({ name: "At least One Product to Prioritize..", element: productError.domElement });
                            flag = false;
                        }
                        if (selectedStore().companyId() == undefined) {
                            var haveIsDefaultTerritory = false;
                            var haveIsBillingDefaultAddress = false;
                            var haveIsShippingDefaultAddress = false;
                            var haveIsDefaultAddress = false;
                            var haveIsDefaultUser = false;
                            _.each(selectedStore().companyTerritories(), function (territory) {
                                if (territory.isDefault()) {
                                    haveIsDefaultTerritory = true;
                                }
                            });
                            _.each(selectedStore().addresses(), function (address) {
                                if (address.isDefaultTerrorityBilling()) {
                                    haveIsBillingDefaultAddress = true;
                                }
                                if (address.isDefaultTerrorityShipping()) {
                                    haveIsShippingDefaultAddress = true;
                                }
                                if (address.isDefaultAddress()) {
                                    haveIsDefaultAddress = true;
                                }
                            });
                            _.each(selectedStore().users(), function (user) {
                                if (user.isDefaultContact()) {
                                    haveIsDefaultUser = true;
                                }
                            });
                            if (!haveIsDefaultTerritory) {
                                errorList.push({ name: "At least one default territory required.", element: searchCompanyTerritoryFilter.domElement });
                                flag = false;
                            }
                            if (!haveIsBillingDefaultAddress) {
                                errorList.push({ name: "At least one Territory Default Billing Address required.", element: searchAddressFilter.domElement });
                                flag = false;
                            }
                            if (!haveIsShippingDefaultAddress) {
                                errorList.push({ name: "At least one Territory Default Shipping Address required.", element: searchAddressFilter.domElement });
                                flag = false;
                            }
                            if (!haveIsDefaultAddress) {
                                errorList.push({ name: "At least one Company Default Address required.", element: searchAddressFilter.domElement });
                                flag = false;
                            }
                            if (!haveIsDefaultUser) {
                                errorList.push({ name: "At least one Default Company Contact required.", element: searchCompanyContactFilter.domElement });
                                flag = false;
                            }
                        }
                        return flag;
                    },
                //Save Store For prospect or customer
               //Save Store
                    saveStore = function (callback) {
                        if (doBeforeSave()) {
                            var storeToSave = model.Store().convertToServerData(selectedStore());
                            
                            //#region Company Territories
                            _.each(newCompanyTerritories(), function (territory) {
                                storeToSave.NewAddedCompanyTerritories.push(territory.convertToServerData());
                            });
                            
                            //#endregion
                           //#endregion
                            //#region Addresses
                            _.each(newAddresses(), function (address) {
                                storeToSave.NewAddedAddresses.push(address.convertToServerData());
                            });
                           
                            //#endregion
                            
                            // #region Company Contacts
                            _.each(newCompanyContacts(), function (companyContact) {
                                storeToSave.NewAddedCompanyContacts.push(companyContact.convertToServerData());
                            });
                            
                            _.each(selectedStore().mediaLibraries(), function (item) {
                                storeToSave.MediaLibraries.push(item.convertToServerData());
                            });

                            dataservice.saveStore(
                                storeToSave, {
                                    success: function (data) {
                                        //new store adding
                                        if (selectedStore().companyId() == undefined || selectedStore().companyId() == 0) {
                                            selectedStore().companyId(data.CompanyId);
                                            stores.splice(0, 0, selectedStore());
                                        }
                                        if (selectedStoreListView() && selectedStoreListView().companyId() == selectedStore().companyId()) {
                                            _.each(stores(), function (store) {
                                                if (store.companyId() == selectedStore().companyId()) {
                                                    store.name(selectedStore().name());
                                                    store.url(selectedStore().url());
                                                    store.status(selectedStore().status());
                                                    if (selectedStore().type() == "4") {
                                                        store.type("Retail Customer");
                                                    } else if (selectedStore().type() == "3") {
                                                        store.type("Corporate");
                                                    }
                                                }
                                            });
                                        }
                                        //selectedStore().storeId(data.StoreId);
                                        isStoreEditorVisible(false);
                                        isEditorVisible(false);
                                        toastr.success("Successfully save.");
                                        resetObservableArrays();
                                        if (callback && typeof callback === "function") {
                                            callback();
                                        }
                                    },
                                    error: function (response) {
                                        toastr.error("Failed to Update . Error: " + response);
                                        isStoreEditorVisible(false);
                                    }
                                });
                        }
                    },

                //#endregion

                //#region ____________ INITIALIZE ____________
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   pager(new pagination.Pagination({ PageSize: 5 }, customersForListView, getCustomers));
                   getCustomers();
                   getBaseDataFornewCompany();
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
                    onCreateNewStoreForProspectOrCustomerScreen: onCreateNewStoreForProspectOrCustomerScreen ,
                    closeEditDialog: closeEditDialog,
                    //selectStore: selectStore,
                    selectedStore: selectedStore,
                    systemUsers: systemUsers,
                    searchAddressFilter: searchAddressFilter,
                    
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
                    
                    isSavingNewAddress: isSavingNewAddress,
                    onCreateNewStore: onCreateNewStore,
                    //#region Company Contacts
                    selectedCompanyContact: selectedCompanyContact,
                    companyContactFilter: companyContactFilter,
                    deletedCompanyContacts: deletedCompanyContacts,
                    edittedCompanyContacts: edittedCompanyContacts,
                    newCompanyContacts: newCompanyContacts,
                    companyContactPager: companyContactPager,
                    searchCompanyContactFilter: searchCompanyContactFilter,
                    searchCompanyContact: searchCompanyContact,
                    companyContactFilterSelected: companyContactFilterSelected,
                    isSavingNewCompanyContact: isSavingNewCompanyContact,
                    templateToUseCompanyContacts: templateToUseCompanyContacts,
                    onCreateNewCompanyContact: onCreateNewCompanyContact,
                    onDeleteCompanyContact: onDeleteCompanyContact,
                    onEditCompanyContact: onEditCompanyContact,
                    onCloseCompanyContact: onCloseCompanyContact,
                    doBeforeSaveCompanyContact: doBeforeSaveCompanyContact,
                    onSaveCompanyContact: onSaveCompanyContact,
                    //#endregion Company Contacts
                    newCompanyTerritories: newCompanyTerritories,
                    roles: roles,
                    registrationQuestions: registrationQuestions,
                    countries: countries,
                    states: states,
                    addressCompanyTerritoriesFilter: addressCompanyTerritoriesFilter,
                    allCompanyAddressesList: allCompanyAddressesList,
                    filteredStates: filteredStates,
                    onSaveAddress: onSaveAddress,
                    selectedBussinessAddress: selectedBussinessAddress,
                    selectedBussinessAddressId: selectedBussinessAddressId,
                    selectedShippingAddress: selectedShippingAddress,
                    selectedShippingAddressId: selectedShippingAddressId,
                    selectBussinessAddress: selectBussinessAddress,
                    selectShippingAddress: selectShippingAddress,
                    bussinessAddresses: bussinessAddresses,
                    shippingAddresses: shippingAddresses,
                    errorList: errorList
                };
                //#endregion
            })()
        };
        return ist.crm.viewModel;
    });
