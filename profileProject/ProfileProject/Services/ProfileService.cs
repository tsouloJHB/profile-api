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
            List<Education> educations = await _context.Educations.ToListAsync();
            List<Experience> experience = await _context.Experiences.ToListAsync();
            List<Skills> skill = await _context.Skills.ToListAsync();
            List<Projects> project = await _context.Projects.ToListAsync(); 
            List<About> about = await _context.Abouts.ToListAsync();
            List<Profile> li = new  List<Profile>();
            foreach(var item in value ){
                
                //get user profile
                Profile profiler = new Profile();
                profiler.name = item.name;
                profiler.surname = item.surname;
                profiler.email = item.email;
                profiler.cell = item.cell;
                

                foreach(var edu in educations ){
                    if(edu.UserId == item.id){
                        profiler.MyEducation = edu.MyEducation;
                    }
                }

                foreach (var ex in experience)
                {
                    if(ex.UserId == item.id)
                    {
                        profiler.MyExperience = ex.MyExperience;
                    }
                }

                foreach (var pro in project)
                {
                    if (pro.UserId == item.id)
                    {
                        profiler.MyProjects = pro.MyProjects;
                    }
                }
                foreach (var edu in about.Where(edu => edu.UserId == item.id))
                {
                    profiler.MyEducation = edu.about;
                }
                foreach(var edu in skill){
                    if(edu.UserId == item.id){
                        profiler.MySkills = edu.MySkills;
                    }
                }
                li.Add(profiler);  
            }  
            return li;
    }

    public async Task<List<Profile>> GetProfileByName(string name)
    {
           var list1 = await _context.Educations.ToListAsync();
               var  results = (from a in _context.users
                where a.name == name
           
                select new {
                    id = a.id,
                    name = a.name,
                    surname = a.surname,
             
                } );
            Users users =  new Users();
            foreach(var item in results){
                users.id = item.id;
            }
            var  result = (from a in _context.Abouts where a.Id == users.id
                join b in _context.Educations on users.id equals b.UserId 
                join c in _context.Experiences on users.id  equals c.UserId
                join d in _context.Projects on users.id  equals d.UserId
                join e in _context.users on users.id  equals e.id
                join f in _context.Skills on users.id  equals f.UserId
                select new {
                    about = a.about,
                    education = b.MyEducation,
                    educationDesription = b.Description,
                    experienceDescription = c.Description,
                    experience = c.MyExperience,
                    projects = d.MyProjects,
                    skill = f.MySkills,
                    e.cell,
                    name = e.name,
                    surname = e.surname,
                    email = e.email,
                    occupation = e.currentOccupation,
                    id = e.id,
                    image = e.image, 
                } );
            Profile profile = new Profile();
            List<Profile> profileList = new List<Profile>();   
                foreach(var item in result){
                    profile.about = item.about;
                    profile.MyEducation = item.education;
                    profile.MyEducationDescription = item.educationDesription;
                    profile.MyExperience = item.experience;
                    profile.MyExperienceDescription = item.experienceDescription;
                    profile.MyProjects = item.projects;
                    profile.MySkills = item.skill;
                    profile.cell = item.cell;
                    profile.email = item.email;
                    profile.currentOccupation = item.occupation;
                    profile.name = item.name;
                    profile.surname = item.surname;
                    profile.UserId = item.id;
                    profile.image = item.image;
                    profileList.Add(profile);
            }
                return profileList;
    }

  

    public async Task<Profile> CreateProfile(Profile profile)
    {

        Users users = CreateUser(profile);
        bool educationResponse = CreateEducation(profile, users.id);
        bool aboutResponse = CreateAbout(profile, users.id);
        bool experienceResponse = CreateExperience(profile, users.id);
        bool projectResponse = CreateProject(profile, users.id);
        bool skillsResponse = CreateSkills(profile, users.id);
        await _context.SaveChangesAsync();
        return profile;
    }

    public async Task<bool> UpdateProfile(Profile profile) {
            Users users = new Users();
            users.name = profile.name;
            users.surname = profile.surname;
            users.cell = profile.cell;
            users.currentOccupation = profile.currentOccupation;
            users.email = profile.email;
            users.id = profile.UserId;
            users.image = profile.image;
            _context.Entry(users).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            // create Education 
            var list1 = await _context.Educations.ToListAsync();
            int educationId =  GetTableByUserId(profile.UserId,list1);
            foreach (var item in list1)
            {
                if (item.Id == educationId)
                {
                    item.UserId = profile.UserId;
                    item.MyEducation = profile.MyEducation;
                    item.Description = profile.MyEducationDescription;
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }   
            }
            
            //create experience
            var list2 = await _context.Experiences.ToListAsync();
            int experienceId =  GetTableByUserId(profile.UserId,list2);
            foreach (var item in list2)
            {
                if (item.Id == experienceId)
                {
                    item.UserId = profile.UserId;
                    item.MyExperience = profile.MyExperience;
                    item.Description = profile.MyExperienceDescription;
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }   
            }

            //create project
            var list3 = await _context.Projects.ToListAsync();
            int projectId =  GetTableByUserId(profile.UserId,list3);
            foreach (var item in list3)
            {
                if (item.Id == projectId)
                {
                    item.UserId = profile.UserId;
                    item.MyProjects = profile.MyProjects;
                    item.Description = profile.MyProjectsDescription;
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }   
            }
            
            //skills
            var list4 = await _context.Skills.ToListAsync();
            int skillsId =  GetTableByUserId(profile.UserId,list4);
            foreach (var item in list4)
            {
                if (item.Id == skillsId)
                {
                    item.UserId = profile.UserId;
                    item.MySkills = profile.MySkills;
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }   
            }
            
            //about
            var list5 = await _context.Abouts.ToListAsync();
            int aboutId =  GetTableByUserId(profile.UserId,list5);
            foreach (var item in list5)
            {
                if (item.Id == aboutId)
                {
                    item.UserId = profile.UserId;
                    item.about = profile.about;
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }   
            }
            return true;
    }

    public Users CreateUser(Profile profile)
    {
        Users users = new Users();
        users.name = profile.name;
        users.surname = profile.surname;
        users.cell = profile.cell;
        users.currentOccupation = profile.currentOccupation;
        users.email = profile.email;
        users.image = profile.image;
        _context.users.AddRangeAsync(users);
        return users;
    }

    public bool CreateEducation(Profile profile , int userId) {
        
        Education education = new Education();
           
        education.UserId = userId;
        education.MyEducation = profile.MyEducation;
        education.Description = profile.MyEducationDescription;
        var response = _context.Educations.AddAsync(education);
        
        return response.IsCompletedSuccessfully;
    }

    public bool CreateAbout(Profile profile, int userId) {
        About about = new About();
        about.UserId = userId;
        about.about = profile.about;
        //var response =  _context.Abouts.AddRangeAsync(about);
        var response =  _context.Abouts.AddAsync(about);
        return response.IsCompletedSuccessfully;
    }

    public bool CreateExperience(Profile profile , int userId){
        Experience experience = new Experience();
        experience.UserId = userId;
        experience.MyExperience = profile.MyExperience;
        experience.Description = profile.MyProjectsDescription;
        var response =  _context.Experiences.AddAsync(experience);
        return response.IsCompletedSuccessfully;
    }

    public bool CreateProject(Profile profile, int userId){
        Projects projects = new Projects();
        projects.UserId = userId;
        projects.MyProjects = profile.MyProjects;
        projects.Description = profile.MyProjectsDescription;
        var response = _context.Projects.AddAsync(projects);
        return response.IsCompletedSuccessfully;
    }

    public bool CreateSkills(Profile profile,int userId){
        
        Skills skills = new Skills();
        skills.UserId = userId;
        skills.MySkills = profile.MySkills;
        var response = _context.Skills.AddAsync(skills);
        return response.IsCompletedSuccessfully;
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
        throw new NotImplementedException();
    }

    public int GetTableByUserId(int userId, List<Education> educationList)
    {
        throw new NotImplementedException();
    }

    public int GetTableByUserId(int userId, List<Skills> skillsList)
    {
        throw new NotImplementedException();
    }

    public int GetTableByUserId(int userId, List<Projects> projectsList)
    {
        throw new NotImplementedException();
    }

    public int GetTableByUserId(int userId, List<About> aboutList)
    {
        throw new NotImplementedException();
    }
}