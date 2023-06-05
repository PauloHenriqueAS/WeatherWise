document.addEventListener("DOMContentLoaded", function () {
    getReportInformations();
});

function getReportInformations() {
    const user = getUserData();
    const url = `${baseUrl}/Weather/GetAlertByUser?email_user=${user.email_user}`;

    fetch(url, {
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then((response) => response.json())
        .then((result) => {
            if (result.success) {
                buildDatatable(result.data);
            }
        })
        .catch((error) => {
            reject(error);
        });
}

function getAirPollutionAqiDescription(aqi) {
    switch (aqi) {
        case 1:
            return "Bom";
        case 2:
            return "Normal";
        case 3:
            return "Moderado";
        case 4:
            return "Pobre";
        case 5:
            return "Muito pobre";
        default:
            return "Inexistente";
    }
}

function buildDatatable(reportData) {
    $('#reportTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copyHtml5',
            'excelHtml5',
            'csvHtml5',
            'pdfHtml5'
        ],
        data: reportData,
        columns: [
            {
                title: 'Precipitação </br> máxima diária',
                data: 'preciptation',
                className: 'text-center',
                render: function ( data, type, row) {
                    return `${data} mm`
                }
            },
            {
                title: 'Velocidade do vento',
                data: 'wind_speed',
                className: 'text-center',
                render: function ( data, type, row) {
                    if(data != null){
                        return `${data} km/h`
                    }
                    return ''
                }
            },
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
                title: 'Poluição do ar',
                data: 'air_pollution_aqi',
                className: 'text-center',
                render: function ( data, type, row) {
                    return getAirPollutionAqiDescription(data)
                }
            },
            {
                title: 'Ações',
                data: 'desactivationDate',
                className: 'text-center',
                render: function ( data, type, row) {
                    if(data == null){
                        return `<i class="fas fa-power-off activate-power-button" title="Clique para desativar" onclick="activateDesactivateAlert(${false})"></i>`;
                    }else{
                        return `<i class="fas fa-power-off desactivate-power-button" "Clique para ativar" onclick="activateDesactivateAlert(${true})></i>`;
                    }
                }
            }
        ],
    });
}

function activateDesactivateAlert(activate){
    let actionText = activate ? 'ativar' : 'desativar';
    Swal.fire({
        title: `Deseja ${actionText} alerta?`,
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Cancelar',
      }).then((result) => {
        if (result.isConfirmed) {
          Swal.fire('Alerta desativado com sucesso', '', 'success').then(() => {
            getReportInformations();
          });
        } 
      })
}