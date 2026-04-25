using System.Collections.Generic;

namespace ZeusApp.Application.Constants;

public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static class Ayarlar
    {
        public const string View = "Permissions.Ayarlar.View";
        public const string Create = "Permissions.Ayarlar.Create";
        public const string Edit = "Permissions.Ayarlar.Edit";
        public const string Delete = "Permissions.Ayarlar.Delete";
    }

    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
    }
}
