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
                    errorList = ko.observableArray([]),
                    // #region Busy Indicators
                    isLoadingsectionflags = ko.observable(false),
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
                                }

                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                    // save flags
                    saveFlag = function () {
                        var list = [];
                        var obj = { SectionFlags: [] };

                        _.each(selectedsection().sectionflags(), function (flag) {
                            var serverObj = flag.convertToServerData();
                            obj.SectionFlags.push(serverObj);
                        });
                        if (obj.SectionFlags.length == 0) {
                            var flag = new model.SectionFlag();
                            flag.id(-1);
                            flag.sectionId(selectedsection().id());
                            obj.SectionFlags.push(flag.convertToServerData()); // dummy to avoid null on server
                        }
                        dataservice.saveSectionFlags(obj, {
                            success: function (data) {
                                selectedsection().reset();
                                toastr.success("Seccessfully updated!");
                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                    // Delete Section Flag
                    deleteSectionFlag = function (flag) {
                        var selectedFlag = selectedsection().sectionflags.find(function (item) {
                            return item.id() == flag.id();
                        });
                        selectedsection().sectionflags.remove(selectedFlag);
                    },
                    //Add section flag
                    addSectionFlag = function () {
                        var obj = new model.SectionFlag();
                        obj.sectionId(selectedsection().id());
                        selectedsection().sectionflags.push(obj);
                    },
                    sectionHasChanged=  ko.computed(function() {
                        var hasChanges = false;
                        if (selectedsection()) {
                            hasChanges = selectedsection().hasChanges();
                        }
                        return hasChanges;
                    }),
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
                    deleteSectionFlag: deleteSectionFlag,
                    addSectionFlag: addSectionFlag,
                    saveFlag:saveFlag,
                    selectedsection: selectedsection,
                    sectionHasChanged: sectionHasChanged

                };
            })()
        };
        return ist.sectionflags.viewModel;
    });
