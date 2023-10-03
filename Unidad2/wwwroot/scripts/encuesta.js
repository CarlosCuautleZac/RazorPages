const pregunta = document.querySelector("#pregunta");
const escala = document.querySelector(".escala");
const boton = document.querySelector("#btn");
const loader = document.querySelector("#loader");


var basUrl = "https://integracion.itesrc.net/api/encuesta";
var listaPreguntas = null, actual, seleccionado;

pregunta.hidden = true;
escala.style.display = 'none';
boton.hidden = true;


async function descargarEncuesta() {
    let response = await fetch(basUrl);
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


};

escala.addEventListener("click", function (e) {
    if (e.target.tagName == "IMG") {
        if (seleccionado) {
            seleccionado.classList.remove("selected");
        }
        e.target.classList.add("selected");
        seleccionado = e.target;
    }
});

descargarEncuesta();