/*
    Module with the view model for the My Organization.
*/
define("paperSheet/paperSheet.viewModel",
    ["jquery", "amplify", "ko", "paperSheet/paperSheet.dataservice", "paperSheet/paperSheet.model", "common/confirmation.viewModel", "common/pagination"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.paperSheet = {
            viewModel: (function () {
                var
                    view,
                    paperSheets = ko.observableArray([]),
                    selectedPaperSheet = ko.observable(),
                    selectedPaperSheetCopy = ko.observable(),
                    isLoadingPaperSheet = ko.observable(false),
                    sortOn = ko.observable(1),
                    sortIsAsc = ko.observable(true),
                    pager = ko.observable(),

                    templateToUse = function (paperSheet) {
                        //return (paperSheet === selectedPaperSheet() ? 'editPaperSheetTemplate' : 'itemPaperSheetTemplate');
                    },

                    makeEditable = ko.observable(false),
                    selectPaperSheet = function (paperSheet) {
                        if (selectedPaperSheet() !== paperSheet) {
                            selectedPaperSheet(paperSheet);
                        }
                        //if (selectedTaxRate() === taxRate) {
                        //    makeEditable(true);
                        //    return (selectedTaxRate() === taxRate && taxRate() ? "editTaxRateTemplate" : "itemTaxRateTemplate");
                        //}
                        //isEditable(true);
                    },
                    createNewPaperSheet = function () {
                        //var paperSheet = new model.PaperSheet();
                       // selectedPaperSheet(paperSheet);
                       // paperSheets.splice(0, 0, paperSheet);
                        paperSheets.splice(0, 0, model.PaperSheet());
                        selectedPaperSheet(paperSheets()[0]);
                        openEditDialog();
                    },
                    deletePaperSheet = function (paperSheet) {
                        dataservice.deletePaperSheet({
                            PaperSheetId: selectedPaperSheet().paperSizeId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    paperSheets.remove(paperSheet);
                                    toastr.success(" Deleted Successfully !");
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Delete . Error: " + response);
                            }
                        });
                    },
                    getPaperSheets = function () {
                        isLoadingPaperSheet(true);
                        dataservice.getPaperSheets({
                            success: function (data) {
                                paperSheets.removeAll();
                                if (data != null) {
                                    _.each(data.PaperSheets, function (item) {
                                        var module = model.paperSheetClientMapper(item);
                                        paperSheets.push(module);
                                    });
                                    if (paperSheets().length > 0) {
                                        selectedPaperSheet(paperSheets()[0]);
                                    }
                                }
                                isLoadingPaperSheet(false);
                            },
                            error: function () {
                                isLoadingPaperSheet(false);
                                toastr.error(ist.resourceText.loadAddChargeDetailFailedMsg);
                            }
                        });
                    },
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedPaperSheet().isValid()) {
                            selectedPaperSheet().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    //Save Paper Sheet
                    savePaperSheet = function () {
                        if (doBeforeSave()) {
                            if (selectedPaperSheet().paperSizeId()> 0) {
                                saveEdittedPaperSheet();
                            } else {
                                saveNewPaperSheet();
                            }
                        }
                    },
                    saveNewPaperSheet = function() {
                        dataservice.saveNewPaperSheet(model.paperSheetServerMapper(selectedPaperSheet()), {
                            success: function (data) {
                                paperSheets.splice(0, 0, selectedPaperSheet());
                                view.hidePaperSheetDialog();
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
                    saveEdittedPaperSheet = function() {
                            dataservice.savePaperSheet(model.paperSheetServerMapper(selectedPaperSheet()), {
                                success: function (data) {
                                    paperSheets.splice(0, 0, selectedPaperSheet());
                                    view.hidePaperSheetDialog();
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
                    openEditDialog = function () {
                        view.showPaperSheetDialog();
                        selectedPaperSheetCopy(selectedPaperSheet());
                    },
                    closeEditDialog = function() {
                        if (selectedPaperSheet().paperSizeId() > 0) {
                            view.hidePaperSheetDialog();
                            selectedPaperSheet(selectedPaperSheetCopy());
                        } else {
                            view.hidePaperSheetDialog();
                            paperSheets.remove(selectedPaperSheet());
                        }
                    }

                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getPaperSheets();
                    // Set Pager
                    //pager(new pagination.Pagination({}, additionalTypeCharges, getAdditionalCharges));
                    //getAdditionalCharges();
                    // selectedPaperSheet(new model.CompanySites());
                };

                return {
                    paperSheets: paperSheets,
                    selectedPaperSheet: selectedPaperSheet,
                    selectedPaperSheetCopy: selectedPaperSheetCopy,
                    isLoadingPaperSheet: isLoadingPaperSheet,
                    deletePaperSheet: deletePaperSheet,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    selectPaperSheet: selectPaperSheet,
                    createNewPaperSheet: createNewPaperSheet,
                    getPaperSheets: getPaperSheets,
                    doBeforeSave: doBeforeSave,
                    savePaperSheet: savePaperSheet,
                    saveNewPaperSheet: saveNewPaperSheet,
                    saveEdittedPaperSheet: saveEdittedPaperSheet,
                    openEditDialog: openEditDialog,
                    closeEditDialog: closeEditDialog,
                    initialize: initialize,
                };
            })()
        };
        return ist.paperSheet.viewModel;
    });
