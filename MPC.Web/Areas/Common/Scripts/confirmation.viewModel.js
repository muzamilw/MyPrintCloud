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

                    defaultButtonTextYes = "Yes",
                    yesBtnText = ko.observable(defaultButtonTextYes),

                    defaultButtonTextNo = "No",

                    noBtnText = ko.observable(defaultButtonTextNo),
                    // On Proceed
                    afterProceed = ko.observable(),
                    // On Cancel
                    afterCancel = ko.observable(),
                    // On No
                    afterNo = ko.observable(),
                    // Is Proceed Visible
                    isProceedVisible = ko.observable(true),
                    // Proceed with the request
                    proceed = function () {
                        if (typeof afterProceed() === "function") {
                            afterProceed()();
                        }
                        hide();
                        
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

                    // Hide the dialog
                    hideWarningPopup = function () {
                        // Reset Call Backs
                        resetDialog();
                        view.hideWarningPopup();
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
                    };

                return {
                    isLoading: isLoading,
                    headingText: headingText,
                    initialize: initialize,
                    show: show,
                    cancel: cancel,
                    Warningcancel: Warningcancel,
                    proceed: proceed,
                    no: no,
                    afterProceed: afterProceed,
                    afterCancel: afterCancel,
                    afterNo: afterNo,
                    isProceedVisible: isProceedVisible,
                    resetDialog: resetDialog,
                    messageText: messageText,
                    yesBtnText: yesBtnText,
                    noBtnText: noBtnText,
                    hide: hide,
                    showWarningPopup: showWarningPopup,
                    hideWarningPopup: hideWarningPopup,
                    showUpgradePopup: showUpgradePopup
                };
            })()
        };

        return ist.confirmation.viewModel;
    });

