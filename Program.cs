 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
namespace lab1
{
  class Program
  {
    static void Main(string[] args)
    {
    }

    interface Movable
{
  Vector<int> position 
  {
    get {
      return position;
    }
    set
    {
      position = value;
    }

  }
  Vector<int> velocity 
  {
    get {
      return velocity;
    }
    set
    {
      velocity = value;
    }
  

  }
  void Move()
  {
    this.position[0] = this.position[0] + this.velocity[0];
  }

}

  }
}