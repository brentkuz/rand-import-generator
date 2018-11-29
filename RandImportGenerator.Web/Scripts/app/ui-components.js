// Shared UI Components

(function ($, Vue) {
    Vue.component("submit-button", {
        props: [
            "isEdit"
        ],
        data: function () {
            return {
                ButtonText: null
            };
        },
        watch: {
            isEdit: {
                immediate: true,
                handler: function (newVal) {
                    this.ButtonText = this.SelectText(newVal);
                }
            }
        },
        methods: {
            SelectText: function (isEdit) {
                return isEdit === true ? "Update" : "Add"
            }
        },
        template: `
            <button type="submit" class ="btn btn-primary">{{ButtonText}}</button>
            `
    });

})(jQuery, Vue)