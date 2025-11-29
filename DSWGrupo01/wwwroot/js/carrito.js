namespace DSWGrupo01.wwwroot.js
{
    document.querySelectorAll(".btn-mas").forEach(btn => {
        btn.addEventListener("click", () => {
            fetch(`/Carrito/CambiarCantidad?id=${btn.dataset.id}&delta=1`)
                .then(() => location.reload());
        })
    });

    document.querySelectorAll(".btn-menos").forEach(btn => {
        btn.addEventListener("click", () => {
            fetch(`/Carrito/CambiarCantidad?id=${btn.dataset.id}&delta=-1`)
                .then(() => location.reload());
        })
    });
}
