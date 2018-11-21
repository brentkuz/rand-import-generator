﻿// Auto-Incremented Column component

(function ($, Vue, app, util, models) {
    var name = "AutoIncrementedColumnComponent";
    util.CheckDependencies(name, arguments);

    Vue.component('AutoIncrementedColumn', {
        props: [
            'definition',
            'type'
        ],
        data: function(){
            return {
                Definition: this.$props.definition || new models.AutoIncrementedColumn(this.$props.type),
                IsEdit: this.$props.definition != null
            };
        },
        methods: {
            Submit: function () {
                if (!this.IsEdit) {
                    app.EventBus.$emit("Editor_Add", this.Definition);
                } else {
                    app.EventBus.$emit("Editor_Update", this.Definition);
                }
            },
            CancelEdit: function () {
                $.Topic("CancelEdit").Publish();
            }
        },
        template: `
                <form v-on:submit.prevent="Submit">
                    <div class ="row">
                        <div class ="col-sm-6">
                            <label>Name</label>
                            <input type="text" v-model="Definition.Name" v-bind:disabled="IsEdit == true" class ="form-control inline" required />
                        </div>
                        <div class ="col-sm-6">
                            <label>Order</label>
                            <input type="number" v-model="Definition.Order" class ="form-control inline" required min="0" />
                        </div>
                    </div>
                    <hr class ="thin"/>
                    <div class ="row">
                        <div class ="col-sm-6">
                            <label>Starting Sequence #</label>
                            <input type="number" v-model="Definition.StartingSequenceNumber" class ="form-control inline" required min="0"/>
                        </div>
                        <div class ="col-sm-6">
                            <label>Increment Value</label>
                            <input type="number" v-model="Definition.IncrementValue" class ="form-control inline" required min="0"/>
                        </div>
                    </div>
                    <div class ="row">
                        <div class ="col-sm-12">
                            <button type="submit" class ="btn btn-primary">Save</button>
                        </div>
                    </div>
                </form>
            `
    })

})(jQuery, Vue, window.App, window.App.Utility, window.App.Models);