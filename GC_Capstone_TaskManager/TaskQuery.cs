using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
namespace GC_Capstone_TaskManager
{
    class TaskQuery
    {
        #region fields
        private string property;
        private string query;
        #endregion
        #region properties
        public string Property
        {
            get
            {
                return property;
            }
            set
            {
                property = value;
            }
        }
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
            }
        }
        #endregion
        #region constructors
        public TaskQuery(string _property, string _query)
        {
            property = _property;
            query = _query;
        }
        public TaskQuery() { }
        #endregion
    }
}
