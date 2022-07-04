using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProfileProject.Controllers;
using ProfileProject.Data;
using ProfileProject.Models;
using ProfileProject.Services;

namespace ProfileProject.UnitTest.Systems.Controllers;

public class TestProfileController
{
    [Fact]
    public void GetOnSuccess_ReturnStatusCode200()
    {
        //Arrange
        //var sut = new ProfileController()
        
        //Act
        
        //Assert

    }
    [Fact]
     public void Get_OnSuccess_InvokesProfileService()
     {
         var mockProfileService = new Mock<IProfile>();
         var dbContext = new Mock<IProfileDbContext>();
         var mockProfile = new Mock<Profile>();
         // Func<IEnumerable<Profile>> expected = Enumerable.Empty<Profile>;
         // var sut = new ProfileController(dbContext.Object,mockProfileService.Object);
         // var su = new Mock<ProfileController>(dbContext.Object, mockProfileService.Object);
         // su.Setup(m => m.GetProfile()) .Returns<List<Profile>>(ids =>
         // {
         //     return expected.Where(user => ids.Contains(user.Id)).ToList();
         // });
         // //Func<IEnumerable<Profile>>  = GetSampleEmployee();
         // IEnumerable<Profile> expectedq = GetSampleEmployee();
         // su.Setup(m => m.GetProfile()) .Returns<IEnumerable<Profile>>(expectedq);
         
         
         //mockContext.Setup(m => m.Blogs).Returns(mockSet.Object);
         //Act
         //var result = sut.GetProfile();
         //Assert.NotEmpty(result);
         
     }

     private IEnumerable<Profile> GetSampleEmployee()
     {
         List<Profile> profiles = new List<Profile>()
         {
            new Profile
            {
                name = "Thabang",
                surname = "soulo",
                about = "Im good",
                currentOccupation = "intern",
                email = "em@mdfg",
                cell = 54543,
                
            }
         };
         return profiles;
     }
} 