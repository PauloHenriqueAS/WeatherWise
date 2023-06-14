const baseUrl = 'https://localhost:7126';

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