function createChart(chartwarp,data,color)
{
    chartwarp.highcharts({
        chart: {
            type: 'column',
            backgroundColor:null
        },
        title: {
            text: ''
        },
        credits:{enabled:false},
        xAxis: {
            categories: [
                '1#',
                '2#',
                '3#',
                '4#',
                '5#',
                '6#',
                '7#',
                '8#',
                '9#',
                '10#',
                '11#',
                '12#'
            ],
            labels: {
                rotation: -45,
                align: 'right',
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }

        },
        legend: {
            enabled:false
        },
        yAxis: {
            min: 0,
            title: {
                text: '效率(%)'
            },
            gridLineColor: "#4a4a4a"
        },        
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        colors: [color],
        series: [{
            name: '卷包设备',
            data: data,
            dataLabels: {
                enabled: true,
                rotation: -90,
                color: '#FFFFFF',
                align: 'right',
                x: 4,
                y: 10,
                style: {
                    fontSize: '11px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 0px black'
                }
            }

        }]
    });
}