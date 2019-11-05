using System.Collections;

namespace ServiceRepoDemo.Service.ViewModels
{
    public interface IListViewModel
    {
        IList Data { get; }
        PageInfo PageInfo { get; set; }
    }
}