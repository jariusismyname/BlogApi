using MySql.Data.MySqlClient;
using BlogApi.Models;

namespace BlogApi.Services
{
    public class PostService
    {
        private readonly IConfiguration _config;

        public PostService(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<Post> GetAll()
        {
            var posts = new List<Post>();
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();
            using var cmd = new MySqlCommand("SELECT * FROM Posts ORDER BY CreatedAt DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                posts.Add(new Post
                {
                    Id = reader.GetInt32("Id"),
                    Content = reader.GetString("Content"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                });
            }
            return posts;
        }

        public void Add(Post post)
        {
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();
            using var cmd = new MySqlCommand("INSERT INTO Posts (Content, CreatedAt) VALUES (@content, @createdAt)", conn);
            cmd.Parameters.AddWithValue("@content", post.Content);
            cmd.Parameters.AddWithValue("@createdAt", post.CreatedAt);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();
            using var cmd = new MySqlCommand("DELETE FROM Posts WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
