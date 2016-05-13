﻿/*
    Module with the view model for Phrase Library
*/
define("common/systemUser.viewModel",
    ["jquery", "amplify", "ko", "common/systemUser.dataservice", "common/systemUser.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.systemUser = {
            viewModel: (function () {
                var // The view 
                    view,
                    selectedSystemUser = ko.observable(model.SystemUser()),
                   usersList = ko.observableArray([]),
                    getSystemUserSignature = function () {
                        dataservice.getSystemUserSignature({
                            success: function (data) {
                                if (data != null) {
                                    selectedSystemUser().signature(data);
                                    selectedSystemUser().reset();
                                }
                                view.showSystemUserDialog();
                            },
                            error: function () {
                                toastr.error("Error: Failed To load signature ", "", ist.toastrOptions);
                            }
                        });
                        
                    },
                    getSystemUsers = function () {
                        dataservice.getSystemUsers({
                            success: function (data) {
                                if (data != null) {
                                    usersList.removeAll();
                                    _.each(data.SystemUsers, function (user) {
                                        usersList.push(model.SystemUser.Create(user));
                                    });
                                }
                            },
                            error: function () {
                                toastr.error("Error: Failed To load system users ", "", ist.toastrOptions);
                            }
                        });

                    },
                    saveSignature = function () {
                        var user = selectedSystemUser().convertToServerData();
                        dataservice.saveSystemUserSignature(selectedSystemUser().convertToServerData(), {
                            success: function () {
                                selectedSystemUser(undefined);
                                view.hideSystemUserDialog();
                                toastr.success("Singature saved successfully.");
                            },
                            error: function (response) {
                                toastr.error("Error: Failed To save signature." + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    editUser = function() {
                        
                    },
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     if (view.bindingRoot)
                         ko.applyBindings(view.viewModel, view.bindingRoot);
                     if (view.userListBinding) {
                         ko.cleanNode(view.userListBinding);
                         ko.applyBindings(view.viewModel, view.userListBinding);
                     }
                     
                     if (page != undefined && page == "all") {
                         getSystemUsers();
                     }
                 };

                return {
                    getSystemUserSignature: getSystemUserSignature,
                    initialize: initialize,
                    saveSignature: saveSignature,
                    selectedSystemUser: selectedSystemUser,
                    editUser: editUser,
                    usersList: usersList
                };
            })()
        };

        return ist.systemUser.viewModel;
    });

