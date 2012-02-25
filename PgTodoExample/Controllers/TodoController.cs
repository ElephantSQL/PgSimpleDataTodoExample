using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Data;
using System.Configuration;

namespace PgTodoExample.Controllers
{
    public class TodoController : Controller
    {
        readonly dynamic db;
        public TodoController()
        {
            var uriString = ConfigurationManager.AppSettings["CLOUDPOSTGRES_URL"];
            var uri = new Uri(uriString);
            var db = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var passwd = uri.UserInfo.Split(':')[1];
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3}", uri.Host, db, user, passwd);
            this.db = Database.OpenConnection(connStr);
        }

        public ActionResult Index()
        {
            ViewBag.Todos = db.Todo.All().OrderById();
            return View();
        }

        [HttpPost]
        public ActionResult Create(string text)
        {
            if (string.IsNullOrEmpty(text))
                ModelState.AddModelError("text", "A text is required");

            if (ModelState.IsValid)
            {
                db.Todo.Insert(Text: text);
                TempData["Notice"] = "Your todo item was created";
            }
            else
            {
                TempData["Error"] = "There was a problem with the todo item";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int id, string text, bool done)
        {
            try
            {
                var todo = db.Todo.FindById(id);
                todo.Done = done;
                if (!string.IsNullOrEmpty(text))
                    todo.Text = text;
                db.Todo.Update(todo);
                TempData["Notice"] = "Your todo item was updated";
            }
            catch (Exception e)
            {
                TempData["Error"] = "Todo item was NOT updated: " + e;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                db.Todo.DeleteById(id);
                TempData["Notice"] = "Todo item was deleted";
            }
            catch
            {
                TempData["Error"] = "Todo item was NOT deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
