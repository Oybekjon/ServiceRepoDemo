using Microsoft.AspNetCore.Mvc;
using ServiceRepoDemo.Service;
using ServiceRepoDemo.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRepoDemo.Web.Controllers
{

    [Route("api/Item")]
    public class ItemController : Controller
    {
        private readonly IItemService Service;
        public ItemController(IItemService service)
        {
            Service = service;
        }
        [HttpGet("")]
        public ActionResult<ListViewModel<ItemViewModel>> GetItems([FromQuery] ItemQueryViewModel viewModel)
        {
            if (!viewModel.PageSize.HasValue || viewModel.PageSize.Value > 200)
                viewModel.PageSize = 200;
            else if (viewModel.PageSize.Value <= 0)
                viewModel.PageSize = 30;
            var result = Service.GetItems(viewModel);
            return new ActionResult<ListViewModel<ItemViewModel>>(result);
            
        }

        [HttpPost("")]
        public ActionResult<ItemViewModel> PostItem([FromBody] ItemViewModel viewModel)
        {
            Service.Save(viewModel);
            return new ActionResult<ItemViewModel>(viewModel);
        }
    }
}
