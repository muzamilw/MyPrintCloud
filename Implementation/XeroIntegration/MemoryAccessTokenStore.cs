﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Common;
using Xero.Api.Infrastructure.Interfaces;

namespace MPC.Implementation.XeroIntegration
{
    public class MemoryAccessTokenStore : ITokenStore
    {
        private static readonly IDictionary<string, IToken> _tokens = new ConcurrentDictionary<string, IToken>();

        public IToken Find(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            IToken token;

            _tokens.TryGetValue(userId, out token);
            return token;
        }

        public void Add(IToken token)
        {
            _tokens[token.UserId] = token;
        }

        public void Delete(IToken token)
        {
            if (_tokens.ContainsKey(token.UserId))
            {
                _tokens.Remove(token.UserId);
            }
        }
    }
}
