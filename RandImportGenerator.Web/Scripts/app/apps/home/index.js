(function ($, util) {
    var name = "Index";
    util.CheckDependencies(name, arguments);

    var notification = util.Notification();
    var config = util.Config;
    var urls = util.Urls;

    var editorEl = "#columnEditorContent";

    $(function () {
        var indexApp = new Vue({
            el: "#indexApp",
            data: {
                ColumnTypeOptions: [],
                ColumnType: null
            },
            created: function () {
                if (config.ColumnTypes !== null) {
                    var columnTypes = JSON.parse(config.ColumnTypes);
                    var options = [];
                    
                    for (var key in columnTypes) {
                        options.push({ Value: key, Text: columnTypes[key] });
                    }
                    this.ColumnTypeOptions = options;
                    
                    
                }
            },
            methods: {
                LoadColumnEditor: function () {
                    try{
                        $.get(urls.CSVBuilder_GetColumnTemplate + this.ColumnType,
                            null,
                            function (resp) {
                                $(editorEl).html(resp);
                            },
                            "html"
                            );
                        
                    } catch (err) {
                        notification.UI("An error occurred.", true);
                        throw err;
                    }

                }
            }
        });
       

    });



})(jQuery, window.App.Utility);