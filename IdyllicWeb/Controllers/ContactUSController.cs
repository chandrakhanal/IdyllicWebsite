using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IdyllicWeb.View_Models;
using IdyllicWeb.Models;
using IdyllicWeb.Persistence;
using IdyllicWeb.Infrastructure.Extensions;
using IdyllicWeb.Infrastructure.Filters;
using IdyllicWeb.Data_Contexts;
using System.Threading.Tasks;

namespace IdyllicWeb.Controllers
{
    public class ContactUSController : Controller
    {
        // GET: ContactUS
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var contactus = uow.ContactUSRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<ContactUS>, List<ContactUSIndexVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<ContactUS>, IEnumerable<ContactUSIndexVM>>(contactus).ToList();
                return View(indexDto);
            }
        }
        public ActionResult Create()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(ContactUSCrtVM objContactus)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ContactUSCrtVM, ContactUS>();
                });
                IMapper mapper = config.CreateMapper();
                ContactUS CreateDto = mapper.Map<ContactUSCrtVM, ContactUS>(objContactus);
                uow.ContactUSRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var contactus = uow.ContactUSRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ContactUS, ContactUSUPVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<ContactUS, ContactUSUPVM>(contactus);
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ContactUSUPVM objContactusUvm)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ContactUSUPVM, ContactUS>();
                });
                IMapper mapper = config.CreateMapper();
                ContactUS UpdateDto = mapper.Map<ContactUSUPVM, ContactUS>(objContactusUvm);
                uow.ContactUSRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var DeleteItem = await uow.ContactUSRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.ContactUSRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}