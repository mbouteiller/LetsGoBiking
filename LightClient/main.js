var stations;

function retrieveAllContracts() {
    var targetUrl = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + document.getElementById("apiKey").value;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    // The header set below limits the elements we are OK to retrieve from the server.
    caller.setRequestHeader("Accept", "application/json");
    // onload shall contain the function that will be called when the call is finished.
    caller.onload = contractsRetrieved;

    caller.send();
}

function contractsRetrieved() {
    // Let's parse the response:
    var response = JSON.parse(this.responseText);

    response.forEach(contract => {
        var div = document.createElement("OPTION");
        div.setAttribute("value", contract.name);
        document.getElementById("data").appendChild(div);
    })
}

function retrieveContractStations() {
    var targetUrl = "https://api.jcdecaux.com/vls/v1/stations?contract=" + document.getElementById("contract").value + "&apiKey=" + document.getElementById("apiKey").value;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    caller.setRequestHeader("Accept", "application/json");
    caller.onload = stationsRetrieved;

    caller.send();
}

function stationsRetrieved() {

    stations = JSON.parse(this.responseText);

    console.log(stations);

}

function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2 - lat1);
    var dLon = deg2rad(lon2 - lon1);
    var a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2)
        ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = earthRadius * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI / 180)
}

function getClosestStation() {
    var latitude = document.getElementById("latitude").value;
    var longitude = document.getElementById("longitude").value;

    var nearest = stations[0].name;
    var bestDistance = getDistanceFrom2GpsCoordinates(latitude, longitude, stations[0].position.lat, stations[0].position.lng);

    stations.forEach(station => {
        var currentDistance = getDistanceFrom2GpsCoordinates(latitude, longitude, station.position.lat, station.position.lng);
        if (currentDistance < bestDistance) {
            bestDistance = currentDistance;
            nearest = station.name;
        }
    })

    console.log(nearest);
}

var coordinatesStartWalk;

function retrieveStartWalkCoordinates() {
    var targetUrl = "http://localhost:8733/WcfServiceLibrary/Biking/rest/startwalk?a="
        + document.getElementById("longitudeStart").value
        + "&b="
        + document.getElementById("latitudeStart").value
        + "&c="
        + document.getElementById("longitudeEnd").value
        + "&d="
        + document.getElementById("latitudeEnd").value;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    // The header set below limits the elements we are OK to retrieve from the server.
    caller.setRequestHeader("Accept", "application/json");
    // onload shall contain the function that will be called when the call is finished.
    caller.onload = startWalkCoordinatesRetrieved;
    caller.send();
}

function startWalkCoordinatesRetrieved() {
    // Let's parse the response:
    var response = JSON.parse(this.responseText);
    var test = JSON.parse(response);

    console.log(test);

    console.log(test.coordinates[0][0]);

    document.getElementById("workingTest").innerHTML = this.responseText;

}