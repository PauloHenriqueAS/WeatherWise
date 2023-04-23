function registerAlert(){
    const user = getUserData();
    const url = `${baseUrl}/Weather/InsertAlert`;

    const body = {
        "wind_speed": parseFloat($('#windSpeedAlert').val() == '' ? 0 : $('#windSpeedAlert').val()),
        "email_user": user.email_user,
        "precipitation": parseFloat($('#preciptationAlert').val() == '' ? 0 : $('#preciptationAlert').val()),
        "visibility": parseFloat($('#visibilityAlert').val() == '' ? 0 : $('#visibilityAlert').val()),
        "air_pollution_aqi": parseInt($('#airPollutionSituation').val() == '' ? 0 : $('#airPollutionSituation').val())
    }

    console.log(JSON.stringify(body));

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8',  
        },
        body: JSON.stringify(body),
    })
        .then((response) => response.json())
        .then((result) => {
            if (result.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Alerta cadastrado com sucesso!'
                }).then(() => { 
                    window.location.reload();
                })
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro ao cadastrar alerta!',
                    text: result.message
                });
            }
        });
}