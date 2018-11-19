// Auto-Incremented Column app

(function ($, Vue, util, initializers, models) {
    var name = "AutoIncrementedColumnApp";
    util.CheckDependencies(name, arguments);

    initializers.AutoIncrementedColumnApp = function (type, columnToEdit) {
        console.debug(name + " init");

        var autoIncrementedColumnApp = new Vue({
            el: "#autoIncrementedColumnApp",
            data: {
                Definition: columnToEdit || new models.AutoIncrementedColumn(type),
                IsEdit: columnToEdit !== undefined
            },
            methods: {
                Submit: function () {
                    if (!this.IsEdit) {
                        $.Topic("AddColumn").Publish(this.Definition);
                    } else {
                        $.Topic("UpdateColumn").Publish(this.Definition);
                    }
                },
                CancelEdit: function () {
                    $.Topic("CancelEdit").Publish();
                }
            }
        });
    };

})(jQuery, Vue, window.App.Utility, window.App.Initializers, window.App.Models);