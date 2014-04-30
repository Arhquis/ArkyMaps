// Transforms a coordinate from display projection to data projection.
function transformLonLatDisplayToData(displayLonLat) {
    return displayLonLat.transform(m_map.displayProjection, m_map.projection);
}


// Transforms a coordinate from data projection to display projection.
function transformLonLatDataToDisplay(dataLonLat) {
    return dataLonLat.transform(m_map.projection, m_map.displayProjection);
}


// Creates an OpenLayers LonLat object from longitude and latitude components.
function GetOpenLayersLonLat(longitude, latitude) {
    return new OpenLayers.LonLat(longitude, latitude);
}
