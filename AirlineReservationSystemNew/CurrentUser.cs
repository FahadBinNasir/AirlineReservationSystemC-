namespace AirlineReservationSystemNew
{
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }

        public static void SetCurrentUser(int userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
        }

        public static void Clear()
        {
            UserId = 0;
            Username = string.Empty;
            Role = string.Empty;
        }
    }
}