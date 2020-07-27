function barcharts(reporte,fecha) {
    'use strict';
    var button = document.getElementById("submitButton");
    submitButton.addEventListener("click", function () {
        chart.destroy();
    });
    

    //el metodo
    var dias=[]
    var ordenes = [];

    for (var i = 0; i < 20; i++) {
        ordenes[i] = [];
    }
    var or = [];
    
    //debugger;
    for (var x = 0; x < ordenes.length; x++) {
        for (var y = 0; y < 31; y++) {            
            for (var i = 0; i < reporte.length; i++) {                                
                if (reporte[i].fechaDia - 1 == y) {
                    if (ordenes[x][y] == null) {
                        if (or.indexOf(reporte[i].ordenID) == -1) {

                            ordenes[x][y] = reporte[i].total;

                            or.push(reporte[i].ordenID);


                        }
                    }
                }
                
                
            }
        }
    }




    //poner los dias
    //debugger;
    if (fecha == 2) {
        if (fecha % 4 == 0) {
            dias = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29]
        } else {
            dias = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28]
        }

    } else if (fecha % 2 != 0) {
        dias = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31]
    } else {
        dias = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30]
    }


    var ctx = document.getElementById("barcharts").getContext("2d");
    var chart = new Chart(ctx, {
        type: 'bar',
        
        
        data: {
            
            labels: dias,
            for() {

            },
            datasets: [{   
                backgroundColor: "#ff0018", 
                
                
                data: ordenes[0]
            },
            {

                backgroundColor:"#f7b11b",
                
                data: ordenes[1]
            },
            {

                    backgroundColor: "#ff6c60",

                data: ordenes[2]

                //agegar 27 mas
            }]
            },
        options: {
            responsive:true,
            legend: { display: false },                        
            scales: {
                xAxes: [{
                    stacked:true
                }],
                yAxes: [{
                    stacked:true,
                    ticks: {
                        beginAtZero: true,     
                        
                    }
                }]
            }
            
        }
    });
    
   //window.alert(ordenes[1][16]);
  
    /*
    //Pie Chart
  new Chart(document.getElementById("pie-chart"), {
    type: 'pie',
    data: {
      labels: ["Hunger House", "Food Lounge", "Delizious", "Red Resturant", "Hunger Lounge"],
      datasets: [{
        label: "Population (millions)",
        backgroundColor: ["#ff0018", "#f7b11b","#ff6c60","#8663e1","#08bf6f"],
        data: [2478,5267,734,784,433]
      }]
    },
    options: {
      title: {
        display: true,
        text: 'Predicted Resturant Ratings (millions) in 2050'
      }
    }
  });
  */

/*
  // Bar chart
  var barChart = new Chart(document.getElementById("bar-chart"), {
      type: 'bar',
      data: {
        labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
        datasets: [
          {
            label: "Population (millions)",
            backgroundColor: ["#ff0018", "#f7b11b","#ff6c60","#8663e1","#08bf6f"],
            data: [2478,5267,1734,3384,1433]
          }
        ]
      },
      options: {
        legend: { display: false },
        title: {
          display: true,
          text: 'Predicted world population (millions) in 2050'
        }
      }
  });

  //Line Chart
  var ctx = document.getElementById('line-chart').getContext("2d");
  var gradientStroke = ctx.createLinearGradient(0, 0, 0, 450);
  gradientStroke.addColorStop(0, '#ff0018');

  var gradientFill = ctx.createLinearGradient(0, 0, 0, 450);
  gradientFill.addColorStop(0, "rgba(53,127,250,0.4)");
  gradientFill.addColorStop(1, "rgba(255,255,255,0)");

  // all data
  var data_1 = [1800, 1600, 2300, 2800, 3600, 2900, 3000, 3800, 3600];
  var data_2 = [4100, 3800, 3200, 3400, 2700, 2600, 3300, 3000, 2900];
  var labels = ["Jan-11", "Jan-12", "Jan-13", "Jan-14", "Jan-15", "Jan-16", "Jan-17","Jan-18", "Jan-19"];

  var lineChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: labels,
        datasets: [{
            label: "Data",
            borderColor: gradientStroke,
            pointBorderColor: gradientStroke,
            pointBackgroundColor: gradientStroke,
            pointHoverBackgroundColor: gradientStroke,
            pointHoverBorderColor: gradientStroke,
            pointBorderWidth: 1,
            pointHoverRadius: 4,
            pointHoverBorderWidth: 1,
            pointRadius: 2,
            fill: true,
            backgroundColor: gradientFill,
            borderWidth: 1,
            data: data_1
        }]
    },
    options: {
    /*    legend: {
        display: false,
        position: "bottom"
        },
        scales: {
          yAxes: [{
            ticks: {
              fontColor: "rgba(0,0,0,0.5)",
              fontStyle: "bold",
              beginAtZero: true,
              maxTicksLimit: 200,
              padding: 20
            },
            gridLines: {
              drawTicks: false,
              display: false
            }

        }],
        xAxes: [{
            gridLines: {
              zeroLineColor: "transparent"
            },
            ticks: {
              padding: 20,
              fontColor: "rgba(0,0,0,0.5)",
              fontStyle: "bold"
            }
        }]
      }
    }
  });




  //Polar Chart
  new Chart(document.getElementById("polar-chart"), {
    type: 'polarArea',
    data: {
      labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
      datasets: [
        {
          label: "Population (millions)",
          backgroundColor: ["#ff0018", "#f7b11b","#ff6c60","#8663e1","#08bf6f"],
          data: [2478,5267,734,784,433]
        }
      ]
    },
    options: {
      title: {
        display: true,
        text: 'Predicted world population (millions) in 2050'
      }
    }
  });

  //Doughnut Chart
  new Chart(document.getElementById("doughnut-chart"), {
    type: 'doughnut',
    data: {
      labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
      datasets: [
        {
          label: "Population (millions)",
          backgroundColor: ["#ff0018", "#f7b11b","#ff6c60","#8663e1","#08bf6f"],
          data: [2478,5267,734,784,433]
        }
      ]
    },
    options: {
      title: {
        display: true,
    /*    text: 'Predicted world population (millions) in 2050'
      }
    }
  });

  //Group Bar Chart
  new Chart(document.getElementById("bar-chart-grouped"), {
    type: 'bar',
    data: {
      labels: ["1900", "1950", "1999", "2050"],
      datasets: [
        {
          label: "Africa",
          backgroundColor: "#3e95cd",
          data: [133,221,783,2478]
        }, {
          label: "Europe",
          backgroundColor: "#8e5ea2",
          data: [408,547,675,734]
        }
      ]
    },
    options: {
      title: {
        display: true,
        text: 'Population growth (millions)'
      }
    }
  });

  //Mixed Chart
  new Chart(document.getElementById("mixed-chart"), {
    type: 'bar',
    data: {
      labels: ["1900", "1950", "1999", "2050"],
      datasets: [{
          label: "Europe",
          type: "line",
          borderColor: "#8e5ea2",
          data: [408,547,675,734],
          fill: false
        }, {
          label: "Africa",
          type: "line",
          borderColor: "#3e95cd",
          data: [133,221,783,2478],
          fill: false
        }, {
          label: "Europe",
          type: "bar",
          backgroundColor: "#ff6c60",
          data: [408,547,675,734],
        }, {
          label: "Africa",
          type: "bar",
          backgroundColor: "#f7b11b",
          backgroundColorHover: "#3e95cd",
          data: [133,221,783,2478]
        }
      ]
    },
    options: {
      title: {
        display: true,
        text: 'Population growth (millions): Europe & Africa'
      },
      legend: { display: false }
    }
  });
  */
};

function piecharts(reporte) {
    'use strict';
    //destructor
    var button = document.getElementById("submitButton");
    submitButton.addEventListener("click", function () {
        chart.destroy();
    });

    //varaibles
    var producto = [];
    var cantidad = [];
    var ganancia =[];

    for (var x = 0; x < reporte.length; x++) {
        producto.push(reporte[x].producto);
        cantidad.push(reporte[x].ventatotal)
        ganancia.push(reporte[x].gananciatotal)
    }

    //cuadros
    new Chart(document.getElementById("pie-chart-m1"), {
        type: 'pie',
        data: {
            labels: producto,
            datasets: [{
                label: "Population (millions)",
                backgroundColor: ["#ff0018", "#f7b11b", "#ff6c60", "#8663e1", "#08bf6f"],
                data: cantidad
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Predicted Resturant Ratings (millions) in 2050'
            }
        }
    });

    new Chart(document.getElementById("pie-chart-m2"), {
        type: 'pie',
        data: {
            labels: producto,
            datasets: [{
                label: "Population (millions)",
                backgroundColor: ["#ff0018", "#f7b11b", "#ff6c60", "#8663e1", "#08bf6f"],
                data: ganancia
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Predicted Resturant Ratings (millions) in 2050'
            }
        }
    });
};

function piechartsadmin(reporte) {
    'use strict';
    //destructor
    var button = document.getElementById("submitButton");
    submitButton.addEventListener("click", function () {
        chart.destroy();
    });

    //varaibles
    var nombre = [];    
    var ganancia = [];

    for (var x = 0; x < reporte.length; x++) {
        nombre.push(reporte[x].nombre);        
        ganancia.push(reporte[x].gananciatotal)
    }

    //cuadros
    new Chart(document.getElementById("pie-chart-adm1"), {
        type: 'pie',
        data: {
            labels: nombre,
            datasets: [{
                label: "Population (millions)",
                backgroundColor: ["#ff0018", "#f7b11b", "#ff6c60", "#8663e1", "#08bf6f"],
                data: ganancia
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Predicted Resturant Ratings (millions) in 2050'
            }
        }
    });
    
};
