namespace Sleemon.Service
{
    using System;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    using Sleemon.Core;
    using Sleemon.Data;
    using Sleemon.Common;

    public class UserService : IUserService
    {
        private readonly ISleemonEntities _invoicingEntities;
        public UserService()
        {
            this._invoicingEntities = new SleemonEntities();
        }

        public SyncResult SyncUser(IEnumerable<UserProfile> users)
        {
            var userList = users.ToList();
            var result = new SyncResult()
            {
                IsSuccess = true,
                Quantity = userList.Count,
                StatusCode = (int)StatusCode.Success
            };

            var userDepartment = (from user in userList
                                  from department in user.Department
                                  select new UserDepartmentSyncModel() { DepartmentId = department, UserId = user.UserId }).ToList();

            this._invoicingEntities.spSyncUser(Utilities.GetXElementFromObject(userList).ToString());

            this._invoicingEntities.spSyncUserDepartment(Utilities.GetXElementFromObject(userDepartment).ToString());

            return result;
        }

        public UserViewModel GetUserInfoById(string userId)
        {
            var user = this._invoicingEntities.User.FirstOrDefault(p => p.IsActive && p.UserUniqueId.Equals(userId));

            if (user == null) return null;

            return new UserViewModel()
            {
                UserId = user.UserUniqueId,
                Name = user.Name,
                Avatar = user.Avatar,
                Mobile = user.Mobile,
                Grade = user.Grade,
                Point = user.Point,
                ProductAbility = user.ProductAbility,
                SalesAbility = user.SalesAbility,
                ExhibitAbility = user.ExhibitAbility,
                IsAdmin = user.IsAdmin,
                IsSuperAdmin = user.IsSuperAdmin
            };
        }

        public User GetUserById(string userId)
        {
            return this._invoicingEntities.User.FirstOrDefault(p => p.IsActive && p.UserUniqueId == userId);
        }

        public IList<User> GetUsersForDepartment(int departmentId)
        {
            return this._invoicingEntities.Database.SqlQuery<User>(@"
WITH [Departments] AS
(
	SELECT [Department].[ParentId]
          ,[Department].[UniqueId]
	FROM [dbo].[Department]
	WHERE [Department].[IsActive] = 1
		AND [Department].[UniqueId] = @departmentId

	UNION ALL

	SELECT [Department].[ParentId]
          ,[Department].[UniqueId]
	FROM [dbo].[Department]
	JOIN [Departments]
		ON [Departments].[UniqueId] = [Department].[ParentId]
	WHERE [Department].[IsActive] = 1
)
SELECT [User].*
FROM [dbo].[UserDepartment]
JOIN [Departments]
	ON [Departments].[UniqueId] = [UserDepartment].[DepartmentUniqueId]
JOIN [dbo].[User]
    ON [User].[UserUniqueId] = [UserDepartment].[UserUniqueId]
WHERE [User].[IsActive] = 1", new SqlParameter("@departmentId", departmentId)).ToList();
        }

        public UserDailySignInModel UserDailySignIn(string userId)
        {
            var signPoint = 0; //TODO: Get sign point from SystemConfig
            var currentPoint = 0; //TODO: Get user current total point.

            var dailySignInModel = new UserDailySignInModel()
            {
                UserId = userId,
                Point = currentPoint,
                SignInDate = DateTime.Now
            };

            if (this._invoicingEntities.Database.SqlQuery<UserDailySignIn>(@"
SELECT *
FROM [dbo].[UserDailySignIn]
WHERE [UserDailySignIn].[UserUniqueId] = @userId
    AND [UserDailySignIn].[SignInDate] = @currentDate
    AND [UserDailySignIn].[IsActive] = 1",
    new SqlParameter("@userId", userId),
    new SqlParameter("currentDate", DateTime.Now.ToString("yyyy-MM-dd"))).Any())
            {
                dailySignInModel.StatusCode = (int)StatusCode.SignedIn;
                dailySignInModel.Message = "Oops, you already signed in today.";
            }
            else
            {
                var entity = this._invoicingEntities.UserDailySignIn.Create();

                entity.UserUniqueId = userId;
                entity.SignInDate = DateTime.Now;
                entity.LastUpdateTime = DateTime.Now;
                entity.IsActive = true;

                this._invoicingEntities.UserDailySignIn.Add(entity);

                this._invoicingEntities.SaveChanges();

                dailySignInModel.Point += signPoint;
                dailySignInModel.StatusCode = (int)StatusCode.Success;
                dailySignInModel.Message = string.Format("congragulations! you got {0} points", signPoint);
            }

            return dailySignInModel;
        }

        public User GetUserByUniqueId(string userUniqueId)
        {
            return this._invoicingEntities.User.FirstOrDefault(p => p.IsActive && p.UserUniqueId == userUniqueId);
        }

        public User Login(string userUniqueId, string password)
        {
            return
                this._invoicingEntities.User.FirstOrDefault(
                    p => p.IsActive && p.UserUniqueId == userUniqueId && p.Password == password);
        }

        public bool ResetPassword(string userId, string password)
        {
            var entity = this._invoicingEntities.User.FirstOrDefault(p => p.IsActive && p.UserUniqueId == userId);

            if (entity == null) return false;

            entity.Password = password;
            entity.IsOriginalPassword = false;

            this._invoicingEntities.SaveChanges();

            return true;
        }

        public IList<UserListModel> GetUserList(int pageIndex, int pageSize, string depName, string userName)
        {
            var userList = new List<UserListModel>();
            var totalCount = 0;
            var serachList = from ud in this._invoicingEntities.UserDepartment
                             join u in this._invoicingEntities.User on ud.UserUniqueId equals u.UserUniqueId
                             join d in this._invoicingEntities.Department on ud.DepartmentUniqueId equals d.UniqueId
                             join rp in this._invoicingEntities.UserRole on u.UserUniqueId equals rp.UserUniqueId into UserRole1
                             from rp in UserRole1.DefaultIfEmpty()
                             join r in this._invoicingEntities.Role on rp.RoleId equals r.Id into UserRole2
                             from r in UserRole2.DefaultIfEmpty()
                             where (u.Name.IndexOf(userName) >= 0 || string.IsNullOrEmpty(userName)) && (d.Name.IndexOf(depName) >= 0 || string.IsNullOrEmpty(depName)) && u.IsActive == true
                             orderby d.Name, u.Name
                             select new
                             {
                                 u,
                                 DepartmentName = d.Name,
                                 RoleName = r.Name,
                                 Roleid = r.Id == null ? 0 : r.Id//r.id可能为null
                             };
            if (serachList == null || serachList.ToList() == null)
            {
                totalCount = 0;
                return userList;
            }
            totalCount = serachList.ToList().Count;
            var list = serachList.Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();
            if (list == null || list.Count <= 0)
            {
                return userList;
            }
            for (int i = 0; i < list.Count; i++)
            {
                var model = new UserListModel();
                model.User = list[i].u;
                model.DepartmentName = list[i].DepartmentName;
                model.RoleName = list[i].RoleName;
                model.RoleId = list[i].Roleid;
                model.pageIndex = pageIndex;
                model.pageSize = pageSize;
                model.totalCount = totalCount;
                userList.Add(model);
            }
            return userList;
        }

        public bool SetUserRole(string currentUserId, string userUniqueid, int roleid)
        {
            var role = from r in this._invoicingEntities.Role
                       where r.Id == roleid
                       select r;
            if (role == null)
            {
                LogHelper<UserService>.WriteInfo("权限不存在，权限Id:" + roleid);
                return false;
            }
            var userRoleByUser = from ur in this._invoicingEntities.UserRole
                                 where ur.UserUniqueId == userUniqueid
                                 select ur;

            var userRole = this._invoicingEntities.UserRole.Create();
            userRole.IsActive = true;
            userRole.UserUniqueId = userUniqueid;
            userRole.RoleId = roleid;
            userRole.LastUpdateTime = DateTime.UtcNow;
            userRole.LastUpdateUser = currentUserId;
            if (userRoleByUser.ToList().Count > 0)
            {
                this._invoicingEntities.UserRole.Remove(userRoleByUser.ToList()[0]);
            }

            this._invoicingEntities.UserRole.Add(userRole);

            int res = this._invoicingEntities.SaveChanges();
            return res > 0;
        }

        public ResultBase UpdateUserProfile(UserProfile userProfile)
        {
            var result = new ResultBase()
            {
                IsSuccess = true,
                StatusCode = (int)StatusCode.Success
            };

            var userEntity =
                this._invoicingEntities.User.FirstOrDefault(p => p.IsActive && p.UserUniqueId == userProfile.UserId);

            if (userEntity != null)
            {
                userEntity.Name = userProfile.Name;
                userEntity.Position = userProfile.Position;
                userEntity.Mobile = userProfile.Mobile;
                userEntity.Gender = userProfile.Gender == 1;
                userEntity.Email = userProfile.Email;
                userEntity.WeixinId = userProfile.WeixinId;
                userEntity.Avatar = userProfile.Avatar;
                userEntity.Country = userProfile.Country;
                userEntity.Province = userProfile.Province;
                userEntity.City = userProfile.City;
                userEntity.District = userProfile.District;

                this._invoicingEntities.SaveChanges();
            }
            else
            {
                result.IsSuccess = false;
                result.Message = string.Format("Cannot find user by UserId: {0}", userProfile.UserId);
                result.StatusCode = (int)StatusCode.Failed;
            }

            return result;
        }

        public bool IsOriginalPassword(string userId)
        {
            return
                this._invoicingEntities.User.FirstOrDefault(p => p.IsActive && p.UserUniqueId == userId)
                    .IsOriginalPassword;
        }

        public IList<Permission> GetUserPermissions(string userUniqueId)
        {
            return this._invoicingEntities.spGetUserPermissions(userUniqueId).Select(p => new Permission()
            {
                Description = p.Description,
                IconClass = p.IconClass,
                Id = p.Id,
                IsActive = p.IsActive,
                IsMenu = p.IsMenu,
                LastUpdateTime = p.LastUpdateTime,
                LastUpdateUser = p.LastUpdateUser,
                Name = p.Name,
                ParentId = p.ParentId,
                Url = p.Url,
                Sort = p.Sort
            }).ToList();
        }
    }
}
