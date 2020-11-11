const computeColumnChart = (rawData, graph_id) => {
    google.charts.load("current", {packages:['corechart']});
    google.charts.setOnLoadCallback(drawChart);

    var graphData = [["Date", "Temperature", {role: "style"}]];
    rawData.forEach(obj => {
        graphData.push([obj.Date.substr(5, 5), parseFloat(obj.Temp), "green"]); // [Date, Temperature, BarColor]
    });
    console.log(graphData);
                    
    function drawChart() {
        var data = google.visualization.arrayToDataTable(graphData);

        var view = new google.visualization.DataView(data);
        view.setColumns([0, 1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" }, 2]);
        var options = {
            title: "Employee Temperature by Day:",
            height: 400,
            bar: {groupWidth: "95%"},
            legend: { position: "none" },
        };
        google.visualization.NumberFormat()
        var chart = new google.visualization.ColumnChart(document.getElementById(graph_id));
        chart.draw(view, options);
    }
}

const computeLineChart = (rawData, graph_id) => {
    google.charts.load('current', {'packages':['corechart']});
    google.charts.setOnLoadCallback(drawChart);

    var graphData = [["Date", "Temperature"]];
    rawData.forEach(obj => {
        graphData.push([obj.Date.substr(5, 5), parseFloat(obj.Temp)]); // [Date, Temperature, BarColor]
    });
    console.log(graphData);

    function drawChart() {
        var data = google.visualization.arrayToDataTable(graphData);
        var options = { title: 'Employee Temperatures', curveType: 'function', height: 400, legend: { position: 'bottom' } };
        var chart = new google.visualization.LineChart(document.getElementById(graph_id));
        chart.draw(data, options);
    }
}