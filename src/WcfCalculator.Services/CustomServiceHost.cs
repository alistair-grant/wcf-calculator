using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfCalculator.Services
{
    public class CustomServiceHost : ServiceHost
    {
        public CustomServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            // No action required
        }

        protected override void OnOpening()
        {
            base.OnOpening();

            var manager = new CustomServiceAuthorizationManager();
            GetAuthorizationBehavior().ServiceAuthorizationManager = manager;
        }

        private ServiceAuthorizationBehavior GetAuthorizationBehavior()
        {
            var behaviors = Description.Behaviors;

            var authorizationBehavior = behaviors.Find<ServiceAuthorizationBehavior>();
            if (authorizationBehavior == null)
            {
                authorizationBehavior = new ServiceAuthorizationBehavior();
                behaviors.Add(authorizationBehavior);
            }

            return authorizationBehavior;
        }
    }
}