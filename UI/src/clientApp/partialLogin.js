const baseUrl = 'https://localhost:7126';

function createNewLoginUser(){
    const urlBase = 'https://localhost:7126';
    const url = `${urlBase}/User/PostUserInfo`;
    
    const body = {
        "name_user": "string",
        "email_user": "string",
        "password_user": "string",
        "type_user": 0
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
            jsLoading(false);
            if (result.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Imagem anexada com sucesso',
                    text: result.message
                });

                saveAttachedImages(getProtocolNumberByUrl(), true);
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro ao anexar imagem',
                    text: result.message
                });
            }
        });
}

function storeUserOnLocalStorage(user){
    sessionStorage.setItem("userData", JSON.stringify(user))
}

function LoginUser(){
    const url = `${baseUrl}/User/AuthorizeUser`;
    
    console.log(url)

    const body = {
        "email_user": $("#exampleInputEmail").val(),
        "password_user": $("#exampleInputPassword").val()
    }
    
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'mode': 'no-cors',
        },
        body: JSON.stringify(body),
    })
        .then((response) => response.json())
        .then((result) => {
            if (result.success) {
                sessionStorage.setItem("userData", JSON.stringify(result.data))
                window.location.replace("report.html");
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Não foi possível fazer login',
                    text: result.message
                });
            }
        });
}