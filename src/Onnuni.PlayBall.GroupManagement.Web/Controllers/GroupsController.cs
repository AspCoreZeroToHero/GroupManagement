using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Onnuni.PlayBall.GroupManagement.Web.Models;

namespace Onnuni.PlayBall.GroupManagement.Web.Controllers
{

    [Route("groups")]
    public class GroupsController: Controller
    {
        private static List<GroupViewModel> groups = new()
        {
            new GroupViewModel
            {
                Id = 1,
                Name = "Soccer"
            },
            new GroupViewModel
            {
                Id = 2,
                Name = "Voleyball"
            },
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View(groups);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = groups.Where(x => x.Id == id).SingleOrDefault();
            if(group is null) 
                return NotFound();

            return View(group);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("", Name = "CreateGroup")]
        public IActionResult CreateGroup(GroupViewModel model)
        {
            model.Id = groups.Count;
            groups.Add(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = groups.Where(x => x.Id == id).SingleOrDefault();
            if(group is null) 
                return NotFound();
            group.Name = model.Name;
            return RedirectToAction(nameof(Index));
        }
    }
}