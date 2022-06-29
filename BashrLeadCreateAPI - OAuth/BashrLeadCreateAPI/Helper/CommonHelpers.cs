using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;

namespace BashrLeadCreateAPI.Helper
{
    public class CommonHelpers
    {
        internal Entity ValidateUserDetailsInCRM(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ConnectToCRM crmconnecion = new ConnectToCRM();
            OrganizationServiceProxy _serviceProxy = crmconnecion.GetServiceConnection();
            if (_serviceProxy != null)
            {
                EntityCollection collection = _serviceProxy.RetrieveMultiple(new FetchExpression(string.Format(Constants.FetchXmls.getUserByFullName, context.UserName)));
                if (collection.Entities.Count == 1)
                {
                    return collection.Entities[0];
                }
            }
            return null;
        }

        internal Entity ValidateClientIDSecretinCRM(string clientId, string clientSecret)
        {
            ConnectToCRM crmconnecion = new ConnectToCRM();
            OrganizationServiceProxy _serviceProxy = crmconnecion.GetServiceConnection();
            if (_serviceProxy != null)
            {
                EntityCollection collection = _serviceProxy.RetrieveMultiple(new FetchExpression(string.Format(Constants.FetchXmls.getUserByOrgID, clientSecret, clientId)));
                if (collection.Entities.Count == 1)
                {
                    return collection.Entities[0];
                }
            }
            return null;
        }
    }
    public class Constants
    {
        public class FetchXmls
        {
            public const string getUserByOrgID = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' no-lock='true' distinct='false'>
  <entity name='systemuser'>
    <attribute name='fullname' />
    <attribute name='businessunitid' />
    <attribute name='title' />
    <attribute name='address1_telephone1' />
    <attribute name='positionid' />
    <attribute name='systemuserid' />
    <order attribute='fullname' descending='false' />
    <filter type='and'>
      <condition attribute='systemuserid' operator='eq' value='{0}' />
      <condition attribute='organizationid' operator='eq' value='{1}' />
    </filter>
  </entity>
</fetch>";
            public const string getUserByFullName = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' no-lock='true' distinct='false'>
  <entity name='systemuser'>
    <attribute name='fullname' />
    <attribute name='businessunitid' />
    <attribute name='title' />
    <attribute name='address1_telephone1' />
    <attribute name='positionid' />
    <attribute name='systemuserid' />
    <order attribute='fullname' descending='false' />
    <filter type='and'>
      <condition attribute='fullname' operator='eq' value='{0}' />
      <condition attribute='isdisabled' operator='eq' value='0' />
    </filter>
  </entity>
</fetch>";
        }

    }
}