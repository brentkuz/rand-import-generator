// Column List app

(function ($, Vue, util, initializers) {
    var name = "ColumnListApp";
    util.CheckDependencies(name, arguments);

    initializers.ColumnListApp = function () {
        console.debug(name + " init");

        var columnListApp = new Vue({
            el: "#columnListApp",
            data: {
                Columns: []
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
                    alert(this.Columns.length)
                }
            }
        });
    };



})(jQuery, Vue, window.App.Utility, window.App.Initializers);
