$(document).ready(function () {


    var width = $(window).width();
    var height = $(window).height();
    console.log(width);
    console.log(height);




    var LiveState = true;
    if (LiveState)
        $("#LiveTxt").addClass("live");
    else
        $("#LiveTxt").removeClass("live");


    $("#ViewMode").click(function () {
        $("#ViewOptions").fadeToggle("slow");
    });

    $("#NoneEqualView").click(function () {
        $(".main-container .none-equal").show();
        $(".main-container .equal").hide();
        $("#ViewMode").trigger("click");
    })

    $("#EqualView").click(function () {
        $(".main-container .none-equal").hide();
        $(".main-container .equal").show();
        $("#ViewMode").trigger("click");
    })


    let participantsCount = 1;
    switch (participantsCount) {
        case 1:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.equal").append("<div class=\"others col-lg-12 col-md-12 col-sm-12\"><div id=\"participant" + i + "\" class=\"other single\"></div></div>");
            }
            break;
        case 2:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.equal").append("<div class=\"others col-lg-6 col-md-6 col-sm-6\"><div id=\"participant" + i + "\" class=\"other\"></div></div>");                
            }
            break;
        case 3:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.equal").append("<div class=\"others col-lg-4 col-md-4 col-6\"><div id=\"participant"+i+"\" class=\"other\"></div></div>");                
            }
            break;
        case 4:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.equal").append("<div class=\"others col-lg-3 col-md-6 col-6\"><div id=\"participant"+i+"\" class=\"other\"></div></div>");
            }
            break;
        case 5:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.equal").append("<div class=\"others col-lg-3 col-md-4 col-6\"><div id=\"participant"+i+"\" class=\"other\"></div></div>");
            }
            break;
        case 6:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.equal").append("<div class=\"others1 col-lg-4 col-md-4 col-6\"><div id=\"participant"+i+"\" class=\"other multiple\"></div></div>");
            }
            break;
        default:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.equal").append("<div class=\"others1 col-lg-3 col-md-4 col-6\"><div id=\"participant"+i+"\" class=\"other multiple\"></div></div>");
            }
    }

    switch (participantsCount) {
        case 1:
            for (let i = 0; i < participantsCount; i++) {
                $(".stream-container.none-equal").append("<div class=\"others col-lg-12 col-md-12 col-sm-12\"><div id=\"participant"+i+"\" class=\"active single\"></div></div>");
            }
            break;    
        default:
            $(".stream-container.none-equal").append("<div class=\"others col-lg-9 col-md-9 col-sm-9\"><div id=\"participant0\" class=\"active\"></div></div>");
            $(".stream-container.none-equal").append("<div class=\"others group col-lg-3 col-md-3 col-sm-3\"><div class=\"group-row row\"></div></div>");
            for (let i = 1; i < participantsCount; i++) {
                $(".group .row").append("<div id=\"participant" + i + "\" class=\"other col-lg-3 col-md-12 col-sm-12 col-3\"></div>");
            }
    }

});
