//App

(function ($, util, services, models) {
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
                publish: callbacks.fire,
                subscribe: callbacks.add,
                unsubscribe: callbacks.remove
            };
            if (id) {
                topics[id] = topic;
            }
        }
        return topic;
    };

})(jQuery, app.Utility, app.Services, app.Models);