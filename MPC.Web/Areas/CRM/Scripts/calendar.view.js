/*
    View for the Calendar. Used to keep the viewmodel clear of UI related logic
*/
define("calendar/calendar.view",
    ["jquery", "calendar/calendar.viewModel", "crm/contacts.viewModel"], function ($, calendarViewModel, contactsViewModel) {

        var ist = window.ist || {};

        // View 
        ist.calendar.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#fullCalendarBinding")[0],
                // Show Calendar Activity dialog
                showCalendarActivityDialog = function () {
                    $("#calendarActivityModal").modal("show");
                },
                // Hide Calendar Activity dialog
                hideCalendarActivityDialog = function () {
                    $("#calendarActivityModal").modal("hide");
                },
                  // Show Company dialog
                showCompanyDialog = function () {
                    $("#companyDialog").modal("show");
                },
                // Hide Company dialog
                hideCompanyDialog = function () {
                    $("#companyDialog").modal("hide");
                },
                  // Show Contact dialog
                showContactSelectorDialog = function () {
                    $("#contactSelectorDialog").modal("show");
                },
                // Hide Contact dialog
                hideContactSelectorDialog = function () {
                    $("#contactSelectorDialog").modal("hide");
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
                showCompanyDialog: showCompanyDialog,
                hideCompanyDialog: hideCompanyDialog,
                showContactSelectorDialog: showContactSelectorDialog,
                hideContactSelectorDialog:hideContactSelectorDialog

            };
        })(calendarViewModel);

        // Initialize the view model
        if (ist.calendar.view.bindingRoot) {
            calendarViewModel.initialize(ist.calendar.view);
        }
        // Initialize the view model
        if (ist.contacts.view.contactProfileBindingRoot) {
            contactsViewModel.initializeForCalendar(ist.contacts.view);
        }
        return ist.calendar.view;
    });