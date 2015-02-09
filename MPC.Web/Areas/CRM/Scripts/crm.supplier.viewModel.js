/*
    Module with the view model for the crm
*/
define("crm/crm.supplier.viewModel",
    ["jquery", "amplify", "ko", "crm/crm.supplier.dataservice", "crm/crm.supplier.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm) {
        var ist = window.ist || {};
        ist.crmSupplier = {
            viewModel: (function () {
                var //View
                view,

                //#region ____________OBSERVABLES____________
                 //Pager
                pager = ko.observable(),
                //Is Loading suppliers
                isLoadingSuppliers = ko.observable(false),
                //Search Filter
                searchFilter = ko.observable(),
                //Sort On
                sortOn = ko.observable(1),
                 //Sort In Ascending
                sortIsAsc = ko.observable(true),
                selectedSupplier = ko.observable(),
                //#endregion

                //#region ____________OBSERVABLE ARRAYS____________
                //suppliers List
                suppliers = ko.observableArray([]),
                //#endregion

                //#region ____________SUPPLIERS LIST VIEW____________
                
                //GET Suppliers For Suppliers List View
                getSuppliers = function () {
                    isLoadingSuppliers(true);
                    //dataservice.getStores({
                    dataservice.getSuppliers({
                        SearchString: searchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            suppliers.removeAll();
                            if (data != null) {
                                pager().totalCount(data.RowCount);
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
                templateToUse = function (store) {
                    return (store === selectedSupplier() ? 'itemSupplierTemplate' : 'itemSupplierTemplate');
                },
                resetFilterSection = function () {
                    searchFilter(undefined);
                    getSuppliers();
                },
                //#endregion
                //Initialize
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   pager(new pagination.Pagination({ PageSize: 5 }, suppliers, getSuppliers));
                   getSuppliers();
               };

                return {
                    pager: pager,
                    isLoadingSuppliers: isLoadingSuppliers,
                    searchFilter: searchFilter,
                    sortOn: sortOn,
                    selectedSupplier: selectedSupplier,
                    sortIsAsc: sortIsAsc,
                    
                    suppliers: suppliers,

                    getSuppliers: getSuppliers,
                    templateToUse: templateToUse,
                    resetFilterSection: resetFilterSection,

                    initialize: initialize
                };
            })()
        };
        return ist.crmSupplier.viewModel;
    });
