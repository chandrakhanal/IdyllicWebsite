using AutoMapper;
using IdyllicWeb.Areas.Admin.Models;
using IdyllicWeb.Areas.Admin.View_Models;
using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Infrastructure.Filters;
using IdyllicWeb.Infrastructure.Helpers.Account;
using IdyllicWeb.Models;
using IdyllicWeb.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdyllicWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = CustomRoles.Admin)]
    [CSPLHeaders]
    public class MediaCategoryMasterController : Controller
    {
        // GET: MediaCategoryMaster
        public async Task<ActionResult> Index()
        {
            var role = UserAccountHelper.getCurrentUserRole();
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                //var mediaCtgry = await uow.MediaCategoryMstrRepo.GetAllAsync();
                var mediaCtgry = await uow.MediaCategoryMstrRepo.FindAsync(x => x.UserRole == role);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaCategoryMaster, MediaCategoryMasterVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MediaCategoryMaster>, List<MediaCategoryMasterVM>>(mediaCtgry);
                await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: MediaCategoryMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediaCategoryMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(MediaCategoryMasterVM objmediaCtgryVm)
        {
            var role = UserAccountHelper.getCurrentUserRole();
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaCategoryMasterVM, MediaCategoryMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MediaCategoryMaster CreateDto = mapper.Map<MediaCategoryMasterVM, MediaCategoryMaster>(objmediaCtgryVm);
                CreateDto.UserRole = role;
                uow.MediaCategoryMstrRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Create");
            }
        }

        // GET: MediaCategoryMaster/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var mediaCtgry = await uow.MediaCategoryMstrRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaCategoryMaster, MediaCategoryMasterVM>();
                });
                IMapper mapper = config.CreateMapper();
                MediaCategoryMasterVM UpdateDto = mapper.Map<MediaCategoryMaster, MediaCategoryMasterVM>(mediaCtgry);
                uow.Commit();
                return View(UpdateDto);
            }
        }

        // POST: MediaCategoryMaster/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(MediaCategoryMasterVM objmediaCtgryVm)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaCategoryMasterVM, MediaCategoryMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MediaCategoryMaster UpdateDto = mapper.Map<MediaCategoryMasterVM, MediaCategoryMaster>(objmediaCtgryVm);
                uow.MediaCategoryMstrRepo.Update(UpdateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var DeleteItem = await uow.MediaCategoryMstrRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.MediaCategoryMstrRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
