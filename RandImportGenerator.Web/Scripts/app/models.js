
(function (models) {
    function ColumnBase(type, name, order) {
        this.Type = type;
        this.Name = name;
        this.Order = order;
    }

    models.AutoIncrementedColumn = function (type, name, order, startingSequenceNumber, incrementValue) {
        ColumnBase.call(this, type, name, order);
        this.StartingSequenceNumber = startingSequenceNumber;
        this.IncrementValue = incrementValue;
    };

    models.StaticColumn = function (type, name, order, value) {
        ColumnBase.call(this, type, name, order);
        this.Value = value;
    };

    models.RandomizedColumn = function (type, name, order, options) {
        ColumnBase.call(this, type, name, order);
        this.RandomizationOptions = options || [];
    };
})(window.App.Models);