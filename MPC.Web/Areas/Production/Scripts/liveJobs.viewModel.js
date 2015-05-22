/*
    Module with the view model for the live Jobs.
*/
define("liveJobs/liveJobs.viewModel",
    ["jquery", "amplify", "ko", "liveJobs/liveJobs.dataservice", "liveJobs/liveJobs.model", "common/pagination"],
    function ($, amplify, ko, dataservice, model, pagination) {
        var ist = window.ist || {};
        ist.liveJobs = {
            viewModel: (function () {
                var // the view 
                    view,
                    //Currency Symbol
                    currencySymbol = ko.observable(),
                    // #region Arrays
                    //Items
                    items = ko.observableArray([]),

                    // #endregion
                    // #region Observables
                    selectedItem = ko.observable(),
                    // Search Filter
                    searchFilter = ko.observable(),
                    //Pager
                    pager = ko.observable(),
                     //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    // #endregion

                // Get Items
                getItems = function () {
                    dataservice.getItems({
                        SearchString: searchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            items.removeAll();
                            if (data !== null && data !== undefined) {
                                var itemList = [];
                                _.each(data.Items, function (item) {
                                    itemList.push(model.Item.Create(item));
                                });
                                ko.utils.arrayPushAll(items(), itemList);
                                items.valueHasMutated();

                                pager().totalCount(data.TotalCount);
                            }

                        },
                        error: function () {
                            toastr.error("Failed to Items.");
                        }
                    });
                },

                        // Get Items
                    downloadArtwork = function () {
                        dataservice.downloadArtwork({
                            success: function (data) {


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
                        pager(new pagination.Pagination({ PageSize: 5 }, items, getItems));
                        getItems();

                    };
                //#endregion 


                return {
                    initialize: initialize,
                    searchFilter: searchFilter,
                    pager: pager,
                    items: items,
                    getItems: getItems,
                    downloadArtwork: downloadArtwork,


                };
            })()
        };
        return ist.liveJobs.viewModel;
    });
