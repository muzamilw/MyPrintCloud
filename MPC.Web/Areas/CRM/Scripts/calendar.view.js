/*
    View for the Calendar. Used to keep the viewmodel clear of UI related logic
*/
define("calendar/calendar.view",
    ["jquery", "calendar/calendar.viewModel"], function ($, calendarViewModel) {

        var ist = window.ist || {};

        // View 
        ist.calendar.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#fullCalendarBinding")[0],

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
            };
        })(calendarViewModel);

        // Initialize the view model
        if (ist.calendar.view.bindingRoot) {
            calendarViewModel.initialize(ist.calendar.view);
        }
        return ist.calendar.view;
    });