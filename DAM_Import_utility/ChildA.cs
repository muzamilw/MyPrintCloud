using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWatcher
{
    public class ChildA : BaseRepository<MyModel>
    {
        
        private void myTest()
        {
            var aa = this.Update();
            var test = Class1.FirstName;
            Class2 cl2 = new Class2();
            Class1.FirstName = "Naveed";

        }

        public override long Create()
        {
            throw new NotImplementedException();
        }
    }

    public class MyModel
    {
        
    }

}
