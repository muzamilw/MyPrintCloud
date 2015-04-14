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
            if (source !== undefined && source !== null && source.item !== undefined && source.widget !== null && source.item.statusId() !== 11) {
                source.item.statusId(11);
                dataservice.saveItem(source.item.convertToServerData(), {
                    success: function (data) {

                        toastr.success("Saved Successfully.");
                    },
                    error: function (exceptionMessage, exceptionType) {

                        if (exceptionType === ist.exceptionType.MPCGeneralException) {

                            toastr.error(exceptionMessage);

                        } else {

                            toastr.error("Failed to save.");

                        }

                    }
                });
            }
        },
         dropped = function (source, target, event) {
             toastr.success("test");
             //     if (selectedCurrentPageId() !== undefined && source !== undefined && source !== null && source.widget !== undefined && source.widget !== null && source.widget.widgetControlName !== undefined && source.widget.widgetControlName() !== "") {

             //}
             //if (selectedCurrentPageId() === undefined) {
             //    toastr.error("Before add widget please select page !", "", ist.toastrOptions);
             //}
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
                    dropped: dropped,
                    droppedNeedAssigning:droppedNeedAssigning

                };
            })()
        };
        return ist.itemJobStatus.viewModel;
    });
