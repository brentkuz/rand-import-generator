(function ($, Vue, app, util, initializers) {
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
                CurrentEditor: null,
                ToEdit: null,
                EditorType: null,
                IsEdit: false,
                EditorKey: 0
            },
            mounted: function () {
                //private methods
                this.FindColumn = function (name) {
                    var match = $.grep(this.Definition.Columns, function (item) {
                        return item.Name === name;
                    });
                    if (match.length == 1) {
                        return match[0];
                    } else {
                        return null;
                    }
                };
                
                this.columnTypes = JSON.parse(config.ColumnTypes);
                var options = [];                    
                for (var key in this.columnTypes) {
                    options.push({ Value: key, Text: this.columnTypes[key] });
                }

                this.ColumnTypeOptions = options;

                //subscriptions
                app.EventBus.$on("Editor_Add", this.AddColumn)
                app.EventBus.$on("ColumnList_Delete", this.DeleteColumn)
                app.EventBus.$on("ColumnList_Edit", this.EditColumn)
                app.EventBus.$on("Editor_Update", this.UpdateColumn)
                
            },
            methods: {
                LoadColumnEditor: function (columnToEdit) {
                    try {
                        this.EditorKey = this.EditorKey + 1;
                        var self = this;
                        var type = columnToEdit != undefined ? columnToEdit.Type : this.ColumnType;

                        var columnType = self.columnTypes[type];
                        this.CurrentEditor = (columnType + "Column").replace(" ", "");

                        this.ToEdit = columnToEdit;
                        this.EditorType = type;                       

                    } catch (err) {
                        notification.UI("An error occurred.", true);
                        throw err;
                    }
                },
                AddColumn: function (column) {
                    var columnWithName = this.FindColumn(column.Name);
                    if (columnWithName != null) {
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
                    if (column == null) {
                        notification.UI("Column with the name " + name + " does not exist. Please refresh the page.", true);
                    }
                    //must use splice for Vue reactivity
                    this.Definition.Columns.splice(this.Definition.Columns.indexOf(column), 1)

                    this.RefreshList();

                    notification.UI("Success", false);
                },
                EditColumn: function(name){
                    var column = this.FindColumn(name);
                    if (column == null) {
                        notification.UI("There are either multiple columns with the name " + name + " or the column does not exist. Please refresh the page.", true);
                    }

                    this.IsEdit = true;
                    this.ColumnType = column.Type;
                    //load editor
                    this.LoadColumnEditor(column);
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
                    app.EventBus.$emit("ColumnList_Refresh", displayColumns);
                }
            }
        });
       

    });

})(jQuery, Vue, window.App, window.App.Utility, window.App.Initializers);