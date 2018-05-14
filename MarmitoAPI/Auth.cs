using System;
using System.Collections.Generic;
using MarmitoAPI.Models;

namespace MarmitoAPI
{
    public class Auth
    {
        static private Auth m_auth = null;
        private Dictionary<string, User> m_auths;
        private Auth()
        {
            m_auths = new Dictionary<string, User>();
        }

        public static Auth getAuth()
        {
            if (m_auth == null)
            {
                m_auth = new Auth();
            }
            return m_auth;
        }

        public string logUser(User user)
        {
            Guid guid = Guid.NewGuid();
            string uuidstring = guid.ToString();
            m_auths[uuidstring] = user;
            return uuidstring;
        }

        public long getId(string token)
        {
            return m_auths[token].Id;
        }

        public Boolean isLogged(string tokenValue)
        {
            return m_auths.ContainsKey(tokenValue);
        }

        public User getLoggedUser(string tokenValue)
        {
            return m_auths[tokenValue];
        }
    }
}