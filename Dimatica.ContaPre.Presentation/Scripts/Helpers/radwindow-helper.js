function showSpinnerInWindow(oWnd, innerHTML = '')
{

    console.log(oWnd.get_id());
    // Obtener la instancia de la ventana
    var window = $find(oWnd.get_id());

    if (innerHTML === '')
    {
        innerHTML = "<p>Cargando el contenido, espere por favor...</p>";
    }

    if (window)
    {
        // Crear un spinner dentro de la ventana
        var contentElement = window.get_contentElement();
        var spinner = document.createElement("div");
        spinner.id = "my-spinner";
        spinner.style.position = "absolute";
        spinner.style.top = "50%";
        spinner.style.left = "50%";
        spinner.style.transform = "translate(-50%, -50%)";
        spinner.style.zIndex = "9999";
        spinner.innerHTML = innerHTML;
        contentElement.appendChild(spinner);
    }
}

function hideSpinnerInWindow(oWnd)
{
    // Ocultar el spinner una vez que el contenido esté cargado
    var spinner = document.getElementById("my-spinner");
    if (spinner)
    {
        spinner.remove();
    }
}
