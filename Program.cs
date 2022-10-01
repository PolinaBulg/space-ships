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
  

}

class Move
  {
    void move(Movable movableObject) 
    {
      
        movableObject.position =  movableObject.position + movableObject.velocity;
      

    }
  }
  }


  
}
