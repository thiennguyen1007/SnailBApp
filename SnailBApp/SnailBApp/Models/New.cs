namespace SnailBApp.Models
{
    public class New
    {
        public string Title { get; set; }
        public string IMG { get; set; }
        public string Detail { get; set; }
        public New(New x)
        {
            Title = x.Title;
            IMG = x.IMG;
            Detail = x.Detail;
        }
        public New() { }
    }
}
