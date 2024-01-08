using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(new Entity.Tag { Text = "web programlama", Url = "web-programlama", Color = TagColors.warning },
                    new Entity.Tag { Text = "backend", Url = "back-end", Color = TagColors.warning },
                    new Entity.Tag { Text = "php", Url = "php", Color = TagColors.secondary },
                    new Entity.Tag { Text = "Yazılım", Url = "Yazılım", Color = TagColors.primary },
                    new Entity.Tag { Text = "Kodlama", Url = "Kodlama", Color = TagColors.danger },
                    new Entity.Tag { Text = "frontEnd", Url = "fornt-end", Color = TagColors.success }
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any()) // hiç kayıt kayıt yoksa 
                {
                    context.Users.AddRange(
                        new User { UserName = "ertuğrul şimşek", Image = "man.jpg" }, // entity şart değildir
                        new Entity.User { UserName = "mehmet şimşek", Image = "man.jpg" },
                        new Entity.User { UserName = "Beyza Yıldız", Image = "woman.jpg" }
                    );
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Title = "asp.net Core",
                            Content = "asp.net Core dersleri",
                            Url = "aspnet-core",
                            IsActive = true,
                            Image = "netcore.png",
                            PublishedOn = DateTime.Now.AddDays(-8),
                            Tags = context.Tags.Take(1).ToList(),
                            UserId = 1,
                            comments = new List<Comment>{new Comment{Text="çok faydalı bir kurs olmuş",PublishedOn= DateTime.Now.AddDays(-10),UserId=2,PostId=1},
                                new Comment{Text="çok yararlı bir kurs olmuş",PublishedOn= DateTime.Now.AddDays(-10),UserId=3,PostId=1}
                            }

                        },
                        new Post
                        {
                            Title = "php",
                            Content = "php dersleri",
                            Url = "php",
                            IsActive = true,
                            Image = "php.png",
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 1
                        },
                        new Post
                        {
                            Title = "Backend",
                            Content = "backend dersleri",
                            Url = "back-end",
                            Image = "backend.png",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-15),
                            Tags = context.Tags.Take(0).ToList(),
                            UserId = 1

                        },
                        new Post
                        {
                            Title = "Django",
                            Content = "Django dersleri",
                            Url = "django",
                            IsActive = true,
                            Image = "php.png",
                            PublishedOn = DateTime.Now.AddDays(-32),
                            Tags = context.Tags.Take(0).ToList(),
                            UserId = 1

                        },
                        new Post
                        {
                            Title = "React",
                            Content = "React dersleri",
                            Url = "React",
                            IsActive = true,
                            Image = "php.png",
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(0).ToList(),
                            UserId = 1

                        },
                        new Post
                        {
                            Title = "Web-Api",
                            Content = "Api dersleri",
                            Url = "web-api",
                            IsActive = true,
                            Image = "php.png",
                            PublishedOn = DateTime.Now.AddDays(-43),
                            Tags = context.Tags.Take(0).ToList(),
                            UserId = 1


                        },
                        new Post
                        {
                            Title = "Angular",
                            Content = "Angular dersleri",
                            Url = "angular",
                            IsActive = true,
                            Image = "php.png",
                            PublishedOn = DateTime.Now.AddDays(-50),
                            UserId = 1,
                            Tags = context.Tags.Take(0).ToList()

                        }

                    );
                    context.SaveChanges();
                }


            }
        }
    }
}