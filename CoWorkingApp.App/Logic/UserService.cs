using CoWorkingApp.Data;
using CoWorkingApp.App.Enumerations;
using System;
using CoWorking.App.Models;
using CoWorkingApp.Data.Tools;
using System.Globalization;
using System.Linq;
using CoWorkingApp.App.Tools;

namespace CoWorkingApp.App.Logic
{
    public class UserService
    {
        private UserData userData {get;set;}
        private DeskData deskData {get;set;}
        private ReservationData reservationData {get;set;}
        public UserService(UserData userData, DeskData deskData)
        {
            this.userData = userData;
            this.deskData = deskData;
            this.reservationData = new ReservationData();
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
                           newUser.PassWord = PasswordManager.GetPassWord();
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
                            userFound.PassWord = PasswordManager.GetPassWord();
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
                            userFoundPassWord.PassWord = PasswordManager.GetPassWord();
                            userData.EditUser(userFoundPassWord);
                            Console.WriteLine("Usuario Editado!");
                        break;
                    }

        }

        public User LoginUser(bool isAdmin)
        {
              bool loginResult = false;

                while (!loginResult) 
                {
                    Console.WriteLine("Ingrese usuario");
                    var userLogin = Console.ReadLine();
                    Console.WriteLine("Ingrese contraseña");
                    var passwordLogin = PasswordManager.GetPassWord();

                    var userLogged = userData.Login(userLogin, passwordLogin, isAdmin);
                    loginResult = userLogged!=null;
                    if(!loginResult) Console.WriteLine("Usuario o contraseña incorrecta");
                    else return userLogged;
                }

                return null;
        }

        public void ExecuteActionByUser(User user, MenuUser menuUserSelected)
        {
                switch( menuUserSelected)
                {
                    case MenuUser.ReservarPuesto:
                         var deskList = deskData.GetAvailableDesks();
                         Console.WriteLine("Puestos disponibles:");
                         foreach(var item in deskList)
                         {
                             Console.WriteLine($"{item.Number} - {item.Description}");
                         }
                         var newReservation = new Reservation();

                        Console.WriteLine("Ingrese número del puesto");
                        var deskFound = deskData.FindDesk(Console.ReadLine());

                        while(deskFound == null)
                        {
                                Console.WriteLine("Ingrese número del puesto");
                                deskFound = deskData.FindDesk(Console.ReadLine());
                        }
                        newReservation.DeskId = deskFound.DeskId;

                        var dateSelected = new DateTime();

                        while(dateSelected.Year == 0001)
                        {
                            Console.WriteLine("Ingrese la fecha de reserva (dd-mm-yyyy)");
                            DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, DateTimeStyles.None, out dateSelected);    
                        }

                        newReservation.ReservationDate = dateSelected;
                        newReservation.UserId = user.UserId;
                        reservationData.CreateReservation(newReservation);
                        Console.WriteLine("Reservación creada!");
                    break;
                    case MenuUser.CancelarReserva:
                        Console.WriteLine("Estas son las reservaciones disponibles");
                        var userReservations = reservationData.GetReservationsByUser(user.UserId).ToList();
                        var deskUserList = deskData.GetAvailableDesks().ToList();
                        int indexReservation = 1;

                        foreach(var item in userReservations)
                        {
                            Console.WriteLine($"{indexReservation} - {deskUserList.FirstOrDefault(p => p.DeskId == item.DeskId).Number} - {item.ReservationDate.ToString("dd-MM-yyyy")}");
                            indexReservation++;
                        }

                        var indexReservationSelected = 0;
                        while(indexReservationSelected<1 || indexReservationSelected> indexReservation)
                        {
                            Console.WriteLine("Ingrese el número de la reservación que desea eliminar");
                            indexReservationSelected = int.Parse(Console.ReadLine());
                        }

                        var reservationToDelete = userReservations[indexReservationSelected];

                        reservationData.CancelReservation(reservationToDelete.ReservationId);
                        Console.WriteLine("Reservación cancelada.");
                     break;
                    case MenuUser.HistorialReservas:
                        Console.WriteLine("Tus Reservas");
                        var historyReservationUser = reservationData.GetReservationsHistoryByUser(user.UserId);
                        var deskHistoryList = deskData.GetAllDesks().ToList();
                        foreach(var item in historyReservationUser)
                        {
                            Console.ForegroundColor  = item.ReservationDate > DateTime.Now ? ConsoleColor.Green : ConsoleColor.DarkGray;
                            Console.WriteLine($"{deskHistoryList.FirstOrDefault(p => p.DeskId == item.DeskId).Number} - {item.ReservationDate.ToString("dd-MM-yyyy")} {(item.ReservationDate > DateTime.Now ? "(Activa)" : "")}");
                            Console.ResetColor();
                        }

                    break;
                    case MenuUser.CambiarPassword:
                            Console.WriteLine("Escriba el password");
                            user.PassWord = PasswordManager.GetPassWord();
                            userData.EditUser(user);
                            Console.WriteLine("Password actualizado.");
                    break;
                }
        }
    }
}