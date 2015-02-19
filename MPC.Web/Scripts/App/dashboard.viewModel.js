/*
    Module with the view model for the Dashboard
*/
define("dashboard.viewModel",
    ["jquery", "amplify", "ko", "dashboard.dataservice", "dashboard.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm) {
        var ist = window.ist || {};
        ist.dashboard = {
            viewModel: (function () {
                var //View
                view,
                // Search filter 
                searchFilter = ko.observable(),
                // Pager for pagging
                pager = ko.observable(),
                // Sort On
                sortOn = ko.observable(1),
                 // Sort In Ascending
                sortIsAsc = ko.observable(true),
                // Pending Orders Count
                pendingOrdersCount = ko.observable(0),
                // In-production Orders Count
                inProductionOrdersCount = ko.observable(0),
                // Completed Orders Count
                completedOrdersCount = ko.observable(0),
                // Canceled Orders Count
                canceledOrdersCount = ko.observable(0),
                 // Total Earnings
                totalEarning = ko.observable(undefined),
                 // Live Stores
                liveStoresCount = ko.observable(0),
                  // current Month Orders Count
                currentMonthOrdersCount = ko.observable(0),
                // Get Orders Statuses
                getOrderStatusCount = function () {
                    dataservice.getOrderStauses({},
                    {
                        success: function (data) {
                            if (data != null) {
                                setOrderStatusesCount(data);
                            }
                        },
                        error: function () {
                            toastr.error("Error: Failed To load orders statues!!");
                        }
                    });
                },
                // Sets Orders Statuses Count
                setOrderStatusesCount = function(data) {
                    pendingOrdersCount(data.PendingOrdersCount);
                    inProductionOrdersCount(data.InProductionOrdersCount);
                    completedOrdersCount(data.CompletedOrdersCount);
                    canceledOrdersCount(data.UnConfirmedOrdersCount);
                    totalEarning(data.TotalEarnings !== null ? data.TotalEarnings.toFixed(2) + '$' : 0);
                    liveStoresCount(data.LiveStoresCount);
                    currentMonthOrdersCount(data.CurrentMonthOdersCount);
                },
                //Initialize
                initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   getOrderStatusCount();
                   //  pager(new pagination.Pagination({ PageSize: 5 }, companyContactsForListView, getCompanyContacts));
               };
                return {
                    initialize: initialize,
                    pendingOrdersCount: pendingOrdersCount,
                    inProductionOrdersCount: inProductionOrdersCount,
                    completedOrdersCount: completedOrdersCount,
                    canceledOrdersCount: canceledOrdersCount,
                    totalEarning: totalEarning,
                    liveStoresCount: liveStoresCount,
                    currentMonthOrdersCount: currentMonthOrdersCount
                };
            })()
        };
        return ist.dashboard.viewModel;
    });
