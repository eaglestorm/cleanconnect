﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CleanConnect.Core.Dal.Record;
using CleanConnect.Core.Model.Client;
using Marten;

namespace CleanConnect.Infrastructure
{
    public class ClientRepository: IClientRepository
    {
        private readonly DocumentStore _store;
        private readonly IMapper _mapper;
        private readonly IDictionary<Guid,ClientRecord> _cache = new ConcurrentDictionary<Guid, ClientRecord>();

        public ClientRepository(DocumentStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }
        
        public IList<Client> GetAll()
        {
            using (var session = _store.OpenSession())
            {
                var records = session.Query<ClientRecord>().ToList();
                foreach (var record in records)
                {
                    AddToCache(record);
                }
                return _mapper.Map<IList<Client>>(records);
            }
        }

        public Client Get(Guid id)
        {
            Client client = TryGetCache(id);
            if (client == null)
            {
                using (var session = _store.OpenSession())
                {
                    var record = session.LoadAsync<ClientRecord>(id);
                    AddToCache(record.Result);
                    client = _mapper.Map<Client>(record.Result);
                }
            }
            return client;
        }        

        public void Save(Client client)
        {
            using (var session = _store.OpenSession())
            {
                var record = _mapper.Map<ClientRecord>(client);
                session.Store(record);
                session.SaveChanges();
                InValidateCache(client);                
            }
        }

        private void AddToCache(ClientRecord record)
        {
            if (!_cache.ContainsKey(record.Id))
            {
                _cache.Add(record.Id,record);
            }
        }
        
        private Client TryGetCache(Guid id)
        {
            if (_cache.ContainsKey(id))
            {
                var record = _cache[id];
                return _mapper.Map<Client>(record);
            }

            return null;
        }

        private void InValidateCache(Client client)
        {
            if (_cache.ContainsKey(client.Id))
            {
                _cache.Remove(client.Id);
            }
        }
    }
}