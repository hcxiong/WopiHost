using System;
using System.Collections.Generic;
using System.Timers;

namespace WopiHost
{
    public class EditSessionManager
    {
        private static volatile EditSessionManager m_instance;
        private static object m_syncObj = new object();
        private Dictionary<string, EditSession> m_sessions;
        private Timer m_timer;
        private readonly int m_timeout = 60 * 60 * 1000;
        private readonly int m_closewait = 3 * 60 * 60;

        public static EditSessionManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncObj)
                    {
                        if (m_instance == null)
                            m_instance = new EditSessionManager();
                    }
                }
                return m_instance;
            }
        }

        public EditSessionManager()
        {
            m_timer = new Timer(m_timeout);
            m_timer.AutoReset = true;
            m_timer.Elapsed += CleanUp;
            m_timer.Enabled = true;

            m_sessions = new Dictionary<string, EditSession>();
        }

        public EditSession GetSession(string sessionId)
        {
            EditSession es;

            lock (m_syncObj)
            {
                if (!m_sessions.TryGetValue(sessionId, out es))
                {
                    return null;
                }
            }

            return es;
        }

        public void AddSession(EditSession session)
        {
            lock (m_syncObj)
            {
                m_sessions.Add(session.SessionId, session);
            }
        }

        public void DelSession(EditSession session)
        {
            lock (m_syncObj)
            {
                // clean up
                session.Dispose();
                m_sessions.Remove(session.SessionId);
            }
        }

        private void CleanUp(object sender, ElapsedEventArgs e)
        {
            lock (m_syncObj)
            {
                foreach (var session in m_sessions.Values)
                {
                    if (session.LastUpdated.AddSeconds(m_closewait) < DateTime.Now)
                    {
                        // save the changes to the file
                        session.Save();

                        // clean up
                        session.Dispose();
                        m_sessions.Remove(session.SessionId);
                    }
                }
            }
        }
    }
}
