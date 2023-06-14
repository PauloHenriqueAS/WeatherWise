document.addEventListener("DOMContentLoaded", function () {
    getFavoriteLocations();
});

function getFavoriteLocations() {
    const user = getUserData();
    const url = `${baseUrl}/User/GetHistoricCoordenatesUserInfo?emailUser=${user.email_user}`;

    return fetch(url, {
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then((response) => response.json())
        .then((result) => {
            if (result.success) {
                buildDatatable(result.data);
            } else {
                throw new Error('Erro na consulta das localidades favoritas');
            }
        })
        .catch((error) => {
            Swal.fire(
                'Erro!',
                'Erro na consulta das localidades favoritas',
                'error'
            );
        });
}

function buildDatatable(favoritesData) {
    $('#favoriteTable').DataTable({
        bDestroy: true,
        searching: false,
        dom: 'Bfrtip',
        buttons: [
            'copyHtml5',
            'excelHtml5',
            'csvHtml5',
            'pdfHtml5'
        ],
        data: favoritesData,
        columns: [
            {
                title: 'Localidade',
                data: 'displayName',
                className: 'text-center',
                render: function (data, type, row) {
                    return `${data}`;
                }
            },
            {
                title: 'Latitude',
                data: 'lat',
                className: 'text-center',
                render: function (data, type, row) {
                    if (data != null) {
                        return `${data}`;
                    }
                    return '';
                }
            },
            {
                title: 'Longitude',
                data: 'lon',
                className: 'text-center',
                render: function (data, type, row) {
                    if (data != null) {
                        return `${data}`;
                    }
                    return '';
                }
            },
            {
                title: 'Status',
                data: 'desactivationDate',
                className: 'text-center',
                render: function (data, type, row) {
                    if (data == null) {
                        return `<i class="fas fa-power-off activate-power-button" style="color:green" title="Clique para desativar" onclick="activateDesactivateFavorite(${false})"></i>`;
                    } else {
                        return `<i class="fas fa-power-off desactivate-power-button" style="color:red" "Clique para ativar" onclick="activateDesactivateFavorite(${true})></i>`;
                    }
                }
            }
        ],
    });
}

function activateDesactivateFavorite(activate) {
    let actionText = activate ? 'ativar' : 'desativar';
    Swal.fire({
        title: `Deseja ${actionText} localidade favorita?`,
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Cancelar',
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire('Alerta desativado com sucesso', '', 'success').then(() => {
                getFavoriteLocations();
            });
        }
    })
}