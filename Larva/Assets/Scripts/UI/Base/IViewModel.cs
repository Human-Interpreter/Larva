using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Larva.UI.Base
{
    public interface IViewModel
    {
        public void NotifyPropertyChanged(string propertyName);
    }
}
