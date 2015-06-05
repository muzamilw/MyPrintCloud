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
                     // Total Earnings
                    totalEarnings = ko.observableArray([]),
                    date = new Date(),
                    year = date.getFullYear(),

                     // customers
                     customers = ko.observableArray([]),
                    dummyTotalEarnings = ko.observableArray([
                        { month: year + '-01-01', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-02-02', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-03-03', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-04-04', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-05-05', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-06-06', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-07-07', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-08-08', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-09-09', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-10-10', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-11-11', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 },
                        { month: year + '-12-12', orders: 0, total: 0, monthname: 0, year: 0, store: "", flag: 0 }

                    ]),
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
                        { year: '2013', value: 6 },
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
                                mapCustomer(data.Companies);

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
                    mapTotalEarnings = function (data) {
                        //totalEarnings.removeAll();

                        _.each(data, function (tEarning) {
                            if (tEarning.monthname !== null && tEarning.monthname !== 0) {
                                var item = dummyTotalEarnings()[tEarning.monthname - 1];
                                if (item !== undefined && item !== null) {
                                    if (item.flag === 0) {
                                        item.orders = tEarning.Orders;
                                        item.total = tEarning.Total;
                                        item.monthname = tEarning.monthname;
                                        item.year = tEarning.year;
                                        item.store = tEarning.store;
                                        item.flag = 1;
                                    }
                                    else {
                                        var duplicateItem = model.TotalEarnings.Create(tEarning);
                                        duplicateItem.month = item.month;
                                        dummyTotalEarnings.push(duplicateItem);
                                    }
                                }
                            }
                        });
                        ko.utils.arrayPushAll(totalEarnings(), dummyTotalEarnings());
                        totalEarnings.valueHasMutated();
                    },
                // Get Total earning of current whole year
                getTotalEarnings = function (callBack) {
                    dataservice.getTotalEarnings({
                        success: function (data) {
                            if (data != null) {
                                mapTotalEarnings(data);
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
                     // Map Customer 
                    mapCustomer = function (data) {
                        customers.removeAll();
                        _.each(data, function (customer) {
                            var customerModel = new model.customerViewListModel.Create(customer);
                            customers.push(customerModel);
                        });
                    },
                // Sets Orders Statuses Count
                setOrderStatusesCount = function (data) {
                    pendingOrdersCount(data.PendingOrdersCount);
                    inProductionOrdersCount(data.InProductionOrdersCount);
                    completedOrdersCount(data.CompletedOrdersCount);
                    canceledOrdersCount(data.UnConfirmedOrdersCount);
                    totalEarning(data.TotalEarnings !== null ? data.TotalEarnings.toFixed(2) : 0);
                    liveStoresCount(data.LiveStoresCount);
                    currentMonthOrdersCount(data.CurrentMonthOdersCount);
                },
                // Go to Order
                goToOrder = function (orderId) {
                    view.goToOrder(orderId);
                },
                // Go to Customer
                goToCustomer = function (customerId) {
                    view.goToCustomer(customerId);
                },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getDashboardData();
                    getTotalEarnings();
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
                    line: line,
                    totalEarnings: totalEarnings,
                    customers: customers,
                    goToCustomer: goToCustomer
                };
            })()
        };
        return ist.dashboard.viewModel;
    });
