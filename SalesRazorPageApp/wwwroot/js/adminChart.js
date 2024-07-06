const baseUrl = `${window.location.protocol}//${window.location.host}`;

//Draw charts
function drawChart(data) {
    const ctx = document.getElementById('myChart').getContext('2d');

    // Process data to get labels and totalSales
    const labels = data.map(item => `${item.year}-${item.month}`);
    const totalSales = data.map(item => item.totalSales);

    // Create the chart
    const myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Total Sales',
                data: totalSales,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

document.addEventListener("DOMContentLoaded", async () => {
    try {
        const response = await fetch(`${baseUrl}/api/stats`);
        const data = await response.json();

        console.log('Stat data:');
        console.log({data});

        drawChart(data);

    } catch (error) {
        console.error(error);
    }
});


