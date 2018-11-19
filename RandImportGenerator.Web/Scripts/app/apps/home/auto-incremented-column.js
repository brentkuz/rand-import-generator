// Auto-Incremented Column app

(function ($, Vue, util, initializers, models) {
    var name = "AutoIncrementedColumnApp";
    util.CheckDependencies(name, arguments);

    initializers.AutoIncrementedColumnApp = function () {
        console.debug(name + " init");

        var autoIncrementedColumnApp = new Vue({
            el: "#autoIncrementedColumnApp",
            data: {
                Definition: new models.AutoIncrementedColumn()
            },
            created: function () {

            },
            methods: {
                Submit: function () {
                    $.Topic("AddColumn").Publish("AutoIncremented", this.Definition);
                    this.Definition = new models.AutoIncrementedColumn();
                }
            }
        });
    };

})(jQuery, Vue, window.App.Utility, window.App.Initializers, window.App.Models);