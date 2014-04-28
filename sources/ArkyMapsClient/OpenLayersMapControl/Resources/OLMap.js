var m_map;

/********************************************************************************
* Lifetime.
********************************************************************************/
function Load() {
    m_map = new OpenLayers.Map('map', {
        projection: 'EPSG:3857',
        layers: [
            new OpenLayers.Layer.Google(
                "Google Streets", // the default
                { numZoomLevels: 20 }
            )
        ],
        center: new OpenLayers.LonLat(10.2, 48.9)
            // Google.v3 uses web mercator as projection, so we have to
            // transform our coordinates
            .transform('EPSG:4326', 'EPSG:3857'),
        zoom: 5
    });

    window.external.MapLoadedCallback();
}
