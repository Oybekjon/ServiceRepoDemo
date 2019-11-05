using ServiceRepoDemo.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRepoDemo.Service
{
    public interface IItemService
    {
        ListViewModel<ItemViewModel> GetItems(ItemQueryViewModel viewModel);
        void Save(ItemViewModel viewModel);
        void Delete(long itemId);
    }
}
