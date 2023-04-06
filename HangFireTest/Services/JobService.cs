using HangFireTest.Models;
using HangFireTest.Services.Utility;
using System;
using System.Threading.Tasks;

namespace HangFireTest.Services
{
    public class JobService : IJobService
    {
        
        private readonly HttpService _httpSrevice;
        
        public JobService(HttpService httpService)
        {
            _httpSrevice = httpService;
        }

        public async Task ReccuringJob(string path)
        {
           var result = await _httpSrevice.GetAsync<ApiResponseModel>(path);

        }

        public async Task DelayedJob(string path)
        {
            var result = await _httpSrevice.GetAsync<ApiResponseModel>(path);
        }
    
    }
}
