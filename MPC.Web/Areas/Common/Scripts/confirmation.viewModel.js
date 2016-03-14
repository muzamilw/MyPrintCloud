/*
    Module with the view model for Confirmation
*/
define("common/confirmation.viewModel",
    ["jquery", "amplify", "ko"], function ($, amplify, ko) {
        var ist = window.ist || {};
        ist.confirmation = {
            viewModel: (function () {
                var// The view 
                    view,
                    // True if we are loading data
                    isLoading = ko.observable(false),
                    // default Header text
                    defaultHeaderText = ist.resourceText.defaultHeaderText,
                    // Heading Text
                    headingText = ko.observable(defaultHeaderText),
                    // default confirmation text
                    defaultConfirmationText = "Do you want to proceed with the request?",
                    // Message Text
                    messageText = ko.observable(defaultConfirmationText),
                     //Delete Message Text
                    deletemessageText = ko.observable(),

                    defaultButtonTextYes = "Yes",
                    yesBtnText = ko.observable(defaultButtonTextYes),

                    defaultButtonTextNo = "No",

                    noBtnText = ko.observable(defaultButtonTextNo),

                    defaultIsCancelVisible = true;

                   IsCancelVisible = ko.observable(defaultIsCancelVisible),
                    // On Proceed
                    afterProceed = ko.observable(),
                // On Proceed
                    afterActionProceed = ko.observable(),

                    // On Cancel
                    afterCancel = ko.observable(),
                    // On No
                    afterNo = ko.observable(),
                    // Is Proceed Visible
                    isProceedVisible = ko.observable(true),


                     // Comments for logs
                    comment = ko.observable(),

                    UserRandomNum = ko.observable(),

                    RandomNumber = ko.observable(),
                     //errors = ko.validation.group({
                     //    comment: comment,
                     //    UserRandomNum: UserRandomNum
                     //}),
                    // Proceed with the request
                    proceed = function () {
                        if (typeof afterProceed() === "function") {
                            afterProceed()();
                        }
                        hide();
                        
                    },
                    // Proceed with the request
                    proceedAction = function () {

                        if (comment() == "" || comment() == undefined)
                        {
                            toastr.error("Please enter comment to submit!");
                            return false;
                        }
                        if (UserRandomNum() == "" || UserRandomNum() == undefined) {
                            toastr.error("Please enter number to submit!");
                            return false;
                        }

                        if (UserRandomNum() == RandomNumber())
                        {
                           
                            if (typeof afterActionProceed() === "function") {
                                afterActionProceed()();
                            }
                            hideActionPopup();
                        }
                        else
                        {
                            toastr.error("Number not match!");
                            return false;
                        }

                    },
                    // Reset Dialog
                    resetDialog = function () {
                        afterCancel(undefined);
                        afterProceed(undefined);
                        
                        afterNo(undefined);
                        isProceedVisible(true);
                        headingText(defaultHeaderText);
                        messageText(defaultConfirmationText);
                        yesBtnText(defaultButtonTextYes);
                        noBtnText(defaultButtonTextNo);
                        IsCancelVisible(defaultIsCancelVisible);
                    },
                    // Show the dialog
                    show = function () {
                        isLoading(true);
                        view.show();
                    },
                    // Hide the dialog
                    hide = function () {
                        // Reset Call Backs
                        resetDialog();
                        view.hide();
                    },


                    showWarningPopup = function () {
                        isLoading(true);
                        view.showWarningPopup();
                    },
                    showUpgradePopup = function () {
                        isLoading(true);
                        view.showUpgradePopup();
                    },
                    showActionPopup = function () {
                        isLoading(true);
                        var num = Math.floor(Math.random() * 90000) + 10000;
                        RandomNumber(num);
                       
                          view.showActionPopup();
                      },

                    // Hide the dialog
                    hideWarningPopup = function () {
                        // Reset Call Backs
                        resetDialog();
                        view.hideWarningPopup();
                        view.hide();
                    },

                     // Hide the dialog
                    hideActionPopup = function () {
                        // Reset Call Backs
                        resetDialog();
                        view.hideActionPopup();
                        view.hide();
                    },
                    // Cancel 
                    cancel = function () {
                        if (typeof afterCancel() === "function") {
                            afterCancel()();
                        }
                        hide();
                    },
                      // Cancel 
                    Warningcancel = function () {
                        if (typeof afterCancel() === "function") {
                            afterCancel()();
                        }
                        hideWarningPopup();
                    },
             
                    ActionPopupCancel = function () {
                        if (typeof afterCancel() === "function") {
                            afterCancel()();
                        }
                        hideActionPopup();
                    },
                    // No
                    no = function () {
                        if (typeof afterNo() === "function") {
                            afterNo()();
                        }
                        hide();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        ko.applyBindings(view.viewModel, view.bindingRootq);
                        ko.applyBindings(view.viewModel, view.bindingRootupgrade);
                        ko.applyBindings(view.viewModel, view.bindingRootaction);
                    };

                return {
                    isLoading: isLoading,
                    headingText: headingText,
                    initialize: initialize,
                    show: show,
                    cancel: cancel,
                    Warningcancel: Warningcancel,
                    proceed: proceed,
                    proceedAction: proceedAction,
                    no: no,
                    afterProceed: afterProceed,
                    afterActionProceed: afterActionProceed,
                    afterCancel: afterCancel,
                    afterNo: afterNo,
                    isProceedVisible: isProceedVisible,
                    resetDialog: resetDialog,
                    messageText: messageText,
                    yesBtnText: yesBtnText,
                    noBtnText: noBtnText,
                    IsCancelVisible: IsCancelVisible,
                    hide: hide,
                    showWarningPopup: showWarningPopup,
                    hideWarningPopup: hideWarningPopup,
                    showUpgradePopup: showUpgradePopup,
                    showActionPopup: showActionPopup,
                    hideActionPopup: hideActionPopup,
                    comment: comment,
                    ActionPopupCancel: ActionPopupCancel,
                    //errors: errors,
                    UserRandomNum: UserRandomNum,
                    deletemessageText: deletemessageText
                };
            })()
        };

        return ist.confirmation.viewModel;
    });

