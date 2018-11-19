
(function (models) {
    models.AutoIncrementedColumn = function (name, order, startingSequenceNumber, incrementValue) {
        this.Name = name;
        this.Order= order;
        this.StartingSequenceNumber = startingSequenceNumber;
        this.IncrementValue = incrementValue;
    };

})(window.App.Models);