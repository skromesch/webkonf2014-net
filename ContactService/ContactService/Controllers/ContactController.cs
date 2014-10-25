using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using ContactService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactService.Controllers
{
    public class ContactController : ApiController
    {
        AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        /*
         * GET localhost:49997/api/contact
         * */
        public IEnumerable<Contact> Get()
        {
            try { 
                DynamoDBContext context = new DynamoDBContext(client);
                IEnumerable<Contact> contactQueryResults;

                contactQueryResults = context.Scan<Contact>(null);

                return contactQueryResults;
            }
            catch (AmazonDynamoDBException e) { 
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (AmazonServiceException e) {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (Exception e) {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }

        }
        /*
         * GET localhost:49997/api/contact?id=22c6bf90-a321-4554-bc71-5c7ba3c24cb3
         * */
        public Contact Get(string id)
        {
            try { 
                DynamoDBContext context = new DynamoDBContext(client);
                IEnumerable<Contact> contactQueryResults;
                contactQueryResults = context.Scan<Contact>(
                                new ScanCondition("Id", ScanOperator.Equal, id));
                return contactQueryResults.First();
            }
            catch (AmazonDynamoDBException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (AmazonServiceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }
        /*
         * POST localhost:49997/api/contact
         * x-www-form-urlencoded
         * name=John+Dee&age=44&email=test%40test.com
         * */
        public HttpResponseMessage Post([FromBody]Contact contact)
        {
            try { 
                DynamoDBContext context = new DynamoDBContext(client);
                context.Save<Contact>(contact);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (AmazonDynamoDBException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (AmazonServiceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }

        /*
         * PUT localhost:49997/api/contact/22c6bf90-a321-4554-bc71-5c7ba3c24cb3
         * x-www-form-urlencoded
         * name=John+Dee&age=44&email=test%40test.com
         * */
        public HttpResponseMessage Put(string id, [FromBody]Contact contact)
        {
            try { 
                DynamoDBContext context = new DynamoDBContext(client);
                IEnumerable<Contact> contactQueryResults;
                contactQueryResults = context.Scan<Contact>(
                                new ScanCondition("Id", ScanOperator.Equal, id));
                if (contactQueryResults.Count() == 0)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Contact with id {0} not found",id)));

                }
                contact.Id = id;
                context.Save<Contact>(contact);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (AmazonDynamoDBException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (AmazonServiceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }

        /*
         * DELETE localhost:49997/api/contact/22c6bf90-a321-4554-bc71-5c7ba3c24cb3
         * */
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                DynamoDBContext context = new DynamoDBContext(client);
                IEnumerable<Contact> contactQueryResults;
                contactQueryResults = context.Scan<Contact>(
                                new ScanCondition("Id", ScanOperator.Equal, id));
                if (contactQueryResults.Count() == 0)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Contact with id {0} not found", id)));

                }
                context.Delete<Contact>(contactQueryResults.First());
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (AmazonDynamoDBException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (AmazonServiceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }
    }
}
