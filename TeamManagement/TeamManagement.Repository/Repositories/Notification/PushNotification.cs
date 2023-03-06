using FirebaseAdmin.Messaging;

namespace TeamManagement.Repository.Repositories.Notification
{
    public class PushNotification
    {
        public static async Task SendMessage(string email, string title, string content, Dictionary<string, string> dataSend)
        {
            try
            {
                var topic = email;
                var message = new FirebaseAdmin.Messaging.Message
                {
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = title,
                        Body = content
                    },
                    Android = new AndroidConfig()
                    {
                        Notification = new AndroidNotification
                        {
                            Color = "#f45342",
                            Sound = "default"
                        }
                    },
                    Data = dataSend,
                    Topic = topic.ToString().Split('@')[0]
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
