using asp_tuto_01.Classes;
using asp_tuto_01.Classes.Posts;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.Run(async (HttpContext context) =>
{
    // HttpContext Object
    await context.Response.WriteAsync($"Method : {context.Request.Method} \n");
    await context.Response.WriteAsync($"Url : {context.Request.Path} \n");
    await context.Response.WriteAsync("");
    //await context.Response.WriteAsync(context.Request.Query);

    foreach (var key in context.Request.Headers.Keys)
    {
        await context.Response.WriteAsync($"{key} : {context.Request.Headers[key]} \n");
    }

    if (context.Request.Path == "/users" && context.Request.Method == "GET")
    {
        var userRepo = new UserRepository();

        userRepo.SetUser(new User("jojo", "Jojo@123", 32));

        foreach (var user in userRepo.GetAllUsers())
        {
            await context.Response.WriteAsync($" user name - {user?.Name} \n");
        }
        return;
    }

    if(context.Request.Path.StartsWithSegments("/employees") && context.Request.Method.Equals("GET") ){
        await context.Response.WriteAsync("Emplyoe list");
        await context.Response.WriteAsync("\n \n");

        var posts = PostRepository.GetAllPosts();

        foreach (var post in posts)
        {
            await context.Response.WriteAsync($"Post Id :{post.Id}  \n");
            await context.Response.WriteAsync($"Post name is {post.Name} \n");
            await context.Response.WriteAsync($"Desc : {post.Description} \n ");

        }
        return;
    }

});

//app.MapGet("/", () => "Hello C#!");

app.Run();


