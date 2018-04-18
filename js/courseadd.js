
window.onload = function () {
    //alert();
    //$('#wait').show();
    $('#status').delay(750).fadeOut(); // will first fade out the loading animation 
    $('#preloader').delay(750).fadeOut('slow'); // will fade out the white DIV that covers the website. 
    $('body').delay(750).css({ 'overflow': 'visible' });
    $.ajax({
        type: "POST",
        url: "../../WebService2.asmx/HelloWorld",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var arr = JSON.parse(r.d);
            var id = "fb";
            var sender = "Submit";

            for (var i = 0; i < arr.Table.length; i++) {
                
                var html = $("<div class='col-md-3' id='get1'>" + "<div class='item item-left'>" +
                    "<a href='../employee/coursedetails.aspx?id=" + arr.Table[i].CourseID + "'>" +
                    '<img src=' + arr.Table[i].ImagePath + ' />' +
                    "<a href='../employee/coursedetails.aspx?id=" + arr.Table[i].CourseID + "'><h5 style='color:red;'>" + arr.Table[i].CourseName + "</h5></a>" +
                    '<div>Duration: &nbsp' + arr.Table[i].CourseDuration + '</div>' +
                    '<div>Faculty : &nbsp' + arr.Table[i].Faculty + '</div>' + 
                    "<a class='btn btn-primary' href='../employee/coursedetails.aspx?id=" + arr.Table[i].CourseID + "'>View</a>"+
                    "</div>" +
                    "</div>" +
                    "</div>" +
                    "</div>");
                if (arr.Table[i].CourseType == 'Business') {
                    $("#getResutrants").append(html);
                }
                else if (arr.Table[i].CourseType == 'Technical')
                {
                    $("#getResutrants1").append(html);
                }
                else {
                    $("#getResutrants2").append(html);
                }
            }
        },
        error: function (r) {
            console.log(r);
        },
        failure: function (r) {
            console.log(time);
        }
    })
};


function sendData(cousrname) {

    var user = $("#getuser").val();


    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../WebService2.asmx/AddCourse",
        data: "{'name':'" + cousrname + "','user':'" + user + "'}",
        dataType: "json",
        success: function (data) {
            var data1 = data.d;
            if (data1 == "0") {
               alert("Already added to MyCourses. Check MyCourses to access the Course");
            } else if (data1 == "1") {
                alert("'" + cousrname + "' Course Registration Successful! Check MyCourses to access the Course");
               
                window.location.href = 'home.aspx';
            }

        },
        error: function (result) {
            alert("Error......");
        }
    });



}

