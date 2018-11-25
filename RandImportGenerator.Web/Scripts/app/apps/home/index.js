(function ($, Vue, app, util, models) {
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
                QuoteTypeOptions: [],
                ColumnType: null,
                Definition: new models.CSVDefinition(),
                CurrentEditor: null,
                ToEdit: null,
                EditorType: null,
                IsEdit: false,
                EditorKey: 0
            },
            mounted: function () {
                var self = this;
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
                var colOptions = [];                    
                for (var key in this.columnTypes) {
                    colOptions.push({ Value: key, Text: this.columnTypes[key] });
                }

                this.ColumnTypeOptions = colOptions;

                this.columnKeyNameMap = JSON.parse(config.ColumnKeyNameMap);
            
                //custom validation delegates
                this.Validators = {
                    DeleteColumn: {
                        "*": function (column) {
                            var dependents = $.grep(this.Definition.Columns, function (item) {
                                return item.hasOwnProperty("DependsOn") && item.DependsOn == column.Name;
                            });
                            const reducer = (result, current) => result + current.Name + ", ";
                            if (dependents.length > 0) {
                                var dependentNames = dependents.reduce(reducer, "", 0);
                                throw ("Failed to delete column " + column.Name + ". The following columns are dependent on it: " + dependentNames + ".");
                            }
                        }
                    }
                }

                //event handlers
                app.EventBus.$on("Editor_Add", this.AddColumn)
                app.EventBus.$on("ColumnList_Delete", this.DeleteColumn)
                app.EventBus.$on("ColumnList_Edit", this.EditColumn)
                app.EventBus.$on("Editor_Update", this.UpdateColumn)
                app.EventBus.$on("Editor_ColumnExists", this.ColumnExists);
            },
            computed:{
                IsDefinitionValid: function () {
                    return this.Definition.Columns.length > 0;
                }
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
                        notification.UI("An error occurred loading the column editor.", true);
                        throw err;
                    }
                },
                AddColumn: function (column) {
                    try{
                        var columnWithName = this.FindColumn(column.Name);
                        if (columnWithName != null) {
                            notification.UI("Column with the name " + column.Name + " already exists", true);
                            return;
                        }
                        this.Definition.Columns.push(column);

                        //refresh UI
                        this.RefreshList();
                        this.LoadColumnEditor();

                        notification.UI("Successfully added.", false);
                    } catch (err) {
                        notification.UI("An error occurred adding the column.", true);
                        throw err;
                    }
                },
                DeleteColumn: function (name) {
                    try{
                        var column = this.FindColumn(name);
                        if (column == null) {
                            notification.UI("Column with the name " + name + " does not exist. Please refresh the page.", true);
                        }

                        this.Validators.DeleteColumn["*"].call(this, column);

                        //must use splice for Vue reactivity
                        this.Definition.Columns.splice(this.Definition.Columns.indexOf(column), 1)

                        this.RefreshList();

                        notification.UI("Successfully deleted.", false);
                    } catch (err) {
                        notification.UI(err, true);
                        throw err;
                    }
                },
                EditColumn: function (name) {
                    try{
                        var column = this.FindColumn(name);
                        if (column == null) {
                            notification.UI("There are either multiple columns with the name " + name + " or the column does not exist. Please refresh the page.", true);
                        }

                        this.IsEdit = true;
                        this.ColumnType = column.Type;
                        //load editor
                        this.LoadColumnEditor(column);
                    } catch (err) {
                        notification.UI("Failed to initiate edit.", true);
                        throw err;
                    }
                },
                UpdateColumn: function (column) {
                    try{
                        this.IsEdit = false;
                        this.RefreshList();

                        this.LoadColumnEditor();

                        notification.UI("Successfully updated", false);
                    } catch (err) {
                        notification.UI("An error occurred.", true);
                        throw err;
                    }
                },
                CancelEdit: function () {
                    this.IsEdit = false;
                    this.LoadColumnEditor();
                    this.RefreshList();
                },
                RefreshList: function () {
                    var self = this;
                    var displayColumns = $.map(this.Definition.Columns, function (item) {
                        return { Name: item.Name, Type: self.columnTypes[item.Type], ColumnOrder: item.ColumnOrder };
                    });
                    app.EventBus.$emit("ColumnList_Refresh", displayColumns);
                },
                ColumnExists: function (obj) {
                    var exists = this.FindColumn(obj.ColumnName) != null;
                    obj.Callback(exists);
                },
                CreateFile: function () {
                    try{
                        if (this.IsDefinitionValid == true) {
                            var dto = {};
                            dto.RowCount = this.Definition.RowCount;
                            for (var key in this.columnKeyNameMap) {
                                dto[this.columnKeyNameMap[key]] = $.grep(this.Definition.Columns, function (item) {
                                    return item.Type == key;
                                });
                            }

                            $.post(urls.CSVBuilder_CreateFile, dto, function (resp) {
                                if (resp.Success === true) {
                                    //redirect to download
                                    window.location.href = urls.CSVBuilder_DownloadFile + resp.Data.FileId;
                                    notification.UI("File successfully created", false);
                                } else {
                                    notification.UI(resp.Message, true);
                                }
                            })
                        }
                    } catch (err) {
                        notification.UI("An error occurred.", true);
                        throw err;
                    }
                },
                Reset: function () {
                    this.Definition = new models.CSVDefinition();
                    this.LoadColumnEditor();
                    app.EventBus.$emit("Reset");
                    notification.UI("", false);
                }
            }
        });
       

    });

})(jQuery, Vue, window.App, window.App.Utility, window.App.Models);