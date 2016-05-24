/*
    Module with the view model for the item Job Status.
*/
define("itemJobStatus/itemJobStatus.viewModel",
    ["jquery", "amplify", "ko", "itemJobStatus/itemJobStatus.dataservice", "itemJobStatus/itemJobStatus.model", "common/sharedNavigation.viewModel", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, shared, reportManager) {
        var ist = window.ist || {};
        ist.itemJobStatus = {
            viewModel: (function () {
                var // the view 
                    view,
                    //Currency Symbol
                    currencySymbol = ko.observable(),
                    columnlable1 = ko.observable(),
                    columnlable2 = ko.observable(),
                    columnlable3 = ko.observable(),
                    columnlable4 = ko.observable(),
                    columnlable5 = ko.observable(),
                    columnlable6 = ko.observable(),
                    // #region Arrays
                    //Items
                    items = ko.observableArray([]),
                    // Item Status
                    jobStatuses = ko.observableArray([
                        {
                            StatusId: 11,
                            StatusName: "Need Assigning"
                        },
                        {
                            StatusId: 12,
                            StatusName: "In Studio"
                        },
                        {
                            StatusId: 13,
                            StatusName: "In Print/Press"
                        },
                        {
                            StatusId: 14,
                            StatusName: "In Post Press/Bindery"
                        },
                        {
                            StatusId: 15,
                            StatusName: "Ready for Shipping"
                        },
                        {
                            StatusId: 16,
                            StatusName: "Shipped & Invoiced"
                        }
                    ]),
                    // #endregion
                    // #region Observables
                    selectedItem = ko.observable(),
                    // #endregion
                    dragged = function (source) {
                        return {
                            row: source.$parent,
                            item: source.$data
                        };
                    },
                    droppedNeedAssigning = function (source, target, event) {
                        if (source !== undefined && source !== null && source.item !== undefined && source.item !== null && source.item.statusId() !== 11) {
                            source.item.statusId(11);
                            saveIitem(source.item);
                        }
                    },

                    droppedInStudio = function (source, target, event) {
                        if (source !== undefined && source !== null && source.item !== undefined && source.item !== null && source.item.statusId() !== 12) {
                            source.item.statusId(12);
                            saveIitem(source.item);
                        }
                    },

                    droppedInPrint = function (source, target, event) {
                        if (source !== undefined && source !== null && source.item !== undefined && source.item !== null && source.item.statusId() !== 13) {
                            source.item.statusId(13);
                            saveIitem(source.item);
                        }
                    },

                    droppedInPostPress = function (source, target, event) {
                        if (source !== undefined && source !== null && source.item !== undefined && source.item !== null && source.item.statusId() !== 14) {
                            source.item.statusId(14);
                            saveIitem(source.item);
                        }
                    },

                    droppedInReadyForShipping = function (source, target, event) {
                        if (source !== undefined && source !== null && source.item !== undefined && source.item !== null && source.item.statusId() !== 15) {
                            source.item.statusId(15);
                            saveIitem(source.item);
                        }
                    },
                    droppedInInvoiceAndShipped = function (source, target, event) {
                        if (source !== undefined && source !== null && source.item !== undefined && source.item !== null && source.item.statusId() !== 16) {
                            source.item.statusId(16);
                            saveIitem(source.item);
                        }
                    },

                    saveIitem = function (item) {
                        dataservice.saveItem(item.convertToServerData(), {
                            success: function (data) {

                                toastr.success("Job status successfully updated.");
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
                    //#region Utility funntions
                    needAssigningTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),
                    inStudioTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),
                    inPrintTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),
                    inPostPressTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),
                    inReadyForShippingTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),
                    inInvoiceAndShippedTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),

                    calculateTotal = ko.computed(function () {
                        needAssigningTotal(0);
                        inStudioTotal(0);
                        inPrintTotal(0);
                        inPostPressTotal(0);
                        inReadyForShippingTotal(0);
                        inInvoiceAndShippedTotal(0);
                        _.each(items(), function (item) {
                            var qty1NetTotal = item.qty1NetTotal() === undefined || item.qty1NetTotal() === null ? 0 : item.qty1NetTotal();
                            // 1 -> Late started  2-> Late delivery 
                           if (item.statusId() === 11 || item.statusId() === 1) {
                                var total = (parseFloat(needAssigningTotal()) + parseFloat(qty1NetTotal));
                                total.toFixed(2);
                                needAssigningTotal(total);  // also being used in  late start sectoin as total 
                            }
                           else if (item.statusId() === 12 || item.statusId() === 2) {
                                var total1 = (parseFloat(inStudioTotal()) + parseFloat(qty1NetTotal));
                                total1.toFixed(2);
                                inStudioTotal(total1);    // also being used in late delivery section as total 
                            }
                            else if (item.statusId() === 13) {
                                var total2 = (parseFloat(inPrintTotal()) + parseFloat(qty1NetTotal));
                                total2.toFixed(2);
                                inPrintTotal(total2);
                            }
                            else if (item.statusId() === 14) {
                                var total3 = (parseFloat(inPostPressTotal()) + parseFloat(qty1NetTotal));
                                total3.toFixed(2);
                                inPostPressTotal(total3);
                            }
                            else if (item.statusId() === 15) {
                                var total4 = (parseFloat(inReadyForShippingTotal()) + parseFloat(qty1NetTotal));
                                total4.toFixed(2);
                                inReadyForShippingTotal(total4);
                            }
                            else if (item.statusId() === 16) {
                                var total5 = (parseFloat(inInvoiceAndShippedTotal()) + parseFloat(qty1NetTotal));
                                total5.toFixed(2);
                                inInvoiceAndShippedTotal(total5);
                            }
                        });
                    }),
                // Get Items job status 
                getItems = function () {
                    dataservice.getItems({
                        IsLateItemScreen: $("#HiddenFlag_12").val() !== undefined ? true : false,
                    }, {
                        success: function (data) {
                            if (data !== null && data !== undefined) {
                                currencySymbol(data.CurrencySymbol);
                                columnlable1(data.ProductionBoardLabel1);
                                columnlable2(data.ProductionBoardLabel2);
                                columnlable3(data.ProductionBoardLabel3);
                                columnlable4(data.ProductionBoardLabel4);
                                columnlable5(data.ProductionBoardLabel5);
                                columnlable6(data.ProductionBoardLabel6);
                                var itemList = [];
                                _.each(data.Items, function (item) {
                                    itemList.push(model.Item.Create(item));
                                });
                                ko.utils.arrayPushAll(items(), itemList);
                                items.valueHasMutated();
                            }

                        },
                        error: function () {
                            toastr.error("Failed to Items.");
                        }
                    });
                },
                
                 openExternalReportsJob = function (item) {

                     reportManager.outputTo("preview");


                     reportManager.OpenExternalReport(ist.reportCategoryEnums.JobCards, 1, item.id());





                 },

                //Initialize
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   getItems();

               };
                //#endregion 


                return {
                    initialize: initialize,
                    items: items,
                    dragged: dragged,
                    droppedInStudio: droppedInStudio,
                    droppedNeedAssigning: droppedNeedAssigning,
                    droppedInPrint: droppedInPrint,
                    droppedInPostPress: droppedInPostPress,
                    droppedInReadyForShipping: droppedInReadyForShipping,
                    droppedInInvoiceAndShipped: droppedInInvoiceAndShipped,
                    currencySymbol: currencySymbol,
                    needAssigningTotal: needAssigningTotal,
                    inStudioTotal: inStudioTotal,
                    inPrintTotal: inPrintTotal,
                    inPostPressTotal: inPostPressTotal,
                    inReadyForShippingTotal: inReadyForShippingTotal,
                    inInvoiceAndShippedTotal: inInvoiceAndShippedTotal,
                    openExternalReportsJob: openExternalReportsJob
                };
            })()
        };
        return ist.itemJobStatus.viewModel;
    });
