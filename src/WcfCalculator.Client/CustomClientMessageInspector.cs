using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WcfCalculator.Client
{
    public class CustomClientMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var headers = GetHttpRequest(request)?.Headers;
            if (headers != null)
            {
                headers.Add("X-Custom", Guid.NewGuid().ToString());
            }

            return request;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            // No action required
        }

        private HttpRequestMessageProperty GetHttpRequest(Message request)
        {
            var properties = request.Properties;
            var propertyName = HttpRequestMessageProperty.Name;

            if (!properties.TryGetValue(propertyName, out var propertyObject))
            {
                propertyObject = new HttpRequestMessageProperty();
                properties.Add(propertyName, propertyObject);
            }

            return propertyObject as HttpRequestMessageProperty;
        }
    }
}
