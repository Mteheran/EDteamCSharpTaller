using CoWorking.App.Models;
using System.Linq;
using System;
using CoWorkingApp.Data.Tools;

namespace  CoWorkingApp.Data
{
    public class UserData
    {
        private JsonManager<User> jsonManager;

        public UserData()
        {
            jsonManager = new JsonManager<User>();
        }

        public bool CreateAdmin()
        {
            var userCollection = jsonManager.GetCollection();

            if(!userCollection.Any(p=> p.Name == "ADMIN" && 
                                p.LastName == "ADMIN" && 
                                p.Email== "ADMIN"))
                                {
                                    try
                                    {
                                           var adminUser = new User()
                                            {
                                                Name = "ADMIN",
                                                LastName = "ADMIN",
                                                Email = "ADMIN",
                                                UserId = Guid.NewGuid(),
                                                PassWord = EncryptData.EncryptText("4dmin!")

                                            };

                                            userCollection.Add(adminUser);

                                            jsonManager.SaveCollection(userCollection);
                                            }
                                    catch
                                    {
                                        return false;
                                    }
                                 

                                return true;

                                }

            return true;  
        } 
    }
}