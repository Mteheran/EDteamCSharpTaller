using CoWorkingApp.Data;
using CoWorkingApp.App.Enumerations;
using System;
using CoWorking.App.Models;
using CoWorkingApp.Data.Tools;
using CoWorking.App.Models.Enumerations;

namespace CoWorkingApp.App.Logic
{
    public class DeskService
    {
        private DeskData deskData {get;set;}

        public DeskService(DeskData deskData)
        {
            this.deskData = deskData;
        }

        public void ExecuteAction(AdminPuestos adminDeskAction)
        {

                    switch(adminDeskAction)
                    {
                        case AdminPuestos.Agregar:
                           Desk newDesk = new Desk();
                           Console.WriteLine("Ingrese por favor el número, ejemplo: A-001");
                           newDesk.Number = Console.ReadLine();
                           Console.WriteLine("Ingrese una descripción");
                           newDesk.Description = Console.ReadLine();
                           deskData.CreateDesk(newDesk);
                           Console.WriteLine("Puesto creado con exito");
                        break;
                        case AdminPuestos.Editar:
                            Console.WriteLine("Ingrese numero del puesto");
                            var deskFound = deskData.FindDesk(Console.ReadLine());

                            while(deskFound == null)
                            {
                                Console.WriteLine("Ingrese numero del puesto");
                                deskFound = deskData.FindDesk(Console.ReadLine());
                            }

                           Console.WriteLine("Ingrese por favor el número, ejemplo: A-001");
                           deskFound.Number = Console.ReadLine();
                           Console.WriteLine("Ingrese una descripción");
                           deskFound.Description = Console.ReadLine();
                           Console.WriteLine("Ingrese estado puesto, 1=activo, 2=inactivo, 3=bloqueado");
                           deskFound.DeskStatus = Enum.Parse<DeskStatus>(Console.ReadLine());
                           deskData.EditDesk(deskFound);
                           Console.WriteLine("Puesto editado con exito");
                        break;
                        case AdminPuestos.Eliminar:
                               Console.WriteLine("Ingrese numero del puesto");
                            var deskFoundDelete = deskData.FindDesk(Console.ReadLine());

                            while(deskFoundDelete == null)
                            {
                                Console.WriteLine("Ingrese numero del puesto");
                                deskFoundDelete = deskData.FindDesk(Console.ReadLine());
                            }

                            deskData.DeleteDesk(deskFoundDelete.DeskId);
                            Console.WriteLine("Puesto borrado con exito");
                        break;
                        case AdminPuestos.Bloquear:
                            Console.WriteLine("Ingrese numero del puesto");
                            var deskFoundBlock = deskData.FindDesk(Console.ReadLine());

                            while(deskFoundBlock == null)
                            {
                                Console.WriteLine("Ingrese numero del puesto");
                                deskFoundBlock = deskData.FindDesk(Console.ReadLine());
                            }
                         
                           deskFoundBlock.DeskStatus = DeskStatus.Blocked;
                           deskData.EditDesk(deskFoundBlock);
                           Console.WriteLine("Puesto bloqueado con exito");
                        break;
                    }
        }

    }
}