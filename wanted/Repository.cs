using EMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;

namespace EMS.Repository
{
    public class EMSRepository : IRepository
    {

        protected readonly EMSDBContainer _DbContext;

        public EMSRepository(EMSDBContainer EMSDb)
        {
            _DbContext = EMSDb;
        }

        public IEnumerable<Event> GetEventForntEnd(int EventId, string Country, int startIndex, int endIndex, string name)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetEventFrontEnd(EventId, Country, startIndex, endIndex,name).Select(
                p => new Event
                {
                    EventId = p.Event_Id,
                    Name = p.Name,
                    IsActive = p.Is_Active,
                    StartIndex = startIndex,
                    EndIndex = endIndex,
                    Picture = p.Picture != null ? p.Picture : "~/Images/profile-default1.png",
                    
                });
        }

        public IEnumerable<CompanyProfile> GetCompanyProfileFrontEnd(int CategoryId, string Country, int startIndex,
            int endIndex)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetCompanyProfileFrontEnd(CategoryId, Country, startIndex, endIndex).Select(
                p => new CompanyProfile
                {
                    CompanyProfileId = p.Company_Profile_Id,
                    Name = p.Name,
                    CompanyProfilePhoto = p.Company_Profile_Photo!= null ? p.Company_Profile_Photo : "~/Images/profile-default1.png",
                    StartIndex = startIndex,
                    EndIndex = endIndex

                });
        }

        public IEnumerable<CompanyProfile> GetCompanyProfileFrontEndCount(string country)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetCompanyProfileFrontEndCount(country).Select(
                t => new CompanyProfile
                {
                    Total = t.TotalItem.Value
                });
        }

        public IEnumerable<PreDefinedTask> GetPreDefinedTask(int EventId, string Country, int startIndex,
            int endIndex)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetPreDefinedTask(EventId, Country, startIndex, endIndex).Select(
                p => new PreDefinedTask
                {
                    PreDefinedTaskId = p.Pre_Defined_Task_id,
                    Title = p.Title,
                    Name = p.Name,
                    CompanyProfileId = p.Company_profile_id.Value,
                    StartIndex = startIndex,
                    EndIndex = endIndex

                });
        }


        public IEnumerable<PersonalEvent> GetPersonalEvent(int EventId, String UserId)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetPersonalEvent(EventId, UserId).Select(
                p => new PersonalEvent
                {
                    PersonalEventId = p.Personal_Event_Id,
                    Name = p.Name,
                    Description = p.Description,
                    Status = p.Status.Value,
                    Picture = p.Picture,
                    Counrty = p.Country,
                    EventId = p.Event_Id,
                    UserId = p.User_Id

                });
        }

        public IEnumerable<Guest> GetGuestList(int eventId, string userId)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetGuestList(eventId, userId).Select(
                p => new Guest
                {
                    GuestListId = p.Guest_List_Id,
                    Name = p.Name,
                    Type = p.Type,
                    EventId = p.Event_Id,
                    UserId = p.User_Id,
                    Done = p.Done.Value,
                    NoOfPeople = p.NoOfPeople.Value

                });
        }

        public IEnumerable<ManageTicket> GetManageTicket(int eventId, string userId)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetManageTicket(eventId, userId).Select(
                p => new ManageTicket
                {
                    ManageTicketId = p.Manage_Ticket_Id,
                    Description = p.Description,
                    Price = p.Price.Value,
                    EventId = p.Event_Id,
                    UserId = p.User_Id

                });
        }

        public PreDefinedTask GetPreDefinedTaskFrontEnd(int EventId, string Country)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetPreDefinedTaskFrontEnd(EventId, Country).Select(
                p => new PreDefinedTask
                {
                    TotalItem = p.TotalItem.Value


                }).FirstOrDefault();
        }

        public IEnumerable<PreDefinedTaskList> GetPreDefinedTaskList(int Id)
        {
            //if list inside list use select many(ex- department include employees) otherwice use select
            return _DbContext.GetPreDefinedTaskList(Id).Select(p => new PreDefinedTaskList
            {
                Pre_Defined_Task_List_Id = p.Pre_Defined_Task_List_Id,
                Description = p.Description,
                Month = p.Month.Value,
                Person = p.Person
            });
        }

        public IEnumerable<PreDefinedTask> GetFavouritePreDefinedTask(string userId, int eventId)
        {
            return _DbContext.GetFavouritePreDefinedTask(userId, eventId).Select(f => new PreDefinedTask
            {
                PreDefinedTaskId = f.Pre_Defined_Task_Id.Value,
                Title = f.Title
            });
        }

        public IEnumerable<PreDefinedTaskListTrack> GetPreDefinedTaskListTrack(int preDefinedId, string userId)
        {
            return _DbContext.GetPreDefinedTaskListTrack(preDefinedId, userId).Select(f => new PreDefinedTaskListTrack
            {
                Pre_Defined_Task_List_Track_Id = f.Pre_Defined_Task_List_Track_Id,
                Pre_Defined_Task_List_Id = f.Pre_Defined_Task_List_Id.Value,
                Done = f.Done
            });
        }

        public bool InsertPersonalEvent(String Name, String Description, bool Status, 
            String Picture, String Country, int EventId, String UserId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertPersonalEvent(Name, Description, Status, Picture, Country, EventId, UserId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertManageTicket(int EventId, string userId, String Description, decimal Price)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertManageTicket(Description, Price, userId, EventId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertGuestList(int EventId, string userId, String name, String type, int noOfPeople = 1)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertGuestList(EventId, userId, name, type, noOfPeople, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertPreDefinedTaskListTrack(int preDefinedId, string userId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertPreDefinedTaskListTrack(preDefinedId, userId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertFavouritePreDefinedTask(FavouritePreDefinedTask fav)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertFavouritePreDefinedTask(fav.UserId, fav.PreDefinedTaskId, fav.EventId, Success, Message);

            bool success = (bool)Success.Value;
            return success;
        }

        public User GetUser(String userId)
        {
            return _DbContext.GetUser(userId).Select(u => new User
            {
                FirstName = u.First_Name,
                LastName = u.Last_Name,
                Country = u.Country,
                ProfilePhoto = u.Profile_Photo != null ? u.Profile_Photo : "Images/profile-default.png"
            }).FirstOrDefault();
        }

        public UserDefinedTaskDate GetDate(int eventId, string userId)
        {
            return _DbContext.GetUserDefinedTaskDate(eventId, userId).Select(d => new UserDefinedTaskDate
            {
                NewDate = d.date,
                EventId = d.event_id

            }).FirstOrDefault();
        }

        public bool UpdatePersonalEvent(int PersonalEventId, String Name, String Description,
            bool Status, String Picture, String Country)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdatePersonalEvent(PersonalEventId, Name, Description, Status, Picture, Country, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        public bool UpdateUserDefinedTaskDate(DateTime date, int eventId, string userId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateUserDefinedTaskDate(date, eventId, userId, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        public bool UpdateUserDetails(User userDetails, String userId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateUserDetails(userId, userDetails.FirstName, userDetails.LastName, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }


        public bool UpdateUserProfilePhoto(UserProfilePhoto userDetails, String userId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateUserProfilePhoto(userId, userDetails.ProfilePhoto, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        public bool DeletePersonalEvent(int PersonalEventId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeletePersonalEvent(PersonalEventId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool DeleteFavouritePreDefinedTask(int PreDefinedTaskId, string UserId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteFavouritePreDefinedTask(PreDefinedTaskId, UserId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool DeleteManageTicket(int ManageTicketId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteManageTicket(ManageTicketId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool DeleteGuestList(int guestListId, string userId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteGuestList(guestListId, userId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertUserDefinedTaskDate(DateTime date, int eventId, string userId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertUserDefinedTaskDate(date, eventId, userId, Success, Message);

            bool success = (bool)Success.Value;
            return success;
        }

        //user functions end

        public bool InsertCompanyProfile(CompanyProfile com)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");
            ObjectParameter CompanyProfileId = new ObjectParameter("CompanyProfileId", 0);

            _DbContext.InsertCompanyProfile(com.Name, com.Address, com.City, com.Phone,
                com.Email, com.Url, com.Description, com.CompanyProfilePhoto,
                com.CompanyCategoryId, com.UserId, com.PositionId, CompanyProfileId, Success, Message);

            int companyProfileId = (int)CompanyProfileId.Value;
            bool success = (bool)Success.Value;
            return success;
        }

        public IEnumerable<CompanyProfile> GetCompanyProfileByCompanyId(int companyId)
        {
            return _DbContext.GetCompanyProfileById(companyId).Select(c =>
                new CompanyProfile
                {
                    CompanyProfileId = c.Company_Profile_Id,
                    Name = c.Name,
                    Address = c.Address,
                    City = c.City,
                    Description = c.Description,
                    Email = c.Email,
                    Phone = c.Phone,
                    CompanyProfilePhoto = c.Company_Profile_Photo != null ? c.Company_Profile_Photo : "~/Images/profile-default1.png",
                    Url = c.Url,
                    
                }
                );
        }

        public IEnumerable<CompanyProfile> GetCompanyProfileById(string UserId, int CompanyProfileId = 0)
        {
            return _DbContext.GetCompanyProfile(UserId, CompanyProfileId).Select(c =>
                new CompanyProfile
                {
                    CompanyProfileId = c.Company_Profile_Id,
                    Name = c.Name,
                    Address = c.Address,
                    City = c.City,
                    Description = c.Description,
                    Email = c.Email,
                    Phone = c.Phone,
                    CompanyProfilePhoto =c.Company_Profile_Photo != null ? c.Company_Profile_Photo : "~/Images/profile-default1.png",
                    CompanyCategoryId = c.Company_Category_Id,
                    PositionId = c.Position_Id,
                    IsActive = c.Is_Active,
                    Url = c.Url,
                    CompanyUserId = c.Company_User_Id,
                    UserId = c.User_Id
                }
                );
        }

        public IEnumerable<CompanyProfile> GetCompanyProfilesByUserId(string UserId)
        {
            var profiles = _DbContext.GetCompanyProfilesByUserId(UserId).AsQueryable().ToList();
            List<CompanyProfile> companyProfiles = new List<CompanyProfile>();
            if (profiles != null && profiles.Count() > 0)
            {
                foreach (var c in profiles)
                {
                    companyProfiles.Add(new CompanyProfile
                    {
                        CompanyProfileId = c.Company_Profile_Id,
                        Name = c.Name,
                        Address = c.Address,
                        City = c.City,
                        Description = c.Description,
                        Email = c.Email,
                        Phone = c.Phone,
                        CompanyProfilePhoto = c.Company_Profile_Photo ?? "~/Images/Gallery10131514028411865191.jpeg",
                        CompanyCategoryId = c.Company_Category_Id,
                        IsActive = c.Is_Active,
                        Url = c.Url,
                        PositionId = c.Position_Id,
                        CompanyUserId = c.Company_User_Id,
                        UserId = c.User_Id,
                        CategoryName = c.Category_Name,
                        PositionName = c.Position_Name
                    });
                }
            }


            return companyProfiles;
        }

        public bool UpdateCompanyProfile(CompanyProfile com)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateCompanyProfile(com.Name, com.Address, com.City, com.Phone,
                com.Email, com.Url, com.Description, com.CompanyProfilePhoto,
                com.CompanyCategoryId, com.UserId, com.CompanyProfileId, com.PositionId, Success, Message);

            bool success = (bool)Success.Value;
            return success;

        }

        public bool DeleteCompanyProfile(int CompanyProfileId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteCompanyProfile(CompanyProfileId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertCompanyCategory(CompanyCategory com)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertCompanyCategory(com.Name, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public IEnumerable<CompanyCategory> GetCompanyCategoryById(int id = 0)
        {
            var categories = _DbContext.GetCompanyCategory(id).AsQueryable().ToList();
            List<CompanyCategory> companyCategories = new List<CompanyCategory>();
            if (categories != null && categories.Count() > 0)
            {
                foreach (var c in categories)
                {
                    companyCategories.Add(new CompanyCategory
                    {
                        CompanyCategoryId = c.Company_Category_Id,
                        Name = c.Name,
                        IsActive = c.Is_Active

                    });
                }
            }

            return companyCategories;
        }


        public bool UpdateCompanyCategory(CompanyCategory com)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateCompanyCategory(com.Name, com.IsActive, com.CompanyCategoryId, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        public bool DeleteCompanyCategory(int CompanyCategoryId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteCompanyCategory(CompanyCategoryId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertPosition(Position pos)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertPosition(pos.Name, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public IEnumerable<Position> GetPositionById(int id = 0)
        {
            var positions = _DbContext.GetPosition(id).AsQueryable().ToList();
            List<Position> Positions = new List<Position>();
            if (positions != null && positions.Count() > 0)
            {
                foreach (var p in positions)
                {
                    Positions.Add(new Position
                    {
                        PositionId = p.Position_Id,
                        Name = p.Name,
                        IsActive = p.Is_Active

                    });
                }
            }

            return Positions;
        }


        public bool UpdatePosition(Position pos)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdatePosition(pos.Name, pos.PositionId, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        public bool DeletePosition(int PositionId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeletePosition(PositionId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertService(Service ser)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertService(ser.Description, ser.CompanyProfileId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public IEnumerable<Service> GetServiceById(int ServiceId = 0)
        {
            var services = _DbContext.GetService(ServiceId).AsQueryable().ToList();
            List<Service> Services = new List<Service>();
            if (services != null && services.Count() > 0)
            {
                foreach (var s in services)
                {
                    Services.Add(new Service
                    {
                        ServiceId = s.Service_Id,
                        Description = s.Description,
                        CompanyProfileId = s.Company_Profile_Id,
                        IsActive = s.Is_Active

                    });
                }
            }

            return Services;
        }

        public IEnumerable<Service> GetServiceByCompanyProfileId(int CompanyProfileId = 0)
        {
            var services = _DbContext.GetServiceByCompanyProfileId(CompanyProfileId).AsQueryable().ToList();
            List<Service> Services = new List<Service>();
            if (services != null && services.Count() > 0)
            {
                foreach (var s in services)
                {
                    Services.Add(new Service
                    {
                        ServiceId = s.Service_Id,
                        Description = s.Description,
                        CompanyProfileId = s.Company_Profile_Id,
                        IsActive = s.Is_Active

                    });
                }
            }

            return Services;
        }


        public bool UpdateService(Service ser)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateService(ser.Description, ser.ServiceId, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        public bool DeleteService(int ServiceId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteService(ServiceId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertPackage(Package pak)
        {
            var connection = string.Copy(System.Configuration.ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString);

            SqlParameter Success;
            SqlParameter Message;

            SqlCommand command;

            using (SqlConnection Connection = new SqlConnection(connection))
            {
                Connection.Open();
                using (command = Connection.CreateCommand())
                {
                    command.CommandText = "dbo.InsertPackage";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter sparameter;
                    SqlParameter eparameter;

                    sparameter = command.Parameters
                                      .AddWithValue("@ServiceIds", CreateDataTable<int>(pak.ServiceIds, "ServiceId"));
                    eparameter = command.Parameters
                                      .AddWithValue("@EventIds", CreateDataTable<int>(pak.EventIds, "EventId"));

                    command.Parameters
                                      .AddWithValue("@Name", pak.Name);
                    command.Parameters
                                      .AddWithValue("@Price", pak.Price);
                    command.Parameters
                                      .AddWithValue("@Limit", pak.Limit);
                    Success = command.Parameters.AddWithValue("@Success", false);
                    Message = command.Parameters.Add("@Message", SqlDbType.VarChar, 500);

                    Message.Direction = ParameterDirection.Output;
                    Success.Direction = ParameterDirection.Output;

                    sparameter.SqlDbType = SqlDbType.Structured;
                    sparameter.TypeName = "dbo.ServicePackageTypex";

                    eparameter.SqlDbType = SqlDbType.Structured;
                    eparameter.TypeName = "dbo.EventPackageTypex";

                    command.ExecuteNonQuery();
                }
            }

            bool success = (bool)command.Parameters["@Success"].Value;
            var message = command.Parameters["@Message"].Value.ToString();
            return success;
        }

        public bool UpdatePackage(Package pak)
        {
            var connection = string.Copy(System.Configuration.ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString);

            SqlParameter Success;
            SqlParameter Message;

            SqlCommand command;

            using (SqlConnection Connection = new SqlConnection(connection))
            {
                Connection.Open();
                using (command = Connection.CreateCommand())
                {
                    command.CommandText = "dbo.UpdatePackage";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter sparameter;
                    SqlParameter eparameter;

                    sparameter = command.Parameters
                                      .AddWithValue("@ServiceIds", CreateDataTable<int>(pak.ServiceIds, "ServiceId"));
                    eparameter = command.Parameters
                                      .AddWithValue("@EventIds", CreateDataTable<int>(pak.EventIds, "EventId"));
                    command.Parameters
                                      .AddWithValue("@PackageId", pak.PackageId);
                    command.Parameters
                                      .AddWithValue("@Name", pak.Name);
                    command.Parameters
                                      .AddWithValue("@Price", pak.Price);
                    command.Parameters
                                      .AddWithValue("@Limit", pak.Limit);
                    Success = command.Parameters.AddWithValue("@Success", false);
                    Message = command.Parameters.Add("@Message", SqlDbType.VarChar, 500);

                    Message.Direction = ParameterDirection.Output;
                    Success.Direction = ParameterDirection.Output;

                    sparameter.SqlDbType = SqlDbType.Structured;
                    sparameter.TypeName = "dbo.ServicePackageTypex";

                    eparameter.SqlDbType = SqlDbType.Structured;
                    eparameter.TypeName = "dbo.EventPackageTypex";

                    command.ExecuteNonQuery();
                }
            }

            bool success = (bool)command.Parameters["@Success"].Value;
            var message = command.Parameters["@Message"].Value.ToString();
            return success;
        }

        public IEnumerable<Package> GetPackageById(int id = 0)
        {
            var packages = _DbContext.GetPackage(id).AsQueryable().ToList();
            List<Package> Packages = new List<Package>();
            if (packages != null && packages.Count() > 0)
            {
                foreach (var p in packages)
                {
                    Packages.Add(new Package
                    {
                        PackageId = p.Package_Id,
                        Name = p.Name,
                        IsActive = p.Is_Active,
                        Price = p.Price.HasValue ? p.Price.Value : 0,
                        Limit = p.Limit.HasValue ? p.Limit.Value : 0,
                        EventIds = p.EventIds.Split(',').Select(e => int.Parse(e)).ToList(),
                        ServiceIds = p.ServiceIds.Split(',').Select(e => int.Parse(e)).ToList()
                    });
                }
            }

            return Packages;
        }

        public IEnumerable<Package> GetPackagesByEventId(int id)
        {
            var packages = _DbContext.GetPackageByEventId(id).AsQueryable().ToList();
            List<Package> Packages = new List<Package>();
            if (packages != null && packages.Count() > 0)
            {
                foreach (var p in packages)
                {
                    Packages.Add(new Package
                    {
                        PackageId = p.Package_Id,
                        Name = p.Name,
                        IsActive = p.Is_Active,
                        Price = p.Price.HasValue ? p.Price.Value : 0,
                        Limit = p.Limit.HasValue ? p.Limit.Value : 0,
                        EventIds = p.EventIds.Split(',').Select(e => int.Parse(e)).ToList(),
                        ServiceIds = p.ServiceIds.Split(',').Select(e => int.Parse(e)).ToList()
                    });
                }
            }

            return Packages;
        }

        public IEnumerable<Package> GetPackagesByServiceId(int id)
        {
            var packages = _DbContext.GetPackageByServiceId(id).AsQueryable().ToList();
            List<Package> Packages = new List<Package>();
            if (packages != null && packages.Count() > 0)
            {
                foreach (var p in packages)
                {
                    Packages.Add(new Package
                    {
                        PackageId = p.Package_Id,
                        Name = p.Name,
                        IsActive = p.Is_Active,
                        Price = p.Price.HasValue ? p.Price.Value : 0,
                        Limit = p.Limit.HasValue ? p.Limit.Value : 0,
                        EventIds = p.EventIds.Split(',').Select(e => int.Parse(e)).ToList(),
                        ServiceIds = p.ServiceIds.Split(',').Select(e => int.Parse(e)).ToList()
                    });
                }
            }

            return Packages;
        }

        public bool DeletePackage(int PackageId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeletePackage(PackageId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertEvent(Event ev)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.InsertEvent(ev.Name, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public IEnumerable<Event> GetEventById(int id = 0)
        {

            var events = _DbContext.GetEvent(id).AsQueryable().ToList();
            List<Event> Events = new List<Event>();
            if (events != null && events.Count() > 0)
            {
                foreach (var p in events)
                {
                    Events.Add(new Event
                    {
                        EventId = p.Event_Id,
                        Name = p.Name,
                        IsActive = p.Is_Active



                    });

                }
            }

            return Events;
        }


        public bool UpdateEvent(Event ev)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateEvent(ev.Name, ev.EventId, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        public bool DeleteEvent(int EventId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteEvent(EventId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public bool InsertGallery(int CompanyProfileId, IEnumerable<string> urls)
        {
            var connection = string.Copy(System.Configuration.ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString);

            SqlParameter Success;
            SqlParameter Message;

            SqlCommand command;

            using (SqlConnection Connection = new SqlConnection(connection))
            {
                Connection.Open();
                using (command = Connection.CreateCommand())
                {
                    command.CommandText = "dbo.InsertGallery";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter sparameter;

                    sparameter = command.Parameters
                                      .AddWithValue("@Urls", CreateDataTable<string>(urls, "Url"));

                    command.Parameters
                                      .AddWithValue("@CompanyProfileId", CompanyProfileId);

                    Success = command.Parameters.AddWithValue("@Success", false);
                    Message = command.Parameters.Add("@Message", SqlDbType.VarChar, 500);

                    Message.Direction = ParameterDirection.Output;
                    Success.Direction = ParameterDirection.Output;

                    sparameter.SqlDbType = SqlDbType.Structured;
                    sparameter.TypeName = "dbo.GalleryType";



                    command.ExecuteNonQuery();
                }
            }

            bool success = (bool)command.Parameters["@Success"].Value;
            var message = command.Parameters["@Message"].Value.ToString();
            return success;

        }

        public Gallery GetGalleryById(int id)
        {
            var Gallery = _DbContext.GetGallery(id).Select(e => new Gallery
            {
                GalleryId = e.Gallery_Id,
                CompanyProfileId = e.Company_Profile_Id,
                Url = e.Url,
                IsActive = e.Is_Active
            }).FirstOrDefault();

            return Gallery;
        }

        public bool DeleteGallery(int GalleryId)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.DeleteGallery(GalleryId, Success, Message);
            bool success = (bool)Success.Value;
            return success;
        }

        public IEnumerable<Gallery> GetGalleryByProfileId(int id)
        {
            var gallery = _DbContext.GetGalleryByCompanyProfileId(id).ToList();

            List<Gallery> Gallery = new List<Gallery>();
            if (gallery != null && gallery.Count() > 0)
            {
                foreach (var g in gallery)
                {
                    Gallery.Add(new Gallery
                    {
                        GalleryId = g.Gallery_Id,
                        IsActive = g.Is_Active,
                        CompanyProfileId = g.Company_Profile_Id,
                        Url = g.Url
                    });
                }
            }
            return Gallery;
        }

        public CompanyProfilePhoto GetCompanyProfilePhotoById(int id)
        {
            var p = _DbContext.GetCompanyProfilePhoto(id).AsQueryable().ToList().FirstOrDefault();
            var photo = new CompanyProfilePhoto()
            {
                CompanyProfileId = p.Company_Profile_Id,
                ProfilePhoto = p.Company_Profile_Photo
            };

            return photo;
        }


        public bool UpdateCompanyProfilePhoto(CompanyProfilePhoto cp)
        {
            ObjectParameter Success = new ObjectParameter("Success", false);
            ObjectParameter Message = new ObjectParameter("Message", "");

            _DbContext.UpdateCompanyProfilePhoto(cp.ProfilePhoto, cp.CompanyProfileId, Success, Message);
            bool success = (bool)Success.Value;
            return success;

        }

        private static DataTable CreateDataTable<T>(IEnumerable<T> Ids, string ColumnName)
        {
            DataTable table = new DataTable();
            table.Columns.Add(ColumnName, typeof(T));
            foreach (var id in Ids)
            {
                table.Rows.Add(id);
            }

            return table;
        }

    }

}