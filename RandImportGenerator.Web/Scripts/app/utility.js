//Utility helpers

(function ($, util) {
    var name = "Utility";
    CheckDependencies(name, arguments);

    var defaultNotificationEl = "#notification";

    //Check module dependencies and log
    function CheckDependencies(caller, deps) {
        console.debug(caller + " loaded");
        for (var i = 0; i < deps.length; i++) {
            var dep = deps[i];
            if (!dep || dep == null)
                console.debug(caller + ": Missing dependency at argument idx = " + i);
        }
    }
    util.CheckDependencies = CheckDependencies;

    //Session helper
    util.Session = {
        Get: function (key) {
            var s = window.sessionStorage[key];
            return s == null ? null : JSON.parse(s);
        },
        Set: function (key, obj) {
            window.sessionStorage[key] = JSON.stringify(obj);
        },
        Reset: function (key) {
            window.sessionStorage[key] = null;
        }
    };

    //Console logger
    util.Log = function (src, msg, err) {
        console.debug(src + ": " + msg + (err ? " - " + err.message : ""));
    };

    //User notifications
    util.Notification = function (el) {
        el = el || defaultNotificationEl;
        return {
            Alert: function (msg, isError) {
                //TODO: add in fancy modal
                alert(msg);
            },
            UI: function (msg, isError) {
                var e = $(el);
                if (isError) {
                    e.addClass("error-text");
                }
                else {
                    e.removeClass("error-text");
                }
                e.html(msg);
            },
            Prompt: function (msg, isError) {
                return prompt(msg);
            }
        }
    };

    //UI loader/spinner
    util.Loader = function (el) {
        return {
            Show: function () {

            },
            Hide: function () {

            }
        }
    };

    //load urls
    var conf = $("#config").html();
    if (conf) {
        util.Config = JSON.parse(conf);
    }

    //load urls
    var urls = $("#urlConfig").html();
    if (urls) {
        util.Urls = JSON.parse(urls);
    }

    //init modals
    function InitModal(el) {
        return {
            SetTemplate: function (template) {
                $(el).find(".modal-content").html(template);
            },
            Show: function () {
                $(el).modal("show");
            },
            Hide: function () {
                $(el).modal("hide");
            }
        }
    }
    util.AppModal = InitModal("#appModal");

})(jQuery, app.Utility);