using CoWorkingApp.Data;
using CoWorkingApp.App.Enumerations;
using System;
using CoWorking.App.Models;
using CoWorkingApp.Data.Tools;

namespace CoWorkingApp.App.Logic
{
    public class UserService
    {
        private UserData userData {get;set;}

        public UserService(UserData userData)
        {
            this.userData = userData;
        }

        public void ExecuteAction(AdminUser menuAdminUserSelected)
        {
               switch(menuAdminUserSelected)
                    {
                        case AdminUser.Agregar:
                        User newUser = new User();
                           Console.WriteLine("Escriba el nombre");
                           newUser.Name = Console.ReadLine();
                           Console.WriteLine("Escriba el apellido");
                           newUser.LastName = Console.ReadLine();
                           Console.WriteLine("Escriba el Email");
                           newUser.Email = Console.ReadLine();
                           Console.WriteLine("Escriba el password");
                           newUser.PassWord = EncryptData.GetPassWord();
                           userData.CreateUser(newUser);
                           Console.WriteLine("Usuario creado!");
                        break;
                        case AdminUser.Editar:
                            Console.WriteLine("Escriba correo del usuario");
                            var userFound = userData.FindUser(Console.ReadLine());

                            while(userFound == null)
                            {
                                Console.WriteLine("Escriba correo del usuario");
                                userFound = userData.FindUser(Console.ReadLine());
                            }

                            Console.WriteLine("Escriba el nombre");
                            userFound.Name = Console.ReadLine();
                            Console.WriteLine("Escriba el apellido");
                            userFound.LastName = Console.ReadLine();
                            Console.WriteLine("Escriba el Email");
                            userFound.Email = Console.ReadLine();
                            Console.WriteLine("Escriba el password");
                            userFound.PassWord = EncryptData.GetPassWord();
                            userData.EditUser(userFound);
                            Console.WriteLine("Usuario Editado!");

                        break;
                        case AdminUser.Eliminar:
                           
                            Console.WriteLine("Escriba correo del usuario");
                            var userFoundDelete = userData.FindUser(Console.ReadLine());

                            while(userFoundDelete == null)
                            {
                                Console.WriteLine("Escriba correo del usuario");
                                userFoundDelete = userData.FindUser(Console.ReadLine());
                            }


                            Console.WriteLine($"¿Esta seguro que desea eliminar a {userFoundDelete.Name} {userFoundDelete.LastName}, SI-NO?");

                            if(Console.ReadLine() == "SI")
                            {
                                userData.DeleteUser(userFoundDelete.UserId);
                            }
                            Console.WriteLine("Usuario eliminado con exito");
                        break;
                        case AdminUser.CambiarPassword:
                             Console.WriteLine("Escriba correo del usuario");
                            var userFoundPassWord = userData.FindUser(Console.ReadLine());

                            while(userFoundPassWord == null)
                            {
                                Console.WriteLine("Escriba correo del usuario");
                                userFoundPassWord = userData.FindUser(Console.ReadLine());
                            }

                      
                            Console.WriteLine("Escriba el password");
                            userFoundPassWord.PassWord = EncryptData.GetPassWord();
                            userData.EditUser(userFoundPassWord);
                            Console.WriteLine("Usuario Editado!");
                        break;
                    }

        }
    }
}