using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ServiceRepoDemo.Data;
using ServiceRepoDemo.DomainObjects;
using ServiceRepoDemo.Service.Errors;
using ServiceRepoDemo.Service.ViewModels;

namespace ServiceRepoDemo.Service.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> Repository;
        public ItemService(IRepository<Item> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Delete(long itemId)
        {
            var item = Repository.Where(x => x.ItemId == itemId).FirstOrDefault();
            if (item == null)
                throw new NotFoundException("No such item");
            Repository.Delete(item);
            Repository.SaveChanges();
        }

        public ListViewModel<ItemViewModel> GetItems(ItemQueryViewModel viewModel)
        {
            viewModel.PageNumber = viewModel.PageNumber ?? 1;
            viewModel.PageSize = viewModel.PageSize ?? 30;
            var query = Repository.GetAll();
            Expression<Func<Item, ItemViewModel>> proj = x => new ItemViewModel
            {
                ItemId = x.ItemId,
                ItemName = x.ItemName,
                ItemNumber = x.ItemNumber,
                ItemType = x.ItemType,
                UserId = x.UserId
            };

            if (viewModel.ItemId.HasValue)
            {
                var result = query.Where(x => x.ItemId == viewModel.ItemId).Select(proj).FirstOrDefault();
                if (result == null)
                    throw new NotFoundException("No such item");
                return new ListViewModel<ItemViewModel>(new List<ItemViewModel> { result }, 1, 1);
            }

            if (!string.IsNullOrWhiteSpace(viewModel.Name))
            {
                query = query.Where(x => x.ItemName.Contains(viewModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(viewModel.Type))
            {
                query = query.Where(x => x.ItemType == viewModel.Type);
            }

            if (viewModel.NumberMin.HasValue)
            {
                query = query.Where(x => x.ItemNumber >= viewModel.NumberMin);
            }

            if (viewModel.NumberMax.HasValue)
            {
                query = query.Where(x => x.ItemNumber < viewModel.NumberMax);
            }
            var count = query.Count();
            query = query.OrderBy(x => x.ItemName);
            if (viewModel.PageNumber.Value > 1)
            {
                query = query.Skip((viewModel.PageNumber.Value - 1) * viewModel.PageSize.Value);
            }
            query = query.Take(viewModel.PageSize.Value);

            var materialized = query.Select(proj).ToList();
            var totalPages = (int)Math.Ceiling((double)count / viewModel.PageSize.Value);
            return new ListViewModel<ItemViewModel>(materialized, viewModel.PageNumber.Value, totalPages);
        }

        public void Save(ItemViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (!viewModel.ItemId.HasValue && !viewModel.UserId.HasValue)
            {
                throw new ArgumentNullException("Either ItemId or UserId should be present");
            }
            var item = (Item)null;
            if (!viewModel.ItemId.HasValue)
            {
                item = new Item();
                item.UserId = viewModel.UserId.Value;
                Repository.Add(item);
            }
            else
            {
                item = Repository.Where(x => x.ItemId == viewModel.ItemId).FirstOrDefault();
            if (item == null)
                throw new NotFoundException("No such item");
            }


            item.ItemName = viewModel.ItemName;
            item.ItemNumber = viewModel.ItemNumber;
            item.ItemType = viewModel.ItemType;

            Repository.SaveChanges();
            viewModel.ItemId = item.ItemId;
        }
    }
}
