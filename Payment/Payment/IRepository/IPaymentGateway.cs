using Payment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.IRepository
{
    public interface IPaymentGateway
    {
        int ProcessPayment(TransectionDetails transectionDetails);
    }
}
