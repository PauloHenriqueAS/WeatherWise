const locations = [
  {
      address: "Avenida Antônio Thomaz Ferreira de Rezende, 1766 - Bairro Marta Helena",
      displayName: "Rural Brasil",
      region: "Norte",
      lat: -18.87491506886587,
      lon: -48.27947888151518
      // https://diariodeuberlandia.com.br/noticia/29612/apos-estragos-neste-domingo-17--uberlandia-segue-em-alerta-vermelho-por-conta-da-chuva
  },
  {
      address: "Av. Felipe Calixto Milken, 960 - Bairro Morumbi",
      displayName: "Próximo ao UAI Morumbi",
      region: "Leste",
      lat: -18.913570338202373,
      lon: -48.19142804673244
      // https://g1.globo.com/minas-gerais/triangulo-mineiro/noticia/2012/10/chuva-de-10-minutos-alaga-ruas-no-bairro-morumbi-em-uberlandia-mg.html
  },
  {
      address: "Rua Idalina Vieira Borges, 54 - Bairro Taiaman",
      displayName: "Próximo ao Bosque Municipal do Guanandi",
      region: "Oeste",
      lat: -18.901398174591435,
      lon: -48.32454296305783
      // https://g1.globo.com/mg/triangulo-mineiro/noticia/2023/03/15/casas-ficam-destruidas-apos-temporal-em-uberlandia.ghtml
  },
  {
      address: "Avenida Rondon Pacheco, 350 - Bairro Copacabana",
      displayName: "Próximo ao Praia Clube",
      region: "Sul",
      lat: -18.931304406958176,
      lon: -48.290165987724464
      // https://g1.globo.com/mg/triangulo-mineiro/noticia/2022/01/16/forte-chuva-atinge-uberlandia-e-causa-alagamento-em-diversas-ruas-da-cidade.ghtml
  },
  {
      address: "Avenida Professora Minervina Cândida Oliveira - Bairro Martins",
      displayName: "Próximo à Rodoviária",
      region: "Centro",
      lat: -18.904887204577225,
      lon: -48.286895702971265
      // https://diariodeuberlandia.com.br/noticia/29756/apos-quase-20-dias-estragos-causados-pela-chuva-na-avenida-minervina-candida-oliveira-seguem-sem-reparos-em-uberlandia
  }
];

document.addEventListener("DOMContentLoaded", function () {
  setWeatherScreenData();
  getAirPollution();
  generateChartWindSpeed();
  generateChartPollution();

  // destruir DT quando modal sumir, para poder criar outras depois
  $('#myModal').on('hidden.bs.modal', function (e) {
    if ($.fn.dataTable.isDataTable('#regionTable')) {
      table = $('#regionTable').DataTable();
      table.destroy();
    }
  });
});

function setWeatherScreenData() {
  getCurrentWeather()
    .then((result) => {
      if (result != null) {
        $('#windSpeed').text(`${result.wind.speed} Km/h`);
        $('#visibility').text(`${result.visibility} m`);
      }
    });
}

function getCurrentWeather(displayName = "Uberlandia", lat = -18.909216, lon = -48.2622005) {
  const url = `${baseUrl}/Weather/GetCurrentWeather`;
  const body = {
    "displayName": displayName,
    "lat": lat,
    "lon": lon
  };

  return new Promise((resolve, reject) => {
    fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    })
      .then((response) => response.json())
      .then((result) => {
        if (result.success) {
          resolve(result.data);
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Erro buscar tempo real!',
            text: result.message
          }).then(() => {
            resolve(null);
          })
        }
      })
      .catch((error) => {
        reject(error);
      });
  });
}

function getAirPollution(displayName = "Uberlandia", lat = -18.909216, lon = -48.2622005) {
  const url = `${baseUrl}/AirPollution/GetAirPollution`;

  const body = {
    "displayName": displayName,
    "lat": lat,
    "lon": lon
  }

  fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body),
  })
    .then((response) => response.json())
    .then((result) => {
      if (result.success) {
        // $('#airPollutionDescription').text(result.data.air_pollution_description);
        let aqi = result.data.list[0].main.aqi ?? 0;

        const cardN1 = document.getElementById('cardN1');
        const cardN2 = document.getElementById('cardN2');
        const cardN3 = document.getElementById('cardN3');
        const cardN4 = document.getElementById('cardN4');
        const cardN5 = document.getElementById('cardN5');

        cardN1.classList.add('disabled-card');
        cardN2.classList.add('disabled-card');
        cardN3.classList.add('disabled-card');
        cardN4.classList.add('disabled-card');
        cardN5.classList.add('disabled-card');

        const cards = [cardN1, cardN2, cardN3, cardN4, cardN5];

        if (aqi >= 1 && aqi <= 5) {
          cards[aqi - 1].classList.remove('disabled-card');
        }
      } else {
        Swal.fire({
          icon: 'error',
          title: 'Erro ao buscar poluição do ar!',
          text: result.message
        });
      }
    });
}

function getDataDashBoardWindSpeed() {
  const url = `${baseUrl}/Weather/GetWindDashboardInformation`;

  return new Promise((resolve, reject) => {
    fetch(url)
      .then((response) => response.json())
      .then((result) => {
        if (result.success) {
          resolve(result.data);
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Erro buscar informações da velocidade do vento!',
            text: result.message
          }).then(() => {
            resolve(null);
          })
        }
      })
      .catch((error) => {
        reject(error);
      });
  });
}

function getDataSets(windSpeedInfo){

  let dataSets = []
  windSpeedInfo.forEach(element => {
    dataSets.push({
      label: element.label,
      data: element.data,
      fill: false,
      borderColor: element.borderColor,
      backgroundColor: element.backgroundColor,
      borderWidth: 2
    });
  });
  return dataSets;
}

function generateChartWindSpeed() {
  var ctx = document.getElementById("windChart").getContext('2d');
  getDataDashBoardWindSpeed()
  .then((result) => {
    if (result != null) {
      var myChart = new Chart(ctx, {
        type: 'line',
        data: {
          labels: result[0].generalLabels,
          datasets: getDataSets(result)
        },
        options: {
          responsive: true, // Instruct chart js to respond nicely.
          maintainAspectRatio: false, // Add to prevent default behaviour of full-width/height 
        }
      });
    }
  });
  
}

function generateGenericChart() {
  var ctx = document.getElementById("windChart").getContext('2d');

  var myChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: ["12:00", "13:00", "14:00", "15:00", "16:00", "17:00"],
      datasets: [{
        label: 'Sul',
        data: [2.2, 3.1, 4.2, 1.7, 3.7, 5.5],
        fill: false,
        borderColor: '#8856a7',
        backgroundColor: '#8856a7',
        borderWidth: 2
      },
      {
        label: 'Norte',
        data: [7.2, 3.1, 2.2, 5.7, 1.7, 3.5],
        fill: false,
        borderColor: '#9ebcda',
        backgroundColor: '#9ebcda',
        borderWidth: 2
      },
      {
        label: 'Oeste',
        data: [1.2, 4.1, 4.2, 5.7, 6.7, 7.5],
        fill: false,
        borderColor: '#99d8c9',
        backgroundColor: '#99d8c9',
        borderWidth: 2
      },
      {
        label: 'Centro',
        data: [3.2, 4.1, 2.2, 7.7, 5.7, 6.5],
        fill: false,
        borderColor: '#a8ddb5',
        backgroundColor: '#a8ddb5',
        borderWidth: 2
      },
      {
        label: 'Leste',
        data: [4.2, 4.1, 4.2, 2.7, 6.7, 10.5],
        fill: false,
        borderColor: '#fdbb84',
        backgroundColor: '#fdbb84',
        borderWidth: 2
      }]
    },
    options: {
      responsive: true, // Instruct chart js to respond nicely.
      maintainAspectRatio: false, // Add to prevent default behaviour of full-width/height 
    }
  });
}

var valores = [];
function generateChartPollution() {
  var ctx = document.getElementById("pollutionChart").getContext('2d');
  getDataDashPollution()
    .then((result) => {
      if (result != null) {
        for (var chave in result) {
          var valor = result[chave];
          valores.push(valor);
        }
        const data = {
          labels: ["CO", "NO", "NO2", "O3", "SO2", "PM2.5", "PM10", "NH3"],
          datasets: [{
            data: valores,
            backgroundColor: [
              'rgba(44,127,184)',
              'rgba(49,163,84)',
              'rgba(222,45,38)',
              'rgba(254,196,79)',
              'rgba(250,159,181)',
              'rgba(201,148,199)',
              'rgba(166,189,219)',
              'rgba(28,144,153)'
            ],
            borderWidth: 1
          }]
        };
        var myChart = new Chart(ctx, {
          type: 'doughnut',
          data: data,
          responsive: true,
        });
      }
    });
}

function getDataDashPollution() {
  const url = `${baseUrl}/AirPollution/GetDataAirPollutionDashBoard`;
  const body = {
    "displayName": "Uberlandia",
    "lat": -18.882,
    "lon": -48.2831
  };

  return new Promise((resolve, reject) => {
    fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    })
      .then((response) => response.json())
      .then((result) => {
        if (result.success) {
          resolve(result.data);
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Erro buscar informações da poluição do ar!',
            text: result.message
          }).then(() => {
            resolve(null);
          })
        }
      })
      .catch((error) => {
        reject(error);
      });
  });
}

//#region informações de regioes
function openModal(argument) {
  var myModal = new bootstrap.Modal(document.getElementById('myModal'));
  
  const location = locations.find(a => a.region === argument);

  // TODO: criar um endpoint que retorna os trem resumido de tempo poluição vento etc (ver se já nao tem no Weather/GetCurrentWeather) e colocar pra regiao
  // getAirPollutionRegion(location);
  setRegionWeatherData(location);
  document.getElementById('myModalLabel').textContent = "Região " + argument;
  myModal.show();
}

function getAirPollutionRegion(location = {displayName: "Uberlandia", lat: -18.909216, lon: -48.2622005, region: "Uberlandia", address: ""}) {
  const url = `${baseUrl}/AirPollution/GetAirPollution`;

  document.getElementById('modalContent').textContent = "Carregando..."

  const body = {
    "displayName": location.displayName,
    "lat": location.lat,
    "lon": location.lon
  }

  fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body),
  })
    .then((response) => response.json())
    .then((result) => {
      if (result.success) {
          console.log(result)
          let components = result.data.list[0].components
          document.getElementById('modalContent').textContent = "Conteúdo específico sobre a região " + location.region + ": " + 
                                                              location.displayName + " - " + result.data.air_pollution_description +
                                                              " | Co: " + components.co + " | No2: " + components.no2 + " | O3: " +
                                                              components.o3;
      } else {
        Swal.fire({
          icon: 'error',
          title: 'Erro ao buscar poluição do ar!',
          text: result.message
        });
      }
    });
}

function setRegionWeatherData(location) {
  const listRegionData = [];

  getCurrentWeather(location.displayName, location.lat, location.lon)
    .then((result) => {
      if (result != null) {
        result.displayName = location.displayName
        listRegionData.push(result)
        buildDatatable(listRegionData, location)
      }
    });
}

function buildDatatable(regionData, location) {
  $('#regionTable').DataTable({
      dom: 'Bfrtip',
      buttons: [
          'copyHtml5',
          'excelHtml5',
          'csvHtml5',
          'pdfHtml5'
      ],
      data: regionData,
      searching: false,
      columns: [
          {
            title: 'Lugar',
            data: 'displayName',
            className: 'text-center',
            render: function ( data, type, row) {
                return `${data}`
            }
          },
          // {
          //     title: 'Poluição',
          //     data: 'air_pollution_description',
          //     className: 'text-center',
          //     render: function ( data, type, row) {
          //         if(data != null){
          //             return `${data}`
          //         }
          //         return ''
          //     }
          // },
          {
            title: 'Visibilidade',
            data: 'visibility',
            className: 'text-center',
            render: function ( data, type, row) {
                if(data != null){
                    return `${data} m`
                }
                return ''
            }
          },
          {
            title: 'Velocidade do vento',
            data: 'wind.speed',
            className: 'text-center',
            render: function ( data, type, row) {
              if(data != null){
                return `${data} km/h`
              }
              return ''
            }
          }
      ],
  });
}
//#endregion