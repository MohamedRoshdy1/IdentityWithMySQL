$(document).ready(function () {

    $.ajax({
        url: '/api/Speakers/GetAllSpeaker',
        type: 'GET',
        dataType: 'json',
        success: function (dataSpeakers, textStatus, xhr) {
            if (dataSpeakers != null) {
                dataSpeakers.forEach((item, index) => {
                    let imgpath = item.speakerPhoto == "" ? "" : item.speakerPhoto;

                    $('#mainrow').append(`    <div class="col-lg-3 col-md-6">
                    <div class="speaker">
                                <img src=/${imgpath} alt="Speaker 1" class="img-fluid">
                        <div class="details">
                              <h3><a href="/Speakers/Spekersdetails/${item.id}">${item.speakerName}</a></h3>
                                        <p>${item.speakerPosition}</p>
                            <div class="social">
  <a href="http://${item.twitterAccount}" target="_blank"><i class="fa fa-twitter"></i></a>
                                            <a href="http://${item.facebookAccount}" target="_blank"><i class="fa fa-facebook"></i></a>
                                            <a href="http://${item.googleAccount}" target="_blank"><i class="fa fa-google-plus"></i></a>
                                            <a href="http://${item.linkedInAccount}" target="_blank"><i class="fa fa-linkedin"></i></a>
                                 </div>
                        </div>
                    </div>
                </div>`);
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');

        }
    });



});