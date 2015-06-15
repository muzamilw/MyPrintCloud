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
                    errorList = ko.observableArray([]),
                     reportcategoriesList = ko.observableArray([]),
                     reportParamsList = ko.observableArray([]),
                     selectedReportId = ko.observable(),
                     selectedItemId = ko.observable(0),
                     IsExternalReport = ko.observable(0),
                     selectedItemName = ko.observable(),
                     selectedItemCode = ko.observable(),
                     selectedItemTitle = ko.observable(),
                     To = ko.observable(),
                     CC = ko.observable(),
                     Subject = ko.observable(),
                     Attachment = ko.observable(),
                     AttachmentPath = ko.observable(),
                     Signature = ko.observable(),

                       ckEditorOpenFrom = ko.observable("stores"),
                    hTmlMessageA = ko.observable(),
                    OpenReport = function () {
                        if (selectedReportId() > 0) {
                          //  getParams();

                            if (outputTo() == "preview") {
                                view.hide();
                                showProgress();
                                view.showWebViewer();
                                hideProgress();
                            } else if (outputTo() == "email") {
                                showEmailView();
                            } else if (outputTo() == "pdf") {

                            } else if (outputTo() == "excel") {

                            }
                        } else {
                            errorList.push({ name: "Please Select a Report to View", element: null });

                        }
                    },
                    getParams = function () {
                        if (selectedReportId() > 0) {
                            reportParamsList.removeAll();
                            dataservice.getreportparamsbyId({
                                Id: selectedReportId()
                            }, {
                                success: function (data) {
                                    _.each(data, function (item) {
                                        reportParamsList.push(model.reportParamsMapper(item));
                                    });
                                },
                                error: function (response) {

                                }
                            });

                        }
                    },

                     getReportEmailBaseData = function () {
                         if (selectedReportId() > 0) {
                             dataservice.getReportEmailData({
                                 Reportid: selectedReportId(),
                                 SignedBy: CategoryId,
                                 ContactId: IsExternal,
                                 RecordId: RecordId,
                                 ReportType: ReportType,
                                 OrderId: OrderId,
                                 CriteriaParam: CriteriaParam
                             }, {
                                 success: function (data) {
                                     To(data.To);
                                     CC(data.CC);
                                     Subject(data.Subject);
                                     Attachment(data.Attachment);
                                     AttachmentPath(data.AttachmentPath);
                                     Signature(data.Signature);
                                 },
                                 error: function (response) {

                                 }
                             });
                             isLoading(true);
                             view.showEmailView();
                         }
                     },
                    SelectReportById = function (report) {
                        $(".dd-handle").removeClass("selectedReport")
                        $("#" + report.ReportId()).addClass("selectedReport");
                        var scr = "/mis/Home/Viewer?id=" + report.ReportId() + "&itemId=" + selectedItemId();
                        selectedReportId(report.ReportId());
                        $("#ReportViewerIframid").attr("src", scr);
                        errorList.removeAll();

                    },


                    show = function (CategoryId, IsExternal, ItemId, Name, ItemCode, ItemTitle) {
                        reportcategoriesList.removeAll();
                        selectedItemId(ItemId);
                        selectedReportId(0);
                        selectedItemName(Name);
                        selectedItemCode(ItemCode);
                        selectedItemTitle(ItemTitle);
                        IsExternalReport(IsExternal);
                        $("#ReportViewerIframid").attr("src", "/mis/Home/Viewer?id=0&itemId=0");
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
                    showEmailView = function () {



                      
                        getReportEmailBaseData();
                    },
                    hideEmailView= function() {
                        view.hideEmailView();
                    },
                    
            // Widget being dropped
            // ReSharper disable UnusedParameter
            droppedEmailSection = function (source, target, event) {
                var val = CKEDITOR.instances.content.getData();
                hTmlMessageA(val);
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
                    selectedItemId: selectedItemId,
                    IsExternalReport:IsExternalReport,
                    selectedReportId: selectedReportId,
                    SelectReportById: SelectReportById,
                    selectedItemName:selectedItemName,
                    selectedItemCode :selectedItemCode,
                    selectedItemTitle: selectedItemTitle,
                    errorList:errorList,
                    show: show,
                    hide: hide,
                    showEmailView: showEmailView,
                    hideEmailView: hideEmailView,
                    hTmlMessageA: hTmlMessageA,
                    droppedEmailSection: droppedEmailSection,
                    ckEditorOpenFrom: ckEditorOpenFrom
                };
            })()
        };

        return ist.reportManager.viewModel;
    });