document.addEventListener("DOMContentLoaded", function () {
    setWeatherScreenData();
    getAirPollution();
});

function setWeatherScreenData(){
    getCurrentWeather()
        .then((result) => {
            if(result != null){
                $('#windSpeed').text(`${result.wind.speed} Km/h`);
                $('#visibility').text(result.visibility);                
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
  


function getAirPollution(){
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