// Column List app

(function ($, Vue, util, initializers) {
    var name = "ColumnListApp";
    util.CheckDependencies(name, arguments);

    initializers.ColumnListApp = function () {
        console.debug(name + " init");

        var autoIncrementedColumnApp = new Vue({
            el: "#columnListApp",
            data: {
                Definition: {
                    Name: "",
                    Order: null,
                    StartingSequenceNumber: null,
                    IncrementValue: null
                }
            },
            created: function () {
                //subscriptions
                $.Topic("RefreshColumnList").subscribe(this.Refresh);
            },
            methods: {
                Refresh: function (columns) {
                    alert(JSON.stringify(columns))
                }
            }
        });
    };



})(jQuery, Vue, window.App.Utility, window.App.Initializers);
