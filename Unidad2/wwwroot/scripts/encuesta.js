const pregunta = document.querySelector("#pregunta");
const escala = document.querySelector(".escala");
const boton = document.querySelector("#btn");
const loader = document.querySelector("#loader");

var basUrl = "https://integracion.itesrc.net/api/encuesta";
var listaPreguntas = null, actual, seleccionado, respuestas = [];

pregunta.hidden = true;
escala.style.display = 'none';
boton.hidden = true;


async function descargarEncuesta() {
    var opts = {
        headers: {
            'mode': 'cors'
        }
    }
    let response = await fetch(basUrl,opts);
    if (response.ok) {
        listaPreguntas = await response.json();
        actual = 0;
        mostrarPregunta();
        loader.hidden = true;

        //mostrar controles
        pregunta.hidden = false;
        escala.style.display = null;
        boton.hidden = false;
    }
    else {
        console.log("Ha ocurrido un error al descargar la encuesta.");
    }
}

function mostrarPregunta() {
    let objeto = listaPreguntas[actual];
    pregunta.textContent = objeto.pregunta;

    if (seleccionado) {
        seleccionado.classList.remove("selected");
        seleccionado = null;
    }

};7

escala.addEventListener("click", function (e) {
    if (e.target.tagName == "IMG") {
        if (seleccionado) {
            seleccionado.classList.remove("selected");
        }
        e.target.classList.add("selected");
        seleccionado = e.target;
    }
});


boton.addEventListener("click", async function () {
    //Verificar que selecciono algo
    if (seleccionado) {
        let respuesta = {
            id: listaPreguntas[actual].id,
            valor: seleccionado.dataset.valor
        }
        console.log(respuesta);
        respuestas.push(respuesta);
        //verificar si quedan preguntas
        if (actual < listaPreguntas.length - 1) {
            actual++;
            mostrarPregunta();

            boton.value = actual == listaPreguntas.length - 1 ? "TERMINAR" : "SIGUIENTE";
        }
        else {//Termino
            console.log(respuestas);

            //mostrar controles
            pregunta.hidden = true;
            escala.style.display = "none";
            boton.hidden = true;
            loader.hidden = false;

            let response = await fetch(basUrl, {
                method: "post",
                body: JSON.stringify(respuestas),
                headers: {
                    "Content-Type": "application/json"
                }  
            });

            if (response.ok) {
                window.location.href = "/encuesta/agradecimiento";
            }

        }
    }
    else {
        alert("Debes seleccionar una respuesta para continuar");
    }
});

descargarEncuesta();