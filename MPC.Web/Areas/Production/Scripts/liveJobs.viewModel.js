/*
    Module with the view model for the live Jobs.
*/
define("liveJobs/liveJobs.viewModel",
    ["jquery", "amplify", "ko", "liveJobs/liveJobs.dataservice", "liveJobs/liveJobs.model", "common/pagination", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, reportManager) {
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
                    // System Users
                    systemUsers = ko.observableArray([]),

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
                     // Job Statuses
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
                            StatusName: "Shipped, Not Invoiced"
                        },
                        {
                            StatusId: 17,
                            StatusName: "Not Progressed to Job"
                        }
                    ]),
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
                                resetHiddenFields();
                                items.removeAll();
                                if (data !== null && data !== undefined) {
                                    var itemList = [];
                                    _.each(data.Items, function (item) {
                                        var itemModel = model.Item.Create(item);
                                        var user = _.find(systemUsers(), function (sysUser) {
                                            return sysUser.SystemUserId === itemModel.jobManagerId();
                                        });
                                        if (user !== null && user !== undefined) {
                                            itemModel.jobManagerName(user.FullName);
                                        }
                                     var newItem=   jobStatuses.find(function(obj) {
                                         if (itemModel.statusId() === obj.StatusId)
                                                return obj;
                                     });
                                     if (newItem) {
                                         itemModel.statusName(newItem.StatusName);
                                        }
                                        itemList.push(itemModel);

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
                    // open job card report
                    openExternalReportsJob = function (item) {

                        reportManager.outputTo("preview");

                        
                        reportManager.OpenExternalReport(ist.reportCategoryEnums.JobCards, 1, item.id());
                           
                        



                    },

               


                    // Get Items
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                ko.utils.arrayPushAll(systemUsers(), data);
                                systemUsers.valueHasMutated();
                                getItems();
                            },
                            error: function () {
                                toastr.error("Failed to Base data.");
                            }
                        });
                    },
                    // Download Artwork
                    downloadArtwork = function () {
                        dataservice.downloadArtwork({
                            success: function (data) {


                            },
                            error: function () {
                                toastr.error("Failed to Items.");
                            }
                        });

                    },
                    // on click on checkbox
                    selectItem = function (item) {
                        var index = items.indexOf(item);
                        if (item.isSelected()) {
                            item.isSelected(false);
                            $("#item" + index).val(null);
                        } else {
                            item.isSelected(true);
                            $("#item" + index).val(item.id());
                        }
                    },
                    // Reset Hidden Fields
                    resetHiddenFields = function () {
                        for (i = 0; i < 10; i++) {
                            $("#item" + i).val(null);
                        }
                    },
                    // Enable/Disable Download button
                    enableDownloadArtwork = ko.computed(function () {
                        var item = _.find(items(), function (sItem) {
                            return (sItem.isSelected() === true || sItem.isSelected() === 1);
                        });
                        if (item !== undefined) {
                            return true;
                        }
                        return false;
                    }),
                    //Initialize
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(new pagination.Pagination({ PageSize: 10 }, items, getItems));
                        getBaseData();


                    };
                //#endregion 


                return {
                    initialize: initialize,
                    searchFilter: searchFilter,
                    pager: pager,
                    items: items,
                    systemUsers: systemUsers,
                    getItems: getItems,
                    downloadArtwork: downloadArtwork,
                    selectItem: selectItem,
                    enableDownloadArtwork: enableDownloadArtwork,
                    openExternalReportsJob: openExternalReportsJob

                };
            })()
        };
        return ist.liveJobs.viewModel;
    });
