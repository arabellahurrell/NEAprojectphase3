window.onload = function(){
    var xvalue = 0;
    var yvalue = 10;
    var datacount = 6;
    dataPoints = [];//syntax to create array in javascript
    var chart = new CanvasJS.Chart("chartContainer",{
        theme: "light2",title: {
            text: "Graph"
        }, axisY: {
            suffix: " " 
        },data: [{
            type: "spline", dataPoints: dataPoints
        }]
    });
    function addData(data) {
        if (datacount !== 1) {
            $.each(data, function (key, value) {
                dataPoints.push({x: value.x, y: parseInt(value.y)});
                xvalue++;
                yvalue = parseInt(value.y);
            });
            datacount = 1;
        }
    }}
    
