/*
    Module with the view model for Supplier
*/
define("common/supplier.viewModel",
    ["jquery", "amplify", "ko", "common/pagination", "common/supplier.dataservice", "common/supplier.model"], function ($, amplify, ko, pagination, dataservice, model) {
        var ist = window.ist || {};
        ist.supplier = {
            viewModel: (function () {
                var // The view 
                    view,
                    //Active Supplier
                    selectedSupplier = ko.observable(),
                    // True if we are loading data
                    isLoading = ko.observable(false),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    supplierPager = ko.observable(),
                    //Search Filter
                    searchSupplierFilter = ko.observable(),
                    //#region Array
                    suppliers = ko.observableArray([]);
                //#endregion
                // Show the dialog
                show = function () {
                    isLoading(true);
                    view.showSupplierDialog();
                },
                // Hide the dialog
                hide = function () {
                    view.hideSupplierDialog();
                },
                //Get Suppliers
                getSuppliers = function () {
                    isLoading(true);
                    dataservice.getSuppliers({
                        SearchString: searchSupplierFilter(),
                        PageSize: supplierPager().pageSize(),
                        PageNo: supplierPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            supplierPager().totalCount(data.TotalCount);
                            suppliers.removeAll();
                            var supplierList = [];
                            _.each(data.Suppliers, function (item) {
                                var supplier = new model.SupplierListView.Create(item);
                                supplierList.push(supplier);
                            });
                            ko.utils.arrayPushAll(suppliers(), supplierList);
                            suppliers.valueHasMutated();
                            isLoading(false);
                        },
                        error: function () {
                            isLoading(false);
                            toastr.error("Failed to load suppliers.");
                        }
                    });
                },
                //Search Supplier
                searchSupplier = function () {
                    getSuppliers();
                },
                reset = function () {
                    searchSupplierFilter(undefined);
                    getSuppliers();
                },
                     onSelectSupplierColseDialog = ko.computed(function () {
                         if (selectedSupplier() !== undefined) {
                             if (selectedSupplier().isSelected()) {
                                 hide();
                             }
                         }
                     }, this),
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    supplierPager(pagination.Pagination({ PageSize: 5 }, suppliers, getSuppliers));
                };

                return {
                    selectedSupplier: selectedSupplier,
                    isLoading: isLoading,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    supplierPager: supplierPager,
                    show: show,
                    searchSupplierFilter: searchSupplierFilter,
                    hide: hide,
                    initialize: initialize,
                    suppliers: suppliers,
                    getSuppliers: getSuppliers,
                    searchSupplier: searchSupplier,
                    reset: reset,
                };
            })()
        };

        return ist.supplier.viewModel;
    });

