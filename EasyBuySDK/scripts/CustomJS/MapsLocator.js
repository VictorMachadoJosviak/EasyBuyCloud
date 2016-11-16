
var map = null;
//Essa e a funcao que criara o mapa GoogleMaps
function chamaMapa() {
    //aqui vamos definir as coordenadas de latitude e longitude no qual
    //sera exibido o nosso mapa
    var latlng = new google.maps.LatLng(-23.64340873969638, -46.730219057147224); //DEFINE A LOCALIZAÇÃO EXATA DO MAPA
    //aqui vamos configurar o mapa, como o zoom, o centro do mapa, etc
    var myOptions = {
        zoom: 5,//utilizaremos o zoom 15
      //  center: latlng,//aqui a nossa variavel constando latitude e
        //longitude ja declarada acima
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    //criando o mapa dentro da div com o id="map_canvas" que ja criamos
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
    //DEFINE AS COORDENADAS do ponto exato - CENTRALIZAÇÃO DO MAPA
    map.setCenter(new google.maps.LatLng(-20.7233106, -46.6146444));

}
function abreLink() {
    window.open('http://www.google.com.br');
}

function chamaMarcacaoEndereco(localizacao) {
    //colocando o endereco no padrao correto
    var endereco = localizacao;

    //MUDANDO O ZOOM DO MAPA
    map.setZoom(17);
    //Buscando lat e log por endereco (no formato: nome rua, numero, bairro - cidade)
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': endereco }, function (results, status) {
        //se o retorno de status for ok
        if (status = google.maps.GeocoderStatus.OK) {
            //pega o retorno de result, que sao a latitude e longitude
            var latlng = results[0].geometry.location;
            //faz marcacao no mapa
            var marker = new google.maps.Marker({ position: latlng, map: map });
            map.setCenter(latlng);//leva o mapa para a posicao da marcacao
        }
    });
}