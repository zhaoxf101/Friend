var timer = null;
var iInterval = 500;

$(function ()
{
    var ww = $(window).width();
    var wh = $(window).height();
    //$(".MainContent").height(wh-$(".MainContent iframe").offset().top);
    $(".MenuList").height(wh - $(".MenuList").offset().top - 93);
    var item_i = $("<i></i>");
    item_i.appendTo($('.MenuList li:has(ul)'));
    LIClick();
    $(".open_menu a").click(function ()
    {
        var awidth = $(".Menu").width();
        if (awidth == 159)
        {
            $(".Menu").animate({ "width": 51 }, 500);
            $(".MenuList").find("i").hide().removeClass("hover");
            $(".Content").animate({ "margin-left": 53 }, 500);
            $(this).addClass("on");
            $('.MenuList li').each(function ()
            {
                $(this).has("ul").children("a:first").unbind("click");
                $(this).removeClass("hover");
            });
            $('.MenuList li> ul').hide();
            LIHover();
        }
        else
        {
            $(".Menu").animate({ "width": 159 }, 500);
            $(".MenuList").find("i").show(500);
            $(".Content").animate({ "margin-left": 161 }, 500);
            $(this).removeClass("on");
            $('.MenuList li').unbind("mouseenter").unbind("mouseleave");
            LIClick();
        }
    });
})
function LIClick()
{
    $(".MenuList ul").each(function ()
    {
        $(this).find("li").has("ul").each(function ()
        {
            $(this).children("a:first").unbind("click").click(function (e)
            {
                var athis = $(this);
                var state = athis.next("ul").css("display");
                if (state == "none")
                {
                    athis.parent().addClass("hover");
                    athis.next("ul").slideDown(500);
                    athis.parent().find(' > i').addClass('hover');
                }
                else
                {
                    athis.parent().removeClass("hover");
                    athis.parent().find("a").removeClass("hover");
                    athis.parent().find("ul").slideUp(500);
                    athis.parent().find(' > i').removeClass('hover');
                }

            });
        });
    });

    timer = setInterval(farmeTopSize, iInterval);
}
function LIHover()
{

    $('.MenuList ul').each(function ()
    {
        $(this).find("li").hover(function ()
        {
            $(".MenuList").toggleClass("on");
            $(this).find(">ul").toggle();
            $(this).find(">ul").toggleClass("on");
            var aa = $(this).find("ul").size();
            if (aa > 1)
            {
                var ahref = $(this).find("ul:last").find("li:first a").attr("href");
                $(this).find("ul:last").prev().attr("href", ahref);
            }
        });
    });
    var iWidth = $("#indexLayout").width();    
    //$("#indexLayout").width();
    //$("#framecenter").width = $("#indexLayout").width();

    //$("#indexLayout").setWidth(500);
    timer = setInterval(farmeTopSize, iInterval);
}

function farmeTopSize()
{
    $(window).resize();
    clearInterval(timer);
}

$(".Helpselect").find("dl").hide();
$(".Helpselect").find(".txtval").click(function ()
{
    $(this).next("dl").show();
});
$(".Helpselect").find("dl").find("dt").each(function ()
{
    $(this).click(function ()
    {
        var aval = $(this).find("a").text();
        $(this).parent().prev().text(aval);
        $(this).parent().hide();
    });
});

//菜单选择效果
var subId = GetQueryString("id");
function GetQueryString(name)
{
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
$("#sub" + subId).parents("ul").show();
$("#sub" + subId).parent().parent().addClass("hover");
$("#sub" + subId).find(">a").addClass("on");