/*
    Module with the view model for Company Selector
*/
define("common/companySelector.viewModel",
    ["jquery", "amplify", "ko", "common/companySelector.dataservice", "common/companySelector.model", "common/pagination"], function ($, amplify, ko, dataservice, model,
    pagination) {
        var ist = window.ist || {};
        ist.companySelector = {
            viewModel: (function () {
                var // The view 
                    view,
                    // Stock Items
                    companies = ko.observableArray([]),
                    // Company Contacts
                    companyContacts = ko.observableArray([]),
                    // company Dialog Filter
                    companyDialogFilter = ko.observable(),
                    // company Dialog is Customer Filter
                    companyDialogStoreTypeFilter = ko.observableArray([]),
                    // Is Opened from Order
                    isOpenedFromOrder = ko.observable(),
                    // Pagination For Company Dialog
                    companyDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, companies)),
                    // Pagination For Contact Dialog
                    companyContactDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, companyContacts)),
                    // Search Stock Items
                    searchCompanies = function () {
                        if (isOpenedFromOrder()) {
                            companyContactDialogPager().reset();
                            getCompanyContacts();
                            return;
                        }
                        companyDialogPager().reset();
                        getCompanies();
                    },
                    // Reset Stock Items
                    resetCompanies = function () {
                        // Reset Text 
                        resetDialogFilters();
                        // Reset Callback
                        afterSelect = null;
                        // Reset List
                        companies.removeAll();
                        companyContacts.removeAll();
                        // Reset Pager
                        companyDialogPager().reset();
                        // Reset Pager
                        companyContactDialogPager().reset();
                    },
                    // after selection
                    afterSelect = null,
                    // Reset Stock Dialog Filters
                    resetDialogFilters = function () {
                        // Reset Text 
                        companyDialogFilter(undefined);
                        // Reset Category
                        companyDialogStoreTypeFilter(undefined);
                        // Reset Opened From Order Flag
                        isOpenedFromOrder(undefined);
                    },
                    // Show
                    show = function (afterSelectCallback, storeType, isForOrder) {
                        resetCompanies();
                        view.showDialog();
                        if (storeType) {
                            companyDialogStoreTypeFilter(storeType);
                        }
                        isOpenedFromOrder(isForOrder || undefined);
                        afterSelect = afterSelectCallback;
                        if (isForOrder) {
                            getCompanyContacts();
                            return;
                        }
                        getCompanies();
                    },
                    // On Select Company
                    onSelectCompany = function (company) {
                        if (afterSelect && typeof afterSelect === "function") {
                            afterSelect(isOpenedFromOrder() ?
                                {
                                    id: company.companyId, name: company.companyName, isCustomer: company.isCustomer, storeId: company.storeId,
                                    contactId: company.id, addressId: company.addressId
                        } : company);
                        }
                        view.hideDialog();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        companyDialogPager(new pagination.Pagination({ PageSize: 5 }, companies, getCompanies));
                        companyContactDialogPager(new pagination.Pagination({ PageSize: 5 }, companyContacts, getCompanyContacts));
                    },
                    // Map Stock Items 
                    mapCompanies = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Company.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(companies(), itemsList);
                        companies.valueHasMutated();
                    },
                    // Map Company Contacts 
                    mapCompanyContacts = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.CompanyContact.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(companyContacts(), itemsList);
                        companyContacts.valueHasMutated();
                    },
                    // Get Contact Companies
                    getCompanyContacts = function () {
                        dataservice.getCompanyContactsForOrderByType({
                            SearchString: companyDialogFilter(),
                            PageSize: companyContactDialogPager().pageSize(),
                            PageNo: companyContactDialogPager().currentPage(),
                            CustomerTypes: companyDialogStoreTypeFilter(),
                            ForOrder: isOpenedFromOrder()
                        }, {
                            success: function (data) {
                                companyContacts.removeAll();
                                if (data && data.RowCount > 0) {
                                    mapCompanyContacts(data.CompanyContacts);
                                    companyContactDialogPager().totalCount(data.RowCount);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load company items" + response);
                            }
                        });
                    },
                    // Get Stock Items
                    getCompanies = function () {
                        dataservice.getCompaniesByType({
                            SearchString: companyDialogFilter(),
                            PageSize: companyDialogPager().pageSize(),
                            PageNo: companyDialogPager().currentPage(),
                            CustomerTypes: companyDialogStoreTypeFilter(),
                            ForOrder: isOpenedFromOrder()
                        }, {
                            success: function (data) {
                                companies.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapCompanies(data.Companies);
                                    companyDialogPager().totalCount(data.TotalCount);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load company items" + response);
                            }
                        });
                    };

                return {
                    //Arrays
                    companyDialogFilter: companyDialogFilter,
                    companies: companies,
                    companyContacts: companyContacts,
                    searchCompanies: searchCompanies,
                    resetCompanies: resetCompanies,
                    companyDialogPager: companyDialogPager,
                    companyContactDialogPager: companyContactDialogPager,
                    isOpenedFromOrder: isOpenedFromOrder,
                    //Utilities
                    onSelectCompany: onSelectCompany,
                    initialize: initialize,
                    show: show
                };
            })()
        };

        return ist.companySelector.viewModel;
    });

