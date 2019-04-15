using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Models;
using Core_Service_MiddleWare.Service;
using Microsoft.AspNetCore.Mvc;

namespace Core_Service_MiddleWare.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IRepository<Student> _repository;
        public HomeController(IRepository<Student> repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return Content("Hello From HomeController");
        }

        public IActionResult Index2()
        {
            var models = _repository.GetAll();
            return View(models);
        }

        public IActionResult Detail(int id)
        {
            var model = _repository.GetById(id);
            if (model == null)
                return RedirectToAction(nameof(Index2));
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var id = _repository.GetAll().Max(n => n.Id);
                student.Id = id + 1;
                _repository.Add(student);
                return RedirectToAction(nameof(Index2));
            }
            return View();
            
        }
    }
}