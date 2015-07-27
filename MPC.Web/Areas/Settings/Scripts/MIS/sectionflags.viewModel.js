/*
    Module with the view model for the My Organization.
*/
define("sectionflags/sectionflags.viewModel",
    ["jquery", "amplify", "ko", "sectionflags/sectionflags.dataservice", "sectionflags/sectionflags.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.sectionflags = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    selectedsectionflags = ko.observableArray(),
                    selectedsection = ko.observable(undefined),
                     //Filtered Markups
                    filteredMarkups = ko.observableArray([]),
                    errorList = ko.observableArray([]),
                    // #region Busy Indicators
                    isLoadingsectionflags = ko.observable(false),
                    
                    idCounter = ko.observable(-1),
                    selectedFlag = ko.observable(),
                    // #endregion Busy Indicators
                    // #region Observables
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getBase();
                    },
                    // get Section Flags
                    getSectionFlags = function (section) {
                        if (selectedsection() !== undefined && selectedsection().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(saveFlag);
                            confirmation.afterCancel(function () {
                                selectedsection(section);
                                getFlags();
                            });
                            confirmation.show();
                            return;
                        } else {
                            selectedsection(section);
                            getFlags();
                        }
                    },
                    // section flags
                    getFlags = function () {
                        dataservice.getSectionFlags({ sectionId: selectedsection().id(), }, {
                            success: function (data) {
                                selectedsection().sectionflags.removeAll();
                                if (data != null) {
                                    _.each(data, function (item) {
                                        selectedsection().sectionflags.push(new model.SectionFlag.Create(item));
                                    });
                                    selectedsection().reset();
                                    filteredMarkups.removeAll();
                                }

                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                    // save flags
                    saveFlag = function () {
                        if (!doBeforeSave()) {
                            return;
                        }
                        var list = [];
                        var obj = { SectionFlags: [] };

                        _.each(selectedsection().sectionflags(), function (flag) {
                            var serverObj = flag.convertToServerData();
                            obj.SectionFlags.push(serverObj);
                        });
                        if (obj.SectionFlags.length == 0) {
                            var flag = new model.SectionFlag();
                            flag.id(0);
                            flag.sectionId(selectedsection().id());
                            obj.SectionFlags.push(flag.convertToServerData()); // dummy to avoid null on server
                        }
                        dataservice.saveSectionFlags(obj, {
                            success: function (data) {
                                filteredMarkups.removeAll();
                                selectedsection().reset();
                                toastr.success("Successfully updated!");
                                getFlags();
                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                    // Delete Section Flag
                    oNdeleteSectionFlag = function (flag) {
                        selectedFlag(flag);
                        confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                            confirmation.afterProceed(deleteSectionFlaf);
                            confirmation.afterCancel(function () {

                            });
                            confirmation.show();
                    },
                    deleteSectionFlaf= function() {
                        var obj = selectedsection().sectionflags.find(function (item) {
                            return item.id() == selectedFlag().id();
                        });
                        if ((obj !== undefined && obj !== null) && (obj.isDefault()===undefined || obj.isDefault()===null || !obj.isDefault())) {
                            selectedsection().sectionflags.remove(obj);
                        } else {
                            toastr.error('Default flag can not be deleted!');
                        }
                    },
                     // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedsection().isValid()) {
                            selectedsection().showAllErrors();
                            selectedsection().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    //Add section flag
                    addSectionFlag = function () {
                        var markup = filteredMarkups()[0];
                        if ((filteredMarkups().length === 0) || (markup !== undefined && markup !== null && markup.name() !== undefined  && markup.isValid())) {
                            var newMarkup = model.SectionFlag();
                            newMarkup.id(idCounter());
                            idCounter(idCounter()-1);
                            newMarkup.sectionId(selectedsection().id());
                            selectedsection().sectionflags.push(newMarkup);
                            filteredMarkups.splice(0, 0, newMarkup);
                        }
                        if (markup !== undefined && markup !== null && !markup.isValid()) {
                            markup.errors.showAllMessages();
                        }
                    },
                    sectionHasChanged=  ko.computed(function() {
                        var hasChanges = false;
                        if (selectedsection()) {
                            hasChanges = selectedsection().hasChanges();
                        }
                        return hasChanges;
                    }),
                     // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    closeSectionScreen = function () {
                        if (selectedsection() !== undefined && selectedsection().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(saveFlag);
                            confirmation.afterCancel(function () {
                                window.open("..", '_parent');
                            });
                            confirmation.show();
                            return;
                        } else {
                            window.open("..", '_parent');
                        }
                       
                    },
                    clearErrorSummary= function() {
                        errorList.removeAll();
                    },
                    //Get Prefix
                    getBase = function () {
                        isLoadingsectionflags(true);
                        dataservice.getSections({
                            success: function (data) {
                                // getPrefixByOrganisationId();
                                if (data != null) {
                                    _.each(data, function (item) {
                                        selectedsectionflags.push(new model.Section.Create(item));
                                    });
                                    getSectionFlags(selectedsectionflags()[0]);
                                }

                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    };



                return {
                    // Observables
                    selectedsectionflags: selectedsectionflags,
                    // Utility Methods
                    initialize: initialize,
                    getSectionFlags: getSectionFlags,
                    oNdeleteSectionFlag: oNdeleteSectionFlag,
                    addSectionFlag: addSectionFlag,
                    saveFlag:saveFlag,
                    selectedsection: selectedsection,
                    sectionHasChanged: sectionHasChanged,
                    gotoElement:gotoElement,
                    errorList: errorList,
                    closeSectionScreen: closeSectionScreen,
                    clearErrorSummary: clearErrorSummary,
                    selectedFlag: selectedFlag

                };
            })()
        };
        return ist.sectionflags.viewModel;
    });
