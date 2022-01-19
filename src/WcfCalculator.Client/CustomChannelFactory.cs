using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfCalculator.Client
{
    public class CustomChannelFactory<TChannel> : ChannelFactory<TChannel>
    {
        public CustomChannelFactory(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
            // No action required
        }

        protected override ServiceEndpoint CreateDescription()
        {
            var description = base.CreateDescription();

            var behavior = new CustomEndpointBehavior();
            description.EndpointBehaviors.Add(behavior);

            return description;
        }
    }
}
