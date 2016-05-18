/*
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
                    rolesList = ko.observableArray([]),
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
                                    rolesList.removeAll();
                                    _.each(data.UserRoles, function (role) {
                                        rolesList.push(role);
                                    });
                                }
                            },
                            error: function () {
                                toastr.error("Error: Failed To load system users ", "", ist.toastrOptions);
                            }
                        });
                    },
                    saveSignature = function () {
                        
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
                    onsaveSystemUser = function() {
                        saveSystemUser(closeEditor);
                    },
                    saveSystemUser = function (callback) {
                        if (selectedSystemUser().isValid()) {
                            dataservice.saveSystemUser(selectedSystemUser().convertToServerData(), {
                                success: function (data) {
                                    
                                    var roleObj = rolesList.find(function (temp) {
                                        return temp.RoleId == data.RoleId;
                                    });
                                    var newObj = usersList.find(function (temp) {
                                        return temp.userId() == data.SystemUserId;
                                    });
                                    
                                    newObj.roleName(roleObj.RoleName);
                                    toastr.success("User saved successfully.");
                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                },
                                error: function (response) {
                                    toastr.error("Error: Failed To save user." + response, "", ist.toastrOptions);
                                }
                            });
                        }
                    },
                    editUser = function(user) {
                        selectedSystemUser(undefined);
                        selectedSystemUser(user);
                        selectedSystemUser().reset();
                       
                        view.showEditSystemUserDialog();
                    },
                    onCloseUser = function() {
                        if (selectedSystemUser().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(function () {
                                saveSystemUser(closeEditor);
                            });
                            confirmation.afterCancel(function () {
                                selectedSystemUser().reset();
                                closeEditor();
                            });
                            confirmation.show();
                        } else {
                            closeEditor();
                        }
                    },
                    closeEditor = function() {
                        view.hideEditSystemUserDialog();
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
                     var userList = $('#userList').val();
                     if (userList != undefined && userList == "all") {
                         getSystemUsers();
                     }
                 };

                return {
                    getSystemUserSignature: getSystemUserSignature,
                    initialize: initialize,
                    saveSignature: saveSignature,
                    selectedSystemUser: selectedSystemUser,
                    editUser: editUser,
                    usersList: usersList,
                    rolesList: rolesList,
                    onCloseUser: onCloseUser,
                    onsaveSystemUser: onsaveSystemUser
                };
            })()
        };

        return ist.systemUser.viewModel;
    });

