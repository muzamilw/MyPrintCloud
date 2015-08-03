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

                       // Top performing stores
                    topPerformingStores = ko.observableArray([]),

                       // monthly order count
                    monthlyOrdersCount = ko.observableArray([]),

                       // Total Earnings
                    estimateToOrderConversion = ko.observableArray([]),

                       // Total Earnings
                    estimateToOrderConversionCount = ko.observableArray([]),

                       // Registered Users
                    RegisteredUsers = ko.observableArray([]),

                       // Total Earnings
                    top10PerformingStores = ko.observableArray([]),

                    date = new Date(),
                    year = date.getFullYear(),
                    counter = 1,
                    // Y axis point for chart
                    yAxisPoints = [],
                    yAxisPointsWithStoreName = ko.observableArray([]),
                    chartLabels = [],
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
                // months = ['jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];
                line = ko.observable([

                                { month: '2008-01', a: 100, b: 90, c: 120 },
                                { month: '2008-02', a: 110, b: 90, c: 40 },
                                { month: '2008-03', a: 200, b: 90, c: 60 },
                                { month: '2008-04', a: 20, b: 90, c: 70 },
                                { month: '2008-05', a: 100, b: 90, c: 80 },
                                { month: '2008-06', a: 100, b: 90, c: 90 },
                                { month: '2008-07', a: 10, b: 90, c: 90 },
                                { month: '2008-08', a: 100, b: 90, c: 80 },
                                { month: '2008-09', a: 100, b: 90, c: 60 },
                                { month: '2008-10', a: 100, b: 90, c: 100 },
                                { month: '2008-11', a: 300, b: 90, c: 90 },
                                { month: '2008-12', a: 100, b: 90, c: 130 }
                    //{ year: 'Jun', value: 8 },
                    //{ year: 'Aug', value: 20 },
                    //{ year: 'Sep', value: 20 },
                    //{ year: 'Oct', value: 20 },
                    //{ year: 'Nov', value: 20 },
                    //{ year: 'Dec', value: 20 }
                ]),
                //line = ko.observable([

                //                { month: '2008-01', jan: 100, feb: 90, mar: 120, apri: 100, e: 910, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-02', a: 110, b: 90, c: 120, d: 500, e: 920, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-03', a: 120, b: 90, c: 120, d: 600, e: 930, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-04', a: 130, b: 90, c: 120, d: 200, e: 940, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-05', a: 140, b: 90, c: 120, d: 300, e: 950, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-06', a: 150, b: 90, c: 120, d: 500, e: 90, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-07', a: 160, b: 90, c: 120, d: 600, e: 90, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-08', a: 170, b: 90, c: 120, d: 100, e: 960, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-09', a: 180, b: 90, c: 120, d: 600, e: 90, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-10', a: 190, b: 90, c: 120, d: 100, e: 970, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-11', a: 200, b: 90, c: 120, d: 100, e: 970, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //                { month: '2008-12', a: 210, b: 90, c: 120, d: 100, e: 90, f: 120, g: 100, h: 90, i: 120, j: 100, k: 90, l: 120 },
                //    //{ year: 'Jun', value: 8 },
                //    //{ year: 'Aug', value: 20 },
                //    //{ year: 'Sep', value: 20 },
                //    //{ year: 'Oct', value: 20 },
                //    //{ year: 'Nov', value: 20 },
                //    //{ year: 'Dec', value: 20 }
                //]),
                 line1 = ko.observable([

                     { month: 'January', a: 100, b: 90 },
                    { month: 'February', a: 110, b: 90 },
                    { month: 'March', a: 200, b: 90 },
                    { month: 'April', a: 60, b: 90 },
                    { month: 'May', a: 90, b: 90 },
                    { month: 'June', a: 100, b: 90 },
                    { month: 'July', a: 150, b: 90 },
                    { month: 'August', a: 170, b: 90 },
                    { month: 'September', a: 190, b: 90 },
                    { month: 'October', a: 120, b: 90 },
                    { month: 'November', a: 180, b: 90 },
                 { month: 'December', a: 195, b: 90 }


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
                    _.each(data, function (item) {
                        var categor = _.filter(yAxisPointsWithStoreName(), function (tEarn) {
                            return item.store !== null && tEarn.store !== null && tEarn.store.toLowerCase() === item.store.toLowerCase();
                        });
                        if (yAxisPointsWithStoreName().length === 0 || categor.length === 0) {

                            yAxisPointsWithStoreName().push({ y: counter, store: (item.store === null ? "" : item.store) });
                            yAxisPoints.push(counter);
                            chartLabels.push(item.store === null ? "" : item.store);
                            counter = counter + 1;
                        }
                    });
                    _.each(data, function (tEarning) {
                        if (tEarning.monthname !== null && tEarning.monthname !== 0) {
                            var item = dummyTotalEarnings()[tEarning.monthname - 1];
                            if (item !== undefined && item !== null) {
                                var duplicateItem = model.TotalEarnings.Create(tEarning);
                                var category = _.filter(yAxisPointsWithStoreName(), function (tEarn) {
                                    return duplicateItem.store !== null && tEarn.store !== null && tEarn.store.toLowerCase() === duplicateItem.store.toLowerCase();
                                });
                                if (category.length > 0) {
                                    duplicateItem.month = item.month;
                                    duplicateItem[category[0].y] = duplicateItem.total;
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
                            
                            // totalEarning
                            totalEarnings.removeAll();
                            ko.utils.arrayPushAll(totalEarnings(), data.TotalEarningResult);
                            totalEarnings.valueHasMutated();

                            // registered users
                            //RegisteredUsers.removeAll();
                            //ko.utils.arrayPushAll(RegisteredUsers(), data.RegisteredUserByStores);
                           // RegisteredUsers.valueHasMutated();

                            // top performing stores
                            topPerformingStores.removeAll();
                            ko.utils.arrayPushAll(topPerformingStores(), data.TopPerformingStores);
                            topPerformingStores.valueHasMutated();

                            //monthly orders
                            monthlyOrdersCount.removeAll();
                            ko.utils.arrayPushAll(monthlyOrdersCount(), data.MonthlyOrdersCount);
                            monthlyOrdersCount.valueHasMutated();

                            // estimate to order
                            estimateToOrderConversion.removeAll();
                            ko.utils.arrayPushAll(estimateToOrderConversion(), data.EstimateToOrderConversion);
                            estimateToOrderConversion.valueHasMutated();

                            // estimate to order count
                            estimateToOrderConversionCount.removeAll();
                            ko.utils.arrayPushAll(estimateToOrderConversionCount(), data.EstimateToOrderConversionCount);
                            estimateToOrderConversionCount.valueHasMutated();

                            // top 10 perfoming companies
                            top10PerformingStores.removeAll();
                            ko.utils.arrayPushAll(top10PerformingStores(), data.Top10PerformingCustomers);
                            top10PerformingStores.valueHasMutated();

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
                    goToCustomer: goToCustomer,
                    yAxisPoints: yAxisPoints,
                    chartLabels: chartLabels,
                    line1: line1,
                    topPerformingStores: topPerformingStores,

                    monthlyOrdersCount: monthlyOrdersCount,
                    estimateToOrderConversion: estimateToOrderConversion,

                    estimateToOrderConversionCount: estimateToOrderConversionCount,
                    RegisteredUsers: RegisteredUsers,
                    top10PerformingStores: top10PerformingStores
                    // xLabelFormat: xLabelFormat
                };
            })()
        };
        return ist.dashboard.viewModel;
    });
