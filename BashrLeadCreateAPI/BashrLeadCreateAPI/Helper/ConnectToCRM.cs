using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Sdk.Client;
using System.Configuration;
using System.ServiceModel.Description;

namespace BashrLeadCreateAPI.Helper
{
    public class ConnectToCRM
    {
        internal OrganizationServiceProxy GetServiceConnection()
        {
            string URL = ConfigurationManager.AppSettings["CRMURL"] as string;
            string CRMUserName = ConfigurationManager.AppSettings["CRMUsername"] as string;
            string CRMPassword = ConfigurationManager.AppSettings["CRMPassword"] as string;
            string CRMDomain = ConfigurationManager.AppSettings["CRMDomain"] as string;
            Uri url = new Uri(URL);
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = CRMDomain+@"\"+CRMUserName;
            clientCredentials.UserName.Password = CRMPassword;
            return new OrganizationServiceProxy(url, null, clientCredentials, null);
        }
    }
}