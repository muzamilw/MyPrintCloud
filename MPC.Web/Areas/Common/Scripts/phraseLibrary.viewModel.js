/*
    Module with the view model for Phrase Library
*/
define("common/phraseLibrary.viewModel",
    ["jquery", "amplify", "ko", "common/phraseLibrary.dataservice", "common/phraseLibrary.model"], function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.phraseLibrary = {
            viewModel: (function () {
                var // The view 
                    view,
                    //Active Supplier
                    selectedSupplier = ko.observable(),
                    //Sections
                    sections = ko.observableArray([]),
                    //Phrase List
                    phrases = ko.observableArray([]),
                      //#endregion
                         //get All Sections
                    getAllSections = function () {
                        dataservice.getSections({
                            success: function (data) {

                                //Company Types
                                //companyTypes.removeAll();
                                //ko.utils.arrayPushAll(companyTypes(), data.CompanyTypes);
                                //companyTypes.valueHasMutated();
                                //Markups
                            },
                            error: function () {
                                toastr.error("Failed to base data.");
                            }
                        });
                    },
                   //Call function for Save Supplier
                    onSaveSupplier = function (supplier) {
                        errorList.removeAll();
                        if (doBeforeSave()) {
                            if (addSupplier().addresses().length !== 0) {
                                addSupplier().addresses([]);
                            }
                            if (addSupplier().companyContacts().length !== 0) {
                                addSupplier().companyContacts([]);
                            }
                            addSupplier().addresses().push(addSupplier().addressInSupplier().convertToServerData());
                            addSupplier().companyContacts().push(addSupplier().companyContact().convertToServerData());
                            saveSupplier(supplier);
                        }
                    },
                  //Save Supplier
                    saveSupplier = function (supplier) {
                        dataservice.saveSupplier(addSupplier().convertToServerData(supplier), {
                            success: function (data) {
                                var supplierResult = new model.SupplierListView.Create(data);
                                addSupplier().id(data.SupplierId);
                                suppliers.splice(0, 0, supplierResult);
                                view.saveImage();
                                closeSupplierEditor();
                                toastr.success("Successfully save.");
                            },
                            error: function (exceptionMessage, exceptionType) {

                                if (exceptionType === ist.exceptionType.CaresGeneralException) {

                                    toastr.error(exceptionMessage);

                                } else {

                                    toastr.error("Failed to save.");

                                }

                            }
                        });
                    },
                  // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!addSupplier().isValid() || !addSupplier().addressInSupplier().isValid() || !addSupplier().companyContact().isValid()) {
                            addSupplier().errors.showAllMessages();
                            addSupplier().addressInSupplier().errors.showAllMessages();
                            addSupplier().companyContact().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                     //Get Store For editting
                    getPhrasesBySectionId = function () {
                        dataservice.getPhrasesBySectionId({
                            sectionId: 32
                        }, {
                            success: function (data) {
                                phrases.removeAll();
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Failed to Load Stores . Error: " + response);
                            }
                        });
                    },
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    //getAllSections();
                    getPhrasesBySectionId();
                    sections.push(model.Section(0, "Home"));
                    sections.push(model.Section(0, "About Us"));
                    sections.push(model.Section(0, "Contact us"));
                    view.showPhraseLibraryDialog();
                };

                return {
                    selectedSupplier: selectedSupplier,
                    //Arrays
                    sections: sections,
                    phrases: phrases,
                    //Utilities
                    initialize: initialize,
                };
            })()
        };

        return ist.phraseLibrary.viewModel;
    });

