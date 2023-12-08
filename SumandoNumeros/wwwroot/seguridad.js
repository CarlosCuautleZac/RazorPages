

async function estoyAutenticado() {
    return sessionStorage.jwt;
}

async function fetchToken(username, password) {
    let userDto = { Usuario: username, Contraseña: password };
    let response = fetch("https://integracion.itesrc.net/login", {
        method: "post",
        body: JSON.stringify(userDto),
        headers: {
            "content-type": "application/json"
        }
    });

    if (response.ok) {

        var token = await response.json();

    } else if (response.status == 401) {


    }
}

async function login() {
    var credenciales = await navigator.credentials.get({ password: true });


    if (credenciales) {    //ya tengo credenciales

    }
    else {  //No tengo guardadas
        location.href = "/login";
    }
}

async function descargarDatos() {
    if (estoyAutenticado()) {


    } else {
        //si no, tratamos de autenticarnos
        await login();
    }
}