using ProfileProject.Models;

namespace ProfileProject.Services;

public interface IProfile
{
    void GetAllUsers();
    Task<List<Profile>> GetAllProfiles();
    Task<Profile> GetProfileByName(string name);
    // Task<List<Profile>> GetProfileById(int id);
    
    //get data
    Task<Profile> GetProfileById(int id);
    //create data
    Users  CreateUser(Profile profile);
    Education CreateEducation(Profile profile);
    About CreateAbout(Profile profile);
    Experience CreateExperience(Profile profile);

    Projects CreateProject(Profile profile);
    Skills CreateSkills(Profile profile);
    //create objects
    Task<Profile> CreateProfile(Profile profile);
    Users CreateUserObject(Profile profile);
    Education CreateEducationObject(Profile profile);
    Experience CreateExperienceObject(Profile profile);
    Skills CreateSkillsObject(Profile profile);
    Projects CreateProjectObject(Profile profile);
    
    //update database
    Task<bool> UpdateProfile(Profile profile);
    public int GetUserIdByName(string name);
    
    int GetTableByUserId(int userId, List<Experience> experienceList);
    int GetTableByUserId(int userId, List<Education> educationList);
    int GetTableByUserId(int userId, List<Skills> skillsList);
    int GetTableByUserId(int userId, List<Projects> projectsList);
    int GetTableByUserId(int userId, List<About> aboutList);
}