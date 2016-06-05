var RestaurantsResultVM = function() {

    var self = this;
    self.Restaurants = ko.observableArray([]);
    self.searchInput = ko.observable($('#outcode-input').val());
    self.hasResults = ko.computed(function () {

        return self.searchInput().length > 0 && self.Restaurants().length > 0;
    });

    self.searchRestaurantsByOutpost = function () {

        if (self.searchInput().length > 0) {

            $.get(
                "/api/Restaurants/" + self.searchInput(),
                null,
                function (data) {

                    self.Restaurants(data.Restaurants);
                });
        }
    }
}