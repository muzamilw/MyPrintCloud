define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    Role = function (specifiedId, specifiedRoleName, specifiedRoleDescription) {
        var self,
            roleName = ko.observable(specifiedRoleName),
            roleId = ko.observable(specifiedId),
            roleDescription = ko.observable(specifiedRoleDescription),
            roleRights = ko.observableArray([]),
            isCheckChange = ko.observable(),
            roleRightsUi = ko.computed(function () {
                if (!roleRights || roleRights().length === 0) {
                    return "";
                }

                var rights = "";
                roleRights.each(function (right, index) {
                    var rightName = right.RightName;
                    if (index < roleRights().length - 1) {
                        rightName = rightName + ",";
                    }
                    rights += rightName;
                });

                return rights;
            }),
            dirtyFlag = new ko.dirtyFlag({
                roleName: roleName,
                roleDescription: roleDescription,
                roleRights: roleRights,
                isCheckChange: isCheckChange
            }),
            // True If Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset Dirty State
            reset = function () {
                dirtyFlag.reset();
            },

            convertToServerData = function () {
                return {
                    RoleId: roleId(),
                    RoleName: roleName(),
                    RoleDescription: roleDescription(),
                    Rolerights : roleRights()
                };
            };

        self = {
            roleName: roleName,
            roleId: roleId,
            roleDescription : roleDescription,
            dirtyFlag: dirtyFlag,
            roleRights : roleRights,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData,
            roleRightsUi: roleRightsUi,
            isCheckChange: isCheckChange

        };
        return self;
    };
    Role.Create = function (source) {
        var role = new Role(source.RoleId, source.RoleName, source.RoleDescription);

        if (source.Rolerights && source.Rolerights.length > 0) {
            var rolerights = [];

            _.each(source.Rolerights, function (item) {
                rolerights.push(item);
            });

            // Push to Original Item
            ko.utils.arrayPushAll(role.roleRights(), rolerights);
            role.roleRights.valueHasMutated();
        }

        // Reset State to Un-Modified
        role.reset();

        return role;
    };
   
    return {
        Role: Role,
        
    };
   
});