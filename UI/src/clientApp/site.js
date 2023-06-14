const baseUrl = 'https://localhost:7126';

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

document.addEventListener("DOMContentLoaded", function (event) {
    const isLoginPage = window.location.href.includes('login.html');

    if (!isSessionOn() && !isLoginPage && isPagePrivate()) {

        Swal.fire({
            icon: 'warning',
            title: 'Acesso não autorizado!',
            text: 'Faça login!'
        }).then(() => { window.location.replace("login.html"); });
    }else {
        const user = getUserData();

        $('.userName').text(user.name_user);

        if(user.profile_image != null && user.profile_image != undefined){
            $('.img-profile').prop('src', user.profile_image);
        }        
    }
});

function isSessionOn() {
    const user = localStorage.getItem("userData");
    return user != null && user != '' && user != undefined;
}

function clearSession() {
    localStorage.removeItem("userData");
}

function getUserData(){
    const user = localStorage.getItem("userData")

    return JSON.parse(user);
}

function logoutUser() {
    clearSession()
    window.location.replace("login.html");
}


function warningAlert(titleMessage = '', textMessage = '')
{
    Swal.fire({
        icon: 'warning',
        title: titleMessage,
        text: textMessage
    });
}

function isPagePrivate() {
    const currentPage = window.location.href;
    const privatePages = ['dashboard.html', 'report.html', 'warning.html', 'profile.html', 'warning.html']

    for (let i = 0; i < privatePages.length; i++) {
        if (currentPage.includes(privatePages[i])) {
            return true;
        }
    }

    return false;
}

function openModal(argument) {
    var myModal = new bootstrap.Modal(document.getElementById('myModal'));
    
    const location = locations.find(a => a.region === argument);

    getAirPollutionRegion(location)
    // getAirPollutionRegion("Praça da Bicota", -18.9229, -48.2791, argument)
    document.getElementById('myModalLabel').textContent = "Região " + argument;
    // document.getElementById('modalContent').textContent = "Conteúdo específico sobre a região " + argument;
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