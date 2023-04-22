function isSessionOn() {
    const user = sessionStorage.getItem("userData");
    return user != null && user != '' && user != undefined;
}

function clearSession(){
    sessionStorage.removeItem("userData");   
}

function logoutUser(){
    clearSession()
    window.location.replace("login.html");
}

function isPagePrivate() {
    const currentPage = window.location.href;
    const privatePages = ['dashboard.html', 'report.html', 'warning.html', 'profile.html', 'warning.html']

    for(let i = 0; i < privatePages.length; i++)
    {
        console.log('pages', currentPage, privatePages[i], currentPage.includes(privatePages[i]))
        if(currentPage.includes(privatePages[i])){
            return true;
        }
    }

    // privatePages.forEach((page) => {
    //     console.log('pages', currentPage, page, currentPage.includes(page))
    //     if(currentPage.includes(page)){
    //         return true;
    //     }
    // });

    return false;
}

document.addEventListener("DOMContentLoaded", function(event) { 
    const isLoginPage = window.location.href.includes('login.html');
    
    console.log(isPagePrivate())

    if(!isSessionOn() && !isLoginPage && isPagePrivate()){

        Swal.fire({
            icon: 'warning',
            title: 'Acesso não autorizado!',
            text: 'Faça login!'        
    }).then(() => { window.location.replace("login.html"); });
    }
});