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
                    //#region ___________ OBSERVABLES
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
                //#endregion

                //#region ___________ OBSERVABLE ARRAYS
                // Customers array for list view
                customersForListView = ko.observableArray(),
                //#endregion

                //#region ___________ LIST VIEW
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
                // Select Store
                selectStore = function(store) {
                    if (selectedStore() !== store) {
                        selectedStore(store);
                    }
                },
                //#endregion
                
                //#region CREATE NEW STORE
                onCreateNewStore = function () {
                    selectedStore(undefined);
                    isEditorVisible(true);
                },
                closeEditDialog = function() {
                    isEditorVisible(false);
                },
                //#endregion
               
                //Initialize
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   pager(new pagination.Pagination({ PageSize: 5 }, customersForListView, getCustomers));
                   getCustomers();
               };
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
                    selectStore: selectStore,
                    selectedStore: selectedStore
                };
            })()
        };
        return ist.crm.viewModel;
    });
