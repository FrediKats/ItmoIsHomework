﻿using System.Collections.Generic;
using ServeYourself.Core.VisitablePoints;
using ServeYourself.Core.VisitorInputStream;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.ServeServices
{
    public class SimpleServeService : IServeService
    {
        private readonly IVisitable _endpoint;
        private readonly IVisitorInputStream _visitorInputStream;
        private readonly IVisitable _shop;
        private readonly IVisitable _multiWorkerShop;

        public SimpleServeService()
        {
            _visitorInputStream = new RandomVisitorInputStream(ServeConfiguration.RandomInputStreamMaxDelay);
            _shop = new DummyShop();
            _multiWorkerShop = new MultiWorkerShop();
            _endpoint = new ServiceEndpoint();
        }

        public void Iteration()
        {
            _endpoint.GetServedClientList();

            _shop.Invoke();
            _multiWorkerShop.Invoke();

            List<IVisitor> served = _shop.GetServedClientList();
            served.AddRange(_multiWorkerShop.GetServedClientList());
            served.ForEach(c => _endpoint.AddClient(c, 0));

            List<IVisitor> newClients = _visitorInputStream.GenerateClientStream(ServeConfiguration.DeltaTime);
            newClients.ForEach(c => _shop.AddClient(c, ServeConfiguration.DummyClientTransitionTime));
            newClients.ForEach(c => _multiWorkerShop.AddClient(c, ServeConfiguration.DummyClientTransitionTime));
        }

        public string GetStatistic()
        {
            return _shop.GetStatistic().ToString();
        }

        public List<IVisitable> GetAllVisitableList()
        {
            return new List<IVisitable> {_shop, _multiWorkerShop};
        }
    }
}