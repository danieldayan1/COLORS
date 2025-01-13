$(document).ready(function () {
    loadColors();
    
     // Load colors
    function loadColors() {
        $.ajax({
            url: 'ManageColors.aspx/GetColors',
            method: 'POST',
            contentType: 'application/json',
            success: function (response) {
                var data = response.d;
                var tbody = $("#colorTable tbody");
                tbody.empty();
                $.each(data, function (index, item) {
                    var row = `<tr>
                        <td>${item.ColorName}</td>
                        <td>${item.ColorDisplayOrder}</td>
                        <td>${item.ColorPrice}</td>
                        <td>${item.ColorInStock ? "V" : "X"}</td>
                        <td>
                            <button onclick="initFields('${item.ColorName}', ${item.ColorPrice}, ${item.ColorDisplayOrder}, ${item.ColorInStock})">+</button>
                            <button onclick="deleteColor(${item.ColorID})">Delete</button>
                        </td>
                    </tr>`;
                    tbody.append(row);
                });
            }
        });
    }


    // Add color
    $("#btnAdd").click(function () {
        var color = {
            ColorName: $("#Name").val(),
            ColorDisplayOrder: $("#DisplayOrder").val(),
            ColorPrice: $("#Price").val(),
            ColorInStock: $("#InStock").is(":checked")
        };
        $.ajax({
            url: 'ManageColors.aspx/AddColor',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ color: color }),
            success: function () {
                loadColors();
                resetFields();
            }
        });
    });


    // Delete color
    window.deleteColor = function (id) {
        $.ajax({
            url: 'ManageColors.aspx/DeleteColor',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ id: id }),
            success: function () {
                loadColors();
            }
        });
    };
            
            
     // Update color
    $("#btnUpdate").click(function () {
        var color = {
            ColorID: $("ColorID").val(),
            ColorName: $("ColorName").val(),
            Price: $("ColorPrice").val(),
            DisplayOrder: $("#ColorDisplayOrder").val(),
            InStock: $("#ColorInStock").is(":checked")
        };
        $.ajax({
            url: 'ManageColors.aspx/UpdateColor',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ color: color }),
            success: function () {
                loadColors();
                resetFields();
            }
        });
    });
    
    
  
    // initial fields on screen with chosen color
    window.initFields = function (name, price, order, stock) {
        $("#Name").val(name);
        $("#DisplayOrder").val(price);
        $("#Price").val(order);
        $("#InStock").prop('checked', stock);
    };
    
    
    //reset fields on screen
    function resetFields() {
        $("#Name").val('');
        $("#DisplayOrder").val('');
        $("#Price").val('');
        $("#InStock").prop('checked', false);
    }
});