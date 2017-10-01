using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace aca_helper.Dialogs
{
    [LuisModel("79467c0c-6079-45cc-b70b-4d1326894d31", "c1b58a39279a44fdb7c3351c33da98fb")]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Try something that makes sense.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ImportantDates")]
        public async Task ImportantDates(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response = "";

            var start2018 = new DateTime(2018, 01, 01);
            var start2019 = new DateTime(2019, 01, 01);

            Chronic.Parser parser = new Chronic.Parser();
            EntityRecommendation date = new EntityRecommendation();
            Chronic.Span dateResult;
            result.TryFindEntity("builtin.datetimeV2.date", out date);
            if (date != null)
            {
                dateResult = parser.Parse(date.Entity);

                if (dateResult.Start >= start2018 && dateResult.Start < start2019)
                {
                    response = $"I understand what you said, but someone is training me on how to respond. Thanks for your patience";
                }
                else
                {
                    response = $"Do you mean 2018?";
                }
            }

            EntityRecommendation dateRange = new EntityRecommendation();
            result.TryFindEntity("builtin.datetimeV2.daterange", out dateRange);
            if (dateRange != null)
            {
                //dateRangeResult = parser.Parse(dateRange.Entity);

                DateTime entityStart = new DateTime(), entityEnd = new DateTime();

                var testing = dateRange.Resolution["values"];

                var first = testing.ToString();

                var objects = JArray.Parse(first); // parse as array  
                foreach (JObject root in objects)
                {
                    string timex = "", start = "", end = "";

                    foreach (KeyValuePair<String, JToken> app in root)
                    {
                        var key = app.Key;

                        if (key == "timex")
                        {
                            timex = (String)app.Value;
                        }
                        else if (key == "start")
                        {
                            start = (String)app.Value;
                        }
                        else if (key == "end")
                        {
                            end = (String)app.Value;
                        }
                    }

                    if (!string.IsNullOrEmpty(start))
                    {
                        var parts = start.Split('-');
                        entityStart = new DateTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
                    }

                    if (!string.IsNullOrEmpty(end))
                    {
                        var parts = start.Split('-');
                        entityEnd = new DateTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
                    }
                }

                if (entityStart >= start2018 && entityEnd <= start2019)
                {
                    response = $"I understand what you said, but someone is training me on how to respond. Thanks for your patience";
                } else
                {
                    response = $"Do you mean 2018?";
                }

            }

            await context.PostAsync(response);

            context.Wait(this.MessageReceived);
        }
    }
}