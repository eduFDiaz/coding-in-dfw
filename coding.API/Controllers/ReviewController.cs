using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Comments;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using System.Net.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Models.Messages;
using coding.API.Dtos;
using coding.API.Models.Reviews;
using coding.API.Dtos.Reviews;
using System;
using coding.API.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private string url = "http://localhost:4200/pages/review/referal?id=";
        private readonly Repository<Review> _reviewDal;

        public ReviewController(
            Repository<Review> reviewDal, IConfiguration config, IMapper mapper)
        {

            _reviewDal = reviewDal;
            _config = config;
            _mapper = mapper;

        }

        [Authorize]        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DraftReviewDto request)
        {
            Review reviewToCreate = new Review();

            reviewToCreate.Name = "";
            reviewToCreate.Email = request.Email;
            reviewToCreate.Body = "";
            reviewToCreate.Company = request.Company;
            reviewToCreate.UserId = request.UserId;
            reviewToCreate.Status = "draft";

            Review createdReview = await _reviewDal.Add(reviewToCreate);

            createdReview.Url = url + reviewToCreate.Id;

            //Now send the email
            // EmailConfigurationDev emailConfigurationDev = new EmailConfigurationDev();
            EmailConfigurationProd emailConfigurationProd = new EmailConfigurationProd();
            MailSender mailSender = new MailSender(emailConfigurationProd);
            string msg = $@"Hello, please write a review in this url: <a href='{createdReview.Url}'> {createdReview.Url} </a> please don't share this url with anyone since this could be used to modify your review of our services.";
            MailTemplate template = new MailTemplate(createdReview.Email, msg);

            try
            {

                await mailSender.SendEmailAsync(template);
            }
            catch (Exception ex)
            {
                Exception exMsg = ex;
                return BadRequest(exMsg);

            }

            if (await _reviewDal.Update(createdReview))
                return Ok(new ReviewPresenter(createdReview));

            return BadRequest("Can't create the review");

        }

        [Authorize]
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
        public async Task<IActionResult> UpdateMessage(Guid reviewid, [FromBody] UpdateReviewDto request)
        {
            var reviewToUpdate = (await _reviewDal.GetById(reviewid));

            if (reviewToUpdate == null)
                return NotFound();

            reviewToUpdate.Body = request.Body;
            reviewToUpdate.Name = request.Name;
            reviewToUpdate.Status = "created";

            if (await _reviewDal.Update(reviewToUpdate))
                return Ok(new ReviewPresenter(reviewToUpdate));

            return BadRequest("cant update the message!");

        }

        [HttpGet("all", Name = "Return all reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = (await _reviewDal.ListAsync()).Where(rw => rw.Status == "published")
            .ToList().OrderByDescending(r => r.DateCreated).Take(10);

            return Ok(reviews);
        }

        [Authorize]
        [HttpGet("{userid}/status/{status}")]
        public async Task<IActionResult> GetByStatus(Guid userid, string status)
        {
            List<ReviewPresenter> presentedReviews = new List<ReviewPresenter>();

            var reviews = (await _reviewDal.ListAsync()).Where(rw => rw.UserId ==
             userid && rw.Status == status).ToList();

            foreach (var review in reviews)
            {
                presentedReviews.Add(new ReviewPresenter(review));
            }

            return Ok(presentedReviews);
        }

        [Authorize]
        [HttpGet("foruser/{userid}")]
        public async Task<IActionResult> GetReviewForUser(Guid userid)
        {
            List<ReviewPresenter> presentedReviews = new List<ReviewPresenter>();

            var reviews = (await _reviewDal.ListAsync()).Where(rw => rw.UserId ==
             userid).ToList();

            foreach (var review in reviews)
            {
                presentedReviews.Add(new ReviewPresenter(review));
            }

            return Ok(presentedReviews);
        }
        

        [Authorize]
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
