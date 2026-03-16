namespace NottsBAAPI.Models.News
{
    public class NewsItems
    {
        public int Id { get; set; }
        public NewsCatagory CatagoryId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }   
        public string Content { get; set; }
        public string Link { get; set; }
    }

    public enum NewsCatagory
    {
        LeagueInfo = 1,
        Senior = 2,
        Masters = 3,
        OutLaws = 4
        //More to come
    }
}
