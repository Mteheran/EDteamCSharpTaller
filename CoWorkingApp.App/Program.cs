using System;
using CoWorkingApp.Data;
using CoWorkingApp.App.Enumerations;
using CoWorkingApp.App.Logic;
using CoWorkingApp.Data.Tools;
using CoWorking.App.Models;
using CoWorkingApp.App.Tools;

namespace CoWorkingApp.App
{
    class Program
    {
        static User ActiveUser {get;set;}
        static UserData UserDataService {get;set;} = new UserData();
        static DeskData DeskDataService {get;set;} = new DeskData();
        static UserService UserLogicService {get;set;} = new UserService(UserDataService, DeskDataService);
        static DeskService DeskLogicService {get;set;} = new DeskService(DeskDataService);
        static void Main(string[] args)
        {
            string roleSelected= "";

            Console.WriteLine("Bienvenido al Coworking");
            Console.WriteLine("Seleccione el rol con el que desea ingresar");
            Console.WriteLine();
            while(roleSelected!="1" && roleSelected!="2")
            {
                Console.WriteLine("1=Admin, 2=Usuario");
                roleSelected = Console.ReadLine();
            }

            if(Enum.Parse<UserRole>(roleSelected) == UserRole.Admin)
            {
                UserLogicService.LoginUser(true);
                SpinnerManager.Show();

                string menuAdminSelected = "";

                while(true)
                {

                while(menuAdminSelected!="1" && menuAdminSelected!="2")
                {
                    Console.WriteLine("1=Administración de puestos, 2=Administración de usuarios");
                    menuAdminSelected = Console.ReadLine();
                }

                if(Enum.Parse<MenuAdmin>(menuAdminSelected)== MenuAdmin.AdministracionPuestos)
                {
                    string menuPuestosSelected = "";
                    while(menuPuestosSelected!="1" && 
                    menuPuestosSelected!="2" &&
                    menuPuestosSelected!="3" &&
                    menuPuestosSelected!="4" )
                    {

                        Console.WriteLine("Administración de puestos");
                        Console.WriteLine("1=Crear,2=Editar,3=eliminar,4=bloquear");
                        menuPuestosSelected = Console.ReadLine();
                    }

                    AdminPuestos menuAdminPuestoSelected = Enum.Parse<AdminPuestos>(menuPuestosSelected);

                    DeskLogicService.ExecuteAction(menuAdminPuestoSelected);

                }
                else if(Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdministracionUsuarios)
                {
                    string menuUsuarioSelected = "";
                    while(menuUsuarioSelected!="1" && 
                    menuUsuarioSelected!="2" &&
                    menuUsuarioSelected!="3" &&
                    menuUsuarioSelected!="4" )
                    {

                        Console.WriteLine("Administración de Usuarios");
                        Console.WriteLine("1=Crear,2=Editar,3=eliminar,4=cambiar contraseña");
                        menuUsuarioSelected = Console.ReadLine();
                    }

                    AdminUser menuAdminUserSelected = Enum.Parse<AdminUser>(menuUsuarioSelected);

                    UserLogicService.ExecuteAction(menuAdminUserSelected);
                 
                }

                menuAdminSelected = "";

                }

            }
            else if (Enum.Parse<UserRole>(roleSelected)== UserRole.User)
            {
                //Login before actions
                ActiveUser = UserLogicService.LoginUser(false);

            while(true)
            {


                string menuUsuarioSelected = "";

                while(menuUsuarioSelected!="1" && 
                     menuUsuarioSelected!="2" &&
                     menuUsuarioSelected!="3" &&
                     menuUsuarioSelected!="4")
                {
                    Console.WriteLine("1=Reservar puesto, 2=Cancelar reserva, 3= Ver el historial de reservas, 4=Cambiar contraseña");
                    menuUsuarioSelected = Console.ReadLine();
                }

                MenuUser menuUserSelected = Enum.Parse<MenuUser>(menuUsuarioSelected);

                menuUsuarioSelected="";

                UserLogicService.ExecuteActionByUser(ActiveUser, menuUserSelected);
               
            }

            }
        }
        
    }
}
