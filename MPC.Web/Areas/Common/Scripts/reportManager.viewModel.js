define("common/reportManager.viewModel",
    ["jquery", "amplify", "ko", "common/reportManager.dataservice", "common/reportManager.model"], function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.reportManager = {
            viewModel: (function () {
                var// The view 
                    view,
                    // True if we are loading data
                    isLoading = ko.observable(false),
                    outputTo = ko.observable("preview"),
                     reportcategoriesList = ko.observableArray([]),
                     selectedReportId = ko.observable(),
                     selectedItemId = ko.observable(0),
                    OpenReport = function () {
                        if (outputTo() == "preview") {
                            view.hide();
                            showProgress();
                            view.showWebViewer();
                            hideProgress();
                        } else if (outputTo() == "email") {

                        } else if (outputTo() == "pdf") {

                        } else if (outputTo() == "excel") {

                        }
                    },
                    SelectReportById = function (report) {
                        $(".dd-handle").removeClass("selectedReport")
                        $("#" + report.ReportId()).addClass("selectedReport");
                        var scr = "/mis/Home/Viewer?id=" + report.ReportId() + "&itemId=" + selectedItemId();
                        $("#ReportViewerIframid").attr("src", scr);
                      //  selectedReportId(report.ReportId());
                    },
                

                    show = function (CategoryId, IsExternal, ItemId) {
                        reportcategoriesList.removeAll();
                        selectedItemId(ItemId);
                        if (CategoryId != undefined && CategoryId != null && CategoryId != 0) {
                            dataservice.getreportcategories({
                                CategoryId: CategoryId,
                                IsExternal: IsExternal,
                               
                            }, {
                                success: function (data) {

                                    reportcategoriesList.push(model.ReportCategory(data));
                                },
                                error: function (response) {
                                    toastr.error("Failed to Load . Error: " + response);
                                }
                            });


                        }


                        isLoading(true);
                        view.show();
                    },
                // Hide the dialog
                    hide = function () {
                        // Reset Call Backs
                        //  resetDialog();
                        view.hide();
                    },


                // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                    };

                return {
                    isLoading: isLoading,
                    reportcategoriesList: reportcategoriesList,
                    outputTo: outputTo,
                    initialize: initialize,
                    OpenReport: OpenReport,
                    selectedItemId:selectedItemId,
                    selectedReportId:selectedReportId,
                    SelectReportById:SelectReportById,
                    show: show,
                    hide: hide
                };
            })()
        };

        return ist.reportManager.viewModel;
    });