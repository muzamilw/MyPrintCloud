
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
                    //height and weight unit
                    unit = ko.observable(),
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
                    selectedPaperSheetForDelete = ko.observable(),
                    //Length Unit
                    lengthUnit = ko.observable(),
                    //Organisation culure
                    orgCulture = ko.observable(),
                    //Template To Use
                    templateToUse = function (paperSheet) {
                        return (paperSheet === selectedPaperSheet() ? 'editPaperSheetTemplate' : 'itemPaperSheetTemplate');
                    },
                    //Make Edittable
                    makeEditable = ko.observable(false),
                    //Create New Paper Sheet
                    createNewPaperSheet = function () {
                        var paperSheet = new model.PaperSheet();
                        //paperSheet.IsImperical("true");
                        editorViewModel.selectItem(paperSheet);
                        selectedPaperSheet().isArchived(false);
                        openEditDialog();
                        selectedPaperSheet().IsImperical("true");
                        selectedPaperSheet().reset();
                    },
                    //On Edit Click Of Paper Sheet
                    onEditItem = function (item) {
                      
                        if (item.IsImperical() == true || item.IsImperical() == "true") {
                            item.IsImperical("true");
                        }
                        else {
                            item.IsImperical("false");
                        }
                           

                        selectedPaperSheetForDelete(item);


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
                                    paperSheets.remove(selectedPaperSheetForDelete());
                                    view.hidePaperSheetDialog();
                                    toastr.success("Successfully archive!");
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
                        confirmation.messageText("WARNING - Item will be removed from the system and you won’t be able to recover.  There is no undo");
                        confirmation.afterProceed(function () {
                            deletePaperSheet(paperSheet);
                        });
                        confirmation.show();
                    },
                    //GET Paper Sheets
                    getPaperSheets = function () {
                        isLoadingPaperSheet(true);
                        dataservice.getPaperSheets({
                            SearchString: searchFilter(),
                            Region: orgCulture(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                paperSheets.removeAll();
                                if (data != null) {
                                    _.each(data.PaperSheets, function (item) {
                                        var module = model.paperSheetServertoClientMapper(item);
                                        paperSheets.push(module);
                                    });
                                    pager().totalCount(data.RowCount);
                                }
                                isLoadingPaperSheet(false);
                            },
                            error: function (response) {
                                isLoadingPaperSheet(false);
                                toastr.error("Error: Failed to Load Paper Sheet Data." + response);
                            }
                        });
                    },
                    getPaperSheetsByFilter = function () {
                        pager().reset();
                        getPaperSheets();
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
                        dataservice.saveNewPaperSheet(selectedPaperSheet().convertToServerData(orgCulture()), {
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
                        dataservice.savePaperSheet(selectedPaperSheet().convertToServerData(orgCulture()), {
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
                                if (exceptionType === ist.exceptionType.MPCGeneralException) {
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
                        view.initializeLabelPopovers();


                     
                        //if (selectedPaperSheet().IsImperical() == true || selectedPaperSheet().IsImperical() == "true") {
                        //        selectedPaperSheet().IsImperical("true");
                        //}
                        // else {
                        //        selectedPaperSheet().IsImperical("false");
                        //}
                        
                       // ist.paperSheet
                       

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
                     resetFilterSection = function () {
                         searchFilter(undefined);
                         getPaperSheets();
                     },

                    // Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                if (data) {
                                    lengthUnit(data.LengthUnit);
                                    orgCulture(data.Culture);
                                    getPaperSheets();
                                }
                            },
                            error: function (response) {
                                getPaperSheets();
                            }
                        });
                    },
                    // On Close Editor
                    onClosePaperSheet = function () {
                        if (selectedPaperSheet().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(savePaperSheet);
                            confirmation.afterCancel(function () {
                                selectedPaperSheet().reset();
                                view.hidePaperSheetDialog();
                            });
                            confirmation.show();
                            return;
                        }
                        view.hidePaperSheetDialog();
                    },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 10 }, paperSheets, getPaperSheets));
                    getBaseData();
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
                    resetFilterSection: resetFilterSection,
                    editorViewModel: editorViewModel,
                    onEditItem: onEditItem,
                    lengthUnit: lengthUnit,
                    unit: unit,
                    getBaseData: getBaseData,
                    onClosePaperSheet: onClosePaperSheet,
                    initialize: initialize,
                    getPaperSheetsByFilter: getPaperSheetsByFilter,
                    orgCulture: orgCulture
                };
            })()
        };
        return ist.paperSheet.viewModel;
    });
