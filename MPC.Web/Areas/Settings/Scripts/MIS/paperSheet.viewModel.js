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
                    isLoadingPaperSheet = ko.observable(false),
                    sortOn = ko.observable(1),
                    sortIsAsc = ko.observable(true),
                    pager = ko.observable(),

                    templateToUse = function (paperSheet) {
                        //return (paperSheet === selectedPaperSheet() ? 'editPaperSheetTemplate' : 'itemPaperSheetTemplate');
                    },

                    //paperSheets,selectedPaperSheet getBase isLoadingPaperSheet  sortOn sortIsAsc pager templateToUse makeEditable 
                    //selectPaperSheet createNewPaperSheet onDeletePaperSheet getPaperSheetById onSavePaperSheet doBeforeSave savePaperSheet

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
                        $('#myModal').modal('show');
                        var paperSheet = paperSheets()[0];
                        if (paperSheet.name() !== undefined)//&& paperSheet.tax1() !== undefined
                        {
                            paperSheets.splice(0, 0, model.PaperSheet());
                        }
                    },
                    onDeletePaperSheet = function (paperSheet) {
                        if (!paperSheet.id()) {
                            paperSheets.remove(paperSheet);
                            return;
                        }

                        confirmation.afterProceed(function () {
                            paperSheets.remove(paperSheet);
                        });
                        confirmation.show();
                    },
                    getPaperSheets = function (paperSheet) {
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
                    onSavePaperSheet = function (myOrg) {
                        if (doBeforeSave()) {

                            //addCharge.additionalChargesList.removeAll();
                            //ko.utils.arrayPushAll(addCharge.additionalChargesList(), additionalCharges());
                            savePaperSheet(myOrg);
                        }
                    },
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedPaperSheet().isValid()) {
                            selectedPaperSheet().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    savePaperSheet = function (paparSheet) {
                        //dataservice.savePaperSheet(model.CompanySitesServerMapper(paparSheet), {
                        //    success: function (data) {
                        //        var additionalCharge = data;
                        //        //if (selectedAdditionalChargeType().id() > 0) {
                        //        //    selectedAdditionalChargeType().isEditable(additionalCharge.isEditable()),
                        //        //        closeAdditionalChargeEditor();
                        //        //} else {
                        //        //    additionalTypeCharges.splice(0, 0, additionalCharge);
                        //        //    closeAdditionalChargeEditor();
                        //        //}
                        //        toastr.success("Successfully save.");
                        //    },
                        //    error: function (exceptionMessage, exceptionType) {

                        //        if (exceptionType === ist.exceptionType.CaresGeneralException) {

                        //            toastr.error(exceptionMessage);

                        //        } else {

                        //            toastr.error("Failed to save.");

                        //        }

                        //    }
                        //});
                    },
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
                    isLoadingPaperSheet: isLoadingPaperSheet,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    selectPaperSheet: selectPaperSheet,
                    createNewPaperSheet: createNewPaperSheet,
                    onDeletePaperSheet: onDeletePaperSheet,
                    getPaperSheets: getPaperSheets,
                    onSavePaperSheet: onSavePaperSheet,
                    doBeforeSave: doBeforeSave,
                    savePaperSheet: savePaperSheet,
                    initialize: initialize,
                };
            })()
        };
        return ist.paperSheet.viewModel;
    });
