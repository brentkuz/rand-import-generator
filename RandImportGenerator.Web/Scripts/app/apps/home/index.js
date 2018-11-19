(function ($, Vue, util, initializers) {
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
                ColumnType: null,
                Definition: {
                    AutoIncremented: []

                }
            },
            created: function () {
                if (config.ColumnTypes !== null) {
                    this.columnTypes = JSON.parse(config.ColumnTypes);
                    var options = [];
                    
                    for (var key in this.columnTypes) {
                        options.push({ Value: key, Text: this.columnTypes[key] });
                    }
                    this.ColumnTypeOptions = options;
                }

                //subscriptions
                $.Topic("AddColumn").subscribe(this.AddColumn);
                $.Topic("RemoveColumn").subscribe(this.RemoveColumn);

                //load components
                initializers.ColumnListApp();
            },
            methods: {
                LoadColumnEditor: function () {
                    try {
                        var self = this;
                        $.get(urls.CSVBuilder_GetColumnTemplate + this.ColumnType,
                            null,
                            function (resp) {
                                if (resp != null) {
                                    $(editorEl).html(resp);
                                    var columnType = self.columnTypes[self.ColumnType];
                                    //    $.grep(this.ColumnTypeOptions, function (item) {
                                    //    return item.Value === this.ColumnType;
                                    //});
                                    var initializerName = (columnType + "ColumnApp").replace(" ", "");
                                    initializers[initializerName]();
                                } else {
                                    notification.UI("Failed to load column editor.", true);
                                }
                            },
                            "html");
                        
                    } catch (err) {
                        notification.UI("An error occurred.", true);
                        throw err;
                    }
                },
                AddColumn: function (type, column) {
                    var columns = this.Definition[type];
                    var columnWithName = $.grep(columns, function (item) {
                        return item.Name === column.Name;
                    })
                    if (columnWithName.length > 0) {
                        notification.UI("Column with the name " + column.Name + " already exists", true);
                        return;
                    }
                    columns.push(column);

                    //refresh column list
                    //var displayColumns = $.map()
                    $.Topic("RefreshColumnList").publish(this.Definition);
                },
                RemoveColumn: function (name) {

                }
            }
        });
       

    });

})(jQuery, Vue, window.App.Utility, window.App.Initializers);