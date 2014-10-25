using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactService.Models
{
    [DynamoDBTable("Contacts")]
    public class Contact
    {
        [DynamoDBHashKey(AttributeName="id")]
        public string Id { get; set; }
        [DynamoDBProperty(AttributeName = "email")]
        public string Email { get; set; }
        [DynamoDBProperty(AttributeName = "age")]
        public int Age { get; set; }
        [DynamoDBProperty(AttributeName = "name")]
        public string Name { get; set; }
        public override string ToString()
        {
            return string.Format(@"{0} - {1} - {2} - {3}",Id,Name,Email,Age);
        }
    }

}