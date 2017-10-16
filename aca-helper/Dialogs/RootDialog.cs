using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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
            var responseMessages = Responses.GetResponses("None");

            foreach (var responseMessage in responseMessages)
            {
                string message = string.Format(responseMessage.Message, result.Query);

                await context.PostAsync(message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ImportantDates")]
        public async Task ImportantDates(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var start2018 = new DateTime(2018, 01, 01);
            var start2019 = new DateTime(2019, 01, 01);

            var responseMessages = Responses.GetResponses("ImportantDates");

            Chronic.Parser parser = new Chronic.Parser();
            EntityRecommendation date = new EntityRecommendation();
            Chronic.Span dateResult;
            result.TryFindEntity("builtin.datetimeV2.date", out date);
            if (date != null)
            {
                dateResult = parser.Parse(date.Entity);

                if (dateResult.Start < start2018 || dateResult.Start >= start2019)
                {
                    string _message = $"I'm assuming you're asking for the important dates to sign up for coverage for 2018.";

                    await context.PostAsync(_message);
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

                if (entityStart < start2018 || entityEnd >= start2019)
                {
                    string _message = $"I'm assuming you're asking for the important dates to sign up for coverage for 2018.";

                    await context.PostAsync(_message);
                }

            }

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("PlanAndPrices")]
        public async Task PlanAndPrices(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("PlanAndPrices");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("GettingReady")]
        public async Task GettingReady(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("GettingReady");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("IsSavingMoney")]
        public async Task IsSavingMoney(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("IsSavingMoney");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SubmitDocuments")]
        public async Task SubmitDocuments(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("SubmitDocuments");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("InconmeChange")]
        public async Task InconmeChange(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("InconmeChange");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("WaysToApply")]
        public async Task WaysToApply(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("WaysToApply");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("InformationBeforeApply")]
        public async Task InformationBeforeApply(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("InformationBeforeApply");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CostSharingReductions")]
        public async Task CostSharingReductions(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("CostSharingReductions");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("EstimatingIncome")]
        public async Task EstimatingIncome(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("EstimatingIncome");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("IncludedInHousehold")]
        public async Task IncludedInHousehold(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("IncludedInHousehold");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("TotalHealthCareCost")]
        public async Task TotalHealthCareCost(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("TotalHealthCareCost");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("Greeting");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("HowAreYou")]
        public async Task HowAreYou(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            var responseMessages = Responses.GetResponses("HowAreYou");

            foreach (var responseMessage in responseMessages)
            {
                string _message = string.Format(responseMessage.Message);

                await context.PostAsync(_message);
            }

            context.Wait(this.MessageReceived);
        }
    }
}