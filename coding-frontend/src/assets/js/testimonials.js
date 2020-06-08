$(document).ready(function() {

	$.ajax({
        type: "GET",
        datatype: "json",
        url: "http://localhost:5050/api/review/08d8073e-7c82-9a7a-b280-01e1a1f353db/status/published",
        success: function(result) {
					// $('#demo').html('<div id="testing" class="owl-carousel"></div>');
            for(var i=0;i<result.length;i++){
                $(".owl-carousel").append('<div class="item"><div class="quote-holder"><blockquote class="quote-content" style="height: 50px">'+ result[i].body + '</blockquote><i class="fas fa-quote-left"></i></div><div class="source-holder"><div class="source-profile"><img src="assets/images/clients/profile-1.png" alt="image" /></div><div class="meta"><div class="name">'+ result[i].name + '</div><div class="info">' + result[i].company + '</div></div></div></div>');
            };
            var owl = $(".testimonial-carousel");
            owl.owlCarousel({
                margin:30,
							  nav:false,
							 	loop: false,
							 	rewind: true,
							  responsive:{
							        0:{
							             items:1,
							         },
							         768:{
							             items:2,
							         },
							         1200: {
							 	        items:3,
							         }
            } });

            // $('#test').html(result);
            // var owl = $(".testimonial-carousel");
            // owl.owlCarousel({
					  //       margin:30,
					  //       nav:false,
						// 			loop: false,
						// 			rewind: true,
					  //       responsive:{
						//         0:{
						//             items:1,
						//         },
						//         768:{
						//             items:2,
						//         },
						//         1200: {
						// 	        items:3,
						//         }
						//     });
        }
    });


	// $('.testimonial-carousel').owlCarousel({
  //       margin:30,
  //       nav:false,
	// 			loop: false,
	// 			rewind: true,
  //       responsive:{
	//         0:{
	//             items:1,
	//         },
	//         768:{
	//             items:2,
	//         },
	//         1200: {
	// 	        items:3,
	//         }
	//     }
	//
	// });


});
