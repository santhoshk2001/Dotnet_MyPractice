using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace RailwayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.WriteLine("*********************************************");
                Console.WriteLine("*    Welcome to Railway Reservation System   *");
                Console.WriteLine("*********************************************");
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
            Console.WriteLine("*************");
            Console.WriteLine("*    Register    *");
            Console.WriteLine("*************");
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
            Console.WriteLine("*************");
            Console.WriteLine("*     Login *");
            Console.WriteLine("*************");
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
                    var userId = (int)table.Rows[0][0];
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
                Console.WriteLine("*************");
                Console.WriteLine("*    User Menu*");
                Console.WriteLine("*************");
                Console.ResetColor();

                Console.WriteLine("1. Search Trains");
                Console.WriteLine("2. Book Tickets");
                Console.WriteLine("3. Cancel Tickets");
                Console.WriteLine("4. View Bookings");
                Console.WriteLine("5. Process Refund");
                Console.WriteLine("6. Check Waiting List Status");
                Console.WriteLine("7. Logout");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SearchTrains();
                        break;
                    case "2":
                        BookTickets(userId);
                        break;
                    case "3":
                        CancelTickets(userId);
                        break;
                    case "4":
                        ViewBookings(userId);
                        break;
                    case "5":
                        ProcessRefund(userId);
                        break;
                    case "6":
                        CheckWaitingListStatus(userId);
                        break;
                    case "7":
                        return;
                }
            }
        }

        private static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*************");
                Console.WriteLine("*   Admin Menu   *");
                Console.WriteLine("*************");
                Console.ResetColor();
                Console.WriteLine("1. Add Train");
                Console.WriteLine("2. Update Train");
                Console.WriteLine("3. Delete Train");
                Console.WriteLine("4. View All Bookings");
                Console.WriteLine("5. View All Cancellations");
                Console.WriteLine("6. Manage Users");
                Console.WriteLine("7. View Waiting List");
                Console.WriteLine("8. Logout");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTrain();
                        break;
                    case "2":
                        UpdateTrain();
                        break;
                    case "3":
                        DeleteTrain();
                        break;
                    case "4":
                        ViewAllBookings();
                        break;
                    case "5":
                        ViewAllCancellations();
                        break;
                    case "6":
                        ManageUsers();
                        break;
                    case "7":
                        ViewWaitingList();
                        break;
                    case "8":
                        return;
                }
            }
        }

        private static void SearchTrains()
        {
            Console.Clear();
            Console.Write("Enter Source Station: ");
            var fromStation = Console.ReadLine();

            Console.Write("Enter Destination Station: ");
            var destStation = Console.ReadLine();

            Console.Write("Enter Date of Travel (YYYY-MM-DD): ");
            var dateOfTravel = DateTime.Parse(Console.ReadLine());

            var query = "SELECT * FROM Trains WHERE FromStation = @FromStation AND DestStation = @DestStation AND DateOfTravel = @DateOfTravel";
            var parameters = new[]
            {
                new SqlParameter("@FromStation", fromStation),
                new SqlParameter("@DestStation", destStation),
                new SqlParameter("@DateOfTravel", dateOfTravel)
            };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"Train No: {row["Tno"]}, Name: {row["Tname"]}, Class: {row["ClassOfTravel"]}, Price: {row["Price"]}, Seats Available: {row["SeatsAvailable"]}");
                }
            }
            else
            {
                Console.WriteLine("No trains found.");
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

            Console.Write("Enter Number of Seats: ");
            var numberOfSeats = int.Parse(Console.ReadLine());

            Console.Write("Enter Date of Travel (YYYY-MM-DD): ");
            var dateOfTravel = DateTime.Parse(Console.ReadLine());

            if (numberOfSeats > 3)
            {
                Console.WriteLine("You can book a maximum of 3 tickets at a time.");
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
                return;
            }

            var query = "SELECT SeatsAvailable, Price FROM Trains WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel AND DateOfTravel = @DateOfTravel";
            var parameters = new[]
            {
                new SqlParameter("@Tno", tno),
                new SqlParameter("@ClassOfTravel", classOfTravel),
                new SqlParameter("@DateOfTravel", dateOfTravel)
            };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count == 0)
            {
                Console.WriteLine("Train not found or class not available for the specified date.");
                Console.ReadLine();
                return;
            }

            var seatsAvailable = (int)table.Rows[0]["SeatsAvailable"];
            var price = (decimal)table.Rows[0]["Price"];

            if (numberOfSeats > seatsAvailable)
            {
                Console.WriteLine("Not enough seats available. Would you like to be added to the waiting list? (Y/N)");
                var waitingListChoice = Console.ReadLine().ToUpper();
                if (waitingListChoice == "Y")
                {
                    AddToWaitingList(userId, tno, classOfTravel, numberOfSeats, dateOfTravel);
                }
                return;
            }

            var totalPrice = price * numberOfSeats;

            var insertBookingQuery = "INSERT INTO Bookings (Tno, UserId, NumberOfSeats, BookingDate, ClassOfTravel, DateOfTravel, TotalPrice) VALUES (@Tno, @UserId, @NumberOfSeats, @BookingDate, @ClassOfTravel, @DateOfTravel, @TotalPrice); SELECT SCOPE_IDENTITY();";
            var insertBookingParameters = new[]
            {
                new SqlParameter("@Tno", tno),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@NumberOfSeats", numberOfSeats),
                new SqlParameter("@BookingDate", DateTime.Now),
                new SqlParameter("@ClassOfTravel", classOfTravel),
                new SqlParameter("@DateOfTravel", dateOfTravel),
                new SqlParameter("@TotalPrice", totalPrice)
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
                var bookingId = Convert.ToInt32(DatabaseHelper.ExecuteScalar(insertBookingQuery, insertBookingParameters));
                DatabaseHelper.ExecuteNonQuery(updateSeatsQuery, updateSeatsParameters);
                Console.WriteLine("Booking successful.");
                Console.WriteLine($"Booking ID: {bookingId}");
                Console.WriteLine($"Train Number: {tno}");
                Console.WriteLine($"Class of Travel: {classOfTravel}");
                Console.WriteLine($"Number of Seats: {numberOfSeats}");
                Console.WriteLine($"Date of Travel: {dateOfTravel.ToShortDateString()}");
                Console.WriteLine($"Total Price: {totalPrice}");
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

            var query = "SELECT * FROM Bookings WHERE BookingId = @BookingId AND UserId = @UserId";
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

            var booking = table.Rows[0];
            var tno = (int)booking["Tno"];
            var classOfTravel = (string)booking["ClassOfTravel"];
            var numberOfSeats = (int)booking["NumberOfSeats"];
            var totalPrice = (decimal)booking["TotalPrice"];
            var dateOfTravel = (DateTime)booking["DateOfTravel"];

            // Calculate refund amount
            decimal refundAmount = CalculateRefundAmount(totalPrice, dateOfTravel);

            var deleteBookingQuery = "DELETE FROM Bookings WHERE BookingId = @BookingId";
            var updateSeatsQuery = "UPDATE Trains SET SeatsAvailable = SeatsAvailable + @NumberOfSeats WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel";
            var insertRefundQuery = "INSERT INTO Refunds (BookingId, RefundAmount, RefundDate) VALUES (@BookingId, @RefundAmount, @RefundDate)";

            var deleteBookingParameters = new[] { new SqlParameter("@BookingId", bookingId) };
            var updateSeatsParameters = new[]
            {
                new SqlParameter("@NumberOfSeats", numberOfSeats),
                new SqlParameter("@Tno", tno),
                new SqlParameter("@ClassOfTravel", classOfTravel)
            };
            var insertRefundParameters = new[]
            {
                new SqlParameter("@BookingId", bookingId),
                new SqlParameter("@RefundAmount", refundAmount),
                new SqlParameter("@RefundDate", DateTime.Now)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(deleteBookingQuery, deleteBookingParameters);
                DatabaseHelper.ExecuteNonQuery(updateSeatsQuery, updateSeatsParameters);
                DatabaseHelper.ExecuteNonQuery(insertRefundQuery, insertRefundParameters);

                Console.WriteLine("Cancellation successful.");
                Console.WriteLine($"Refund Amount: {refundAmount:C}");

                // Check waiting list and book for the next person in line
                CheckAndBookFromWaitingList(tno, classOfTravel, numberOfSeats, dateOfTravel);
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
                    Console.WriteLine($"Booking ID: {row["BookingId"]}, Train No: {row["Tno"]}, Class: {row["ClassOfTravel"]}, Seats: {row["NumberOfSeats"]}, Date: {row["DateOfTravel"]}, Total Price: {row["TotalPrice"]:C}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void ProcessRefund(int userId)
        {
            Console.Clear();
            Console.Write("Enter Booking ID for refund: ");
            var bookingId = int.Parse(Console.ReadLine());

            var query = "SELECT * FROM Bookings WHERE BookingId = @BookingId AND UserId = @UserId";
            var parameters = new[]
            {
                new SqlParameter("@BookingId", bookingId),
                new SqlParameter("@UserId", userId)
            };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count == 0)
            {
                Console.WriteLine("Booking not found or you do not have permission to process this refund.");
                Console.ReadLine();
                return;
            }

            var booking = table.Rows[0];
            var tno = (int)booking["Tno"];
            var classOfTravel = (string)booking["ClassOfTravel"];
            var numberOfSeats = (int)booking["NumberOfSeats"];
            var totalPrice = (decimal)booking["TotalPrice"];
            var dateOfTravel = (DateTime)booking["DateOfTravel"];

            decimal refundAmount = CalculateRefundAmount(totalPrice, dateOfTravel);

            var deleteBookingQuery = "DELETE FROM Bookings WHERE BookingId = @BookingId";
            var updateSeatsQuery = "UPDATE Trains SET SeatsAvailable = SeatsAvailable + @NumberOfSeats WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel";
            var insertRefundQuery = "INSERT INTO Refunds (BookingId, RefundAmount, RefundDate) VALUES (@BookingId, @RefundAmount, @RefundDate)";

            var deleteBookingParameters = new[] { new SqlParameter("@BookingId", bookingId) };
            var updateSeatsParameters = new[]
            {
                new SqlParameter("@NumberOfSeats", numberOfSeats),
                new SqlParameter("@Tno", tno),
                new SqlParameter("@ClassOfTravel", classOfTravel)
            };
            var insertRefundParameters = new[]
            {
                new SqlParameter("@BookingId", bookingId),
                new SqlParameter("@RefundAmount", refundAmount),
                new SqlParameter("@RefundDate", DateTime.Now)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(deleteBookingQuery, deleteBookingParameters);
                DatabaseHelper.ExecuteNonQuery(updateSeatsQuery, updateSeatsParameters);
                DatabaseHelper.ExecuteNonQuery(insertRefundQuery, insertRefundParameters);

                Console.WriteLine("Refund processed successfully.");
                Console.WriteLine($"Refund Amount: {refundAmount:C}");

                CheckAndBookFromWaitingList(tno, classOfTravel, numberOfSeats, dateOfTravel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static decimal CalculateRefundAmount(decimal totalPrice, DateTime dateOfTravel)
        {
            var daysUntilTravel = (dateOfTravel - DateTime.Now).Days;
            if (daysUntilTravel > 7)
            {
                return totalPrice * 0.9m;
            }
            else if (daysUntilTravel > 3)
            {
                return totalPrice * 0.75m;
            }
            else if (daysUntilTravel > 1)
            {
                return totalPrice * 0.5m;
            }
            else
            {
                return totalPrice * 0.25m;
            }
        }

        private static void AddToWaitingList(int userId, int tno, string classOfTravel, int numberOfSeats, DateTime dateOfTravel)
        {
            var query = "INSERT INTO WaitingList (UserId, Tno, ClassOfTravel, NumberOfSeats, DateOfTravel, RequestDate) VALUES (@UserId, @Tno, @ClassOfTravel, @NumberOfSeats, @DateOfTravel, @RequestDate)";
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Tno", tno),
                new SqlParameter("@ClassOfTravel", classOfTravel),
                new SqlParameter("@NumberOfSeats", numberOfSeats),
                new SqlParameter("@DateOfTravel", dateOfTravel),
                new SqlParameter("@RequestDate", DateTime.Now)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                Console.WriteLine("Added to waiting list successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void CheckWaitingListStatus(int userId)
        {
            Console.Clear();
            var query = "SELECT * FROM WaitingList WHERE UserId = @UserId ORDER BY RequestDate";
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId)
            };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"Waiting ID: {row["WaitingId"]}, Train No: {row["Tno"]}, Class: {row["ClassOfTravel"]}, Seats: {row["NumberOfSeats"]}, Date: {row["DateOfTravel"]}, Request Date: {row["RequestDate"]}");
                }
            }
            else
            {
                Console.WriteLine("You are not on any waiting list.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void CheckAndBookFromWaitingList(int tno, string classOfTravel, int availableSeats, DateTime dateOfTravel)
        {
            var query = "SELECT TOP 1 * FROM WaitingList WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel AND DateOfTravel = @DateOfTravel AND NumberOfSeats <= @AvailableSeats ORDER BY RequestDate";
            var parameters = new[]
            {
                new SqlParameter("@Tno", tno),
                new SqlParameter("@ClassOfTravel", classOfTravel),
                new SqlParameter("@DateOfTravel", dateOfTravel),
                new SqlParameter("@AvailableSeats", availableSeats)
            };

            var table = DatabaseHelper.ExecuteQuery(query, parameters);
            if (table.Rows.Count > 0)
            {
                var waitingBooking = table.Rows[0];
                var userId = (int)waitingBooking["UserId"];
                var numberOfSeats = (int)waitingBooking["NumberOfSeats"];
                var waitingId = (int)waitingBooking["WaitingId"];

                // Book the ticket for the waiting user
                BookTickets(userId);

                // Remove from waiting list
                var deleteWaitingQuery = "DELETE FROM WaitingList WHERE WaitingId = @WaitingId";
                var deleteWaitingParameters = new[] { new SqlParameter("@WaitingId", waitingId) };
                DatabaseHelper.ExecuteNonQuery(deleteWaitingQuery, deleteWaitingParameters);

                Console.WriteLine($"Booked ticket for user on waiting list (User ID: {userId})");
            }
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

            Console.Write("Enter Price: ");
            var price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Seats Available: ");
            var seatsAvailable = int.Parse(Console.ReadLine());

            Console.Write("Enter Train Status (active/inactive): ");
            var trainStatus = Console.ReadLine();

            Console.Write("Enter Date of Travel (YYYY-MM-DD): ");
            var dateOfTravel = DateTime.Parse(Console.ReadLine());

            var query = "INSERT INTO Trains (Tno, Tname, FromStation, DestStation, ClassOfTravel, Price, SeatsAvailable, TrainStatus, DateOfTravel) " +
                        "VALUES (@Tno, @Tname, @FromStation, @DestStation, @ClassOfTravel, @Price, @SeatsAvailable, @TrainStatus, @DateOfTravel)";
            var parameters = new[]
            {
                new SqlParameter("@Tno", tno),
                new SqlParameter("@Tname", tname),
                new SqlParameter("@FromStation", fromStation),
                new SqlParameter("@DestStation", destStation),
                new SqlParameter("@ClassOfTravel", classOfTravel),
                new SqlParameter("@Price", price),
                new SqlParameter("@SeatsAvailable", seatsAvailable),
                new SqlParameter("@TrainStatus", trainStatus),
                new SqlParameter("@DateOfTravel", dateOfTravel)
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

            Console.Write("Enter Class of Travel to Update: ");
            var classOfTravel = Console.ReadLine();

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

            var updateQuery = $"UPDATE Trains SET {string.Join(", ", updates)} WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel";
            parameters.Add(new SqlParameter("@Tno", tno));
            parameters.Add(new SqlParameter("@ClassOfTravel", classOfTravel));

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

            Console.Write("Enter Class of Travel to Delete: ");
            var classOfTravel = Console.ReadLine();

            var query = "DELETE FROM Trains WHERE Tno = @Tno AND ClassOfTravel = @ClassOfTravel";
            var parameters = new[]
            {
                new SqlParameter("@Tno", tno),
                new SqlParameter("@ClassOfTravel", classOfTravel)
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
                    Console.WriteLine($"Booking ID: {row["BookingId"]}, Train No: {row["Tno"]}, User ID: {row["UserId"]}, Class: {row["ClassOfTravel"]}, Seats: {row["NumberOfSeats"]}, Date: {row["DateOfTravel"]}, Total Price: {row["TotalPrice"]:C}");
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
            var query = "SELECT R.RefundId, R.BookingId, R.RefundAmount, R.RefundDate, B.Tno, B.UserId, B.ClassOfTravel " +
                        "FROM Refunds R " +
                        "JOIN Bookings B ON R.BookingId = B.BookingId";

            var table = DatabaseHelper.ExecuteQuery(query);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"Refund ID: {row["RefundId"]}, Booking ID: {row["BookingId"]}, Train No: {row["Tno"]}, User ID: {row["UserId"]}, Class: {row["ClassOfTravel"]}, Refund Amount: {row["RefundAmount"]:C}, Refund Date: {row["RefundDate"]}");
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

        private static void ViewWaitingList()
        {
            Console.Clear();
            var query = "SELECT * FROM WaitingList ORDER BY RequestDate";

            var table = DatabaseHelper.ExecuteQuery(query);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"Waiting ID: {row["WaitingId"]}, User ID: {row["UserId"]}, Train No: {row["Tno"]}, Class: {row["ClassOfTravel"]}, Seats: {row["NumberOfSeats"]}, Date: {row["DateOfTravel"]}, Request Date: {row["RequestDate"]}");
                }
            }
            else
            {
                Console.WriteLine("No entries in the waiting list.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }

    public static class DatabaseHelper
    {
        private static string connectionString = @"data source=ICS-LT-17YRQ73\SQLEXPRESS; initial catalog=RailwayReservationSystem;" + "user id=sa; password=santu2001;";

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

        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                return command.ExecuteScalar();
            }
        }
    }
}
