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
                     SelectedEmailReport = ko.observable(),
                     SignedBy = ko.observable(),
                     ContactId = ko.observable(),
                     RecordId = ko.observable(),
                     OrderId = ko.observable(),
                      CategoryId = ko.observable(),
                     CriteriaParam = ko.observable(),

                     ReportTitle = ko.observable(),
                     //SignedBy: CategoryId,
                        //ContactId: IsExternal,
                        //RecordId: RecordId,
                        //ReportType: ReportType,
                        //OrderId: OrderId,
                        //CriteriaParam: CriteriaParam


                       ckEditorOpenFrom = ko.observable("stores"),
                    hTmlMessageA = ko.observable(),


                OpenExternalReport = function(CategoryId, IsExternal, ItemId)
                {
                    selectedItemId(ItemId);


                    $("#ReportViewerIframid").attr("src", "/mis/Home/Viewer?id=0&itemId=0");
                    if (CategoryId != undefined && CategoryId != null && CategoryId != 0) {
                        dataservice.getreportcategories({
                            CategoryId: CategoryId,
                            IsExternal: IsExternal,

                        }, {
                            success: function (data) {

                                reportcategoriesList.push(model.ReportCategory(data));
                                SelectReportById(model.ReportCategory(data).reports()[0]);

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
                            //  getParams();

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
                                 SignedBy: SignedBy(),
                                 ContactId: ContactId(),
                                 RecordId: RecordId(),
                                 ReportType: CategoryId(),
                                 OrderId: OrderId(),
                                 CriteriaParam: CriteriaParam()
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


                     sendEmailReport = function()
                     {
                         

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
                        if (IsExternal == true)
                        {
                            ReportTitle("Print Order Report");

                        }
                        else
                        {
                            ReportTitle("Report(s)");
                        }
                        $("#ReportViewerIframid").attr("src", "/mis/Home/Viewer?id=0&itemId=0");
                        if (CategoryId != undefined && CategoryId != null && CategoryId != 0) {
                            dataservice.getreportcategories({
                                CategoryId: CategoryId,
                                IsExternal: IsExternal,

                            }, {
                                success: function (data) {

                                    reportcategoriesList.push(model.ReportCategory(data));
                                    SelectReportById(model.ReportCategory(data).reports()[0]);
                                    
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
                            dataservice.downloadExternalReport({
                                ReportId: selectedReportId(),
                                Mode: true                               
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
                             dataservice.downloadExternalReport({
                                 ReportId: selectedReportId(),
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
                    SetOrderData = function(oSignedBy,oContactId,oRecordId,oCategoryId,oOrderId,oCriteriaParam)
                    {
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
                    IsExternalReport:IsExternalReport,
                    selectedReportId: selectedReportId,
                    SelectReportById: SelectReportById,
                    selectedItemName:selectedItemName,
                    selectedItemCode :selectedItemCode,
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
                    sendEmailReport: sendEmailReport
                };
            })()
        };

        return ist.reportManager.viewModel;
    });