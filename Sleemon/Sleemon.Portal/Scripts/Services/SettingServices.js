//
SleemonPortal.service('Contacts', function ($http, Promise, HttpGet) {
    return {
        GetDepartment: function () {
            return Promise(function (defer) {
                HttpGet('/Setting/GetDepartment', null, defer);
            });
        },
        GetUserListByDepartment: function (departmentId) {
            return Promise(function (defer) {
                HttpGet('/Setting/GetUserListByDepartment', { departmentId: departmentId }, defer);
            });
        }
    };
});