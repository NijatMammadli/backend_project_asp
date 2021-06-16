let subInput;

$(document).on("click", "#button-subscribe", function () {
    $("#response-subscribe").empty()
    subInput = $("#Email-subscirbe").val()
    if (subInput.length > 1) {
        $.ajax({
            type: "Get",
            url: "/Home/Subscribe",
            data: {
                "email": subInput
            },
            success: function (res) {
                $("#response-subscribe").append(res)
               
            }

        })
    }
  
})
let searchInputCourse;
$(document).on("keyup", "#search-Course", function () {
    searchInputCourse = $(this).val().trim();
    $("#New-Course").empty();
    if (searchInputCourse.length > 1) {
        $("#Old-Course").css("display", "none")
       

        $.ajax({
            type: "Get",
            url: "Course/Search",
            data: {
                "search": searchInputCourse
            },
            success: function (res) {

                $("#New-Course").append(res)

            }

        })
    } else {
        $("#Old-Course").css("display", "block")

    }
})
let searchInpuBlog;
$(document).on("keyup", "#search-Blog", function () {
    searchInpuBlog = $(this).val().trim();
    $("#New-Blog").empty();
    if (searchInpuBlog.length > 1) {
        $("#Old-Blog").css("display", "none")
        $.ajax({
            type: "Get",
            url: "Blog/Search",
            data: {
                "search": searchInpuBlog
            },
            success: function (res) {

                $("#New-Blog").append(res)

            }

        })
    } else {
        $("#Old-Blog").css("display", "block")

    }
})
let searchInpuEvent;
$(document).on("keyup", "#search-Event", function () {
    searchInpuEvent = $(this).val().trim();
    $("#New-Event").empty();
    if (searchInpuEvent.length > 1) {
        $("#Old-Event").css("display", "none")
        $("#EventPagination").css("display", "none")
        $.ajax({
            type: "Get",
            url: "Event/Search",
            data: {
                "search": searchInpuEvent
            },
            success: function (res) {

                $("#New-Event").append(res)

            }

        })
    } else {
        $("#Old-Event").css("display", "block")
        $("#EventPagination").css("display", "block")


    }
})
let searchInpuTeacher;
$(document).on("keyup", "#search-Teacher", function () {
    searchInpuTeacher = $(this).val().trim();
    $("#New-Teacher").empty();
    if (searchInpuTeacher.length > 1) {
        $("#Old-Teacher").css("display", "none")
        $.ajax({
            type: "Get",
            url: "Teacher/Search",
            data: {
                "search": searchInpuTeacher
            },
            success: function (res) {

                $("#New-Teacher").append(res)

            }

        })
    } else {
        $("#Old-Teacher").css("display", "block")

    }
})
let searchInputGlobal
$(document).on("keyup", "#global-search", function () {
    searchInputGlobal = $(this).val().trim();
    $("#global-search-box #global-list").remove();
    if (searchInputGlobal.length > 1) {
        $.ajax({
            type: "Get",
            url: "/Home/GlobalSearch",
            data: {
                "search": searchInputGlobal
            },
            success: function (res) {

                $("#global-search-box").append(res)

            }

        })
    } else {
        $("#Old-Teacher").css("display", "block")

    }
})