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
                    Columns: []

                },
                IsEdit: false
            },
            created: function () {
                //private methods
                this.FindColumn = function (name) {
                    return $.grep(this.Definition.Columns, function (item) {
                        return item.Name === name;
                    })
                };
                
                this.columnTypes = JSON.parse(config.ColumnTypes);
                var options = [];                    
                for (var key in this.columnTypes) {
                    options.push({ Value: key, Text: this.columnTypes[key] });
                }

                this.ColumnTypeOptions = options;

                //subscriptions
                $.Topic("AddColumn").Subscribe(this.AddColumn);
                $.Topic("DeleteColumn").Subscribe(this.DeleteColumn);
                $.Topic("EditColumn").Subscribe(this.EditColumn);
                $.Topic("UpdateColumn").Subscribe(this.UpdateColumn);
                $.Topic("CancelEdit").Subscribe(this.CancelEdit);

                //load components
                initializers.ColumnListApp();
            },
            destroyed: function(){
                //subscriptions
                $.Topic("AddColumn").Unsubscribe(this.AddColumn);
                $.Topic("DeleteColumn").Unsubscribe(this.DeleteColumn);
                $.Topic("EditColumn").Subscribe(this.EditColumn);
                $.Topic("UpdateColumn").Unsubscribe(this.UpdateColumn);
                $.Topic("CancelEdit").Unsubscribe(this.CancelEdit);
            },
            methods: {
                LoadColumnEditor: function (columnToEdit) {
                    try {
                        var self = this;
                        var type = columnToEdit != undefined ? columnToEdit.Type : this.ColumnType;
                        $.get(urls.CSVBuilder_GetColumnTemplate + type,
                            null,
                            function (resp) {
                                if (resp != null) {
                                    $(editorEl).html(resp);
                                    var columnType = self.columnTypes[type];
                                
                                    var initializerName = (columnType + "ColumnApp").replace(" ", "");
                                    initializers[initializerName](type, columnToEdit);
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
                AddColumn: function (column) {
                    var columnWithName = this.FindColumn(column.Name);
                    if (columnWithName.length > 0) {
                        notification.UI("Column with the name " + column.Name + " already exists", true);
                        return;
                    }
                    this.Definition.Columns.push(column);

                    //refresh UI
                    this.RefreshList();
                    this.LoadColumnEditor();

                    notification.UI("Success", false);
                },
                DeleteColumn: function (name) {
                    var column = this.FindColumn(name);
                    if (column.length == 0) {
                        notification.UI("Column with the name " + name + " does not exist. Please refresh the page.", true);
                    }
                    //must use splice for Vue reactivity
                    this.Definition.Columns.splice(this.Definition.Columns.indexOf(column), 1)

                    this.RefreshList();

                    notification.UI("Success", false);
                },
                EditColumn: function(name){
                    var columns = this.FindColumn(name);
                    if (columns.length > 1) {
                        notification.UI("There are multiple columns with the name " + name + ". Please refresh the page.", true);
                    }
                    var columnToEdit = columns[0];

                    this.IsEdit = true;
                    this.ColumnType = columnToEdit.Type;
                    //load editor
                    this.LoadColumnEditor(columnToEdit);
                },
                UpdateColumn: function(column){
                    this.IsEdit = false;
                    this.RefreshList();

                    this.LoadColumnEditor();

                    notification.UI("Success", false);
                },
                CancelEdit: function () {
                    this.IsEdit = false;
                    this.LoadColumnEditor();
                    this.RefreshList();
                },
                RefreshList: function () {
                    var self = this;
                    var displayColumns = $.map(this.Definition.Columns, function (item) {
                        return { Name: item.Name, Type: self.columnTypes[item.Type], Order: item.Order };
                    });
                    $.Topic("RefreshColumnList").Publish(displayColumns);
                }
            }
        });
       

    });

})(jQuery, Vue, window.App.Utility, window.App.Initializers);