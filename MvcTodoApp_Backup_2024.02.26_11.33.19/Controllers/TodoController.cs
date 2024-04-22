using Azure;
using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using MvcTodoApp.Services;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using TodoModels.Models;
using Kendo.Mvc.UI;

namespace MvcTodoApp.Controllers
{

    public class TodoController : Controller
    {
        public IWebRequestService _webRequestService;
        private readonly IConfiguration _configuration;

        public TodoController(IWebRequestService webRequestService, IConfiguration configuration)
        {
            _webRequestService = webRequestService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes");
            List<Todo> todos = DeseralizeObjectToList(result);
            return View(todos);
        }

        public IActionResult Details(Guid Id)
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes/{Id}");
            Todo todo = DeseralizeObject(result)!;
            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind()] Todo todo)
        {
            string postData = JsonConvert.SerializeObject(todo);
            string result = _webRequestService.WebRequestPost($"{_configuration["API:URL"]}/Todoes", postData);
            Console.WriteLine(result);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid Id)
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes/{Id}");
            Todo todo = DeseralizeObject(result)!;
            return View(todo);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit([Bind()] Todo todo)
        {
            Guid Id = todo.TodoId;
            string updateData = JsonConvert.SerializeObject(todo);
            string result = _webRequestService.WebRequestPut($"{_configuration["API:URL"]}/Todoes/{Id}", updateData);
            Console.WriteLine(result);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid Id)
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes/{Id}");
            Todo todo = DeseralizeObject(result)!;
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid Id)
        {
            string result = _webRequestService.WebRequestDelete($"{_configuration["API:URL"]}/Todoes/{Id}");
            Console.WriteLine(result);
            return RedirectToAction(nameof(Index));
        }


        //--------------------------------------------------------
        //JsonConvert 
        public List<Todo> DeseralizeObjectToList(string result)
        {
            List<Todo> t = JsonConvert.DeserializeObject<List<Todo>>(result)!;
            return t;
        }

        public Todo DeseralizeObject(string result)
        {
            Todo t = JsonConvert.DeserializeObject<Todo>(result)!;
            return t;
        }

        public string SeralizeObject(Todo entity)
        {
            string data = JsonConvert.SerializeObject(entity);
            return data;
        }


    }
}
