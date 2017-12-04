using BookUp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BookUp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchString)
        {
            var books = new List<Book>();
            if (searchString != String.Empty && searchString != null)
            {
                using (var httpClient = new HttpClient())
                {
                    string url = String.Format("https://www.googleapis.com/books/v1/volumes?q={0}", searchString);
                    var response = httpClient.GetStringAsync(new Uri(url)).Result;

                    JObject responseObject = JObject.Parse(response);
                    if (responseObject["error"] != null)
                    {
                        ViewBag.Books = books;
                        return View(books);
                    }
                    JArray items = (JArray)responseObject["items"];
                    foreach (var item in items)
                    {
                        var authors = new List<string>();
                        foreach (var author in item["volumeInfo"]["authors"])
                        {
                            
                            authors.Add(author.ToString());
                        }
                        var book = new Book
                        {
                            Title = item["volumeInfo"]["title"].ToString(),
                            Authors = authors
                        };

                        books.Add(book);
                    }
                }
            }

            ViewBag.Books = books;

            return View(books);
        }


    }
}