﻿using System;
using Microsoft.TeamFoundation.Client;
using TFS = Microsoft.TeamFoundation.WorkItemTracking.Client;
using TFSAggregator.TfsFacade;

namespace TFSAggregator
{
    /// <summary>
    /// Singleton Used to access TFS Data.  This keeps us from connecting each and every time we get an update.
    /// </summary>
    public class Store
    {
        private readonly string _tfsServerUrl;
        public Store(string tfsServerUrl)
        {
            _tfsServerUrl = tfsServerUrl;
        }

        private TFSAccess _access;
        public TFSAccess Access
        {
            get { return _access ?? (_access = new TFSAccess(_tfsServerUrl)); }
        }
    }

    /// <summary>
    /// Don't use this class directly.  Use the StoreSingleton.
    /// </summary>
    public class TFSAccess
    {
        private readonly TFS.WorkItemStore _store;
        public TFSAccess(string tfsUri)
        {
            TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(tfsUri));
            _store = (TFS.WorkItemStore)tfs.GetService(typeof(TFS.WorkItemStore));
        }

        public WorkItem GetWorkItem(int workItemId)
        {
            return new WorkItem(_store.GetWorkItem(workItemId));
        }
    }
}