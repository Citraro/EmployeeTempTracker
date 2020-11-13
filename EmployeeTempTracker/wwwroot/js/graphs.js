const computeLineChart = (rawData, graph_id) => {
    google.charts.load('current', {'packages':['corechart']});
    google.charts.setOnLoadCallback(drawChart);

    var graphData = [["Date", "Temperature"]];
    rawData.forEach(obj => {
        graphData.push([obj.Date.substr(5, 5), parseFloat(obj.Temp)]);
    });
    console.log(graphData);

    function drawChart() {
        var data = google.visualization.arrayToDataTable(graphData);
        var options = { title: 'Most Recent <= Oldest', curveType: 'function', height: 400, legend: { position: 'bottom' } };
        var chart = new google.visualization.LineChart(document.getElementById(graph_id));
        chart.draw(data, options);
    }
}