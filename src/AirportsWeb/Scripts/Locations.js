let CountriesViewModel = function (data) {
    let self = this;

    self.isoCode = data.IsoCode;
    self.name = data.Name;
    self.region = data.Region;

    return self;
}

let AirportsViewModel = function (data, id) {
    let self = this;

    self.id = id;
    self.iata = data.iata;
    self.lon = data.lon;
    self.iso = data.iso;
    self.status = data.status;
    self.name = data.name;
    self.continent = data.continent;
    self.type = data.type;
    self.lat = data.lat;
    self.size = data.size;

    return self;
}

function MainViewModel() {
    let self = this;

    self.allAirports = ko.observableArray();
    self.displayAirports = ko.observableArray();
    self.pageIndex = ko.observable(0);

    self.countries = ko.observableArray();
    self.filterCountry = ko.observable("");
    self.sourceAirport = ko.observable("");
    self.destinationAirport = ko.observable("");

    self.distance = ko.observable("");
    self.milesDifferential = 0.00062137119223733;
    self.recordCount = 5;

    self.filteredData = ko.computed(function () {
        self.pageIndex(0);
        let countryFilter = self.filterCountry();
        let data = self.allAirports();

        if (countryFilter) {
            data = ko.utils.arrayFilter(data, function (location) {
                return location.iso === countryFilter;
            });
        }

        return data;
    });


    self.totalPages = ko.computed(function () {
        let page = Math.ceil(self.filteredData().length / self.recordCount);
        page += self.filteredData().length % self.recordCount > 0 ? 1 : 0;
        return page - 1;
    });

    self.getCountries = function () {
        $.ajax({
            url: "/Locations/Countries",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            error: function (a, b, c, d) {
                console.log(a);
                console.log(b);
                console.log(c);
                console.log(d);
            },
            success: function (result) {
                result.forEach((data, index) => {
                    self.countries.push(new CountriesViewModel(data));
                });
            }
        });
    }

    self.getAirports = function () {
        $.ajax({
            url: "/Locations/Airports",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            error: function (a, b, c, d) {
                console.log(a);
                console.log(b);
                console.log(c);
                console.log(d);
            },
            success: function (result) {
                result.forEach((data, index) => {
                    self.allAirports.push(new AirportsViewModel(data, index));
                });
            }
        });
    }

    self.getDistance = function(coordinates) {
        $.ajax({
            type: "POST",
            url: "/Locations/GetDistance",
            data: JSON.stringify(coordinates),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            error: function (a, b, c, d) {
                console.log(a);
                console.log(b);
                console.log(c);
                console.log(d);
            },
            success: function (result) {
                self.distance((Math.ceil(result * self.milesDifferential)) + "-miles");
            }
        });
    }

    self.paginated = ko.computed(function () {
        let data = self.filteredData();
        let first = self.pageIndex() * self.recordCount;
        data = data.slice(first, first + self.recordCount);

        return data;
    });

    self.hasPrevious = ko.computed(function () {
        return self.pageIndex() > 0;
    });

    self.hasNext = ko.computed(function () {
        return self.pageIndex() < (self.totalPages() - 1);
    });

    self.next = function () {
        if (self.pageIndex() < self.totalPages()) {
            self.pageIndex(self.pageIndex() + 1);
        }
    }

    self.previous = function () {
        if (self.pageIndex() != 0) {
            self.pageIndex(self.pageIndex() - 1);
        }
    }

    self.calculateDistance = function () {
        let distanceInput = { SourceAirport: self.sourceAirport(), DestinationAirport: self.destinationAirport() };

        self.getDistance(distanceInput);
    }

    self.getCountries();
    self.getAirports();
}

function initViewModel() {
    let vm = new MainViewModel();
    let container = $("#airportsContainer").get(0);
    ko.applyBindings(vm, container);
}

$(document).ready(function () {
    initViewModel();
});