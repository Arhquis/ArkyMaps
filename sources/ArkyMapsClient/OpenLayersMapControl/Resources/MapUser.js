// Create a new map user.
function AddMapUser(id, name, displayLonLat) {

    var dataLonLat = transformLonLatDisplayToData(displayLonLat.clone());

    var icon = new OpenLayers.Icon('markers/phone.png');
    var marker = new OpenLayers.Marker(dataLonLat, icon);

    icon.size.w = 32;
    icon.size.h = 32;
    icon.offset.x = -(icon.size.w / 2);
    icon.offset.y = -(icon.size.h / 2);

    marker.id = id;
    marker.name = name;
    marker.isVisible = true;
    marker.displayLonLat = displayLonLat.clone();

    marker.nameTable = CreateNameTable(dataLonLat, name, marker);

    m_userLayer.addMarker(marker);
    return marker;
}


function CreateNameTable(lonlat, name, parentMarker) {
    var nameTable = new OpenLayers.Popup(null, lonlat, null, name, false);

    nameTable.autoSize = true;
    nameTable.panMapIfOutOfView = false;
    nameTable.keepInMap = false;

    m_map.addPopup(nameTable);

    var newPx = m_map.getLayerPxFromLonLat(lonlat);

    newPx.x -= nameTable.size.w / 2;
    newPx.y += parentMarker.icon.size.h / 2;

    nameTable.moveTo(newPx);

    return nameTable;
}


// Move the map user to the specififed location.
function MoveMapUser(marker, displayLonLat) {
    marker.displayLonLat = displayLonLat;

    var dataLonLat = transformLonLatDisplayToData(displayLonLat.clone());

    var px = m_map.getLayerPxFromViewPortPx(m_map.getPixelFromLonLat(dataLonLat));

    marker.moveTo(px);

    var newPx = px.clone();

    newPx.x -= marker.nameTable.size.w / 2;
    newPx.y += marker.icon.size.h / 2;

    marker.nameTable.moveTo(newPx);
}

function CenterMapObject(marker) {

    m_map.panTo(marker.lonlat);
}
