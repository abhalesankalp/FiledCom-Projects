using System;
using Microsoft.AspNetCore.Mvc;
using Payment.IRepository;
using Payment.Models;
using Payment.Repository;

namespace Payment.Controllers
{
    public class PaymentController : Controller
    {
        public IPaymentGateway _paymentGateway;
        public PaymentController(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

        [HttpPost]
        public ActionResult ProcessPayment([FromBody]TransectionDetails transectionDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (transectionDetails.Amount < 20)
                    {
                        _paymentGateway = new CheapPaymentGateway();
                    }

                    else if (transectionDetails.Amount > 20 && transectionDetails.Amount < 501)
                    {
                        _paymentGateway = new ExpensivePaymentGateway();
                    }

                    else
                    {
                        _paymentGateway = new PremiumPaymentService();
                    }

                    int result = _paymentGateway.ProcessPayment(transectionDetails);

                    if (result == 0 && _paymentGateway.GetType() == typeof(ExpensivePaymentGateway))
                    {
                        _paymentGateway = new CheapPaymentGateway();
                        result = _paymentGateway.ProcessPayment(transectionDetails);
                    }

                    if (result > 0)
                    {
                        return Ok();
                    }

                    return StatusCode(500);
                }
                else
                {
                    return BadRequest();
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}