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
                    //Store Image
                    storeImage = ko.observable(),
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

                        dataservice.saveStore(model.Store().convertToServerData(selectedStore()), {
                            success: function (data) {
                                //new store adding
                                if (selectedStore().storeId() == undefined || selectedStore().storeId() == 0) {
                                    stores.splice(0, 0, selectedStore());
                                }
                                //selectedStore().storeId(data.StoreId);
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
                        view.initializeForm();
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
                                    storeImage(data.ImageSource);
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
                        dataservice.getBaseData({
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
                    },

                    // ***** RAVE REVIEW BEGIN*****//

                    //Selected Rave Review
                    selectedRaveReview = ko.observable(),
                      // Template Chooser For Rave Review
                    templateToUseRaveReviews = function (raveReview) {
                        return (raveReview === selectedRaveReview() ? 'editRaveReviewTemplate' : 'itemRaveReviewTemplate');
                    },
                    //Create Stock Sub Category
                     onCreateNewRaveReview = function () {
                         var raveReview = new model.RaveReview();
                         selectedRaveReview(raveReview);
                         view.showRaveReviewDialog();
                         //var raveReview = selectedRaveReview().raveReviews()[0];
                         ////Create Rave Reviews for the very First Time
                         //if (raveReview == undefined) {
                         //    selectedRaveReview().raveReviews.splice(0, 0, new model.RaveReview());
                         //    selectedRaveReview(selectedStore().raveReviews()[0]);
                         //}
                         //    //If There are already rave reviews in list
                         //else {
                         //    if (!raveReview.isValid()) {
                         //        raveReview.errors.showAllMessages();
                         //    }
                         //    else {
                         //        selectedRaveReview().raveReviews.splice(0, 0, new model.RaveReview());
                         //        selectedRaveReview(selectedStore().raveReviews()[0]);
                         //    }
                         //}
                     },
                     // Delete a Rave review
                    onDeleteRaveReview = function (raveReview) {
                        //if (raveReview.reviewId() > 0) {
                        selectedStore().raveReviews.remove(raveReview);
                        return;
                        //}
                    },

                    onEditRaveReview = function (raveReview) {
                        selectedRaveReview(raveReview);
                        view.showRaveReviewDialog();
                    },
                    onCloseRaveReview = function() {
                        view.hideRaveReviewDialog();
                    },
                    // ***** RAVE REVIEW END*****//

                    // ***** COMPANY CMYK COLOR BEGIN*****// 

                    //Selected Company CMYK Color
                    // ReSharper disable InconsistentNaming
                    selectedCompanyCMYKColor = ko.observable(),
                     // Template Chooser For Company CMYK Color
                    templateToUseCompanyCMYKColors = function (companyCMYKColor) {
                        return (companyCMYKColor === selectedCompanyCMYKColor() ? 'editCompanyCMYKColorTemplate' : 'itemCompanyCMYKColorTemplate');
                    },
                    //Create Stock Sub Category
                     onCreateNewCompanyCMYKColor = function () {
                         var companyCMYKColor = selectedStore().companyCMYKColors()[0];
                         //Create Company CMYK Color for the very First Time
                         if (companyCMYKColor == undefined) {
                             selectedStore().companyCMYKColors.splice(0, 0, new model.CompanyCMYKColor());
                             selectedCompanyCMYKColor(selectedStore().companyCMYKColors()[0]);
                         }
                             //If There are already company CMYK Color in list
                         else {
                             if (!companyCMYKColor.isValid()) {
                                 companyCMYKColor.errors.showAllMessages();
                             }
                             else {
                                 selectedStore().companyCMYKColors.splice(0, 0, new model.CompanyCMYKColor());
                                 selectedCompanyCMYKColor(selectedStore().companyCMYKColors()[0]);
                             }
                         }
                     },
                     // Delete a company CMYK Color
                    onDeleteCompanyCMYKColors = function (companyCMYKColor) {
                        if (companyCMYKColor.colorId() > 0) {
                            selectedStore().companyCMYKColors.remove(companyCMYKColor);
                            return;
                        }
                    },
                    // ***** COMPANY CMYK COLOR END*****//

                //Initialize
                // ReSharper disable once AssignToImplicitGlobalInFunctionScope
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 5 }, stores, getStores));
                    getStores();
                    getBaseData();
                    view.initializeForm();
                };

                return {
                    stores: stores,
                    storeImage: storeImage,
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
                    //saveNewStore: saveNewStore,
                    //saveEdittedStore: saveEdittedStore,
                    openEditDialog: openEditDialog,
                    getStoreForEditting: getStoreForEditting,
                    closeEditDialog: closeEditDialog,
                    resetFilterSection: resetFilterSection,
                    templateToUseRaveReviews: templateToUseRaveReviews,
                    selectedRaveReview:selectedRaveReview,
                    onCreateNewRaveReview: onCreateNewRaveReview,
                    onDeleteRaveReview: onDeleteRaveReview,
                    onEditRaveReview: onEditRaveReview,
                    onCloseRaveReview: onCloseRaveReview,
                    templateToUseCompanyCMYKColors: templateToUseCompanyCMYKColors,
                    onCreateNewCompanyCMYKColor: onCreateNewCompanyCMYKColor,
                    onDeleteCompanyCMYKColors: onDeleteCompanyCMYKColors,
                    initialize: initialize
                };
            })()
        };
        return ist.stores.viewModel;
    });
