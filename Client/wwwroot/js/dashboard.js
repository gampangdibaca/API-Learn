let donutSeries = [];

$(document).ready(function () {

    $.ajax({
        type: "GET",
        url: "https://localhost:44330/Employees/GetGenderDistribution",
        context: document.body,
    }).done((result) => {
        donutSeries.push(result.data.Male);
        donutSeries.push(result.data.Female);
        var optionsGender = {
            chart: {
                type: 'donut',
                height: 350,
                toolbar: {
                    show: true,
                    offsetX: 0,
                    offsetY: 0,
                    tools: {
                        download: true,
                        selection: true,
                        zoom: true,
                        zoomin: true,
                        zoomout: true,
                        pan: true,
                        reset: true | '<img src="/static/icons/reset.png" width="20">',
                        customIcons: []
                    },
                    export: {
                        csv: {
                            filename: undefined,
                            columnDelimiter: ',',
                            headerCategory: 'category',
                            headerValue: 'value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: undefined,
                        },
                        png: {
                            filename: undefined,
                        }
                    },
                    autoSelected: 'zoom'
                },
            },
            series: donutSeries,
            labels: ['Male', 'Female']
        }

        var genderChart = new ApexCharts(document.querySelector("#chart-gender"), optionsGender);

        genderChart.render();
    }).fail((error) => {
        console.log(error.responseJSON.message);
    });

    $.ajax({
        type: "GET",
        url: "https://localhost:44354/api/Educations/GetUniversityDistribution",
        context: document.body,
    }).done((result) => {
        let counts = [];
        let universities = [];
        for (let i = 0; i < result.data.length; i++) {
            counts.push(result.data[i].count);
            if (result.data[i].name.indexOf(' ') == -1) {
                universities.push(result.data[i].name);
            } else {
                universities.push(result.data[i].name.split(' '));
            }
        }
        var options = {
            chart: {
                type: 'bar',
                height: 350,
            },
            series: [{
                name: 'counts',
                data: counts
            }],
            xaxis: {
                categories: universities
            }
        }

        var chart = new ApexCharts(document.querySelector("#chart-university"), options);

        chart.render();
    }).fail((error) => {
        console.log(error.responseJSON.message);
    });

});

