window.initializeChart = (canvasId) => {
    var ctx = document.getElementById(canvasId).getContext('2d');
    window.CoinChart = new Chart(ctx, {
        type: 'line',
        data: {
            datasets: [{
                label: 'Price in USD',
                backgroundColor: 'rgba(197, 186, 107, 1)',
                borderColor: 'rgba(0, 0, 0, 0.7)', // Línea más definida
                borderWidth: 1, // Incremento del grosor de la línea
                fill: true,
                tension: 0.3, // Ligera tensión para suavizar la línea
                pointBackgroundColor: 'rgba(0, 0, 0, 0.7)', // Color de los puntos
                pointBorderColor: 'rgba(255, 255, 255, 0.8)', // Color del borde de los puntos
                pointBorderWidth: 1, // Ancho del borde de los puntos
                pointRadius: 1, // Tamaño de los puntos
                pointHoverRadius: 1, // Tamaño de los puntos al pasar el mouse
                pointHoverBackgroundColor: 'rgba(0, 0, 0, 1)', // Color de los puntos al pasar el mouse
                pointHoverBorderColor: 'rgba(255, 255, 255, 1)', // Color del borde de los puntos al pasar el mouse
                pointHoverBorderWidth: 1 // Ancho del borde de los puntos al pasar el mouse
            }]
        },
        options: {
            plugins: {
                legend: { display: false },
                tooltip: { // Opciones para la tooltip
                    callbacks: {
                        label: function (context) {
                            let label = context.dataset.label || '';
                            if (label) {
                                label += ': ';
                            }
                            if (context.parsed.y !== null) {
                                label += `$${context.parsed.y.toFixed(2)}`;
                            }
                            return label;
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: {
                        display: false // Eliminar líneas de la cuadrícula del eje X
                    }
                },
                y: {
                    grid: {
                        color: 'rgba(0, 0, 0, 0.1)', // Color de las líneas de la cuadrícula del eje Y
                        lineWidth: 1 // Ancho de las líneas de la cuadrícula del eje Y
                    }
                }
            },
            elements: {
                line: {
                    borderJoinStyle: 'round' // Mejora la apariencia de las uniones de las líneas
                }
            }
        }
    });
};

window.updateChart = (labels, data) => {
    var chart = window.CoinChart;
    chart.data.labels = labels;
    chart.data.datasets[0].data = data;
    chart.update();
};