/*
    Module with the view model for Shared Navigation
*/
define("common/sharedNavigation.viewModel",
    ["jquery", "amplify", "ko", "common/confirmation.viewModel"], function ($, amplify, ko, confirmation) {
        var ist = window.ist || {};
        ist.sharedNavigation = {
            viewModel: (function () {
                var// The view 
                    view,
                    // selected Screen Item that have to check ,whther it has changes
                    selectedScreenItem,
                    //save Function
                    saveFunctionCallback = null,
                    //contain href or target link
                    target = ko.observable(),
                    //show Confirmation Dialog
                    showConfirmationDialog = function (data, element) {
                        target(element);
                        if (selectedScreenItem && selectedScreenItem() && selectedScreenItem().hasChanges && selectedScreenItem().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(saveAndNavigate);
                            confirmation.afterCancel(function () {
                                navigateToUrl(element);
                            });
                            confirmation.show();
                            return false;
                        }
                        navigateToUrl();
                        return false;
                    },

                    navigateToUrl = function () {
                        if (!target() || target().localName !== "a" || !target().href) {
                            return;
                        }

                        window.location.href = target().href;
                        // target(undefined);
                    },

                    saveAndNavigate = function () {
                        if (saveFunctionCallback && typeof saveFunctionCallback === "function") {
                            saveFunctionCallback(navigateToUrl);
                        }
                    },
                    // Initialize the view model
                    initialize = function (entity, callback) {
                        selectedScreenItem = entity;
                        saveFunctionCallback = callback;
                    };

                $(function () {
                    $(".my-navigator").on('click', function () {
                        return showConfirmationDialog(null, this);
                    });
                });

                //Show Message, when url change in address bar and changes exist
                window.onbeforeunload = function () {
                    if (selectedScreenItem().hasChanges() && target() === undefined) {
                        // call this if the box wasn't shown.
                        return 'You have made some changes which you might want to save.';
                    }
                };
                return {
                    selectedScreenItem: selectedScreenItem,
                    saveFunctionCallback: saveFunctionCallback,
                    showConfirmationDialog: showConfirmationDialog,
                    initialize: initialize,
                };
            })()
        };

        return ist.sharedNavigation.viewModel;
    });

