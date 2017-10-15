using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aca_helper
{
    public static class Responses
    {
        public static List<Response> ResponseList
        {
            get
            {
                return new List<Response>
                {
                    new Response()
                    {
                        Order = 1,
                        Intent = "None",
                        Message = "Sorry, I did not understand {0}. Try something that makes sense."
                    },

                    new Response()
                    {
                        Order = 1,
                        Intent = "ImportantDates",
                        Message = "Open Enrollment begins November 1, 2017 and it ends December 15, 2017. After December 15, you can still buy a health plan if you qualify for a Special Enrollment Period (https://www.healthcare.gov/glossary/special-enrollment-period). If you buy any plans, coverage starts on January 1, 2018."
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "PlanAndPrices",
                        Message = "2018 plans and prices will be available for preview shortly before Open Enrollment starts on November 1, 2017"
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "PlanAndPrices",
                        Message = "For now, read these Marketplace tips: https://www.healthcare.gov/quick-guide"
                    },
                    new Response()
                    {
                        Order = 3,
                        Intent = "PlanAndPrices",
                        Message = "And use this checklist to gather everything you’ll need to apply: " +
                $"https://marketplace.cms.gov/outreach-and-education/marketplace-application-checklist.pdf"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "GettingReady",
                        Message = "It's important that you take the time to get ready"
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "GettingReady",
                        Message = "First, you can go to this website to see you're eligible to apply: https://www.healthcare.gov/quick-guide/eligibility"
                    },
                    new Response()
                    {
                        Order = 3,
                        Intent = "GettingReady",
                        Message = "Then, you can go through this checklist to make sure you're ready: https://marketplace.cms.gov/outreach-and-education/marketplace-application-checklist.pdf"
                    },
                    new Response()
                    {
                        Order = 4,
                        Intent = "GettingReady",
                        Message = "And finally, you can get an overview of the Marketplace here: https://www.healthcare.gov/quick-guide"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "IsSavingMoney",
                        Message = "Your savings depend on your expected household income for 2018. To get a quick idea if you’ll save, " +
                $"go here: https://www.healthcare.gov/lower-costs. We'll tell you if your income's in the saving range. But you'll find out " +
                $"exactly how much you'll save when you apply."
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "SubmitDocuments",
                        Message = "The method for uploading documents online depends on the information you're providing"
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "SubmitDocuments",
                        Message = "If you need to confirm application information, like your income, check out this quick guide with pictures: " +
                $"https://www.healthcare.gov/downloads/howto-uploaddocs-datamatching-FINAL.pdf"
                    },
                    new Response()
                    {
                        Order = 3,
                        Intent = "SubmitDocuments",
                        Message = "If you need to confirm your Special Enrollment Period eligibility, like if you lost other health coverage or moved, check out this quick guide with pictures: " +
                $"https://www.healthcare.gov/downloads/SEPV-how-to-upload-docs_final.pdf"
                    },
                    new Response()
                    {
                        Order = 4,
                        Intent = "SubmitDocuments",
                        Message = "If you need to verify your identity if ID proofing was unsuccessful, check out this quick guide with pictures: " +
                $"https://www.healthcare.gov/downloads/how-to-verify-identity-Final.pdf"
                    },
                    new Response()
                    {
                        Order = 5,
                        Intent = "SubmitDocuments",
                        Message = "If you don't want to submit your documents online, go to the following page for information: " +
                $"https://www.healthcare.gov/tips-and-troubleshooting/uploading-documents/#by-mail"
                    },
                    new Response()
                    {
                        Order = 6,
                        Intent = "SubmitDocuments",
                        Message = "You can find general information on submitting your documents here: " +
                $"https://www.healthcare.gov/tips-and-troubleshooting/uploading-documents"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "InconmeChange",
                        Message = $"Update your application as soon as possible. If you don’t, your savings could be wrong " +
                $"and you could wind up paying higher premiums or owing money when you file taxes."
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "InconmeChange",
                        Message = $"Here you can see what changes to report: " +
                $"https://www.healthcare.gov/reporting-changes/which-changes-to-report"
                    },
                    new Response()
                    {
                        Order = 3,
                        Intent = "InconmeChange",
                        Message = $"And here you can learn how to report your changes: " +
                $"https://www.healthcare.gov/reporting-changes/how-to-report-changes"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "WaysToApply",
                        Message = $"You can apply online, by phone, with the help of a trained assister in your community, " +
                $"through an agent or broker, or with a paper application. Here's some information: " +
                $"https://www.healthcare.gov/apply-and-enroll/how-to-apply/#howtoapply"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "InformationBeforeApply",
                        Message = $"When you apply, you’ll provide details about your household, income, and any coverage you currently have. " +
                $"You can use this checklist to make sure you're ready: " +
                $"https://marketplace.cms.gov/outreach-and-education/marketplace-application-checklist.pdf"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "CostSharingReductions",
                        Message = $"\"Cost sharing reductions\" lower your out-of-pocket costs for health insurance. If you " +
                $"qualify, you must enroll in a Silver plan to get these extra savings. With Silver, you’ll have a pretty low " +
                $"premium, with a lower deductible and lower costs whenever you go to the doctor or have other medical expenses."
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "CostSharingReductions",
                        Message = $"You can find more information here: " +
                $"https://www.healthcare.gov/lower-costs/save-on-out-of-pocket-costs"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "EstimatingIncome",
                        Message = $"The Marketplace bases savings on your estimated income for the year you want coverage – not last year's."
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "EstimatingIncome",
                        Message = $"You can find information on how to estimate you expected income here: " +
                $"https://www.healthcare.gov/income-and-household-information/how-to-report"
                    },
                    new Response()
                    {
                        Order = 3,
                        Intent = "EstimatingIncome",
                        Message = $"And here you can information on what counts as income: " +
                $"https://www.healthcare.gov/income-and-household-information/income"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "IncludedInHousehold",
                        Message = $"Most households include the person applying for coverage, their spouse (if married), " +
                $"and anybody they claim as a tax dependent - including those who don't need health coverage. When you " +
                $"apply, you can say who needs coverage and who doesn’t"
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "IncludedInHousehold",
                        Message = $"You can find more information here: " +
                $"https://www.healthcare.gov/income-and-household-information/household-size"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "TotalHealthCareCost",
                        Message = $"To learn how your deductible, premium, and other costs work together to make up your total health care cost, go here: " +
                $"https://www.healthcare.gov/choose-a-plan/your-total-costs"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "Greeting",
                        Message = $"Hello, how can I help you?"
                    },
                    new Response()
                    {
                        Order = 1,
                        Intent = "HowAreYou",
                        Message = $"I'm a robot. How bad can things be?"
                    },
                    new Response()
                    {
                        Order = 2,
                        Intent = "HowAreYou",
                        Message = $"How can I help you?"
                    }
                };
            }
        }

        public static List<Response> GetResponses(string intent)
        {
            return ResponseList.Where(x => x.Intent == intent).OrderBy(x => x.Order).ToList();
        }
    }

    public class Response
    {
        public int Order { get; set; }
        public string Intent { get; set; }
        public string Message { get; set; }
    }
}