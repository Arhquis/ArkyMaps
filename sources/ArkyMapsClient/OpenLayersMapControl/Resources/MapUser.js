// Create a new map user.
function AddMapUser(id, name, displayLonlat) {

    var dataLonLat = transformLonLatDisplayToData(displayLonlat.clone());

    var icon = new OpenLayers.Icon('markers/phone.png');
    var marker = new OpenLayers.Marker(dataLonLat, icon);

    icon.size.w = 32;
    icon.size.h = 32;
    icon.offset.x = -(icon.size.w / 2);
    icon.offset.y = -(icon.size.h / 2);

    marker.id = id;
    marker.name = name;
    marker.isVisible = true;

    m_userLayer.addMarker(marker);

    //MoveMapObject(marker, displayLonlat);

    return marker;
}


// Move the map user to the specififed location.
function MoveMapUser(marker, displayLonLat) {
    var dataLonLat = transformLonLatDisplayToData(displayLonLat.clone());
    var px = m_map.getLayerPxFromViewPortPx(m_map.getPixelFromLonLat(dataLonLat));

    marker.moveTo(px);

    //CenterMapObject(marker);
}

function CenterMapObject(marker) {

    m_map.panTo(marker.lonlat);
}
