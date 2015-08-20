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
                    monthlyOrderStoresList = ko.observableArray([]),
                       // Total Earnings
                    estimateToOrderConversion = ko.observableArray([]),
                    tempUsers = ko.observableArray([]),
                       // Total Earnings
                    estimateToOrderConversionCount = ko.observableArray([]),
                    dummyUsers = ko.observableArray([
                        { monthname: '2015-01', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 1, year: 2015 },
                        { monthname: '2015-02', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 2, year: 2015 },
                        { monthname: '2015-03', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 3, year: 2015 },
                        { monthname: '2015-04', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 4, year: 2015 },
                        { monthname: '2015-05', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 5, year: 2015 },
                        { monthname: '2015-06', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 6, year: 2015 },
                        { monthname: '2015-07', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 7, year: 2015 },
                        { monthname: '2015-08', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 8, year: 2015 },
                        { monthname: '2015-09', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 9, year: 2015 },
                        { monthname: '2015-10', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 10, year: 2015 },
                        { monthname: '2015-11', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 11, year: 2015 },
                        { monthname: '2015-12', totalStore1: 0, totalStore2: 0, totalStore3: 0, totalStore4: 0, totalStore5: 0, month: 12, year: 2015 }
                    ]),
                       // Registered Users
                    RegisteredUsers = ko.observableArray([]),

                       // Total Earnings
                    top10PerformingStores = ko.observableArray([]),
                    monthlyEarningsByStore = ko.observableArray([]),
                    monthlyEarningStores = ko.observableArray([]),
                    date = new Date(),
                    year = date.getFullYear(),
                    counter = 1,
                    // Y axis point for chart
                    yAxisPoints = [],
                    yAxisPointdummy = ko.observableArray(['totalStore1', 'totalStore2', 'totalStore3', 'totalStore4', 'totalStore5']),
                    //chartLabelsdummy = ko.observableArray(['PinkCards.com', 'Goldwell.com', 'saleflow.com', 'yolkpm.com', 'eazyprint.com', 'sunnyland.com', 'printtech.com', 'printmedia.com', 'cloudfusion.com']),
                    chartLabelsdummy = ko.observableArray([]),
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

                                { month: '2015-01-01', a: 100, b: 110, c: 120, d:130, e:140 },
                                { month: '2015-02-02', a: 120, b: 130, c: 140, d: 140, e: 150},
                                { month: '2015-03', a: 130, b: 140, c: 160, d: 150, e: 160 },
                                { month: '2015-04', a: 120, b: 190, c: 170, d: 160, e: 170 },
                                { month: '2015-05', a: 100, b: 120, c: 180, d: 170, e: 180},
                                { month: '2015-06', a: 100, b: 110, c: 110, d: 160, e: 190 },
                                { month: '2015-07', a: 105, b: 90, c: 110, d: 170, e: 120 },
                                { month: '2015-08', a: 100, b: 90, c: 80, d: 130, e: 200 },
                                { month: '2015-09', a: 100, b: 90, c: 60, d: 180, e: 210 },
                                { month: '2015-10', a: 100, b: 90, c: 100, d: 190, e: 220 },
                                { month: '2015-11', a: 300, b: 90, c: 90, d: 180, e: 225},
                                { month: '2015-12', a: 100, b: 90, c: 130, d: 170, e: 230 }
                    
                ]),
                //line = ko.observable([
                //                { month: '2015-01', a: 100 },
                //                { month: '2015-02', a: 120},
                //                { month: '2015-03', a: 130},
                //                { month: '2015-04', a: 120},
                //                { month: '2015-05', a: 100},
                //                { month: '2015-06', a: 100},
                //                { month: '2015-07', a: 105},
                //                { month: '2015-08', a: 100},
                //                { month: '2015-09', a: 100},
                //                { month: '2015-10', a: 100},
                //                { month: '2015-11', a: 300},
                //                { month: '2015-12', a: 100}
                
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
            //Map Registered Users
                    //select Name, count(*) as TotalContacts, Month, MonthName, Year 
            mapRegisteredUsers = function(data) {
                
                 transposeUsersData(data);
               
            },
                    transposeUsersData = function (data) {
                        var currentMonth = 0;
                        var uCounter = 1;
                        chartLabelsdummy.removeAll();
                        _.each(data, function (item) {
                            var store = _.filter(chartLabelsdummy(), function (label) {
                                return store !== null && label.toLowerCase() === item.Name.toLowerCase();
                            });
                            if (chartLabelsdummy().length === 0 || store.length === 0) {
                                chartLabelsdummy().push(item.Name);
                            }
                        });
                        _.each(data, function (tUser) {
                            var item = dummyUsers()[tUser.Month - 1];
                            if (tUser.Month != currentMonth) {
                                if (chartLabelsdummy()[0] == tUser.Name) {
                                    item.totalStore1 = tUser.TotalContacts;
                                }
                                if (chartLabelsdummy()[1] == tUser.Name) {
                                    item.totalStore2 = tUser.TotalContacts;
                                }
                                if (chartLabelsdummy()[2] == tUser.Name) {
                                    item.totalStore3 = tUser.TotalContacts;
                                }
                                if (chartLabelsdummy()[3] == tUser.Name) {
                                    item.totalStore4 = tUser.TotalContacts;
                                }
                                if (chartLabelsdummy()[4] == tUser.Name) {
                                    item.totalStore5 = tUser.TotalContacts;
                                }

                                currentMonth = tUser.Month;
                                item.month = currentMonth;
                                item.monthname = tUser.Year + "-0" + currentMonth;
                                uCounter = 1;
                               // chartLabelsdummy().push(tUser.Name);
                                tempUsers.push(item);
                            } else {
                                
                                uCounter = uCounter + 1;
                                _.each(tempUsers(), function (user) {
                                    if (user.month == currentMonth && chartLabelsdummy()[0] == tUser.Name) {
                                        user.totalStore1 = tUser.TotalContacts;
                                        user.month = tUser.Month;
                                    }
                                    if (user.month == currentMonth && chartLabelsdummy()[1] == tUser.Name) {
                                        user.totalStore2 = tUser.TotalContacts;
                                        user.month = tUser.Month;
                                    }
                                    if (user.month == currentMonth && chartLabelsdummy()[2] == tUser.Name) {
                                        user.totalStore3 = tUser.TotalContacts;
                                        user.month = tUser.Month;
                                    }
                                    if (user.month == currentMonth && chartLabelsdummy()[3] == tUser.Name) {
                                        user.totalStore4 = tUser.TotalContacts;
                                        user.month = tUser.Month;
                                    }
                                    if (user.month == currentMonth && chartLabelsdummy()[4] == tUser.Name) {
                                        user.totalStore5 = tUser.TotalContacts;
                                        user.month = tUser.Month;
                                    }
                                    
                                });
                                
                            }
                            
                            
                        });
                        ko.utils.arrayPushAll(RegisteredUsers(), tempUsers());
                        RegisteredUsers.valueHasMutated();
                    },
                    
            mapMonthlyEarningByStore = function (data) {
                var currentMonth = 0;
                var uCounter = 1;
                monthlyEarningStores.removeAll();
                _.each(data, function (item) {
                    var store = _.filter(monthlyEarningStores(), function (label) {
                        return store !== null && label.toLowerCase() === item.Name.toLowerCase();
                    });
                    if (monthlyEarningStores().length === 0 || store.length === 0) {
                        monthlyEarningStores().push(item.Name);
                    }
                });
                _.each(data, function (tUser) {
                    var item = dummyUsers()[tUser.Month - 1];
                    if (tUser.Month != currentMonth) {
                        if (monthlyEarningStores()[0] == tUser.Name) {
                            item.totalStore1 = tUser.TotalEarning.toFixed(2);
                        }
                        if (monthlyEarningStores()[1] == tUser.Name) {
                            item.totalStore2 = tUser.TotalEarning.toFixed(2);
                        }
                        if (monthlyEarningStores()[2] == tUser.Name) {
                            item.totalStore3 = tUser.TotalEarning.toFixed(2);
                        }
                        if (monthlyEarningStores()[3] == tUser.Name) {
                            item.totalStore4 = tUser.TotalEarning.toFixed(2);
                        }
                        if (monthlyEarningStores()[4] == tUser.Name) {
                            item.totalStore5 = tUser.TotalEarning.toFixed(2);
                        }

                        currentMonth = tUser.Month;
                        item.month = currentMonth;
                        item.monthname = tUser.Year + "-0" + currentMonth;
                        uCounter = 1;
                        // chartLabelsdummy().push(tUser.Name);
                        tempUsers.push(item);
                    } else {

                        uCounter = uCounter + 1;
                        _.each(tempUsers(), function (user) {
                            if (user.month == currentMonth && monthlyEarningStores()[0] == tUser.Name) {
                                user.totalStore1 = tUser.TotalEarning.toFixed(2);
                                user.month = tUser.Month;
                            }
                            if (user.month == currentMonth && monthlyEarningStores()[1] == tUser.Name) {
                                user.totalStore2 = tUser.TotalEarning.toFixed(2);
                                user.month = tUser.Month;
                            }
                            if (user.month == currentMonth && monthlyEarningStores()[2] == tUser.Name) {
                                user.totalStore3 = tUser.TotalEarning.toFixed(2);
                                user.month = tUser.Month;
                            }
                            if (user.month == currentMonth && monthlyEarningStores()[3] == tUser.Name) {
                                user.totalStore4 = tUser.TotalEarning.toFixed(2);
                                user.month = tUser.Month;
                            }
                            if (user.month == currentMonth && monthlyEarningStores()[4] == tUser.Name) {
                                user.totalStore5 = tUser.TotalEarning.toFixed(2);
                                user.month = tUser.Month;
                            }

                        });

                    }


                });
                ko.utils.arrayPushAll(monthlyEarningsByStore(), tempUsers());
                monthlyEarningsByStore.valueHasMutated();
            },
            
            mapMonthlyOrdersCountByStore = function (data) {
                 var currentMonth = 0;
                 var uCounter = 1;
                 monthlyOrderStoresList.removeAll();
                 tempUsers.removeAll();
                _.each(dummyUsers(), function(dum) {
                    dum.totalStore1 = 0;
                    dum.totalStore2 = 0;
                    dum.totalStore3 = 0;
                    dum.totalStore4 = 0;
                    dum.totalStore5 = 0;
                });
                 _.each(data, function (item) {
                     var store = _.filter(monthlyOrderStoresList(), function (label) {
                         return store !== null && label.toLowerCase() === item.CompanyName.toLowerCase();
                     });
                     if (monthlyOrderStoresList().length === 0 || store.length === 0) {
                         monthlyOrderStoresList().push(item.CompanyName);
                     }
                 });
                 _.each(data, function (tUser) {
                     var item = dummyUsers()[tUser.Month - 1];
                     if (tUser.Month != currentMonth) {
                         if (monthlyOrderStoresList()[0] == tUser.CompanyName) {
                             item.totalStore1 = tUser.TotalOrders;
                         }
                         if (monthlyOrderStoresList()[1] == tUser.CompanyName) {
                             item.totalStore2 = tUser.TotalOrders;
                         }
                         if (monthlyOrderStoresList()[2] == tUser.CompanyName) {
                             item.totalStore3 = tUser.TotalOrders;
                         }
                         if (monthlyOrderStoresList()[3] == tUser.CompanyName) {
                             item.totalStore4 = tUser.TotalOrders;
                         }
                         if (monthlyOrderStoresList()[4] == tUser.CompanyName) {
                             item.totalStore5 = tUser.TotalOrders;
                         }

                         currentMonth = tUser.Month;
                         item.month = currentMonth;
                         item.monthname = tUser.year + "-0" + currentMonth;
                         uCounter = 1;
                         // chartLabelsdummy().push(tUser.Name);
                         tempUsers.push(item);
                         
                     } else {

                         uCounter = uCounter + 1;
                         _.each(tempUsers(), function (user) {
                             if (user.month == currentMonth && monthlyOrderStoresList()[0] == tUser.CompanyName) {
                                 user.totalStore1 = tUser.TotalOrders;
                                 user.month = tUser.Month;
                             }
                             if (user.month == currentMonth && monthlyOrderStoresList()[1] == tUser.CompanyName) {
                                 user.totalStore2 = tUser.TotalOrders;
                                 user.month = tUser.Month;
                             }
                             if (user.month == currentMonth && monthlyOrderStoresList()[2] == tUser.CompanyName) {
                                 user.totalStore3 = tUser.TotalOrders;
                                 user.month = tUser.Month;
                             }
                             if (user.month == currentMonth && monthlyOrderStoresList()[3] == tUser.CompanyName) {
                                 user.totalStore4 = tUser.TotalOrders;
                                 user.month = tUser.Month;
                             }
                             if (user.month == currentMonth && monthlyOrderStoresList()[4] == tUser.CompanyName) {
                                 user.totalStore5 = tUser.TotalOrders;
                                 user.month = tUser.Month;
                             }

                         });

                     }


                 });
                 ko.utils.arrayPushAll(monthlyOrdersCount(), tempUsers());
                 monthlyOrdersCount.valueHasMutated();
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

                            // chart 2 Registered Users By Store
                            mapRegisteredUsers(data.RegisteredUserByStores);
                            

                            // chart 1 top performing stores
                            topPerformingStores.removeAll();
                            ko.utils.arrayPushAll(topPerformingStores(), data.TopPerformingStores);
                            topPerformingStores.valueHasMutated();
                            
                            //chart 3 Monthly Earning By Store
                            mapMonthlyEarningByStore(data.MonthlyEarningsbyStore);

                            //Chart 4 Monthly Orders Count
                            mapMonthlyOrdersCountByStore(data.MonthlyOrdersCount);
                            

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
                    monthlyOrderStoresList:monthlyOrderStoresList,
                    estimateToOrderConversion: estimateToOrderConversion,

                    estimateToOrderConversionCount: estimateToOrderConversionCount,
                    RegisteredUsers: RegisteredUsers,
                    top10PerformingStores: top10PerformingStores,
                    yAxisPointdummy: yAxisPointdummy,
                    chartLabelsdummy: chartLabelsdummy,
                    monthlyEarningStores: monthlyEarningStores,
                    monthlyEarningsByStore: monthlyEarningsByStore
                    // xLabelFormat: xLabelFormat
                };
            })()
        };
        return ist.dashboard.viewModel;
    });
