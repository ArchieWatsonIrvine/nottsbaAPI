using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Models.Auth;
using NottsBAAPI.Models.News;
using System.Net;

namespace NottsBAAPI;

public class DummyData
{
    public static List<UserInfo> Users = new()
    {
         new UserInfo() { Id = 1, Username = "admin", Password = "adminsetup", Role = "Admin", Forename = "admin", Surname = "admin", RefreshToken = "asdqwerasdewwqr" },
         new UserInfo() { Id = 2, Username = "user", Password = "userpassword", Role = "User", Forename = "userforname", Surname = "usersurname", RefreshToken = "asdqwerasdewwqr" }
    };

    public static List<NewsItems> DummyNews = new()
    {
        new NewsItems() { CatagoryId = NewsCatagory.LeagueInfo, Date = DateTime.Now, Title = "Have Your Say – Join the Nottinghamshire Badminton Association Steering Group", Content = "'The Nottinghamshire Badminton Association (NBA) is inviting members from across the county to express their interest in joining a new NBA Steering Group.',\r\n        'The purpose of the Steering Group is to bring together voices from across the Nottinghamshire badminton community to support the NBA Executive in shaping the future of badminton in the county.',\r\n        'The first Steering Group meeting will take place on 31st March and will be led by NBA Vice Chair, Yvez McKenzie.'", Link = "https://forms.office.com/e/M5qj0UpPKW" },
        new NewsItems() { CatagoryId = NewsCatagory.LeagueInfo, Date = new DateTime(2026, 02, 12), Title = "***Knock Out Cup Relaunch***", Content = "The NottsBA league is set to start on the 1st of September. Teams are currently being formed and more information will be released soon.", Link = "https://www.nottsba.co.uk/league-update" },
        new NewsItems() { CatagoryId = NewsCatagory.Senior, Date = DateTime.Now.AddDays(-7), Title = "Senior Team Triumphs", Content = "The NottsBA senior team has won the regional championship, securing their place in the national finals.", Link = "https://www.nottsba.co.uk/senior-team-triumphs" },
        new NewsItems() { CatagoryId = NewsCatagory.Masters, Date = DateTime.Now.AddDays(-14), Title = "Masters Tournament Announced", Content = "The annual NottsBA Masters tournament has been announced, with registration opening next month.", Link = "https://www.nottsba.co.uk/masters-tournament-announced" },
        new NewsItems() { CatagoryId = NewsCatagory.OutLaws, Date = DateTime.Now.AddDays(-21), Title = "Outlaws Team Expansion", Content = "The NottsBA Outlaws team is expanding and is currently recruiting new players for the upcoming season.", Link = "https://www.nottsba.co.uk/outlaws-team-expansion" }
    };
}