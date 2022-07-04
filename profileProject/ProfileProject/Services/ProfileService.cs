using Microsoft.EntityFrameworkCore;
using ProfileProject.Data;
using ProfileProject.Models;

namespace ProfileProject.Services;

public class ProfileService: IProfile
{
    private readonly ProfileDbContext _context;
    public ProfileService(ProfileDbContext context)
    {
        _context = context;
    }

    public void GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Profile>> GetAllProfiles()
    {
         List<Users> value = await _context.users.ToListAsync();
         List<Profile> profiles = new List<Profile>();
         foreach (var item in value)
         {
             Profile profile = await GetProfileById(item.Id);
             profiles.Add(profile);
         }
         
         return profiles;
    }

    public async Task<Profile> GetProfileById(int id)
    {
        Profile profile = new Profile();
        var education = await _context.Educations.FindAsync(id);
        profile.MyEducation = education.MyEducation;
        profile.MyEducationDescription = education.Description;
        //experience
        var experience = await _context.Experiences.FindAsync(id);
        profile.MyExperience = experience.MyExperience;
        profile.MyExperienceDescription = experience.Description;
        //skills
        var skills = await _context.Skills.FindAsync(id);
        profile.MySkills = skills.MySkills;
        //projects
        var projects = await _context.Projects.FindAsync(id);
        profile.MyProjects = projects.MyProjects;
        profile.MyProjectsDescription = projects.Description;
        //user
        var user = await _context.users.FindAsync(id);
        profile.name = user.name;
        profile.surname = user.surname;
        profile.email = user.email;
        profile.cell = user.cell;
        profile.image = user.image;
        profile.UserId = user.Id;
        profile.Theme = user.Theme;
        profile.currentOccupation = user.currentOccupation;
        //about
        var about = await _context.Abouts.FindAsync(id);
        profile.about = about.about;
        
        return profile;
    }

    public int GetUserIdByName(string name) {
        var  results = (from a in _context.users
            where a.name == name
            select new {
                id = a.Id,
            } );
       return results.First().id;
    }

    public async Task<Profile> GetProfileByName(string name)
    {
        Profile profile = new Profile();
        int id =  GetUserIdByName(name);
        profile = await GetProfileById(id);
        return profile;
    }
    
    public async Task<Profile> CreateProfile(Profile profile)
    {
    
        Users users = CreateUser(profile);
        await _context.AddAsync(users);
        await _context.SaveChangesAsync();
        profile.UserId = users.Id;
        await _context.Educations.AddAsync( CreateEducation(profile));
       
        await _context.Abouts.AddAsync( CreateAbout(profile));
        await _context.Experiences.AddAsync( CreateExperience(profile));
        await _context.Skills.AddAsync( CreateSkills(profile));
        await _context.Projects.AddAsync( CreateProject(profile));
        await _context.SaveChangesAsync();
      
        return profile;
    }

    public async Task<bool> UpdateProfile(Profile profile) {
             Users users = new Users();
             users = CreateUser(profile);
            _context.Entry(users).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await UpdateEducation(profile);
            await UpdateExperience(profile);
            await UpdateProject(profile);
            await UpdateSkills(profile);
            await UpdateAbouts(profile);
            return true;
    }

    private async Task<bool> UpdateEducation(Profile profile) {
        var edcuationList = await _context.Educations.ToListAsync();
        int educationId =  GetTableByUserId(profile.UserId,edcuationList);
        var edcucation = await _context.Educations.FindAsync(educationId);
        edcucation.UserId = profile.UserId;
        edcucation.MyEducation = profile.MyEducation;
        edcucation.Description = profile.MyEducationDescription;
        _context.Entry(edcucation).State = EntityState.Modified;
        var isCompletedSuccessfully = _context.SaveChangesAsync().IsCompletedSuccessfully;
        return isCompletedSuccessfully;
    }

    private async Task<bool> UpdateExperience(Profile profile)
    {
        var experiencelist = await _context.Experiences.ToListAsync();
        int experienceId =  GetTableByUserId(profile.UserId,experiencelist);
        var experience = await _context.Experiences.FindAsync(experienceId);
        experience.UserId = profile.UserId;
        experience.MyExperience = profile.MyExperience;
        experience.Description = profile.MyExperienceDescription;
        _context.Entry(experience).State = EntityState.Modified;
        var isCompletedSuccessfully = _context.SaveChangesAsync().IsCompletedSuccessfully;
        return isCompletedSuccessfully;
    }

    private async Task<bool> UpdateProject(Profile profile) {
        var projectsList = await _context.Projects.ToListAsync();
        int projectId =  GetTableByUserId(profile.UserId,projectsList);
        var project = await _context.Projects.FindAsync(projectId);
        project.UserId = profile.UserId;
        project.MyProjects = profile.MyProjects;
        project.Description = profile.MyProjectsDescription;
        _context.Entry(project).State = EntityState.Modified;
        var isCompletedSuccessfully = _context.SaveChangesAsync().IsCompletedSuccessfully;
        return isCompletedSuccessfully;
    }

    private async Task<bool> UpdateSkills(Profile profile) {
        var skillsList = await _context.Skills.ToListAsync();
        int skillsId =  GetTableByUserId(profile.UserId,skillsList);
        var skills = await _context.Skills.FindAsync(skillsId);
        skills.UserId = profile.UserId;
        skills.MySkills = profile.MySkills;
        var isCompletedSuccessfully = _context.SaveChangesAsync().IsCompletedSuccessfully;
        return isCompletedSuccessfully;
    }

    private async Task<bool> UpdateAbouts(Profile profile) {
        var aboutslist = await _context.Abouts.ToListAsync();
        int aboutId =  GetTableByUserId(profile.UserId,aboutslist);
        var about = await _context.Abouts.FindAsync(aboutId);
        about.UserId = profile.UserId;
        about.about = profile.about;
        _context.Entry(about).State = EntityState.Modified;
        var isCompletedSuccessfully = _context.SaveChangesAsync().IsCompletedSuccessfully;
        return isCompletedSuccessfully;  
    }

    public Users CreateUser(Profile profile)
    {
        Users users = new Users();
        users.name = profile.name;
        users.surname = profile.surname;
        users.cell = profile.cell;
        users.currentOccupation = profile.currentOccupation;
        users.email = profile.email;
        users.Id = profile.UserId;
        users.image = profile.image;
        users.Theme = profile.Theme;
        users.PasswordHash = Array.Empty<byte>();
        users.PasswordSalt = Array.Empty<byte>();
        return users;
    }

    public Education CreateEducation(Profile profile) {
        
        Education education = new Education();
           
        education.UserId = profile.UserId;
        education.MyEducation = profile.MyEducation;
        education.Description = profile.MyEducationDescription;
      
        
        return education;
    }

    public About CreateAbout(Profile profile) {
        About about = new About();
        about.UserId = profile.UserId;
        about.about = profile.about;
        //var response =  _context.Abouts.AddRangeAsync(about);
        // var response =  _context.Abouts.AddAsync(about);
        return about;
    }

    public Experience CreateExperience(Profile profile){
        Experience experience = new Experience();
        experience.UserId = profile.UserId;
        experience.MyExperience = profile.MyExperience;
        experience.Description = profile.MyProjectsDescription;
        return experience;
    }

    public Projects CreateProject(Profile profile){
        Projects projects = new Projects();
        projects.UserId = profile.UserId;
        projects.MyProjects = profile.MyProjects;
        projects.Description = profile.MyProjectsDescription;
        return projects;
    }

    public Skills CreateSkills(Profile profile){
        
        Skills skills = new Skills();
        skills.UserId = profile.UserId;
        skills.MySkills = profile.MySkills;
        return skills;
    }

    public Users CreateUserObject(Profile profile)
    {
        throw new NotImplementedException();
    }

    public Education CreateEducationObject(Profile profile)
    {
        throw new NotImplementedException();
    }

    public Experience CreateExperienceObject(Profile profile)
    {
        throw new NotImplementedException();
    }

    public Skills CreateSkillsObject(Profile profile)
    {
        throw new NotImplementedException();
    }

    public Projects CreateProjectObject(Profile profile)
    {
        throw new NotImplementedException();
    }

    public int GetTableByUserId(int userId, List<Experience> experienceList)
    {
        var  results = (from a in experienceList
            where a.UserId == userId
            
            select new {
                id = a.Id,
            
            } );
        return results.First().id;
    }

    public int GetTableByUserId(int userId, List<Education> educationList)
    {
        var  results = (from a in educationList
            where a.UserId == userId
            
            select new {
                id = a.Id,
            
            } );
        return results.First().id;
    }

    public int GetTableByUserId(int userId, List<Skills> skillsList)
    {
        var  results = (from a in skillsList
            where a.UserId == userId
            
            select new {
                id = a.Id,
            
            } );
        return results.First().id;
    }

    public int GetTableByUserId(int userId, List<Projects> projectsList)
    {
        var  results = (from a in projectsList
            where a.UserId == userId
            
            select new {
                id = a.Id,
            
            } );
        return results.First().id;
    }

    public int GetTableByUserId(int userId, List<About> aboutList)
    {
        var  results = (from a in aboutList
            where a.UserId == userId
            
            select new {
                id = a.Id,
            
            } );
        return results.First().id;
    }
}