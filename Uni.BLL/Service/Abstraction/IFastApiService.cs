using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Uni.BLL.Service.Abstraction
{
    public  interface IFastApiService
    {
        public Task<string> UploadStringAsync(string prompt);
    }
}
