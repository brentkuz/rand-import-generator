
(function (models) {
    models.AutoIncrementedColumn = function (type, name, order, startingSequenceNumber, incrementValue) {
        this.Type = type;
        this.Name = name;
        this.Order= order;
        this.StartingSequenceNumber = startingSequenceNumber;
        this.IncrementValue = incrementValue;
    };
})(window.App.Models);