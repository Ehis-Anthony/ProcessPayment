using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProcessPayment.Models;
using ProcessPayment.Repositories;

namespace ProcessPayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProcessController : ControllerBase
    {
        #region repositories
        private UnitOfWork _unitOfWork;
        #endregion

        // POST api/values
        public async Task<IActionResult> Payment(paymentProcessViewModel model)
        {
            //Setting values for the business rules
            int bL1 = 20; //First Business Rule
            int bL2 = 500; //Second Business Rule

            //Checking for invalid request
            if (!ModelState.IsValid)
            {
                return BadRequest("The request is invalid: 400 bad request");
            }

            //Forming the objects to be passed to the any of the payment gateway if the condition of the business logic is met
            var gatewayParams = new paymentGatewayParametersViewModel
            {
                creditCardNumber = model.creditCardNumber,
                cardHolder = model.cardHolder,
                expirationDate = model.expirationDate,
                securityCode = model.securityCode,
                amount = model.amount
            };

            //Checking if the amount to be paid is less that bL1
            if (model.amount <= bL1)
            {
                //If business logic is true, send parameters to ICheapPaymentGateway

                //Sending the parameters converted to JSON to ICheapPayment GateWay
                var postData = JsonConvert.SerializeObject(gatewayParams);

                // Send the JSON parameters to ICheapPayment Gateway URL endpoint
                //var client = "URL goes in here"; 
                var request = ""; //Parsing the request from the endpoint

                //If payment is successful at the ICheapPayment Gateway endpoint
                if (request == "successfull")
                {
                    var completePaymentTransaction = await SavePaymentProcessAndState(model, request);

                    if(completePaymentTransaction)
                    {
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }

                //If payment is not successful at the ICheapPayment Gateway endpoint
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

            }


            //Checking if the amount to be paid is greater than bL1 and lesser than bL2
            if (model.amount > bL1 || model.amount <= bL2)
            {
                //If business logic is true, send parameters to IExpensivePaymentGateway

                //Sending the parameters converted to JSON to IExpensivePayment GateWay
                var postData = JsonConvert.SerializeObject(gatewayParams);

                // Send the JSON parameters to IExpensivePayment Gateway URL endpoint
                //var client = "URL goes in here"; 
                var request = ""; //Parsing the request from the endpoint

                //If payment is successful at the IExpensivePayment Gateway endpoint
                if (request == "successfull")
                {
                    var completePaymentTransaction = await SavePaymentProcessAndState(model, request);

                    if (completePaymentTransaction)
                    {
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }

                //If payment is not successful at the IExpensivePayment Gateway endpoint
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }

            //Checking if the amount to be paid is greater than bL2
            if (model.amount > bL2)
            {
                //If business logic is true, send parameters to IPremiumPaymentGateway

                //Sending the parameters converted to JSON to IPremiumPayment GateWay
                var postData = JsonConvert.SerializeObject(gatewayParams);

                // Send the JSON parameters to IPremiumPayment Gateway URL endpoint
                //var client = "URL goes in here"; 
                var request = ""; //Parsing the request from the endpoint

                //If payment is successful at the IPremiumPayment Gateway endpoint
                if (request == "successfull")
                {
                    var completePaymentTransaction = await SavePaymentProcessAndState(model, request);

                    if (completePaymentTransaction)
                    {
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }

                //If payment is not successful at the IPremiumPayment Gateway endpoint
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }





        //Function to store Payment and Payment State entities
        public async Task<bool> SavePaymentProcessAndState(paymentProcessViewModel model, string response)
        {
            var savePaymentProcess = new PRProcessPayment
            {
                creditCardNumber = model.creditCardNumber,
                cardHolder = model.cardHolder,
                expirationDate = model.expirationDate,
                securityCode = model.securityCode,
                amount = model.amount
            };

            var savePaymentState = new PaymentState
            {
                paymentState = response,
                processPaymentID = savePaymentProcess.processPaymentID

            };

            await _unitOfWork.PaymentProcess.AddAsync(savePaymentProcess);
            await _unitOfWork.PaymentState.AddAsync(savePaymentState);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}