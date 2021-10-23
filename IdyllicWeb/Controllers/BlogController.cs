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
    public class BlogController : Controller
    {
        // GET: Blog
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
        public async Task<ActionResult> Create(BlogCrtVM objBlog)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BlogCrtVM, ContactUS>();
                });
                IMapper mapper = config.CreateMapper();
                Blog CreateDto = mapper.Map<BlogCrtVM, Blog>(objBlog);
                uow.BlogRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var blog = uow.BlogRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Blog, BlogUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<Blog, BlogUpVM>(blog);
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(BlogUpVM objBlogvm)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BlogUpVM, Blog>();
                });
                IMapper mapper = config.CreateMapper();
                Blog UpdateDto = mapper.Map<BlogUpVM, Blog>(objBlogvm);
                uow.BlogRepo.Update(UpdateDto);
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
                var DeleteItem = await uow.BlogRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.BlogRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}