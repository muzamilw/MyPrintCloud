/*
    View for the reportManager. Used to keep the viewmodel clear of UI related logic
*/
define("common/reportManager.view",
    ["jquery", "common/reportManager.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.reportManager.view = (function (specifiedViewModel) {
            var// View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#divReportManager")[0],

                 // Binding for param
                bindingRootParam = $("#idReportParams")[0],
                // bindingRoot = $("#reportEmailView")[0],

                // Show the dialog
                show = function () {
                    $("#divReportManager").modal("show");
                },
                // Hide the dialog
                hide = function () {
                    $("#divReportManager").modal("hide");
                },
                 // Show the dialog
                showEmailView = function () {
                    $("#reportEmailView").modal("show");
                },
                // Hide the dialog
                hideEmailView = function () {
                    $("#reportEmailView").modal("hide");
                },
                  // Show the dialog
                showReportParamView = function () {
                    $("#idReportParams").modal("show");
                },
                  // Show the dialog
                hideReportParamView = function () {
                    $("#idReportParams").modal("hide");
                },

            showWebViewer = function () {
                $("#idReportViewerHtml").modal("show");
            },
            hideWebViewer = function () {
                $("#idReportViewerHtml").modal("hide");

            };
            return {
                bindingRoot: bindingRoot,
                bindingRootParam: bindingRootParam,
                viewModel: viewModel,
                show: show,
                hide: hide,
                showWebViewer: showWebViewer,
                hideWebViewer: hideWebViewer,
                showEmailView: showEmailView,
                hideEmailView: hideEmailView,
                showReportParamView: showReportParamView,
                hideReportParamView: hideReportParamView
            };
        })(ist.reportManager.viewModel);

        // Initialize the view model
        if (ist.reportManager.view.bindingRoot) {
            ist.reportManager.viewModel.initialize(ist.reportManager.view);
        }


    });