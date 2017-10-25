using EMS.Models;
using EMS.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web.Mvc;
using PagedList;
using System.Collections.Generic;

namespace EMS.Controllers
{
    [Authorize]
    public class EventController : Controller
    {

        private IRepository _Repo;
        private ApplicationDbContext _ApplicationDbContext;
        private ApplicationUserManager _UserManager;

        public EventController(IRepository Repo)
        {
            _Repo = Repo;
            _ApplicationDbContext = new ApplicationDbContext();
            _UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_ApplicationDbContext));
        }

        
        public PartialViewResult GetAllEvents(int EventId = 0, int page = 1,string name="")
        {
          
            //var events = _Repo.GetEventById(EventId);

            //return PartialView("_GetAllEvents", events);

            var pageSize = 7;
            var startIndex = (page - 1) * pageSize + 1;
            var endIndex = startIndex + pageSize;

            var user = _Repo.GetUser(User.Identity.GetUserId());

            var totalItem = _Repo.GetEventById(EventId);

            ICollection<Event> c = totalItem as ICollection<Event>;

            int x = c.Count;

            var company = _Repo.GetEventForntEnd(EventId, user.Country, startIndex, endIndex,name);

            var usersAsIPagedList = new StaticPagedList<Event>(company, page, pageSize, x);

            return PartialView("_GetAllEvents",usersAsIPagedList);
        }


     
        

        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Dashboard(int id)
        {
            var eventTitle = _Repo.GetEventById(id);
            return View(eventTitle);
        }

       
        public ActionResult Tutorial(int id, int page = 1)
        {
   

            var pageSize = 9;
            var startIndex = (page - 1) * pageSize + 1;
            var endIndex = startIndex + pageSize;
            
            var user = _Repo.GetUser(User.Identity.GetUserId());

            var totalItem = _Repo.GetPreDefinedTaskFrontEnd(id, user.Country);

          

            var company = _Repo.GetPreDefinedTask(id, user.Country, startIndex, endIndex);

            var usersAsIPagedList = new StaticPagedList<PreDefinedTask>(company, page, pageSize, totalItem.TotalItem);

            return View(usersAsIPagedList);

        }

        public PartialViewResult TaskList(int id )
        {

            var events = _Repo.GetPreDefinedTaskList(id);

            return PartialView("_TaskList", events);


        }

        [HttpPost]
        public JsonResult CreateFavouritePreDefinedTask(int PreDefinedTaskId, int EventId)
        {

            FavouritePreDefinedTask fav = new FavouritePreDefinedTask();
            fav.UserId = User.Identity.GetUserId();
            fav.PreDefinedTaskId = PreDefinedTaskId;
            fav.EventId = EventId;
            //fav.EventId = 2;
            //fav.PreDefinedTaskId = 2;



            bool result = _Repo.InsertFavouritePreDefinedTask(fav);

            return Json(result);
        }

        public ActionResult MyTask()
        {
            return View(); 
        }

        public ActionResult PersonalEvent()
        {
            return View(); 
        }

        public PartialViewResult GetPersonalEvent(int id)
        {
            
            var events = _Repo.GetPersonalEvent(id, User.Identity.GetUserId());
            return PartialView("_GetPersonalEvent", events);
        }

        public PartialViewResult FavouritePreDefinedTask(int id)
        {
            var fav = _Repo.GetFavouritePreDefinedTask(User.Identity.GetUserId(), id);
            return PartialView("_FavouritePreDefinedTask", fav);
        }

        
        public ActionResult FavouritePreDefinedTaskList(int id)
        {
            var result = _Repo.GetPreDefinedTaskList(id);
            return View(result);
        }

        [HttpPost]
        public JsonResult DeleteFavouritePreDefinedTask(int PreDefinedTaskId)
        {

            //var preDefinedTask = _Repo.GetFavouritePreDefinedTask(User.Identity.GetUserId(), id);
            //FavouritePreDefinedTask fav = new FavouritePreDefinedTask();
            
            bool result = _Repo.DeleteFavouritePreDefinedTask(PreDefinedTaskId, User.Identity.GetUserId());

            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteGuestList(int GuestListId)
        {

            //var preDefinedTask = _Repo.GetFavouritePreDefinedTask(User.Identity.GetUserId(), id);
            //FavouritePreDefinedTask fav = new FavouritePreDefinedTask();

            bool result = _Repo.DeleteGuestList(GuestListId, User.Identity.GetUserId());

            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteManageTicket(int ManageTicketId)
        {

            //var preDefinedTask = _Repo.GetFavouritePreDefinedTask(User.Identity.GetUserId(), id);
            //FavouritePreDefinedTask fav = new FavouritePreDefinedTask();

            bool result = _Repo.DeleteManageTicket(ManageTicketId);

            return Json(result);
        }


        public PartialViewResult CreateUserDefinedTaskDate(int id)
        {
            var result = _Repo.GetDate(id, User.Identity.GetUserId());
            return PartialView("_CreateUserDefinedTaskDate",result);
        }

        [HttpPost]
        public JsonResult CreateUserDefinedTaskDate(UserDefinedTaskDate eventDate, int id)
        {
            bool result = _Repo.InsertUserDefinedTaskDate(eventDate.NewDate, id, User.Identity.GetUserId());

            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateUserDefinedTaskDate(UserDefinedTaskDate eventDate, int id)
        {
            bool result = _Repo.UpdateUserDefinedTaskDate(eventDate.NewDate, id, User.Identity.GetUserId());

            return Json(result);
        }

        public ActionResult GuestList()
        {
            return View();
        }

       

        public PartialViewResult GetGuestList(int id)
        {
            var result = _Repo.GetGuestList(id, User.Identity.GetUserId());
            return PartialView("_GetGuestList", result);
        }

        [HttpPost]
        public JsonResult InsertPersonalEvent(int id, String Name, String Description, bool Status=false,
            String Picture=""  )
        {
            User user = _Repo.GetUser(User.Identity.GetUserId());
            bool result = _Repo.InsertPersonalEvent(Name, Description, Status, Picture,
                user.Country, id, User.Identity.GetUserId());

            return Json(result);
        }

        [HttpPost]
        public JsonResult DeletePersonalEvent(int PersonalEventId)
        {
            
            bool result = _Repo.DeletePersonalEvent(PersonalEventId);

            return Json(result);
        }

        [HttpPost]
        public JsonResult InsertGuestList(int id, Guest guest)
        {
            
            bool result = _Repo.InsertGuestList(
                id, User.Identity.GetUserId(), guest.Name, guest.Type, guest.NoOfPeople);

            return Json(result);
        }

        [HttpPost]
        public JsonResult InsertManageTicket(int id, ManageTicket mt)
        {

            bool result = _Repo.InsertManageTicket(id, User.Identity.GetUserId(), mt.Description, mt.Price);

            return Json(result);
        }


        [HttpGet]
        public PartialViewResult GetPreDefinedTaskListTrack(int id)
        {

            var result = _Repo.GetPreDefinedTaskListTrack(id, User.Identity.GetUserId());
            return PartialView("_GetPreDefinedTaskListTrack", result);
            
        }


        [HttpPost]
        public JsonResult InsertPreDefinedTaskListTrack(int id)
        {
            bool result = _Repo.InsertPreDefinedTaskListTrack(id, User.Identity.GetUserId());
            return Json(result);

        }
        public ActionResult ManageTicket()
        {
           
            return View();
        }

        public PartialViewResult GetManageTicket(int id)
        {
            var result = _Repo.GetManageTicket(id, User.Identity.GetUserId());
            return PartialView("_GetManageTicket", result);
        }
    }
}