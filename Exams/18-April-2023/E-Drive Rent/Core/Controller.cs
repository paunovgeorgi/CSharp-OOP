using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users = new UserRepository();
        private VehicleRepository vehicles = new VehicleRepository();
        private RouteRepository routes = new RouteRepository();
        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.GetAll().Any(u=>u.DrivingLicenseNumber == drivingLicenseNumber))
            {
                return String.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }

            IUser user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return String.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != nameof(PassengerCar) && vehicleType != nameof(CargoVan))
            {
                return String.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }

            if (vehicles.GetAll().Any(v=>v.LicensePlateNumber == licensePlateNumber))
            {
                return String.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }

            IVehicle vehicle = null;

            if (vehicleType == nameof(PassengerCar))
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            vehicles.AddModel(vehicle);

            return String.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            if (routes.GetAll().Any(r=>r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length == length))
            {
                return String.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
            }
            if (routes.GetAll().Any(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length < length))
            {
                return String.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
            }

            IRoute route = new Route(startPoint, endPoint, length, routes.GetAll().Count + 1);
            IRoute checkRoute = routes.GetAll().FirstOrDefault(r =>
                r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length > length);
            if (checkRoute != null)
            {
                checkRoute.LockRoute();
            }
            routes.AddModel(route);
            return String.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);

        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.GetAll().FirstOrDefault(u => u.DrivingLicenseNumber == drivingLicenseNumber);
            IVehicle vehicle = vehicles.GetAll().FirstOrDefault(v => v.LicensePlateNumber == licensePlateNumber);
            IRoute route = routes.GetAll().FirstOrDefault(r => r.RouteId == int.Parse(routeId));

            if (user.IsBlocked)
            {
                return String.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }

            if (vehicle.IsDamaged)
            {
                return String.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }

            if (route.IsLocked)
            {
                return String.Format(OutputMessages.RouteLocked, routeId);
            }

            vehicle.Drive(route.Length);

            if (isAccidentHappened == true)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString();
        }

        public string RepairVehicles(int count)
        {
            IVehicle[] damagedVehicles = vehicles.GetAll().Where(v => v.IsDamaged == true).OrderBy(v=>v.Brand).ThenBy(v=>v.Model).ToArray();
            if (damagedVehicles.Length >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    damagedVehicles[i].ChangeStatus();
                    damagedVehicles[i].Recharge();
                }
            }
            else
            {
                foreach (IVehicle vehicle in damagedVehicles)
                {
                    vehicle.ChangeStatus();
                    vehicle.Recharge();
                }
                count = damagedVehicles.Length;
            }

            return String.Format(OutputMessages.RepairedVehicles, count);
        }

        public string UsersReport()
        {
            StringBuilder sb = new();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in users.GetAll().OrderByDescending(u=>u.Rating).ThenBy(u=>u.LastName).ThenBy(u=>u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
