using System;
using System.Linq;
using coding.API.Models.Awards;
using coding.API.Models.Users;
using coding.API.Tests.DataContextForTests;
using Xunit;

namespace coding.API.Tests.Models
{
    public class TestAwards
    {
       
        [Fact]
        public void TestAwardModelsInMemory()
        {
           //Arrange    
            var factory = new ConnectionFactory();  
  
            //Get the instance of DataContext  
            var context = factory.CreateContextForInMemory();  
                          
            var award = new Award() { Title = "Test Title 3", Company = "Test Company 3", DateCreated = DateTime.Now , Year = 2020 , UserId = new Guid()};  

            var user = new User() { Username = "TestUser" , Email = "user@domain.com" , FullName = "Test Subject" };
  
            //Act    
            var data = context.Awards.Add(award);
            var userData = context.Users.Add(user);
            
            
            context.SaveChanges();  
  
            //Assert    
            //Get the award count  
            var awardCount = context.Awards.Count();  
            if (awardCount != 0)  
            {  
                Assert.Equal(1, awardCount);  
            }  
  
            //Get single award detail  
            var singleAward = context.Awards.FirstOrDefault();  
            if (singleAward != null)  
            {  
                Assert.Equal("Test Title 3", singleAward.Title);  
            }  
        }
    }
}