var DISPLAY_PROJECTION = 'EPSG:4326';
var DATA_PROJECTION = 'EPSG:3857';
var MAP_DIV_ID = 'map';

var m_map;
var m_userLayer;

/********************************************************************************
* Lifetime.
********************************************************************************/
function LoadMap() {
    m_map = new OpenLayers.Map(MAP_DIV_ID, {
        projection: DATA_PROJECTION,
        displayProjection: DISPLAY_PROJECTION,
        layers: [
            new OpenLayers.Layer.Google(
                "Google Streets", // the default
                { numZoomLevels: 20 }
            )
        ],
        center: new OpenLayers.LonLat(20.1480674743652, 46.2530722893968)
            .transform(DISPLAY_PROJECTION, DATA_PROJECTION),
        zoom: 16
    });

    m_userLayer = new OpenLayers.Layer.Markers("UserLayer");
    m_map.addLayer(m_userLayer);

    window.external.MapLoadedCallback();
}
