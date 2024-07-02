# Goodbye Complexity🙋‍♂️, Hello Middleware Magic👋! Supercharge Your .NET Web API Like a Pro!🚀🔥
 We're diving into a crucial aspect of .NET Web API development – Middleware. If you want to enhance your APIs with powerful functionalities, you've come to the right place! 🎉
 From handling requests and responses to logging and authentication, middleware is the unsung hero of Web API. Stick around to learn why it's useful, when to use it, and how to create your own middleware using Request Delegate and the IMiddleware interface!

 # What is Middleware?
 Middleware is a piece of software that intercepts HTTP requests and responses in the pipeline. It allows you to handle cross-cutting concerns like logging, authentication, and error handling.

 # Why should you use Middleware?
 It allows for a clean separation of concerns, reusability of code, and a centralized way to handle common functionalities. You should consider using middleware when you need to handle tasks like logging, authentication, error handling, and even performance monitoring.

 #  Middleware using Request Delegate
      public class AuthenticationMiddleware(RequestDelegate next)
     {
         public async Task InvokeAsync(HttpContext context)
         {
             var authHeader = context.Request.Headers["AuthState"].FirstOrDefault();
             if (!string.IsNullOrEmpty(authHeader))
             {
                 await next(context);
             }
             else
             {
                 var error = new ProblemDetails()
                 {
                     Title = "No authentication Header Found",
                     Status = StatusCodes.Status404NotFound,
                     Detail = "No authentication Header [AuthState] found"
                 };
                 context.Response.ContentType = "application/json";
                 await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                 return;
             }
         }
     }

# Middleware Using IMiddleware Interface
     public class AuthorizationMiddleware : IMiddleware
     {
         public async Task InvokeAsync(HttpContext context, RequestDelegate next)
         {
             var authState = context.Request.Headers["AuthState"].FirstOrDefault();
             if (Equals(authState, "Authenticated"))
             {
                 await next(context);
             }
             else
             {
                 var error = new ProblemDetails()
                 {
                     Title = $"AuthState={authState}",
                     Status = StatusCodes.Status401Unauthorized,
                     Detail = "You are not allowed to access"
                 };
                 context.Response.ContentType = "application/json";
                 await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                 return;
             }
         }
     }

  # Create simple middleware
      app.Use(async (context, next) =>
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        await next();
    
    });

  # Use all the middlewares
    builder.Services.AddTransient<AuthorizationMiddleware>();
    app.UseMiddleware<AuthenticationMiddleware>();
    app.UseMiddleware<AuthorizationMiddleware>();
    app.Use(async (context, next) =>
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        await next();
    
    });
    app.MapControllers();

# Authentication Middleware
  ![image](https://github.com/Netcode-Hub/DemoMiddlewareInWebAPI/assets/110794348/3e109307-454a-43a8-804d-371779880757)

# Authorization Middleware
![image](https://github.com/Netcode-Hub/DemoMiddlewareInWebAPI/assets/110794348/aa896aa5-575f-44b3-9763-caeef42a59ba)

# Why Choose Request Delegate vs. IMiddleware?
Use Request Delegate for simple, quick middleware where dependency injection is not needed. Opt for IMiddleware when you need a more organized structure and want to leverage dependency injection for your middleware.

# Summary
To wrap it up, middleware is a powerful tool in your Web API arsenal. It helps you manage common tasks efficiently. Whether you choose Request Delegate or IMiddleware depends on your specific needs. Understanding and implementing middleware will take your API development to the next level! 🚀

# Here's a follow-up section to encourage engagement and support for Netcode-Hub:
🌟 Get in touch with Netcode-Hub! 📫
1. GitHub: [Explore Repositories](https://github.com/Netcode-Hub/Netcode-Hub) 🌐
2. Twitter: [Stay Updated](https://twitter.com/NetcodeHub) 🐦
3. Facebook: [Connect Here](https://web.facebook.com/NetcodeHub) 📘
4. LinkedIn: [Professional Network](https://www.linkedin.com/in/netcode-hub-90b188258/) 🔗
5. Email: Email: [business.netcodehub@gmail.com](mailto:business.netcodehub@gmail.com) 📧
   
# ☕️ If you've found value in Netcode-Hub's work, consider supporting the channel with a coffee!
1. Buy Me a Coffee: [Support Netcode-Hub](https://www.buymeacoffee.com/NetcodeHub) ☕️
