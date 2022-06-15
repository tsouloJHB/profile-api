using ProfileProject.Models;

namespace ProfileProject.Services;

public interface IProfile
{
    void GetAllUsers();
    Task<List<Profile>> GetAllProfiles();
    Task<List<Profile>> GetProfileByName(string name);
    // Task<List<Profile>> GetProfileById(int id);
    
    
    //create data
    Users CreateUser(Profile profile);
    bool CreateEducation(Profile profile,int userId);
    bool CreateAbout(Profile profile, int userId);
    bool CreateExperience(Profile profile, int userId);

    bool CreateProject(Profile profile,int userId);
    bool CreateSkills(Profile profile,int userId);
    //create objects
    Task<Profile> CreateProfile(Profile profile);
    Users CreateUserObject(Profile profile);
    Education CreateEducationObject(Profile profile);
    Experience CreateExperienceObject(Profile profile);
    Skills CreateSkillsObject(Profile profile);
    Projects CreateProjectObject(Profile profile);
    
    //update database
    Task<bool> UpdateProfile(Profile profile);
    
    int GetTableByUserId(int userId, List<Experience> experienceList);
    int GetTableByUserId(int userId, List<Education> educationList);
    int GetTableByUserId(int userId, List<Skills> skillsList);
    int GetTableByUserId(int userId, List<Projects> projectsList);
    int GetTableByUserId(int userId, List<About> aboutList);
}