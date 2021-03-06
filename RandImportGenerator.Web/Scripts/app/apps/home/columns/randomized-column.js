﻿//Randomized Column component

(function ($, Vue, _, app, util, models) {
    var name = "RandomizedColumnComponent";
    util.CheckDependencies(name, arguments);

    Vue.component('RandomizedColumn', {
        props: [
            'definition',
            'type'
        ],
        data: function () {
            return {
                Definition: this.$props.definition || new models.RandomizedColumn(this.$props.type),
                IsEdit: this.$props.definition != null,
                OptionsTemp: this.$props.definition != null ? this.$props.definition.RandomizationOptions.join("\n") : null
            };
        },
        created: function(){
            this.DebouncedGetOptionsTemp = _.debounce(function () {
                if (this.OptionsTemp != "") {
                    var options = this.OptionsTemp.trim().split(/\s+/);
                    this.Definition.RandomizationOptions = options;
                }
            }, 500);
        },        
        watch:{
            OptionsTemp: function (val) {
                this.DebouncedGetOptionsTemp();                
            }
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
                            <input type="text" v-model="Definition.Name" v-bind:disabled="IsEdit == true" class ="form-control" required />
                        </div>
                        <div class ="col-sm-6">
                            <label>Column Order</label>
                            <input type="number" v-model="Definition.ColumnOrder" class ="form-control" required min="0" />
                        </div>
                    </div>
                    <hr class ="thin"/>
                    <div class ="row">
                        <div class ="col-sm-6">
                            <label>Randomization Options (1 option per line)</label>
                            <textarea v-model="OptionsTemp" class ="form-control height-100" placeholder="Option 1 \nOption 2" required></textarea>
                        </div>
                    </div>
                    <hr class ="thin"/>
                    <div class ="row">
                        <div class ="col-sm-12">
                            <submit-button v-bind:isEdit="IsEdit" />
                        </div>
                    </div>
                </form>
            `
    })

})(jQuery, Vue, _, window.App, window.App.Utility, window.App.Models);