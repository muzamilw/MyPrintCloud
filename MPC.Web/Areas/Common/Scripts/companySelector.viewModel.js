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
                    // company Dialog Filter
                    companyDialogFilter = ko.observable(),
                    // company Dialog is Customer Filter
                    companyDialogStoreTypeFilter = ko.observableArray([]),
                    // Is Opened from Order
                    isOpenedFromOrder = ko.observable(),
                    // Pagination For Press Dialog
                    companyDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, companies)),
                    // Search Stock Items
                    searchCompanies = function () {
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
                        // Reset Pager
                        companyDialogPager().reset();
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
                        getCompanies();
                    },
                    // On Select Company
                    onSelectCompany = function (company) {
                        if (afterSelect && typeof afterSelect === "function") {
                            afterSelect(company);
                        }
                        view.hideDialog();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        companyDialogPager(new pagination.Pagination({ PageSize: 5 }, companies, getCompanies));
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
                    searchCompanies: searchCompanies,
                    resetCompanies: resetCompanies,
                    companyDialogPager: companyDialogPager,
                    //Utilities
                    onSelectCompany: onSelectCompany,
                    initialize: initialize,
                    show: show
                };
            })()
        };

        return ist.companySelector.viewModel;
    });

