/*
    Module with the view model for the Store.
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
                filteredCompanySetId = ko.observable(),
                //stores List
                 stores = ko.observableArray([]),
                //Store Image
                storeImage = ko.observable(),
                //system Users
                systemUsers = ko.observableArray([]),
                //Company Banners
                companyBanners = ko.observableArray([]),
                //Filetered Company Bannens List
                filteredCompanyBanners = ko.observableArray([]),
                //Company Banner Set List
                companyBannerSetList = ko.observableArray([]),
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
                    var storeToSave = model.Store().convertToServerData(selectedStore());

                    _.each(newCompanyTerritories(), function (territory) {
                        storeToSave.NewAddedCompanyTerritories.push(territory.convertToServerData());
                    });
                    _.each(edittedCompanyTerritories(), function (territory) {
                        storeToSave.EdittedCompanyTerritories.push(territory.convertToServerData());
                    });
                    _.each(deletedCompanyTerritories(), function (territory) {
                        storeToSave.DeletedCompanyTerritories.push(territory.convertToServerData());
                    });

                    _.each(companyBannerSetList(), function (bannerSet) {
                        var bannerSetServer = bannerSet.convertToServerData(bannerSet);
                        var banners = [];
                        _.each(companyBanners(), function (banner) {
                            if (banner.companySetId() === bannerSetServer.CompanySetId) {
                                banners.push(banner.convertToServerData(banner));
                            }
                        });
                        ko.utils.arrayPushAll(bannerSetServer.CompanyBanners, banners);
                        storeToSave.CompanyBannerSets.push(bannerSetServer);
                        //storeToSave.NewAddedCompanyTerritories.push(territory.convertToServerData());
                    });
                    dataservice.saveStore(
                        storeToSave, {
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
                                companyBannerSetList.removeAll();
                                companyBanners.removeAll();
                                filteredCompanyBanners.removeAll();
                                _.each(data.CompanyBannerSets, function (item) {
                                    companyBannerSetList.push(model.CompanyBannerSet.Create(item));
                                    //Extract Company Banners from company banner set item
                                    _.each(item.CompanyBanners, function (bannerSet) {
                                        var banner = model.CompanyBanner.Create(bannerSet);
                                        banner.setName(item.SetName);
                                        companyBanners.push(banner);
                                    });
                                });
                                ko.utils.arrayPushAll(filteredCompanyBanners(), companyBanners());
                                filteredCompanyBanners.valueHasMutated();
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
                                    var systemUser = new model.SystemUser.Create(item);
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
                    selectedStore().raveReviews.remove(raveReview);
                    return;
                },
                onEditRaveReview = function (raveReview) {
                    selectedRaveReview(raveReview);
                    view.showRaveReviewDialog();
                },
                onCloseRaveReview = function () {
                    view.hideRaveReviewDialog();
                },
                //Do Before Save Rave Review
                doBeforeSaveRaveReview = function () {
                    var flag = true;
                    if (!selectedRaveReview().isValid()) {
                        selectedRaveReview().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSaveRaveReview = function () {
                    if (doBeforeSaveRaveReview()) {
                        selectedStore().raveReviews.splice(0, 0, selectedRaveReview());
                        view.hideRaveReviewDialog();
                    }
                },
                // ***** RAVE REVIEW END*****//

                // ***** C O M P A N Y   T E R R I T O R Y ****//

                //Selected CompanyTerritory
                selectedCompanyTerritory = ko.observable(),
                //Deleted Company Territory 
                deletedCompanyTerritories = ko.observableArray([]),
                edittedCompanyTerritories = ko.observableArray([]),
                newCompanyTerritories = ko.observableArray([]),
                //Company Territory Pager
                companyTerritoryPager = ko.observable(),
                //CompanyTerritory Search Filter
                searchCompanyTerritoryFilter = ko.observable(),
                //Search Company Territory
                searchCompanyTerritory = function () {
                    dataservice.searchCompanyTerritory({
                        SearchFilter: searchCompanyTerritoryFilter(),
                        CompanyId: selectedStore().companyId(),
                        PageSize: companyTerritoryPager().pageSize(),
                        PageNo: companyTerritoryPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
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
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Company territories" + response);
                        }
                    });
                },
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
                    isSavingNewCompanyTerritory(true);
                    view.showCompanyTerritoryDialog();
                },
                // Delete Company Territory
                onDeleteCompanyTerritory = function (companyTerritory) {
                    if (companyTerritory.companyId() !== undefined) {
                        _.each(edittedCompanyTerritories(), function (item) {
                            if (item.territoryId() == companyTerritory.territoryId()) {
                                edittedCompanyTerritories.remove(companyTerritory);
                            }
                        });
                        deletedCompanyTerritories.push(companyTerritory);
                    }
                    selectedStore().companyTerritories.remove(companyTerritory);
                    return;
                },
                onEditCompanyTerritory = function (companyTerritory) {
                    selectedCompanyTerritory(companyTerritory);
                    isSavingNewCompanyTerritory(false);
                    view.showCompanyTerritoryDialog();
                },
                onCloseCompanyTerritory = function () {
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
                onSaveCompanyTerritory = function () {
                    if (doBeforeSaveCompanyTerritory()) {
                        if (selectedCompanyTerritory().companyId() === undefined && isSavingNewCompanyTerritory() === true) {
                            selectedStore().companyTerritories.splice(0, 0, selectedCompanyTerritory());
                            newCompanyTerritories.push(selectedCompanyTerritory());
                        } else {
                            //pushing item in editted Company Territories List
                            if (selectedCompanyTerritory().companyId() != undefined) {
                                var match = ko.utils.arrayFirst(edittedCompanyTerritories(), function (item) {
                                    return (selectedCompanyTerritory().territoryName() === item.territoryName() && selectedCompanyTerritory().territoryCode() === item.territoryCode());
                                });

                                if (!match) {
                                    edittedCompanyTerritories.push(selectedCompanyTerritory());
                                }

                            }
                        }
                        view.hideCompanyTerritoryDialog();
                    }
                },
                // ***** Company Territory END *****

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
                    var companyCMYKColor = new model.CompanyCMYKColor();
                    selectedCompanyCMYKColor(companyCMYKColor);
                    view.showCompanyCMYKColorDialog();
                    //var companyCMYKColor = selectedStore().companyCMYKColors()[0];
                    ////Create Company CMYK Color for the very First Time
                    //if (companyCMYKColor == undefined) {
                    //    selectedStore().companyCMYKColors.splice(0, 0, new model.CompanyCMYKColor());
                    //    selectedCompanyCMYKColor(selectedStore().companyCMYKColors()[0]);
                    //}
                    //    //If There are already company CMYK Color in list
                    //else {
                    //    if (!companyCMYKColor.isValid()) {
                    //        companyCMYKColor.errors.showAllMessages();
                    //    }
                    //    else {
                    //        selectedStore().companyCMYKColors.splice(0, 0, new model.CompanyCMYKColor());
                    //        selectedCompanyCMYKColor(selectedStore().companyCMYKColors()[0]);
                    //    }
                    //}
                },
                // Delete a company CMYK Color
                onDeleteCompanyCMYKColors = function (companyCMYKColor) {
                    selectedStore().companyCMYKColors.remove(companyCMYKColor);
                    return;
                },
                onEditCompanyCMYKColor = function (companyCMYKColor) {
                    selectedCompanyCMYKColor(companyCMYKColor);
                    view.showCompanyCMYKColorDialog();
                },
                onCloseCompanyCMYKColor = function () {
                    view.hideCompanyCMYKColorDialog();
                },
                //Do Before Save Rave Review
                doBeforeSaveCompanyCMYKColor = function () {
                    var flag = true;
                    if (!selectedCompanyCMYKColor().isValid()) {
                        selectedCompanyCMYKColor().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSaveCompanyCMYKColor = function () {
                    if (doBeforeSaveCompanyCMYKColor()) {
                        selectedStore().companyCMYKColors.splice(0, 0, selectedCompanyCMYKColor());
                        view.hideCompanyCMYKColorDialog();
                    }
                },

                // ***** COMPANY CMYK COLOR END*****//

                // ***** COMPANY BANNER AND COMPANY BANNER SET*****//
                bannerEditorViewModel = new ist.ViewModel(model.CompanyBanner),
                selectedCompanyBanner = bannerEditorViewModel.itemForEditing,
                selectedCompanyBannerSet = ko.observable();
                addBannerCount = ko.observable(-1);
                addBannerSetCount = ko.observable(-1);
                //Craete Banner
                onCreateBanner = function () {
                    selectedCompanyBanner(model.CompanyBanner());
                    view.showEditBannerDialog();
                },
                //Create Banner Set
                onAddSetBanner = function () {
                    selectedCompanyBannerSet(model.CompanyBannerSet.CreateNew());
                    view.showSetBannerDialog();
                },
                //Save Company Banner
                onSaveCompanyBanner = function (companyBanner) {
                    if (doBeforeSaveCompanyBanner()) {
                        _.each(companyBannerSetList(), function (item) {
                            if (item.id() === companyBanner.companySetId()) {
                                companyBanner.setName(item.setName());
                            }
                        });
                        if (companyBanner.id() === undefined) {
                            companyBanner.id(addBannerCount() - 1);
                            if (companyBanner.companySetId() === filteredCompanySetId() || filteredCompanySetId() === undefined) {
                                filteredCompanyBanners.splice(0, 0, companyBanner);
                                companyBanners.splice(0, 0, companyBanner);
                            } else {
                                companyBanners.splice(0, 0, companyBanner);
                            }
                        } else {
                            _.each(companyBanners(), function (item) {
                                if (item.id() === companyBanner.id()) {
                                    item.heading(companyBanner.heading());
                                    item.description(companyBanner.description());
                                    item.itemURL(companyBanner.itemURL());
                                    item.buttonURL(companyBanner.buttonURL());
                                    item.companySetId(companyBanner.companySetId());
                                }
                            });
                        }
                        view.hideEditBannerDialog();
                    }
                },
                //Save Banner Set
                onSaveBannerSet = function (bannerSet) {
                    if (doBeforeSaveCompanyBannerSet()) {
                        bannerSet.id(addBannerSetCount() - 1);
                        companyBannerSetList.push(bannerSet);
                        view.hideSetBannerDialog();
                    }
                },
                // Do Before Logic
                doBeforeSaveCompanyBannerSet = function () {
                    var flag = true;
                    if (!selectedCompanyBannerSet().isValid()) {
                        selectedCompanyBannerSet().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                // Do Before Logic
                doBeforeSaveCompanyBanner = function () {
                    var flag = true;
                    if (!selectedCompanyBanner().isValid()) {
                        selectedCompanyBanner().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //Edit Company Banner
                onEditCompanyBanner = function (banner) {
                    // bannerEditorViewModel.selectItem(banner);
                    //selectedCompanyBanner().reset();
                    view.showEditBannerDialog();
                },
                //Filter Banners based on banner set id
                filterBannerSet = ko.computed(function () {
                    if (filteredCompanySetId() !== undefined) {
                        filteredCompanyBanners.removeAll();
                        _.each(companyBanners(), function (item) {
                            if (item.companySetId() === filteredCompanySetId()) {
                                filteredCompanyBanners.push(item);
                            }
                        });
                    } else {
                        filteredCompanyBanners.removeAll();
                        ko.utils.arrayPushAll(filteredCompanyBanners(), companyBanners());
                        filteredCompanyBanners.valueHasMutated();
                    }


                }, this),
                //Dalete company Banner
                onDeleteCompanyBanner = function (banner) {
                    if (!banner.id()) {
                        companyBanners.remove(banner);
                        return;
                    }
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        _.each(companyBanners(), function (item) {
                            if (item.id() === banner.id()) {
                                companyBanners.remove(item);
                            }
                        });
                        filteredCompanyBanners.remove(banner);
                    });
                    confirmation.show();
                }
                // ***** COMPANY BANNER eND*****//
                //Initialize
                // ReSharper disable once AssignToImplicitGlobalInFunctionScope
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 5 }, stores, getStores));
                    companyTerritoryPager(pagination.Pagination({ PageSize: 5 }, stores, getStores));
                    getStores();
                    getBaseData();
                    view.initializeForm();
                };

                return {
                    filteredCompanySetId: filteredCompanySetId,
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
                    selectedRaveReview: selectedRaveReview,
                    onCreateNewRaveReview: onCreateNewRaveReview,
                    onDeleteRaveReview: onDeleteRaveReview,
                    onEditRaveReview: onEditRaveReview,
                    onCloseRaveReview: onCloseRaveReview,
                    doBeforeSaveRaveReview: doBeforeSaveRaveReview,
                    onSaveRaveReview: onSaveRaveReview,
                    templateToUseCompanyCMYKColors: templateToUseCompanyCMYKColors,
                    selectedCompanyCMYKColor: selectedCompanyCMYKColor,
                    onCreateNewCompanyCMYKColor: onCreateNewCompanyCMYKColor,
                    onDeleteCompanyCMYKColors: onDeleteCompanyCMYKColors,
                    onEditCompanyCMYKColor: onEditCompanyCMYKColor,
                    onCloseCompanyCMYKColor: onCloseCompanyCMYKColor,
                    doBeforeSaveCompanyCMYKColor: doBeforeSaveCompanyCMYKColor,
                    onSaveCompanyCMYKColor: onSaveCompanyCMYKColor,
                    selectedCompanyTerritory: selectedCompanyTerritory,
                    templateToUseCompanyTerritories: templateToUseCompanyTerritories,
                    searchCompanyTerritoryFilter: searchCompanyTerritoryFilter,
                    onCreateNewCompanyTerritory: onCreateNewCompanyTerritory,
                    onDeleteCompanyTerritory: onDeleteCompanyTerritory,
                    onEditCompanyTerritory: onEditCompanyTerritory,
                    onCloseCompanyTerritory: onCloseCompanyTerritory,
                    doBeforeSaveCompanyTerritory: doBeforeSaveCompanyTerritory,
                    onSaveCompanyTerritory: onSaveCompanyTerritory,
                    companyTerritoryPager: companyTerritoryPager,
                    searchCompanyTerritory: searchCompanyTerritory,
                    deletedCompanyTerritories: deletedCompanyTerritories,
                    edittedCompanyTerritories: edittedCompanyTerritories,
                    newCompanyTerritories: newCompanyTerritories,
                    isSavingNewCompanyTerritory: isSavingNewCompanyTerritory,
                    /*****Company Banner****/
                    selectedCompanyBanner: selectedCompanyBanner,
                    companyBanners: companyBanners,
                    selectedCompanyBannerSet: selectedCompanyBannerSet,
                    filteredCompanyBanners: filteredCompanyBanners,
                    companyBannerSetList: companyBannerSetList,
                    onCreateBanner: onCreateBanner,
                    onAddSetBanner: onAddSetBanner,
                    onSaveBannerSet: onSaveBannerSet,
                    onSaveCompanyBanner: onSaveCompanyBanner,
                    onEditCompanyBanner: onEditCompanyBanner,
                    onDeleteCompanyBanner: onDeleteCompanyBanner,
                    initialize: initialize
                };
            })()
        };
        return ist.stores.viewModel;
    });
