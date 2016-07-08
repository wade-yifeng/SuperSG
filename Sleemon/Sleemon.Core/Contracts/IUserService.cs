namespace Sleemon.Core
{
    using System.Collections.Generic;

    using Sleemon.Data;

    public interface IUserService
    {
        SyncResult SyncUser(IEnumerable<UserProfile> users);

        UserViewModel GetUserInfoById(string userId);

        User GetUserById(string userId);

        IList<User> GetUsersForDepartment(int departmentId);

        UserDailySignInModel UserDailySignIn(string userId);

        User GetUserByUniqueId(string userUniqueId);

        User Login(string userUniqueId, string password);

        bool ResetPassword(string userId, string password);

        IList<UserListModel> GetUserList(int pageIndex, int pageSize,string depName, string userName);

        ResultBase SetUserRole(string currentUserId, string userUniqueid, int roleid);

        ResultBase UpdateUserProfile(UserProfile userProfile);

        bool IsOriginalPassword(string userId);

        IList<Permission> GetUserPermissions(string userUniqueId);
    }
}