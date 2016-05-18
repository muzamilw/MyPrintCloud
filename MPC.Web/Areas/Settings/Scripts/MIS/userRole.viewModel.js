/*
    Module with the view model for Phrase Library
*/
define("userRole/userRole.viewModel",
    ["jquery", "amplify", "ko", "userRole/userRole.dataservice", "userRole/userRole.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.userRole = {
            viewModel: (function () {
                var // The view 
                    view,
                   selectedRole = ko.observable(model.Role()),
                   rolesList = ko.observableArray([]),
                    accessRights = ko.observableArray([]),
                    selectedRights = ko.observableArray([]),
                    isApplyToAll = ko.observable(),
                    getUserRoles = function () {
                        dataservice.getUserRoles({
                            success: function (data) {
                                if (data != null) {
                                    rolesList.removeAll();
                                    _.each(data.UserRoles, function (role) {
                                        rolesList.push(model.Role.Create(role));
                                    });
                                    accessRights.removeAll();
                                    _.each(data.AccessRights, function (access) {
                                        accessRights.push(access);
                                    });
                                }
                                
                            },
                            error: function () {
                                toastr.error("Error: Failed To load roles ", "", ist.toastrOptions);
                            }
                        });
                        
                    },
                    editRole = function (role) {
                        selectedRole(undefined);
                        selectedRole(role);
                        setRoleSelectedRights(selectedRole().roleRights());
                        selectedRole().reset();
                        isApplyToAll.subscribe(function () {
                            selectAllRights();
                        });
                        accessRights.subscribe(function () {
                            checkeChanged();
                        });
                        if (selectedRole().isCompanyLevel() == 1)
                            isApplyToAll(true);
                        view.showRolesDialog();
                    },
                    createNewUserRole = function() {
                        var role = model.Role.Create({});
                        selectedRole(role);
                        isApplyToAll.subscribe(function () {
                            selectAllRights();
                        });
                        accessRights.subscribe(function () {
                            checkeChanged();
                        });
                        if (selectedRole().isCompanyLevel() == 1)
                            isApplyToAll(true);
                        view.showRolesDialog();
                    },
                    updateRoleRights = function() {
                        selectedRights.removeAll();
                        selectedRole().roleRights.removeAll();
                        _.each(accessRights(), function (access) {
                            if (access.IsSelected == true) {
                                selectedRights.push(access);
                                var right = getRight(access.RightId);
                                selectedRole().roleRights.push({ RoleId: selectedRole().roleId(), RightId: access.RightId, RightName: right.RightName });
                            }
                        });
                        saveRole();
                    },
                    getRight = function(rightId) {
                        return accessRights.find(function (right) {
                            return right.RightId === rightId;
                        });
                    },
                    setRoleSelectedRights = function (rightsList) {
                        var tempRights = [];
                        _.each(accessRights(), function (access) {
                            access.IsSelected = false;
                            tempRights.push(access);
                        });
                        accessRights.removeAll();
                        _.each(tempRights, function (access) {
                            _.each(rightsList, function(right) {
                                if (access.RightId == right.RightId) {
                                    access.IsSelected = true;
                                }
                            });
                        });
                        ko.utils.arrayPushAll(accessRights(), tempRights);
                        accessRights.valueHasMutated();
                    },
                    checkeChanged = function () {
                        var isChange = selectedRole().isCheckChange();
                        selectedRole().isCheckChange(isChange ? false : true);
                        selectedRole().isCompanyLevel(isChange ? 1 : 0);
                    },
                    saveRole = function () {
                        var role = selectedRole().convertToServerData();
                        if (selectedRole().isValid()) {
                            dataservice.saveUserRole(selectedRole().convertToServerData(), {
                                success: function () {
                                    selectedRole(undefined);
                                    view.hideRolesDialog();
                                    toastr.success("Role saved successfully.");
                                },
                                error: function (response) {
                                    toastr.error("Error: Failed To save role." + response, "", ist.toastrOptions);
                                }
                            });
                        }
                        
                    },
                    selectAllRights = function () {
                        var tempRights = [];
                        //selectedRole().isCheckChange(true);
                        
                        if (isApplyToAll() == true) {
                            selectedRole().isCompanyLevel(1);
                            _.each(accessRights(), function (access) {
                                access.IsSelected = true;
                                tempRights.push(access);
                            });
                            accessRights.removeAll();
                            ko.utils.arrayPushAll(accessRights(), tempRights);
                            accessRights.valueHasMutated();
                        } else {
                            selectedRole().isCompanyLevel(0);
                            _.each(accessRights(), function (access) {
                                access.IsSelected = false;
                                tempRights.push(access);
                            });
                            accessRights.removeAll();
                            ko.utils.arrayPushAll(accessRights(), tempRights);
                            accessRights.valueHasMutated();
                        }

                    },
                    
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     getUserRoles();
                    
                 };

                return {
                    
                    initialize: initialize,
                    rolesList: rolesList,
                    editRole: editRole,
                    createNewUserRole: createNewUserRole,
                    updateRoleRights: updateRoleRights,
                    accessRights: accessRights,
                    selectedRole: selectedRole,
                    checkeChanged: checkeChanged,
                    isApplyToAll: isApplyToAll
                   
                };
            })()
        };

        return ist.userRole.viewModel;
    });

