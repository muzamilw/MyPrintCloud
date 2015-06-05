/*
    View for the dashboard. Used to keep the viewmodel clear of UI related logic
*/
define("dashboard.view",
    ["jquery", "dashboard.viewModel"], function ($, dashboardViewModel) {
        var ist = window.ist || {};
        // View 
        ist.dashboard.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // binding root
                bindingRoot = $("#dashboardBindingRoot")[0],
                goToOrder = function(orderId) {
                    window.open('/mis/orders/home?id=' + orderId, "_blank");
                },
                  goToCustomer = function (customerId) {
                      window.open('/mis/CRM/home?id=' + customerId, "_blank");
                  },
                // Initialize
                initialize = function() {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                goToOrder: goToOrder,
                goToCustomer: goToCustomer
            };
        })(dashboardViewModel);

        // Initialize the view model
        if (ist.dashboard.view.bindingRoot) {
            dashboardViewModel.initialize(ist.dashboard.view);
        }
        return ist.dashboard.view;
    });