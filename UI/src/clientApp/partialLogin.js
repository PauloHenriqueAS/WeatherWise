function createNewLoginUser() {
    const url = `${baseUrl}/User/PostUserInfo`;

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
            if (result.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Usuário cadastrado com sucesso!',
                    text: result.message
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro ao cadastrar usuário!',
                    text: result.message
                });
            }
        });
}

function storeUserOnLocalStorage(user) {
    localStorage.setItem("userData", JSON.stringify(user))
}

function LoginUser() {
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
                storeUserOnLocalStorage(result.data);
                window.location.replace("dashboard.html");
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Não foi possível fazer login',
                    text: result.message
                });
            }
        });
}

function validateRegisterUserData() {
    return new Promise((resolve, reject) => {
        const thereIsEmptyFild = $('#exampleFirstName').val().trim() == '' ||
            $('#exampleLastName').val().trim() == '' ||
            $('#exampleInputEmail').val().trim() == '' ||
            $('#examplePasswordInput').val().trim() == '' ||
            $('#exampleRepeatPasswordInput').val().trim() == '';

        if (thereIsEmptyFild) {
            warningAlert('Não foi possível realizar cadastro', 'Preencha todos os campos!');
            resolve(false);
        } else if ($('#examplePasswordInput').val().trim() != $('#exampleRepeatPasswordInput').val().trim()) {
            warningAlert('Não foi possível realizar cadastro!', 'Senhas não coincidem!');
            resolve(false);
        } else {
            resolve(true);
        }
    });
}

function registerUser() {
    const url = `${baseUrl}/User/PostUserInfo`;
    validateRegisterUserData().then((isDataValid) => {
        if (isDataValid) {
            const body = {
                "name_user": `${$('#exampleFirstName').val().trim()} ${$('#exampleLastName').val().trim()}`,
                "email_user": `${$('#exampleInputEmail').val().trim()}`,
                "password_user": `${$('#examplePasswordInput').val().trim()}`,
                "type_user": 0
            }

            console.log(JSON.stringify(body));

        
            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json;charset=UTF-8',
                    'mode': 'no-cors',
                },
                body: JSON.stringify(body),
            })
                .then((response) => response.json())
                .then((result) => {
                    if (result.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Cadastro realizado com sucesso!'
                        }).then(() => { window.location.replace("login.html"); });
                        
                    } else {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Erro ao realizar cadastro'
                        });
                    }
                });
        }
    });
}