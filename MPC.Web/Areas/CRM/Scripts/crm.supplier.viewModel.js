/*
    Module with the view model for the crm
*/
define("crm/crm.supplier.viewModel",
    ["jquery", "amplify", "ko", "crm/crm.supplier.dataservice", "crm/crm.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm) {
        var ist = window.ist || {};
        ist.crmSupplier = {
            viewModel: (function () {
                var //View
                view,

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
                   // Search button handler
                searchButtonHandler = function () {
                    supplierpager().reset();
                    getSuppliers();
                },

                //GET Suppliers For Suppliers List View
                getSuppliers = function () {
                    isLoadingSuppliers(true);
                    //dataservice.getStores({
                    dataservice.getSuppliers({
                        SearchString: searchSupplierFilter(),
                        PageSize: supplierpager().pageSize(),
                        PageNo: supplierpager().currentPage(),
                        SortBy: supplierSortOn(),
                        IsAsc: supplierSortIsAsc(),
                        CustomerType: 2
                    }, {
                        success: function (data) {
                            suppliers.removeAll();
                            if (data != null) {
                                supplierpager().totalCount(data.RowCount);
                                _.each(data.Companies, function (item) {
                                    var module = model.CrmSupplierListViewModel.Create(item);
                                    suppliers.push(module);
                                });
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
                //Initialize
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   supplierpager(new pagination.Pagination({ PageSize: 5 }, suppliers, getSuppliers));
                   getSuppliers();
               };

                return {
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
                    initialize: initialize,
                    searchButtonHandler: searchButtonHandler
                };
            })()
        };
        return ist.crmSupplier.viewModel;
    });
