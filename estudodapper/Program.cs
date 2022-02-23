using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using estudodapper.Models;

namespace estudodapper
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=banco;User ID=sa;Password=YOURPASSWORD";

            using (var connection = new SqlConnection(connectionString))
            {
                //ListCategories(connection);
                //CreateCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection)k
                //CreateManyCategory(connection);
                //ListCategories(connection);
                //ExecuteProcedure(connection);
                //ExecuteReadProcedure(connection);
                //ExecuteScalar(connection);
                //ReadView(connection);
                //OneToOne(connection);
                //OneToMany(connection);
                //QueryMultiple(connection);
                //SelectIn(connection);
                //Like(connection, "api");
                Transaction(connection);

            }

            ;
        }

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] from [Category]");

            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "Cloud";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;
            var insertSql = @"INSERT INTO [Category] 
                                  VALUES(   @Id, 
                                            @Title, 
                                            @Url, 
                                            @Summary, 
                                            @Order, 
                                            @Description, 
                                            @Featured)";


            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"{rows} linhas inseridas");

        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("C8A9AF4C-9D74-4A6A-93EE-2C2FF5BC032A"),
                title = "Frontend 2022"
            });
            Console.WriteLine($"{rows} linhas atualizadas");
        }

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = @"DELETE FROM Category WHERE [Title]=@title";

            var rows = connection.Execute(deleteQuery, new
            {
                title = "Frontend 2022"
            });

            Console.WriteLine($@"{rows} linhas deletadas");
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "Cloud";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "Categoria Nova Nova";
            category2.Description = "Categoria Nova";
            category2.Order = 9;
            category2.Summary = "Categoria Nova";
            category2.Featured = false;

            var insertSql = @"INSERT INTO [Category] 
                                  VALUES(   @Id, 
                                            @Title, 
                                            @Url, 
                                            @Summary, 
                                            @Order, 
                                            @Description, 
                                            @Featured)";

            var rows = connection.Execute(insertSql, new[]
            {
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured
                }
            });
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var prodecure = "spDeleteStudent";

            var pars = new {StudentId = "C9686F2E-0AE6-4F33-BE68-C6BFCB1FBAA8"};
            
            var affectdRows = connection.Execute(prodecure, pars, commandType: CommandType.StoredProcedure);
            
            Console.WriteLine($"{affectdRows}, linhas afetadas" );
            
        }

        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "spGetCoursesByCategory";
            var pars = new {CategoryId = "09CE0B7B-CFCA-497B-92C0-3290AD9D5142"};
            var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);
            
            
            Console.WriteLine(courses);
            foreach (var item in courses)
            {
                Console.WriteLine(item.Title);
            }
        }
        
        static void ExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Amazon AWS";
            category.Url = "Cloud";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;
            var insertSql = @"INSERT INTO [Category] 
                                  OUTPUT inserted.Id
                                  VALUES(   newid(), 
                                            @Title, 
                                            @Url, 
                                            @Summary, 
                                            @Order, 
                                            @Description, 
                                            @Featured)";


            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"A categoria inserida foi: {id}");

        }

        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT * FROM vwCourses";
            var courses = connection.Query(sql);

            foreach (var item  in courses)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void OneToOne(SqlConnection connection)
        {
            var sql = @"SELECT * 
                              FROM CareerItem 
                                   INNER JOIN Course on CareerItem.CourseId = Course.Id";

            var items = connection.Query<CareerItem, Course, CareerItem>(sql, 
                (careerItem, course) =>
            {
                careerItem.Course = course;
                return careerItem;
            }, splitOn: "Id");

            foreach (var item in items)
            {
                Console.WriteLine(item.Course.Title);
            }
        }

        static void OneToMany(SqlConnection connection)
        {
            var sql = @"SELECT Career.Id
                             , Career.Title
                             , CareerItem.CareerId
                             , CareerItem.Title
                        FROM Career
                             INNER JOIN CareerItem on CareerItem.CareerId = Career.Id";
            var careers = new List<Career>();
            var items = connection.Query<Career, CareerItem, Career>(sql,
                (career, item) =>
                {
                    var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
                    if (car == null)
                    {
                        car = career;
                        car.Items.Add(item);
                        careers.Add(car);
                    }
                    else
                    {
                        car.Items.Add(item);
                    }
                    return career;
                    }, splitOn: "CareerId");

            foreach (var career in careers)
            {
                Console.WriteLine($"{career.Title}");
                foreach (var item in career.Items)
                {
                    Console.WriteLine($" - {item.Title}");
                }
            }

        }

        static void QueryMultiple(SqlConnection connection)
        {
            var query = @"SELECT * FROM Category; SELECT * FROM Course";

            using (var multi = connection.QueryMultiple(query))
            {
                var categories = multi.Read<Category>();
                var courses = multi.Read<Course>();

                foreach (var item in categories)
                {
                    Console.WriteLine(item.Title);
                }
                foreach (var item in courses)
                {
                    Console.WriteLine(item.Title);
                }
                
            }
        }

        static void SelectIn(SqlConnection connection)
        {
            var query = @"SELECT * FROM Career where Id in @Id";

            var items = connection.Query<Career>(query, new
            {
                Id = new[]
                {
                    "01AE8A85-B4E8-4194-A0F1-1C6190AF54CB", 
                    "E6730D1C-6870-4DF3-AE68-438624E04C72"
                }
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Like(SqlConnection connection, string term)
        {
            var query = @"SELECT * FROM Course WHERE Title LIKE @exp";

            var items = connection.Query<Course>(query, new
            {
                exp = $"%{term}%"
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Transaction(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Minha categoria";
            category.Url = "Cloud";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 9;
            category.Summary = "AWS Cloud";
            category.Featured = false;
            var insertSql = @"INSERT INTO [Category] 
                                  VALUES(   @Id, 
                                            @Title, 
                                            @Url, 
                                            @Summary, 
                                            @Order, 
                                            @Description, 
                                            @Featured)";

            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                
                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                }, transaction);
                
                //transaction.Commit();
                transaction.Rollback();

                Console.WriteLine($"{rows} linhas inseridas");
            }
        }
    }
}