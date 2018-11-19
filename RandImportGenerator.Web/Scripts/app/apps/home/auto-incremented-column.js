// Auto-Incremented Column app

(function ($, Vue, util, initializers) {
    var name = "AutoIncrementedColumnApp";
    util.CheckDependencies(name, arguments);

    initializers.AutoIncrementedColumnApp = function () {
        console.debug(name + " init");

        var autoIncrementedColumnApp = new Vue({
            el: "#autoIncrementedColumnApp",
            data: {
                Definition: {
                    Name: "",
                    Order: null,
                    StartingSequenceNumber: null,
                    IncrementValue: null
                }
            },
            created: function () {

            },
            methods: {
                Submit: function () {
                    $.Topic("AddColumn").publish("AutoIncremented", this.Definition);
                }
            }
        });
    };

})(jQuery, Vue, window.App.Utility, window.App.Initializers);