//App

(function ($, app, util) {
    var name = "App";
    util.CheckDependencies(name, arguments);
    //init Vue event bus
    app.EventBus = new Vue();

})(jQuery, window.App, window.App.Utility);