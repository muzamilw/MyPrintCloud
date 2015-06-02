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
                    // orders
                    orders = ko.observableArray([]),
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

                    line = ko.observable([
                        { year: '2008', value: 20 },
                        { year: '2009', value: 10 },
                        { year: '2010', value: 17 },
                        { year: '2011', value: 5 },
                        { year: '2013', value:6 },
                        { year: '2015', value: 8 },
                        { year: '2012', value: 20 }
                    ]),
             
              
                // In-production Orders Count
                inProductionOrdersCount = ko.observable(0),
                // Completed Orders Count
                completedOrdersCount = ko.observable(0),
                // Canceled Orders Count
                canceledOrdersCount = ko.observable(0),
                 // Total Earnings
                totalEarning = ko.observable(undefined).extend({ numberInput: ist.numberFormat }),
                 // Live Stores
                liveStoresCount = ko.observable(0),
                //Order Search String
                orderSearchString = ko.observable(),
                  // current Month Orders Count
                currentMonthOrdersCount = ko.observable(0),
                // Get Orders Statuses / dashboard data  getOrderStatusCount
                getDashboardData = function () {
                    dataservice.getOrderStauses({
                        SearchString: orderSearchString()
                    },
                    {
                        success: function (data) {
                            if (data != null) {
                                setOrderStatusesCount(data);
                                mapOrders(data.Estimates);
                            }
                            //load the tour
                            //openTourInit();
                        },
                        error: function () {
                            toastr.error("Error: Failed To load orders statues!!");
                            //openTourInit();
                        }
                    });
                },
                // Map Orders 
                    mapOrders = function (data) {
                        orders.removeAll();
                        _.each(data, function (order) {
                            orders.push(model.Estimate.Create(order));
                        });
                    },
                // Sets Orders Statuses Count
                setOrderStatusesCount = function(data) {
                    pendingOrdersCount(data.PendingOrdersCount);
                    inProductionOrdersCount(data.InProductionOrdersCount);
                    completedOrdersCount(data.CompletedOrdersCount);
                    canceledOrdersCount(data.UnConfirmedOrdersCount);
                    totalEarning(data.TotalEarnings !== null ? data.TotalEarnings.toFixed(2) : 0);
                    liveStoresCount(data.LiveStoresCount);
                    currentMonthOrdersCount(data.CurrentMonthOdersCount);
                },
                // Go to Order
                goToOrder = function(orderId) {
                    view.goToOrder(orderId);
                },
                //Initialize
                initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   getDashboardData();
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
                    currentMonthOrdersCount: currentMonthOrdersCount,
                    orderSearchString: orderSearchString,
                    getDashboardData: getDashboardData,
                    orders: orders,
                    goToOrder: goToOrder,
                    line: line
                };
            })()
        };
        return ist.dashboard.viewModel;
    });
