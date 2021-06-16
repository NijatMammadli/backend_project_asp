let subInput;

$(document).on("click", "#button-subscribe", function () {
    $("#response-subscribe").empty()
    subInput = $("#Email-subscirbe").val()
    if (subInput.length > 1) {
        $.ajax({
            type: "Get",
            url: "Home/Subscribe",
            data: {
                "email": subInput
            },
            success: function (res) {
                $("#response-subscribe").append(res)
               
            }

        })
    }
  
})
let searchInput;
$(document).on("keyup", "#search-Course", function () {
    searchInput = $(this).val().trim();
    $("#New-Course").empty();
    if (searchInput.length > 1) {
        $("#Old-Course").css("display", "none")
        $.ajax({
            type: "Get",
            url: "Course/Search",
            data: {
                "search": searchInput
            },
            success: function (res) {

                $("#New-Course").append(res)

            }

        })
    } else {
        $("#Old-Course").css("display", "block")

    }
})