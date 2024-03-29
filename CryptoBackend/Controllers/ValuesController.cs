﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoBackend.Integrations;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBackend.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var cex = new CexIntegration();
            cex.UpdateOrderbook();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var cex = new CexIntegration();
            var bitfinex = new BitfinexIntegration();
            var kraken = new KrakenIntegration();
            var binance = new BinanceIntegration();
            RecurringJob.AddOrUpdate(() => cex.UpdateCoinDetails(), Cron.Minutely);
            RecurringJob.AddOrUpdate(() => bitfinex.UpdateCoinDetails(), Cron.Minutely);
            RecurringJob.AddOrUpdate(() => kraken.UpdateCoinDetails(), Cron.Minutely);
            RecurringJob.AddOrUpdate(() => binance.UpdateCoinDetails(), Cron.Minutely);
            return "ok";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
