/*
    View for the To Do List. Used to keep the viewmodel clear of UI related logic
*/
define("toDoList/toDoList.view",
    ["jquery", "toDoList/toDoList.viewModel"], function ($, toDoListViewModel) {

        var ist = window.ist || {};

        // View 
        ist.toDoList.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#toDoListBinding")[0],
                //// Show Calendar Activity dialog
                showCalendarActivityDialog = function () {
                    $("#calendarActivityModal").modal("show");
                },
                // Hide Calendar Activity dialog
                hideCalendarActivityDialog = function () {
                    $("#calendarActivityModal").modal("hide");
                },

                  // Show Contact dialog
                showContactSelectorDialog = function () {
                    $("#contactSelectorDialog").modal("show");
                },
                // Hide Contact dialog
                hideContactSelectorDialog = function () {
                    $("#contactSelectorDialog").modal("hide");
                },
                  // Show Company dialog
                showCompanyDialog = function () {
                    $("#companyDialog").modal("show");
                },
                // Hide Company dialog
                hideCompanyDialog = function () {
                    $("#companyDialog").modal("hide");
                },
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showCalendarActivityDialog: showCalendarActivityDialog,
                hideCalendarActivityDialog: hideCalendarActivityDialog,
                showContactSelectorDialog: showContactSelectorDialog,
                hideContactSelectorDialog: hideContactSelectorDialog,
                showCompanyDialog: showCompanyDialog,
                hideCompanyDialog: hideCompanyDialog,

            };
        })(toDoListViewModel);

        // Initialize the view model
        if (ist.toDoList.view.bindingRoot) {
            toDoListViewModel.initialize(ist.toDoList.view);
        }
        return ist.toDoList.view;
    });