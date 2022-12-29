namespace FilmsLibrary.Models.Sql
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public Movie Movie { get; set; }
    }
}
