using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/SocialNetwork/") };
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            var parts = input.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) continue;

            var command = parts[0].ToLower();
            var username = parts[1];

            switch (command)
            {
                case "post":
                    if (parts.Length < 3) continue;
                    var content = parts[2];
                    var postResponse = await httpClient.PostAsJsonAsync("PostMessage", new Post 
                        { 
                        Author = username, 
                        Content = content, 
                        Timestamp = DateTime.Now 
                        });
                    if (postResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{username} Posted -> '{content}' a las {DateTime.Now}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {postResponse.StatusCode}");
                        var errorContent = await postResponse.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error details: {errorContent}");
                    }
                    break;

                case "follow":
                    if (parts.Length < 3) continue;
                    var followee = parts[2];
                    var followResponse = await httpClient.PutAsJsonAsync($"FollowUser/?followerUsername={username}&followeeUsername={followee}", new { });
                    if (followResponse.IsSuccessStatusCode)

                    {
                        Console.WriteLine($"{username} Empezo a seguir a {followee}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {followResponse.StatusCode}");
                        var errorContent = await followResponse.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error details: {errorContent}");
                    }
                    break;

                case "dashboard":
                    try
                    {
                        var response = await httpClient.GetAsync($"GetDashboard/{username}");
                        var posts = await response.Content.ReadAsAsync<List<Post>>();
                        foreach (var post in posts)
                        {
                            Console.WriteLine($"{post.Author} Posted -> '{post.Content}' a las {post.Timestamp}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    break;

                case "wall":
                    try
                    {
                        var response = await httpClient.GetAsync($"GetWall/{username}");
                        var posts = await response.Content.ReadAsAsync<List<Post>>();
                        foreach (var post in posts)
                        {
                            Console.WriteLine($"{post.Author} Posted -> '{post.Content}' a las {post.Timestamp}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}

public class Post
{
    public string id { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime Timestamp { get; set; }
}

public class User
{
    public string Username { get; set; }
}