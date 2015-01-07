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
                    //View
                    view,
                    //Paper Sheets
                    paperSheets = ko.observableArray([]),
                    //Is Loading Paper Sheet
                    isLoadingPaperSheet = ko.observable(false),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    // Editor View Model
                    editorViewModel = new ist.ViewModel(model.PaperSheet),
                    //Selected Paper Sheet
                    selectedPaperSheet = editorViewModel.itemForEditing,
                    //Template To Use
                    templateToUse = function (paperSheet) {
                        return (paperSheet === selectedPaperSheet() ? 'editPaperSheetTemplate' : 'itemPaperSheetTemplate');
                    },
                    //Make Edittable
                    makeEditable = ko.observable(false),
                    //Create New Paper Sheet
                    createNewPaperSheet = function () {
                        var paperSheet = new model.PaperSheet();
                        editorViewModel.selectItem(paperSheet);
                        openEditDialog();
                    },
                    //On Edit Click Of Paper Sheet
                    onEditItem = function (item) {
                        editorViewModel.selectItem(item);
                        openEditDialog();
                    },
                    //Delete Paper Sheet
                    deletePaperSheet = function (paperSheet) {
                        dataservice.deletePaperSheet({
                            PaperSheetId: paperSheet.paperSizeId(),
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
                    onDeletePaperSheet = function (paperSheet) {
                        if (!paperSheet.paperSizeId()) {
                            paperSheets.remove(paperSheet);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deletePaperSheet(paperSheet);
                        });
                        confirmation.show();
                    },
                    //GET Paper Sheets
                    getPaperSheets = function () {
                        isLoadingPaperSheet(true);
                        dataservice.getPaperSheets({
                            PaperSheetFilterText: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                paperSheets.removeAll();
                                if (data != null) {
                                    pager().totalCount(data.RowCount);
                                    _.each(data.PaperSheets, function (item) {
                                        var module = model.paperSheetServertoClientMapper(item);
                                        paperSheets.push(module);
                                    });
                                }
                                isLoadingPaperSheet(false);
                            },
                            error: function (response) {
                                isLoadingPaperSheet(false);
                                toastr.error("Error: Failed to Load Paper Sheet Data." + response);
                            }
                        });
                    },
                    //Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedPaperSheet().isValid()) {
                            selectedPaperSheet().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    //Save Paper Sheet
                    savePaperSheet = function (item) {
                        if (selectedPaperSheet() != undefined && doBeforeSave()) {
                            if (selectedPaperSheet().paperSizeId() > 0) {
                                saveEdittedPaperSheet();
                            } else {
                                saveNewPaperSheet(item);
                            }
                        }
                    },
                    //Save NEW Paper Sheets
                    saveNewPaperSheet = function () {
                        dataservice.saveNewPaperSheet(selectedPaperSheet().convertToServerData(), {
                            success: function (data) {
                                selectedPaperSheet().paperSizeId(data.PaperSizeId);
                                paperSheets.splice(0, 0, selectedPaperSheet());
                                view.hidePaperSheetDialog();
                                toastr.success("Successfully save.");
                            },
                            error: function (response) {
                                    toastr.error("Failed to save." + response);
                            }
                        });
                    },
                    //Save EDIT Paper Sheets
                    saveEdittedPaperSheet = function () {
                        dataservice.savePaperSheet(selectedPaperSheet().convertToServerData(), {
                            success: function (data) {
                                var newItem = model.paperSheetServertoClientMapper(data);
                                var newObjtodelete = paperSheets.find(function (temp) {
                                    return temp.paperSizeId() == newItem.paperSizeId();
                                });
                                paperSheets.remove(newObjtodelete);
                                paperSheets.push(newItem);
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
                    //Open Paper Sheet Dialog
                    openEditDialog = function () {
                        view.showPaperSheetDialog();
                    },
                    //CLose Paper Sheet Dialog
                    closeEditDialog = function () {
                        if (selectedPaperSheet() != undefined) {
                            if (selectedPaperSheet().paperSizeId() > 0) {
                                view.hidePaperSheetDialog();
                            } else {
                                view.hidePaperSheetDialog();
                                paperSheets.remove(selectedPaperSheet());
                            }
                            editorViewModel.revertItem();
                        }
                    },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 10 }, paperSheets, getPaperSheets));
                    getPaperSheets();
                };

                return {
                    paperSheets: paperSheets,
                    selectedPaperSheet: selectedPaperSheet,
                    isLoadingPaperSheet: isLoadingPaperSheet,
                    deletePaperSheet: deletePaperSheet,
                    onDeletePaperSheet: onDeletePaperSheet,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    createNewPaperSheet: createNewPaperSheet,
                    getPaperSheets: getPaperSheets,
                    doBeforeSave: doBeforeSave,
                    savePaperSheet: savePaperSheet,
                    saveNewPaperSheet: saveNewPaperSheet,
                    saveEdittedPaperSheet: saveEdittedPaperSheet,
                    openEditDialog: openEditDialog,
                    closeEditDialog: closeEditDialog,
                    searchFilter: searchFilter,
                    editorViewModel: editorViewModel,
                    onEditItem: onEditItem,
                    initialize: initialize,
                };
            })()
        };
        return ist.paperSheet.viewModel;
    });
