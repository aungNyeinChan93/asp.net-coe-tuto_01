namespace asp_tuto_01.Classes.Posts
{
    public class Post
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Post(int id, string name , string desc)
        {
            this.Id = id;
            this.Name = name;
            this.Description = desc;
        }
    }

    static class PostRepository
    {
        private static List<Post> _posts = new List<Post>()
        {
            new Post(1,"Post One","Post one desc"),
            new Post(2,"Post Two","Post two desc"),
            new Post(3,"Post Three","Post three desc"),
        };

        public static List<Post> GetAllPosts() => PostRepository._posts;


    }
}
