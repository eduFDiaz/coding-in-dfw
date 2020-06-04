using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Comments;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Models.Messages;
using coding.API.Dtos;
using coding.API.Models.Reviews;
using coding.API.Dtos.Reviews;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private string Url = new string("http://localhost:4200/pages/review/referal?id=");

        private readonly Repository<Review> _reviewDal;

        public ReviewController(
            Repository<Review> reviewDal, IConfiguration config, IMapper mapper)
        {

            _reviewDal = reviewDal;
            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(Guid userid, string email)
        {
            var reviewToCreate = new Review();

            reviewToCreate.Name = "";
            reviewToCreate.Email = email;
            reviewToCreate.Body = "";
            reviewToCreate.Company = "";
            reviewToCreate.UserId = userid;
            reviewToCreate.Status = "draft";

            var createdReview = await _reviewDal.Add(reviewToCreate);

            createdReview.Url = Url + reviewToCreate.Id;

            if (await _reviewDal.SaveAll())
              return Ok(new ReviewPresenter(createdReview));

            return BadRequest("Cant create the review");

        }


        [HttpDelete("{reviewid}/delete", Name = "Delete Review")]
        public async Task<IActionResult> DeleteMessage(Guid reviewid)
        {
            var rw = (await _reviewDal.GetById(reviewid));

            if (rw == null)
                return NotFound();

            if (await _reviewDal.Delete(rw))
                return NoContent();

            return BadRequest("Cant delete the review!");

        }

        [HttpPut("{reviewid}/update", Name = "Update Review")]
        public async Task<IActionResult> UpdateMessage(Guid reviewid, [FromBody] CreateReviewDto request)
        {
            var reviewToUpdate = (await _reviewDal.GetById(reviewid));

            if (reviewToUpdate == null)
                return NotFound();

            reviewToUpdate.Body = request.Body;
            reviewToUpdate.Name = request.Name;
            reviewToUpdate.Email = request.Email;
            reviewToUpdate.Company = request.Company;
            reviewToUpdate.Status = "created";

            if (await _reviewDal.Update(reviewToUpdate))
                return Ok(new ReviewPresenter(reviewToUpdate));

            return BadRequest("cant update the message!");

        }

        [HttpGet("foruser/{userid}", Name = "Return all reviews for given user")]
        public async Task<IActionResult> GetAllMessages(Guid userid)
        {
            var reviews = (await _reviewDal.ListAsync()).Where(rw => rw.UserId == userid)
            .ToList();

            return Ok(reviews);
        }

        [HttpGet("{userid}/status/{status}")]
        public async Task<IActionResult> GetByStatus(Guid userid, string status)
        {
          List<ReviewPresenter> presentedReviews = new List<ReviewPresenter>();

          var reviews = (await _reviewDal.ListAsync()).Where(rw => rw.UserId ==
           userid && rw.Status == status).ToList();

           foreach (var review in reviews) {
             presentedReviews.Add(new ReviewPresenter(review));
           }

           return Ok(presentedReviews);
        }

        [HttpPut("publish/{reviewid}")]
        public async Task<IActionResult> PublishReview(Guid reviewid)
        {
          var toPublish = (await _reviewDal.GetById(reviewid));

          toPublish.Status = "published";

          if (await _reviewDal.Update(toPublish))
              return NoContent();

          return BadRequest("Cant publish the review");
        }


    }
}
