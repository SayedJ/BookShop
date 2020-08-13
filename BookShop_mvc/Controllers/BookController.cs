using BookShop_mvc.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace BookShop_mvc.Controllers
{
    public class BookController : Controller
    {


        string apiUrl = "http://localhost:8080/";
        

        public async Task<ActionResult> Index()
        {
            List<Book> BookInfo = new List<Book>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/books");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var BookResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    
                    BookInfo = JsonConvert.DeserializeObject<List<Book>>(BookResponse, settings);
                }
                return View(BookInfo);
            }
        }

        public ActionResult AddOrEdit(int id = 0)
        {

            if (id == 0)

                return View(new Book());
            else
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/books/" + id.ToString()).Result;
                    return View(response.Content.ReadAsAsync<Book>().Result);
                }
            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit( Book book)
        {

            if (book.bookId == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync("books", book).Result;

                    TempData["SuccessMessage"] = "Book saved successfully";
                    
                }
            }

            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PutAsJsonAsync("books/" + book.bookId, book).Result;

                    TempData["SuccessMessage"] = "Book Edited successfully";
                    
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("books/" + id.ToString()).Result;

                TempData["SuccessMessage"] = "Book Deleted successfully";
                return RedirectToAction("Index");

            }

        }
            
        
    }
}