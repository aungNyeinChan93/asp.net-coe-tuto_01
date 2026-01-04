

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

    if(context.Request.Path == "/users" && context.Request.Method == "GET")
    {
        await context.Response.WriteAsync("usersz");
    }

});

//app.MapGet("/", () => "Hello C#!");

app.Run();


