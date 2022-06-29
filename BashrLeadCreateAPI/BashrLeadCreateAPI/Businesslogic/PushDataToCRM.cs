using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BashrLeadCreateAPI.Models;
using BashrLeadCreateAPI.Helper;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using System.Net;
using BashrLeadCreateAPI.Models.InputRequest;
using BashrLeadCreateAPI.Models.InputDocRequest;

namespace BashrLeadCreateAPI.Businesslogic
{
    public class PushDataToCRM
    {
        internal OutputResponse CreateSMEApplication(InputRequest enquiryDetails)
        {
            ConnectToCRM crmconnecion = new ConnectToCRM();
            OutputResponse outModel = new OutputResponse();
            if (enquiryDetails != null)
            {
                try
                {
                    OrganizationServiceProxy _serviceProxy = crmconnecion.GetServiceConnection();
                    if (_serviceProxy != null)
                    {
                        Entity lead = new Entity("lead");
                        if (enquiryDetails.dataArea != null)
                        {
                            if (enquiryDetails.dataArea.CompanyName != null)
                            {
                                lead["companyname"] = enquiryDetails.dataArea.CompanyName;
                            }
                            if (enquiryDetails.dataArea.Spoc != null)
                            {
                                foreach (var spc in enquiryDetails.dataArea.Spoc)
                                {
                                    lead["lastname"] = spc.Name;
                                    lead["jobtitle"] = spc.Designation;
                                    foreach (var s in spc.Contacts)
                                    {
                                        if (s.Email != null)
                                            lead["emailaddress1"] = s.Email;
                                        if (s.MobileNumber != null)
                                            lead["mobilephone"] = s.MobileNumber;
                                        if (s.Website != null)
                                            lead["websiteurl"] = s.Website;

                                    }
                                }
                            }
                        }
                        Guid id = _serviceProxy.Create(lead);
                        if (enquiryDetails.applicationArea != null)
                        {
                            outModel.applicationArea = enquiryDetails.applicationArea;
                        }
                        outModel.dataArea = new Models.InputRequest.DataArea
                        {
                            ReferenceNumber = id.ToString()
                        };
                        outModel.ResponseStatus = new ResponseStatus
                        {
                            Status = "SUCCESS",
                            StatusCode = HttpStatusCode.OK

                        };

                    }
                    else
                    {
                        if (enquiryDetails.applicationArea != null)
                        {
                            outModel.applicationArea = enquiryDetails.applicationArea;
                        }
                        outModel.ResponseStatus = new ResponseStatus
                        {
                            Status = "ERROR",
                            StatusCode = HttpStatusCode.InternalServerError,
                            errorDetails = new ErrorDetails[]
                             {
                                 new ErrorDetails
                                 {
                                  ErrorCode = "500",
                                   ErrorDesc = "SERVICE PROXY IS NULL"
                                 }
                             }

                        };
                    }
                }
                catch (Exception e)
                {
                    if (enquiryDetails.applicationArea != null)
                    {
                        outModel.applicationArea = enquiryDetails.applicationArea;
                    }
                    outModel.ResponseStatus = new ResponseStatus
                    {
                        Status = "ERROR",
                        StatusCode = HttpStatusCode.InternalServerError,
                        errorDetails = new ErrorDetails[]
                         {
                                 new ErrorDetails
                                 {
                                  ErrorCode = "500",
                                   ErrorDesc = e.Message,
                                 }
                         }

                    };
                }
            }
            else
            {
                //if (enquiryDetails.applicationArea != null)
                //{
                //    outModel.applicationArea = enquiryDetails.applicationArea;
                //}
                outModel.ResponseStatus = new ResponseStatus
                {
                    Status = "ERROR",
                    StatusCode = HttpStatusCode.InternalServerError,
                    errorDetails = new ErrorDetails[]
                     {
                                 new ErrorDetails
                                 {
                                  ErrorCode = "500",
                                   ErrorDesc = "INPUT REQUEST IS EMPTY",
                                 }
                     }

                };
            }
            return outModel;
        }

        internal OutputResponse UploadDocuments(InputDocumentRequest enquiryDetails)
        {
            ConnectToCRM crmconnecion = new ConnectToCRM();
            OutputResponse outModel = new OutputResponse();
            if (enquiryDetails != null)
            {
                try
                {
                    Guid LeadId = Guid.Empty;
                    OrganizationServiceProxy _serviceProxy = crmconnecion.GetServiceConnection();
                    if (_serviceProxy != null)
                    {
                        if (enquiryDetails.DataArea != null)
                        {
                            if (enquiryDetails.DataArea.ReferenceNumber != null)
                            {
                                LeadId = new Guid(enquiryDetails.DataArea.ReferenceNumber);
                                if (enquiryDetails.DataArea.Attachments != null && enquiryDetails.DataArea.Attachments.Count() > 0)
                                {
                                    foreach (var attc in enquiryDetails.DataArea.Attachments)
                                    {
                                        Entity notes = new Entity("annotation");
                                        if (attc.FileName != null)
                                        {
                                            notes["subject"] = "SMEAPPDOCS-" + attc.FileName;
                                        }
                                        if (attc.FileContent != null)
                                        {
                                            notes["documentbody"] = attc.FileContent;
                                        }
                                        if (attc.MimeType != null)
                                        {
                                            notes["mimetype"] = attc.MimeType;
                                        }
                                        if (LeadId != Guid.Empty)
                                        {
                                            notes["objectid"] = new EntityReference("lead", LeadId);
                                            _serviceProxy.Create(notes);
                                            if (enquiryDetails.ApplicationArea != null)
                                            {
                                                outModel.applicationArea = enquiryDetails.ApplicationArea;
                                            }

                                            outModel.ResponseStatus = new ResponseStatus
                                            {
                                                Status = "UPLOADED DOCUMENTS SUCCESSFULLY",
                                                StatusCode = HttpStatusCode.OK

                                            };
                                        }
                                        else
                                        {
                                            if (enquiryDetails.ApplicationArea != null)
                                            {
                                                outModel.applicationArea = enquiryDetails.ApplicationArea;
                                            }
                                            outModel.ResponseStatus = new ResponseStatus
                                            {
                                                Status = "ERROR",
                                                StatusCode = HttpStatusCode.InternalServerError,
                                                errorDetails = new ErrorDetails[]
                                                 {
                                                     new ErrorDetails
                                                     {
                                                      ErrorCode = "500",
                                                       ErrorDesc = "LEAD ID IS NOT RESOLVED TO PROCESS THE REQUEST",
                                                     }
                                                 }

                                            };
                                        }
                                    }
                                }
                                else
                                {
                                    if (enquiryDetails.ApplicationArea != null)
                                    {
                                        outModel.applicationArea = enquiryDetails.ApplicationArea;
                                    }
                                    outModel.ResponseStatus = new ResponseStatus
                                    {
                                        Status = "ERROR",
                                        StatusCode = HttpStatusCode.InternalServerError,
                                        errorDetails = new ErrorDetails[]
                                         {
                                                     new ErrorDetails
                                                     {
                                                      ErrorCode = "500",
                                                       ErrorDesc = "NO DOCUMENT ARE FOUND IN THE REQUEST",
                                                     }
                                         }

                                    };
                                }
                            }
                            else
                            {
                                if (enquiryDetails.ApplicationArea != null)
                                {
                                    outModel.applicationArea = enquiryDetails.ApplicationArea;
                                }
                                outModel.ResponseStatus = new ResponseStatus
                                {
                                    Status = "ERROR",
                                    StatusCode = HttpStatusCode.InternalServerError,
                                    errorDetails = new ErrorDetails[]
                                     {
                                                     new ErrorDetails
                                                     {
                                                      ErrorCode = "500",
                                                       ErrorDesc = "REFERENCE NUMBER IS NOT FOUND IN THE REQUEST",
                                                     }
                                     }

                                };
                            }
                        }
                        else
                        {
                            if (enquiryDetails.ApplicationArea != null)
                            {
                                outModel.applicationArea = enquiryDetails.ApplicationArea;
                            }
                            outModel.ResponseStatus = new ResponseStatus
                            {
                                Status = "ERROR",
                                StatusCode = HttpStatusCode.InternalServerError,
                                errorDetails = new ErrorDetails[]
                                 {
                                                     new ErrorDetails
                                                     {
                                                      ErrorCode = "500",
                                                       ErrorDesc = "DATA AREA IS NOT FOUND IN THE REQUEST",
                                                     }
                                 }

                            };
                        }
                    }
                    else
                    {
                        if (enquiryDetails.ApplicationArea != null)
                        {
                            outModel.applicationArea = enquiryDetails.ApplicationArea;
                        }
                        outModel.ResponseStatus = new ResponseStatus
                        {
                            Status = "ERROR",
                            StatusCode = HttpStatusCode.InternalServerError,
                            errorDetails = new ErrorDetails[]
                             {
                                                     new ErrorDetails
                                                     {
                                                      ErrorCode = "500",
                                                       ErrorDesc = "CRM SERVICE PROXY IS NULL",
                                                     }
                             }

                        };
                    }
                }
                catch (Exception e)
                {
                    if (enquiryDetails.ApplicationArea != null)
                    {
                        outModel.applicationArea = enquiryDetails.ApplicationArea;
                    }
                    outModel.ResponseStatus = new ResponseStatus
                    {
                        Status = "ERROR",
                        StatusCode = HttpStatusCode.InternalServerError,
                        errorDetails = new ErrorDetails[]
                         {
                                 new ErrorDetails
                                 {
                                  ErrorCode = "500",
                                   ErrorDesc = e.Message,
                                 }
                         }

                    };
                }
            }
            else
            {
                outModel.ResponseStatus = new ResponseStatus
                {
                    Status = "ERROR",
                    StatusCode = HttpStatusCode.InternalServerError,
                    errorDetails = new ErrorDetails[]
                     {
                                 new ErrorDetails
                                 {
                                  ErrorCode = "500",
                                   ErrorDesc = "INPUT REQUEST IS EMPTY",
                                 }
                     }

                };
            }
            return outModel;
        }
    }
}