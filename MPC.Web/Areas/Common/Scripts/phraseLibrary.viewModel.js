/*
    Module with the view model for Phrase Library
*/
define("common/phraseLibrary.viewModel",
    ["jquery", "amplify", "ko", "common/phraseLibrary.dataservice", "common/phraseLibrary.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
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
                    // Open default section according to 
                    defaultOpenSectionId = ko.observable(),
                    defaultOpenPhraseFieldId = ko.observable(),
                    //selected Phrase
                    selectedPhrase = ko.observable(false),
                    //Sections
                    sections = ko.observableArray([]),
                    //Phrase List
                    phrases = ko.observableArray([]),
                    //job Titles List
                    jobTitles = ko.observableArray([]),
                    // True, if new 
                    AddEditDeleteFlag = ko.observable(false),
                    //#endregion
                    //get All Sections
                    getAllSections = function () {
                        dataservice.getSections({
                            success: function (data) {
                                sections.removeAll();
                                _.each(data, function (item) {
                                    var section = new model.Section.Create(item);
                                    //_.each(item.PhrasesFields, function (phraseFieldItem) {
                                    //    var phraseField = new model.PhraseField.Create(phraseFieldItem);
                                    //    section.phrasesFields.push(phraseField);
                                    //});
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
                    getPhrasesByPhraseFieldId = function (fieldId, resetTreeExpensionAfterSave1) {
                        dataservice.getPhrasesByPhraseFieldId({
                            fieldId: fieldId
                        }, {
                            success: function (data) {
                                phrases.removeAll();
                                selectedPhraseField().phrases.removeAll();
                                _.each(data, function (phraseItem) {
                                    var phrase = new model.Phrase.Create(phraseItem);
                                    selectedPhraseField().phrases.push(phrase);
                                });
                                ko.utils.arrayPushAll(phrases, selectedPhraseField().phrases());
                                phrases.valueHasMutated();
                                if (resetTreeExpensionAfterSave1) {
                                    resetTreeExpensionAfterSave(selectedSection());

                                }

                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Failed to Load Stores . Error: " + response);
                            }
                        });
                    },
                   //select Section(Category)
                   selectSection = function (section) {
                       if (section.phrasesFields().length > 0) {
                           resetTreeExpension(section);
                       } else {
                           getPhraseFields(section);
                       }
                   },
                   //Get Phrase Fields By Setcion Id
                   getPhraseFields = function (section, afterSaveRefreshListFlag) {
                       dataservice.getPhraseFiledsBySectionId({
                           sectionId: section.sectionId(),
                       }, {
                           success: function (data) {
                               if (data != null) {
                                   section.phrasesFields.removeAll();
                                   _.each(data, function (phraseFieldItem) {
                                       var phraseField1 = new model.PhraseField.Create(phraseFieldItem);
                                       section.phrasesFields.push(phraseField1);
                                   });
                                   // true, Refresh The Phrase Fields as well as Phrases
                                   if (afterSaveRefreshListFlag) {
                                       var sectionFilter = _.filter(sections(), function (sectionItem) {
                                           return sectionItem.sectionId() === selectedPhraseField().sectionId();
                                       });
                                       if (sectionFilter.length > 0) {
                                           var phraseField = _.filter(sectionFilter[0].phrasesFields(), function (phrase) {
                                               return phrase.fieldId() === selectedPhraseField().fieldId();
                                           });
                                           if (phraseField.length > 0) {
                                               selectedPhraseField(phraseField[0]);
                                               getPhrasesByPhraseFieldId(selectedPhraseField().fieldId(), true);
                                           }
                                       }
                                   }
                                   else {
                                       resetTreeExpension(section);
                                   }

                               }
                           },
                           error: function (response) {
                               toastr.error("Failed to load Phrase Fileds . Error: ");
                           }
                       });
                   },

                   resetTreeExpension = function (section) {
                       //old menu collapse
                       if (selectedSection() !== undefined) {
                           selectedSection().isExpanded(false);
                       }
                       //new selected section expand
                       section.isExpanded(true);
                       selectedSection(section);
                       selectedPhraseField(undefined);
                       phrases.removeAll();

                       if (section.phrasesFields().length > 0) {
                           selectedPhraseField(section.phrasesFields()[0]);
                           getPhrasesByPhraseFieldId(selectedPhraseField().fieldId(), true);
                       }
                   },

                //select Phrase Field
                selectPhraseField = function (phraseField) {
                    phrases.removeAll();
                    selectedPhraseField(phraseField);
                    if (phraseField.phrases().length > 0) {
                        ko.utils.arrayPushAll(phrases, phraseField.phrases());
                        phrases.valueHasMutated();
                    } else {
                        getPhrasesByPhraseFieldId(phraseField.fieldId());
                    }

                    ////If open from other than phase library secreen like Product
                    //if (!isOpenFromPhraseLibrary()) {
                    //    if (phraseField.sectionId() === 4) {
                    //        selectedPhraseField(phraseField);
                    //        if (phraseField.phrases().length > 0) {
                    //            ko.utils.arrayPushAll(phrases, phraseField.phrases());
                    //            phrases.valueHasMutated();
                    //        } else {
                    //            getPhrasesByPhraseFieldId(phraseField.fieldId());
                    //        }
                    //    }
                    //}
                    //    //If Open From Phase Library Screen
                    //else {
                    //    selectedPhraseField(phraseField);
                    //    if (phraseField.phrases().length > 0) {
                    //        ko.utils.arrayPushAll(phrases, phraseField.phrases());
                    //        phrases.valueHasMutated();
                    //    } else {
                    //        getPhrasesByPhraseFieldId(phraseField.fieldId());
                    //    }
                    //}

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
                    confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                    confirmation.afterProceed(function () {
                        phrase.isDeleted(true);
                    });
                    confirmation.show();

                },
                //Save Phrase Library
                savePhraseLibrary = function (phraseLibrary,applyFlag) {
                    var flagForSave = false;
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
                                            flagForSave = true;
                                        }
                                    });
                                }
                               
                                section.PhrasesFields.push(phraseField);
                            });
                            severModel.Sections.push(section);
                        }
                    });
                    if (flagForSave) {
                        saveLibrary(severModel);
                    } else {
                        if (applyFlag!==true) {
                           // toastr.error("There is no phrase for save.");
                        }
                       
                    }

                },
                //
                saveLibrary = function (phaseLibrary) {
                    dataservice.savePhaseLibrary(
                            phaseLibrary, {
                                success: function (data) {
                                    refershPhraseLibraryAfterSave();
                                    toastr.success("Successfully save.");
                                },
                                error: function (response) {
                                    toastr.error("Failed to Save . Error: " + response);
                                }
                            });
                },

                  // refresh list after save phrase library
                 refershPhraseLibraryAfterSave = function () {
                     if (selectedSection() !== undefined) {
                         getPhraseFields(selectedSection(), true);
                     }
                 },
                  resetTreeExpensionAfterSave = function (section) {
                      section.isExpanded(true);
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
                     if (afterSelectPhrase && typeof afterSelectPhrase === "function") {
                         //if (phrase.phraseId() === undefined || phrase.phraseId() === 0) {
                         //    toastr.error("Please First save the phrase.");
                         //    //phrase.isPhraseChecked(false);
                         //} else {
                         //    afterSelectPhrase(phrase.phraseText());
                         //    afterSelectPhrase = null;
                         //    view.hidePhraseLibraryDialog();
                         //}
                         savePhraseLibrary(null,true);
                         afterSelectPhrase(phrase.phraseText());
                         afterSelectPhrase = null;
                         view.hidePhraseLibraryDialog();
                     }
                     //va
                     //if (phrase.isPhraseChecked()) {
                     //    if (afterSelectPhrase && typeof afterSelectPhrase === "function") {
                     //        if (phrase.phraseId() === undefined || phrase.phraseId() === 0) {
                     //            toastr.error("First save the phrase.");
                     //            phrase.isPhraseChecked(false);
                     //        } else {
                     //            afterSelectPhrase(phrase.phraseText());
                     //            afterSelectPhrase = null;
                     //            view.hidePhraseLibraryDialog();
                     //        }
                     //    }
                     //var phraseLibrarySaveModel = model.PhraseLibrarySaveModel();
                     //var severModel = phraseLibrarySaveModel.convertToServerData(phraseLibrarySaveModel);
                     //_.each(sections(), function (item) {
                     //    if (item.sectionId() === 4) {
                     //        var section = item.convertToServerData(item);
                     //        _.each(item.phrasesFields(), function (phraseFiledItem) {
                     //            var phraseField = phraseFiledItem.convertToServerData(phraseFiledItem);
                     //            if (phraseFiledItem.phrases().length > 0) {
                     //                _.each(phraseFiledItem.phrases(), function (phraseItem) {
                     //                    if (phraseItem.hasChanges()) {
                     //                        phraseField.Phrases.push(phraseItem.convertToServerData(phraseItem));
                     //                    }
                     //                });
                     //            }
                     //            if (phraseFiledItem.hasChanges() || phraseField.Phrases.length > 0) {
                     //                section.PhrasesFields.push(phraseField);
                     //            }
                     //        });
                     //        if (section.PhrasesFields.length > 0)
                     //            severModel.Sections.push(section);
                     //    }
                     //});

                     //if (severModel.Sections.length > 0) {
                     //    saveLibrary(severModel);
                     //}
                     // }
                 },
                // after selection
                 afterSelectPhrase = null,
                // select default section for product
                 selectDefaultSectionForProduct = function () {
                     if (!isOpenFromPhraseLibrary() && defaultOpenSectionId() !== undefined) {
                         // Select defailt sections
                         var defaultOpenSection = sections.find(function (section) {
                             return section.sectionId() === defaultOpenSectionId();
                         });

                         if (defaultOpenSection) {
                             selectedSection(defaultOpenSection);
                             selectSection(defaultOpenSection);
                             if (selectedSection() && selectedSection().phrasesFields().length > 0) {
                                 selectPhraseField(selectedSection().phrasesFields()[0]);
                             }
                         }
                     }
                 },
                // Show
                 show = function (afterSelectPhraseCallback) {
                     //old menu collapse
                     if (selectedSection() !== undefined) {
                         selectedSection().isExpanded(false);
                     }
                     selectedSection(new model.Section());
                     view.showPhraseLibraryDialog();
                     //if (sections().length === 0) {
                     //    getAllSections();
                     //}
                     //else {
                     //    selectDefaultSectionForProduct();
                     //}

                     getAllSections();
                     afterSelectPhrase = afterSelectPhraseCallback;
                 },
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     getAllSections();
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
                    show: show,
                    defaultOpenSectionId: defaultOpenSectionId,
                    defaultOpenPhraseFieldId: defaultOpenPhraseFieldId
                };
            })()
        };

        return ist.phraseLibrary.viewModel;
    });

