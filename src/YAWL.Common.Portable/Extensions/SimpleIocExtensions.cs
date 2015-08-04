using GalaSoft.MvvmLight.Ioc;

namespace YAWL.Common.Extensions
{
    public static class SimpleIocExtensions
    {
        public static TService TryGetInstance<TService>(this SimpleIoc simpleIoc)
        {
            return simpleIoc.IsRegistered<TService>()
                ? simpleIoc.GetInstance<TService>() 
                : default(TService);
        }
    }
}
