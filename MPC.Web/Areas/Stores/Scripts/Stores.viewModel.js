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
                    //Tab User And Addressed, Addresses Section Company Territories Filter
                    addressCompanyTerritoriesFilter = ko.observableArray([]),
                    contactCompanyTerritoriesFilter = ko.observableArray([]),
                    //Addresses to be used in store users shipping and billing address
                    allCompanyAddressesList = ko.observableArray([]),
                    //Company Banners
                    companyBanners = ko.observableArray([]),
                    //Roles
                    roles = ko.observableArray([]),
                    //RegistrationQuestions
                    registrationQuestions = ko.observableArray([]),
                    //Filetered Company Bannens List
                    filteredCompanyBanners = ko.observableArray([]),
                    //Company Banner Set List
                    companyBannerSetList = ko.observableArray([]),
                    //Page Categories
                    pageCategories = ko.observableArray([]),
                    //Email Events
                    emailEvents = ko.observableArray([]),
                    //Emails
                    emails = ko.observableArray([]),
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
                    editorViewModel = new ist.ViewModel(model.StoreListView),
                    //Selected store
                    selectedStoreListView = editorViewModel.itemForEditing,
                    //selectedStore
                    selectedStore = ko.observable(new model.Store),
                    //// Editor View Model
                    //editorViewModelListView = new ist.ViewModel(model.StoreListView),
                    ////Selected store
                    //selectedStoreListView = editorViewModelListView.itemForEditing,
                    //Template To Use
                    templateToUse = function (store) {
                        return (store === selectedStore() ? 'editStoreTemplate' : 'itemStoreTemplate');
                    },
                    //Selected Address
                    selectedCompanyContact = ko.observable(),
                    //Make Edittable
                    makeEditable = ko.observable(false),
                    //Create New Store
                    createNewStore = function () {
                        var store = new model.Store();
                        editorViewModel.selectItem(store);
                        //selectedStore(store);
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
                            companyId: store.companyId(),
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
                    //GET Stores For Stores List View
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
                                        var module = model.StoreListView.Create(item);
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
                        //1- New saving company should have 1 address and 1 user
                        //2- if company is editting then company should have a 1 address and 1 user in database after saving
                        //1
                        if (!(addressPager().totalCount() + (newAddresses().length - deletedAddresses().length) > 1)) {
                            toastr.error("There Should be Atleast One Address to save this Store");
                            flag = false;
                        }
                        if (!(contactCompanyPager().totalCount() + (newCompanyContacts(), length - deletedCompanyContacts().length)) > 1) {
                            toastr.error("There Should be Atleast One User to save this Store");
                            flag = false;
                        }
                        return flag;
                    },
                    //Save Store
                    saveStore = function (item) {
                        if (doBeforeSave()) {

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

                            //Secondary Pages
                            _.each(newAddedSecondaryPage(), function (sPage) {
                                storeToSave.NewAddedCmsPages.push(sPage.convertToServerData(sPage));
                            });
                            _.each(editedSecondaryPage(), function (sPage) {
                                storeToSave.EditCmsPages.push(sPage.convertToServerData(sPage));
                            });
                            _.each(deletedSecondaryPage(), function (sPage) {
                                storeToSave.DeletedCmsPages.push(sPage.convertToServerData(sPage));
                            });
                            //Page category
                            _.each(pageCategories(), function (pageCategory) {
                                storeToSave.PageCategories.push(pageCategory.convertToServerData(pageCategory));
                            });
                            //Emails (Campaigns)
                            _.each(emails(), function (email) {
                                storeToSave.Campaigns.push(email.convertToServerData(email));
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
                            //Addresses
                            _.each(newAddresses(), function (address) {
                                storeToSave.NewAddedAddresses.push(address.convertToServerData());
                            });
                            _.each(edittedAddresses(), function (address) {
                                storeToSave.EdittedAddresses.push(address.convertToServerData());
                            });
                            _.each(deletedAddresses(), function (address) {
                                storeToSave.DeletedAddresses.push(address.convertToServerData());
                            });
                            //Company Contacts
                            _.each(newCompanyContacts(), function (companyContact) {
                                storeToSave.NewAddedCompanyContacts.push(companyContact.convertToServerData());
                            });
                            _.each(edittedCompanyContacts(), function (companyContact) {
                                storeToSave.EdittedCompanyContacts.push(companyContact.convertToServerData());
                            });
                            _.each(deletedCompanyContacts(), function (companyContact) {
                                storeToSave.DeletedCompanyContacts.push(companyContact.convertToServerData());
                            });
                            dataservice.saveStore(
                                storeToSave, {
                                    success: function (data) {
                                        //new store adding
                                        if (selectedStore().companyId() == undefined || selectedStore().companyId() == 0) {
                                            selectedStore().companyId(data.CompanyId);
                                            stores.splice(0, 0, selectedStore());
                                        }
                                        //selectedStore().storeId(data.StoreId);
                                        isStoreEditorVisible(false);
                                        isEditorVisible(false);
                                        toastr.success("Successfully save.");
                                        resetObservableArrays();
                                        newAddedSecondaryPage.removeAll();
                                        editedSecondaryPage.removeAll();
                                        deletedSecondaryPage.removeAll();
                                    },
                                    error: function (response) {
                                        toastr.error("Failed to Update . Error: " + response);
                                        isStoreEditorVisible(false);
                                    }
                                });
                        }
                    },
                    //Open Store Dialog
                    openEditDialog = function () {
                        isEditorVisible(true);
                        getStoreForEditting();
                        view.initializeForm();
                        getBaseData();
                    },
                    //Get Store For editting
                    getStoreForEditting = function () {
                        dataservice.getStoreById({
                            //dataservice.getStores({
                            companyId: selectedStoreListView().companyId()
                        }, {
                            success: function (data) {
                                selectedStore(undefined);
                                if (data != null) {
                                    selectedStore(model.Store.Create(data.Company));
                                    //_.each(data.AddressResponse.Addresses, function (item) {
                                    //    selectedStore().addresses.push(model.Address.Create(item));
                                    //});
                                    //_.each(data.CompanyTerritoryResponse.CompanyTerritories, function (item) {
                                    //    selectedStore().companyTerritories.push(model.CompanyTerritory.Create(item));
                                    //});
                                    //_.each(data.CompanyContactResponse.CompanyContacts, function (item) {
                                    //    selectedStore().users.push(model.CompanyContact.Create(item));
                                    //});
                                    emails.removeAll();
                                    _.each(data.Company.Campaigns, function (item) {
                                        emails.push(model.Campaign.Create(item));
                                    });

                                    addressPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().addresses, searchAddress));
                                    companyTerritoryPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().companyTerritories, searchCompanyTerritory));
                                    contactCompanyPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().users, searchCompanyContact));


                                    //Seconday Page List And Pager
                                    secondaryPagePager(new pagination.Pagination({ PageSize: 5 }, selectedStore().secondaryPages, getSecondoryPages));
                                    secondaryPagePager().totalCount(data.SecondaryPageResponse.RowCount);
                                    _.each(data.SecondaryPageResponse.CmsPages, function (item) {
                                        selectedStore().secondaryPages.push(model.SecondaryPageListView.Create(item));
                                    });
                                    storeImage(data.ImageSource);
                                    companyBannerSetList.removeAll();
                                    companyBanners.removeAll();
                                    filteredCompanyBanners.removeAll();
                                    _.each(data.Company.CompanyBannerSets, function (item) {
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
                            companyId: selectedStoreListView().companyId()
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    systemUsers.removeAll();
                                    addressCompanyTerritoriesFilter.removeAll();
                                    contactCompanyTerritoriesFilter.removeAll();
                                    addressTerritoryList.removeAll();
                                    roles.removeAll();
                                    registrationQuestions.removeAll();
                                    allCompanyAddressesList.removeAll();
                                    pageCategories.removeAll();
                                    _.each(data.SystemUsers, function (item) {
                                        var systemUser = new model.SystemUser.Create(item);
                                        systemUsers.push(systemUser);
                                    });
                                    _.each(data.CompanyTerritories, function (item) {
                                        var territory = new model.CompanyTerritory.Create(item);
                                        addressCompanyTerritoriesFilter.push(territory);
                                        contactCompanyTerritoriesFilter.push(territory);
                                        addressTerritoryList.push(territory);
                                    });
                                    _.each(data.CompanyContactRoles, function (item) {
                                        var role = new model.Role.Create(item);
                                        roles.push(role);
                                    });
                                    _.each(data.RegistrationQuestions, function (item) {
                                        var registrationQuestion = new model.RegistrationQuestion.Create(item);
                                        registrationQuestions.push(registrationQuestion);
                                    });
                                    _.each(data.Addresses, function (item) {
                                        var address = new model.Address.Create(item);
                                        allCompanyAddressesList.push(address);
                                    });
                                    _.each(data.PageCategories, function (item) {
                                        pageCategories.push(model.PageCategory.Create(item));
                                    });
                                    ko.utils.arrayPushAll(emailEvents(), data.EmailEvents);
                                    emailEvents.valueHasMutated();
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
                    companyTerritoryPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
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
                                companyTerritoryPager().totalCount(data.RowCount);
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
                            if (selectedCompanyTerritory().territoryId() === undefined && isSavingNewCompanyTerritory() === true) {
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

                    //#region  COMPANY BANNER AND COMPANY BANNER SET
                    bannerEditorViewModel = new ist.ViewModel(model.CompanyBanner),
                    selectedCompanyBanner = bannerEditorViewModel.itemForEditing,
                    selectedCompanyBannerSet = ko.observable(),
                    addBannerCount = ko.observable(-1),
                    addBannerSetCount = ko.observable(-1),
                    //Craete Banner
                    onCreateBanner = function () {
                        selectedCompanyBanner(model.CompanyBanner());
                        selectedCompanyBanner().reset();
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
                                        item.fileBinary(companyBanner.fileBinary());
                                        item.imageSource(companyBanner.fileBinary());
                                        item.filename(companyBanner.filename());

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
                        bannerEditorViewModel.selectItem(banner);
                        selectedCompanyBanner().reset();
                        view.showEditBannerDialog();
                    },
                    //Cancel Company Banner
                    onCloseCompanyBanner = function () {
                        if (selectedCompanyBanner() != undefined) {

                            view.hideEditBannerDialog();
                            bannerEditorViewModel.revertItem();
                        }
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
                    },
                    //#endregion 

                    //#region Email
                    selectedEmail = ko.observable(),
                   //Create One Time Marketing Email
                  onCreateOneTimeMarketingEmail = function () {
                      var campaign = model.Campaign();
                      campaign.campaignType(3);
                      campaign.reset();
                      selectedEmail(campaign);
                      selectedEmail().reset();
                      view.showEmailCamapaignDialog();
                  },
                 //Create Interval Marketing Email
                 onCreateIntervalMarketingEmail = function () {
                     var campaign = model.Campaign();
                     campaign.campaignType(2);
                     selectedEmail(campaign);
                     selectedEmail().reset();
                     view.showEmailCamapaignDialog();
                 },
                onSaveEmail = function (email) {
                    if (dobeforeSaveEmail()) {
                        if (email.emailEventId() !== undefined) {
                            _.each(emailEvents(), function (item) {
                                if (item.EmailEventId === email.emailEventId()) {
                                    email.eventName(item.EventName);
                                }
                            });
                        }
                        if (email.id() === undefined) {
                            emails.splice(0, 0, email);
                        } else {

                        }
                        view.hideEmailCamapaignDialog();
                    }
                }
                //Do Before Save Email
                dobeforeSaveEmail = function () {
                    var flag = true;
                    if (!selectedEmail().isValid()) {
                        selectedEmail().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //Edit Email
                onEditEmail = function (campaign) {
                    selectedEmail(campaign);
                    selectedEmail().reset();
                    view.showEmailCamapaignDialog();
                }
                // Delete Email
                onDeleteEmail = function (email) {
                    emails.remove(email);
                },
                //#endregion

                //***** ADDRESSES ****//
                //Selected Address
                selectedAddress = ko.observable(),
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
                                if (item.isDefaultTerrorityShipping() == true && item.territoryId() == selectedCompanyContact().territoryId()) {
                                    shippingAddresses.push(item);
                                }
                                if (item.isDefaultTerrorityBilling() == true && item.territoryId() == selectedCompanyContact().territoryId()) {
                                    bussinessAddresses.push(item);
                                }
                            });
                        }
                    }),
                selectBussinessAddress = ko.computed(function () {
                    if (selectedBussinessAddressId() != undefined) {
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedBussinessAddressId()) {
                                selectedBussinessAddress(item);
                                selectedCompanyContact().bussinessAddressId(item.addressId());
                            }
                        });
                    }
                    if (selectedBussinessAddressId() == undefined) {
                        selectedBussinessAddress(undefined);
                        if (selectedCompanyContact() != undefined) {
                            selectedCompanyContact().bussinessAddressId(undefined);
                        }
                    }
                }),
                selectShippingAddress = ko.computed(function () {
                    if (selectedShippingAddressId() != undefined) {
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedShippingAddressId()) {
                                selectedShippingAddress(item);
                                selectedCompanyContact().shippingAddressId(item.addressId());
                            }
                        });
                    }
                    if (selectedShippingAddressId() == undefined) {
                        selectedShippingAddress(undefined);
                        if (selectedCompanyContact() != undefined) {
                            selectedCompanyContact().shippingAddressId(undefined);
                        }
                    }
                }),
                //Address Pager
                addressPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Contact Company Pager
               contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Secondary Page Pager
                secondaryPagePager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Address Search Filter
                searchAddressFilter = ko.observable(),
                //Search Address
                searchAddress = function () {
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
                            selectedStore().addresses.removeAll();
                            addressPager().totalCount(data.RowCount);
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
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Addresses" + response);
                        }
                    });
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
                    view.showAddressDialog();
                },
                // Delete Address
                onDeleteAddress = function (address) {
                    if (address.addressId() !== undefined) {
                        _.each(edittedAddresses(), function (item) {
                            if (item.addressId() == address.addressId()) {
                                edittedAddresses.remove(address);
                            }
                        });
                        deletedAddresses.push(address);
                    }
                    selectedStore().addresses.remove(address);
                    return;
                },
                onEditAddress = function (address) {
                    selectedAddress(address);
                    isSavingNewAddress(false);
                    view.showAddressDialog();
                },
                onCloseAddress = function () {
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
                onSaveAddress = function () {
                    if (doBeforeSaveAddress()) {
                        if (selectedAddress().addressId() === undefined && isSavingNewAddress() === true) {
                            selectedStore().addresses.splice(0, 0, selectedAddress());
                            newAddresses.push(selectedAddress());
                        } else {
                            //pushing item in editted Addresses List
                            if (selectedAddress().addressId() != undefined) {
                                var match = ko.utils.arrayFirst(edittedAddresses(), function (item) {
                                    return (selectedAddress().addressId() === item.addressId());
                                });

                                if (!match) {
                                    edittedAddresses.push(selectedAddress());
                                }

                            }
                        }
                        view.hideAddressDialog();
                    }
                },
                // ***** Address END *****

                //***** Secondry Page *****
                //#region Secondry Page
            selectedSecondaryPage = ko.observable(),
            selectedPageCategory = ko.observable(),
            newAddedSecondaryPage = ko.observableArray([]),
            editedSecondaryPage = ko.observableArray([]),
            deletedSecondaryPage = ko.observableArray([]),
            nextSecondaryPageIdCounter = ko.observable(0),
                //Add New Secondary PAge
            onAddSecondaryPage = function () {
                selectedSecondaryPage(model.CMSPage());
                view.showSecondoryPageDialog();
            },
                //Add Secondry Page Category
            onAddSecondryPageCategory = function () {
                selectedPageCategory(model.PageCategory());
                view.showSecondaryPageCategoryDialog();
            },
                //Get Secondory Pages
            getSecondoryPages = function () {
                dataservice.getSecondaryPages({
                    CompanyId: selectedStore().companyId(),
                    PageSize: secondaryPagePager().pageSize(),
                    PageNo: secondaryPagePager().currentPage(),
                    SortBy: sortOn(),
                    IsAsc: sortIsAsc()
                }, {
                    success: function (data) {
                        selectedStore().secondaryPages.removeAll();
                        _.each(data.CmsPages, function (cmsPage) {
                            selectedStore().secondaryPages.push(model.SecondaryPageListView.Create(cmsPage));
                        });
                    },
                    error: function (response) {
                        toastr.error("Failed To Load Secondary Pages" + response);
                    }
                });
            },
                //Edit Secondary Page
            onEditSecondaryPage = function (secondaryPage) {
                //If Newly added item edited i-e It is not save in db yet
                if (secondaryPage.pageId() < 0) {
                    _.each(newAddedSecondaryPage(), function (item) {
                        if (item.id() === secondaryPage.pageId())
                            selectedSecondaryPage(item);
                    });
                } else {
                    dataservice.getSecondryPageById({
                        id: secondaryPage.pageId(),
                    }, {
                        success: function (data) {
                            if (data != null) {
                                selectedSecondaryPage(model.CMSPage.Create(data));
                                view.showSecondoryPageDialog();
                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Secondary Page Detail . Error: ");
                        }
                    });
                }


            },
                //Delete Secondary Page
            onDeleteSecondaryPage = function (secondaryPage) {
                if (!secondaryPage.pageId()) {
                    //companyBanners.remove(secondaryPage);
                    return;
                }
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    _.each(newAddedSecondaryPage(), function (item) {
                        if (item.id() == secondaryPage.pageId()) {
                            newAddedSecondaryPage.remove(item);
                        }
                    });
                    //Delete From Current Secondary Page
                    _.each(selectedStore().secondaryPages(), function (item) {
                        if (item.pageId() == secondaryPage.pageId()) {
                            selectedStore().secondaryPages.remove(item);
                        }
                    });
                    //delete Form edit Secondary Pages
                    _.each(editedSecondaryPage(), function (item) {
                        if (item.id() == secondaryPage.pageId()) {
                            editedSecondaryPage.remove(item);
                        }
                    });
                });
                confirmation.show();
            },
                //Add Default PAge Keywords
            addDefaultPageKeyWords = function () {
                selectedSecondaryPage().pageKeywords(selectedSecondaryPage().defaultPageKeyWords());
            },
                //Save Secondary Page
            onSaveSecondaryPage = function (sPage) {
                if (doBeforeSaveSecondaryPage()) {
                    //Newly Added, Edit 
                    if (sPage.id() < 0) {
                        _.each(newAddedSecondaryPage(), function (item) {
                            if (item.id() == sPage.id()) {
                                editedSecondaryPage.remove(item);
                            }
                        });
                        _.each(selectedStore().secondaryPages(), function (item) {
                            if (item.pageId() == sPage.id()) {
                                secondaryPageCopierForListView(item, sPage);
                            }
                        });
                        editedSecondaryPage.push(sPage);
                    }
                        //Old Secondary Page Edited that is saved in db already
                    else if (sPage.id() > 0) {
                        _.each(editedSecondaryPage(), function (item) {
                            if (item.id() == sPage.id()) {
                                editedSecondaryPage.remove(item);
                            }
                        });
                        _.each(selectedStore().secondaryPages(), function (item) {
                            if (item.pageId() == sPage.id()) {
                                secondaryPageCopierForListView(item, sPage);
                            }
                        });
                        editedSecondaryPage.push(sPage);
                    }
                        //New Secondary PAge Added
                    else if (sPage.id() === undefined) {
                        var nextId = nextSecondaryPageIdCounter() - 1;
                        sPage.id(nextId);
                        newAddedSecondaryPage.push(sPage);
                        var newPage = model.SecondaryPageListView();
                        newPage.pageId(sPage.id());
                        secondaryPageCopierForListView(newPage, sPage);
                        selectedStore().secondaryPages.splice(0, 0, newPage);
                        nextSecondaryPageIdCounter(nextId);
                    }
                    //Hide Dialog
                    view.hideSecondoryPageDialog();
                }
            },
                //Secondary Page Copier
            secondaryPageCopier = function (target, source) {
                target.pageTitle(source.pageTitle());
                target.pageKeywords(source.pageKeywords);
                target.metaTitle(source.metaTitle());
                target.metaDescriptionContent(source.metaDescriptionContent());
                target.metaCategoryContent(source.metaCategoryContent());
                target.metaRobotsContent(source.metaRobotsContent());
                target.metaAuthorContent(source.metaAuthorContent());
                target.metaLanguageContent(source.metaLanguageContent());
                target.metaRevisitAfterContent(source.metaRevisitAfterContent());
                target.categoryId(source.categoryId());
                target.pageHTML(source.pageHTML());
                target.imageSrc(source.imageSrc());
                target.fileName(source.fileName());
                target.defaultPageKeyWords(source.defaultPageKeyWords());
                //return target;
            },
                //Secondary Page Copier FOr List View
            secondaryPageCopierForListView = function (target, source) {
                target.pageTitle(source.pageTitle());
                target.metaTitle(source.metaTitle());
                target.isEnabled(false);
                target.isDisplay(false);
                _.each(pageCategories(), function (item) {
                    if (item.id() == source.categoryId()) {
                        target.categoryName(item.name());
                    }
                });
            },
                //Save Page Category
            onSavePageCategory = function (categoryPage) {
                var flag = true;
                _.each(pageCategories(), function (item) {
                    if (flag) {
                        if (!doBeforeSavePageCategory(item)) {
                            flag = false;
                        }
                    }
                });
                if (flag) {
                    //Update The Page Category Name in Secondary Page List
                    _.each(pageCategories(), function (item) {
                        _.each(selectedStore().secondaryPages(), function (sPage) {
                            if (item.pageId() === sPage.categoryId()) {
                                sPage.categoryName(item.name);
                            }
                        });
                    });
                    //Hide Dialog
                    view.hideSecondaryPageCategoryDialog();
                }
            },
                //Before Save Logic
            doBeforeSaveSecondaryPage = function () {
                var flag = true;
                if (!selectedSecondaryPage().isValid()) {
                    selectedSecondaryPage().errors.showAllMessages();
                    flag = false;
                }
                return flag;
            },
                //Before Save Logic
            doBeforeSavePageCategory = function (categoryPage) {
                var flag = true;
                if (!categoryPage.isValid()) {
                    categoryPage.errors.showAllMessages();
                    flag = false;
                }
                return flag;
            },
                //#endregion
                //***** Secondy Page End
             MultipleImageFilesLoadedCallback = function (file, data) {
                 selectedCompanyBanner().fileBinary(data);
                 selectedCompanyBanner().filename(file.name);
                 selectedCompanyBanner().fileType(data.imageType);
             },
            SecondaryImageFileLoadedCallback = function (file, data) {
                selectedSecondaryPage().imageSrc(data);
                selectedSecondaryPage().fileName(file.name);
            },

                //*****    COMPANY CONTACT      ***************//

                //companyContactFilter
            companyContactFilter = ko.observable(),
            contactCompanyTerritoryFilter = ko.observable(),
                //Deleted Company Contact 
            deletedCompanyContacts = ko.observableArray([]),
            edittedCompanyContacts = ko.observableArray([]),
            newCompanyContacts = ko.observableArray([]),
                //Company Contact  Pager
            companyContactPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Company Contact Search Filter
            searchCompanyContactFilter = ko.observable(),
                //Search Company Contact        
            searchCompanyContact = function () {
                dataservice.searchCompanyContact({
                    SearchFilter: searchCompanyContactFilter(),
                    CompanyId: selectedStore().companyId(),
                    TerritoryId: contactCompanyTerritoryFilter(),
                    PageSize: companyContactPager().pageSize(),
                    PageNo: companyContactPager().currentPage(),
                    SortBy: sortOn(),
                    IsAsc: sortIsAsc()
                }, {
                    success: function (data) {
                        selectedStore().users.removeAll();
                        contactCompanyPager().totalCount(data.RowCount);
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
                    },
                    error: function (response) {
                        toastr.error("Failed To Load Users" + response);
                    }
                });
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
                selectedCompanyContact(user);
                isSavingNewCompanyContact(true);
                view.showCompanyContactDialog();
            },
                // Delete CompanyContact
            onDeleteCompanyContact = function (companyContact) {
                if (companyContact.contactId() !== undefined) {
                    _.each(edittedCompanyContacts(), function (item) {
                        if (item.contactId() == companyContact.contactId()) {
                            edittedCompanyContacts.remove(companyContact);
                        }
                    });
                    deletedCompanyContacts.push(companyContact);
                }
                selectedStore().users.remove(companyContact);
                return;
            },
            onEditCompanyContact = function (companyContact) {
                selectedCompanyContact(companyContact);
                isSavingNewCompanyContact(false);
                view.showCompanyContactDialog();
            },
            onCloseCompanyContact = function () {
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
            onSaveCompanyContact = function () {
                if (doBeforeSaveCompanyContact()) {
                    if (selectedCompanyContact().contactId() === undefined && isSavingNewCompanyContact() === true) {
                        selectedStore().users.splice(0, 0, selectedCompanyContact());
                        newCompanyContacts.push(selectedCompanyContact());
                    } else {
                        //pushing item in editted CompanyContacts List
                        if (selectedCompanyContact().contactId() != undefined) {
                            var match = ko.utils.arrayFirst(edittedCompanyContacts(), function (item) {
                                return (selectedCompanyContact().contactId() === item.contactId());
                            });

                            if (!match) {
                                edittedCompanyContacts.push(selectedCompanyContact());
                            }

                        }
                    }
                    view.hideCompanyContactDialog();
                }
            },
                UserProfileImageFileLoadedCallback = function (file, data) {
                    selectedCompanyContact().image(data);
                    selectedCompanyContact().fileName(file.name);
                },
                // ***** CompanyContact END *****
            resetObservableArrays = function () {

                deletedAddresses.removeAll();
                edittedAddresses.removeAll();
                newAddresses.removeAll();
                deletedCompanyTerritories.removeAll();
                edittedCompanyTerritories.removeAll();
                newCompanyTerritories.removeAll();
                deletedCompanyContacts.removeAll();
                edittedCompanyContacts.removeAll();
                newCompanyContacts.removeAll();

            },
                //Initialize
                // ReSharper disable once AssignToImplicitGlobalInFunctionScope
        initialize = function (specifiedView) {
            view = specifiedView;
            ko.applyBindings(view.viewModel, view.bindingRoot);
            pager(new pagination.Pagination({ PageSize: 5 }, stores, getStores));
            getStores();
            view.initializeForm();
        };

                return {
                    MultipleImageFilesLoadedCallback: MultipleImageFilesLoadedCallback,
                    SecondaryImageFileLoadedCallback: SecondaryImageFileLoadedCallback,
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
                    selectedAddress: selectedAddress,
                    deletedAddresses: deletedAddresses,
                    edittedAddresses: edittedAddresses,
                    newAddresses: newAddresses,
                    addressPager: addressPager,
                    searchAddressFilter: searchAddressFilter,
                    searchAddress: searchAddress,
                    isSavingNewAddress: isSavingNewAddress,
                    templateToUseAddresses: templateToUseAddresses,
                    onCreateNewAddress: onCreateNewAddress,
                    onDeleteAddress: onDeleteAddress,
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
                    onEditAddress: onEditAddress,
                    onCloseAddress: onCloseAddress,
                    doBeforeSaveAddress: doBeforeSaveAddress,
                    onSaveAddress: onSaveAddress,
                    addressCompanyTerritoriesFilter: addressCompanyTerritoriesFilter,
                    addressTerritoryFilter: addressTerritoryFilter,
                    addressTerritoryFilterSelected: addressTerritoryFilterSelected,
                    //editorViewModelListView: editorViewModelListView,
                    selectedStoreListView: selectedStoreListView,
                    contactCompanyPager: contactCompanyPager,
                    onAddSecondaryPage: onAddSecondaryPage,
                    onAddSecondryPageCategory: onAddSecondryPageCategory,
                    selectedCompanyContact: selectedCompanyContact,
                    companyContactFilter: companyContactFilter,
                    deletedCompanyContacts: deletedCompanyContacts,
                    edittedCompanyContacts: edittedCompanyContacts,
                    newCompanyContacts: newCompanyContacts,
                    companyContactPager: companyContactPager,
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
                    contactCompanyTerritoriesFilter: contactCompanyTerritoriesFilter,
                    contactCompanyTerritoryFilter: contactCompanyTerritoryFilter,
                    addressTerritoryList: addressTerritoryList,
                    resetObservableArrays: resetObservableArrays,
                    registrationQuestions: registrationQuestions,
                    roles: roles,
                    secondaryPagePager: secondaryPagePager,
                    selectedSecondaryPage: selectedSecondaryPage,
                    onEditSecondaryPage: onEditSecondaryPage,
                    onDeleteSecondaryPage: onDeleteSecondaryPage,
                    pageCategories: pageCategories,
                    addDefaultPageKeyWords: addDefaultPageKeyWords,
                    onSaveSecondaryPage: onSaveSecondaryPage,
                    onSavePageCategory: onSavePageCategory,
                    selectedPageCategory: selectedPageCategory,
                    allCompanyAddressesList: allCompanyAddressesList,
                    shippingAddresses: shippingAddresses,
                    bussinessAddresses: bussinessAddresses,
                    populateAddressesList: populateAddressesList,
                    selectedBussinessAddress: selectedBussinessAddress,
                    selectedShippingAddress: selectedShippingAddress,
                    selectedBussinessAddressId: selectedBussinessAddressId,
                    selectedShippingAddressId: selectedShippingAddressId,
                    selectBussinessAddress: selectBussinessAddress,
                    selectShippingAddress: selectShippingAddress,
                    UserProfileImageFileLoadedCallback: UserProfileImageFileLoadedCallback,
                    emailEvents: emailEvents,
                    emails: emails,
                    onCreateIntervalMarketingEmail: onCreateIntervalMarketingEmail,
                    onCreateOneTimeMarketingEmail: onCreateOneTimeMarketingEmail,
                    selectedEmail: selectedEmail,
                    onEditEmail: onEditEmail,
                    onSaveEmail: onSaveEmail,
                    onDeleteEmail: onDeleteEmail,
                    onCloseCompanyBanner: onCloseCompanyBanner,
                    initialize: initialize
                };
            })()
        };
        return ist.stores.viewModel;
    });
