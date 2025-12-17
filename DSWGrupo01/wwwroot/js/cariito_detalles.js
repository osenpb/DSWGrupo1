// Manejo de cantidad de productos
(function () {
    'use strict';

    // Elementos del DOM
    const cantidadInput = document.getElementById("cantidad");
    const btnMenos = document.getElementById("btnMenos");
    const btnMas = document.getElementById("btnMas");

    // Validar que los elementos existen
    if (!cantidadInput || !btnMenos || !btnMas) {
        console.error("No se encontraron los elementos necesarios para el control de cantidad");
        return;
    }

    /**
     * Disminuir cantidad
     */
    btnMenos.addEventListener("click", function () {
        let valorActual = parseInt(cantidadInput.value) || 1;
        if (valorActual > 1) {
            cantidadInput.value = valorActual - 1;
        }
    });

    /**
     * Aumentar cantidad
     */
    btnMas.addEventListener("click", function () {
        let valorActual = parseInt(cantidadInput.value) || 1;
        cantidadInput.value = valorActual + 1;
    });

    /**
     * Validar input manual
     */
    cantidadInput.addEventListener("change", function () {
        let valor = parseInt(this.value);

        // Si no es un número válido o es menor que 1, resetear a 1
        if (isNaN(valor) || valor < 1) {
            this.value = 1;
        }
    });

    /**
     * Prevenir valores negativos al escribir
     */
    cantidadInput.addEventListener("keypress", function (e) {
        // Permitir solo números
        if (e.key === "-" || e.key === "+" || e.key === "e") {
            e.preventDefault();
        }
    });

})();