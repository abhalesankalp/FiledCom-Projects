using Payment.EntityFramework;
using Payment.IRepository;
using Payment.Models;
using SendGrid.Helpers.Errors.Model;
using System;
using Polly;

namespace Payment.Repository
{
    public class PremiumPaymentService : IPaymentGateway
    {
        public int ProcessPayment(TransectionDetails transectionDetails)
        {
            int retryCount = 0;
            var jitterer = new Random();

            var exponentailBackoffPolicy = Polly.Retry.RetryPolicy
                                            .Handle<TooManyRequestsException>()
                                            .WaitAndRetry(3,
                                                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) // exponential backoff retry strategy
                                                              + TimeSpan.FromMilliseconds(jitterer.Next(0, 2000)) // add a jitter number for high-thruput scenarios
                                            );
            var isTransectionProcess = exponentailBackoffPolicy.ExecuteAndCapture(() =>
            {
                retryCount++;
                using (var context = new Transectioncontext())
                {
                    context.TransectionDetails.Add(transectionDetails);
                    if (context.SaveChanges() > 0)
                    {
                        transectionDetails.PaymentState = new PaymentState() { TransectionID = transectionDetails.ID, PaymentStatusID = 1 };
                        context.SaveChanges();
                    }

                }
            });

            return 0;
        }

    }
}
