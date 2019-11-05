using System;

namespace ServiceRepoDemo.Service.ViewModels
{
    public class ResultViewModel
    {
        public Boolean Success { get; private set; }
        public ResultViewModel(Boolean success)
        {
            Success = success;
        }
    }
}