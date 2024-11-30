using Microsoft.AspNetCore.SignalR;

namespace DiabetesApp.API.Hubs
{
	public class ChatHub:Hub
	{
		public async Task SendNotificationToUser(string userId, string message, string patientId)
		{
			// Check if the current logged-in user's userId matches the one you want to send the message to
			if (Context.UserIdentifier == userId)
			{
				// If they match, send the notification to the user with the specified userId
				var notification = new
				{
					Message = message,
					PatientId = patientId  // Add the patientId to the notification payload
				};

				// Send the notification including both message and patientId
				await Clients.User(userId).SendAsync("ReceiveMessage", notification);
			}
			
		}

	}
}
