//nothing important, just a snippet to convert ui.router states into an array of sidebar items to be used in the partial template (sidebar.html)
//make a list of sidebar items using router states in angular/js/app.js
SleemonPortal.service('SidebarList', function () {
    //parent name for a state
    var getParentName = function (name) {
        var name = (/^(.+)\.[^.]+$/.exec(name) || [null, null])[1];
        return name;
    };
    //how many parents does this state have?
    var getParentCount = function (name) {
        return name.split('.').length;
    };

    this.getList = function (uiStateList) {

        var sidebar = { 'root': [] };//let's start with root and call it root! (see views/layouts/default/partial/sidebar.html)
        var parentList = {};//each node(item) can be a parent, so we add it to this list, and later if we find its children we know where to find the parent!

        for (var i = 0 ; i < uiStateList.length ; i++) {
            var state = uiStateList[i];
            if (!state.name) continue;

            //copy state to 'item' (so state is not changed)
            var item = {};
            angular.copy(state, item);
            delete item['resolve']; delete item['templateUrl'];//delete these, we don't need them

            //item.name is state's name (dashboard, ui.elements, etc)
            item.url = item.name || '';

            parentList[item.name] = item;//save this item as a possible parent, and later we add possible children to it as submenu

            var parentName = getParentName(item.name);
            if (!parentName) {
                //no parent, so a root item
                sidebar.root.push(item);
                item['level-1'] = true;
            }
            else {
                //get the parent and add this item as a submenu element of parent
                var parentNode = parentList[parentName];
                if (!('submenu' in parentNode)) parentNode['submenu'] = [];
                parentNode['submenu'].push(item);
                item['level-' + getParentCount(item.name)] = true;
            }
        }

        parentList = null;

        return sidebar;
    };

});

//just load localStorage stored values, such as ace.settings, or ace.sidebar
SleemonPortal.service('StorageGet', function ($localStorage) {

    this.load = function ($scope, name) {
        $localStorage[name] = $localStorage[name] || {};

        var $ref = $scope;
        var parts = name.split('.');//for example when name is "ace.settings" or "ace.sidebar"
        for (var i = 0; i < parts.length; i++) $ref = $ref[parts[i]];
        //now $ref refers to $scope.ace.settings

        for (var prop in $localStorage[name]) if ($localStorage[name].hasOwnProperty(prop)) {
            $ref[prop] = $localStorage[name][prop];
        }
    };

});
