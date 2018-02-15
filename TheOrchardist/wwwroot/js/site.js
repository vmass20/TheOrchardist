

    function FillCity() {
            var stateId = $('#State').val();
            $.ajax({
      url: '/sortOrder/FillCity',
                type: "GET",
                dataType: "JSON",
                data: {State: stateId },
                success: function (cities) {
      $("#City").html(""); // clear before appending new list
    $.each(cities, function (i, city) {
      $("#City").append(
        $('<option></option>').val(city.CityId).html(city.CityName));
    });
                }
            });
        }
