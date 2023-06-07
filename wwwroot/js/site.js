var map = L.map('map').setView([56.977884, 24.127176], 7);
var markers = [];

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 18,
}).addTo(map);