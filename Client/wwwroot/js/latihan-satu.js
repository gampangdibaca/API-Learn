//console.log("WOW");
//var a = 1;
//console.log(a);
//a = "test";
//console.log(a);

//// array 1 dimensi

//let array = [1, 2, 3, 4, "hehe"];
//console.log(array);

////insert array paling belakang
//array.push(true);
//console.log(array);

////delete array paling belakang
//array.pop();
//console.log(array);

////insert paling depan
//array.unshift("front");
//console.log(array);

////delete array paling depan
//array.shift();
//console.log(array);

//// array multidimensi
//let arrayMulti = ['a', 'b', 'c', [1, 2, 3]];
//console.log(arrayMulti);

////deklarasi objek
//let mhs = {
//    nama: "bimo",
//    nim: "20015",
//    jurusan: "TI",
//    umur: 22,
//    isActive: true,
//    hobby: ["Kebun", "Main"]
//};

//console.log(mhs);
//console.log(mhs.hobby[2]);

//const user = {};
//user.username = "rudi";
//user.password = "123";
//console.log(user);

//user.username = "pandi";
//console.log(user);
//console.log(user["password"]);

//const csv = "1|2|3";

//const [one, two, three] = csv.split("|");
//console.log(one);
//console.log(three);

//const animals = [
//    {
//        name : "Alex",
//        species : "cat",
//        class: {name: "mamalia"}
//    },
//    {
//        name: "Nemu",
//        species: "Turtle",
//        class: { name: "reptilia" }
//    },
//    {
//        name: "Acil",
//        species: "cat",
//        class: { name: "mamalia" }
//    },
//    {
//        name: "Found",
//        species: "Turtle",
//        class: { name: "reptilia" }
//    },
//    {
//        name: "Carlo",
//        species: "cat",
//        class: { name: "mamalia" }
//    }
//];

//console.log(animals);

//let onlyCat = [];

//animals.forEach((a) => {
//    if (a.species == "cat") {
//        onlyCat.push({ ...a });
//        return;
//    }
//    a.class.name = "not mamalia";
//});
//console.log(onlyCat);   
//console.log(animals);

//onlyCat[0].name = "udin";
//console.log(onlyCat);
//console.log(animals);

//$("h1").html("testing rubah dari jquery");

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    context: document.body
}).done((result) => {
    let text = "";
    //console.log(result.results);
    $.each(result.results, function (key, value) {
        //text += `<li>${value.name}</li>`;
        text += `<tr>
            <td>${key+1}</td>
            <td style="text-transform: capitalize;">${value.name}</td>
            <td><button type="button" onclick="detailPoke('${value.url}')" class="btn btn-primary" data-toggle="modal" data-target="#modalDetail">Pokemon Detail</button></td>
        </tr>`
    });
    $("#listPoke").html(text);
}).fail((error) => {
    console.log(error);
});


function detailPoke(urlPoke) {
    $.ajax({
        url: urlPoke,
        context: document.body
    }).done((result) => {
        $("#modalDetailTitle").html(result.name);
        $("#modalDetailTitle").css("text-transform", "capitalize");
        $("#pokeImage").attr("src", `${result.sprites.other.dream_world.front_default}`);
        $("#modalIcon").attr("src", `${result.sprites.front_default}`);
        $("#pokedexNo").html(result.id);
        $("#pokeHeight").html(`${result.height / 10} m`);
        $("#pokeWeight").html(`${result.weight / 10} kg`);
        let text = "";
        for (let i = 0; i < result.types.length; i++) {
            text += `<span style="padding: 6px 4px 4px 4px; margin: 0 2px; text-transform: uppercase;"class="badge poke-type type-${result.types[i].type.name}">${result.types[i].type.name}</span>`;
        };
        $("#typesContainer").html(text);
        var statData = [];
        for (let i = 0; i < 6; i++) {
            statData.push(result.stats[i].base_stat);
        }
        let data = {
            labels: ['HP', 'ATK', 'DEF', 'SP-ATK', 'SP-DEF', 'SPD'],
            datasets: [{    
                data: statData,
                fill: true,
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgb(255, 99, 132)',
                pointBackgroundColor: 'rgb(255, 99, 132)',
                pointBorderColor: '#fff',
                pointHoverBackgroundColor: '#fff',
                pointHoverBorderColor: 'rgb(255, 99, 132)'
            }]
        }

        const config = {
            type: 'radar',
            data: data,
            options: {
                elements: {
                    line: {
                        borderWidth: 3
                    }
                },
                plugins: {
                    legend: {
                        display: false
                    }
                },
                scales: {
                    r: {
                        angleLines: {
                            display: false
                        },
                        suggestedMin: 50,
                        suggestedMax: 100,
                        pointLabels: {
                            font: {
                                size: 12,
                                weight: "bold",
                                family: "'Pokemon', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif"
                            }
                        }
                    }
                }
            },
        };

        const myChart = new Chart(
            document.getElementById('pokeStats'),
            config
        );

        $('#modalDetail').on('hidden.bs.modal', function () {
            myChart.destroy();
        })
        
        let abilities = "";
        for (var i = 0; i < result.abilities.length; i++) {
            let effect = "";
            $.ajax({
                url: result.abilities[i].ability.url,
                context: document.body
            }).done((result_effect) => {
                for (let j = 0; j < result_effect.effect_entries.length; j++) {
                    
                    if (result_effect.effect_entries[j].language.name == "en") {
                        effect = result_effect.effect_entries[j].effect;
                        abilities += `<div class="row mx-4"><div class="col"><a tabindex="0" class="btn btn-lg btn-primary" style="text-transform: capitalize;" role="button" data-toggle="popover" data-trigger="focus" title="Effect" data-content="${effect}">${result_effect.name}</a></div></div>`;
                    }
                }
                $("#nav-ability").html(abilities);
                $('[data-toggle="popover"]').popover();
                $('.popover-dismiss').popover({
                    trigger: 'focus'
                })
            }).fail((error) => {
                console.log(error);
            });
        }

        let evolutionChain = [];

        $.ajax({
            url: result.species.url,
            context: document.body
        }).done((result) => {
            $.ajax({
                url: result.evolution_chain.url,
                context: document.body
            }).done((result) => {
                let isMore = true;
                let chain = result.chain;
                evolutionChain.push(result.chain.species.name);
                while (isMore) {
                    for (var i = 0; i < chain.evolves_to.length; i++) {
                        evolutionChain.push(chain.evolves_to[i].species.name);
                        if (chain.evolves_to[i].evolves_to.length > 0) {
                            chain = chain.evolves_to[i];
                        } else {
                            isMore = false;
                        }
                    }
                }
                let evoIndicator = "";
                let evoImage = "";
                let active = "active";
                let idx = 0;
                for (var i = 0; i < evolutionChain.length; i++) {
                    $.ajax({
                        url: "https://pokeapi.co/api/v2/pokemon/" + evolutionChain[i],
                        context: document.body
                    }).done((poke) => {
                        if (idx > 0) {
                            active = "";
                        }
                        evoImage += `<div class="carousel-item ${active}"><img src="${poke.sprites.other.dream_world.front_default}" class="d-block w-100" style="height:200px; width:200px;" alt="..."></div>`;
                        evoIndicator += `<li data-target="#pokemonCarousel" data-slide-to="${idx}" class="${active}" style="background-color:#000;"></li>`;
                        idx++;
                        $("#carousel-inner").html(evoImage);
                        $("#pokemon-carousel-indicators").html(evoIndicator);
                    }).fail((error) => {
                        console.log(error);
                    });
                }
            }).fail((error) => {
                console.log(error);
            });
        }
        ).fail((error) => {
            console.log(error);
        });


        

    }).fail((error) => {
        console.log(error);
    });
}
