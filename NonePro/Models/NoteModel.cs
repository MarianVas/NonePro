using System;
using System.ComponentModel;
namespace NonePro.Models
{
    class NoteModel : INotifyPropertyChanged
    {
        private string _header;
        private string _text;
        public string CreationTime { get; set; } = DateTime.Now.ToString();

        public int NoteID { get; set; }

        public string Header
        { 
            get { return _header; }
            set
            {
                if (_header == value)
                    return;
                _header = value;
                OnPropertyChanged("Header");
            } 
        }

        public string Text
        {
            get { return _text; }
            set {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
