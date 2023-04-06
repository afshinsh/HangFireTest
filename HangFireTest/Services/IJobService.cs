using System.Threading.Tasks;

namespace HangFireTest.Services
{
    public interface IJobService
    {
        Task ReccuringJob(string path);
        Task DelayedJob(string path);
    }
}
