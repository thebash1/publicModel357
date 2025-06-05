namespace Model357App.Model
{
    public class Question
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public Question(string id, string text, string type)
        {
            Id = id;
            Text = text;
            Type = type;
        }
    }
}