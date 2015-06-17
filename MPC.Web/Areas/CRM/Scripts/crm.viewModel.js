/*
    Module with the view model for the crm
*/
define("crm/crm.viewModel",
    ["jquery", "amplify", "ko", "crm/crm.dataservice", "crm/crm.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm, reportManager) {
        var ist = window.ist || {};
        ist.crm = {
            viewModel: (function () {
                var //View
                    view,
                //#region ___________ OBSERVABLES ____________
                    // Search filter 
                    searchFilter = ko.observable(),
                    // Pager for Prospect pagging
                    prospectPager = ko.observable(),
                    // Determines Company type
                    companyType = ko.observable(2),
                    orderPager = ko.observable(),
                    purchaseOrderPager = ko.observable(),
                    goodsReceivedNotePager = ko.observable(),
                    invoicePager = ko.observable(),
                    // Value holder of company DD
                    companyDdSelector = ko.observable(),
                    // Sort On
                    sortOn = ko.observable(1),
                    // Sort In Ascending
                    sortIsAsc = ko.observable(true),
                     // Orders list
                    ordersList = ko.observableArray(),
                    purchasesList = ko.observableArray(),
                    goodRecievedNotesList = ko.observableArray(),
                    sectionFlagList = ko.observableArray(),
                     // Invoices list
                    invoicesList = ko.observableArray(),
                    isOrderTab = ko.observable(false),
                    isPurchaseOrderTab = ko.observable(false),
                    isGoodsReceivedNoteTab = ko.observable(false),
                    isInvoiceTab = ko.observable(false),
                    isEditorVisible = ko.observable(false),
                    //Selected Store
                    selectedStore = ko.observable(),

                    //Store Image
                    storeImage = ko.observable(),
                    //New Uploaded Media File
                    newUploadedMediaFile = ko.observable(model.MediaLibrary()),
                    isBaseDataLoded = ko.observable(false),
                    //Is Loading stores
                    isLoadingStores = ko.observable(false),
                    //Selected Company Contact
                    companyContactEditorViewModel = new ist.ViewModel(model.CompanyContact),
                    selectedCompanyContact = companyContactEditorViewModel.itemForEditing,
                    //Check if screen is Prospect Or Customer Screen
                    isProspectOrCustomerScreen = ko.observable(false),
                    //Setting up computed method calling 
                    isUserAndAddressesTabOpened = ko.observable(false),
                    //selected Company
                    selectedCompany = ko.observable(),
                //#endregion

                //#region ___________ SUPPLIER SCREEN ____________
                //#region ____________OBSERVABLES____________
                 //Pager
                supplierpager = ko.observable(),
                //Is Loading suppliers
                isLoadingSuppliers = ko.observable(false),
                //Search Filter
                searchSupplierFilter = ko.observable(),
                //Sort On
                supplierSortOn = ko.observable(1),
                 //Sort In Ascending
                supplierSortIsAsc = ko.observable(true),
                selectedSupplier = ko.observable(),
                //#endregion

                //#region ____________OBSERVABLE ARRAYS____________
                //suppliers List
                suppliers = ko.observableArray([]),
                //#endregion

                //#region ____________SUPPLIERS LIST VIEW____________

                searchSuppliersByFilters = function() {
                    supplierpager().reset();
                    getSuppliers();
                },

                //GET Suppliers For Suppliers List View
                getSuppliers = function () {
                    isLoadingSuppliers(true);
                    dataservice.getSuppliers({
                        SearchString: searchSupplierFilter(),
                        PageSize: supplierpager().pageSize(),
                        PageNo: supplierpager().currentPage(),
                        SortBy: supplierSortOn(),
                        IsAsc: supplierSortIsAsc()
                    }, {
                        success: function (data) {
                            suppliers.removeAll();
                            if (data != null) {
                               
                                _.each(data.Companies, function (item) {
                                    var module = model.CrmSupplierListViewModel.Create(item);
                                    suppliers.push(module);
                                });
                                supplierpager().totalCount(data.RowCount);
                            }
                            isLoadingSuppliers(false);
                        },
                        error: function (response) {
                            isLoadingSuppliers(false);
                            toastr.error("Error: Failed To load Suppliers " + response);
                        }
                    });
                },
                //Template To Use
                templateToUseSupplier = function (store) {
                    return (store === selectedSupplier() ? 'itemSupplierTemplate' : 'itemSupplierTemplate');
                },
                resetSupplierFilterSection = function () {
                    searchSupplierFilter(undefined);
                    getSuppliers();
                },
                //#endregion

                //#endregion

                //#region ___________ OBSERVABLE ARRAYS ______
                // Customers array for list view
                customersForListView = ko.observableArray(),
                //system Users
                systemUsers = ko.observableArray([]),
                //Contact Company Territories Filter
                contactCompanyTerritoriesFilter = ko.observableArray([]),
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
                //Stores List For DropDown
                storesListForDropDown = ko.observableArray([]),
                //#endregion

                //#region ___________ LIST VIEW ______________
                // Gets customers for list view
                getCustomers = function () {
                    customersForListView.removeAll();
                    dataservice.getCustomersForListView({
                        SearchString: searchFilter(),
                        IsCustomer: companyType(),
                        PageSize: prospectPager().pageSize(),
                        PageNo:   prospectPager().currentPage() ,
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    },
                    {
                        success: function (data) {
                            if (data != null) {
                                _.each(data.Customers, function (customer) {
                                    var customerModel = new model.customerViewListModel.Create(customer);
                                    customersForListView.push(customerModel);
                                });
                                prospectPager().totalCount(data.RowCount);
                            }
                        },
                        error: function () {
                            toastr.error("Error: Failed To load Customers!");
                        }
                    });
                },

                // Search button handler
                filterHandler = function () {
                    prospectPager().reset();
                    getCustomers(companyType());
                },
                //  Reset button handler
                resetButtonHandler = function () {
                    searchFilter(null);
                    getCustomers();
                },
                onChangeCompany = function () {
                    var opp = companyDdSelector();
                    if (opp == 'All')
                        opp = 2;
                    else if (opp == 'Prospects')
                        opp = 0;
                    else if (opp == 'Customer')
                        opp = 1;
                    companyType(opp);
                    getCustomers();
                },
                //#endregion

                // #region _________C O M P A N Y   T E R R I T O R Y ____________

                    //Selected CompanyTerritory
                    selectedCompanyTerritory = ko.observable(),
                    //Deleted Company Territory 
                    deletedCompanyTerritories = ko.observableArray([]),
                    edittedCompanyTerritories = ko.observableArray([]),
                    newCompanyTerritories = ko.observableArray([]),
                    //Company Territory Pager
                    companyTerritoryPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                    //CompanyTerritory Search Filter
                    searchCompanyTerritoryFilter = ko.observable(),
                    //Search Company Territory
                    searchCompanyTerritory = function () {
                        if (isUserAndAddressesTabOpened() && selectedStore().companyId() != undefined && isEditorVisible()) {
                            dataservice.searchCompanyTerritory({
                                SearchFilter: searchCompanyTerritoryFilter(),
                                CompanyId: selectedStore().companyId(),
                                PageSize: companyTerritoryPager().pageSize(),
                                PageNo: companyTerritoryPager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc()
                            }, {
                                success: function (data) {
                                    var isStoreDirty = selectedStore().hasChanges();
                                    selectedStore().companyTerritories.removeAll();
                                    _.each(data.CompanyTerritories, function (companyTerritoryItem) {
                                        var companyTerritory = new model.CompanyTerritory.Create(companyTerritoryItem);
                                        selectedStore().companyTerritories.push(companyTerritory);
                                    });
                                    _.each(edittedCompanyTerritories(), function (item) {
                                        _.each(selectedStore().companyTerritories(), function (territoryItem) {
                                            if (item.territoryId() == territoryItem.territoryId()) {
                                                selectedStore().companyTerritories.remove(territoryItem);
                                            }
                                        });
                                    });
                                    _.each(deletedCompanyTerritories(), function (item) {
                                        _.each(selectedStore().companyTerritories(), function (territoryItem) {
                                            if (item.territoryId() == territoryItem.territoryId()) {
                                                selectedStore().companyTerritories.remove(territoryItem);
                                            }
                                        });
                                    });
                                    companyTerritoryPager().totalCount(data.RowCount);
                                    //check on client side, push all if new added work
                                    if (searchCompanyTerritoryFilter() == "" || searchCompanyTerritoryFilter() == undefined) {
                                        _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                                            selectedStore().companyTerritories.push(companyTerritoryItem);
                                        });
                                    }
                                    //check on client side, if filter is not null
                                    if (searchCompanyTerritoryFilter() != "" && searchCompanyTerritoryFilter() != undefined) {
                                        _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                                            if (companyTerritoryItem.territoryName().indexOf(searchCompanyTerritoryFilter()) != -1 || companyTerritoryItem.territoryCode().indexOf(searchCompanyTerritoryFilter()) != -1) {
                                                selectedStore().companyTerritories.push(companyTerritoryItem);
                                            }
                                        });
                                    }
                                    if (!isStoreDirty) {
                                        selectedStore().reset();
                                    }

                                },
                                error: function (response) {
                                    toastr.error("Failed To Load Company territories" + response, "", ist.toastrOptions);
                                }
                            });
                        }
                        else if (isUserAndAddressesTabOpened() && selectedStore().companyId() == undefined && isEditorVisible()) {
                            selectedStore().companyTerritories.removeAll();
                            //check on client side, push all if new added work
                            if (searchCompanyTerritoryFilter() == "" || searchCompanyTerritoryFilter() == undefined) {
                                _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                                    selectedStore().companyTerritories.push(companyTerritoryItem);
                                });
                            }
                            //check on client side, if filter is not null
                            if (searchCompanyTerritoryFilter() != "" && searchCompanyTerritoryFilter() != undefined) {
                                _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                                    if (companyTerritoryItem.territoryName().indexOf(searchCompanyTerritoryFilter()) != -1 || companyTerritoryItem.territoryCode().indexOf(searchCompanyTerritoryFilter()) != -1) {
                                        selectedStore().companyTerritories.push(companyTerritoryItem);
                                    }
                                });
                            }
                        }
// ReSharper disable UnusedLocals
                    },
                    companyTerritoryFilterSelected = ko.computed(function () {
// ReSharper restore UnusedLocals
                        if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined && selectedStore().companyId() !== undefined) {
                            searchCompanyTerritory();
                        }
                    }),
                    //isSavingNewCompanyTerritory
                    isSavingNewCompanyTerritory = ko.observable(false),
                    // Template Chooser For Rave Review
                    templateToUseCompanyTerritories = function (companyTerritory) {
                        return (companyTerritory === selectedCompanyTerritory() ? 'editCompanyTerritoryTemplate' : 'itemCompanyTerritoryTemplate');
                    },
                    //Create Company Territory
                    onCreateNewCompanyTerritory = function () {
                        var companyTerritory = new model.CompanyTerritory();
                        selectedCompanyTerritory(companyTerritory);
                        if (selectedStore().companyId() == undefined && newCompanyTerritories().length == 0) {
                            selectedCompanyTerritory().isDefault(true);
                        }
                        isSavingNewCompanyTerritory(true);
                        // For Change Detection
                        var territoryIsDefault = selectedCompanyTerritory().isDefault();
                        selectedCompanyTerritory().isDefault(!territoryIsDefault);
                        selectedCompanyTerritory().isDefault(territoryIsDefault || false);
                        view.showCompanyTerritoryDialog();
                    },
                    // Delete Company Territory
                    onDeleteCompanyTerritory = function (companyTerritory) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            //#region Db Saved Record Id > 0
                            if (companyTerritory.companyId() > 0 && companyTerritory.territoryId() > 0) {
                                //Check if company Territory is default and there exist any other territory to set its isDefualt flag to true
                                //Or Company Territory is not default ones
                                //if (!companyTerritory.isDefault() || companyTerritory.isDefault() && selectedStore().companyTerritories().length > 1) {
                                if (!companyTerritory.isDefault()) {
                                    dataservice.deleteCompanyTerritory({
                                        //companyTerritory: companyTerritory.convertToServerData()
                                        CompanyTerritoryId: companyTerritory.territoryId()
                                    }, {
                                        success: function (data) {
                                            if (data) {
                                                companyTerritoryPager().totalCount(companyTerritoryPager().totalCount() - 1);
                                                selectedStore().companyTerritories.remove(companyTerritory);
                                                toastr.success("Deleted Successfully");
                                                isLoadingStores(false);
                                                //Updating Drop downs
                                                _.each(addressCompanyTerritoriesFilter(), function (item) {
                                                    if (item.territoryId() == companyTerritory.territoryId()) {
                                                        addressCompanyTerritoriesFilter.remove(item);
                                                    }
                                                });
                                                _.each(contactCompanyTerritoriesFilter(), function (item) {
                                                    if (item.territoryId() == companyTerritory.territoryId()) {
                                                        contactCompanyTerritoriesFilter.remove(item);
                                                    }
                                                });
                                            } else {
                                                toastr.error("Territory can not be deleted. It might exist in Address or Contact", "", ist.toastrOptions);
                                            }
                                        },
                                        error: function (response) {
                                            isLoadingStores(false);
                                            toastr.error("Error: Failed To Delete Company Territory " + response, "", ist.toastrOptions);
                                        }
                                    });

                                } else {
                                    toastr.error("Make New Default territory first", "", ist.toastrOptions);
                                }
                            }
                                //#endregion
                                //#region Else Company territry is newly created
                            else {
                                companyTerritoryPager().totalCount(companyTerritoryPager().totalCount() - 1);
                                if (companyTerritory.isDefault() && selectedStore().companyTerritories().length == 1) {
                                    toastr.error("Make New Default territory first", "", ist.toastrOptions);

                                } else {
                                    var flag = true;
                                    if (newAddresses != undefined) {
                                        _.each(newAddresses(), function (address) {
                                            if (address.territoryId() == companyTerritory.territoryId()) {
                                                toastr.error("Error: Territory can not deleted as it exist in new created address", "", ist.toastrOptions);
                                                flag = false;
                                            }
                                        });
                                    }
                                    if (newCompanyContacts != undefined) {
                                        _.each(newCompanyContacts(), function (contact) {
                                            if (contact.territoryId() == companyTerritory.territoryId()) {
                                                toastr.error("Error: Territory can not deleted as it exist in new created contact", "", ist.toastrOptions);
                                                flag = false;
                                            }
                                        });
                                    }
                                    if (flag) {
                                        _.each(newCompanyTerritories(), function (item) {
                                            if (item.territoryId() == companyTerritory.territoryId()) {
                                                newCompanyTerritories.remove(companyTerritory);
                                            }
                                        });

                                        addressCompanyTerritoriesFilter.remove(companyTerritory);
                                        contactCompanyTerritoriesFilter.remove(companyTerritory);
                                        selectedStore().companyTerritories.remove(companyTerritory);
                                        if (selectedStore().companyTerritories()[0] != undefined) {
                                            selectedStore().companyTerritories()[0].isDefault(true);
                                            if (selectedStore().companyTerritories()[0].territoryId() > 0) {
                                                edittedCompanyTerritories.push(selectedStore().companyTerritories()[0]);
                                            }
                                        }

                                    } else { 
                                        toastr.error("Territory Exist in Address Or Contact. Please delete them first", "", ist.toastrOptions);
                                    }
                                }

                            }
                            //#endregion
                            view.hideCompanyTerritoryDialog();
                        });
                        confirmation.show();

                        return;
                    },
                    onEditCompanyTerritory = function (companyTerritory) {
                        selectedCompanyTerritory(companyTerritory);
                        isSavingNewCompanyTerritory(false);
                        selectedCompanyTerritory().reset();
                        view.showCompanyTerritoryDialog();
                    },
                    onCloseCompanyTerritory = function () {
                        selectedCompanyTerritory(undefined);
                        view.hideCompanyTerritoryDialog();
                        isSavingNewCompanyTerritory(false);
                    },
                    //Do Before Save Company Territory
                    doBeforeSaveCompanyTerritory = function () {
                        var flag = true;
                        if (!selectedCompanyTerritory().isValid()) {
                            selectedCompanyTerritory().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    companyTerritoryCounter = -1,
                    addCompanyTerritoryCount = function () {
                        companyTerritoryCounter = (companyTerritoryCounter - 1);
                    },
                    onSaveCompanyTerritory = function () {
                        if (doBeforeSaveCompanyTerritory()) {
                            //#region If Store is Editting, CompanyId > 0
                            var territory = selectedCompanyTerritory().convertToServerData();
                            _.each(selectedCompanyTerritory().scopeVariables(), function (item) {
                                territory.ScopeVariables.push(item.convertToServerData(item));
                            });


                            if (selectedStore().companyId() > 0) {
                                selectedCompanyTerritory().companyId(selectedStore().companyId());
                                dataservice.saveCompanyTerritory(
                                    territory,
                                    {
                                        success: function (data) {
                                            if (data) {
                                                companyTerritoryPager().totalCount(companyTerritoryPager().totalCount() + 1);
                                                var savedTerritory = model.CompanyTerritory.Create(data);
                                                if (selectedCompanyTerritory().territoryId() <= 0 || selectedCompanyTerritory().territoryId() == undefined) {
                                                    selectedStore().companyTerritories.splice(0, 0, savedTerritory);
                                                    //Add territory in address drop down to use in saving address
                                                    addressCompanyTerritoriesFilter.push(savedTerritory);
                                                    contactCompanyTerritoriesFilter.push(savedTerritory);
                                                }

                                                if (savedTerritory.isDefault()) {
                                                    _.each(selectedStore().companyTerritories(), function (item) {
                                                        if (item.territoryId() != data.TerritoryId) {
                                                            if (item.isDefault() == true) {
                                                                item.isDefault(false);
                                                            }
                                                        }

                                                    });
                                                }
                                                toastr.success("Saved Successfully");
                                                view.hideCompanyTerritoryDialog();
                                            }
                                        },
                                        error: function (response) {
                                            isLoadingStores(false);
                                            toastr.error("Error: Failed To Save Company Territory " + response, "", ist.toastrOptions);
                                        }
                                    });

                            }
                                //#endregion
                            else {
                                companyTerritoryPager().totalCount(companyTerritoryPager().totalCount() + 1);
                                //Is Saving New Territory
                                if (selectedCompanyTerritory().territoryId() < 0) {
                                    if (selectedCompanyTerritory().isDefault()) {
                                        _.each(newCompanyTerritories(), function (territory) {
                                            if (territory.isDefault() && territory.territoryId() != selectedCompanyTerritory().territoryId()) {
                                                territory.isDefault(false);
                                            }
                                        });
                                        _.each(selectedStore().companyTerritories(), function (territory) {
                                            if (territory.isDefault() && territory.territoryId() != selectedCompanyTerritory().territoryId()) {
                                                territory.isDefault(false);
                                            }
                                        });
                                    }
                                }
                                if (selectedCompanyTerritory().territoryId() === undefined && isSavingNewCompanyTerritory() === true) {

                                    selectedCompanyTerritory().territoryId(companyTerritoryCounter);
                                    addCompanyTerritoryCount();
                                    if (selectedCompanyTerritory().isDefault()) {
                                        _.each(newCompanyTerritories(), function (territory) {
                                            if (territory.isDefault()) {
                                                territory.isDefault(false);
                                            }
                                        });
                                        _.each(selectedStore().companyTerritories(), function (territory) {
                                            if (territory.isDefault()) {
                                                territory.isDefault(false);
                                            }
                                        });
                                    }
                                    selectedStore().companyTerritories.splice(0, 0, selectedCompanyTerritory());
                                    newCompanyTerritories.splice(0, 0, selectedCompanyTerritory());
                                    //Add territory in address drop down to use in saving address
                                    addressCompanyTerritoriesFilter.push(selectedCompanyTerritory());
                                    contactCompanyTerritoriesFilter.push(selectedCompanyTerritory());

                                } else {
                                    //pushing item in editted Company Territories List
                                    if (selectedCompanyTerritory().companyId() != undefined) {
                                        var match = ko.utils.arrayFirst(edittedCompanyTerritories(), function (item) {
                                            return (selectedCompanyTerritory().territoryId() === item.territoryId());
                                        });

                                        if (!match || match == null) {
                                            edittedCompanyTerritories.push(selectedCompanyTerritory());
                                        }
                                    }
                                }

                                view.hideCompanyTerritoryDialog();
                            }

                        }
                    },
                    // #endregion __________________ C O M P A N Y   T E R R I T O R Y __________________

                // #region _________A D D R E S S E S __________________________

                //Selected AddresssearchCompanyTerritory
                addressEditorViewModel = new ist.ViewModel(model.Address),
                selectedAddress = addressEditorViewModel.itemForEditing,
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
                        });
                    }
                }),
                selectBussinessAddress = ko.computed(function () {
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().addressId() != undefined) {
                    }

                    if (selectedCompanyContact() != undefined && selectedCompanyContact().bussinessAddressId() != undefined) {
                        var contactHasChanges = selectedCompanyContact().hasChanges();
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedCompanyContact().bussinessAddressId()) {
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
                                    selectedBussinessAddress().stateName(item.stateName());
                                }
                            }
                        });
                        if (!contactHasChanges) {
                            selectedCompanyContact().reset();
                        }
                    }
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().bussinessAddressId() == undefined) {
                        selectedBussinessAddress(undefined);
                    }
                }),
                selectShippingAddress = ko.computed(function () {

                    if (selectedCompanyContact() != undefined && selectedCompanyContact().shippingAddressId() != undefined) {
                        var contactHasChanges = selectedCompanyContact().hasChanges();
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedCompanyContact().shippingAddressId()) {
                                selectedShippingAddress(item);
                                if (item.city() == null) {
                                    selectedShippingAddress().city(undefined);
                                }
                                if (item.state() == null) {
                                    selectedShippingAddress().state(undefined);
                                }
                                if (selectedCompanyContact() != undefined) {
                                    selectedCompanyContact().shippingAddressId(item.addressId());
                                    selectedShippingAddress().stateName(item.stateName());
                                }
                            }
                        });
                        if (!contactHasChanges) {
                            selectedCompanyContact().reset();
                        }
                    }
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().shippingAddressId() == undefined) {
                        selectedShippingAddress(undefined);
                    }

                }),
                //Get State Name By State Id
                //Method to be called on user and addresses tab selection
                userAndAddressesTabSelected = function () {
                    isUserAndAddressesTabOpened(true);
                },
                //Address Pager
                addressPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Contact Company Pager
                contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),

                //Address Search Filter
                searchAddressFilter = ko.observable(),
                //Search Address
                searchAddress = function () {
                    if (isUserAndAddressesTabOpened() && selectedStore().companyId() != undefined && isEditorVisible()) {
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
                                var isStoreDirty = selectedStore().hasChanges();
                                selectedStore().addresses.removeAll();
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
                                addressPager().totalCount(data.RowCount);
                                //check on client side, push all if new added work
                                if (searchAddressFilter() == "" || searchAddressFilter() == undefined) {
                                    if (addressTerritoryFilter() != undefined) {
                                        _.each(newAddresses(), function (addressItem) {
                                            if (addressItem.territoryId() == addressTerritoryFilter()) {
                                                selectedStore().addresses.push(addressItem);
                                            }
                                        });
                                    } else {
                                        _.each(newAddresses(), function (addressItem) {
                                            selectedStore().addresses.push(addressItem);
                                        });
                                    }

                                }

                                //check on client side, if filter is not null
                                if (searchAddressFilter() != "" && searchAddressFilter() != undefined) {
                                    if (addressTerritoryFilter() == undefined) {
                                        _.each(newAddresses(), function (addressItem) {
                                            if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                                selectedStore().addresses.push(addressItem);
                                            }
                                        });
                                    } else {
                                        _.each(newAddresses(), function (addressItem) {
                                            if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                                if (addressItem.territoryId() == addressTerritoryFilter()) {
                                                    selectedStore().addresses.push(addressItem);
                                                }

                                            }
                                        });
                                    }
                                }
                                if (!isStoreDirty) {
                                    selectedStore().reset();
                                }

                            },
                            error: function (response) {
                                toastr.error("Failed To Load Addresses" + response, "", ist.toastrOptions);
                            }
                        });
                    }
                    else if (isUserAndAddressesTabOpened() && selectedStore().companyId() == undefined && isEditorVisible()) {
                        selectedStore().addresses.removeAll();
                        //check on client side, push all if new added work
                        if (searchAddressFilter() == "" || searchAddressFilter() == undefined) {
                            if (addressTerritoryFilter() != undefined) {
                                _.each(newAddresses(), function (addressItem) {
                                    if (addressItem.territoryId() == addressTerritoryFilter()) {
                                        selectedStore().addresses.push(addressItem);
                                    }
                                });
                            } else {
                                _.each(newAddresses(), function (addressItem) {
                                    selectedStore().addresses.push(addressItem);
                                });
                            }

                        }

                        //check on client side, if filter is not null
                        if (searchAddressFilter() != "" && searchAddressFilter() != undefined) {
                            if (addressTerritoryFilter() == undefined) {
                                _.each(newAddresses(), function (addressItem) {
                                    if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                        selectedStore().addresses.push(addressItem);
                                    }
                                });
                            } else {
                                _.each(newAddresses(), function (addressItem) {
                                    if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                        if (addressItem.territoryId() == addressTerritoryFilter()) {
                                            selectedStore().addresses.push(addressItem);
                                        }

                                    }
                                });
                            }
                        }
                    }
                },
                addressTerritoryFilterSelected = ko.computed(function () {
                    if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined) {
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
                    if (selectedStore().companyId() == undefined) {//selectedStore().type() == 4 && 
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
                    //Updating Case
                    if (selectedStore().companyId() != undefined) {//selectedStore().type() == 4 && 
                        selectedAddress().territoryId(selectedStore().companyTerritories()[0].territoryId());
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
                    //Updating Case
                    if (selectedStore().companyId() != undefined) {
                        selectedAddress().territoryId(selectedStore().companyTerritories()[0].territoryId());
                    }
                    view.showAddressDialog();
                },
                // Delete Address
                onDeleteAddress = function (address) {
                    if (address.isDefaultTerrorityBilling() || address.isDefaultTerrorityShipping() || address.isDefaultAddress()) {
                        toastr.error("Address can not be deleted as it is either Default Billing/ Default Shipping or is default address", "", ist.toastrOptions);
                        return;
                    } else {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            //#region Db Saved Record Id > 0
                            if (address.addressId() > 0) {
                                if (address.companyId() > 0 && address.addressId() > 0) {

                                    if (!address.isDefaultTerrorityBilling() || !address.isDefaultTerrorityShipping() || !address.isDefaultAddress()) {
                                        dataservice.deleteCompanyAddress({
                                            AddressId: address.addressId()
                                        }, {
                                            success: function (data) {
                                                if (data) {
                                                        _.each(selectedStore().addresses(), function (item) {
                                                            if (item.addressId() == address.addressId() ) {
                                                                selectedStore().addresses.remove(item);
                                                            }
                                                        });
                                                      
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
                                                    toastr.error("Address can not be deleted. It might exist in Contact", "", ist.toastrOptions);
                                                }
                                            },
                                            error: function (response) {
                                                isLoadingStores(false);
                                                toastr.error("Error: Failed To Delete Address " + response, "", ist.toastrOptions);
                                            }
                                        });
                                    } else {
                                        toastr.error("Address can not be deleted as it contains default values", "", ist.toastrOptions);
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
                                var selectedObj = null;
                                if (flag) {
                                    _.each(selectedStore().addresses(), function (item) {
                                        if (item.addressId() == address.addressId() ) {
                                            selectedObj = item;
                                        }
                                    });
                                    selectedStore().addresses.remove(selectedObj);
                                } else {
                                    toastr.error("Address can not be deleted as it exist in User", "", ist.toastrOptions);
                                }

                            }
                            //#endregion
                            view.hideAddressDialog();
                        });
                        confirmation.show();

                        return;
                    }
                },
                onEditAddress = function (address) {
                    addressEditorViewModel.selectItem(address);
                    isSavingNewAddress(false);
                    selectedAddress().reset();
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

                            var address = selectedAddress().convertToServerData();

                            dataservice.saveAddress(
                                address,
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
                                                    if (item.isDefaultTerrorityBilling() == true && item.territoryId() == savedAddress.territoryId()) {
                                                        item.isDefaultTerrorityBilling(false);
                                                    }
                                                }
                                                if (savedAddress.isDefaultTerrorityShipping()) {
                                                    if (item.isDefaultTerrorityShipping() == true && item.territoryId() == savedAddress.territoryId()) {
                                                        item.isDefaultTerrorityShipping(false);
                                                    }
                                                }
                                                if (savedAddress.isDefaultAddress()) {
                                                    if (item.isDefaultAddress() == true && item.territoryId() == savedAddress.territoryId()) {
                                                        item.isDefaultAddress(false);
                                                    }
                                                }
                                            });
                                            //Adding saved address in address lists on client side
                                            _.each(allCompanyAddressesList(), function (address) {
                                                if (address.addressId() == savedAddress.addressId()) {
                                                    bussinessAddresses.remove(address);
                                                    shippingAddresses.remove(address);
                                                    allCompanyAddressesList.remove(address);
                                                }
                                            });
                                            bussinessAddresses.push(savedAddress);
                                            shippingAddresses.push(savedAddress);
                                            allCompanyAddressesList.push(savedAddress);

                                            searchAddress();
                                            toastr.success("Saved Successfully");
                                        }
                                    },
                                    error: function (response) {
                                        isLoadingStores(false);
                                        toastr.error("Error: Failed To Save Address " + response, "", ist.toastrOptions);
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
                                //saving state and country name on client side 
                                _.each(states(), function (state) {
                                    if (selectedAddress().state() == state.StateId) {
                                        selectedAddress().stateName(state.StateName);
                                    }
                                });
                                _.each(countries(), function (country) {
                                    if (selectedAddress().country() == country.CountryId) {
                                        selectedAddress().countryName(country.CountryName);
                                    }
                                });
                                selectedStore().addresses.splice(0, 0, selectedAddress());
                                //selectedAddress().territoryName(getTerritoryByTerritoryId(selectedAddress().territoryId()).territoryName());
                                //selectedAddress().territory(getTerritoryByTerritoryId(selectedAddress().territoryId()));
                                newAddresses.splice(0, 0, selectedAddress());

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

                // #region _________C O M P A N  Y   C O N T A C T _________________

                //companyContactFilter
                companyContactFilter = ko.observable(),
                contactCompanyTerritoryFilter = ko.observable(),
                //Deleted Company Contact 
                deletedCompanyContacts = ko.observableArray([]),
                edittedCompanyContacts = ko.observableArray([]),
                newCompanyContacts = ko.observableArray([]),
                //Company Contact  Pager
                //contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Company Contact Search Filter
                searchCompanyContactFilter = ko.observable(),
                //Search Company Contact        
                searchCompanyContact = function () {
                    contactCompanyPager().reset();
                    if (isUserAndAddressesTabOpened() && selectedStore().companyId() != undefined && isEditorVisible()) {
                        dataservice.searchCompanyContact({
                            SearchFilter: searchCompanyContactFilter(),
                            CompanyId: selectedStore().companyId(),
                            TerritoryId: contactCompanyTerritoryFilter(),
                            PageSize: contactCompanyPager().pageSize(),
                            PageNo: contactCompanyPager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                var isStoreDirty = selectedStore().hasChanges();
                                selectedStore().users.removeAll();
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
                                contactCompanyPager().totalCount(data.RowCount);
                                //check on client side, push all if new added work
                                if (searchCompanyContactFilter() == "" || searchCompanyContactFilter() == undefined) {
                                    if (contactCompanyTerritoryFilter() != undefined) {
                                        _.each(newCompanyContacts(), function (companyContactItem) {
                                            if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                                selectedStore().users.push(companyContactItem);
                                            }
                                        });
                                    } else {
                                        _.each(newCompanyContacts(), function (companyContactItem) {
                                            selectedStore().users.push(companyContactItem);
                                        });
                                    }
                                }
                                //check on client side, if filter is not null
                                if (searchCompanyContactFilter() != "" && searchCompanyContactFilter() != undefined) {
                                    if (contactCompanyTerritoryFilter() == undefined) {
                                        _.each(newCompanyContacts(), function (companyContactItem) {
                                            if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                                selectedStore().users.push(companyContactItem);
                                            }
                                        });
                                    } else {
                                        _.each(newCompanyContacts(), function (companyContactItem) {
                                            if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                                if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                                    selectedStore().users.push(companyContactItem);
                                                }
                                            }
                                        });
                                    }

                                }
                                if (!isStoreDirty) {
                                    selectedStore().reset();
                                }

                            },
                            error: function (response) {
                                toastr.error("Failed To Load Users" + response, "", ist.toastrOptions);
                            }
                        });
                    }
                    else if (isUserAndAddressesTabOpened() && selectedStore().companyId() == undefined && isEditorVisible()) {
                        selectedStore().users.removeAll();
                        //check on client side, push all if new added work
                        if (searchCompanyContactFilter() == "" || searchCompanyContactFilter() == undefined) {
                            if (contactCompanyTerritoryFilter() != undefined) {
                                _.each(newCompanyContacts(), function (companyContactItem) {
                                    if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                        selectedStore().users.push(companyContactItem);
                                    }
                                });
                            } else {
                                _.each(newCompanyContacts(), function (companyContactItem) {
                                    selectedStore().users.push(companyContactItem);
                                });
                            }
                        }
                        //check on client side, if filter is not null
                        if (searchCompanyContactFilter() != "" && searchCompanyContactFilter() != undefined) {
                            if (contactCompanyTerritoryFilter() == undefined) {
                                _.each(newCompanyContacts(), function (companyContactItem) {
                                    if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                        selectedStore().users.push(companyContactItem);
                                    }
                                });
                            } else {
                                _.each(newCompanyContacts(), function (companyContactItem) {
                                    if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                        if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                            selectedStore().users.push(companyContactItem);
                                        }
                                    }
                                });
                            }

                        }
                    }

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
                    selectedShippingAddressId(undefined);
                    selectedCompanyContactEmail(undefined);
                    isSavingNewCompanyContact(true);
                    selectedCompanyContact(user);
                    selectedCompanyContact().isWebAccess(true);
                    selectedCompanyContact().isPlaceOrder(true);
                    selectedCompanyContact().contactId(newSavingCompanyContactIdCount);
                    addCompanyContactId();
                    // if (selectedStore().type() == 4) {
                    if (newAddresses != undefined && newAddresses().length == 0) {
                        if (newCompanyTerritories.length > 0) {
                            selectedCompanyContact().territoryId(newCompanyTerritories()[0].territoryId());
                        }
                    }
                    if (selectedStore().companyId() > 0) {
                        if (selectedStore().companyTerritories().length > 0) {
                            selectedCompanyContact().territoryId(selectedStore().companyTerritories()[0].territoryId());
                        }
                    }
                    //Updating role of user as "user", if is retail store
                    _.each(roles(), function (role) {
                        if (role.roleName().toLowerCase() == "user") {
                            selectedCompanyContact().contactRoleId(role.roleId());
                        }
                    });
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
                        
                        //select isDefaultContact for the very first contact by defaault
                        if (newCompanyContacts().length == 0) {
                            selectedCompanyContact().isDefaultContact(true);
                        }

                    }
                    
                    view.showCompanyContactDialog();
                },

            // Delete CompanyContact
            onDeleteCompanyContact = function (companyContact) { //CompanyContact
                if (companyContact.isDefaultContact()) {
                    toastr.error("Default Contact Cannot be deleted", "", ist.toastrOptions);
                    return;
                }
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    //#region Db Saved Record Id > 0
                    if (companyContact.contactId() > 0) {

                        if (companyContact.companyId() > 0 && companyContact.contactId() > 0) {
                            dataservice.deleteCompanyContact({
                                CompanyContactId: companyContact.contactId()
                            }, {
                                success: function (data) {
                                    if (data) {
                                        var contact = selectedStore().users.find(function(user) {
                                            return user.contactId() === companyContact.contactId();
                                        });
                                        if (contact) {
                                            selectedStore().users.remove(contact);
                                            toastr.success("Deleted Successfully");
                                        }
                                        
                                        isLoadingStores(false);
                                    } else {
                                        toastr.error("Contact can not be deleted", "", ist.toastrOptions);
                                    }
                                },
                                error: function (response) {
                                    isLoadingStores(false);
                                    toastr.error("Error: Failed To Delete Company Contact " + response, "", ist.toastrOptions);
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
                    view.hideCompanyContactDialog();

                });
                confirmation.show();
                return;
            },
            selectedCompanyContactEmail = ko.observable(),
            onEditCompanyContact = function (companyContact) {
                //selectedCompanyContact(companyContact);
                companyContactEditorViewModel.selectItem(companyContact);
                selectedCompanyContactEmail(companyContact.email());
                selectedCompanyContact().reset();
                isSavingNewCompanyContact(false);
                view.showCompanyContactDialog();
            },
            closeCompanyContact = function () {
                selectedBussinessAddressId(undefined);
                view.hideCompanyContactDialog();
                isSavingNewCompanyContact(false);
            },
            onCloseCompanyContact = function () {
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
            newSavingCompanyContactIdCount = -1,
            //Add Company Contact Id
            addCompanyContactId = function () {
                newSavingCompanyContactIdCount = newSavingCompanyContactIdCount - 1;
            },
            onSaveCompanyContact = function () {
                if (doBeforeSaveCompanyContact()) {
                    //#region Editting Company Case companyid > 0
                    if (selectedStore().companyId() > 0) {

                        selectedCompanyContact().companyId(selectedStore().companyId());
                        var companyContact = selectedCompanyContact().convertToServerData();
                        _.each(selectedCompanyContact().companyContactVariables(), function (contactVariable) {
                            companyContact.ScopVariables.push(contactVariable.convertToServerData(contactVariable));
                        });
                        dataservice.saveCompanyContact(
                            companyContact,
                            {
                                success: function (data) {
                                    if (data) {
                                        var savedCompanyContact = model.CompanyContact.Create(data);
                                        //updating selected contact rolename
                                        _.each(roles(), function (role) {
                                            if (role.roleId() == selectedCompanyContact().contactRoleId()) {
                                                savedCompanyContact.roleName(role.roleName());
                                                data.RoleName = role.roleName();
                                            }
                                        });
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
                                        else {
                                            selectedCompanyContact(savedCompanyContact);
                                            //_.each(selectedStore().users(), function (user) {
                                            //    if (user.contactId() == savedCompanyContact.contactId()) {
                                            //        user.roleName(savedCompanyContact.roleName());
                                            //    }
                                            //});
                                            var count = 0;
                                            _.each(selectedStore().users(), function (user) {
                                                if (user.contactId() == savedCompanyContact.contactId()) {
                                                    var totalCount = contactCompanyPager().totalCount();
                                                    selectedStore().users.remove(user);
                                                    selectedStore().users.splice(count, 0, savedCompanyContact);
                                                    contactCompanyPager().totalCount(totalCount);
                                                }
                                                count = count + 1;
                                            });
                                        }
                                        if (savedCompanyContact.isDefaultContact()) {
                                            selectedCompany().defaultContactEmail(savedCompanyContact.email());
                                            selectedCompany().defaultContact(savedCompanyContact.firstName() + " " + savedCompanyContact.lastName());
                                        }
                                        
                                        toastr.success("Saved Successfully");
                                        closeCompanyContact();
                                    }
                                },
                                error: function (response) {
                                    isLoadingStores(false);
                                    toastr.error("Error: Failed To Save Contact " + response, "", ist.toastrOptions);
                                    if (response == "Duplicate Email/Username are not allowed") {
                                        selectedCompanyContact().email(selectedCompanyContactEmail());
                                    } else {
                                        onCloseCompanyContact();
                                    }
                                }
                            });
                    }
                        //#endregion
                        //#region New Company Case 
                    else {
                        if (selectedCompanyContact().contactId() < 0 && isSavingNewCompanyContact() === true) {
                            if (!findEmailDuplicatesInCompanyContacts()) {
                                if (selectedCompanyContact().isDefaultContact()) {
                                    _.each(selectedStore().users(), function (user) {
                                        if (user.isDefaultContact()) {
                                            user.isDefaultContact(false);
                                        }
                                    });
                                }
                                selectedStore().users.splice(0, 0, selectedCompanyContact());
                                //Editorial view model

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
                                addCompanyContactId();
                                closeCompanyContact();
                            }

                        } else {
                            if (!findEmailDuplicatesInCompanyContacts()) {
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
                    }
                    //#endregion
                }
            },
            //Method to find email duplicates, returns True is match found else return false
            findEmailDuplicatesInCompanyContacts = function () {
                var flag = false;
                _.each(newCompanyContacts(), function (companyContact) {
                    if (companyContact.email() == selectedCompanyContact().email() && companyContact.contactId() != selectedCompanyContact().contactId()) {
                        selectedCompanyContact().email(undefined);
                        toastr.error('Duplicate Email/Username are not allowed', "", ist.toastrOptions);
                        flag = true;
                    }
                });
                return flag;
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
                openReport = function (isFromEditor) {
                    if (isProspectOrCustomerScreen()) {
                        reportManager.show(ist.reportCategoryEnums.CRM, isFromEditor == true ? true : false, 0);
                    } else {
                        reportManager.show(ist.reportCategoryEnums.Suppliers, isFromEditor == true ? true : false, 0);
                    }
                },
                onCreateNewStore = function () {
                    resetObservableArrays();
                    var store = new model.Store();
                    var currentdate = new Date();
                    store.accountOpenDate(currentdate);
                    selectedStore(store);
                    if (isProspectOrCustomerScreen()) {
                        selectedStore().type(0);
                        createNewTerritoryForProspectOrCustomerStore();
                    }
                    else {
                        selectedStore().type(2);
                        selectedStore().isCustomer(2);
                        createNewTerritoryForProspectOrCustomerStore();
                    }
                    $('#crmTabsId li:first-child a').tab('show');
                    $('#crmTabsId li:eq(0) a').tab('show');
                    isEditorVisible(true);
                },
                //function to create new default territory for Prospect Or customer screen
                createNewTerritoryForProspectOrCustomerStore = function () {
                    //selectedStore is new
                    //new CompanyTerritories have no record
                    if (selectedStore() != undefined && newCompanyTerritories != undefined && selectedStore().type() != undefined
                        && selectedStore().companyId() == undefined && newCompanyTerritories().length == 0) { //&& selectedStore().type() == 0
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
                // On Delete Store Permanently
                onDeletePermanent = function () {
                    confirmation.afterProceed(function () {
                        deleteCompanyPermanently(selectedStore().companyId());
                    });
                    confirmation.show();
                },
                // Get Company By Id
                getCompanyByIdFromListView = function (id) {
                    return stores.find(function (store) {
                        return store.companyId() === id;
                    });
                },
                // Delete Company Permanently
                deleteCompanyPermanently = function (id) {
                dataservice.deleteCompanyPermanent({ CompanyId: id }, {
                    success: function () {
                        toastr.success("Deleted successfully!");
                        isEditorVisible(false);
                        if (selectedStore()) {
                            var store = getCompanyByIdFromListView(selectedStore().companyId());
                            if (store) {
                                stores.remove(store);
                            }
                        }
                        //resetStoreEditor();
                    },
                    error: function (response) {
                        toastr.error("Failed to delete store. Error: " + response, "", ist.toastrOptions);
                    }
                });
            },
                //Close Edit Dialog
                closeEditDialog = function () {
                    var companyIdFromDashboard = $('#CompanyId').val();
                    if (companyIdFromDashboard !== '0' && companyIdFromDashboard != undefined) {
                        getCustomers();
                    }
                    isEditorVisible(false);
                    isOrderTab(false);
                },

                //Get Store For Editting
                getStoreForEditting = function (id) {
                    selectedStore(model.Store());
                    dataservice.getStoreById({
                        companyId: id
                    }, {
                        success: function (data) {
                            if (data != null) {
                                selectedStore().addresses.removeAll();
                                selectedStore(model.Store.Create(data.Company));
                                selectedStore().type(data.Company.IsCustomer);
                                addressPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().addresses, searchAddress));
                                contactCompanyPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().users, searchCompanyContact));
                                addressPager().totalCount(data.Company.CompanyAddressesCount);
                                contactCompanyPager().totalCount(data.Company.CompanyContactCount);
                                storeImage(data.ImageSource);

                                //Media Library
                                _.each(data.Company.MediaLibraries, function (item) {
                                    selectedStore().mediaLibraries.push(model.MediaLibrary.Create(item));
                                });
                                        $('#idCompanyimage')
                               .load(function () {
                                 
                               })
                               .error(function () {
                                   $("#idCompanyimage").attr("src", "/mis/Content/Images/imageplaceholder.png");
                                  
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
                    if (isProspectOrCustomerScreen()) {
                        getStoreForEditting(item.id());
                        getBaseData(item.id() || item);
                    }
                    else {
                        getStoreForEditting(item.companyId());
                        getBaseData(item.companyId());
                    }
                    isUserAndAddressesTabOpened(true);
                },

                //On Edit Click Of Store
                onEditItem = function (item) {
                    selectedCompany(item);
                    openEditDialog(item);
                    $('#crmTabsId li:first-child a').tab('show');
                    $('#crmTabsId li:eq(0) a').tab('show');
                    sharedNavigationVm.initialize(selectedStore, function (saveCallback) { saveStore(saveCallback); });
                },

                //Get Base Data For New Company
                getBaseDataFornewCompany = function () {
                    dataservice.getBaseDataFornewCompany({

                    }, {
                        success: function (data) {
                            if (data != null) {
                                systemUsers.removeAll();
                                addressCompanyTerritoriesFilter.removeAll();
                                contactCompanyTerritoriesFilter.removeAll();
                                addressTerritoryList.removeAll();
                                roles.removeAll();
                                sectionFlagList.removeAll();
                                registrationQuestions.removeAll();
                                allCompanyAddressesList.removeAll();

                                _.each(data.SectionFlags, function (flag) {
                                    sectionFlagList.push(flag);
                                });

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
                                //stores List For DropDown 
                                storesListForDropDown.removeAll();
                                ko.utils.arrayPushAll(storesListForDropDown(), data.StoresListDropDown);
                                storesListForDropDown.valueHasMutated();
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Stores . Error: " + response);
                        }
                    });
                },
                // Set Validation Summary
                setValidationSummary = function (selectedItem) {
                    errorList.removeAll();
                    if (selectedItem.name.error) {
                        errorList.push({ name: selectedItem.name.domElement.name, element: selectedItem.name.domElement });
                    }
                    if (selectedItem.webAccessCode.error) {
                        errorList.push({ name: selectedItem.webAccessCode.domElement.name, element: selectedItem.webAccessCode.domElement });
                    }
                },
                    // Go To Element
                gotoElement = function (validation) {
                    view.gotoElement(validation.element);
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
                        errorList.removeAll();
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
                        //#region Addresses
                        _.each(newAddresses(), function (address) {
                            storeToSave.NewAddedAddresses.push(address.convertToServerData());
                        });

                        //#endregion
                        // #region Company Contacts
                        _.each(newCompanyContacts(), function (companyContact) {
                            storeToSave.NewAddedCompanyContacts.push(companyContact.convertToServerData());
                        });
                        //#endregion
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            storeToSave.MediaLibraries.push(item.convertToServerData());
                        });

                        dataservice.saveStore(
                            storeToSave, {
                                success: function (data) {
                                    //#region new store adding for Customer/Prospect
                                    if (selectedStore().companyId() == undefined && selectedStore().type() == 0) {
                                        selectedStore().companyId(data.CompanyId);
                                        // ReSharper disable once InconsistentNaming
                                        var tempCustomerListView = new model.customerViewListModel();
                                        tempCustomerListView.customerTYpe(selectedStore().isCustomer() || 0);    // Prospect 
                                        tempCustomerListView.id(data.CompanyId);
                                        tempCustomerListView.name(data.Name);
                                        tempCustomerListView.creationdate(data.CreationDate);
                                        tempCustomerListView.status(data.Status);
                                        tempCustomerListView.statusClass(data.CompanyId);
                                        tempCustomerListView.storeImageFileBinary(data.StoreImagePath);
                                        
                                        tempCustomerListView.defaultContactEmail(data.DefaultContactEmail);
                                        tempCustomerListView.defaultContact(data.DefaultContact);

                                        if (data.Status == 0) {
                                            tempCustomerListView.status("Inactive");
                                            tempCustomerListView.statusClass('label label-danger');
                                        }
                                        if (data.Status == 1) {
                                            tempCustomerListView.status("Active");
                                            tempCustomerListView.statusClass('label label-success');
                                        }
                                        if (data.Status == 2) {
                                            tempCustomerListView.status("Banned");
                                            tempCustomerListView.statusClass('label label-default');
                                        }
                                        if (data.Status == 3) {
                                            tempCustomerListView.status("Pending");
                                            tempCustomerListView.statusClass('label label-warning');
                                        }
                                        tempCustomerListView.email("");
                                        customersForListView.splice(0, 0, tempCustomerListView);
                                    }

                                    //#endregion
                                    //#region new store adding for supplier
                                    if (selectedStore().companyId() == undefined && selectedStore().type() == 2) {
                                        selectedStore().companyId(data.CompanyId);
                                        // ReSharper disable once InconsistentNaming
                                        var tempItem = new model.CrmSupplierListViewModel();
                                        tempItem.companyId(data.CompanyId);
                                        tempItem.name(data.Name);
                                        tempItem.createdDate(data.CreationDate);
                                        tempItem.status(data.Status);
                                        tempItem.storeImageFileBinary(data.StoreImagePath);
                                        tempItem.defaultContactEmail(data.DefaultContactEmail);
                                        tempItem.defaultContact(data.DefaultContact);
                                        suppliers.splice(0, 0, tempItem);
                                    }
                                    else if (selectedStore().companyId() > 0) {
                                        //#region Prospect or Customer updation
                                        _.each(customersForListView(), function (customer) {
                                            if (customer.id() == selectedStore().companyId()) {
                                                customer.name(data.Name);
                                                customer.customerTYpe(selectedStore().isCustomer() || 0);    // Prospect 
                                                customer.creationdate(data.CreationDate);
                                                customer.status(data.Status);
                                                customer.storeImageFileBinary(data.StoreImagePath);
                                                if (data.Status == 0) {
                                                    customer.status("Inactive");
                                                    customer.statusClass('label label-danger');
                                                }
                                                if (data.Status == 1) {
                                                    customer.status("Active");
                                                    customer.statusClass('label label-success');
                                                }
                                                if (data.Status == 2) {
                                                    customer.status("Banned");
                                                    customer.statusClass('label label-default');
                                                }
                                                if (data.Status == 3) {
                                                    customer.status("Pending");
                                                    customer.statusClass('label label-warning');
                                                }
                                            }
                                        });
                                        //#endregion
                                        //#region Supplier updation
                                        if (selectedStore().type() == 2) {
                                            _.each(suppliers(), function (supplier) {
                                                if (supplier.companyId() == selectedStore().companyId()) {
                                                    supplier.name(data.Name);
                                                    supplier.createdDate(data.CreationDate);
                                                    supplier.status(data.Status);
                                                    supplier.storeImageFileBinary(data.StoreImagePath);
                                                }
                                            });
                                        }
                                        //#endregion
                                        selectedStore().storeImageFileBinary(data.StoreImagePath);
                                    }
                                    //#endregion
                                    isEditorVisible(false);
                                    toastr.success("Successfully save.");
                                    resetObservableArrays();
                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                },
                                error: function (response) {
                                    toastr.error("Failed to Update . Error: " + response);
                                    isEditorVisible(false);
                                }
                            });
                    }
                },
                //Get Base Data By company Id
            getBaseData = function (id) {
                dataservice.getBaseData({
                    companyId: id
                }, {
                    success: function (data) {
                        if (data != null) {
                            addressCompanyTerritoriesFilter.removeAll();
                            contactCompanyTerritoriesFilter.removeAll();
                            addressTerritoryList.removeAll();
                            allCompanyAddressesList.removeAll();
                            _.each(data.CompanyTerritories, function (item) {
                                var territory = new model.CompanyTerritory.Create(item);
                                addressCompanyTerritoriesFilter.push(territory);
                                contactCompanyTerritoriesFilter.push(territory);
                                addressTerritoryList.push(territory);
                                selectedStore().companyTerritories.push(territory);
                            });
                            _.each(data.Addresses, function (item) {
                                var address = new model.Address.Create(item);
                                allCompanyAddressesList.push(address);
                            });

                        
                        }
                        selectedStore().reset();
                        isLoadingStores(false);
                        isBaseDataLoded(true);
                        $('#idCompanyimage')
                        .load(function () {

                          
                        })
                        .error(function () {
                            $("#idCompanyimage").attr("src", "/mis/Content/Images/imageplaceholder.png");
                           
                        });
                        view.initializeLabelPopovers();
                    },
                    error: function (response) {
                        isLoadingStores(false);
                        toastr.error("Failed to Load Stores . Error: " + response, "Please ReOpen Store", ist.toastrOptions);
                        view.initializeLabelPopovers();
                    }
                });
            },
                resetObservableArrays = function () {
                    companyTerritoryCounter = -1,
                    newAddresses.removeAll();
                    newCompanyTerritories.removeAll();
                    newCompanyContacts.removeAll();
                },

                //Store Image Files Loaded Callback
                    storeImageFilesLoadedCallback = function (file, data) {
                        selectedStore().storeImageFileBinary(data);
                        selectedStore().storeImageName(file.name);
                    },

                //#endregion

                //#region ___________ ORDERS TAB ____________
                   ordersTabClickHandler = function (data) {
                       if (isOrderTab()) {
                           return;
                       }
                       isOrderTab(true);
                       orderPager().reset();
                       getDataForOrderTab(data);
                   },
                    // Gets customers for list view
                getDataForOrderTab = function () {
                    dataservice.getOrdersData({
                        CompanyId: selectedStore().companyId(),
                        PageSize: orderPager().pageSize(),
                        PageNo: orderPager().currentPage(),
                        IsProspectOrCustomer: isProspectOrCustomerScreen(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    },
                    {
                        success: function (data) {
                            if (data != null) {
                                ordersList.removeAll();
                                _.each(data.OrdersList, function (order) {
                                    var newOrder = new model.Estimate.Create(order);
                                    ordersList.push(newOrder);
                                });
                                orderPager().totalCount(data.RowCount);
                            }
                        },
                        error: function () {
                            toastr.error("Error: Failed To load Customers!");
                        }
                    });
                },

                //#endregion

                //#region ___________ PURCHASE ORDERS TAB ____________
                purchaseOrdersTabClickHandler = function (data) {
                    isPurchaseOrderTab(true);
                    purchaseOrderPager().reset();
                    getDataForPurchaseOrderTab(data);
                },
                // Gets purchase orders for list view
                getDataForPurchaseOrderTab = function () {
                    dataservice.getPurchases({
                        CompanyId: selectedStore().companyId(),
                        PageSize: purchaseOrderPager().pageSize(),
                        PageNo: purchaseOrderPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    },
                    {
                        success: function (data) {
                            if (data != null) {
                                purchasesList.removeAll();
                                _.each(data.PurchasesList, function (purchase) {
                                    var newPurchase = new model.PurchaseListViewModel.Create(purchase);
                                    newPurchase.supplierName(selectedStore().name());
                                    purchasesList.push(newPurchase);
                                });
                                purchaseOrderPager().totalCount(data.RowCount);
                            }
                        },
                        error: function () {
                            toastr.error("Error: Failed To load Purchase Orders!");
                        }
                    });
                },

                //#endregion

                //#region ___________  GOOD RECEIVED NOTES TAB ____________
                   goodRecievedNotesTabClickHandler = function (data) {
                       isGoodsReceivedNoteTab(true);
                       goodsReceivedNotePager().reset();
                       getDataForGoodsReceivedNoteTab(data);
                   },
                    // Gets customers for list view
                getDataForGoodsReceivedNoteTab = function () {
                    dataservice.getGoodsReceivedNotes({
                        CompanyId: selectedStore().companyId(),
                        PageSize: goodsReceivedNotePager().pageSize(),
                        PageNo: goodsReceivedNotePager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    },
                    {
                        success: function (data) {
                            if (data != null) {
                                goodRecievedNotesList.removeAll();
                                _.each(data.GoodsReceivedNotesList, function (goodRecievedNote) {
                                    var newgoodRecievedNote = new model.GoodsReceivedNoteListViewModel.Create(goodRecievedNote);
                                    newgoodRecievedNote.supplierName(selectedStore().name());
                                    goodRecievedNotesList.push(newgoodRecievedNote);
                                });
                                goodsReceivedNotePager().totalCount(data.RowCount);
                            }
                        },
                        error: function () {
                            toastr.error("Error: Failed To load Goods Received Notes!");
                        }
                    });
                },

                //#endregion

                //#region ___________ INVOICES TAB ____________
                   invoicesTabClickHandler = function () {
                       if (isInvoiceTab()) {
                           return;
                       }
                       isInvoiceTab(true);
                       invoicePager().reset();
                       getsDataForInvoiceTab();
                   },
                  // Gets Invoices data
                  getsDataForInvoiceTab = function () {
                      dataservice.getInvoices({
                          CompanyId: selectedStore().companyId(),
                          PageSize: invoicePager().pageSize(),
                          PageNo: invoicePager().currentPage(),
                          SortBy: sortOn(),
                          IsAsc: sortIsAsc()
                      },
                      {
                          success: function (data) {
                              if (data != null) {
                                  invoicesList.removeAll();
                                  _.each(data.Invoices, function (item) {
                                      var invoice = new model.Invoice.Create(item);
                                      _.each(sectionFlagList(), function (flag) {
                                          if (invoice.flagId() == flag.SectionFlagId)
                                              invoice.flagColor(flag.FlagColor);
                                      });
                                      invoicesList.push(invoice);
                                  });
                                  invoicePager().totalCount(data.RowCount);
                              }
                          },
                          error: function () {
                              toastr.error("Error: Failed To load Customers!");
                          }
                      });
                  },

                //#endregion

                //#region ________ MEDIA LIBRARY___________

                    //Active Media File
                    selectedMediaFile = ko.observable(),
                    //Media Library Open From
                    mediaLibraryOpenFrom = ko.observable(),
                    mediaLibraryIdCount = ko.observable(0),
                    //Media Library File Loaded Call back
                    mediaLibraryFileLoadedCallback = function (file, data) {
                        //Flag check, whether file is already exist in media libray
                        var flag = true;
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            if (item.fileSource() === data && item.fileName() === file.name) {
                                flag = false;
                            }
                        });

                        if (flag) {
                            var mediaId = mediaLibraryIdCount() - 1;
                            var mediaLibrary = model.MediaLibrary();
                            mediaLibrary.id(mediaId);
                            mediaLibrary.fakeId(mediaId);
                            mediaLibrary.fileSource(data);
                            mediaLibrary.fileName(file.name);
                            mediaLibrary.fileType(file.type);
                            mediaLibrary.companyId(selectedStore().companyId());
                            newUploadedMediaFile(mediaLibrary);
                            selectedStore().mediaLibraries.push(newUploadedMediaFile());
                            //Last set Id
                            mediaLibraryIdCount(mediaId);
                        }
                    },

                    //Open Media Library From Store Background Image
                    showMediaLibraryDialogFromStoreBackground = function () {
                        resetMediaGallery();
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            if (selectedStore().storeBackgroudImageImageSource() !== undefined && item.fileSource() === selectedStore().storeBackgroudImageImageSource()) {
                                item.isSelected(true);
                                selectedStore().storeBackgroudImageImageSource(item.fileSource());
                            }
                        });
                        mediaLibraryOpenFrom("StoreBackground");
                        showMediaLibrary();
                    },
                    //Open Media Library From Company Banner
                    openMediaLibraryDialogFromCompanyBanner = function () {
                        resetMediaGallery();
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            if (selectedCompanyBanner().filePath() !== undefined && (item.id() === selectedCompanyBanner().filePath() || item.filePath() === selectedCompanyBanner().filePath())) {
                                item.isSelected(true);
                                selectedCompanyBanner().fileBinary(item.fileSource());
                            }
                        });
                        mediaLibraryOpenFrom("CompanyBanner");
                        showMediaLibrary();
                    },
                    //Open Media Library From Secondary Page
                    openMediaLibraryDialogFromSecondaryPage = function () {
                        resetMediaGallery();
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            if (selectedSecondaryPage().pageBanner() !== undefined && (item.id() === selectedSecondaryPage().pageBanner() || item.filePath() === selectedSecondaryPage().pageBanner())) {
                                item.isSelected(true);
                                selectedSecondaryPage().imageSrc(item.fileSource());
                            }
                        });
                        mediaLibraryOpenFrom("SecondaryPage");
                        showMediaLibrary();
                    },
                    //Open Media Library From Product Category Thumbnail
                    openMediaLibraryDialogFromProductCategoryThumbnail = function () {
                        resetMediaGallery();
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            if (selectedProductCategoryForEditting().thumbnailPath() !== undefined && (item.id() === selectedProductCategoryForEditting().thumbnailPath() || item.filePath() === selectedProductCategoryForEditting().thumbnailPath())) {
                                item.isSelected(true);
                                selectedProductCategoryForEditting().productCategoryThumbnailFileBinary(item.fileSource());
                            }
                        });
                        mediaLibraryOpenFrom("ProductCategoryThumbnail");
                        showMediaLibrary();
                    },
                    //Open Media Library From Product Category Banner
                    openMediaLibraryDialogFromProductCategoryBanner = function () {
                        resetMediaGallery();
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            if (selectedProductCategoryForEditting().imagePath() !== undefined && (item.id() === selectedProductCategoryForEditting().imagePath() || item.filePath() === selectedProductCategoryForEditting().imagePath())) {
                                item.isSelected(true);
                                selectedProductCategoryForEditting().productCategoryImageFileBinary(item.fileSource());
                            }
                        });
                        mediaLibraryOpenFrom("ProductCategoryBanner");
                        showMediaLibrary();
                    },
                    //Hie Media Library
                    hideMediaLibraryDialog = function () {
                        view.hideMediaGalleryDialog();
                    },

                    //Show Media Library 
                    showMediaLibrary = function () {
                        view.showMediaGalleryDialog();
                    },
                    //select Media File
                    selectMediaFile = function (media) {
                        resetMediaGallery();
                        media.isSelected(true);
                        selectedMediaFile(media);
                    },
                    //Reset Media Gallery
                    resetMediaGallery = function () {
                        _.each(selectedStore().mediaLibraries(), function (item) {
                            item.isSelected(false);
                        });
                    },
                    //Save Media File And Close Library Dialog
                    onSaveMedia = function () {
                        //Open From Store backgound
                        if (mediaLibraryOpenFrom() === "StoreBackground") {
                            selectedStore().storeBackgroudImageImageSource(selectedMediaFile().fileSource());
                        }
                            //If Open From Company Banner
                        else if (mediaLibraryOpenFrom() === "CompanyBanner") {
                            if (selectedMediaFile().id() > 0) {
                                selectedCompanyBanner().filePath(selectedMediaFile().filePath());
                            } else {
                                selectedCompanyBanner().filePath(selectedMediaFile().id());
                            }
                            selectedCompanyBanner().fileBinary(selectedMediaFile().fileSource());
                            selectedCompanyBanner().imageSource(selectedMediaFile().fileSource());
                        }
                            //If Open From Secondary Page
                        else if (mediaLibraryOpenFrom() === "SecondaryPage") {
                            if (selectedMediaFile().id() > 0) {
                                selectedSecondaryPage().pageBanner(selectedMediaFile().filePath());
                            } else {
                                selectedSecondaryPage().pageBanner(selectedMediaFile().id());
                            }
                            selectedSecondaryPage().imageSrc(selectedMediaFile().fileSource());
                        }
                            //If Open From Product Category Banner
                        else if (mediaLibraryOpenFrom() === "ProductCategoryBanner") {
                            if (selectedMediaFile().id() > 0) {
                                selectedProductCategoryForEditting().imagePath(selectedMediaFile().filePath());
                            } else {
                                selectedProductCategoryForEditting().imagePath(selectedMediaFile().id());
                            }
                            selectedProductCategoryForEditting().productCategoryImageFileBinary(selectedMediaFile().fileSource());
                        }
                            //If Open From Product Category Thumbnail
                        else if (mediaLibraryOpenFrom() === "ProductCategoryThumbnail") {
                            if (selectedMediaFile().id() > 0) {
                                selectedProductCategoryForEditting().thumbnailPath(selectedMediaFile().filePath());
                            } else {
                                selectedProductCategoryForEditting().thumbnailPath(selectedMediaFile().id());
                            }
                            selectedProductCategoryForEditting().productCategoryThumbnailFileBinary(selectedMediaFile().fileSource());
                        }

                        //Hide gallery
                        hideMediaLibraryDialog();
                    },
                    //#endregion

                //#region ____________ INITIALIZE ____________
               initialize = function (specifiedView) {
                   view = specifiedView;

                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   if (isProspectOrCustomerScreen()) {
                       prospectPager(new pagination.Pagination({ PageSize: 5 }, customersForListView, getCustomers));
                       var companyIdFromDashboard = $('#CompanyId').val();
                       if (companyIdFromDashboard != 0) {
                           openEditDialog({ id: function () { return companyIdFromDashboard; } });
                       }
                       else
                       {
                           getCustomers();
                       }
                      
                   }
                   else {
                       supplierpager(new pagination.Pagination({ PageSize: 5 }, suppliers, getSuppliers));
                       getSuppliers();
                   }

                   orderPager(new pagination.Pagination({ PageSize: 5 }, ordersList, getDataForOrderTab));
                   purchaseOrderPager(new pagination.Pagination({ PageSize: 5 }, purchasesList, getDataForPurchaseOrderTab));
                   goodsReceivedNotePager(new pagination.Pagination({ PageSize: 5 }, goodRecievedNotesList, getDataForGoodsReceivedNoteTab));
                   invoicePager(new pagination.Pagination({ PageSize: 5 }, invoicesList, getsDataForInvoiceTab));
                   getBaseDataFornewCompany();
               };
                //#endregion

                //#region RETURN
                return {
                    initialize: initialize,
                    //#region Supplier Screen
                    supplierpager: supplierpager,
                    isLoadingSuppliers: isLoadingSuppliers,
                    searchSupplierFilter: searchSupplierFilter,
                    supplierSortOn: supplierSortOn,
                    selectedSupplier: selectedSupplier,
                    supplierSortIsAsc: supplierSortIsAsc,
                    suppliers: suppliers,
                    getSuppliers: getSuppliers,
                    templateToUseSupplier: templateToUseSupplier,
                    resetSupplierFilterSection: resetSupplierFilterSection,
                    //#endregion
                    prospectPager: prospectPager,
                    openReport: openReport,
                    searchFilter: searchFilter,
                    isEditorVisible: isEditorVisible,
                    isProspectOrCustomerScreen: isProspectOrCustomerScreen,
                    customersForListView: customersForListView,
                    filterHandler: filterHandler,
                    resetButtonHandler: resetButtonHandler,
                    sharedNavigationVm: sharedNavigationVm,
                    closeEditDialog: closeEditDialog,
                    selectedStore: selectedStore,
                    systemUsers: systemUsers,
                    onDeletePermanent: onDeletePermanent,
                    searchAddressFilter: searchAddressFilter,

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
                    storesListForDropDown: storesListForDropDown,
                    //#region Company Contacts
                    selectedCompanyContact: selectedCompanyContact,
                    companyContactFilter: companyContactFilter,
                    deletedCompanyContacts: deletedCompanyContacts,
                    edittedCompanyContacts: edittedCompanyContacts,
                    newCompanyContacts: newCompanyContacts,
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
                    //#region Company Territories
                    selectedCompanyTerritory: selectedCompanyTerritory,
                    companyTerritoryPager: companyTerritoryPager,
                    searchCompanyTerritoryFilter: searchCompanyTerritoryFilter,
                    searchCompanyTerritory: searchCompanyTerritory,
                    isSavingNewCompanyTerritory: isSavingNewCompanyTerritory,
                    templateToUseCompanyTerritories: templateToUseCompanyTerritories,
                    onCreateNewCompanyTerritory: onCreateNewCompanyTerritory,
                    onDeleteCompanyTerritory: onDeleteCompanyTerritory,
                    onEditCompanyTerritory: onEditCompanyTerritory,
                    onCloseCompanyTerritory: onCloseCompanyTerritory,
                    onSaveCompanyTerritory: onSaveCompanyTerritory,
                    contactCompanyTerritoriesFilter: contactCompanyTerritoriesFilter,
                    contactCompanyTerritoryFilter: contactCompanyTerritoryFilter,
                    addressTerritoryList: addressTerritoryList,
                    //#endregion 
                    //#region Media Library
                    selectedMediaFile: selectedMediaFile,
                    mediaLibraryOpenFrom: mediaLibraryOpenFrom,
                    mediaLibraryIdCount: mediaLibraryIdCount,
                    newUploadedMediaFile: newUploadedMediaFile,
                    mediaLibraryFileLoadedCallback: mediaLibraryFileLoadedCallback,
                    showMediaLibraryDialogFromStoreBackground: showMediaLibraryDialogFromStoreBackground,
                    openMediaLibraryDialogFromCompanyBanner: openMediaLibraryDialogFromCompanyBanner,
                    openMediaLibraryDialogFromSecondaryPage: openMediaLibraryDialogFromSecondaryPage,
                    openMediaLibraryDialogFromProductCategoryThumbnail: openMediaLibraryDialogFromProductCategoryThumbnail,
                    openMediaLibraryDialogFromProductCategoryBanner: openMediaLibraryDialogFromProductCategoryBanner,
                    hideMediaLibraryDialog: hideMediaLibraryDialog,
                    showMediaLibrary: showMediaLibrary,
                    selectMediaFile: selectMediaFile,
                    resetMediaGallery: resetMediaGallery,
                    onSaveMedia: onSaveMedia,
                    //#endregion 
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
                    errorList: errorList,
                    saveStore: saveStore,
                    resetObservableArrays: resetObservableArrays,
                    gotoElement: gotoElement,
                    ordersTabClickHandler: ordersTabClickHandler,
                    ordersList: ordersList,
                    isOrderTab: isOrderTab,
                    orderPager: orderPager,
                    invoicesTabClickHandler: invoicesTabClickHandler,
                    storeImageFilesLoadedCallback: storeImageFilesLoadedCallback,
                    isInvoiceTab: isInvoiceTab,
                    invoicePager: invoicePager,
                    invoicesList: invoicesList,
                    getsDataForInvoiceTab: getsDataForInvoiceTab,
                    userAndAddressesTabSelected: userAndAddressesTabSelected,
                    UserProfileImageFileLoadedCallback: UserProfileImageFileLoadedCallback,
                    getCustomers: getCustomers,
                    purchaseOrdersTabClickHandler: purchaseOrdersTabClickHandler,
                    purchasesList: purchasesList,
                    goodRecievedNotesList: goodRecievedNotesList,
                    purchaseOrderPager: purchaseOrderPager,
                    goodRecievedNotesTabClickHandler: goodRecievedNotesTabClickHandler,
                    goodsReceivedNotePager: goodsReceivedNotePager,
                    companyDdSelector: companyDdSelector,
                    onChangeCompany: onChangeCompany,
                    searchSuppliersByFilters: searchSuppliersByFilters
                };
                //#endregion
            })()
        };
        return ist.crm.viewModel;
    });
