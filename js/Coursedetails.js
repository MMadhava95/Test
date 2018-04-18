window.onload = function () {
    //alert();
    //$('#wait').show();
    $('#status').delay(750).fadeOut(); // will first fade out the loading animation 
    $('#preloader').delay(750).fadeOut('slow'); // will fade out the white DIV that covers the website. 
    $('body').delay(750).css({ 'overflow': 'visible' });

    var baseUrl = (window.location).href;
    var koopId = baseUrl.substring(baseUrl.lastIndexOf('=') + 1);    
     //  var obj = { Id: koopId }
    $.ajax({
        type: "POST",
        url: "../CourseDetailsWebservice.asmx/Getcourse",
        //data:{},

        data: "{'id':'" + koopId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var arr = JSON.parse(r.d);

            //console.log(arr);

            for (var i = 0; i < arr.Table.length; i++) {
                var html = $('<div class="">' +

                    '<div class="col-md-8  ">' +

                    '<div class="panel panel-primary">' +
                    '<div class="panel-heading">' +
                     '<h3 class="panel-title">' + arr.Table[i].CourseName + '  Description</h3>' +
                    '</div>' +
                    '<div class="panel-body">' +

                    '<h4>Description</h4>' +
                    '<p>' + arr.Table[i].Coursedescription + '</p > ' +

                    '<h4>What Will I Learn? </h4>' +
                    '<ul>' +
                    '<li>' + arr.Table[i].Learncontent + '</li>' +

                    '</ul>' +

                    '<h4>Prerequisites</h4>' +
                    '<ul>' +
                    '<li>' + arr.Table[i].Prerequisites + '</li>' +
                    '</ul>' +
                    "<a  class='btn btn-primary small' onclick='sendData(\"" + arr.Table[i].CourseName + "\")'>Add to My Courses</a>" +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    ' </div>');

                $("#fulldetails").append(html);


            }
        },
        error: function (r) {
            alert(r.responseText);
        },
        failure: function (r) {
            alert(r.responseText);
        }
    });

    $.ajax({
        type: "POST",
        url: "../CourseDetailsWebservice.asmx/videosdata",
        //data:{},

        data: "{'id':'" + koopId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var arr = JSON.parse(r.d);

            //console.log(arr);

            for (var i = 0; i < arr.Table.length; i++) {
               
                     var html = $("<div class='col-md-1' id='get1'>" + "<div class='item item-left'>" +
                    "<a href='../Operations/coursedetails.aspx?id=" + arr.Table[i].CourseID + "'>" +
                    '<video width="100%" controls controlsList="nodownload" id="vid_frame"><source src=' + arr.Table[i].VideoPath + ' type="video/mp4"></video>' +
                    //'<img src=' + arr.Table[i].ImagePath + ' />' +
                    "<a href='../Operations/coursedetails.aspx?id=" + arr.Table[i].CourseID + "'><h5 style='color:red;'>" + arr.Table[i].VideoTitle + "</h5></a>" +
                    "</div>" +
                    "</div>");
                
                $("#VideoBind").append(html);


            }
        },
        error: function (r) {
            alert(r.responseText);
        },
        failure: function (r) {
            alert(r.responseText);
        }
    });


}