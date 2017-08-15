$(function(){
    resize();
    $(window).on('resize', resize);
    setRound();
})

function resize() {
    Scroll1();
}

function Scroll1() {
    try {
        $("#Scroll1").slimScroll({
            height: $(".main-content1").height(),
            alwaysVisible: false,
            color: "#38393d",
            railColor: "#525357",
            railOpacity: 1,
            railVisible: true,
            disableFadeOut: true
        });
    } catch (e) {

    }

}

function setRound() {
    $(".roundproces").each(function (index, el) {
        //debugger;
        var percent = Number($(el).data("percent"));
        //var store = Number($(el).data("store"));
        //debugger;
        //var round = $(el).data("radialIndicator");
        var round = radialIndicator(el, {
            barColor:"#37BEFD",
            barBgColor: "#316A7C",
            fontColor: "#fff",
            fontWeight: "normal",
            fontSize: "10px",
            radius: 20,
            maxValue: 100,
            barWidth: 7,
            initValue:percent,
            roundCorner: true,
            percentage: true

        });
        //$(el).radialIndicator()
        //round.animate(50);

    })

}