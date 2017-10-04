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
            string message = $"Sorry, I did not understand '{result.Query}'. Try something that makes sense.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ImportantDates")]
        public async Task ImportantDates(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response = $"Open Enrollment begins November 1, 2017 and it ends December 15, 2017. After December 15, you can still buy a health plan if you qualify for a Special Enrollment Period (https://www.healthcare.gov/glossary/special-enrollment-period). If you buy any plans, coverage starts on January 1, 2018.";

            var start2018 = new DateTime(2018, 01, 01);
            var start2019 = new DateTime(2019, 01, 01);

            string correctResponse = $"Open Enrollment begins November 1, 2017 and it ends December 15, 2017. After December 15, you can still buy a health plan if you qualify for a Special Enrollment Period (https://www.healthcare.gov/glossary/special-enrollment-period). If you buy any plans, coverage starts on January 1, 2018.";

            Chronic.Parser parser = new Chronic.Parser();
            EntityRecommendation date = new EntityRecommendation();
            Chronic.Span dateResult;
            result.TryFindEntity("builtin.datetimeV2.date", out date);
            if (date != null)
            {
                dateResult = parser.Parse(date.Entity);

                if (dateResult.Start >= start2018 && dateResult.Start < start2019)
                {
                    response = correctResponse;
                }
                else
                {
                    response = $"I'm assuming you're asking for the important dates for signing up for coverage for 2018. " + correctResponse;
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
                    response = correctResponse;
                } else
                {
                    response = $"I'm assuming you're asking for the important dates for signing up for coverage for 2018. " + correctResponse;
                }

            }

            await context.PostAsync(response);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("PlanAndPrices")]
        public async Task PlanAndPrices(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"2018 plans and prices will be available for preview shortly before Open Enrollment starts on November 1, 2017";
            await context.PostAsync(response_one);

            string response_two = $"For now, read these Marketplace tips: https://www.healthcare.gov/quick-guide";
            await context.PostAsync(response_two);

            string response_three = $"And use this checklist to gather everything you’ll need to apply: " +
                $"https://marketplace.cms.gov/outreach-and-education/marketplace-application-checklist.pdf";
            await context.PostAsync(response_three);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("GettingReady")]
        public async Task GettingReady(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"It's important that you take the time to get ready";
            await context.PostAsync(response_one);

            string response_two = $"First, you can go to this website to see you're eligible to apply: https://www.healthcare.gov/quick-guide/eligibility";
            await context.PostAsync(response_two);

            string response_three = $"Then, you can go through this checklist to make sure you're ready: https://marketplace.cms.gov/outreach-and-education/marketplace-application-checklist.pdf";
            await context.PostAsync(response_three);

            string response_four = $"And finally, you can get an overview of the Marketplace here: https://www.healthcare.gov/quick-guide";
            await context.PostAsync(response_four);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("IsSavingMoney")]
        public async Task IsSavingMoney(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response = $"Your savings depend on your expected household income for 2018. To get a quick idea if you’ll save, " +
                $"go here: https://www.healthcare.gov/lower-costs. We'll tell you if your income's in the saving range. But you'll find out " +
                $"exactly how much you'll save when you apply.";

            await context.PostAsync(response);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SubmitDocuments")]
        public async Task SubmitDocuments(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            string response_one = $"The method for uploading documents online depends on the information you're providing";
            await context.PostAsync(response_one);

            string response_two = $"If you need to confirm application information, like your income, check out this quick guide with pictures: " +
                $"https://www.healthcare.gov/downloads/howto-uploaddocs-datamatching-FINAL.pdf";
            await context.PostAsync(response_two);

            string response_three = $"If you need to confirm your Special Enrollment Period eligibility, like if you lost other health coverage or moved, check out this quick guide with pictures: " +
                $"https://www.healthcare.gov/downloads/SEPV-how-to-upload-docs_final.pdf";
            await context.PostAsync(response_three);

            string response_four = $"If you need to verify your identity if ID proofing was unsuccessful, check out this quick guide with pictures: " +
                $"https://www.healthcare.gov/downloads/how-to-verify-identity-Final.pdf";
            await context.PostAsync(response_four);

            string response_five = $"If you don't want to submit your documents online, go to the following page for information: " +
                $"https://www.healthcare.gov/tips-and-troubleshooting/uploading-documents/#by-mail";
            await context.PostAsync(response_five);

            string response_six = $"You can find general information on submitting your documents here: " +
                $"https://www.healthcare.gov/tips-and-troubleshooting/uploading-documents";
            await context.PostAsync(response_six);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("InconmeChange")]
        public async Task InconmeChange(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"Update your application as soon as possible. If you don’t, your savings could be wrong " +
                $"and you could wind up paying higher premiums or owing money when you file taxes.";
            await context.PostAsync(response_one);

            string response_two = $"Here you can see what changes to report: " +
                $"https://www.healthcare.gov/reporting-changes/which-changes-to-report";
            await context.PostAsync(response_two);

            string response_three = $"And here you can learn how to report your changes: " +
                $"https://www.healthcare.gov/reporting-changes/how-to-report-changes";
            await context.PostAsync(response_three);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("WaysToApply")]
        public async Task WaysToApply(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"You can apply online, by phone, with the help of a trained assister in your community, " +
                $"through an agent or broker, or with a paper application. Here's some information: " +
                $"https://www.healthcare.gov/apply-and-enroll/how-to-apply/#howtoapply";
            await context.PostAsync(response_one);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("InformationBeforeApply")]
        public async Task InformationBeforeApply(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"When you apply, you’ll provide details about your household, income, and any coverage you currently have. " +
                $"You can use this checklist to make sure you're ready: " +
                $"https://marketplace.cms.gov/outreach-and-education/marketplace-application-checklist.pdf";
            await context.PostAsync(response_one);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CostSharingReductions")]
        public async Task CostSharingReductions(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"\"Cost sharing reductions\" lower your out-of-pocket costs for health insurance. If you " +
                $"qualify, you must enroll in a Silver plan to get these extra savings. With Silver, you’ll have a pretty low " +
                $"premium, with a lower deductible and lower costs whenever you go to the doctor or have other medical expenses.";
            await context.PostAsync(response_one);

            string response_two = $"You can find more information here: " +
                $"https://www.healthcare.gov/lower-costs/save-on-out-of-pocket-costs";
            await context.PostAsync(response_two);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("EstimatingIncome")]
        public async Task EstimatingIncome(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"The Marketplace bases savings on your estimated income for the year you want coverage – not last year's.";
            await context.PostAsync(response_one);

            string response_two = $"You can find information on how to estimate you expected income here: " +
                $"https://www.healthcare.gov/income-and-household-information/how-to-report";
            await context.PostAsync(response_two);

            string response_three = $"And here you can information on what counts as income: " +
                $"https://www.healthcare.gov/income-and-household-information/income";
            await context.PostAsync(response_three);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("IncludedInHousehold")]
        public async Task IncludedInHousehold(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string response_one = $"Most households include the person applying for coverage, their spouse (if married), " +
                $"and anybody they claim as a tax dependent - including those who don't need health coverage. When you " +
                $"apply, you can say who needs coverage and who doesn’t";
            await context.PostAsync(response_one);

            string response_two = $"You can find more information here: " +
                $"https://www.healthcare.gov/income-and-household-information/household-size";
            await context.PostAsync(response_two);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("TotalHealthCareCost")]
        public async Task TotalHealthCareCost(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            string response_one = $"To learn how your deductible, premium, and other costs work together to make up your total health care cost, go here: " +
                $"https://www.healthcare.gov/choose-a-plan/your-total-costs";
            await context.PostAsync(response_one);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Insult")]
        public async Task Insult(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            string response_one = $"Stop acting like a child.";
            await context.PostAsync(response_one);

            context.Wait(this.MessageReceived);
        }
    }
}