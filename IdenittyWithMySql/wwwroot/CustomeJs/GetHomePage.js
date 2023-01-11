



$(document).ready(function () {




    $.ajax({
        url: '/api/LandPage/GETFAQ',
        type: 'GET',
        dataType: 'json',
        success: function (datafaq, textStatus, xhr) {
            if (datafaq != null) {

                datafaq.forEach((item, index) => {


                    $('#faq-list').append(` <li>
                            <a data-toggle="collapse" class="collapsed" href="#faq${item.id}">${item.question} <i class="fa fa-minus-circle"></i></a>
                            <div id="faq${item.id}" class="collapse" data-parent="#faq-list">
                                <p>
${item.answer}
                                </p>
                            </div>
                        </li>`);

                });






            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });


    $.ajax({
        url: '/api/LandPage/GetConfInfo',
        type: 'GET',
        dataType: 'json',
        success: function (dataConfInfo, textStatus, xhr) {
            if (dataConfInfo != null) {
                $('#ptitle').html(dataConfInfo[0].headerTitle);
                //const d = new Date(dataConfInfo[0].startConfrenceDate);
                //const d2 = new Date(dataConfInfo[0].endConfrenceDate);

                //$('#location').html(d.getDay() + "-" + d2.getDay()+ " " +dataConfInfo[0].location);
                $('#location').html(dataConfInfo[0].location);
                dataConfInfo.forEach((item, index) => {

                    $('#logo').append(`<a href="/#intro" class="scrollto">
                    <img src="${item.logo}" alt="${item.name}" title="${item.name}">
                    </a>`);
                    if (item.viewBuyTicket == true)
                        $('#buyticket').append(` <a href="/Booking/index"> book your seat</a>`);



                    $("#youtubevid").attr("href", "https://www.youtube.com/watch?v=" + item.youtubeLink);


                    $('#infofooter').append(`<img src="${item.logo}" alt="${item.name}">
            <p>${item.name}.</p>`);


                    $('#compinfo').append(`  ${item.address}    <br />

              <strong>Phone:</strong>   ${item.phoneNumber}<br>
              <strong>Email:</strong>  ${item.email}<br> `);


                    $('#socialinfo').append(`     <a href="http://${item.twitterLink}" target="_blank" class="twitter">
<i class="fa fa-twitter"></i></a>
              <a href="http://${item.facebookLink}" target="_blank" class="facebook"><i class="fa fa-facebook"></i></a>
              <a href="http://${item.instegramLink}" target="_blank" class="instagram"><i class="fa fa-instagram"></i></a>
              <a href="http://${item.googleLink}" target="_blank" class="google-plus"><i class="fa fa-google-plus"></i></a>
              <a href="http://${item.linkedInLink}" target="_blank" class="linkedin"><i class="fa fa-linkedin"></i></a>`);


                    $('#coninfo').append(`  <div class="col-md-4">
            <div class="contact-address">
              <i class="ion-ios-location-outline"></i>
              <h3>Address</h3>
              <address> ${item.address} </address>
            </div>
          </div>

          <div class="col-md-4">
            <div class="contact-phone">
              <i class="ion-ios-telephone-outline"></i>
              <h3>Phone Number</h3>
              <p><a href=""> ${item.phoneNumber}</a></p>
            </div>
          </div>

          <div class="col-md-4">
            <div class="contact-email">
              <i class="ion-ios-email-outline"></i>
              <h3>Email</h3>
              <p><a href="mailto:${item.email}">${item.email}</a></p>
            </div>
          </div>`);








                })

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });

    $.ajax({
        url: '/api/LandPage/GetAboutUs',
        type: 'GET',
        dataType: 'json',
        success: function (data, textStatus, xhr) {
            if (data != null) {





                data.forEach((item, index) => {

                    if (index == 0) {
                        let imgpath = item.photo == "" ? "" : item.photo;
                        $('#row1').append(`  
          <div class="col-lg-1"></div>
          <div class="col-lg-1">
            <img src="${imgpath}" alt="" width="80px" style="border-radius:5050px">
          </div>
          <div class="col-lg-8">
            <h2>${item.name}</h2>
            <p> ${item.description}</p>
          </div>
          <div class="col-lg-1"></div>
          <br/><br/>
        </div>
        <hr>`);



                    }
                    else {

                        let imgpath = item.photo == "" ? "" : item.photo;
                        $('#repabout').append(' <div class="col-lg-1"><img src="' + imgpath + '"   alt="" width="80px" style="border-radius:5050px"></div>  <div class="col-lg-5"> <h2 id="message">' + item.name + '</h2>  <p id="messagedesc"> ' + item.description + '  </p> </div>  ');

                    }
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });

    $.ajax({
        url: '/api/LandPage/GetSpeaker',
        type: 'GET',
        dataType: 'json',
        success: function (dataspeaker, textStatus, xhr) {
            if (dataspeaker != null) {

                dataspeaker.forEach((item, index) => {

                    let imgpath = item.speakerPhoto == "" ? "" : item.speakerPhoto;
                    $('#rowspeakercontiner').append(`<div class="col-lg-4 col-md-6">
                            <div class="speaker">
                                <img src=${imgpath} alt="Speaker 1" class="img-fluid">
                                    <div class="details">
                                        <h3><a href="Speakers/Spekersdetails/${item.id}">${item.speakerName}</a></h3>
                                                                           <p>${item.speakerPosition == null ? "" : item.speakerPosition}</p>
                                        <div class="social">
                                            <a href="http://${item.twitterAccount}" target="_blank"><i class="fa fa-twitter"></i></a>
                                            <a href="http://${item.facebookAccount}" target="_blank"><i class="fa fa-facebook"></i></a>
                                            <a href="http://${item.googleAccount}" target="_blank"><i class="fa fa-google-plus"></i></a>
                                            <a href="http://${item.linkedInAccount}" target="_blank"><i class="fa fa-linkedin"></i></a>
                                        </div>
                                    </div>
                            </div>
                        </div>


`);

                });

                $('#rowspeakercontiner').append("<a href='/Speakers/Index' class='about - btn scrollto' style='display: block ; margin: 0 auto; '>See all spekers</a>");

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });









    $.ajax({
        url: '/api/LandPage/GetSponsr',
        type: 'GET',
        dataType: 'json',
        success: function (datasponser, textStatus, xhr) {
            if (datasponser != null) {


                datasponser.forEach((item, index) => {


                    $('#divSponsors').append(`<div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="${item.sponsorPhoto}" class="img-fluid" alt="${item.sponsorName}">
            </div>
          </div>`);

                });






            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });



    $.ajax({
        url: '/api/LandPage/GetCategoryAttr',
        type: 'GET',
        dataType: 'json',
        success: function (dataCategory, textStatus, xhr) {
            if (dataCategory != null) {


                for (let i = 0; i < dataCategory.length; i++) {
                    if (i == 0) {

                        $('#categoryi').append(` <li class="nav-item">
                          <a class="nav-link active hrefclass" href="${dataCategory[i].id}" role="tab" data-toggle="tab">${dataCategory[i].categoryTitle}</a>
                         </li> `);
                        let HTML_DIV = `<div role="tabpanel" class="col-lg-9 tab-pane fade divactive show active" id="${dataCategory[i].id}">`;
                        for (let det = 0; det < dataCategory[i].lstevent.length; det++) {
                            const dt = new Date(dataCategory[i].lstevent[det].time);
                            const dtto = new Date(dataCategory[i].lstevent[det].timeTo);

                            debugger;
                            HTML_DIV = HTML_DIV + (`<div class="row schedule-item">
              <div class="col-md-2"><time>${(dt.getHours() == '0' ? '12' : dt.getHours()) + ":" + (dt.getMinutes() == '0' ? '00' : dt.getMinutes()) + " - " + (dtto.getHours() == '0' ? '12' : dtto.getHours()) + ":" + (dtto.getMinutes() == '0' ? '00' : dtto.getMinutes()) }</time></div>
              <div class="col-md-10">
                ${dataCategory[i].lstevent[det].note}
            
            
  </div></div>`);
                        }

                        HTML_DIV = HTML_DIV + "</div>";
                        $('#categdet').append(HTML_DIV);

                    } else {

                        $('#categoryi').append(` <li class="nav-item">
                          <a class="nav-link hrefclass" href="${dataCategory[i].id}" role="tab" data-toggle="tab">${dataCategory[i].categoryTitle}</a>
                         </li> `);
                        for (let det = 1; det < dataCategory[i].lstevent.length; det++) {
                            let HTML_DIV = `<div role="tabpanel" class="col-lg-9 tab-pane fade divactive  " id="${dataCategory[i].id}">`;

                            for (let det = 0; det < dataCategory[i].lstevent.length; det++) {
                                const dt = new Date(dataCategory[i].lstevent[det].time);
                                const dtto = new Date(dataCategory[i].lstevent[det].timeTo);

                                //  let Gtime = formatAMPM(dataCategory[i].lstevent[det].time);
                                debugger;
                                HTML_DIV = HTML_DIV + (`<div class="row schedule-item">
              <div class="col-md-2"><time>${(dt.getHours() == '0' ? '12' : dt.getHours()) + ":" + (dt.getMinutes() == '0' ? '00' : dt.getMinutes()) + " - " + (dtto.getHours() == '0' ? '12' : dtto.getHours()) + ":" + (dtto.getMinutes() == '0' ? '00' : dtto.getMinutes())
                                    }</time></div>
              <div class="col-md-10">
                ${dataCategory[i].lstevent[det].note}
            
            
  </div></div>`);
                            }

                            HTML_DIV = HTML_DIV + "</div>";
                            $('#categdet').append(HTML_DIV);

                        }

                    }
                }


                $('.hrefclass').click(function () {
                    debugger;
                    let Id = $(this).attr('href');
                    $(".divactive").removeClass("show active");
                    if ($("#" + Id)) {
                        $("#" + Id).addClass("show active");
                    }

                });




            }

        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });





    $.ajax({
        url: '/api/LandPage/GetGallery',
        type: 'GET',
        dataType: 'json',
        success: function (dataGallery, textStatus, xhr) {
            if (dataGallery != null) {

                dataGallery.forEach((item, index) => {

                    $("#galleryrow").append(`<div class="col-lg-3 col-md-4 col-xs-6 thumb">
                        <a class="thumbnail" href="#" data-image-id="" data-toggle="modal" data-title="${item.gallaryName}"
                           data-image="${item.gallaryPhoto}"
                           data-target="#image-gallery">
                            <img class="img-thumbnail"
                                 src="${item.gallaryPhoto}"
                                 alt="${item.gallaryName}">
                        </a>
                    </div>

`)

                })


            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });

    $.ajax({
        url: '/api/LandPage/GetAboutEvent',
        type: 'GET',
        dataType: 'json',
        success: function (dataAboutEvent, textStatus, xhr) {
            if (dataAboutEvent != null) {



                $("#aboutevent").html(dataAboutEvent.details);




            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });


});
function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    console.log(date.getHours());
    minutes = minutes < 10 ? '0' + minutes : minutes;
    console.log(date.getMinutes());
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}


