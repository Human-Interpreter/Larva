using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Larva.UI.Base
{
    public abstract class BaseModel : IModel, INotifyPropertyChanged, IEquatable<BaseModel>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual bool Equals(BaseModel other)
        {
            return object.ReferenceEquals(this, other);
        }
    }
}
