using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfCalculator.Services
{
    public class CustomServiceAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var headers = GetHttpRequest(operationContext)?.Headers;
            if (headers != null)
            {
                var customHeader = headers["X-Custom"];
            }

            return base.CheckAccessCore(operationContext);
        }

        private HttpRequestMessageProperty GetHttpRequest(OperationContext operationContext)
        {
            var properties = operationContext.RequestContext.RequestMessage.Properties;

            if (properties.TryGetValue(HttpRequestMessageProperty.Name, out var propertyObject))
            {
                return propertyObject as HttpRequestMessageProperty;
            }

            return null;
        }
    }
}
