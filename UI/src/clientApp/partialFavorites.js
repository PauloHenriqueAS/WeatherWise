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
                console.log(result.data);
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
            console.error(error);
        });
}

function buildDatatable(favoritesData) {
    $('#favoriteTable').DataTable({
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
                    console.log("DATA LOCALIDADE", data);
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
                title: 'Ações',
                data: 'desactivationDate',
                className: 'text-center',
                render: function (data, type, row) {
                    if (data == null) {
                        return 'ativado';
                    } else {
                        return 'desativado';
                    }
                }
            }
        ],
    });
}