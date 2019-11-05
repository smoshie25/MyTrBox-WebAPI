using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MyTrBox_WebAPI.ModelViewHolder.AccountViewHolder;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface IAccount
    {
        Task<(bool succeeded, string errorMessage)> CreatUserAsync(UserViewModel model);
    }
}
