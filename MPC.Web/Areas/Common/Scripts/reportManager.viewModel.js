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
                     paramComboList = ko.observableArray([]),
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
                     SelectedEmailReport = ko.observable(),
                     SignedBy = ko.observable(),
                     ContactId = ko.observable(),
                     RecordId = ko.observable(),
                     OrderId = ko.observable(),
                      CategoryId = ko.observable(),
                     CriteriaParam = ko.observable(),
                      ComboValue = ko.observable(0),
                      ParamDateFrom = ko.observable(),
                      ParamDateTo = ko.observable(),
                       ParamValue = ko.observable(),
                     ReportTitle = ko.observable(),
                    

                     //SignedBy: CategoryId,
                        //ContactId: IsExternal,
                        //RecordId: RecordId,
                        //ReportType: ReportType,
                        //OrderId: OrderId,
                        //CriteriaParam: CriteriaParam

                        DateFrom = ko.observable(),
                          DateTo = ko.observable(),
                       ckEditorOpenFrom = ko.observable("stores"),
                    hTmlMessageA = ko.observable(),


                OpenExternalReport = function (CategoryId, IsExternal, ItemId) {
                    selectedItemId(ItemId);


                    //$("#ReportViewerIframid").attr("src", "/mis/Home/Viewer?id=0&itemId=0");
                    if (CategoryId != undefined && CategoryId != null && CategoryId != 0) {
                        dataservice.getreportcategories({
                            CategoryId: CategoryId,
                            IsExternal: IsExternal,

                        }, {
                            success: function (data) {

                                reportcategoriesList.push(model.ReportCategory(data));
                                SelectReportById(model.ReportCategory(data).reports()[0]);

                                var report = model.ReportCategory(data).reports()[0];
                                 var scr = "/mis/Home/Viewer?id=" + report.ReportId() + "&itemId=" + selectedItemId();
                                selectedReportId(report.ReportId());
                                 $("#ReportViewerIframid").attr("src", scr);
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
                            error: function (response) {
                                toastr.error("Failed to Load . Error: " + response);
                            }
                        });


                    }
                },


                    OpenReport = function () {

                        if (selectedReportId() > 0) {
                            getParams();


                        } else {
                            errorList.push({ name: "Please Select a Report to View", element: null });

                        }

                    },
                    getParams = function () {
                        if (selectedReportId() > 0) {
                            reportParamsList.removeAll();
                            paramComboList.removeAll();
                            dataservice.getreportparamsbyId({
                                Id: selectedReportId()
                            }, {
                                success: function (data) {
                                    //_.each(data, function (item) {

                                    //    reportParamsList.push(model.reportParamsMapper(item));
                                    //});

                                    if (data.length != 0) {
                                        _.each(data, function (item) {

                                            reportParamsList.push(model.ReportParam(item.param));

                                            _.each(item.ComboList, function (comb) {
                                                paramComboList.push(model.ComboMapper(comb));
                                            });
                                        });


                                        view.showReportParamView();
                                    }
                                    else {

                                        var scr = "/mis/Home/Viewer?id=" + selectedReportId() + "&itemId=" + selectedItemId();
                                        selectedReportId(selectedReportId());
                                        $("#ReportViewerIframid").attr("src", scr);


                                        if (outputTo() == "preview") {
                                            view.hide();
                                            showProgress();
                                            view.showWebViewer();
                                            hideProgress();
                                        } else if (outputTo() == "email") {
                                            showEmailView();
                                        } else if (outputTo() == "pdf") {
                                            downloadPDFReport();
                                        } else if (outputTo() == "excel") {
                                            downloadExcelReport();
                                        }
                                    }

                                },
                                error: function (response) {

                                }
                            });

                        }
                    },


                     OpenReportForParams = function () {


                        
                         if (ParamDateFrom() != undefined)
                         {
                             DateFrom = moment(ParamDateFrom()).format(ist.datePatternUs);
                         }
                         else
                         {
                             DateFrom = undefined;
                         }
                         if (ParamDateTo() != undefined)
                         {
                             DateTo = moment(ParamDateTo()).format(ist.datePatternUs);
                         }
                         else
                         {
                             DateTo = undefined;
                         }


                         if (outputTo() == "preview") {

                             var scr = "/mis/Home/Viewer?id=" + selectedReportId() + "&itemId=" + selectedItemId() + "&ComboValue=" + ComboValue() + "&Datefrom=" + DateFrom + "&DateTo=" + DateTo + "&ParamTextValue=" + ParamValue();
                             selectedReportId(selectedReportId());
                             $("#ReportViewerIframid").attr("src", scr);
                             errorList.removeAll();

                             view.hide();
                             view.hideReportParamView();
                             showProgress();
                             view.showWebViewer();
                             hideProgress();
                         } else if (outputTo() == "email") {

                             var scr = "/mis/Home/Viewer?id=" + selectedReportId() + "&itemId=" + selectedItemId() + "&ComboValue=" + ComboValue() + "&Datefrom=" + DateFrom + "&DateTo=" + DateTo + "&ParamTextValue=" + ParamValue();
                             selectedReportId(selectedReportId());
                             $("#ReportViewerIframid").attr("src", scr);
                             errorList.removeAll();

                             view.hideReportParamView();
                             showEmailView();
                         } else if (outputTo() == "pdf") {
                             downloadPDFReport();
                         } else if (outputTo() == "excel") {
                             downloadExcelReport();
                         }
                         //dataservice.getReportByParams({
                         //    Reportid: selectedReportId(),
                         //    ComboValue: ComboValue(),
                         //    ParamDateFromValue: ParamDateFrom(),
                         //    ParamDateToValue: ParamDateTo(),
                         //    ParamTextBoxValue: ParamValue()
                         //}, {
                         //    success: function (data) {


                         //    },
                         //    error: function (response) {

                         //    }
                         //});
                         isLoading(true);

                     },

                     getReportEmailBaseData = function () {
                         var dropDownvalue = ComboValue();
                         DateFrom = moment(ParamDateFrom()).format(ist.dateTimeWithSeconds);
                         DateTo = moment(ParamDateTo()).format(ist.dateTimeWithSeconds);


                         if (selectedReportId() > 0) {
                             dataservice.getReportEmailData({
                                 Reportid: selectedReportId(),
                                 SignedBy: SignedBy(),
                                 ContactId: ContactId(),
                                 RecordId: RecordId(),
                                 ReportType: CategoryId(),
                                 OrderId: OrderId(),
                                 CriteriaParam: CriteriaParam(),
                                 ComboValue: dropDownvalue,
                                 DateFrom: DateFrom,
                                 DateTo: DateTo,
                                 ParamValue: ParamValue()
                             }, {
                                 success: function (data) {
                                     //To(data.To);
                                     //CC(data.CC);
                                     //Subject(data.Subject);
                                     //Attachment(data.Attachment);
                                     //AttachmentPath(data.AttachmentPath);
                                     //Signature(data.Signature);

                                     SelectedEmailReport().emailTo(data.To);
                                     SelectedEmailReport().emailCC(data.CC);
                                     SelectedEmailReport().emailSubject(data.Subject);
                                     SelectedEmailReport().emailAttachment(data.Attachment);
                                     SelectedEmailReport().emailAttachmentPath(data.AttachmentPath);
                                     SelectedEmailReport().emailSignature(data.Signature);

                                     view.show();
                                     view.showEmailView();

                                 },
                                 error: function (response) {

                                 }
                             });
                             isLoading(true);

                         }
                     },


                     sendEmailReport = function () {


                         if (SelectedEmailReport() != null) {
                             var emailMessage = CKEDITOR.instances.content.getData();
                             SelectedEmailReport().emailSignature(emailMessage);

                             dataservice.sendEmail({
                                 To: SelectedEmailReport().emailTo(),
                                 CC: SelectedEmailReport().emailCC(),
                                 Subject: SelectedEmailReport().emailSubject(),
                                 Attachment: SelectedEmailReport().emailAttachment(),
                                 AttachmentPath: SelectedEmailReport().emailAttachmentPath(),
                                 Signature: SelectedEmailReport().emailSignature(),
                                 ContactId: ContactId(),
                                 SignedBy: SignedBy(),

                             }, {
                                 success: function (data) {

                                     view.hide();
                                     view.hideEmailView();



                                 },
                                 error: function (response) {

                                 }
                             });
                             isLoading(true);

                         }

                     },




                    SelectReportById = function (report) {
                        $(".dd-handle").removeClass("selectedReport")
                        $("#" + report.ReportId()).addClass("selectedReport");
                     //   var scr = "/mis/Home/Viewer?id=" + report.ReportId() + "&itemId=" + selectedItemId();
                        selectedReportId(report.ReportId());
                       // $("#ReportViewerIframid").attr("src", scr);
                        errorList.removeAll();

                    },


                    show = function (CategoryId, IsExternal, ItemId, Name, ItemCode, ItemTitle) {
                        reportcategoriesList.removeAll();
                        ComboValue(undefined);
                        ParamDateFrom(undefined);
                        ParamDateTo(undefined);
                        ParamValue(undefined);
                        selectedItemId(ItemId);
                        selectedReportId(0);
                        selectedItemName(Name);
                        selectedItemCode(ItemCode);
                        selectedItemTitle(ItemTitle);
                        IsExternalReport(IsExternal);
                        if (IsExternal == true) {
                            ReportTitle("Print Order Report");

                        }
                        else {
                            ReportTitle("Report(s)");
                        }
                        //$("#ReportViewerIframid").attr("src", "/mis/Home/Viewer?id=0&itemId=0");
                        if (CategoryId != undefined && CategoryId != null && CategoryId != 0) {
                            dataservice.getreportcategories({
                                CategoryId: CategoryId,
                                IsExternal: IsExternal,

                            }, {
                                success: function (data) {

                                    reportcategoriesList.push(model.ReportCategory(data));
                                    //SelectReportById(model.ReportCategory(data).reports()[0]);
                                    var report = model.ReportCategory(data).reports()[0];
                                    $(".dd-handle").removeClass("selectedReport")
                                    $("#" + report.ReportId()).addClass("selectedReport");
                                   
                                    selectedReportId(report.ReportId());
                                  
                                    errorList.removeAll();

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

                   
               
                    downloadPDFReport = function () {
                        if (selectedReportId() > 0) {
                            var dropDownvalue = ComboValue();

                            dataservice.downloadExternalReport({
                                Reportid: selectedReportId(),
                                ComboValue: dropDownvalue,
                                DateFrom: DateFrom,
                                DateTo: DateTo,
                                ParamValue: ParamValue(),
                                Mode: true,
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        var host = window.location.host;
                                        var path = "http://" + host + data;
                                        //var uri = encodeURI("http://" + host + data);
                                        window.open(path, "_blank");
                                    }
                                    isLoading(false);

                                },
                                error: function (response) {

                                }
                            });
                            isLoading(true);

                        }
                    },

                     downloadExcelReport = function () {

                        
                         if (selectedReportId() > 0) {
                             var dropDownvalue = ComboValue();

                             dataservice.downloadExternalReport({
                                 Reportid: selectedReportId(),
                                 ComboValue: dropDownvalue,
                                 DateFrom: DateFrom,
                                 DateTo: DateTo,
                                 ParamValue: ParamValue(),
                                 Mode: false
                             }, {
                                 success: function (data) {
                                     if (data != null) {
                                         var host = window.location.host;
                                         var path = "http://" + host + data;
                                         //var uri = encodeURI("http://" + host + data);
                                         window.open(path, "_blank");
                                     }
                                     isLoading(false);

                                 },
                                 error: function (response) {

                                 }
                             });
                             isLoading(true);

                         }
                     },

                    hideEmailView = function () {
                        view.hide();
                        view.hideEmailView();

                    },

                    // set order values
                    SetOrderData = function (oSignedBy, oContactId, oRecordId, oCategoryId, oOrderId, oCriteriaParam) {
                        //SignedBy: CategoryId,
                        //ContactId: IsExternal,
                        //RecordId: RecordId,
                        //ReportType: ReportType,
                        //OrderId: OrderId,
                        //CriteriaParam: CriteriaParam
                        SignedBy(oSignedBy);
                        ContactId(oContactId);
                        RecordId(oRecordId);
                        CategoryId(oCategoryId);
                        OrderId(oOrderId);
                        CriteriaParam(oCriteriaParam);



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
                        ko.applyBindings(view.viewModel, view.bindingRootParam);
                        SelectedEmailReport(new model.EmailFields());


                    };

                return {
                    isLoading: isLoading,
                    reportcategoriesList: reportcategoriesList,
                    outputTo: outputTo,
                    initialize: initialize,
                    OpenReport: OpenReport,
                    OpenExternalReport: OpenExternalReport,
                    selectedItemId: selectedItemId,
                    IsExternalReport: IsExternalReport,
                    selectedReportId: selectedReportId,
                    SelectReportById: SelectReportById,
                    selectedItemName: selectedItemName,
                    selectedItemCode: selectedItemCode,
                    selectedItemTitle: selectedItemTitle,
                    SetOrderData: SetOrderData,
                    To: To,
                    CC: CC,
                    Subject: Subject,
                    errorList: errorList,
                    SelectedEmailReport: SelectedEmailReport,
                    show: show,
                    ReportTitle: ReportTitle,
                    hide: hide,
                    showEmailView: showEmailView,
                    hideEmailView: hideEmailView,
                    hTmlMessageA: hTmlMessageA,
                    droppedEmailSection: droppedEmailSection,
                    ckEditorOpenFrom: ckEditorOpenFrom,
                    sendEmailReport: sendEmailReport,
                    reportParamsList: reportParamsList,
                    paramComboList: paramComboList,
                    ComboValue: ComboValue,
                    ParamDateFrom: ParamDateFrom,
                    ParamDateTo: ParamDateTo,
                    ParamValue: ParamValue,
                    OpenReportForParams: OpenReportForParams
                };
            })()
        };

        return ist.reportManager.viewModel;
    });