﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Linq;
using System;

namespace aca_helper
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly TranslationService translationService;

        public MessagesController()
        {
            this.translationService = new TranslationService();
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            var foreignLanguage = "";

            if (!string.IsNullOrEmpty(activity.Locale) && !activity.Locale.ToLower().StartsWith("en"))
            {
                if (checkIfTranslationAvailable(activity.Locale))
                {
                    foreignLanguage = activity.Locale;
                }

            } 

            if (activity.Type == ActivityTypes.Message)
            {
                foreignLanguage = translationService.DetectLanguage(activity.Text);

                if (!string.IsNullOrEmpty(foreignLanguage))
                {
                    if (foreignLanguage.ToLower().StartsWith("en"))
                    {
                        foreignLanguage = "";
                    }
                    else
                    {
                        activity.Text = translationService.TranslateText(activity.Text, "en");
                    }
                }

                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog(foreignLanguage));
            }
            else
            {
                await HandleSystemMessage(activity, foreignLanguage);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task<Activity> HandleSystemMessage(Activity message, string foreignLanguage = "")
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                {
                    ConnectorClient client = new ConnectorClient(new Uri(message.ServiceUrl));

                    var reply = message.CreateReply();

                    var replyText = "Hello, I'm the Affordable Care Act (ACA) helper bot. I will try to answer any questions you may have, but just keep in mind that I'm still being trained, so be patient.";

                    if (!string.IsNullOrEmpty(foreignLanguage))
                    {
                        reply.Text = translationService.TranslateText(replyText, foreignLanguage);
                    }
                    else
                    {
                        reply.Text = replyText;
                    }

                    await client.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        private bool checkIfTranslationAvailable(string locale)
        {
            var availableLanguages = translationService.GetLanguagesForTranslate();

            for(var i = 0; i < availableLanguages.Count; i++)
            {
                if (locale == availableLanguages[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}