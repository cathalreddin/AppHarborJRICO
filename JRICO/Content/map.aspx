<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="JRICO.Content.map" %>


<!DOCTYPE html>
<html>
<head>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body onload="getLocation()">
<form id="Form1" runat="server">

<table>
<tr>
<td></td>
<td valign="top"><br />
    <b>Hospital Details</b><br />
    <asp:Label ID="lblHospitalName" runat="server"></asp:Label><br />
    <asp:Label ID="lblAddress1" runat="server"></asp:Label><br />
    <asp:Label ID="lblPostcode" runat="server"></asp:Label><br /><br />
    Account Number: <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
</td>
<td><div id="mapholder" align="center"></div></td>
</tr>
<tr>
<td colspan="3" align="right">
    <asp:TextBox ID="txtlat" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtlon" runat="server"></asp:TextBox>
    <a href="javascript: window.close()">Close Window</a></td></tr>
</table>

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
        id = qsParm["id"];
        latlon = new google.maps.LatLng(document.getElementById("txtlat").value, document.getElementById("txtlon").value)
        document.getElementById("txtlat").style.visibility = 'hidden';
        document.getElementById("txtlon").style.visibility = 'hidden';
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
</form>
</body>
</html>

