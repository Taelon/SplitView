using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace SplitView
{
    public class NetworkConnectionTrigger : StateTriggerBase
    {
        private bool requiresInternet;
        public NetworkConnectionTrigger()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
        }
        public bool RequiresInternet
        {
            get { return this.requiresInternet; }
            set
            {
                var profile = NetworkInformation.GetInternetConnectionProfile();
                if (profile != null && profile.GetNetworkConnectivityLevel() ==
                NetworkConnectivityLevel.InternetAccess)
                {
                    SetActive(true);
                }
                else
                {
                    SetActive(false);
                }
            }
        }

        private async void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (NetworkInformation.GetInternetConnectionProfile() != null)
                {
                    SetActive(this.RequiresInternet);
                }
                else
                {
                    SetActive(!this.RequiresInternet);
                }
            });
        }
    }
}
