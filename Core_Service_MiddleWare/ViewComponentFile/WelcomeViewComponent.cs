using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Models;
using Core_Service_MiddleWare.Service;
using Microsoft.AspNetCore.Mvc;

namespace Core_Service_MiddleWare.ViewComponentFile
{
    public class WelcomeViewComponent:ViewComponent
    {
        private readonly IRepository<Student> _repository;
        public WelcomeViewComponent(IRepository<Student> repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke(int a)
        {
            var count = _repository.GetAll().Count().ToString();
            return View("Default", count);
        }
    }
}
