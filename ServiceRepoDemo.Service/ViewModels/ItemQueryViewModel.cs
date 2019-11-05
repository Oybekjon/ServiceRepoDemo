using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRepoDemo.Service.ViewModels
{
    public class ItemQueryViewModel
    {
        public long? ItemId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? NumberMin { get; set; }
        public int? NumberMax { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
