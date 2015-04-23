/*
    Module with the view model for the item Job Status.
*/
define("itemJobStatus/itemJobStatus.viewModel",
    ["jquery", "amplify", "ko", "itemJobStatus/itemJobStatus.dataservice", "itemJobStatus/itemJobStatus.model", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, shared) {
        var ist = window.ist || {};
        ist.itemJobStatus = {
            viewModel: (function () {
                var // the view 
                    view,
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
                     // Get Base
                    getItems = function () {
                        dataservice.getItems({
                            success: function (data) {
                                if (data !== null) {
                                    _.each(data, function (item) {
                                        items.push(model.Item.Create(item));
                                    });
                                }
                            },
                            error: function () {
                                toastr.error("Failed to Items.");
                            }
                        });
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
                    droppedInInvoiceAndShipped: droppedInInvoiceAndShipped

                };
            })()
        };
        return ist.itemJobStatus.viewModel;
    });
