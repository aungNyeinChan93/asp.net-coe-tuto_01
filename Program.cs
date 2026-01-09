using asp_tuto_01.Classes;
using asp_tuto_01.Classes.Books;
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
        if (!isUpdateSuccess)
        {
            await context.Response.WriteAsync($"Product Not Found!");
            return;
        }
        await context.Response.WriteAsync($"Product Id {product?.Id} was successfully updated");

        return;
    }

    //// PATCH -> /products
    //if(context.Request.Method == "PATCH" && context.Request.Path == "/products")
    //{
    //    using var reader = new StreamReader(context.Request.Body);
    //    var body = await reader.ReadToEndAsync();
    //}

    if(context.Request.Path == "/tests/queryString")
    {
        //await context.Response.WriteAsync(context.Request.QueryString.ToString());
        foreach(var key in context.Request.Query.Keys)
        {
            await context.Response.WriteAsync($"{key} : {context.Request.Query[key]} \n");
        }
        return;
    }

    // DELETE -> /products
    if(context.Request.Method == "DELETE" && context.Request.Path == "/products")
    {
        // add authentication
        if (!context.Request.Headers.ContainsKey("Authorization") || context.Request.Headers["Authorization"] != "admin")
        {
            await context.Response.WriteAsync("You are not authorize");
            return;
        }

        if (context.Request.Query.ContainsKey("id"))
        {
            var id = context.Request.Query["id"];
            if (int.TryParse(id, out int productId))
            {
                var isDeleteSuccess = ProductRepository.DeleteProduct(productId);
                if(!isDeleteSuccess)
                {
                    await context.Response.WriteAsync($"Product not found!");
                    return ;
                }
                await context.Response.WriteAsync($"Product {productId} was successfully deleted!");
                return;
            }
        }
        await context.Response.WriteAsync("Product id not found!");
        return;
    }

    if(context.Request.Path == "/tests/header")
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authorization = context.Request.Headers["Authorization"];
            
            string token = authorization.ToString().Split(" ").Last();
            await context.Response.WriteAsync($" Authorization Token {token}");
        }
        return;
    }

    // GET =>/books
    if(context.Request.Path == "/books" && context.Request.Method == "GET")
    {
        var books = BookRepository.GetAllBooks();

        try
        {
            if(books is null || books.Count <= 0)
            {
                context.Response.StatusCode = 400;
            }
        }
        catch (Exception err)
        {
            await context.Response.WriteAsync($"{err?.Message}");
            return;
        }

        context.Response.Headers["Content-Type"] = "text/html";

        await context.Response.WriteAsync($"<h1> {context.Request.Path.ToString().ToUpper()} </h1>");

        foreach (var book in books)
        {
            await context.Response.WriteAsync($"<h2>Book Id :{book?.Id} , Title :{book?.Title} ,Author :{book?.Author} ,Year :{book?.Year} </h2>");
        }
        context.Response.StatusCode = 200;
        return;
    }

    // POST => /books
    if (context.Request.Path == "/books" && context.Request.Method == "POST")
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();

        Book? books = JsonSerializer.Deserialize<Book>(body!);
        BookRepository.AddBook(books);

        context.Response.StatusCode = 201;
        await context.Response.WriteAsync($"Book was successfully created!");
        return;
    }

    // PUT => /books
    if (context.Request.Path == "/books" && context.Request.Method == "PUT")
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();

        Book? books = JsonSerializer.Deserialize<Book>(body!);
        var isUpdateSuccess = BookRepository.UpdateBook(books);

        if (!isUpdateSuccess)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"Book Update fail!");
            return;
        }
        context.Response.StatusCode = 201;
        await context.Response.WriteAsync($"Book was successfully updated!");
        return;
    }

    // DELETE => /books
    if (context.Request.Path == "/books" && context.Request.Method == "DELETE")
    {
        var token = context.Request.Headers.ContainsKey("Authorization")
                        ? context.Request.Headers["Authorization"].ToString().Split(" ").Last()
                        : null;

        if(token is null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync($"You have not permission!");
            return;
        }

        if(!context.Request.Query.ContainsKey("id"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync($"Query id key is needed!");
            return;
        }

        string? id = context.Request.Query["id"][0];
        if(int.TryParse(id,out int bookId))
        {
            bool isDeleteSuccess = BookRepository.DeleteBook(bookId);

            if(!isDeleteSuccess)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync($"Book delete fail!");
                return;
            }
            //await context.Response.WriteAsJsonAsync(new { name = context.Request.Query["name"][0] });
            //await context.Response.WriteAsJsonAsync(new { Query = context.Request.Query["id"][0] });
            context.Response.StatusCode = 202;
            await context.Response.WriteAsync($"Successfully delete!");
            //await context.Response.WriteAsync($"");
            return;
        }
    }

    if(context.Request.Path == "/tests/err")
    {
        try
        {
            throw new Exception("Test Error");
        }
        catch (Exception err)
        {
            context.Response.StatusCode = 500;
            context.Response.Headers.ContentType = "text/html";
            await context.Response.WriteAsync($"<h3> Error :{err?.Message} </h3>");
        }
    }

    if(context.Request.Path == "/redirection")
    {
        context.Response.Redirect("/users");
        context.Response.StatusCode = 302;
        return;
    }
});

//app.MapGet("/", () => "Hello C#!");

app.Run();


