using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdamOneilSoftware
{
    public abstract class Document : INotifyPropertyChanged
    {
        private Dictionary<string, object> _values = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        public string Filename { get; protected set; }
        [XmlIgnore]
        public bool IsModified { get; protected set; }

        // thanks to http://stackoverflow.com/questions/2246777/raise-an-event-whenever-a-propertys-value-changed

        protected void Set<T>(T newValue, [CallerMemberName]string propertyName = null)
        {
            bool changed = false;

            if (!_values.ContainsKey(propertyName))
            {
                _values.Add(propertyName, newValue);
                changed = true;                
            }
            else
            {
                if (!EqualityComparer<T>.Default.Equals(_values[propertyName]))
                {
                    _values[propertyName] = newValue;
                    changed = true;
                }
            }

            if (changed)
            {
                IsModified = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected T Get<T>(T defaultValue = default(T), [CallerMemberName]string propertyName = null)
        {
            return (_values.ContainsKey(propertyName)) ? (T)_values[propertyName] : defaultValue;
        }

        public static T Load<T>(string fileName) where T : Document
        {
            var doc = XmlSerializerHelper.Load<T>(fileName);
            doc.Filename = fileName;
            return doc;
        }

        public bool SaveAs(string fileName)
        {
            Filename = Filename;
            return Save();            
        }

        public bool Save(bool createFolder = true)
        {
            if (string.IsNullOrEmpty(Filename))
            {
                string fileName;
                if (GetFilename(out fileName))
                {
                    Filename = fileName;
                }
                else
                {
                    return false;
                }                
            }

            XmlSerializerHelper.Save(this, Filename, createFolder);
            IsModified = false;
            return true;
        }

        protected abstract bool GetFilename(out string fileName);        
    }
}
