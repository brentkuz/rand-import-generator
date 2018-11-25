// Column List component

(function ($, Vue, app, util, initializers) {
    var name = "ColumnListComponent";
    util.CheckDependencies(name, arguments);
 
    var blocker = util.Blocker("#columnListApp");
   
    Vue.component("column-list", {
        data: function () {
            return {
                Columns: [],
                IsEdit: false
            };
        },
        mounted: function () {
            //event handlers
            app.EventBus.$on("ColumnList_Refresh", this.Refresh);
            app.EventBus.$on("Reset", this.Refresh);
        },
        methods: {
            Refresh: function (columns) {
                if (columns == null) {
                    this.Columns = [];
                } else {
                    function SortPredicate(a, b) {
                        var aOrder = a.ColumnOrder;
                        var bOrder = b.ColumnOrder;
                        return (aOrder < bOrder) ? -1 : ((aOrder > bOrder) ? 1 : 0);
                    }

                    this.Columns = [];
                    for (var i = 0; i < columns.length; i++) {
                        this.Columns.push(columns[i]);
                    }

                    this.Columns = this.Columns.sort(SortPredicate);
                }
                this.IsEdit = false;
                blocker.Unblock();
            },
            Delete: function (name) {
                if (confirm("Would you like to delete column " + name + "?")) {
                    app.EventBus.$emit("ColumnList_Delete", name);
                }
            },
            Edit: function (name) {
                this.IsEdit = true;
                blocker.Block();
                app.EventBus.$emit("ColumnList_Edit", name);
            }
        },
        template: `
            <div id="columnListApp" class="app-container radius">
            <div class="row table-header">
                <div class="col-sm-3">
                    <label>Name</label>
                </div>
                <div class="col-sm-3">
                    <label>Type</label>
                </div>
                <div class="col-sm-3">
                    <label>Order</label>
                </div>
                <div class="col-sm-3">
                   Count: {{Columns.length}}
                </div>
            </div>
            <div class="row" v-for="col in Columns" >
                <div class="col-sm-3">
                    {{col.Name}}
                </div>
                <div class="col-sm-3">
                    {{col.Type}}
                </div>
                <div class="col-sm-3">
                    {{col.ColumnOrder}}
                </div>
                <div class="col-sm-3">
                    <button class="btn btn-default" v-on:click="Edit(col.Name)" v-bind:disabled="IsEdit == true">Edit</button>
                    <button class="btn btn-danger" v-on:click="Delete(col.Name)" v-bind:disabled="IsEdit == true">Delete</button>
                </div>
            </div>
        </div>`
    });

})(jQuery, Vue, window.App, window.App.Utility, window.App.Initializers);
