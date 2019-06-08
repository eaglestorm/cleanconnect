using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AutoMapper;
using CleanConnect.Core.Dal.Record;
using CleanConnect.Core.Model.Authorization;
using CleanConnect.Core.Model.Client;
using CleanConnect.Infrastructure.Record;
using Marten;

namespace CleanConnect.Infrastructure.Repository
{
    public class AuthorizationRequestRepository: IAuthorizationRequestRepository
    {
        private readonly DocumentStore _store;
        private readonly IMapper _mapper;
        private readonly IDictionary<long,AuthorizationRequestRecord> _cache = new ConcurrentDictionary<long, AuthorizationRequestRecord>();

        public AuthorizationRequestRepository(DocumentStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }
        
        
        public void Save(AuthenticationRequest request)
        {
            using (var session = _store.OpenSession())
            {
                var record = _mapper.Map<AuthorizationRequestRecord>(request);
                session.Store(record);
                session.SaveChanges();
                InValidateCache(record);                
            }
        }

        public AuthenticationRequest Get(long id)
        {
            AuthenticationRequest client = TryGetCache(id);
            if (client == null)
            {
                using (var session = _store.OpenSession())
                {
                    var record = session.LoadAsync<AuthorizationRequestRecord>(id);
                    AddToCache(record.Result);
                    client = _mapper.Map<AuthenticationRequest>(record.Result);
                }
            }
            return client;
        }
        
        private void AddToCache(AuthorizationRequestRecord record)
        {
            if (!_cache.ContainsKey(record.Id))
            {
                _cache.Add(record.Id,record);
            }
        }
        
        private AuthenticationRequest TryGetCache(long id)
        {
            if (_cache.ContainsKey(id))
            {
                var record = _cache[id];
                return _mapper.Map<AuthenticationRequest>(record);
            }

            return null;
        }
        
        private void InValidateCache(AuthorizationRequestRecord client)
        {
            if (_cache.ContainsKey(client.Id))
            {
                _cache.Remove(client.Id);
            }
        }
    }
}