namespace SportsScheduler.Models
{
    public enum MenuType
    {
        Home = 0,
        Soccer = 1,
        Tennis = 2
    }
        
    public class MenuItem
    {
        public MenuType MenuType { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Id { get; set; }
        public string Icon { get; set; }

        public MenuItem()
        {
            MenuType = MenuType.Home;
        }
    }
}