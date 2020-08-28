using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

using System.IO;
using System.Reflection;


    [Serializable]
    [XmlRoot("XMLSavedSearchRoot")]
    public class SavedSearch : IComparable
    {
        #region SaveName
        private string _SaveName = "";
        public string SaveName
        {
            get { return _SaveName; }
            set { _SaveName = value; }
        }
        #endregion

        #region SearchFor
        private string _SearchFor = "";
        public string SearchFor
        {
            get { return _SearchFor; }
            set { _SearchFor = value; }
        }
        #endregion

        #region StatusFilter
        private string _Status = "";
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        #endregion

        #region ProcessFilter
        private string _ProcessFilter = "";

        public string ProcessFilter
        {
            get { return _ProcessFilter; }
            set { _ProcessFilter = value; }
        }
        #endregion

        #region SortKey
        private string _SortKey = "";
        public string SortKey
        {
            get 
            {
                if (_SaveName.ToUpper().Trim() == "DEFAULT")
                    return "*" + _SaveName; // this will place Default as the first record when sorting
                else
                    return _SaveName;
            }
            set { _SortKey = value; }
        }
        #endregion

        public int CompareTo(object obj)
        {
            if (obj is SavedSearch)
            {
                SavedSearch ss = (SavedSearch)obj;
                return SortKey.CompareTo(ss.SortKey);
            }
            throw new ArgumentException("Object is not a SavedSearch.");    
        }

        public virtual string SerializeFromList(List<SavedSearch> ss)
        {
            string result = string.Empty;

            XmlSerializer Serializer = new XmlSerializer(ss.GetType());

            StringWriter sw = new StringWriter();

            Serializer.Serialize(sw, ss);

            result = sw.ToString();

            return result;
        }

        public virtual List<SavedSearch> DeserializeToList<T>(string SerializeString)
        {
            List<SavedSearch> result = new List<SavedSearch>();

            Type t = result.GetType();

            XmlSerializer serializer = new XmlSerializer(t);

            StringReader sr = new StringReader(SerializeString);

            object objReturned = serializer.Deserialize(sr);

            for (int i = 0; i < ((List<SavedSearch>)objReturned).Count; i++)
            {
                SavedSearch ss = new SavedSearch();
                result.Add(ss);
            }

            t = this.GetType();

            PropertyInfo[] props = t.GetProperties();

            foreach (PropertyInfo pro in props)
            {
                object[] attributes = pro.GetCustomAttributes(true);
                bool continued = false;

                foreach (object objIter in attributes)
                    if (objIter.GetType().Equals(typeof(System.Xml.Serialization.XmlIgnoreAttribute)))
                    {
                        continued = true;
                        break;
                    }

                if (continued)
                    continue;

                for (int i = 0; i < ((List<SavedSearch>)objReturned).Count; i++)
                {
                    t.GetProperty(pro.Name).SetValue(result[i],
                            t.GetProperty(pro.Name).GetValue(((List<SavedSearch>)objReturned)[i], null), null);

                }
            }
            return result;
        }
    }
