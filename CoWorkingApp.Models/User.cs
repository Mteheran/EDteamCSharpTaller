using System;
namespace CoWorking.App.Models
{
    public class User
    {
        public Guid UserId { get;set; } = Guid.NewGuid();
        public string Name {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string PassWord {get;set;}
    }
}