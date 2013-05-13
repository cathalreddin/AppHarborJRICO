<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="JRICO.Content.map" %>

<!DOCTYPE html>
<html>
<body onload="getLocation()">
<a href="javascript: window.close()">Close Window</a>
<div id="mapholder" align="center"></div>
<script src="http://maps.google.com/maps/api/js?sensor=false"></script>
<script>

    var qsParm = new Array();
    function getLocation() {
        var query = window.location.search.substring(1);
        var parms = query.split('&');
        for (var i=0; i<parms.length; i++) 
        {
            var pos = parms[i].indexOf('=');
            if (pos > 0) 
            {
                var key = parms[i].substring(0,pos);
                var val = parms[i].substring(pos+1);
                qsParm[key] = val;
            }
        }
        window.resizeTo(800, 500);
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition, showError);
        }
        else { x.innerHTML = "Geolocation is not supported by this browser."; }
    }

    function showPosition(position) {
        lat = qsParm["lat"];
        lon = qsParm["lon"];
        if (lat != "0.00000000" && lon != "0.00000000") {
            latlon = new google.maps.LatLng(lat, lon)
            mapholder = document.getElementById('mapholder')
            mapholder.style.height = '300px';
            mapholder.style.width = '700px';

            var myOptions = {
                center: latlon, zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,
                navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL }
            };
            var map = new google.maps.Map(document.getElementById("mapholder"), myOptions);
            var marker = new google.maps.Marker({ position: latlon, map: map, title: "location is here!" });
        }
        else {
            alert("Co-ordinates not mapped!");
        }
    }

    function showError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                x.innerHTML = "User denied the request for Geolocation."
                break;
            case error.POSITION_UNAVAILABLE:
                x.innerHTML = "Location information is unavailable."
                break;
            case error.TIMEOUT:
                x.innerHTML = "The request to get user location timed out."
                break;
            case error.UNKNOWN_ERROR:
                x.innerHTML = "An unknown error occurred."
                break;
        }
    }
</script>
</body>
</html>

