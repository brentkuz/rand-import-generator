//App

(function ($, app, util) {
    var name = "App";
    util.CheckDependencies(name, arguments);
    //pub/sub
    var topics = {};
    $.Topic = function (id) {
        var callbacks,
            topic = id && topics[id];
        if (!topic) {
            callbacks = $.Callbacks();
            topic = {
                Publish: callbacks.fire,
                Subscribe: callbacks.add,
                Unsubscribe: callbacks.remove
            };
            if (id) {
                topics[id] = topic;
            }
        }
        return topic;
    };

    app.EventBus = new Vue();

})(jQuery, window.App, window.App.Utility);