/*
    Module with the view model for the My Organization.
*/
define("stores/stores.viewModel",
    ["jquery", "amplify", "ko", "stores/stores.dataservice", "stores/stores.model", "common/confirmation.viewModel", "common/pagination"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.stores = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //stores List
                    stores = ko.observableArray([]),
                    //system Users
                    systemUsers = ko.observableArray([]),
                    //Is Loading stores
                    isLoadingStores = ko.observable(false),
                    //Is Editorial View Visible
                    isEditorVisible = ko.observable(false),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    // Editor View Model
                    editorViewModel = new ist.ViewModel(model.Store),
                    //Selected store
                    selectedStore = editorViewModel.itemForEditing,

                    //Template To Use
                    templateToUse = function (store) {
                        return (store === selectedStore() ? 'editStoreTemplate' : 'itemStoreTemplate');
                    },
                    //Make Edittable
                    makeEditable = ko.observable(false),
                    //Create New Store
                    createNewStore = function () {
                        var store = new model.Store();
                        editorViewModel.selectItem(store);
                        selectedStore(store);
                        isStoreEditorVisible(true);
                    },
                    //On Edit Click Of Store
                    onEditItem = function (item) {
                        editorViewModel.selectItem(item);
                        openEditDialog();
                    },
                    //To Show/Hide Edit Section
                    isStoreEditorVisible = ko.observable(false),
                    //Delete Stock Category
                    deleteStore = function (store) {
                        dataservice.deleteStore({
                            storeId: store.storeId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    stores.remove(store);
                                    toastr.success(" Deleted Successfully !");
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Delete . Error: " + response);
                            }
                        });
                    },
                    //GET Stores
                    getStores = function () {
                        isLoadingStores(true);
                        dataservice.getStores({
                            SearchString: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                stores.removeAll();
                                if (data != null) {
                                    pager().totalCount(data.RowCount);
                                    _.each(data.Companies, function (item) {
                                        var module = model.Store.Create(item);
                                        stores.push(module);
                                    });
                                }
                                isLoadingStores(false);
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Error: Failed To load Stores " + response);
                            }
                        });
                    },
                    //Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedStore().isValid()) {
                            selectedStore().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    //Save Store
                    saveStore = function (item) {
                        if (selectedStore() != undefined && doBeforeSave()) {
                            if (selectedStore().storeId() > 0) {
                                saveEdittedStore();
                            } else {
                                saveNewStore(item);
                            }
                        }
                    },
                    //Save NEW Store
                    saveNewStore = function () {
                        dataservice.saveNewStore(model.Store().convertToServerData(selectedStore()), {
                            success: function (data) {
                                selectedStore().storeId(data.StoreId);
                                stores.splice(0, 0, selectedStore());
                                isStoreEditorVisible(false);
                                toastr.success("Successfully save.");
                            },
                            error: function (response) {
                                toastr.error("Error: Failed to save. " + response);
                            }
                        });
                    },
                    //Save EDIT Store
                    saveEdittedStore = function () {
                        dataservice.saveStore(model.Store().convertToServerData(selectedStore()), {
                            success: function () {
                                isStoreEditorVisible(false);
                                toastr.success("Successfully save.");
                            },
                            error: function (response) {
                                toastr.error("Failed to Update . Error: " + response);
                                isStoreEditorVisible(false);
                            }
                        });
                    },
                    //Open Store Dialog
                    openEditDialog = function () {
                        isEditorVisible(true);
                        getStoreForEditting();
                    },
                    //Get Store For editting
                    getStoreForEditting = function () {
                        dataservice.getStoreById({
                            //dataservice.getStores({
                            CompanyId: selectedStore().companyId()
                        }, {
                            success: function (data) {
                                selectedStore(undefined);
                                if (data != null) {
                                    selectedStore(model.Store.Create(data));
                                }
                                isLoadingStores(false);
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Failed to Load Stores . Error: " + response);
                            }
                        });
                    },
                    //Close Store Dialog
                    closeEditDialog = function () {
                        if (selectedStore() != undefined) {
                            if (selectedStore().companyId() > 0) {
                                isEditorVisible(false);
                            } else {
                                isEditorVisible(false);
                                stores.remove(selectedStore());
                            }
                            editorViewModel.revertItem();
                        }
                    },
                    resetFilterSection = function () {
                        searchFilter(undefined);
                        getStores();
                    },
                    //Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData( {
                            success: function (data) {
                                if (data != null) {
                                    _.each(data.SystemUsers, function (item) {
                                        var systemUser = new model.SystemUsers.Create(item);
                                        systemUsers.push(systemUser);
                                    });
                                }
                                isLoadingStores(false);
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Failed to Load Stores . Error: " + response);
                            }
                        });
                    }
                //Initialize
                // ReSharper disable once AssignToImplicitGlobalInFunctionScope
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 5 }, stores, getStores));
                    getStores();
                    getBaseData();
                };

                return {
                    stores: stores,
                    systemUsers: systemUsers,
                    isLoadingStores: isLoadingStores,
                    isEditorVisible: isEditorVisible,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    searchFilter: searchFilter,
                    editorViewModel: editorViewModel,
                    selectedStore: selectedStore,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    createNewStore: createNewStore,
                    onEditItem: onEditItem,
                    isStoreEditorVisible: isStoreEditorVisible,
                    deleteStore: deleteStore,
                    getStores: getStores,
                    doBeforeSave: doBeforeSave,
                    saveStore: saveStore,
                    saveNewStore: saveNewStore,
                    saveEdittedStore: saveEdittedStore,
                    openEditDialog: openEditDialog,
                    getStoreForEditting: getStoreForEditting,
                    closeEditDialog: closeEditDialog,
                    resetFilterSection: resetFilterSection,
                    initialize: initialize
                };
            })()
        };
        return ist.stores.viewModel;
    });
