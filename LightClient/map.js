function computeTrip() {
    var targetUrl = "http://localhost:8733/WcfServiceLibrary/Biking/rest/getCoordinates?a="
        + document.getElementById("addressStart").value
        + "&b="
        + document.getElementById("addressEnd").value;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    // The header set below limits the elements we are OK to retrieve from the server.
    caller.setRequestHeader("Accept", "application/json");
    // onload shall contain the function that will be called when the call is finished.
    caller.onload = computeTripRetrieved;
    caller.send();
}

function computeTripRetrieved() {
    var str = JSON.parse(this.responseText);
    var response = str.split('@');

    // Let's parse the responses:
    var startWalkCoordinates = JSON.parse(response[0]);
    var bikingCoordinates = JSON.parse(response[1]);
    var endWalkCoordinates = JSON.parse(response[2]);

    //Start walking part
    for (i = 0; i < startWalkCoordinates.coordinates.length-1; i++) {
        // Create an array containing the GPS positions you want to draw
        var coords = [[startWalkCoordinates.coordinates[i][0], startWalkCoordinates.coordinates[i][1]], [startWalkCoordinates.coordinates[i + 1][0], startWalkCoordinates.coordinates[i+1][1]]];
        var lineString = new ol.geom.LineString(coords);

        // Transform to EPSG:3857
        lineString.transform('EPSG:4326', 'EPSG:3857');

        // Create the feature
        var feature = new ol.Feature({
            geometry: lineString,
            name: 'Line'
        });

        // Configure the style of the line
        var lineStyle = new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: '#ffcc33',
                width: 5
            })
        });

        var source = new ol.source.Vector({
            features: [feature]
        });

        var vector = new ol.layer.Vector({
            source: source,
            style: [lineStyle]
        });

        map.addLayer(vector);
    }

    //Biking part
    for (i = 0; i < bikingCoordinates.coordinates.length - 1; i++) {
        // Create an array containing the GPS positions you want to draw
        var coords = [[bikingCoordinates.coordinates[i][0], bikingCoordinates.coordinates[i][1]], [bikingCoordinates.coordinates[i + 1][0], bikingCoordinates.coordinates[i + 1][1]]];
        var lineString = new ol.geom.LineString(coords);

        // Transform to EPSG:3857
        lineString.transform('EPSG:4326', 'EPSG:3857');

        // Create the feature
        var feature = new ol.Feature({
            geometry: lineString,
            name: 'Line'
        });

        // Configure the style of the line
        var lineStyle = new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: '#d93f1c',
                width: 5
            })
        });

        var source = new ol.source.Vector({
            features: [feature]
        });

        var vector = new ol.layer.Vector({
            source: source,
            style: [lineStyle]
        });

        map.addLayer(vector);
    }

    //End walking part
    for (i = 0; i < endWalkCoordinates.coordinates.length - 1; i++) {
        // Create an array containing the GPS positions you want to draw
        var coords = [[endWalkCoordinates.coordinates[i][0], endWalkCoordinates.coordinates[i][1]], [endWalkCoordinates.coordinates[i + 1][0], endWalkCoordinates.coordinates[i + 1][1]]];
        var lineString = new ol.geom.LineString(coords);

        // Transform to EPSG:3857
        lineString.transform('EPSG:4326', 'EPSG:3857');

        // Create the feature
        var feature = new ol.Feature({
            geometry: lineString,
            name: 'Line'
        });

        // Configure the style of the line
        var lineStyle = new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: '#ffcc33',
                width: 5
            })
        });

        var source = new ol.source.Vector({
            features: [feature]
        });

        var vector = new ol.layer.Vector({
            source: source,
            style: [lineStyle]
        });

        map.addLayer(vector);
    }
}

var map = new ol.Map({
    target: 'map', // <-- This is the id of the div in which the map will be built.
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],

    view: new ol.View({
        center: ol.proj.fromLonLat([4.834262, 45.761183]), // <-- Those are the GPS coordinates to center the map to.
        zoom: 13 // You can adjust the default zoom.
    })

});
