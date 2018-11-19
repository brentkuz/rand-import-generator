// Column List app

(function ($, Vue, util, initializers) {
    var name = "ColumnListApp";
    util.CheckDependencies(name, arguments);

    initializers.ColumnListApp = function () {
        console.debug(name + " init");

        var listEl = "#columnListApp";

        var blocker = util.Blocker(listEl);

        var columnListApp = new Vue({
            el:listEl,
            data: {
                Columns: [],
                IsEdit: false
            },
            created: function () {
                //subscriptions
                $.Topic("RefreshColumnList").Subscribe(this.Refresh);
            },
            destroyed: function(){
                $.Topic("RefreshColumnList").Unsubscribe(this.Refresh);
            },
            methods: {
                Refresh: function (columns) {
                    this.Columns = [];
                    for (var i = 0; i < columns.length; i++) {
                        this.Columns.push(columns[i]);
                    }
                    this.IsEdit = false;
                    blocker.Unblock();
                },
                Delete: function (name) {
                    $.Topic("DeleteColumn").Publish(name);
                },
                Edit: function (name) {
                    this.IsEdit = true;
                    blocker.Block();
                    $.Topic("EditColumn").Publish(name);
                }
            }
        });
    };



})(jQuery, Vue, window.App.Utility, window.App.Initializers);
