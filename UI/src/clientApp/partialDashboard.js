document.addEventListener("DOMContentLoaded", function () {
  setWeatherScreenData();
  getAirPollution();
  generateChartWindSpeed();
  generateChartPollution();
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

function getCurrentWeather() {
  const url = `${baseUrl}/Weather/GetCurrentWeather`;
  const body = {
    "displayName": "Uberlandia",
    "lat": -18.909216,
    "lon": -48.2622005
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



function getAirPollution() {
  const url = `${baseUrl}/GetAirPollution`;

  const body = {
    "displayName": "Uberlandia",
    "lat": -18.909216,
    "lon": -48.2622005
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
        $('#airPollutionDescription').text(result.data.air_pollution_description);
      } else {
        Swal.fire({
          icon: 'error',
          title: 'Erro buscar poluiçaõ do ar!',
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