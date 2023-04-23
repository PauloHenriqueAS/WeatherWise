document.addEventListener("DOMContentLoaded", function () {
    loadUserInformations();
});

function openProfilePhotoModal() {
    $('#profilePhotoModal').modal('show');
}

function getProfilePhoto(inputFile) {
    return new Promise(function (resolve, reject) {
        if (!inputFile || !inputFile.files || inputFile.files.length === 0) {
            reject('Invalid file input element');
            return;
        }

        const file = inputFile.files[0];
        const reader = new FileReader();
        reader.onload = function () {
            const base64 = reader.result;
            resolve(base64);
        };
        reader.onerror = function () {
            reject(reader.error);
        };

        reader.readAsDataURL(file);
    });
}



function changeProfilePhoto() {
    const fileInput = $('#inputProfilePhoto')[0];

    getProfilePhoto(fileInput)
        .then(function (base64) {
            $('#profilePhoto').prop('src', base64);
            closeProfilePhotoModal();
        });
}

function validateProfileUserData() {
    return new Promise((resolve, reject) => {
        const thereIsEmptyFild = $('#first_name').val().trim() == '' ||
            $('#last_name').val().trim() == '' ||
            $('#profilePassword').val().trim() == '' ||
            $('#profileRepeteatedPassword').val().trim() == '';

        if (thereIsEmptyFild) {
            warningAlert('Não foi possível atualizar perfil!', 'Preencha todos os campos!');
            resolve(false);
        } else if ($('#profilePassword').val() != $('#profileRepeteatedPassword').val()) {
            warningAlert('Não foi possível atualizar perfil!', 'Senhas não coincidem!');
            resolve(false);
        } else {
            resolve(true);
        }
    });
}

function updateLocalStorageUserInformations() {
    return new Promise(function(resolve, reject) {
      const user = getUserData();
      const url = `${baseUrl}/User/GetUserInfo?email_user=${user.email_user}`;
  
      fetch(url, {
          headers: {
              'Content-Type': 'application/json',
          }
      })
          .then((response) => response.json())
          .then((result) => {
              if (result.success) {
                  sessionStorage.setItem("userData", JSON.stringify(result.data));
                  resolve(result.data);
              } else {
                  Swal.fire({
                      icon: 'error',
                      title: 'Erro ao consultar informações de perfil!',
                      text: result.message
                  });
                  reject(result.message);
              }
          })
          .catch((error) => {
              reject(error);
          });
    });
  }

function saveProfileChanges(){
    const user = getUserData();
    const url = `${baseUrl}/User/PutUserInfo`;

    const body = {
        "name_user": `${$('#first_name').val()} ${$('#last_name').val()}`,
        "email_user": user.email_user,
        "password_user": $('#profilePassword').val(),
        "profile_image": $('#profilePhoto').attr("src"),
    }

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
                    title: 'Perfil atualizado com sucesso!'
                }).then(() => { 
                    updateLocalStorageUserInformations().then(() => { 
                        loadUserInformations();
                        window.location.reload();
                    });                   
                })
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro ao atualizar perfil!',
                    text: result.message
                });
            }
        });
}

function validateProfileChanges() {


    Swal.fire({
        title: 'Deseja atualizar o perfil?',
        confirmButtonText: 'Sim',
        showCancelButton: true,
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        console.log(result.isConfirmed);
        if(result.isConfirmed) {
            validateProfileUserData().then((isDataValid) => {
                if(isDataValid){
                    saveProfileChanges();
                }
            });
        }
        
    });
}

function closeProfilePhotoModal() {
    $('#profilePhotoModal').modal('hide');
}

function loadUserInformations(){
    const user = getUserData();
    const splitedName = user.name_user.split(' ');
    const firstName = splitedName[0];
    const lastName = splitedName[splitedName.length - 1];

    if(user.profile_image != null && user.profile_image != undefined){
        $('#profilePhoto').prop('src', user.profile_image);
    }

    $('#first_name').val(firstName);
    $('#last_name').val(lastName);
}