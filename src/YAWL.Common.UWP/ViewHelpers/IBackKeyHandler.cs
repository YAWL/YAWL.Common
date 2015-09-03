using System.Threading.Tasks;

namespace YAWL.Common.ViewHelpers
{
    public interface IBackKeyHandler
    {
        Task<bool> HandleBackKey();
    }
}