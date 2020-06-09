
    var map, infoWindow, marker, address, autocomplete, geocoder;
    var pos = {
        lat: 27.7172,
        lng: 85.3240
    };
    var options = {
        types: [],
        componentRestrictions: { country: 'pe' }
    };


    function GetLatLong() {
        var DirLatLong = {
            AddressName: document.getElementById('address').value,
            Latitude: pos.lat,
            Longitude: pos.lng
        }
        //var myJSON = JSON.stringify(DirLatLong)
        return DirLatLong
    }

    function initMap() {
        geocoder = new google.maps.Geocoder;
        map = new google.maps.Map(
        document.getElementById('map'), {
        zoom: 12,
        center: pos
        });
        detectLocation();
        autocomplete = new google.maps.places.Autocomplete(document.getElementById('address'),options);
        places = new google.maps.places.PlacesService(map);
        autocomplete.addListener('place_changed', onPlaceChanged);
    }


    function detectLocation() {
        infoWindow = new google.maps.InfoWindow;
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                infoWindow.setPosition(pos);
                document.getElementById('latitude').value = pos.lat;
                document.getElementById('longitude').value = pos.lng;
                marker = new google.maps.Marker({
                    position: pos,
                    map: map,
                    draggable: true
                });
                marker.addListener('dragend', handleEvent);
                map.setCenter(pos);
            }, function () {
                handleLocationError(true, infoWindow, map.getCenter());
            });
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    }

    function handleLocationError(browserHasGeolocation, infoWindow, pos) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(browserHasGeolocation ?
            'Error: The Geolocation service failed.' :
            'Error: Your browser doesn\'t support geolocation.');
        infoWindow.open(map);
    }

    function handleEvent(event) {
        pos = {
            lat: event.latLng.lat(),
            lng: event.latLng.lng()
        }
        document.getElementById('latitude').value = event.latLng.lat();
        document.getElementById('longitude').value = event.latLng.lng();
        geocodeLatLng(event.latLng.lat(), event.latLng.lng());
    }

    function onPlaceChanged() {
        var place = autocomplete.getPlace();
        if (place.geometry) {
            map.panTo(place.geometry.location);
            let latlng = new google.maps.LatLng(map.getCenter().lat(), map.getCenter().lng());
            marker.setPosition(latlng);
            pos = {
                lat: map.getCenter().lat(),
                lng: map.getCenter().lng()
            }
            document.getElementById('latitude').value = map.getCenter().lat();
            document.getElementById('longitude').value = map.getCenter().lng();
            map.setZoom(14);
        } else {
            document.getElementById('autocomplete').placeholder = 'Enter a city';
        }
    }

    function geocodeLatLng(lat, lng) {
        var latlng = { lat: lat, lng: lng };
        geocoder.geocode({ 'location': latlng }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    document.getElementById('address').value = results[0].formatted_address;
                } else {
                    window.alert('Error parsing Address');
                }
            } else {
                console.log('Geocoder failed due to: ' + status);
            }
        });
    }













