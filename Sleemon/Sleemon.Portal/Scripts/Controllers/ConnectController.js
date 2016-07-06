//
SleemonPortal.controller('ConnectController', ['$scope', '$timeout', 'Contacts', 'DTOptionsBuilder', 'DTColumnDefBuilder',
    function ($scope, $timeout, Contacts, DTOptionsBuilder, DTColumnDefBuilder) {
        var records = [];
        function createSubTree(endFlag, parentId) {
            if (!endFlag) {
                var items = $.grep(records, function (item) {
                    return item.ParentId == parentId;
                });

                if (items.length === 0) {
                    endFlag = true;
                    return [];
                }

                var res = [];
                angular.forEach(items, function (item, i) {
                    res.push({ "label": item.Name, "id": item.Id, "i": i, "children": createSubTree(endFlag, item.Id) });
                })

                return res;
            }
            else
                return [];
        };

        $scope.treeOptions = {
            injectClasses: {
                ul: 'tree tree-branch-children',
                li: 'tree-branch',
                liSelected: 'tree-selected',
                iExpanded: 'icon-caret ace-icon tree-minus',
                iCollapsed: 'icon-caret ace-icon tree-plus',
                iLeaf: 'icon-item ace-icon fa fa-circle bigger-110',
                label: 'tree-branch-header'
            }
        };

        Contacts.GetDepartment().then(function (result) {
            records = result.records;

            var treeData = [];
            angular.forEach(result.rootIds, function (item) {
                treeData = $.merge(treeData, createSubTree(false, item));
            });

            $scope.treeData = treeData;

            $scope.selected = treeData[0];
            $scope.showSelected(treeData[0]);
        });

        $scope.showSelected = function (node) {
            $scope.Department = node.label;

            Contacts.GetUserListByDepartment(node.id).then(function (result) {
                $scope.Users = result;
            });
        }

        $scope.dtOptions = DTOptionsBuilder.newOptions().withDisplayLength(10).withLanguage({
            "sProcessing": "处理中...",
            "sLengthMenu": "显示 _MENU_ 项结果",
            "sZeroRecords": "没有匹配结果",
            "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
            "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
            "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
            "sInfoPostFix": "",
            "sSearch": "搜索:",
            "sUrl": "",
            "sEmptyTable": "表中数据为空",
            "sLoadingRecords": "载入中...",
            "sInfoThousands": ",",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上页",
                "sNext": "下页",
                "sLast": "末页"
            },
            "oAria": {
                "sSortAscending": ": 以升序排列此列",
                "sSortDescending": ": 以降序排列此列"
            }
        });

        $scope.dtColumnDefs = [
            //DTColumnDefBuilder.newColumnDef(0).notSortable().withClass('sorting_disabled')
        ];
    }
]);