

async function estoyAutenticado() {
    return await sessionStorage.jwt;
}

async function fetchToken(username, password) {
    let userDto = { Usuario: username, Contraseña: password };
    let response = await fetch("https://integracion.itesrc.net/login", {
        method: "post",
        body: JSON.stringify(userDto),
        headers: {
            "content-type": "application/json"
        }
    });

    if (response.ok) {

        var token = await response.text();
        sessionStorage.jwt = token;
        let credencial = new PasswordCredential({
            id: username,
            password: password,
            username: username
        });


        await navigator.credentials.store(credential);

        //redirigir
        location.href = "/panes";

    } else if (response.status == 401) {
        document.querySelector(".error").textContent = "Nombre de usuario o contraseña incorrectas";

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

    let autenticado =  await estoyAutenticado();

    if (autenticado) {
        console.log("si estas dentro mijo");

    } else {
        //si no, tratamos de autenticarnos
        await login();
    }
}
