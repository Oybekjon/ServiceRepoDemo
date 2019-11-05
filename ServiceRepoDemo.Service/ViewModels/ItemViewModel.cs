using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRepoDemo.Service.ViewModels
{
    public class ItemViewModel
    {
        public long? ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int? ItemNumber { get; set; }
        public long? UserId { get; set; }
    }
}
