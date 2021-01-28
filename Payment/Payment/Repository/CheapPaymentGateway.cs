using Payment.EntityFramework;
using Payment.IRepository;
using Payment.Models;

namespace Payment.Repository
{
    public class CheapPaymentGateway : IPaymentGateway
    {
        public int ProcessPayment(TransectionDetails transectionDetails)
        {
            using (var context = new Transectioncontext())
            {
                context.TransectionDetails.Add(transectionDetails);
                if (context.SaveChanges() > 0)
                {
                    transectionDetails.PaymentState = new PaymentState() { TransectionID = transectionDetails.ID, PaymentStatusID = 1 };
                    context.SaveChanges();
                    return 1;
                }

            }
            return 0;
        }

    }
}
