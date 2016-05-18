define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    Role = function (specifiedId, specifiedRoleName, specifiedRoleDescription, specifiedCompanyLevel) {
        var self,
            roleName = ko.observable(specifiedRoleName || undefined).extend({ required: true, message: 'Name is required'}),
            roleId = ko.observable(specifiedId),
            roleDescription = ko.observable(specifiedRoleDescription),
            isCompanyLevel = ko.observable(specifiedCompanyLevel),
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
                isCheckChange: isCheckChange,
                isCompanyLevel: isCompanyLevel
            }),
            // True If Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset Dirty State
            reset = function () {
                dirtyFlag.reset();
            },
             errors = ko.validation.group({
                 roleName: roleName
             }),
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            convertToServerData = function () {
                return {
                    RoleId: roleId(),
                    RoleName: roleName(),
                    RoleDescription: roleDescription(),
                    Rolerights: roleRights(),
                    IsCompanyLevel: isCompanyLevel()
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
            isCheckChange: isCheckChange,
            isCompanyLevel: isCompanyLevel,
            isValid: isValid

        };
        return self;
    };
    Role.Create = function (source) {
        var role = new Role(source.RoleId, source.RoleName, source.RoleDescription, source.IsCompanyLevel);

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