//Dependent Column component

(function ($, Vue, _, app, util, models) {
    var name = "DependentColumnComponent";
    util.CheckDependencies(name, arguments);

    Vue.component('DependentColumn', {
        props: [
            'definition',
            'type'
        ],
        data: function () {
            return {
                Definition: this.$props.definition || new models.DependentColumn(this.$props.type),
                IsEdit: this.$props.definition != null,
                MapTemp: this.$props.definition != null ? JSON.stringify(this.$props.definition.Map, null, 2) : null,
                DependsOnTemp: this.$props.definition != null ? this.$props.definition.DependsOn : null
            };
        },
        created: function () {
            this.DebouncedGetMapTemp = _.debounce(function () {
                if (this.MapTemp != "") {
                    var el = this.$refs.Map;
                    try{
                        this.Definition.Map = JSON.parse(this.MapTemp);
                        el.setCustomValidity("");
                    } catch (err) {
                        el.setCustomValidity("Invalid JSON.");
                    }
                }
            }, 500);
            this.DebouncedGetDependsOn = _.debounce(function () {
                if (this.DependsOnTemp != "") {
                    var self = this;
                    var el = self.$refs.DependsOn;
                    app.EventBus.$emit("Editor_ColumnExists", {
                        ColumnName: self.DependsOnTemp,
                        Callback: function (exists) {
                            if (exists) {
                                self.Definition.DependsOn = self.DependsOnTemp;
                                el.setCustomValidity("");
                            } else {
                                el.setCustomValidity("Column does not exist.");
                            }
                        }
                    })
                }
            }, 500);
        },
        watch: {
            MapTemp: function (val) {
                this.DebouncedGetMapTemp();
            },
            DependsOnTemp: function(){
                this.DebouncedGetDependsOn();
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
                            <input type="number" v-model="Definition.ColumnOrder" class ="form-control inline" required min="0" />
                        </div>
                    </div>
                    <hr class ="thin"/>
                    <div class ="row">
                        <div class ="col-sm-6">
                            <label>Map <br /> (properly formed JSON)</label>
                            <textarea v-model="MapTemp" ref="Map" class ="form-control inline height-100" placeholder='{\n"InputValue1": "OutputValue1"\n"InputValue2": "OutputValue2"\n}' required></textarea>
                        </div>
                        <div class ="col-sm-6">
                            <label>Depends On (column name)</label>
                            <input type="text" ref="DependsOn" v-model="DependsOnTemp" class ="form-control inline" required />
                        </div>
                    </div>
                    <hr class ="thin"/>
                    <div class ="row">
                        <div class ="col-sm-12">
                            <button type="submit" class ="btn btn-primary">Save</button>
                        </div>
                    </div>
                </form>
            `
    })

})(jQuery, Vue, _, window.App, window.App.Utility, window.App.Models);