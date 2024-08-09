using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RailWay
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                //Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.WriteLine("***************************************");
                Console.WriteLine("*    Welcome to Railway Reservation   *");
                Console.WriteLine("***************************************");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Register();
                }
                else if (choice == "2")
                {
                    Login();
                }
                else if (choice == "3")
                {
                    break;
                }
            }
        }

        private static void Register()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("***************************************");
            Console.WriteLine("*             Register                *");
            Console.WriteLine("***************************************");
            Console.Write("Enter Username: ");
            var username = Console.ReadLine();

            Console.Write("Enter Password: ");
            var password = Console.ReadLine();

            Console.Write("Are you registering as an Admin or User? (A/U): ");
            var role = Console.ReadLine().ToUpper();

            string query = role == "A" ?
                "INSERT INTO Admins (Username, Password) VALUES (@Username, @Password)" :
                "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";

            var parameters = new[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                Console.WriteLine("Registration successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("***************************************");
            Console.WriteLine("*              Login                  *");
            Console.WriteLine("***************************************");
            Console.Write("Enter Username: ");
            var username = Console.ReadLine();

            Console.Write("Enter Password: ");
            var password = Console.ReadLine();

            Console.Write("Are you logging in as an Admin or User? (A/U): ");
            var role = Console.ReadLine().ToUpper();

            string query = role == "A" ?
                "SELECT AdminId FROM Admins WHERE Username = @Username AND Password = @Password" :
                "SELECT UserId FROM Users WHERE Username = @Username AND Password = @Password";

            var parameters = new[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count > 0)
            {
                if (role == "A")
                {
                    AdminMenu();
                }
                else
                {
                    var userId = (int)table.Rows[0]["UserId"];
                    UserMenu(userId);
                }
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void UserMenu(int userId)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("***************************************");
                Console.WriteLine("*              User Menu              *");
                Console.WriteLine("***************************************");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("1. Search Trains");
                Console.WriteLine("2. Book Tickets");
                Console.WriteLine("3. Cancel Tickets");
                Console.WriteLine("4. View Bookings");
                Console.WriteLine("5. Logout");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    SearchTrain();
                }
                else if (choice == "2")
                {
                    BookTickets(userId);
                }
                else if (choice == "3")
                {
                    CancelTickets(userId);
                }
                else if (choice == "4")
                {
                    ViewBookings(userId);
                }
                else if (choice == "5")
                {
                    break;
                }
            }
        }

        private static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("***************************************");
                Console.WriteLine("*             Admin Menu              *");
                Console.WriteLine("***************************************");
                Console.ResetColor();
                Console.WriteLine("1. Add Train");
                Console.WriteLine("2. Update Train");
                Console.WriteLine("3. Delete Train");
                Console.WriteLine("4. View All Bookings");
                Console.WriteLine("5. View All Cancellations");
                Console.WriteLine("6. Manage Users");
                Console.WriteLine("7. Logout");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    AddTrain();
                }
                else if (choice == "2")
                {
                    UpdateTrain();
                }
                else if (choice == "3")
                {
                    DeleteTrain();
                }
                else if (choice == "4")
                {
                    ViewAllBookings();
                }
                else if (choice == "5")
                {
                    ViewAllCancellations();
                }
                else if (choice == "6")
                {
                    ManageUsers();
                }
                else if (choice == "7")
                {
                    break;
                }
            }
        }

        private static void SearchTrain()
        {
            Console.Clear();
            Console.Write("Enter Source Station: ");
            var fromStation = Console.ReadLine();

            Console.Write("Enter Destination Station: ");
            var destStation = Console.ReadLine();

            Console.Write("Enter Date of Travel (yyyy-mm-dd): ");
            var dateOfTravel = DateTime.Parse(Console.ReadLine());

            var query = "SELECT Tno, Tname, ClassOfTravel, SeatsAvailable, Price, TrainStatus, DateOfTravel FROM Trains WHERE FromStation = @FromStation AND DestStation = @DestStation AND DateOfTravel = @DateOfTravel";
            var parameters = new[]
            {
        new SqlParameter("@FromStation", fromStation),
        new SqlParameter("@DestStation", destStation),
        new SqlParameter("@DateOfTravel", dateOfTravel)
    };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);

            if (table.Rows.Count == 0)
            {
                Console.WriteLine("No trains found for the specified route and date.");
            }
            else
            {
                Console.WriteLine("Train Number | Train Name | Class of Travel | Seats Available | Price | Train Status | Date of Travel");
                foreach (DataRow row in table.Rows)
                {
                    var tno = row["Tno"];
                    var tname = row["Tname"];
                    var classOfTravel = row["ClassOfTravel"];
                    var seatsAvailable = row["SeatsAvailable"];
                    var price = row["Price"];
                    var trainStatus = row["TrainStatus"];
                    var date = row["DateOfTravel"];

                    Console.WriteLine($"{tno} | {tname} | {classOfTravel} | {seatsAvailable} | {price} | {trainStatus} | {date:d}");
                }
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void BookTickets(int userId)
        {
            Console.Clear();
            Console.Write("Enter Train Number: ");
            var tno = int.Parse(Console.ReadLine());

            Console.Write("Enter Class of Travel: ");
            var classOfTravel = Console.ReadLine();

            Console.Write("Enter Date of Travel (yyyy-mm-dd): ");
            var dateOfTravel = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Number of Seats: ");
            var numberOfSeats = int.Parse(Console.ReadLine());

            if (numberOfSeats > 3)
            {
                Console.WriteLine("You can book a maximum of 3 tickets at a time.");
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
                return;
            }

            var query = "SELECT SeatsAvailable, TrainStatus, Price FROM Trains WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel AND DateOfTravel = @DateOfTravel";
            var parameters = new[]
            {
        new SqlParameter("@Tno", tno),
        new SqlParameter("@ClassOfTravel", classOfTravel),
        new SqlParameter("@DateOfTravel", dateOfTravel)
    };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count == 0)
            {
                Console.WriteLine("Train or class of travel not found.");
                Console.ReadLine();
                return;
            }

            var seatsAvailable = (int)table.Rows[0]["SeatsAvailable"];
            var trainStatus = table.Rows[0]["TrainStatus"].ToString();
            var pricePerSeat = (decimal)table.Rows[0]["Price"];

            if (trainStatus.ToLower() != "active")
            {
                Console.WriteLine("The train is not active and cannot be booked.");
                Console.ReadLine();
                return;
            }

            if (numberOfSeats > seatsAvailable)
            {
                Console.WriteLine("Not enough seats available.");
                Console.ReadLine();
                return;
            }

            var insertBookingQuery = "INSERT INTO Bookings (Tno, ClassOfTravel, UserId, NumberOfSeats, BookingDate, DateOfTravel) VALUES (@Tno, @ClassOfTravel, @UserId, @NumberOfSeats, @BookingDate, @DateOfTravel)";
            var insertBookingParameters = new[]
            {
        new SqlParameter("@Tno", tno),
        new SqlParameter("@ClassOfTravel", classOfTravel),
        new SqlParameter("@UserId", userId),
        new SqlParameter("@NumberOfSeats", numberOfSeats),
        new SqlParameter("@BookingDate", DateTime.Now),
        new SqlParameter("@DateOfTravel", dateOfTravel)
    };

            var updateSeatsQuery = "UPDATE Trains SET SeatsAvailable = SeatsAvailable - @NumberOfSeats WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel AND DateOfTravel = @DateOfTravel";
            var updateSeatsParameters = new[]
            {
        new SqlParameter("@NumberOfSeats", numberOfSeats),
        new SqlParameter("@Tno", tno),
        new SqlParameter("@ClassOfTravel", classOfTravel),
        new SqlParameter("@DateOfTravel", dateOfTravel)
    };

            try
            {
                DatabaseHelper.ExecuteNonQuery(insertBookingQuery, insertBookingParameters);
                DatabaseHelper.ExecuteNonQuery(updateSeatsQuery, updateSeatsParameters);
                Console.WriteLine("Booking successful.");

                // Retrieve the booking details to display
                var bookingDetailsQuery = "SELECT TOP 1 BookingId, Tno, ClassOfTravel, UserId, NumberOfSeats, BookingDate, DateOfTravel FROM Bookings WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel AND UserId = @UserId ORDER BY BookingId DESC";
                var bookingDetailsParameters = new[]
                {
            new SqlParameter("@Tno", tno),
            new SqlParameter("@ClassOfTravel", classOfTravel),
            new SqlParameter("@UserId", userId)
        };

                var bookingTable = DatabaseHelper.ExecuteQuery(bookingDetailsQuery, bookingDetailsParameters);
                if (bookingTable.Rows.Count > 0)
                {
                    var booking = bookingTable.Rows[0];
                    var bookingId = (int)booking["BookingId"];
                    var bookedTno = (int)booking["Tno"];
                    var bookedClassOfTravel = booking["ClassOfTravel"].ToString();
                    var bookedUserId = (int)booking["UserId"];
                    var bookedNumberOfSeats = (int)booking["NumberOfSeats"];
                    var bookedBookingDate = (DateTime)booking["BookingDate"];
                    var bookedDateOfTravel = (DateTime)booking["DateOfTravel"];
                    var totalAmount = bookedNumberOfSeats * pricePerSeat;

                    Console.WriteLine("Booking Details:");
                    Console.WriteLine($"Booking ID: {bookingId}");
                    Console.WriteLine($"Train Number: {bookedTno}");
                    Console.WriteLine($"Class of Travel: {bookedClassOfTravel}");
                    Console.WriteLine($"User ID: {bookedUserId}");
                    Console.WriteLine($"Number of Seats: {bookedNumberOfSeats}");
                    Console.WriteLine($"Booking Date: {bookedBookingDate}");
                    Console.WriteLine($"Date of Travel: {bookedDateOfTravel}");
                    Console.WriteLine($"Total Amount: {totalAmount}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void CancelTickets(int userId)
        {
            Console.Clear();
            Console.Write("Enter Booking ID: ");
            var bookingId = int.Parse(Console.ReadLine());

            Console.Write("Enter Number of Seats to Cancel: ");
            var numberOfSeats = int.Parse(Console.ReadLine());

            var query = "SELECT NumberOfSeats, Tno FROM Bookings WHERE BookingId = @BookingId AND UserId = @UserId";
            var parameters = new[]
            {
        new SqlParameter("@BookingId", bookingId),
        new SqlParameter("@UserId", userId)
    };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count == 0)
            {
                Console.WriteLine("Booking not found or you do not have permission to cancel it.");
                Console.ReadLine();
                return;
            }

            var bookedSeats = (int)table.Rows[0]["NumberOfSeats"];
            var tno = (int)table.Rows[0]["Tno"];
            if (numberOfSeats > bookedSeats)
            {
                Console.WriteLine("Cannot cancel more seats than booked.");
                Console.ReadLine();
                return;
            }

            var updateSeatsQuery = "UPDATE Trains SET SeatsAvailable = SeatsAvailable + @NumberOfSeats WHERE Tno = @Tno";
            var updateSeatsParameters = new[]
            {
        new SqlParameter("@NumberOfSeats", numberOfSeats),
        new SqlParameter("@Tno", tno)
    };

            var updateBookingQuery = "UPDATE Bookings SET NumberOfSeats = NumberOfSeats - @NumberOfSeats WHERE BookingId = @BookingId AND UserId = @UserId";
            var updateBookingParameters = new[]
            {
        new SqlParameter("@NumberOfSeats", numberOfSeats),
        new SqlParameter("@BookingId", bookingId),
        new SqlParameter("@UserId", userId)
    };

            var insertCancellationQuery = "INSERT INTO Cancellations (BookingId, NumberOfSeatsCancelled, CancellationDate) VALUES (@BookingId, @NumberOfSeatsCancelled, @CancellationDate)";
            var insertCancellationParameters = new[]
            {
        new SqlParameter("@BookingId", bookingId),
        new SqlParameter("@NumberOfSeatsCancelled", numberOfSeats),
        new SqlParameter("@CancellationDate", DateTime.Now)
    };

            try
            {
                DatabaseHelper.ExecuteNonQuery(updateSeatsQuery, updateSeatsParameters);
                DatabaseHelper.ExecuteNonQuery(updateBookingQuery, updateBookingParameters);
                DatabaseHelper.ExecuteNonQuery(insertCancellationQuery, insertCancellationParameters);

                // If all seats are canceled, delete the booking
                if (bookedSeats == numberOfSeats)
                {
                    var deleteBookingQuery = "DELETE FROM Bookings WHERE BookingId = @BookingId AND UserId = @UserId";
                    var deleteBookingParameters = new[]
                    {
                new SqlParameter("@BookingId", bookingId),
                new SqlParameter("@UserId", userId)
            };

                    DatabaseHelper.ExecuteNonQuery(deleteBookingQuery, deleteBookingParameters);
                }

                Console.WriteLine("Cancellation successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }



        private static void ViewBookings(int userId)
        {
            Console.Clear();
            var query = "SELECT * FROM Bookings WHERE UserId = @UserId";
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId)
            };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"Booking ID: {row["BookingId"]}, Train No: {row["Tno"]}, Seats: {row["NumberOfSeats"]}, Date: {row["BookingDate"]}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void AddTrain()
        {
            Console.Clear();
            Console.Write("Enter Train Number: ");
            var tno = int.Parse(Console.ReadLine());

            Console.Write("Enter Train Name: ");
            var tname = Console.ReadLine();

            Console.Write("Enter Source Station: ");
            var fromStation = Console.ReadLine();

            Console.Write("Enter Destination Station: ");
            var destStation = Console.ReadLine();

            Console.Write("Enter Class of Travel: ");
            var classOfTravel = Console.ReadLine();

            Console.Write("Enter Date of Travel (yyyy-mm-dd): ");
            var dateOfTravel = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Number of Seats Available: ");
            var seatsAvailable = int.Parse(Console.ReadLine());

            Console.Write("Enter Ticket Price: ");
            var price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Train Status (Active/Inactive): ");
            var trainStatus = Console.ReadLine();

            var query = "INSERT INTO Trains (Tno, ClassOfTravel, Tname, FromStation, DestStation, DateOfTravel, SeatsAvailable, Price, TrainStatus) VALUES (@Tno, @ClassOfTravel, @Tname, @FromStation, @DestStation, @DateOfTravel, @SeatsAvailable, @Price, @TrainStatus)";
            var parameters = new[]
            {
        new SqlParameter("@Tno", tno),
        new SqlParameter("@ClassOfTravel", classOfTravel),
        new SqlParameter("@Tname", tname),
        new SqlParameter("@FromStation", fromStation),
        new SqlParameter("@DestStation", destStation),
        new SqlParameter("@DateOfTravel", dateOfTravel),
        new SqlParameter("@SeatsAvailable", seatsAvailable),
        new SqlParameter("@Price", price),
        new SqlParameter("@TrainStatus", trainStatus)
    };

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                Console.WriteLine("Train added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void UpdateTrain()
        {
            Console.Clear();
            Console.Write("Enter Train Number to Update: ");
            var tno = int.Parse(Console.ReadLine());

            Console.Write("Enter New Train Name (or leave blank to keep current): ");
            var tname = Console.ReadLine();

            Console.Write("Enter New Source Station (or leave blank to keep current): ");
            var fromStation = Console.ReadLine();

            Console.Write("Enter New Destination Station (or leave blank to keep current): ");
            var destStation = Console.ReadLine();

            Console.Write("Enter New Price (or leave blank to keep current): ");
            var priceInput = Console.ReadLine();
            decimal? price = string.IsNullOrEmpty(priceInput) ? (decimal?)null : decimal.Parse(priceInput);

            Console.Write("Enter New Seats Available (or leave blank to keep current): ");
            var seatsInput = Console.ReadLine();
            int? seatsAvailable = string.IsNullOrEmpty(seatsInput) ? (int?)null : int.Parse(seatsInput);

            var updates = new List<string>();
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tname))
            {
                updates.Add("Tname = @Tname");
                parameters.Add(new SqlParameter("@Tname", tname));
            }
            if (!string.IsNullOrEmpty(fromStation))
            {
                updates.Add("FromStation = @FromStation");
                parameters.Add(new SqlParameter("@FromStation", fromStation));
            }
            if (!string.IsNullOrEmpty(destStation))
            {
                updates.Add("DestStation = @DestStation");
                parameters.Add(new SqlParameter("@DestStation", destStation));
            }
            if (price.HasValue)
            {
                updates.Add("Price = @Price");
                parameters.Add(new SqlParameter("@Price", price.Value));
            }
            if (seatsAvailable.HasValue)
            {
                updates.Add("SeatsAvailable = @SeatsAvailable");
                parameters.Add(new SqlParameter("@SeatsAvailable", seatsAvailable.Value));
            }

            if (updates.Count == 0)
            {
                Console.WriteLine("No updates provided.");
                Console.ReadLine();
                return;
            }

            var updateQuery = $"UPDATE Trains SET {string.Join(", ", updates)} WHERE Tno = @Tno";
            parameters.Add(new SqlParameter("@Tno", tno));

            try
            {
                DatabaseHelper.ExecuteNonQuery(updateQuery, parameters.ToArray());
                Console.WriteLine("Train updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void DeleteTrain()
        {
            Console.Clear();
            Console.Write("Enter Train Number to Delete: ");
            var tno = int.Parse(Console.ReadLine());

            var query = "DELETE FROM Trains WHERE Tno = @Tno";
            var parameters = new[]
            {
                new SqlParameter("@Tno", tno)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                Console.WriteLine("Train deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void ViewAllBookings()
        {
            Console.Clear();
            var query = "SELECT * FROM Bookings";

            var table = DatabaseHelper.ExecuteQuery(query);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"Booking ID: {row["BookingId"]}, Train No: {row["Tno"]}, User ID: {row["UserId"]}, Seats: {row["NumberOfSeats"]}, Date: {row["BookingDate"]}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void ViewAllCancellations()
        {
            Console.Clear();
            var query = "SELECT C.CancellationId, C.BookingId, C.NumberOfSeatsCancelled, C.CancellationDate, B.Tno, B.UserId " +
                        "FROM Cancellations C " +
                        "JOIN Bookings B ON C.BookingId = B.BookingId";

            var table = DatabaseHelper.ExecuteQuery(query);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"Cancellation ID: {row["CancellationId"]}, Booking ID: {row["BookingId"]}, Train No: {row["Tno"]}, User ID: {row["UserId"]}, Seats Cancelled: {row["NumberOfSeatsCancelled"]}, Date: {row["CancellationDate"]}");
                }
            }
            else
            {
                Console.WriteLine("No cancellations found.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }


        private static void ManageUsers()
        {
            Console.Clear();
            Console.WriteLine("1. View All Users");
            Console.WriteLine("2. Delete User");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                ViewAllUsers();
            }
            else if (choice == "2")
            {
                DeleteUser();
            }
        }

        private static void ViewAllUsers()
        {
            Console.Clear();
            var query = "SELECT * FROM Users";

            var table = DatabaseHelper.ExecuteQuery(query);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"User ID: {row["UserId"]}, Username: {row["Username"]}");
                }
            }
            else
            {
                Console.WriteLine("No users found.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void DeleteUser()
        {
            Console.Clear();
            Console.Write("Enter User ID to Delete: ");
            var userId = int.Parse(Console.ReadLine());

            var query = "DELETE FROM Users WHERE UserId = @UserId";
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                Console.WriteLine("User deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }

    public static class DatabaseHelper
    {
        private static string connectionString = @"data source=ICS-LT-17YRQ73\SQLEXPRESS; initial catalog=RailwayDB;" + "user id=sa; password=santu2001;";

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static void ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
