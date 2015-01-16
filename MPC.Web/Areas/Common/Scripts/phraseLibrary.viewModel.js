﻿/*
    Module with the view model for Phrase Library
*/
define("common/phraseLibrary.viewModel",
    ["jquery", "amplify", "ko", "common/phraseLibrary.dataservice", "common/phraseLibrary.model"], function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.phraseLibrary = {
            viewModel: (function () {
                var // The view 
                    view,
                    //Active Section (Category)
                    selectedSection = ko.observable(),
                    //select Phrase Field
                    selectedPhraseField = ko.observable(),
                    //Flag for open from Phrase Library
                    isOpenFromPhraseLibrary = ko.observable(true),
                    //selected Phrase
                    selectedPhrase = ko.observable(false),
                    //Sections
                    sections = ko.observableArray([]),
                    //Phrase List
                    phrases = ko.observableArray([]),
                    //job Titles List
                    jobTitles = ko.observableArray([]),
                    //#endregion
                    //get All Sections
                    getAllSections = function () {
                        dataservice.getSections({
                            success: function (data) {
                                sections.removeAll();
                                _.each(data, function (item) {
                                    var section = new model.Section.Create(item);
                                    _.each(item.PhrasesFields, function (phraseFieldItem) {
                                        var phraseField = new model.PhraseField.Create(phraseFieldItem);
                                        section.phrasesFields.push(phraseField);
                                    });
                                    sections.push(section);
                                });
                                selectDefaultSectionForProduct();
                            },
                            error: function () {
                                toastr.error("Failed to phrase library.");
                            }
                        });
                    },
                    //Get Phrases By Phrase Id
                    getPhrasesByPhraseFieldId = function (fieldId) {
                        dataservice.getPhrasesByPhraseFieldId({
                            fieldId: fieldId
                        }, {
                            success: function (data) {
                                phrases.removeAll();
                                _.each(data, function (phraseItem) {
                                    var phrase = new model.Phrase.Create(phraseItem);
                                    selectedPhraseField().phrases.push(phrase);
                                });
                                ko.utils.arrayPushAll(phrases, selectedPhraseField().phrases());
                                phrases.valueHasMutated();
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Failed to Load Stores . Error: " + response);
                            }
                        });
                    },
                   //select Section(Category)
                   selectSection = function (section) {
                       //old menu collapse
                       if (selectedSection() !== undefined) {
                           selectedSection().isExpanded(false);
                       }
                       //new selected section expand
                       section.isExpanded(true);
                       selectedSection(section);
                       selectedPhraseField(undefined);
                       phrases.removeAll();
                   },
                   //select Phrase Field
                   selectPhraseField = function (phraseField) {
                       phrases.removeAll();
                       //If open from other than phase library secreen like Product
                       if (!isOpenFromPhraseLibrary()) {
                           if (phraseField.sectionId() === 4) {
                               selectedPhraseField(phraseField);
                               if (phraseField.phrases().length > 0) {
                                   ko.utils.arrayPushAll(phrases, phraseField.phrases());
                                   phrases.valueHasMutated();
                               } else {
                                   getPhrasesByPhraseFieldId(phraseField.fieldId());
                               }
                           }
                       }
                           //If Open From Phase Library Screen
                       else {
                           selectedPhraseField(phraseField);
                           if (phraseField.phrases().length > 0) {
                               ko.utils.arrayPushAll(phrases, phraseField.phrases());
                               phrases.valueHasMutated();
                           } else {
                               getPhrasesByPhraseFieldId(phraseField.fieldId());
                           }
                       }

                       if (!selectedPhraseField()) {
                           return;
                       }

                       // Reset Checked State for Phrases
                       selectedPhraseField().phrases.each(function (phrase) {
                           phrase.isPhraseChecked(false);
                       });
                   },
                   //Delete Phrase
                   deletePhrase = function (phrase) {
                       phrase.isDeleted(true);
                   },
                   //Save Phrase Library
                   savePhraseLibrary = function (phraseLibrary) {
                       var phraseLibrarySaveModel = model.PhraseLibrarySaveModel();
                       var severModel = phraseLibrarySaveModel.convertToServerData(phraseLibrarySaveModel);
                       _.each(sections(), function (item) {
                           if (item.phrasesFields().length > 0) {
                               var section = item.convertToServerData(item);
                               _.each(item.phrasesFields(), function (phraseFiledItem) {
                                   var phraseField = phraseFiledItem.convertToServerData(phraseFiledItem);
                                   if (phraseFiledItem.phrases().length > 0) {
                                       _.each(phraseFiledItem.phrases(), function (phraseItem) {
                                           if (phraseItem.hasChanges()) {
                                               phraseField.Phrases.push(phraseItem.convertToServerData(phraseItem));
                                           }
                                       });
                                   }
                                   section.PhrasesFields.push(phraseField);
                               });
                               severModel.Sections.push(section);
                           }
                       });
                       saveLibrary(severModel);
                   },
                   //
                   saveLibrary = function (phaseLibrary) {
                       dataservice.savePhaseLibrary(
                               phaseLibrary, {
                                   success: function (data) {
                                       //view.hidePhraseLibraryDialog();
                                       toastr.success("Successfully save.");
                                   },
                                   error: function (response) {
                                       toastr.error("Failed to Save . Error: " + response);
                                   }
                               });
                   },
                   //Add Phrase
                   addPhrase = function () {
                       if (selectedPhraseField() != undefined) {
                           selectedPhraseField().phrases.splice(0, 0, model.Phrase(0, "", selectedPhraseField().fieldId()));
                           phrases.splice(0, 0, selectedPhraseField().phrases()[0]);
                       }
                   },
                   //Edit Job Title (open edit job title dialog)
                   editJobTitle = function () {
                       jobTitles.removeAll();
                       _.each(sections(), function (section) {
                           if (section.sectionId() === 4) {
                               ko.utils.arrayPushAll(jobTitles(), section.phrasesFields());
                               jobTitles.valueHasMutated();
                           }
                       });
                       view.showEditJobTitleModalDialog();
                   },
                   //Close Edit job Title Dialog
                   closeEditJobDialog = function () {
                       view.hideEditJobTitleDialog();
                   },
                   //Template To Use
                    templateToUse = function () {
                        if (isOpenFromPhraseLibrary()) {
                            return 'phraseEditItemTemplate';
                        } else {
                            return 'phraseItemTemplate';
                        }
                    },
                    //Select Phrase
                    selectPhrase = function (phrase) {
                        if (phrase.isPhraseChecked()) {
                            view.hidePhraseLibraryDialog();
                            if (afterSelectPhrase && typeof afterSelectPhrase === "function") {
                                afterSelectPhrase(phrase.phraseText());
                                afterSelectPhrase = null;
                            }
                            var phraseLibrarySaveModel = model.PhraseLibrarySaveModel();
                            var severModel = phraseLibrarySaveModel.convertToServerData(phraseLibrarySaveModel);
                            _.each(sections(), function (item) {
                                if (item.sectionId() === 4) {
                                    var section = item.convertToServerData(item);
                                    _.each(item.phrasesFields(), function (phraseFiledItem) {
                                        var phraseField = phraseFiledItem.convertToServerData(phraseFiledItem);
                                        if (phraseFiledItem.phrases().length > 0) {
                                            _.each(phraseFiledItem.phrases(), function (phraseItem) {
                                                if (phraseItem.hasChanges()) {
                                                    phraseField.Phrases.push(phraseItem.convertToServerData(phraseItem));
                                                }
                                            });
                                        }
                                        if (phraseFiledItem.hasChanges() || phraseField.Phrases.length > 0) {
                                            section.PhrasesFields.push(phraseField);
                                        }
                                    });
                                    if (section.PhrasesFields.length > 0)
                                        severModel.Sections.push(section);
                                }
                            });

                            if (severModel.Sections.length > 0) {
                                saveLibrary(severModel);
                            }
                        }
                    },
                    // after selection
                    afterSelectPhrase = null,
                    // select default section for product
                    selectDefaultSectionForProduct = function() {
                        if (!isOpenFromPhraseLibrary()) {
                            // Select Job Production by default
                            var jobProductionSection = sections.find(function (section) {
                                return section.sectionId() === 4;
                            });

                            if (jobProductionSection) {
                                selectSection(jobProductionSection);
                                if (selectedSection() && selectedSection().phrasesFields().length > 0) {
                                    selectPhraseField(selectedSection().phrasesFields()[0]);
                                }
                            }
                        }
                    },
                    // Show
                    show = function (afterSelectPhraseCallback) {
                        selectedSection(new model.Section());
                        view.showPhraseLibraryDialog();
                        if (sections().length === 0) {
                            getAllSections();
                        }
                        else {
                            selectDefaultSectionForProduct();
                        }
                        afterSelectPhrase = afterSelectPhraseCallback;
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                    };

                return {
                    selectedSection: selectedSection,
                    selectedPhraseField: selectedPhraseField,
                    isOpenFromPhraseLibrary: isOpenFromPhraseLibrary,
                    selectedPhrase: selectedPhrase,
                    //Arrays
                    sections: sections,
                    phrases: phrases,
                    //Utilities
                    initialize: initialize,
                    selectSection: selectSection,
                    selectPhraseField: selectPhraseField,
                    deletePhrase: deletePhrase,
                    savePhraseLibrary: savePhraseLibrary,
                    addPhrase: addPhrase,
                    editJobTitle: editJobTitle,
                    closeEditJobDialog: closeEditJobDialog,
                    templateToUse: templateToUse,
                    selectPhrase: selectPhrase,
                    jobTitles: jobTitles,
                    show: show
                };
            })()
        };

        return ist.phraseLibrary.viewModel;
    });
