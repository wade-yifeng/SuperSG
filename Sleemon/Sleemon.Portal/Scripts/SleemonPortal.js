var SleemonPortal = angular.module('SleemonPortal', [
    'oc.lazyLoad',
    'ui.bootstrap',
    'ui.router',
	'ace.directives',
    'ngStorage'
]);

// $http验证结果处理
SleemonPortal.factory('AuthInterceptor', AuthInterceptor);

var configFunction = function ($stateProvider, $httpProvider) {
    $stateProvider
        .state('Setting', {
            'abstract': true,
            //url: 'Setting/Index/',
            title: '基础设置',
            template: '<ui-view/>',
            icon: 'fa fa-list-alt'
        })
        .state('Setting.Connect', {
            url: '/Setting-Connect',
            title: '连接企业号',
            templateUrl: '/Setting/Connect/',
            controller: 'ConnectController',
            resolve: {
                lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'treeControl',
                            files: ['/Assets/components/angular-tree-control/angular-tree-control.min.js']
                        },
                        {
                            serie: true,
                            name: 'dataTables',
                            files: ['/Assets/components/datatables/media/js/jquery.dataTables.min.js', '/Assets/components/datatables/jquery.dataTables.bootstrap.min.js', '/Assets/components/angular-datatables/dist/angular-datatables.min.js']
                        }]);
                }]
            }
        })
        .state('Setting.Admin', {
            url: '/Setting-Admin',
            title: '管理人员',
            templateUrl: '/Setting/Admin/'
        })
        .state('Setting.Roles', {
            url: '/Setting-Roles',
            title: '角色/权限',
            templateUrl: '/Setting/Roles/'
        })
        .state('Setting.Global', {
            url: '/Setting-Global',
            title: '全局设置',
            templateUrl: '/Setting/Global/'
        });

    $httpProvider.interceptors.push('AuthInterceptor');
}
configFunction.$inject = ['$stateProvider', '$httpProvider'];

SleemonPortal.config(configFunction);