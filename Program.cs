using asp_tuto_01.Classes;
using asp_tuto_01.Classes.Employees;
using asp_tuto_01.Classes.Posts;
using asp_tuto_01.Classes.Products;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.Run(async (HttpContext context) =>
{
    if(context.Request.Method == "GET" && context.Request.Path == "/")
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
    }

    // -> /users
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


    // -> /posts
    if(context.Request.Path.StartsWithSegments("/posts") && context.Request.Method.Equals("GET") ){
        await context.Response.WriteAsync("Posts list");
        await context.Response.WriteAsync("\n \n");

        List<Post> posts = PostRepository.GetAllPosts();

        foreach (var post in posts)
        {
            await context.Response.WriteAsync($"Post Id :{post.Id}  \n");
            await context.Response.WriteAsync($"Post name is {post.Name} \n");
            await context.Response.WriteAsync($"Desc : {post.Description} \n ");

        }
        return;
    }

    // POST -> /posts
    if (context.Request.Method == "POST" && context.Request.Path.StartsWithSegments("/posts"))
    {
        //context.Request.Body
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();

        var newPost = JsonSerializer.Deserialize<Post>(body);
        PostRepository.SetPost(post: newPost!);
        await context.Response.WriteAsync("success");
        return;

    }

    // GET -> /employees
    if (context.Request.Method == "GET" && context.Request.Path == "/employees")
    {
        var employees = EmployeRepository.GetAllEmployees();

        foreach (var employe in employees)
        {
            await context.Response.WriteAsync($"{employe.Id} - " +
                $"Employee Name is {employe.Name}. " +
                $"Salary:[{employe.Salary}]. " +
                $"Position :[{employe?.Position}] \n");
        };

        return;
    }


    // POST ->  /employees
    if (context.Request.Method == "POST" && context.Request.Path == "/employees")
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        var employe = JsonSerializer.Deserialize<Employe>(body);
        EmployeRepository.AddEmployee(employe!);
        return;
    }


    // GET -> /products
    if( context.Request.Method == "GET" && context.Request.Path.StartsWithSegments("/products"))
    {
        var products = ProductRepository.GetProducts();
        foreach (var product in products)
        {
            await context.Response.WriteAsync($"Product ID : {product?.Id} \n");
            await context.Response.WriteAsync($"Product Name : {product?.Name} \n");
            await context.Response.WriteAsync($"Product Price : {product?.Price} \n");
            await context.Response.WriteAsync($"");
        }

        return;
    }



    // POST -> /products
    if (context.Request.Method == "POST" && context.Request.Path.StartsWithSegments("/products"))
    {
        using var reader =new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();

        Product? product = JsonSerializer.Deserialize<Product>(body);
        ProductRepository.AddProduct(product!);

        return;
    }

    // PUT -> /products
    if (context.Request.Method == "PUT" && context.Request.Path.StartsWithSegments("/products"))
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();

        Product? product = JsonSerializer.Deserialize<Product>(body);
        bool isUpdateSuccess = ProductRepository.UpdateProduct(product!);
        if (isUpdateSuccess)
        {
            await context.Response.WriteAsync($"Product Id {product?.Id} was successfully updated");
        }
        return;
    }

    //// PATCH -> /products
    //if(context.Request.Method == "PATCH" && context.Request.Path == "/products")
    //{
    //    using var reader = new StreamReader(context.Request.Body);
    //    var body = await reader.ReadToEndAsync();


    //}


});

//app.MapGet("/", () => "Hello C#!");

app.Run();


